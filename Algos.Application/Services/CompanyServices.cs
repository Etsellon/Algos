using Algos.Application.Models;
using Algos.Core.Abstractions.Interfaces;
using Algos.Core.models;
using Algos.SharedKernel;

namespace Algos.Application.Services
{
    public class CompanyServices(ICompanyRepository repository)
    {
        private readonly ICompanyRepository _companyRepository = repository;

        public async Task<Result<CompanyDomainModel>> Create(CompanyRequest company)
        {
            var domain = CompanyDomainModel.Create(
                Guid.NewGuid(),
                company.Name,
                company.Description,
                DateTime.UtcNow,
                DateTime.UtcNow);

            if (domain.IsFailure)
                return Result<CompanyDomainModel>.Failure(domain.Errors);
            else if (domain.Value == null)
                return Result<CompanyDomainModel>.Failure("An unexpected error has occurred.");

            await _companyRepository.Add(domain.Value);
            return domain;
        }

        public async Task<IEnumerable<CompanyDomainModel>> GetByPage(int page = 1, int size = 20) =>
            await _companyRepository.GetByPages(page, size);

        public async Task<CompanyDomainModel?> GetById(Guid id) =>
            await _companyRepository.GetById(id);

        public async Task Delete(Guid id) =>
            await _companyRepository.Delete(id);
    }
}


