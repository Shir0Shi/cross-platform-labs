using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Profile(int userId)
        {
            //TODO make auth
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
    }
}
