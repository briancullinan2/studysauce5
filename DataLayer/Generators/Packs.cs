using DataLayer.Customization;
using DataLayer.Entities;

namespace DataLayer.Generators
{
    public class Packs
    {
        public static IEnumerable<Pack> Generate()
        {
            return [
                // Course 1: Foundation
                new Pack { Title = "Level 1: Fundamentals", Description = "Mastering goals, distractions, and study environments.", Tokens = 3, Status = PackStatus.Unpublished, Tags = new() { "Basics", "Focus" } },
                new Pack { Title = "Introduction", Description = "Getting started with effective learning habits.", Tokens = 3, Status = PackStatus.Unpublished, Tags = new() { "Intro" } },
                new Pack { Title = "Setting Goals", Description = "Defining clear, actionable academic objectives.", Tokens = 3, Status = PackStatus.Unpublished, Tags = new() { "Planning" } },
                new Pack { Title = "Distractions", Description = "Strategies to maintain deep focus and block noise.", Tokens = 3, Status = PackStatus.Unpublished },
                new Pack { Title = "Procrastination", Description = "Overcoming the urge to delay important tasks.", Tokens = 3, Status = PackStatus.Unpublished },
                new Pack { Title = "Study Environment", Description = "Optimizing your physical space for maximum retention.", Tokens = 3, Status = PackStatus.Unpublished },

                // Course 2: Systems
                new Pack { Title = "Level 2: Systems", Description = "Metrics, planning, and advanced testing strategies.", Price = 3, Status = PackStatus.Unpublished, Tags = new() { "Efficiency" } },
                new Pack { Title = "Study Metrics", Description = "How to track and measure your learning progress.", Tokens = 3, Status = PackStatus.Unpublished },
                new Pack { Title = "Study Plan", Description = "Building a sustainable and flexible study schedule.", Tokens = 3, Status = PackStatus.Unpublished },
                new Pack { Title = "Interleaving", Description = "Mixing subjects to improve long-term retention.", Tokens = 3, Status = PackStatus.Unpublished },
                new Pack { Title = "Studying for Tests", Description = "Preparation techniques for high-stakes exams.", Tokens = 3, Status = PackStatus.Unpublished },

                // Course 3: Mastery
                new Pack { Title = "Level 3: Mastery", Description = "Advanced active learning and memory techniques.", Price = 3, Status = PackStatus.Unpublished, Tags = new() { "Advanced" } },
                new Pack { Title = "Group Study", Description = "Leveraging social learning for better understanding.", Tokens = 3, Status = PackStatus.Unpublished },
                new Pack { Title = "Teach to Learn", Description = "The Feynman technique: teaching as a mastery tool.", Tokens = 3, Status = PackStatus.Unpublished },
                new Pack { Title = "Spaced Repetition", Description = "Using timing to hack your brain's memory curve.", Tokens = 3, Status = PackStatus.Unpublished }
            ];
        }
    }
}
