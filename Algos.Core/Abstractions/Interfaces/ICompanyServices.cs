using Algos.Core.Contracts;
using Algos.Core.models;
using Algos.SharedKernel;
using Algos.SharedKernel.Models;

namespace Algos.Core.Abstractions.Interfaces
{
    public interface ICompanyServices
    {
        Task<Result<CompanyDomainModel>> Create(CompanyRequest company);
        Task<Guid> Update(CompanyDomainModel company);
        Task Delete(Guid id);
        Task<CompanyDomainModel?> GetById(Guid id);
        Task<PagedResult<CompanyDomainModel>> GetByPage(int page = 1, int size = 20);
    }
}