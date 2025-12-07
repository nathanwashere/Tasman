using Microsoft.AspNetCore.Mvc;
using Tasman.Data;
using Tasman.Models;

namespace Tasman.Controllers
{
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
        [HttpPost]
        public ActionResult Create(User _user)
        {
            if (ModelState.IsValid)
            {
                if(_context.Users.Any(u => u.Email == _user.Email))
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View("/Views/Register/Register.cshtml", _user);
                }
                _context.Users.Add(_user);
                _context.SaveChanges();

                return RedirectToAction("Index","Travel");
            }
            
            return View("/Views/Register/Register.cshtml", _user);
            
        }
    }
}
