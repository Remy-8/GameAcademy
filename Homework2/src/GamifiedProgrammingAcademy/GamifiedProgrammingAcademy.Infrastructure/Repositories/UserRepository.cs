using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using GamifiedProgrammingAcademy.Infrastructure.Context;
using GamifiedProgrammingAcademy.Infrastructure.Core;

namespace GamifiedProgrammingAcademy.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GamifiedAcademyContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.IsActive && u.Username.ToLower() == username.ToLower());
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.IsActive && u.Email.ToLower() == email.ToLower());
        }

        public async Task<IEnumerable<User>> GetTopUsersByPointsAsync(int count)
        {
            return await _dbSet
                .Where(u => u.IsActive)
                .OrderByDescending(u => u.TotalPoints)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersByLevelAsync(int level)
        {
            return await _dbSet
                .Where(u => u.IsActive && u.Level == level)
                .ToListAsync();
        }
    }
}