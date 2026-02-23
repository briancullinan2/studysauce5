using System.Reflection;

namespace DataLayer.Customization
{
    // TODO: move
    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum ControlMode
    {
        Unset = 0,
        View = 1,
        Edit = 2,
        Owner = 3,
        Group = 4
    }

    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum PackMode
    {
        //[Description("pack")] // show only one card at a time and return to home
        Card = 1,
        //[Description("quiz")] // show a few cards at a time and return to home
        Quiz = 2,
        //[Description("funnel")] // show a few cards then go to the next step
        Multi = 3,
        //[Description("demo")] // show a few choice cards and go to store page
        Demo = 4
    }


    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum CardType
    {
        Unset = 0,
        FlashCard = 1,
        TrueFalse = 2,
        Multiple = 3,
        Short = 4,
        Match = 5,
    }


    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum PackStatus : int
    {
        Unset = 0,
        Unpublished = 1,
        Published = 2,
        Public = 3,
        Unlisted = 4,
        Deleted = 5
    }


    [Obfuscation(Exclude = true, ApplyToMembers = true)]
    public enum Gender : int
    {
        Female = 1,
        Male = 2,
        Other = 3,
        Unspecified = 0
    }

}
