using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;
using Tasman.Data;

namespace Tasman.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private record PaymentSummary(string Email, string MaskedCard, string Name, string Phone);
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Index", "Login");

            var bookings = await _context.Bookings
                .Include(b => b.Travel)
                .Where(b => b.UserEmail == email)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();

            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Index", "Login");

            var booking = await _context.Bookings.Include(b => b.Travel).FirstOrDefaultAsync(b => b.Id == id && b.UserEmail == email);
            if (booking == null || booking.Travel == null)
                return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            ViewBag.User = user;
            ViewBag.Message = TempData["Message"] as string;
            ViewBag.AddedToCart = TempData["AddedToCart"] != null;
            TempData.Keep("Message");
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int id, string cardNumber, string cardHolder, string expiry, string cvv, string phone, string email)
        {
            var sessionEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(sessionEmail))
                return RedirectToAction("Index", "Login");

            var booking = await _context.Bookings.Include(b => b.Travel).FirstOrDefaultAsync(b => b.Id == id && b.UserEmail == sessionEmail);
            if (booking == null || booking.Travel == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(cardHolder) ||
                string.IsNullOrWhiteSpace(cardNumber) ||
                string.IsNullOrWhiteSpace(expiry) ||
                string.IsNullOrWhiteSpace(cvv) ||
                string.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("", "Please fill in payment details to continue.");
            }

            if (!ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == sessionEmail);
                ViewBag.User = user;
                return View(booking);
            }

            booking.Status = "Paid";
            await _context.SaveChangesAsync();

            var summary = new PaymentSummary(
                email.Trim(),
                MaskCardNumber(cardNumber),
                cardHolder.Trim(),
                phone?.Trim() ?? string.Empty);

            TempData["PaymentSummary"] = JsonSerializer.Serialize(summary);
            TempData["Message"] = "Payment confirmed. Receipt is ready.";

            return RedirectToAction("PaymentConfirmation", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> PaymentConfirmation(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Index", "Login");

            var booking = await _context.Bookings.Include(b => b.Travel).FirstOrDefaultAsync(b => b.Id == id && b.UserEmail == email);
            if (booking == null || booking.Travel == null)
                return NotFound();

            var payment = ReadPaymentSummaryFromTempData();
            if (payment != null)
            {
                ViewBag.PaymentEmail = payment.Email;
                ViewBag.PaymentCard = payment.MaskedCard;
                ViewBag.PaymentName = payment.Name;
                ViewBag.PaymentPhone = payment.Phone;
                TempData.Keep("PaymentSummary");
            }

            ViewBag.Message = TempData["Message"] as string;
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Index", "Login");

            var booking = await _context.Bookings.Include(b => b.Travel).FirstOrDefaultAsync(b => b.Id == id && b.UserEmail == email);
            if (booking == null || booking.Travel == null)
                return NotFound();

            var cutoff = booking.Travel.StartDate.AddDays(-booking.Travel.CancellableDaysBeforeStart);
            if (DateTime.UtcNow > cutoff)
            {
                TempData["Message"] = "Cancellation window has closed.";
                return RedirectToAction("Index");
            }

            booking.Status = "Cancelled";
            booking.CancelledAt = DateTime.UtcNow;
            booking.Travel.AvailableRooms += booking.Rooms;
            await _context.SaveChangesAsync();

            TempData["Message"] = "Booking cancelled.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Receipt(int id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Index", "Login");

            var booking = await _context.Bookings.Include(b => b.Travel).FirstOrDefaultAsync(b => b.Id == id && b.UserEmail == email);
            if (booking == null || booking.Travel == null)
                return NotFound();

            var pdf = BuildSimplePdf(booking, ReadPaymentSummaryFromTempData());
            return File(pdf, "application/pdf", $"receipt-{booking.Id}.pdf");
        }

        private PaymentSummary? ReadPaymentSummaryFromTempData()
        {
            var json = TempData.Peek("PaymentSummary") as string;
            if (string.IsNullOrEmpty(json))
                return null;

            try
            {
                return JsonSerializer.Deserialize<PaymentSummary>(json);
            }
            catch
            {
                return null;
            }
        }

        private static string MaskCardNumber(string cardNumber)
        {
            var digits = new string(cardNumber.Where(char.IsDigit).ToArray());
            if (digits.Length < 4)
                return "****";

            var last4 = digits.Substring(digits.Length - 4);
            return $"**** **** **** {last4}";
        }

        private byte[] BuildSimplePdf(Models.Booking b, PaymentSummary? payment)
        {
            var travel = b.Travel!;
            var paymentLine = payment != null
                ? $"Payment: Paid via card {payment.MaskedCard} ({payment.Email})"
                : $"Payment: {b.Status}";
            var lines = new[]
            {
                "Tasman Receipt",
                $"Booking ID: {b.Id}",
                $"User: {b.UserEmail}",
                $"Destination: {travel.Destination} ({travel.Country})",
                $"Dates: {travel.StartDate:dd MMM yyyy} - {travel.EndDate:dd MMM yyyy}",
                $"Rooms: {b.Rooms}",
                $"Status: {b.Status}",
                $"Total: ${b.TotalPrice}",
                paymentLine,
                "Email receipts coming soon.",
                $"Created: {b.CreatedAt:yyyy-MM-dd HH:mm} UTC"
            };
            var text = string.Join("\\n", lines);
            var content = $"%PDF-1.4\\n1 0 obj<</Type/Catalog/Pages 2 0 R>>endobj\\n2 0 obj<</Type/Pages/Count 1/Kids[3 0 R]>>endobj\\n3 0 obj<</Type/Page/Parent 2 0 R/MediaBox[0 0 612 792]/Contents 4 0 R/Resources<</Font<</F1 5 0 R>>>>>>endobj\\n4 0 obj<</Length {text.Length + 73}>>stream\\nBT /F1 12 Tf 72 720 Td ({text.Replace("(", "\\(").Replace(")", "\\)").Replace("\\n", ") Tj\\n0 -16 Td (")}) Tj\\nET\\nendstream\\nendobj\\n5 0 obj<</Type/Font/Subtype/Type1/BaseFont/Helvetica>>endobj\\nxref\\n0 6\\n0000000000 65535 f \\n0000000010 00000 n \\n0000000060 00000 n \\n0000000117 00000 n \\n0000000280 00000 n \\n0000000500 00000 n \\ntrailer<</Size 6/Root 1 0 R>>\\nstartxref\\n580\\n%%EOF";
            return System.Text.Encoding.ASCII.GetBytes(content);
        }
    }
}
