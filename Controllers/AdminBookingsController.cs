using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasman.Data;
using Tasman.Filters;
using Tasman.Models;

namespace Tasman.Controllers
{
    [AdminAuthorize]
    public class AdminBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminBookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Travel)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            var waitlist = await _context.WaitlistEntries
                .Include(w => w.Travel)
                .OrderBy(w => w.CreatedAt)
                .ToListAsync();

            ViewBag.Waitlist = waitlist;
            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
            if (booking == null)
                return NotFound();

            var travel = await _context.TravelDestinations.FirstOrDefaultAsync(t => t.Id == booking.TravelId);
            if (travel != null)
            {
                travel.AvailableRooms += booking.Rooms;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Booking removed and rooms returned to availability.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Promote(int id)
        {
            var wait = await _context.WaitlistEntries.Include(w => w.Travel).FirstOrDefaultAsync(w => w.Id == id);
            if (wait == null)
                return NotFound();

            if (wait.Travel == null)
            {
                TempData["Message"] = "Travel not found for waitlist item.";
                return RedirectToAction("Index");
            }

            if (wait.Travel.AvailableRooms < wait.RequestedRooms)
            {
                TempData["Message"] = "Not enough rooms to promote this waitlist item.";
                return RedirectToAction("Index");
            }

            var booking = new Booking
            {
                TravelId = wait.TravelId,
                UserEmail = wait.UserEmail,
                Rooms = wait.RequestedRooms,
                CreatedAt = DateTime.UtcNow
            };

            wait.Travel.AvailableRooms -= wait.RequestedRooms;
            _context.Bookings.Add(booking);
            _context.WaitlistEntries.Remove(wait);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Waitlist traveler promoted to booked.";
            return RedirectToAction("Index");
        }
    }
}
