using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Services;

namespace MVC_Cinema_app.Controllers
{
    public class HashTestController : Controller
    {
        private readonly PasswordService _passwordService;

        public HashTestController(PasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Пароль не може бути порожнім";
                return View();
            }

            var hashedPassword = _passwordService.HashPassword(password);
            ViewBag.HashedPassword = hashedPassword;
            return View();
        }
    }
}

