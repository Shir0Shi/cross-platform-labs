﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Security.Cryptography;
using System.Text;

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

                _context.Users.Add(user);
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

        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
    }
}
