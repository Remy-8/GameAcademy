using GamifiedProgrammingAcademy.Domain.Core;

namespace GamifiedProgrammingAcademy.Domain.Entities
{
    public class UserBadge : BaseEntity
    {
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime EarnedAt { get; set; }
        public bool IsDisplayed { get; set; } = true;
    }
}