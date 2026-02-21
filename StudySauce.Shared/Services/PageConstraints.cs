using System.ComponentModel;

namespace StudySauce.Shared.Services
{

    public enum PackMode
    {
        [Description("pack")] // show only one card at a time and return to home
        Card = 1,
        [Description("quiz")] // show a few cards at a time and return to home
        Quiz = 2,
        [Description("funnel")] // show a few cards then go to the next step
        Multi = 3,
        [Description("demo")] // show a few choice cards and go to store page
        Demo = 4
    }
}
