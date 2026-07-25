using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Inventory_SYstem.Data;
using Inventory_SYstem.Models;
using System.Linq;

namespace Inventory_SYstem.Controllers
{
    public class StockOutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockOutController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("Username") != null;
        }

        // GET
        public IActionResult Index()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            ViewBag.Products = _context.Products.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Index(StockOut stockOut)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                var product = _context.Products.Find(stockOut.ProductId);

                if (product != null)
                {
                    // Check available stock
                    if (product.Quantity < stockOut.Quantity)
                    {
                        ViewBag.Error = "Insufficient Stock!";
                        ViewBag.Products = _context.Products.ToList();
                        return View(stockOut);
                    }

                    // Reduce stock
                    product.Quantity -= stockOut.Quantity;

                    // Save Stock Out record
                    _context.StockOuts.Add(stockOut);

                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            ViewBag.Products = _context.Products.ToList();
            return View(stockOut);
        }
    }
}