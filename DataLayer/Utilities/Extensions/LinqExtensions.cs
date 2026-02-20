using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace DataLayer.Utilities.Extensions
{
    public static class LinqExtensions
    {
        public static PropertyInfo FindProperty<TModel, TReturn>(this Expression<Func<TModel, TReturn>> expression)
        {
            // Unrolling the 'Advanced' type from the expression tree
            if (expression.Body is MemberExpression member && member.Member is PropertyInfo prop)
            {
                return prop;
            }

            // Handle boxing if TReturn is 'object' but the property is a struct
            if (expression.Body is UnaryExpression unary && unary.Operand is MemberExpression innerMember)
            {
                return innerMember.Member as PropertyInfo;
            }

            throw new ArgumentException("Could not descend the code tree to find a valid property.");
        }

        private static readonly int _maxDepth = 2;

        public static XDocument ToXDocument(this Expression expression)
        {
            return new XDocument(VisitToXml(expression, 0));
        }

        private static XElement VisitToXml(object node, int currentDepth)
        {
            if (node == null) return new XElement("Null");

            var type = node.GetType();
            var element = new XElement(type.Name);

            // Add the NodeType for easier remapping server-side
            if (node is Expression exp)
            {
                element.Add(new XAttribute("NodeType", exp.NodeType.ToString()));
            }

            // Stop recursion if we've hit the depth limit
            if (currentDepth >= _maxDepth)
            {
                element.Add(new XAttribute("DepthReached", "True"));
                return element;
            }

            // Loop through all public instance properties (The "SOAP" way)
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                // Skip properties that would cause infinite loops or aren't relevant
                if (prop.Name == "CanReduce" || prop.Name == "TailCall") continue;

                try
                {
                    var value = prop.GetValue(node);
                    var propElement = new XElement(prop.Name);

                    if (value is Expression subExpression)
                    {
                        propElement.Add(VisitToXml(subExpression, currentDepth + 1));
                    }
                    else if (value is System.Collections.IEnumerable list && !(value is string))
                    {
                        foreach (var item in list)
                        {
                            propElement.Add(VisitToXml(item, currentDepth + 1));
                        }
                    }
                    else
                    {
                        propElement.Value = value?.ToString() ?? "null";
                    }

                    element.Add(propElement);
                }
                catch { /* Handle properties that might throw during reflection */ }
            }

            return element;
        }

        // A registry to map NodeType to the correct Factory Method
        private static readonly Dictionary<string, ExpressionType> _nodeTypeLookup = Enum.GetValues(typeof(ExpressionType))
            .Cast<ExpressionType>()
            .ToDictionary(t => t.ToString(), t => t);
        private static readonly Dictionary<ExpressionType, MethodInfo> _factoryMap = typeof(Expression)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            // We filter for methods that take Expression arguments to avoid overloads like 'Constant'
            .Where(m => _nodeTypeLookup.ContainsKey(m.Name))
            .GroupBy(m => _nodeTypeLookup[m.Name])
            .ToDictionary(g => g.Key, g => g.First());

        public static Expression ToExpression(this XElement el, DbContext context, out DbSet<Entities.Entity> set)
        {
            set = null;
            var typeStr = el.Attribute("NodeType")?.Value;
            if (!_nodeTypeLookup.TryGetValue(typeStr, out var nodeType))
                return null;

            // Special cases: Parameters and Constants usually need manual handling 
            // because they don't follow the "Children as Expressions" rule perfectly.
            if (nodeType == ExpressionType.Parameter) return BuildParameter(el);
            if (nodeType == ExpressionType.Constant)
            {
                var typeName = el.Element("DataType")?.Value;
                if (typeName.Contains("TranslationContext"))
                {
                    return BuildSource(el, context, out set); // Swap placeholder for Real DB source
                }
                return BuildConstant(el); // Just a regular value
            }
            // Dynamic Lookup via our Factory Map
            if (_factoryMap.TryGetValue(nodeType, out var factoryMethod))
            {
                var parameters = factoryMethod.GetParameters();
                var args = new List<object>();

                foreach (var param in parameters)
                {
                    // Match the XML element name to the Method Parameter name
                    // (e.g., 'left', 'right', 'expression', 'instance')
                    var childElement = el.Element(param.Name);

                    if (childElement != null && childElement.HasElements)
                    {
                        // Recursively reconstruct the child expression
                        args.Add(ToExpression(childElement.Elements().First(), context, out set));
                    }
                    else
                    {
                        // Handle non-expression metadata (like MethodInfo or Property Name)
                        args.Add(ResolveMetadata(param.ParameterType, el.Element(param.Name)?.Value, el));
                    }
                }

                return (Expression)factoryMethod.Invoke(null, args.ToArray());
            }

            throw new NotSupportedException($"No factory found for {nodeType}");
        }

        private static object ResolveMetadata(Type targetType, string? value, XElement el)
        {
            if (targetType == typeof(Type))
                return Type.GetType(value);

            if (targetType == typeof(string))
                return value;

            if (targetType == typeof(ExpressionType))
                return Enum.Parse(typeof(ExpressionType), value);

            if (targetType == typeof(MethodInfo))
            {
                var declaringType = Type.GetType(el.Element("DeclaringType")?.Value);
                var methodName = el.Element("MethodName")?.Value;
                // This is where 'fluffy' reflection pays off
                return declaringType?.GetMethod(methodName);
            }

            return null;
        }

        private static Expression BuildSource(XElement el, DbContext context, out DbSet<Entities.Entity> set)
        {
            var typeName = el.Element("DataType")?.Value;
            var entityType = Type.GetType(typeName);

            // Reflection Sparkle: Find the DbSet<T> property on TranslationContext 
            // where T is our entityType.
            var dbSetProperty = typeof(TranslationContext)
                .GetProperties()
                .FirstOrDefault(p => p.PropertyType.IsGenericType &&
                                     p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                                     p.PropertyType.GetGenericArguments()[0] == entityType);

            if (dbSetProperty != null)
            {
                // Pull the live DbSet instance from your context
                set = dbSetProperty.GetValue(context) as DbSet<Entities.Entity>;
                // TODO: out DbSet until expression can 
                return set.AsQueryable().Expression; // This is the "Real" root for EF Core
            }

            throw new InvalidOperationException($"No DbSet found for {entityType.Name} in TranslationContext.");
        }

        private static ConstantExpression BuildConstant(XElement el)
        {
            var val = el.Element("Value")?.Value;
            var typeName = el.Element("DataType")?.Value;
            var type = Type.GetType(typeName) ?? typeof(string);

            // Use Activator to convert the string value back to the target type
            var convertedValue = Convert.ChangeType(val, type);
            return Expression.Constant(convertedValue, type);
        }

        // Keep a cache of parameters during a single Reconstruction pass
        private static Dictionary<string, ParameterExpression> _parameters = new();

        private static ParameterExpression BuildParameter(XElement el)
        {
            var name = el.Element("Name")?.Value ?? "x";
            var typeName = el.Element("Type")?.Value; // From your 'fluffy' reflection
            var type = Type.GetType(typeName) ?? typeof(object);

            // Important: Re-use the same ParameterExpression object for the same name
            if (!_parameters.TryGetValue(name, out var param))
            {
                param = Expression.Parameter(type, name);
                _parameters.Add(name, param);
            }
            return param;
        }

        public static string ToSerialized(IQueryable query)
        {
            return ToXDocument(query.Expression).ToString();
        }

        public static IQueryable ToQueryable(string query)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(query)))
            {
                reader.MoveToContent();
                XElement root = (XElement)XNode.ReadFrom(reader);

                // 1. Clear the parameter cache for this specific run
                _parameters.Clear();

                // 2. Reconstruct the raw Expression tree
                // TODO: fix this, won't know how until i debug expressions and see what parts of the trees it can show in
                DbSet<Entities.Entity> set;
                Expression finalExpression = root.ToExpression(TranslationContext.Current, out set);
                return TranslationContext.Current.Set<Entities.Entity>().AsQueryable().Provider.CreateQuery(finalExpression);
            }
        }


    }
}
