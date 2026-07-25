using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Inventory_SYstem.Data;
using Inventory_SYstem.Models;
using System.Linq;

namespace Inventory_SYstem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool userExists = _context.Users.Any(x =>
                    x.Username == user.Username || x.Email == user.Email);

                if (userExists)
                {
                    ViewBag.Error = "Username or Email already exists.";
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x =>
                    x.Username == username &&
                    x.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (user.Role == "Supplier")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (user.Role == "Buyer")
                {
                    return RedirectToAction("Index", "Home");
                }

                // Default redirect for any other role
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Username or Password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}