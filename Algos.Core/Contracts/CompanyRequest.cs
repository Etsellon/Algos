namespace Algos.Core.Contracts
{
    public record CompanyRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}


