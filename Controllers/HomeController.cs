using Microsoft.AspNetCore.Mvc;

namespace Tasman.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
