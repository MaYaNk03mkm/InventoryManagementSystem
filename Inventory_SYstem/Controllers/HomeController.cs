using Inventory_SYstem.Data;
using System.Diagnostics;

using Inventory_SYstem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_SYstem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var model = new DashboardViewModel
            {
                TotalProducts = _context.Products.Count(),

                ElectronicsCount = _context.Products
                    .Count(p => p.Category == "Electronics"),

                FurnitureCount = _context.Products
                    .Count(p => p.Category == "Furniture"),

                GroceryCount = _context.Products
                    .Count(p => p.Category == "Grocery"),

                SportsCount = _context.Products
                    .Count(p => p.Category == "Sports"),

                LowStockCount = _context.Products
                    .Count(p => p.Quantity < 5),
                TotalStockQuantity = _context.Products.Sum(p => p.Quantity),

                TotalStockIn = _context.StockIns.Sum(s => s.Quantity),

                TotalStockOut = _context.StockOuts.Sum(s => s.Quantity),

                InventoryValue = _context.Products.Sum(p => p.Price * p.Quantity)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
