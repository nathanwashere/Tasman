using Microsoft.AspNetCore.Mvc;
using Tasman.Models;
using Microsoft.AspNetCore.Identity;
using Tasman.Data;
using Microsoft.AspNetCore.Http;

namespace Tasman.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /Register
        [HttpGet]
        public IActionResult Index()
        {
            // Pass empty model so validation messages work correctly
            return View(new User());
        }

        // POST /Register
        [HttpPost]
        public IActionResult Index(User model)
        {
            // 1. Validate the model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 2. Check duplicate email
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(model);
            }

            // 3. Hash password
            var hasher = new PasswordHasher<User>();
            model.Password = hasher.HashPassword(model, model.Password);
            model.IsAdmin = false;

            // 4. Save user
            _context.Users.Add(model);
            _context.SaveChanges();

            HttpContext.Session.SetString("UserEmail", model.Email);
            HttpContext.Session.SetString("IsAdmin", "False");

            // 5. Redirect after success
            return RedirectToAction("Index", "Travel");
        }
    }
}
    
