using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.Rentals;
using System.Security.Claims;

namespace SportGearRental.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.GetAllAsync();
            return View(rentals);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var rental = await _rentalService.GetByIdAsync(id);
            if (rental == null) return NotFound();
            return View(rental);
        }

        public async Task<IActionResult> Create()
        {
            var model = new RentalFormModel
            {
                SportGears = await _rentalService.GetSportGearsForDropdownAsync()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SportGears = await _rentalService.GetSportGearsForDropdownAsync();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _rentalService.CreateAsync(model, userId!);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var rental = await _rentalService.GetByIdAsync(id);
            if (rental == null) return NotFound();
            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _rentalService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
