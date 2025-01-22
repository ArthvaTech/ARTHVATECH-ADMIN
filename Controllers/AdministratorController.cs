using ARTHVATECH_ADMIN.Interface;
using ARTHVATECH_ADMIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ARTHVATECH_ADMIN.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
        private readonly IAdministrator _administrator;
        public AdministratorController(IAdministrator administrator)
        {
            _administrator = administrator;
        }
        [Route("Administrators")]
        public IActionResult Administrators()
        {
            List<Users> users = new List<Users>();
            users = _administrator.GetUsers();
            return View(users);
        }
    }
}
