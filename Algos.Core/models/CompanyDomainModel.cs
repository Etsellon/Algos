using Algos.SharedKernel;

namespace Algos.Core.models
{
    public class CompanyDomainModel
    {
        private const int MAX_NAME_LENGTH = 32;
        private const int MAX_TEXT_LENGTH = 320;

        private CompanyDomainModel(Guid id,
            string name,
            string? description,
            decimal price,
            DateTime createdAt,
            DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Description = description ?? string.Empty;
            Price = price;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public decimal Price { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        public static Result<CompanyDomainModel> Create(
            Guid id,
            string name,
            string? description,
            decimal price,
            DateTime createdAt,
            DateTime updatedAt)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(name))
                errors.Add("Company name is required field.");
            else if (name.Length > MAX_NAME_LENGTH)
                errors.Add($"Company name must not exceed {MAX_NAME_LENGTH} characters.");

            if (description != null)
                if (description.Length > MAX_TEXT_LENGTH)
                    errors.Add($"Company description must not exceed {MAX_TEXT_LENGTH} characters.");

            if (errors.Count > 0)
                return Result<CompanyDomainModel>.Failure(errors);

            var company = new CompanyDomainModel(id, name, description, price, createdAt, updatedAt);
            return Result<CompanyDomainModel>.Success(company);
        }
    }
}
