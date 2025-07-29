using Microsoft.EntityFrameworkCore;
using GamifiedProgrammingAcademy.Domain.Core;
using GamifiedProgrammingAcademy.Infrastructure.Context;

namespace GamifiedProgrammingAcademy.Infrastructure.Core
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected readonly GamifiedAcademyContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(GamifiedAcademyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(x => x.IsActive).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.IsActive);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsActive = false;
                entity.UpdatedAt = DateTime.UtcNow;
                await UpdateAsync(entity);
            }
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(x => x.Id == id && x.IsActive);
        }
    }
}