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

        public async Task<IActionResult> Index()
        {
            var travels = await _context.TravelDestinations.ToListAsync();
            return View(travels);
        }
    }
}
