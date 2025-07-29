using GamifiedProgrammingAcademy.Domain.Entities;

namespace GamifiedProgrammingAcademy.Domain.Interfaces
{
    public interface IBadgeRepository
    {
        Task<IEnumerable<Badge>> GetAllAsync();
        Task<Badge> GetByIdAsync(int id);
        Task<Badge> AddAsync(Badge badge);
        Task UpdateAsync(Badge badge);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Badge>> GetBadgesByUserAsync(int userId);
        Task<IEnumerable<Badge>> GetAvailableBadgesForUserAsync(int userId);
    }
}