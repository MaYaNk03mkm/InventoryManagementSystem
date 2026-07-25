using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Inventory_SYstem.Data;

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

        public IActionResult Index()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}