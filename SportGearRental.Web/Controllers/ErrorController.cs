using Microsoft.AspNetCore.Mvc;
using SportGearRental.ViewModels;
using System.Diagnostics;

namespace SportGearRental.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                Response.StatusCode = statusCode.Value;

                if (statusCode.Value == 404)
                {
                    return View("Error404");
                }

                if (statusCode.Value == 500)
                {
                    return View("Error500", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
