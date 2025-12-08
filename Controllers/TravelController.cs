using Microsoft.AspNetCore.Mvc;
using Tasman.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
            string? startDate)
        {
            var query = _context.TravelDestinations.AsQueryable();

            // SEARCH FILTER
            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.Destination.Contains(search));

            // CATEGORY FILTER
            if (!string.IsNullOrEmpty(PackageType))
                query = query.Where(t => t.PackageType.ToLower() == PackageType.ToLower());

            // PRICE FILTER
            if (maxPrice.HasValue)
                query = query.Where(t => t.Price <= maxPrice.Value);

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
            ViewBag.MaxPrice = maxPrice ?? 0;

            var result = await query.ToListAsync();
            return View(result);
        }

        // ============================
        //           DETAILS
        // ============================
        public async Task<IActionResult> Details(int id)
        {
            var travel = await _context.TravelDestinations
                .FirstOrDefaultAsync(t => t.Id == id);

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
    }
}
