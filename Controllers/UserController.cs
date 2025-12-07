using Microsoft.AspNetCore.Mvc;
using Tasman.Models;

namespace MyApp.Namespace
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User _user)
        {
            return View();
        }
    }
}
