using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.Review;
using SportGearRental.ViewModels.SportGear;
using System.Security.Claims;

namespace SportGearRental.Web.Controllers
{
    [Authorize]
    public class SportGearController : Controller
    {
        private readonly ISportGearService _sportGearService;
        private readonly IRentalService _rentalService;

        public SportGearController(ISportGearService sportGearService, IRentalService rentalService)
        {
            _sportGearService = sportGearService;
            _rentalService = rentalService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchTerm, Guid? categoryId, Guid? brandId, Guid? conditionId, decimal? maxPrice, int? minRating)
        {
            var model = await _sportGearService.GetFilteredAsync(
                searchTerm, categoryId, brandId, conditionId, maxPrice, minRating);

            return View(model);
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _sportGearService.GetDetailsByIdAsync(id);
            if (model == null) return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && await _rentalService.HasUserRentedGearAsync(id, userId))
            {
                model.NewReview = new ReviewFormModel { SportGearId = id };
            }

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
            if (userId == null) return Unauthorized();

            await _sportGearService.CreateAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _sportGearService.IsOwnerAsync(id, userId!)) return Forbid();

            var model = await _sportGearService.GetFormByIdAsync(id, userId!);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SportGearFormModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _sportGearService.IsOwnerAsync(id, userId!)) return Forbid();

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _sportGearService.IsOwnerAsync(id, userId!)) return Forbid();

            var model = await _sportGearService.GetDetailsByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _sportGearService.IsOwnerAsync(id, userId!)) return Forbid();

            await _sportGearService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
