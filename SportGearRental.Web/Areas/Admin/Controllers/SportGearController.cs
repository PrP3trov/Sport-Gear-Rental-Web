using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.SportGear;
using System.Security.Claims;

namespace SportGearRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SportGearController : Controller
    {
        private readonly ISportGearService _sportGearService;

        public SportGearController(ISportGearService sportGearService)
        {
            _sportGearService = sportGearService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _sportGearService.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _sportGearService.GetDetailsByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var formModel = new SportGearFormModel
            {
                Categories = await _sportGearService.GetCategoryOptionsAsync(),
                Brands = await _sportGearService.GetBrandOptionsAsync(),
                Conditions = await _sportGearService.GetConditionOptionsAsync()
            };
            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SportGearFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _sportGearService.GetCategoryOptionsAsync();
                model.Brands = await _sportGearService.GetBrandOptionsAsync();
                model.Conditions = await _sportGearService.GetConditionOptionsAsync();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _sportGearService.CreateAsync(model, userId!);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _sportGearService.GetFormByIdAsync(id, null);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SportGearFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _sportGearService.GetCategoryOptionsAsync();
                model.Brands = await _sportGearService.GetBrandOptionsAsync();
                model.Conditions = await _sportGearService.GetConditionOptionsAsync();
                return View(model);
            }

            await _sportGearService.EditAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _sportGearService.GetDetailsByIdAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _sportGearService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}