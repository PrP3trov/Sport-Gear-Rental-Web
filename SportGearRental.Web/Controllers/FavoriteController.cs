using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportGearRental.Services.ServiceContracts;
using System.Security.Claims;

namespace SportGearRental.Web.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var model = await _favoriteService.GetFavoritesAsync(userId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Guid gearId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                await _favoriteService.AddFavoriteAsync(userId, gearId);
            }
            return RedirectToAction("Details", "SportGear", new { id = gearId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(Guid gearId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                await _favoriteService.RemoveFavoriteAsync(userId, gearId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
