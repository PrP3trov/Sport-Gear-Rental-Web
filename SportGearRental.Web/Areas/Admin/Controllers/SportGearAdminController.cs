using Microsoft.AspNetCore.Mvc;

namespace SportGearRental.Web.Areas.Admin.Controllers
{
    public class SportGearAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
