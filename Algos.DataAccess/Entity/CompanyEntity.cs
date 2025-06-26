using System.ComponentModel.DataAnnotations;

namespace Algos.DataAccess.Entity
{
    public class CompanyEntity : BaseEntity
    {
        [MaxLength(32)] public string Name { get; set; } = string.Empty;
        [MaxLength(320)] public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
