
using FlashpointStudioManager.Data;
using FlashpointStudioManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlashpointStudioManager.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly StudioDbContext _context;

        public PaymentsController(StudioDbContext context)
        {
            _context = context;
        }

        // Display all payments
        public async Task<IActionResult> Index()
        {
            var payments = await _context.Payments
                .Include(p => p.Booking)
                .ThenInclude(b => b!.Customer)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            ViewBag.TotalPaid = payments.Sum(p => p.Amount);

            return View(payments);
        }

        // Open Add Payment page
        public async Task<IActionResult> Create()
        {
            await LoadBookings();
            return View();
        }

        // Save payment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadBookings();
            return View(payment);
        }

        private async Task LoadBookings()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .ToListAsync();

            ViewBag.BookingId = new SelectList(
                bookings.Select(b => new
                {
                    b.Id,
                    BookingName = "Booking #" + b.Id + " - " +
                                  (b.Customer != null
                                      ? b.Customer.FullName
                                      : "Customer")
                }),
                "Id",
                "BookingName");
        }
    }
}
