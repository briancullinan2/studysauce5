namespace StudySauce.Shared.Layout
{
    public class NavMenuItem
    {
        public string Title { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public string Icon { get; set; } = "bi-circle";
        public string? RoleRequired { get; set; }
        public bool IsBeta { get; set; }
        public bool IsCollapsed { get; set; } = true; // Added state
        public List<NavMenuItem> Children { get; set; } = new();
    }
}
