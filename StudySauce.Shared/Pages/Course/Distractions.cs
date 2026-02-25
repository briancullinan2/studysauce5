using DataLayer.Customization;
using DataLayer.Entities;

namespace StudySauce.Shared.Pages.Course
{
    public class Distractions : DataLayer.Generators.IGenerator<DataLayer.Entities.Card>
    {
        public static IEnumerable<DataLayer.Entities.Card> Generate()
        {
            return [
    // Question 1: Multitasking Truth
    new Card
    {
                Content = "True or False. You are excellent at multitasking.",
        ResponseType = CardType.Multiple,
        Answers = new List<Answer>
        {
            new Answer { Content = "True", Value = "true", IsCorrect = false },
            new Answer { Content = "False", Value = "false", IsCorrect = true }
        }
    },

    // Question 2: Downsides of Multitasking
    new Card
    {
                Content = "Which of the following is NOT a downside of multitasking?",
        ResponseType = CardType.Multiple,
        Answers = new List<Answer>
        {
            new Answer { Content = "Get tired more easily", Value = "tired", IsCorrect = false },
            new Answer { Content = "Shorter memory of material", Value = "shorter", IsCorrect = true },
            new Answer { Content = "Remember less", Value = "remember", IsCorrect = false },
            new Answer { Content = "Takes longer to study", Value = "longer", IsCorrect = false }
        }
    },

    // Question 3: Impact of Technology Distractions
    new Card
    {
                Content = "How much lower do students interrupted by technology score on tests (in research studies)?",
        ResponseType = CardType.Multiple,
        Answers = new List<Answer>
        {
            new Answer { Content = "10%", Value = "10", IsCorrect = false },
            new Answer { Content = "20%", Value = "20", IsCorrect = true },
            new Answer { Content = "30%", Value = "30", IsCorrect = false },
            new Answer { Content = "40%", Value = "40", IsCorrect = false }
        }
    },

    // Question 4: Recovery Time from Distraction
    new Card
    {
                Content = "How long can a text message distract you from your optimal study state?",
        ResponseType = CardType.Multiple,
        Answers = new List<Answer>
        {
            new Answer { Content = "1-3 minutes", Value = "3", IsCorrect = false },
            new Answer { Content = "5-15 minutes", Value = "15", IsCorrect = false },
            new Answer { Content = "25-40 minutes", Value = "40", IsCorrect = true },
            new Answer { Content = "45-60 minutes", Value = "60", IsCorrect = false }
        }
    }
                ];
        }
    }
}
