using ARTHVATECH_ADMIN.Interface;
using ARTHVATECH_ADMIN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet]
        [Route("Administrators/Edit")]
        public IActionResult UserEdit(string id)
        {
            Users user = new Users();
            if (!string.IsNullOrEmpty(id))
            {
                user = _administrator.GetUserById(id);
                ViewBag.Roles = _administrator.GetRoles().Select(role => new { Value = role.RoleId, Text = role.RoleName }).ToList();

            }
            return View("UserEdit", user);
        }
        [HttpPost]
        public IActionResult Save(ARTHVATECH_ADMIN.Models.Users model)
        {
            if (model != null)
            {
                Users user = new Users();
                user.Id = model.Id;
                user.TempCode = model.TempCode ?? string.Empty;
                user.FirstName = model.FirstName ?? string.Empty;
                user.LastName = model.LastName ?? string.Empty;
                user.Designation = model.Designation ?? string.Empty;
                user.RoleId = model.RoleId ?? Guid.Empty;
                _administrator.UpdateUser(user);
                EmailHelper.EmailService emailService = new EmailHelper.EmailService();
                string html = "<div class='card'><div class='card-header'><h5 class='card-title'>Blockquotes</h5><h6 class='card-subtitle text-muted'>For quoting blocks of content from another source within your document.</h6></div><div class='card-body'><blockquote><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p><footer>Someone famous in <cite title='Source Title'>Source Title</cite></footer></blockquote><p class='text-muted m-b-15 m-t-20 font-13'>Add <code>.blockquote-reverse</code> for a blockquote with right-aligned content.</p><blockquote class='blockquote-reverse m-b-0'><p>\tLorem ipsum dolor sit amet, consectetur adipiscing elit. Integer posuere erat a ante.</p><footer>Someone famous in <cite title='Source Title'>Source Title</cite></footer></blockquote></div></div><a href='Arthvatech.com'>Hello</a>";
                emailService.SendEmail(model.Username, "Invitation", "",html);
            }
            return RedirectToAction("UserEdit",new { id = model.Id.ToString()});
        }
    }
}
