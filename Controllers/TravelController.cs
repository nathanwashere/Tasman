using Microsoft.AspNetCore.Mvc;
using Tasman.Data;
using Microsoft.EntityFrameworkCore;

namespace Tasman.Controllers
{
    public class TravelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelController(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<IActionResult> Index(
                string? search,
                string? category,
                string? sort,
                int? maxPrice)
            {
                var query = _context.TravelDestinations.AsQueryable();

                // Search
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(t => t.Destination.Contains(search));
                }

                // Category (case-insensitive)
                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(t => t.PackageType.ToLower() == category.ToLower());
                }

                // Max price
                if (maxPrice.HasValue)
                {
                    query = query.Where(t => t.Price <= maxPrice.Value);
                }

                // Sorting
                query = sort switch
                {
                    "price-asc" => query.OrderBy(t => t.Price),
                    "price-desc" => query.OrderByDescending(t => t.Price),
                    _ => query
                };

                // Pass current filters back to ViewBag so inputs stay filled
                ViewBag.Search = search;
                ViewBag.Category = category;
                ViewBag.Sort = sort;
                ViewBag.MaxPrice = maxPrice ?? 0;

                var result = await query.ToListAsync();
                return View(result);
            }



    }
}
