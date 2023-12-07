using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Services;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly Data.DbContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<L5User> _signInManager;
        private readonly UserManager<L5User> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AccountController(SignInManager<L5User> signInManager, 
            UserManager<L5User> userManager,
            Data.DbContext context, 
            ILogger<AccountController> logger, 
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _interactionService = interactionService;
        }

        public async Task<IActionResult> Profile()
        {
            var userName = User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                _logger.LogError("User.Identity.Name is null or empty.");
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByNameAsync(User?.Identity?.Name);
            if (user == null)
            {
                return NotFound();
            }

            var userInfo = new UserViewModel()
            {
                Username = user.UserName,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
            };

            return View(userInfo);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Register attempt for user {Username}", model.Username);
                
                var userWithSameUserName = await _userManager.FindByNameAsync(model.Username);
                if (userWithSameUserName != null)
                {
                    ModelState.AddModelError("", "This Username is already taken");
                    return View(model);
                }

                var hashedPassword = BitConverter.ToString(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(model.Password))).Replace("-", "").ToLower();
                var user = new L5User
                {
                    UserName = model.Username,
                    FullName = model.FullName,
                    Password = hashedPassword,
                    PhoneNumber = model.Phone,
                    Email = model.Email
                };

                _logger.LogInformation("user entity userName {Username}", user.UserName);

                var result = await _userManager.CreateAsync(user, model.Password);
                //var result = _context.L5User.Add(user);
                

                _logger.LogInformation("Result {username}", user.UserName);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Registration error");
                    return View(model);
                }

                await _signInManager.SignInAsync(user, true);

                return RedirectToAction("Profile");
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
            _logger.LogInformation("Login attempt for user {Username}", model.Username);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {Username} logged in.", model.Username);
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

    }
}

