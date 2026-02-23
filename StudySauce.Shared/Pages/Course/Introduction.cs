using DataLayer.Customization;
using DataLayer.Entities;

namespace StudySauce.Shared.Pages.Course
{
    public class Introduction : DataLayer.Generators.IGenerator<DataLayer.Entities.Card>
    {
        public static IEnumerable<DataLayer.Entities.Card> Generate()
        {
            return [
                new Card
                {
                    PackId = 1, // Introduction Pack
                    Content = "What grade are you in?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "High school student", Value = "highschool" },
                        new Answer { Content = "College Freshman", Value = "college-freshman" },
                        new Answer { Content = "College Sophomore", Value = "college-sophomore" },
                        new Answer { Content = "College Junior", Value = "college-junior" },
                        new Answer { Content = "College Senior", Value = "college-senior" },
                        new Answer { Content = "Graduate student", Value = "graduate" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Which do you agree with more regarding academic ability?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Some people are born good at academics.", Value = "born", IsCorrect = false },
                        new Answer { Content = "People become good at academics through experience and building skills.", Value = "practice", IsCorrect = true }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "How do you manage your time studying for exams?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "I space out my studying far in advance to avoid stress.", Value = "advance", IsCorrect = true },
                        new Answer { Content = "I try to space it out, but usually end up cramming.", Value = "cram", IsCorrect = false },
                        new Answer { Content = "I do my best work under pressure and plan to cram.", Value = "pressure", IsCorrect = false }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "How do you manage your electronic devices when you study?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "I keep them nearby and respond to texts immediately.", Value = "on", IsCorrect = false },
                        new Answer { Content = "I turn them off or put them somewhere they won't distract me.", Value = "off", IsCorrect = true }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "How much do you study per day?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "0-1 hour", Value = "one" },
                        new Answer { Content = "1-2 hours", Value = "two" },
                        new Answer { Content = "2-4 hours", Value = "four" },
                        new Answer { Content = "4+ hours", Value = "more" }
                    }
                }
            ];
        }
    }
}