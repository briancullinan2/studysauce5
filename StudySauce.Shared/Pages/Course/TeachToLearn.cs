using DataLayer.Entities;

namespace StudySauce.Shared.Pages.Course
{
    public class TeachToLearn : DataLayer.Generators.IGenerator<DataLayer.Entities.Card>
    {
        public static IEnumerable<DataLayer.Entities.Card> Generate()
        {
            return [];
        }
    }
}
