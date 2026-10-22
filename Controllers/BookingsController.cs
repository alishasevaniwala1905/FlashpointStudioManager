
using FlashpointStudioManager.Data;
using FlashpointStudioManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlashpointStudioManager.Controllers
{
    public class BookingsController : Controller
    {
        private readonly StudioDbContext _context;

        public BookingsController(StudioDbContext context)
        {
            _context = context;
        }

        // Display all bookings
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .ToListAsync();

            return View(bookings);
        }

        // Open Add Booking page
        public async Task<IActionResult> Create()
        {
            await LoadCustomers();
            return View();
        }

        // Save new booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadCustomers();
            return View(booking);
        }

        // Load customers into dropdown
        private async Task LoadCustomers()
        {
            var customers = await _context.Customers
                .OrderBy(c => c.FullName)
                .ToListAsync();

            ViewBag.CustomerId = new SelectList(
                customers,
                "Id",
                "FullName"
            );

            ViewBag.CustomerCount = customers.Count;
        }
    }
}
