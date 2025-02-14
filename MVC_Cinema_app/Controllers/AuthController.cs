using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BusinessLogic.DTOs;


namespace MVC_Cinema_app.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["LoginModel"] = new LoginDTO();
            ViewData["RegisterModel"] = new RegisterDTO();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["RegisterModel"] = new RegisterDTO();
                return View("Index", model);
            }

            var user = await _userService.AuthenticateUserAsync(model.LoginEmail, model.LoginPassword);
            if (user == null)
            {
                ModelState.AddModelError("LoginPassword", "Невірний логін або пароль.");
                ViewData["RegisterModel"] = new RegisterDTO();
                return View("Index", model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Status.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["LoginModel"] = new LoginDTO();
                return View("Index", model);
            }
            if (model.RegisterPassword != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                ViewData["LoginModel"] = new LoginDTO();
                return View("Index", model);
            }
            var status = await _userService.GetOrCreateUserStatusAsync(UserStatusDTO.Active);

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.RegisterEmail,
                PhoneNumber = model.PhoneNumber,
                Password = model.RegisterPassword,
                StatusId = status.Id
            };

            await _userService.RegisterUserAsync(user);

            // Автоматичний вхід в систему після реєстрації
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Status.Name)
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}