using Inventory_SYstem.Data;
using Inventory_SYstem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_SYstem.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, string transactionType)
        {
            var stockInHistory = (from s in _context.StockIns
                                  join p in _context.Products
                                  on s.ProductId equals p.ProductId
                                  select new TransactionHistoryViewModel
                                  {
                                      ProductName = p.ProductName,
                                      Quantity = s.Quantity,
                                      TransactionType = "Stock In",
                                      Date = s.Date
                                  }).ToList();

            var stockOutHistory = (from s in _context.StockOuts
                                   join p in _context.Products
                                   on s.ProductId equals p.ProductId
                                   select new TransactionHistoryViewModel
                                   {
                                       ProductName = p.ProductName,
                                       Quantity = s.Quantity,
                                       TransactionType = "Stock Out",
                                       Date = s.Date
                                   }).ToList();

            var history = stockInHistory
                .Concat(stockOutHistory)
                .OrderByDescending(x => x.Date)
                .ToList();

            // Search
            if (!string.IsNullOrEmpty(searchString))
            {
                history = history
                    .Where(h => h.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filter
            if (!string.IsNullOrEmpty(transactionType) && transactionType != "All")
            {
                history = history
                    .Where(h => h.TransactionType == transactionType)
                    .ToList();
            }

            ViewBag.SearchString = searchString;
            ViewBag.TransactionType = transactionType;

            return View(history);
        }
    }
}