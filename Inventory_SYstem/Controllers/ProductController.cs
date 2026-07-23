using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Inventory_SYstem.Data;
using Inventory_SYstem.Models;

namespace Inventory_SYstem.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("Username") != null;
        }

        public IActionResult Index(string searchString)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p =>
                    p.ProductName.Contains(searchString) ||
                    p.Category.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(products.ToList());
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Category(string category)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var products = _context.Products
                .Where(p => p.Category == category)
                .ToList();

            return View(products);
        }

        public IActionResult LowStock()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var products = _context.Products
                .Where(p => p.Quantity < 5)
                .ToList();

            return View(products);
        }
    }
}