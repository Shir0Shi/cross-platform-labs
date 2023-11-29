using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

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
            var user = await _context.L5Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password))).Replace("-", "").ToLower();
                var user = new L5User
                {
                    Username = model.Username,
                    FullName = model.FullName,
                    Password = hashedPassword,
                    PhoneNumber = model.Phone,
                    Email = model.Email
                };

                _context.L5Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Profile", new { userId = user.UserId });
            }

            return View(model);
        }



        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var redirectUrl = Url.Action("Profile", "Account");
                return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, "oidc");
            }

            return View(model);
        }

        public async Task<IActionResult> HandleExternalLogin(string returnUrl)
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("oidc");

            if (authenticateResult?.Succeeded == true)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction(nameof(Login));
        }

    }
}
