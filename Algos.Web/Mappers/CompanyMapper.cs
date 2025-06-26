using Algos.Core.Contracts;
using Algos.Core.models;
using Algos.Web.ViewModels;

namespace Algos.Web.Mappers
{
    public static class CompanyMapper
    {
        public static CompanyViewModel ToViewModel(this CompanyDomainModel domain) =>
            new()
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Price = domain.Price,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt,
            };

        public static CompanyRequest ToRequest(this CompanyViewModel viewModel) =>
            new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
            };
    }
}
