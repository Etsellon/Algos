using Algos.Core.Abstractions.Interfaces;
using Algos.Core.Contracts;
using Algos.Core.models;
using Algos.SharedKernel;
using Algos.SharedKernel.Models;

namespace Algos.Application.Services
{
    public class CompanyServices(ICompanyRepository repository) : ICompanyServices
    {
        private readonly ICompanyRepository _companyRepository = repository;

        public async Task<Result<CompanyDomainModel>> Create(CompanyRequest company)
        {
            var domain = CompanyDomainModel.Create(
                Guid.NewGuid(),
                company.Name,
                company.Description,
                company.Price,
                DateTime.UtcNow,
                DateTime.UtcNow);

            if (domain.IsFailure)
                return Result<CompanyDomainModel>.Failure(domain.Errors);
            else if (domain.Value == null)
                return Result<CompanyDomainModel>.Failure("An unexpected error has occurred.");

            await _companyRepository.Add(domain.Value);
            return domain;
        }

        public async Task<IEnumerable<CompanyDomainModel>> GetByPage2(int page = 1, int size = 20) =>
            await _companyRepository.GetByPages(page, size);

        public async Task<PagedResult<CompanyDomainModel>> GetByPage(int page = 1, int size = 20)
        {
            var totalCount = await _companyRepository.GetTotalCount();
            var items = await _companyRepository.GetByPages(page, size);

            var pagedResult = new PagedResult<CompanyDomainModel>
            {
                Items = items,
                CurrentPage = page,
                PageSize = size,
                TotalItems = totalCount
            };

            return pagedResult;
        }

        public async Task<CompanyDomainModel?> GetById(Guid id) =>
            await _companyRepository.GetById(id);

        public async Task<Guid> Update(CompanyDomainModel company) =>
            await _companyRepository.Update(company);

        public async Task Delete(Guid id) =>
            await _companyRepository.Delete(id);
    }
}


