using GamifiedProgrammingAcademy.Domain.Entities;

namespace GamifiedProgrammingAcademy.Domain.Interfaces
{
    public interface IChallengeRepository
    {
        Task<IEnumerable<Challenge>> GetAllAsync();
        Task<Challenge> GetByIdAsync(int id);
        Task<Challenge> AddAsync(Challenge challenge);
        Task UpdateAsync(Challenge challenge);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Challenge>> GetByDifficultyAsync(string difficulty);
        Task<IEnumerable<Challenge>> GetByCategoryAsync(string category);
        Task<IEnumerable<Challenge>> GetCompletedChallengesByUserAsync(int userId);
        Task<IEnumerable<Challenge>> GetAvailableChallengesForUserAsync(int userId);
    }
}