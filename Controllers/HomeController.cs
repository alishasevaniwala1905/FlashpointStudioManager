
using FlashpointStudioManager.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlashpointStudioManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudioDbContext _context;

        public HomeController(StudioDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalCustomers = _context.Customers.Count();
            ViewBag.TotalBookings = _context.Bookings.Count();

            ViewBag.PendingPayments = _context.Bookings
                .Where(b => b.Status == "Pending")
                .Sum(b => (decimal?)b.PackageAmount) ?? 0;

            return View();
        }
    }
}
