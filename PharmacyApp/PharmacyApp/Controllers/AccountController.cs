using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PharmacyApp.Controllers
{
    public class AccountController : Controller
    {
        private PharmacyContext _context;
        public AccountController(PharmacyContext context)
        {
            _context = context;
        }

        [Route("register")]
        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [Route("register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User { Email = model.Email, Password = model.Password };
                    Role userRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    await Authenticate(user); // аутентификация

                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [Route("login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
                ModelState.AddModelError("", "Такого пользователя нет.");
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Login", "Account");
        }
    }
}