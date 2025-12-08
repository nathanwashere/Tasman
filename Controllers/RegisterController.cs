using Microsoft.AspNetCore.Mvc;
using Tasman.Models;
using Microsoft.AspNetCore.Identity;
using Tasman.Data;

namespace Tasman.Controllers
{
    [Route("register")] // Base route for this controller
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /register
        [HttpGet("")] // Access via localhost/register
        public IActionResult Register()
        {
            // Pass empty model so validation messages work correctly
            return View(new User());
        }

        // POST /register
        [HttpPost("")] // Submit to localhost/register
        public IActionResult Register(User model)
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

            // 4. Save user
            _context.Users.Add(model);
            _context.SaveChanges();

            // 5. Redirect after success
            return RedirectToAction("Index", "Travel");
        }
    }
}
    