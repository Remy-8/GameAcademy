using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.Domain.Entities;
using GamifiedProgrammingAcademy.Domain.Interfaces;
using GamifiedProgrammingAcademy.Infrastructure.Context;
using GamifiedProgrammingAcademy.Infrastructure.Core;

namespace GamifiedProgrammingAcademy.Infrastructure.Repositories
{
    public class SubmissionRepository : BaseRepository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(GamifiedAcademyContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByUserAsync(int userId)
        {
            return await _dbSet
                .Where(s => s.IsActive && s.UserId == userId)
                .OrderByDescending(s => s.SubmittedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetSubmissionsByChallengeAsync(int challengeId)
        {
            return await _dbSet
                .Where(s => s.IsActive && s.ChallengeId == challengeId)
                .OrderByDescending(s => s.Score)
                .ToListAsync();
        }

        public async Task<Submission> GetBestSubmissionAsync(int userId, int challengeId)
        {
            return await _dbSet
                .Where(s => s.IsActive && s.UserId == userId && s.ChallengeId == challengeId)
                .OrderByDescending(s => s.Score)
                .FirstOrDefaultAsync();
        }
    }
}