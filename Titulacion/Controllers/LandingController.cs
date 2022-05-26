using Microsoft.AspNetCore.Mvc;

namespace Titulacion.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            TempData["mensaje"] = null;
            return View();
        }
    }
}
