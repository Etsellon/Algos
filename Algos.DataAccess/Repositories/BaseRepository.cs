using Algos.Core.Abstractions.Interfaces;
using Algos.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace Algos.DataAccess.Repositories
{
    public abstract class BaseRepository<TDomain, TEntity> : IBaseRepository<TDomain> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        protected abstract TDomain ToDomain(TEntity entity);
        protected abstract TEntity ToEntity(TDomain domain);

        public virtual async Task<TDomain?> GetById(Guid id)
        {
            var entity = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);

            return entity is null ? default : ToDomain(entity);
        }

        public virtual async Task<IEnumerable<TDomain>> GetByPages(int page = 1, int size = 20)
        {
            var entities = await _dbSet
                .AsNoTracking()
                .OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return entities.Select(e => ToDomain(e));
        }

        public virtual async Task<Guid> Add(TDomain domain)
        {
            var entity = ToEntity(domain);

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task<Guid> Update(TDomain domain)
        {
            var entity = ToEntity(domain);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task Delete(Guid id)
        {
            await _dbSet.Where(e => e.Id == id).ExecuteDeleteAsync();
        }

        public async Task<int> GetTotalCount() =>
            await _dbSet.CountAsync();
    }
}
