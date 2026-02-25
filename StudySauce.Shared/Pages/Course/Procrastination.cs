using DataLayer.Customization;
using DataLayer.Entities;

namespace StudySauce.Shared.Pages.Course
{
    public class Procrastination : DataLayer.Generators.IGenerator<DataLayer.Entities.Card>
    {
        public static IEnumerable<DataLayer.Entities.Card> Generate()
        {
            return [
    // Question 1: Types of Memory
    new Card
    {
                Content = "You have short and long term memory. What are these two types of memory also called?",
        ResponseType = CardType.Short,
        Answers = new List<Answer>
        {
            new Answer { Content = "Active Memory", Value = "active memory", IsCorrect = true },
            new Answer { Content = "Reference Memory", Value = "reference memory", IsCorrect = true }
        }
    },

    // Question 2: Goal of Studying
    new Card
    {
                Content = "What is the goal of studying?",
        ResponseType = CardType.Short,
        Answers = new List<Answer>
        {
            new Answer { Content = "To retain information and commit things to long term memory", Value = "retain long term memory", IsCorrect = true }
        }
    },

    // Question 3: Procrastination Cycle
    new Card
    {
                Content = "What is the solution to stopping the procrastination to cramming cycle?",
        ResponseType = CardType.Short,
        Answers = new List<Answer>
        {
            new Answer { Content = "Space out your studying", Value = "space out studying", IsCorrect = true }
        }
    },

    // Question 4: Tools to reduce Procrastination
    new Card
    {
                Content = "What are two tools that you can use to help reduce procrastination?",
        ResponseType = CardType.Multiple,
        Answers = new List<Answer>
        {
            new Answer { Content = "Creating and analyzing deadlines", Value = "deadlines", IsCorrect = true },
            new Answer { Content = "Building a good study plan", Value = "study plan", IsCorrect = true }
        }
    }
                ];
        }
    }
}
