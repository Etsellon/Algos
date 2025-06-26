using Algos.Core.models;

namespace Algos.Core.Abstractions.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<CompanyDomainModel>
    {
        Task<CompanyDomainModel?> GetByName(string name);
    }
}