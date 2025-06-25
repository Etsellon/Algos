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
    }
}
