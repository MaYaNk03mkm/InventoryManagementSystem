using Microsoft.AspNetCore.Mvc;
using Inventory_SYstem.Data;
using Inventory_SYstem.Models;

namespace Inventory_SYstem.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users
                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        public IActionResult ChangePassword()
        {
            var username = HttpContext.Session.GetString("Username");

            if (username == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(
            string currentPassword,
            string newPassword,
            string confirmPassword)
        {
            var username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users
                .FirstOrDefault(u =>
                    u.Username.Trim().ToLower() ==
                    username.Trim().ToLower());

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (currentPassword.Trim() != user.Password.Trim())
            {
                ViewBag.Error = "Current password is incorrect.";
                return View();
            }

            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "New passwords do not match.";
                return View();
            }

            user.Password = newPassword;

            _context.SaveChanges();

            ViewBag.Success = "Password changed successfully!";

            return View();
        }
    }
}