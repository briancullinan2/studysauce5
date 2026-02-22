using DataLayer.Entities;

namespace StudySauce.Shared.Pages.Course
{
    public class StudyPlan : DataLayer.Generators.IGenerator<DataLayer.Entities.Card>
    {
        public static IEnumerable<DataLayer.Entities.Card> Generate()
        {
            return [];
        }
    }
}
