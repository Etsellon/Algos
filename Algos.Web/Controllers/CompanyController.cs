using Algos.Core.Abstractions.Interfaces;
using Algos.Core.models;
using Algos.Web.Mappers;
using Algos.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Algos.Web.Controllers
{
    [Route("/")]
    public class CompanyController(ICompanyServices companyService) : Controller
    {
        private readonly ICompanyServices _companyService = companyService;

        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1, int size = 5)
        {
            var pagedCompanies = await _companyService.GetByPage(page, size);

            var result = new CompaniesListViewModel
            {
                Companies = pagedCompanies.Items.Select(domain => domain.ToViewModel()),
                CurrentPage = pagedCompanies.CurrentPage,
                PageSize = pagedCompanies.PageSize,
                TotalItems = pagedCompanies.TotalItems,
            };

            return View(result);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("Upsert", new CompanyViewModel());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CompanyViewModel model)
        {
            if (!ModelState.IsValid) return View("Upsert", model);

            var result = await _companyService.Create(model.ToRequest());

            if (result.IsFailure)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error);

                return View("Upsert", model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var company = await _companyService.GetById(id);

            if (company == null) return NotFound();

            var model = company.ToViewModel();

            return View("Upsert", model);
        }

        [HttpPost("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, CompanyViewModel model)
        {
            if (!ModelState.IsValid) return View("Upsert", model);

            var existing = await _companyService.GetById(id);
            if (existing == null) return NotFound();

            var updatedResult = CompanyDomainModel.Create(
                id,
                model.Name,
                model.Description,
                model.Price,
                existing.CreatedAt,
                model.UpdatedAt);

            if (updatedResult.IsFailure)
            {
                foreach (var error in updatedResult.Errors)
                    ModelState.AddModelError(string.Empty, error);

                return View("Upsert", model);
            }

            if (updatedResult.Value == null)
                return BadRequest();

            await _companyService.Update(updatedResult.Value);
            return RedirectToAction("Index");
        }

        [HttpPost("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet("details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var company = await _companyService.GetById(id);
            if (company == null) return NotFound();

            var model = company.ToViewModel();

            return View(model);
        }

        [HttpGet("about")]
        public IActionResult About()
        {
            return View();
        }
    }
}
