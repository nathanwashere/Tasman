using Microsoft.AspNetCore.Mvc;
using Tasman.Models;

namespace Tasman.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View(new User());  
        }

    }
}
