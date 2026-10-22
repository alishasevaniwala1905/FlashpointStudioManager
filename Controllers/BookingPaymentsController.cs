
using FlashpointStudioManager.Data;
using FlashpointStudioManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashpointStudioManager.Controllers
{
    public class BookingPaymentsController : Controller
    {
        private readonly StudioDbContext _context;

        public BookingPaymentsController(StudioDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Payments)
                .OrderByDescending(b => b.EventDate)
                .ToListAsync();

            var result = bookings.Select(b => new BookingPaymentViewModel
            {
                BookingId = b.Id,
                CustomerName = b.Customer?.FullName ?? "Unknown",
                EventType = b.EventType,
                EventDate = b.EventDate,
                TotalAmount = b.PackageAmount,
                AmountPaid = b.Payments.Sum(p => p.Amount)
            }).ToList();

            return View(result);
        }
    }
}
