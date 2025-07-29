using GamifiedProgrammingAcademy.Domain.Entities;

namespace GamifiedProgrammingAcademy.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<User>> GetTopUsersByPointsAsync(int count);
        Task<IEnumerable<User>> GetUsersByLevelAsync(int level);
    }
}