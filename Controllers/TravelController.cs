using Microsoft.AspNetCore.Mvc;
using Tasman.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Tasman.Models;
using Tasman.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Tasman.Controllers
{
    public class TravelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================
        //           INDEX
        // ============================
        public async Task<IActionResult> Index(
            string? search,
            string? PackageType,
            string? sort,
            int? maxPrice,
            string? startDate,
            bool? discounted)
        {
            var query = _context.TravelDestinations
                .Where(t => t.IsVisible)
                .AsQueryable();

            // SEARCH FILTER
            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.Destination.Contains(search));

            // CATEGORY FILTER
            if (!string.IsNullOrEmpty(PackageType))
                query = query.Where(t => t.PackageType.ToLower() == PackageType.ToLower());

            // PRICE FILTER
            if (maxPrice.HasValue)
            {
                if (maxPrice.Value > 0 && maxPrice.Value < 20000)
                {
                    query = query.Where(t => t.Price <= maxPrice.Value);
                }
            }

            if (discounted == true)
            {
                query = query.Where(t =>
                    t.DiscountPrice.HasValue &&
                    t.DiscountEndsAt.HasValue &&
                    t.DiscountEndsAt.Value > DateTime.UtcNow);
            }

            // SORTING OPTIONS
            query = sort switch
            {
                "price-asc" => query.OrderBy(t => t.Price),
                "price-desc" => query.OrderByDescending(t => t.Price),
                _ => query
            };

            // ============================
            //      START DATE FILTER
            // ============================
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                string[] formats = { "d/M/yyyy", "dd/MM/yyyy", "yyyy-MM-dd" };

                if (DateTime.TryParseExact(
                    startDate,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
                {
                    query = query.Where(t => t.StartDate.Date == parsedDate.Date);

                    // Keep dd/MM/yyyy in the input field
                    ViewBag.StartDate = parsedDate.ToString("dd/MM/yyyy");
                }
            }
            else
            {
                ViewBag.StartDate = "";
            }

            // PASS FILTERS TO VIEW
            ViewBag.Search = search;
            ViewBag.PackageType = PackageType;
            ViewBag.Sort = sort;
            ViewBag.MaxPrice = maxPrice ?? 20000;
            ViewBag.Discounted = discounted ?? false;

            var result = await query
                .OrderByDescending(t => t.IsRecommended)
                .ThenBy(t => t.DisplayOrder)
                .ThenBy(t => t.StartDate)
                .ToListAsync();
            return View(result);
        }

        // ============================
        //           DETAILS
        // ============================
        public async Task<IActionResult> Details(int id)
        {
            var travel = await _context.TravelDestinations
                .FirstOrDefaultAsync(t => t.Id == id && t.IsVisible);

            if (travel == null)
                return NotFound();

            // OPTIONAL: show similar trips
            var similarTrips = await _context.TravelDestinations
                .Where(t => t.PackageType == travel.PackageType && t.Id != id)
                .Take(3)
                .ToListAsync();

            ViewBag.SimilarTrips = similarTrips;

            return View(travel);
        }

        // ============================
        //           BOOK
        // ============================
        [HttpGet]
        public async Task<IActionResult> Book(int id)
        {
            var travel = await _context.TravelDestinations.FirstOrDefaultAsync(t => t.Id == id);
            if (travel == null)
                return NotFound();

            ViewBag.Travel = travel;
            return View(new BookingRequestViewModel { TravelId = id, Rooms = 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(BookingRequestViewModel model)
        {
            var travel = await _context.TravelDestinations.FirstOrDefaultAsync(t => t.Id == model.TravelId);
            if (travel == null)
                return NotFound();

            // Booking limit check
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                TempData["Message"] = "Please sign in to book a trip.";
                return RedirectToAction("Index", "Login");
            }

            var activeCount = await _context.Bookings.CountAsync(b => b.UserEmail == userEmail && b.Status == "Booked");
            if (activeCount >= 3)
            {
                TempData["Message"] = "You can only hold up to 3 active bookings at once.";
                return RedirectToAction("Details", new { id = model.TravelId });
            }

            var daysUntil = (travel.StartDate - DateTime.UtcNow).TotalDays;
            if (daysUntil < travel.BookableDaysBeforeStart)
            {
                TempData["Message"] = $"This trip must be booked at least {travel.BookableDaysBeforeStart} days before departure.";
                return RedirectToAction("Details", new { id = model.TravelId });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Travel = travel;
                return View(model);
            }

            if (travel.AvailableRooms >= model.Rooms)
            {
                var extraRooms = Math.Max(0, model.Rooms - 1);
                var basePrice = travel.DiscountPrice.HasValue && travel.DiscountEndsAt.HasValue && travel.DiscountEndsAt.Value > DateTime.UtcNow
                    ? travel.DiscountPrice.Value
                    : travel.Price;
                var total = basePrice + (extraRooms * 100);

                var booking = new Booking
                {
                    TravelId = model.TravelId,
                    UserEmail = userEmail,
                    Rooms = model.Rooms,
                    CreatedAt = DateTime.UtcNow,
                    TotalPrice = total,
                    Status = "Booked"
                };

                travel.AvailableRooms -= model.Rooms;
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Added to cart. Review and pay in checkout.";
                TempData["AddedToCart"] = true;
                return RedirectToAction("Checkout", "Cart", new { id = booking.Id });
            }
            else
            {
                _context.WaitlistEntries.Add(new WaitlistEntry
                {
                    TravelId = model.TravelId,
                    UserEmail = userEmail,
                    RequestedRooms = model.Rooms,
                    CreatedAt = DateTime.UtcNow,
                    Notified = false
                });

                await _context.SaveChangesAsync();
                TempData["Message"] = "No availability. You were added to the waitlist and will be notified when space opens.";
            }

            return RedirectToAction("Details", new { id = model.TravelId });
        }
    }
}
