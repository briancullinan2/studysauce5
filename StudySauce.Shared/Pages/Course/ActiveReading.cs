using DataLayer.Customization;
using DataLayer.Entities;

namespace StudySauce.Shared.Pages.Course
{
    public class ActiveReading : DataLayer.Generators.IGenerator<DataLayer.Entities.Card>
    {
        public static IEnumerable<DataLayer.Entities.Card> Generate()
        {
            return [
               new Card
               {
                    PackId = 1,
                    Content = "What is active reading?",
                    ResponseType = CardType.Short,
                    Answers = new List<Answer>
                    {
                        new Answer
                        {
                            Content = "Active reading is simply trying to understand what you are reading and recognizing which parts are most important to your needs. The key is being curious.",
                            IsCorrect = true,
                            Value = "active_reading_desc"
                        }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Highlighting and underlining is an effective tool for active reading.",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Skimming through the reading is an effective tool for active reading.",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = true, Value = "1" },
                        new Answer { Content = "False", IsCorrect = false, Value = "0" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Self-explanation is an effective tool for active reading",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = true, Value = "1" },
                        new Answer { Content = "False", IsCorrect = false, Value = "0" }
                    }
                }

            ];
        }
    }
}
