using GamifiedProgrammingAcademy.Domain.Core;

namespace GamifiedProgrammingAcademy.Domain.Entities
{
    public class Submission : BaseEntity
    {
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string Code { get; set; }
        public string Language { get; set; } // C#, Python, JavaScript
        public string Status { get; set; } // Pending, Correct, Incorrect
        public int Score { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string Output { get; set; }
        public string ErrorMessage { get; set; }
        public int ExecutionTime { get; set; }
    }
}