using GamifiedProgrammingAcademy.Domain.Core;

namespace GamifiedProgrammingAcademy.Domain.Entities
{
    public class LeaderboardEntry : BaseEntity
    {
        public int UserId { get; set; }
        public int Rank { get; set; }
        public int Points { get; set; }
        public string Period { get; set; } // Daily, Weekly, Monthly, AllTime
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}