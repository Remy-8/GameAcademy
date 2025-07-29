using GamifiedProgrammingAcademy.Domain.Core;

namespace GamifiedProgrammingAcademy.Domain.Entities
{
    public class Badge : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string BadgeType { get; set; } // Achievement, Progress, Special
        public string UnlockCondition { get; set; }
        public int RequiredPoints { get; set; }
        public string Rarity { get; set; } // Common, Rare, Epic, Legendary
    }
}