using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasman.Data;
using Tasman.Filters;

namespace Tasman.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var travelCount = await _context.TravelDestinations.CountAsync();
            var userCount = await _context.Users.CountAsync();
            var bookingsCount = await _context.Bookings.CountAsync();
            var waitlistCount = await _context.WaitlistEntries.CountAsync();

            ViewBag.TravelCount = travelCount;
            ViewBag.UserCount = userCount;
            ViewBag.BookingsCount = bookingsCount;
            ViewBag.WaitlistCount = waitlistCount;

            return View();
        }
    }
}
