using StudySauce.Shared.Layout;
using StudySauce.Shared.Pages.Course;

namespace StudySauce.Shared.Services
{
    public interface ICourseService
    {
        Course.StepMode? Step { get; set; }
        event Action<Course.StepMode?>? OnStepChanged;
        event Action<ICourseMenuItem?>? OnCourseChanged;
        Task SetStep(Course.StepMode? step);
        Task SetCourse(ICourseMenuItem? item);
    }

    public class CourseService : ICourseService
    {
        public event Action<Course.StepMode?>? OnStepChanged;
        public event Action<ICourseMenuItem?>? OnCourseChanged;
        public ICourseMenuItem? Course { get; set; } = null;

        public Course.StepMode? Step { get; set; } = Pages.Course.Course.StepMode.Intro;
        public async Task SetStep(Course.StepMode? step)
        {
            Step = step;
            OnStepChanged?.Invoke(step);
        }

        public async Task SetCourse(ICourseMenuItem? item)
        {
            Course = item;
            OnCourseChanged?.Invoke(item);
        }
    }
}
