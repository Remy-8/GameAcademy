namespace GamifiedProgrammingAcademy.API.Dtos
{
    public class BadgeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public string BadgeType { get; set; }
        public string UnlockCondition { get; set; }
        public int RequiredPoints { get; set; }
        public string Rarity { get; set; }
    }
}