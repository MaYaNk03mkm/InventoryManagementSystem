using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Inventory_SYstem.Data;
using Inventory_SYstem.Models;
using System.Linq;

namespace Inventory_SYstem.Controllers
{
    public class StockInController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockInController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("Username") != null;
        }

        // GET: StockIn
        public IActionResult Index()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            ViewBag.Products = _context.Products.ToList();

            return View();
        }

        // POST: StockIn
        [HttpPost]
        public IActionResult Index(StockIn stockIn)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                // Save Stock In record
                _context.StockIns.Add(stockIn);

                // Update product quantity
                var product = _context.Products.Find(stockIn.ProductId);

                if (product != null)
                {
                    product.Quantity += stockIn.Quantity;
                }

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Products = _context.Products.ToList();
            return View(stockIn);
        }
    }
}