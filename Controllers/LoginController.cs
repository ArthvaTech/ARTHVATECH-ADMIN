using Microsoft.AspNetCore.Mvc;
using ARTHVATECH_ADMIN.Interface;
using ARTHVATECH_ADMIN.Models;

namespace ARTHVATECH_ADMIN.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginrepo;
        public readonly PasswordHasher hasher;
        public LoginController(ILoginRepository loginrepo)
        {
            _loginrepo = loginrepo;
            hasher = new PasswordHasher();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public ActionResult Rergister(string email, string FirstName, string LastName, string password)
        {
            return View();
        }
    }
}
