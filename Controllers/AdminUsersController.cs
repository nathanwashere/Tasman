using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasman.Data;
using Tasman.Filters;
using Tasman.Models;

namespace Tasman.Controllers
{
    [AdminAuthorize]
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.OrderBy(u => u.LastName).ToListAsync();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return NotFound();

            if (user.IsAdmin)
            {
                TempData["Message"] = "Cannot delete another admin user.";
                return RedirectToAction("Index");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["Message"] = "User deleted.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendReminder(string email)
        {
            // Placeholder for real messaging integration
            TempData["Message"] = $"Reminder queued for {email}.";
            return RedirectToAction("Index");
        }
    }
}
