namespace Algos.Web.ViewModels
{
    public class CompaniesListViewModel
    {
        public IEnumerable<CompanyViewModel> Companies { get; set; } = [];
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
