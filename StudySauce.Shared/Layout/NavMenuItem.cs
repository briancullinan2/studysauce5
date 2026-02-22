using StudySauce.Shared.Utilities.Extensions;

namespace StudySauce.Shared.Layout
{
    public interface INavMenuItem<TChildren> where TChildren : class
    {
        string Title { get; set; }
        string Href { get; set; }
        string Icon { get; set; }
        string? RoleRequired { get; set; }
        bool IsBeta { get; set; }
        bool IsCollapsed { get; set; }
        IEnumerable<INavMenuItem> Children { get; set; }

    }

    public interface INavMenuItem : INavMenuItem<NavMenuItem>
    {
    }

    public class NavMenuItem<TChildren> : INavMenuItem where TChildren : NavMenuItem<TChildren>
    {
        public string Title { get; set; } = string.Empty;
        virtual public string Href { get; set; } = string.Empty;
        public string Icon { get; set; } = "bi-circle";
        public string? RoleRequired { get; set; }
        public bool IsBeta { get; set; } = false;
        public bool IsCollapsed { get; set; } = true; // Added state
        public IEnumerable<TChildren> Children { get; set; } = [];
        IEnumerable<INavMenuItem> INavMenuItem<NavMenuItem>.Children { get => Children; set => Children = value.OfType<TChildren>(); }
    }

    public class NavMenuItem : NavMenuItem<NavMenuItem>
    {

    }

    public class CourseMenuItem : NavMenuItem<CourseMenuItem>
    {
        public Type? Lesson { get; set; }
        public int? Level { get; set; }

        public Type? Course => (Lesson != null && CourseMenu.ParentMap.TryGetValue(Lesson, out var parent))
            ? parent
            : Lesson; // If no parent, it IS the course

        public bool IsCourse => Lesson != null && CourseMenu.CourseMap.ContainsKey(Lesson);

        override public string Href
        {
            get
            {
                if (Lesson == null) throw new InvalidOperationException("I hope you find the missing link");

                // Efficient lookup using our cached maps
                var courseType = Course;
                Level = (courseType != null && CourseMenu.CourseMap.TryGetValue(courseType, out var cid)) ? cid : 0;

                // If this item is a top-level course, just return the Level link
                if (Lesson == courseType)
                {
                    return NavigationExtensions.GetUri<Pages.Course.Course>(c => new() { Level = Level });
                }

                // Otherwise, return the Level + Lesson link
                return NavigationExtensions.GetUri<Pages.Course.Course>(c => new()
                {
                    Level = Level,
                    Lesson = Lesson.Name.ToSafe()
                });
            }
        }
    }
}
