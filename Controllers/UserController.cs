using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Tasman.Data;
using Tasman.Models;
using Tasman.ViewModels;

namespace Tasman.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        // For database access
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}
