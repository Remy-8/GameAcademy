using GamifiedProgrammingAcademy.Domain.Entities;

namespace GamifiedProgrammingAcademy.Domain.Interfaces
{
    public interface ISubmissionRepository
    {
        Task<IEnumerable<Submission>> GetAllAsync();
        Task<Submission> GetByIdAsync(int id);
        Task<Submission> AddAsync(Submission submission);
        Task UpdateAsync(Submission submission);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Submission>> GetSubmissionsByUserAsync(int userId);
        Task<IEnumerable<Submission>> GetSubmissionsByChallengeAsync(int challengeId);
        Task<Submission> GetBestSubmissionAsync(int userId, int challengeId);
    }
}