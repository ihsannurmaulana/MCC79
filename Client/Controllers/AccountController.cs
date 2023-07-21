using API.DTOs.Accounts;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;

        public AccountController(IAccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult LoginClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginClient(LoginDto login)
        {
            var result = await repository.Login(login);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Status == "BadRequest")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            else if (result.Status == "OK")
            {
                HttpContext.Session.SetString("JWToken", result.Data);
                return RedirectToAction("Index", "Employees");
            }
            return View();


        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerVM)
        {

            var result = await repository.Register(registerVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Status == "BadRequest")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
                return View();
            }
            else if (result.Status == "OK")
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginClient", "Account");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
