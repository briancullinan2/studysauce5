using DataLayer.Customization;
using DataLayer.Entities;

namespace DataLayer.Generators
{
    public class Cards : IGenerator<Entities.Card>
    {
        public static IEnumerable<Card> Generate()
        {
            var cards = new Card[]
            {
                // From quiz.html.php (Group Study)
                new Card
                {
                    PackId = 1,
                    Content = "Which of the following are usually bad times to study as a group?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Writing a paper", IsCorrect = true, Value = "writing" },
                        new Answer { Content = "Trying to clarify difficult concepts", IsCorrect = false, Value = "difficult" },
                        new Answer { Content = "Looking at material for the first time", IsCorrect = true, Value = "material" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Study groups should take breaks too.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "True. Try not to meet for too long or everyone will get exhausted and you will be less productive.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = true, Value = "1" },
                        new Answer { Content = "False", IsCorrect = false, Value = "0" }
                    }
                },

                // From quiz.html (1).php (Study Metrics)
                new Card
                {
                    PackId = 1,
                    Content = "Which of the following are reasons to track your study hours?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "By studying a certain number of hours per week, you guarantee you will be prepared.", IsCorrect = false, Value = "guarantee" },
                        new Answer { Content = "Tracking your study time helps you avoid procrastination.", IsCorrect = true, Value = "procrastination" },
                        new Answer { Content = "Tracking your study time is an effective way to see if you are actually studying enough.", IsCorrect = true, Value = "tracking" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Why does everyone else look like they have it all together?",
                    ResponseType = CardType.Short,
                    ResponseContent = "They don't. They just typically put on a brave face so that you will think they are smart. Everyone struggles in school."
                },

                // From quiz.html (2).php (Test Taking)
                new Card
                {
                    PackId = 1,
                    Content = "Leading up to the test, it is a super good idea to cram.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "SAY NO TO CRAMMING!!!",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "What is the name of the breathing exercise demonstrated in this video?",
                    ResponseType = CardType.Short,
                    ResponseContent = "It is called four-part breathing. It is also sometimes called combat or tactical breathing."
                },

                // From quiz.html (3).php (Interleaving)
                new Card
                {
                    PackId = 1,
                    Content = "What is it called when you study the same class material for multiple study session?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Blocked practice."
                },
                new Card
                {
                    PackId = 1,
                    Content = "What is another name for interleaving?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Varied practice."
                },

                // From quiz.html (5).php (Active Reading)
                new Card
                {
                    PackId = 1,
                    Content = "What is active reading?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Active reading is simply trying to understand what you are reading and recognizing which parts are most important to your needs."
                },
                new Card
                {
                    PackId = 1,
                    Content = "Highlighting and underlining is an effective tool for active reading.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False. Highlighting and underlining are passive and can lead to a false sense of security.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                // From quiz.html.php (Active Reading)
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
                },

                // From quiz.html (7).php (Strategies)
                new Card
                {
                    PackId = 1,
                    Content = "Which of the following are examples of self-testing?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Reading and rereading your notes", IsCorrect = false, Value = "reading" },
                        new Answer { Content = "Flash cards", IsCorrect = true, Value = "flash" },
                        new Answer { Content = "Teaching others", IsCorrect = true, Value = "teaching" },
                        new Answer { Content = "Creating practice tests", IsCorrect = true, Value = "practice" }
                    }
                },

                // From quiz.html (8).php (Spaced Repetition)
                new Card
                {
                    PackId = 1,
                    Content = "Spacing out your study sessions isn't important at all.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False. Spaced repetition is critical for long-term retention.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                // From quiz.html.php (Setting Goals)
                new Card
                {
                    PackId = 1,
                    Content = "How much more likely are you to perform at a higher level if you set specific and challenging goals?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "20%", IsCorrect = false, Value = "20" },
                        new Answer { Content = "40%", IsCorrect = false, Value = "40" },
                        new Answer { Content = "90%", IsCorrect = true, Value = "90" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "What are the two types of motivation?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Intrinsic and extrinsic motivation."
                },

                // From quiz.html (1).php (Accountability Partners)
                new Card
                {
                    PackId = 1,
                    Content = "Select the two main ways an accountability partners can help you in school:",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "To motivate you", IsCorrect = true, Value = "motivate" },
                        new Answer { Content = "Tutoring for your most difficult classes", IsCorrect = false, Value = "tutor" },
                        new Answer { Content = "Help keep you focused", IsCorrect = true, Value = "focus" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Besides school, name two other things mentioned where accountability partners are used.",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Dieting", IsCorrect = true, Value = "dieting" },
                        new Answer { Content = "Gyms", IsCorrect = true, Value = "gyms" },
                        new Answer { Content = "Churches", IsCorrect = true, Value = "churches" }
                    }
                },

                // From quiz.html (2).php (Procrastination)
                new Card
                {
                    PackId = 1,
                    Content = "You have short and long term memory. What are these two types of memory also called?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Active memory and Reference memory."
                },
                new Card
                {
                    PackId = 1,
                    Content = "What are two of the most effective tools to reduce procrastination?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Creating and analyzing your deadlines and building a good study plan."
                },

                // From quiz.html (3).php (Introduction)
                new Card
                {
                    PackId = 1,
                    Content = "What grade are you in?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "High school student", IsCorrect = true, Value = "highschool" },
                        new Answer { Content = "College Freshman", IsCorrect = true, Value = "college-freshman" },
                        new Answer { Content = "College Sophomore", IsCorrect = true, Value = "college-sophomore" }
                    }
                },

                // From quiz.html (4).php (Distractions)
                new Card
                {
                    PackId = 1,
                    Content = "True or False. You are excellent at multitasking.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False. You are not good at multitasking. No one is.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "true" },
                        new Answer { Content = "False", IsCorrect = true, Value = "false" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "How long does it take to get back 'into the swing of things' after being distracted by a phone?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "1-3 minutes", IsCorrect = false, Value = "3" },
                        new Answer { Content = "5-15 minutes", IsCorrect = false, Value = "15" },
                        new Answer { Content = "25-40 minutes", IsCorrect = true, Value = "40" }
                    }
                },

                // From quiz.html (6).php (Study Plan)
                new Card
                {
                    PackId = 1,
                    Content = "Multiply your class hours by ___ to get the total number of hours you should study per week.",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "1", IsCorrect = false, Value = "1" },
                        new Answer { Content = "2", IsCorrect = true, Value = "2" },
                        new Answer { Content = "3", IsCorrect = false, Value = "3" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "What are two methods you can use to help yourself stick with a study plan?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Starting immediately to get momentum and choosing study locations ahead of time."
                },

                // From quiz.html (7).php (Environment)
                new Card
                {
                    PackId = 1,
                    Content = "Your bed is a great place to study since getting comfortable is critical to memory retention.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False - Your brain associates your bed with sleeping.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "You should study for several hours to get the greatest benefit of prolonged focus.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False - Taking breaks is critical. Try to study for 50-60 minutes then take a 10 minute break.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                // From quiz.html.php (Setting Goals / Quiz 2)
                new Card
                {
                    PackId = 1,
                    Content = "How much more likely are you to perform at a higher level if you set specific and challenging goals?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "20%", IsCorrect = false, Value = "20" },
                        new Answer { Content = "40%", IsCorrect = false, Value = "40" },
                        new Answer { Content = "90%", IsCorrect = true, Value = "90" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "What does the SMART acronym stand for in goal setting?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Specific, Measurable, Achievable, Relevant, and Time-bound."
                },
                new Card
                {
                    PackId = 1,
                    Content = "What are the two types of motivation?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Intrinsic (motivation from within) and Extrinsic (external rewards like grades)."
                },

                // From quiz.html.php (Accountability Partners / Quiz 6)
                new Card
                {
                    PackId = 1,
                    Content = "Select the two main ways an accountability partner can help you in school:",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "The two main ways are by keeping you focused and motivated.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "To motivate you", IsCorrect = true, Value = "motivate" },
                        new Answer { Content = "Tutoring for your most difficult classes", IsCorrect = false, Value = "tutor" },
                        new Answer { Content = "Help keep you focused", IsCorrect = true, Value = "focus" },
                        new Answer { Content = "To incentivize you to achieve your goals", IsCorrect = false, Value = "incentive" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Which of the following is NOT a key attribute to look for when choosing your accountability partner?",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "You should choose someone you trust, who will challenge you and celebrate successes. 'Someone that knows you best' is not necessarily a requirement.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Someone you trust.", IsCorrect = false, Value = "trust" },
                        new Answer { Content = "Someone that will challenge you.", IsCorrect = false, Value = "challenge" },
                        new Answer { Content = "Someone that knows you best.", IsCorrect = true, Value = "knows" },
                        new Answer { Content = "Someone that will celebrate your successes.", IsCorrect = false, Value = "celebrate" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "How often should you talk with your accountability partner?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Ideally, you should communicate with your accountability partner on a weekly basis."
                },
                new Card
                {
                    PackId = 1,
                    Content = "According to the video, which of the following are examples of other ways accountability partners are used?",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "The video specifically highlights gyms, dieting, and churches.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Learning to drive", IsCorrect = false, Value = "drive" },
                        new Answer { Content = "Dieting", IsCorrect = true, Value = "dieting" },
                        new Answer { Content = "Gyms", IsCorrect = true, Value = "gyms" },
                        new Answer { Content = "Churches", IsCorrect = true, Value = "churches" }
                    }
                },
                // --- Group Study (quiz.html.php / Course 3) ---
                new Card
                {
                    PackId = 1,
                    Content = "Which of the following are usually bad times to study as a group?",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "Writing a paper and looking at material for the first time are usually independent activities.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Writing a paper", IsCorrect = true, Value = "writing" },
                        new Answer { Content = "Trying to clarify difficult concepts", IsCorrect = false, Value = "difficult" },
                        new Answer { Content = "Looking at material for the first time", IsCorrect = true, Value = "material" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Study groups should take breaks too.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "True. Taking breaks helps keep everyone fresh and prevents exhaustion.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = true, Value = "1" },
                        new Answer { Content = "False", IsCorrect = false, Value = "0" }
                    }
                },

                // --- Study Metrics (quiz.html (1).php / Course 2) ---
                new Card
                {
                    PackId = 1,
                    Content = "Which of the following are reasons to track your study hours?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "By studying a certain number of hours per week, you guarantee you will be prepared.", IsCorrect = false, Value = "guarantee" },
                        new Answer { Content = "Tracking your study time helps you avoid procrastination.", IsCorrect = true, Value = "procrastination" },
                        new Answer { Content = "Tracking your study time is an effective way to see if you are actually studying enough.", IsCorrect = true, Value = "tracking" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "Why does everyone else look like they have it all together?",
                    ResponseType = CardType.Short,
                    ResponseContent = "They don't. They just typically put on a brave face so that you will think they are smart. Everyone struggles in school."
                },

                // --- Test Taking (quiz.html (2).php / Course 2) ---
                new Card
                {
                    PackId = 1,
                    Content = "Leading up to the test, it is a super good idea to cram.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "SAY NO TO CRAMMING!!!",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "What is the name of the breathing exercise demonstrated in this video?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Four-part breathing (also called combat or tactical breathing)."
                },
                new Card
                {
                    PackId = 1,
                    Content = "What should you be looking for when you skim the test?",
                    ResponseType = CardType.Short,
                    ResponseContent = "The number of questions, the type of questions, and the value of questions to help pace yourself."
                },

                // --- Interleaving (quiz.html (3).php / Course 2) ---
                new Card
                {
                    PackId = 1,
                    Content = "What is it called when you study the same class material for multiple study sessions?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Blocked practice."
                },
                new Card
                {
                    PackId = 1,
                    Content = "What is another name for interleaving?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Varied practice."
                },
                new Card
                {
                    PackId = 1,
                    Content = "When interleaving, alternating similar types of courses is most effective because your brain is already in the right mode.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False, try to alternate very different subjects to activate different parts of the brain.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },

                // --- Study Tests (quiz.html (4).php / Course 2) ---
                new Card
                {
                    PackId = 1,
                    Content = "Which of the following types of tests are objective?",
                    ResponseType = CardType.Multiple,
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Essay", IsCorrect = false, Value = "essay" },
                        new Answer { Content = "Short Answer", IsCorrect = false, Value = "short" },
                        new Answer { Content = "Math & Science", IsCorrect = true, Value = "math" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "What are two tips for open notes tests?",
                    ResponseType = CardType.Short,
                    ResponseContent = "1: You still have to study (maybe even more). 2: Organize your notes so you can find information immediately."
                },

                // --- Active Reading (quiz.html (5).php / Course 3) ---
                new Card
                {
                    PackId = 1,
                    Content = "What is active reading?",
                    ResponseType = CardType.Short,
                    ResponseContent = "Trying to understand what you are reading and recognizing which parts are most important to your needs; being curious."
                },
                new Card
                {
                    PackId = 1,
                    Content = "Highlighting and underlining is an effective tool for active reading.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False. Highlighting and underlining are passive and can lead to a false sense of security.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },

                // --- Teaching (quiz.html (6).php / Course 3) ---
                new Card
                {
                    PackId = 1,
                    Content = "Why is using the 'teaching to learn' strategy similar to learning a new language?",
                    ResponseType = CardType.Short,
                    ResponseContent = "You lose the ability to guess based on context and are forced to understand at a deeper level."
                },
                new Card
                {
                    PackId = 1,
                    Content = "True or false, teaching to learn is an effective strategy for memorizing lots of information.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False. It is better for understanding concepts deeply than for raw memorization.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },

                // --- Spaced Repetition (quiz.html (8).php / Course 3) ---
                new Card
                {
                    PackId = 1,
                    Content = "Spacing out your study sessions isn't important at all.",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "False. Spaced repetition is critical for long-term retention.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "True", IsCorrect = false, Value = "1" },
                        new Answer { Content = "False", IsCorrect = true, Value = "0" }
                    }
                },
                new Card
                {
                    PackId = 1,
                    Content = "What is another name for Spaced Repetition?",
                    ResponseType = CardType.Multiple,
                    ResponseContent = "Spaced practice and Distributed practice.",
                    Answers = new List<Answer>
                    {
                        new Answer { Content = "Spaced practice", IsCorrect = true, Value = "practice" },
                        new Answer { Content = "Distributed practice", IsCorrect = true, Value = "distributed" },
                        new Answer { Content = "Blocked practice", IsCorrect = false, Value = "blocked" }
                    }
                }
        };
            return cards;
        }
    }
}
