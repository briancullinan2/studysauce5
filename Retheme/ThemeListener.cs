using Antlr4.Runtime;
using Retheme.Grammars;

namespace Retheme
{

    public class ThemeListener : css3ParserBaseListener
    {
        public List<ThemeData> Themes { get; } = new();
        private ThemeData _currentTheme;

        // Triggered when a selector like .theme-deepseakelp is found
        public override void EnterClassName(css3Parser.ClassNameContext context)
        {
            var className = context.GetText().TrimStart('.');
            if (className.StartsWith("theme-"))
            {
                _currentTheme = new ThemeData { ClassName = className };
                Themes.Add(_currentTheme);
            }
        }

        // Change this to match the actual rule name in your css3Parser.g4
        // It might be 'EnterKnownDeclaration' or 'EnterVariableDeclaration'
        public override void EnterDeclaration(css3Parser.DeclarationContext context)
        {
            // If this still isn't hitting, your grammar might be using 'variableDeclaration'
            ProcessDeclaration(context);
        }

        // Add this to catch the specific rule many CSS3 grammars use for variables
        public override void EnterVar_(css3Parser.Var_Context context)
        {
            //ProcessDeclaration(context);
        }

        private void ProcessDeclaration(ParserRuleContext context)
        {
            if (_currentTheme == null) return;

            var text = context.GetText();
            // Check if it looks like a variable: --name: value;
            if (text.StartsWith("--"))
            {
                var parts = text.Split(':', 2);
                if (parts.Length == 2)
                {
                    var propertyName = parts[0].Trim();
                    var valueText = parts[1].TrimEnd(';').Trim();
                    _currentTheme.Variables[propertyName] = valueText;
                }
            }
        }

        // Use the Labeled Alternative name from your .g4 file
        public override void EnterKnownDeclaration(css3Parser.KnownDeclarationContext context)
        {
            if (_currentTheme == null) return;

            // In your grammar: declaration : property_ ':' ws expr prio?
            // property_ handles 'Variable' tokens (your --vars)
            var propNode = context.property_();
            var propText = propNode?.GetText()?.Trim();

            if (propText != null && propText.StartsWith("--"))
            {
                // expr is the rule for the value
                var valueText = context.expr()?.GetText()?.Trim();

                if (valueText != null)
                {
                    _currentTheme.Variables[propText] = valueText;
                }
            }
        }
    }

    public class ThemeData
    {
        public string ClassName { get; set; }
        public Dictionary<string, string> Variables { get; set; } = new();
    }
}
