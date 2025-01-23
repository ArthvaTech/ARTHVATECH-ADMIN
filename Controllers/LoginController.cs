using Microsoft.AspNetCore.Mvc;
using ARTHVATECH_ADMIN.Interface;
using ARTHVATECH_ADMIN.Models;
using ARTHVATECH_ADMIN.Common;
using ARTHVATECH_ADMIN.Constants;
using static ARTHVATECH_ADMIN.Constants.AppConstant;
using ARTHVATECH_ADMIN.Repository;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
            if (user != null)
            {
                bool isVerified = hasher.VerifyPassword(user.PasswordHash, password);

                if (isVerified)
                {
                    // Set up user claims
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                    new Claim(ClaimTypes.Email, user.Username)
                };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    // Sign in the user
                    HttpContext.SignInAsync("CookieAuth", principal);
                    AppConstant.Username = user.FirstName + " " + user.LastName;
                    AppConstant.RoleName = user.RoleName;
                    AppConstant.Menus = _loginrepo.GetMaster();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Login");
                }
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
        public ActionResult Register(string email, string FirstName, string LastName)
        {
            //var users = new Users();
            //users.Id = Guid.NewGuid();
            //users.TempCode = hasher.HashPassword(password);
            //users.Password = password;
            //users.FirstName = FirstName;
            //users.LastName = LastName;
            //users.Username = email;
            //_loginrepo.CreateUser(users);
            //return View();
            var exist = _loginrepo.GetUserById(email);
            if (exist == null) {
                var users = new Users();
                users.Id = Guid.NewGuid();
                users.TempCode = _loginrepo.GenerateRandomString(10);
                users.FirstName = FirstName;
                users.LastName = LastName;
                users.Username = email;
                _loginrepo.CreateUser(users);
                ViewBag.message = "Please Wait for Admin to Approve You Will Receieve Email Soon";
            }
            else
            {
                ViewBag.message = "Email Already Exist";
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }

    }
}
