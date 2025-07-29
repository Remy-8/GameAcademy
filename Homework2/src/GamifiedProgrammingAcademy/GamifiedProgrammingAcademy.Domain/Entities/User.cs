using GamifiedProgrammingAcademy.Domain.Core;

namespace GamifiedProgrammingAcademy.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalPoints { get; set; } = 0;
        public int Level { get; set; } = 1;
        public int ExperiencePoints { get; set; } = 0;
        public DateTime JoinDate { get; set; }
        public string Avatar { get; set; }
    }
}