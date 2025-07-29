using GamifiedProgrammingAcademy.Domain.Core;

namespace GamifiedProgrammingAcademy.Domain.Entities
{
    public class Challenge : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public string Difficulty { get; set; } // Beginner, Intermediate, Advanced
        public string Category { get; set; } // Variables, Loops, Functions, etc.
        public string ProblemStatement { get; set; }
        public string ExpectedOutput { get; set; }
        public string TestCases { get; set; }
    }
}