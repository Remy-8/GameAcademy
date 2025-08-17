namespace GamifiedProgrammingAcademy.API.Dtos
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalPoints { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public DateTime JoinDate { get; set; }
    }
}