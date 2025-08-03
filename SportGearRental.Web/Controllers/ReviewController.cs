using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportGearRental.Services.ServiceContracts;
using SportGearRental.ViewModels.Review;
using System.Security.Claims;

namespace SportGearRental.Web.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid gearId, ReviewFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", "SportGear", new { id = gearId });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _reviewService.AddReviewAsync(gearId, userId, model);

            return RedirectToAction("Details", "SportGear", new { id = gearId });
        }
    }
}
