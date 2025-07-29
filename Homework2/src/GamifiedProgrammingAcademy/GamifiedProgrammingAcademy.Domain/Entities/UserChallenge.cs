using GamifiedProgrammingAcademy.Domain.Core;

namespace GamifiedProgrammingAcademy.Domain.Entities
{
    public class UserChallenge : BaseEntity
    {
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }
        public int AttemptsCount { get; set; } = 0;
        public int BestScore { get; set; } = 0;
        public TimeSpan? BestTime { get; set; }
    }
}