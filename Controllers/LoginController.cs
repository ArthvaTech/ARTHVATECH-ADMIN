using Microsoft.AspNetCore.Mvc;
using ARTHVATECH_ADMIN.Interface;
using ARTHVATECH_ADMIN.Models;
using ARTHVATECH_ADMIN.Common;
using ARTHVATECH_ADMIN.Constants;
using static ARTHVATECH_ADMIN.Constants.AppConstant;
using ARTHVATECH_ADMIN.Repository;

namespace ARTHVATECH_ADMIN.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _loginrepo;
        public readonly PasswordHasher hasher;
        public LoginController(IUserRepository loginrepo)
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
            var user = _loginrepo.GetUserById(email);
            bool isVerified = hasher.VerifyPassword(user.PasswordHash, password);

            if (isVerified)
            {
                AppConstant.Username = user.FirstName + " " +user.LastName;
                AppConstant.Menus = _loginrepo.GetMaster();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Login");
            }
         
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string email, string FirstName, string LastName, string password)
        {
            var users = new Users();
            users.Id = Guid.NewGuid();
            users.PasswordHash = hasher.HashPassword(password);
            users.Password = password;
            users.FirstName = FirstName;
            users.LastName = LastName;
            users.Username = email;
            _loginrepo.CreateUser(users);
            return View();
        }
    }
}
