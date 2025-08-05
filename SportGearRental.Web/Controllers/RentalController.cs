using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.Rentals;
using System.Security.Claims;

namespace SportGearRental.Web.Controllers
{
    [Authorize]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rentals = await _rentalService.GetAllAsync(User.IsInRole("Admin") ? null : userId);
            return View(rentals);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rental = await _rentalService.GetByIdAsync(id, User.IsInRole("Admin") ? null : userId);

            if (rental == null)
            {
                return NotFound();
            }

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
            await _rentalService.CreateAsync(model, userId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _rentalService.GetFormByIdAsync(id, User.IsInRole("Admin") ? null : userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RentalFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SportGears = await _rentalService.GetSportGearsForDropdownAsync();
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _rentalService.EditAsync(id, model, User.IsInRole("Admin") ? null : userId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuickRental(RentalFormModel model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
                Console.WriteLine($"SportGearId: {model.SportGearId}");
                Console.WriteLine($"StartDate: {model.StartDate}");
                Console.WriteLine($"EndDate: {model.EndDate}");

                return RedirectToAction("Index", "SportGear");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _rentalService.CreateAsync(model, userId);

            return RedirectToAction("Index", "Rental");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rental = await _rentalService.GetByIdAsync(id, User.IsInRole("Admin") ? null : userId);

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _rentalService.DeleteAsync(id, User.IsInRole("Admin") ? null : userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
