using DataLayer.Customization;
using DataLayer.Entities;

namespace StudySauce.Shared.Pages.Course
{
    public class Partners : DataLayer.Generators.IGenerator<DataLayer.Entities.Card>
    {
        public static IEnumerable<DataLayer.Entities.Card> Generate()
        {

            return [
                // Question 1: How accountability partners help
                new Card
                {
                    Content = "Select the two main ways an accountability partner can help you in school from the list below:",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "To motivate you", Value = "motivate", IsCorrect = true },
                        new Answer { Content = "Tutoring for your most difficult classes", Value = "tutor", IsCorrect = false },
                        new Answer { Content = "Help keep you focused", Value = "focus", IsCorrect = true },
                        new Answer { Content = "To incentivize you to achieve your goals", Value = "incentive", IsCorrect = false }
                    }
                },

                // Question 2: Key attributes
                new Card
                {
                    Content = "Which of the following is not a key attribute to look for when choosing your accountability partner?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Someone you trust.", Value = "trust", IsCorrect = false },
                        new Answer { Content = "Someone that will challenge you.", Value = "challenge", IsCorrect = false },
                        new Answer { Content = "Someone that knows you best.", Value = "knows", IsCorrect = true },
                        new Answer { Content = "Someone that will celebrate your successes.", Value = "celebrate", IsCorrect = false }
                    }
                },

                // Question 3: Frequency of communication
                new Card
                {
                    Content = "How often should you talk with your accountability partner?",
                    ResponseType = CardType.Short,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Ideally, you can communicate with your accountability partner on a weekly basis.", Value = "weekly", IsCorrect = true }
                    }
                },

                // Question 4: Other examples of use
                new Card
                {
                    Content = "According to the video, which of the following are examples of other ways accountability partners are used?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Learning to drive", Value = "drive", IsCorrect = false },
                        new Answer { Content = "Dieting", Value = "dieting", IsCorrect = true },
                        new Answer { Content = "Gyms", Value = "gyms", IsCorrect = true },
                        new Answer { Content = "Churches", Value = "churches", IsCorrect = true }
                    }
                }
            ];
        }
    }
}
