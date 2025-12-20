using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasman.Data;
using Tasman.Filters;
using Tasman.Models;
using System;

namespace Tasman.Controllers
{
    [AdminAuthorize]
    public class AdminTravelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminTravelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var travels = await _context.TravelDestinations.ToListAsync();
            return View(travels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Travel
            {
                PackageType = "adventure",
                AgeLimitation = "All ages",
                StartDate = DateTime.UtcNow.AddDays(30),
                EndDate = DateTime.UtcNow.AddDays(37),
                AvailableRooms = 10
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Travel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.TravelDestinations.Add(model);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Destination created.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var travel = await _context.TravelDestinations.FindAsync(id);
            if (travel == null)
                return NotFound();

            return View(travel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Travel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.TravelDestinations.Update(model);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Destination updated.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var travel = await _context.TravelDestinations.FindAsync(id);
            if (travel == null)
                return NotFound();

            _context.TravelDestinations.Remove(travel);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Destination removed.";
            return RedirectToAction("Index");
        }
    }
}
