using Algos.Core.Abstractions.Interfaces;
using Algos.Core.models;
using Algos.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace Algos.DataAccess.Repositories
{
    public class CompanyRepository(ApplicationDbContext context) : BaseRepository<CompanyDomainModel, CompanyEntity>(context), ICompanyRepository
    {
        protected override CompanyDomainModel ToDomain(CompanyEntity entity)
        {
            var domain = CompanyDomainModel.Create(
                entity.Id,
                entity.Name,
                entity.Description,
                entity.Price,
                entity.CreatedAt,
                entity.UpdatedAt);

            return domain.Value ?? throw new Exception("Failure created domain model");
        }

        protected override CompanyEntity ToEntity(CompanyDomainModel domain)
        {
            var entity = new CompanyEntity()
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Price = domain.Price,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
            };

            return entity;
        }

        public async Task<CompanyDomainModel?> GetByName(string name)
        {
            var entity = await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == name);

            if (entity == null)
                return null;

            return ToDomain(entity);
        }

        public override async Task<Guid> Update(CompanyDomainModel domain)
        {
            await _dbSet
                .Where(e => e.Id == domain.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.Name, domain.Name)
                    .SetProperty(e => e.Description, domain.Description)
                    .SetProperty(e => e.Price, domain.Price));

            return domain.Id;
        }
    }
}
