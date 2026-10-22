
using FlashpointStudioManager.Data;
using FlashpointStudioManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlashpointStudioManager.Controllers
{
    public class CustomersController : Controller
    {
        private readonly StudioDbContext _context;

        public CustomersController(StudioDbContext context)
        {
            _context = context;
        }

        // Display all customers and search by name or phone
        public IActionResult Index(string searchString)
        {
            var customers = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                customers = customers.Where(c =>
                    c.FullName.Contains(searchString) ||
                    c.Phone.Contains(searchString));
            }

            ViewBag.SearchString = searchString;

            return View(customers.ToList());
        }

        // Open Add Customer page
        public IActionResult Create()
        {
            return View();
        }

        // Save new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Open Edit Customer page
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // Save edited customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            _context.Customers.Update(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Delete customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
