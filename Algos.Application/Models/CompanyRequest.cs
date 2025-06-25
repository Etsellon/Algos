namespace Algos.Application.Models
{
    public record CompanyRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}


