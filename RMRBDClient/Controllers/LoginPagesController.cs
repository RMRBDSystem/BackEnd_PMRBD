using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RMRBDClient.Controllers
{
    public class LoginPagesController : BaseController
    {
        public LoginPagesController(IConfiguration configuration) : base(configuration) { }

        public IActionResult LoginGoogle() => Challenge(new AuthenticationProperties { RedirectUri = Url.Action("Login", "LoginPages") }, "Google");

        [Authorize]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("https://facebook.com");
            }
            return RedirectToAction("Logout", "LoginPages");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("CustomerId");
            HttpContext.Session.Remove("UserName");

            HttpContext.Session.Remove("EmployeeId");
            HttpContext.Session.Remove("EmployeeName");
            HttpContext.Session.Remove("EmployeeType");

            // Redirect to home page or any other page
            return RedirectToAction("Index", "Home");
        }
    }
}
