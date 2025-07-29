using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using GamifiedProgrammingAcademy.Infrastructure.Context;
using GamifiedProgrammingAcademy.Infrastructure.Core;

namespace GamifiedProgrammingAcademy.Infrastructure.Repositories
{
    public class ChallengeRepository : BaseRepository<Challenge>, IChallengeRepository
    {
        public ChallengeRepository(GamifiedAcademyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Challenge>> GetByDifficultyAsync(string difficulty)
        {
            return await _dbSet
                .Where(c => c.IsActive && c.Difficulty.ToLower() == difficulty.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Challenge>> GetByCategoryAsync(string category)
        {
            return await _dbSet
                .Where(c => c.IsActive && c.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Challenge>> GetCompletedChallengesByUserAsync(int userId)
        {
            return await _context.UserChallenges
                .Where(uc => uc.UserId == userId && uc.IsCompleted)
                .Join(_dbSet, uc => uc.ChallengeId, c => c.Id, (uc, c) => c)
                .Where(c => c.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Challenge>> GetAvailableChallengesForUserAsync(int userId)
        {
            var completedChallengeIds = await _context.UserChallenges
                .Where(uc => uc.UserId == userId && uc.IsCompleted)
                .Select(uc => uc.ChallengeId)
                .ToListAsync();

            return await _dbSet
                .Where(c => c.IsActive && !completedChallengeIds.Contains(c.Id))
                .ToListAsync();
        }
    }
}