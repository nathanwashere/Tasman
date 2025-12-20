using Microsoft.AspNetCore.Mvc;
using Tasman.Models;
using Tasman.ViewModels;
using Microsoft.AspNetCore.Identity;
using Tasman.Data;
using Microsoft.AspNetCore.Http;

namespace Tasman.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            // 1. Validate ViewModel
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 2. Check if email exists
            var userFromDb = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (userFromDb == null)
            {
                ModelState.AddModelError("", "Email or password is incorrect.");
                return View(model);
            }

            // 3. Verify hashed password
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(userFromDb, userFromDb.Password, model.Password);

            if (result == PasswordVerificationResult.Success)
            {
                HttpContext.Session.SetString("UserEmail", userFromDb.Email);
                HttpContext.Session.SetString("IsAdmin", userFromDb.IsAdmin.ToString());
                Console.WriteLine("Login successful");
                if (userFromDb.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Travel");
            }

            // 4. Wrong password
            ModelState.AddModelError("", "Email or password is incorrect.");
            Console.WriteLine("Login failed");
            return View(model);
        }
    }
}
