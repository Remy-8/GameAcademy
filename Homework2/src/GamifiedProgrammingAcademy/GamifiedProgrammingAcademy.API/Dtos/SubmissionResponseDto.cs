namespace GamifiedProgrammingAcademy.API.Dtos
{
    public class SubmissionResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public int Score { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string Output { get; set; }
        public string ErrorMessage { get; set; }
        public int ExecutionTime { get; set; }
    }
}
