using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using GamifiedProgrammingAcademy.Infrastructure.Context;
using GamifiedProgrammingAcademy.Infrastructure.Core;

namespace GamifiedProgrammingAcademy.Infrastructure.Repositories
{
    public class BadgeRepository : BaseRepository<Badge>, IBadgeRepository
    {
        public BadgeRepository(GamifiedAcademyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Badge>> GetBadgesByUserAsync(int userId)
        {
            return await _context.UserBadges
                .Where(ub => ub.UserId == userId)
                .Join(_dbSet, ub => ub.BadgeId, b => b.Id, (ub, b) => b)
                .Where(b => b.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Badge>> GetAvailableBadgesForUserAsync(int userId)
        {
            var earnedBadgeIds = await _context.UserBadges
                .Where(ub => ub.UserId == userId)
                .Select(ub => ub.BadgeId)
                .ToListAsync();

            return await _dbSet
                .Where(b => b.IsActive && !earnedBadgeIds.Contains(b.Id))
                .ToListAsync();
        }
    }
}