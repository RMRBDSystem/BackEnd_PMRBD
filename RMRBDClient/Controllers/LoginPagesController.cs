using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Newtonsoft.Json.Linq;
using Azure;
using System.Linq;

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
                string GoogleID = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                HttpResponseMessage response = await _httpClient.GetAsync($"{EmployeeUrl}?$filter=GoogleId eq '{GoogleID}'");
                response.EnsureSuccessStatusCode(); 
                var employee = JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync()).FirstOrDefault();

                if (employee == null)
                {
                    response = await _httpClient.GetAsync($"{CustomerUrl}?$filter=GoogleId eq '{GoogleID}'");
                    response.EnsureSuccessStatusCode();
                    var customer = JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync()).FirstOrDefault();

                    if (customer == null)
                    {
                        Customer newCustomer = new Customer();
                        newCustomer.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                        newCustomer.GoogleId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                        newCustomer.UserName = User.FindFirst(ClaimTypes.GivenName)?.Value + " " + User.FindFirst(ClaimTypes.Surname)?.Value;
                        newCustomer.Avatar = "/wwwroot/Image/Avatar/NullAvatar.jpg";

                        var model = new
                        {
                            newCustomer.Email,
                            newCustomer.GoogleId,
                            newCustomer.UserName,
                            newCustomer.Avatar
                        };

                        var json = JsonConvert.SerializeObject(model);
                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                        response = await _httpClient.PostAsync($"{CustomerUrl}", content);
                        response.EnsureSuccessStatusCode();
                        return Redirect("https://youtube.com");
                    }
                    else if (customer != null && customer.AccountStatus == 1)
                    {
                        HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
                        HttpContext.Session.SetString("UserName", customer.UserName);
                        HttpContext.Session.SetInt32("Coin", customer.Coin);
                        HttpContext.Session.SetString("Avatar", customer.Avatar);

                        ViewData["CustomerId"] = customer.CustomerId;
                        ViewData["Avatar"] = customer.Avatar;
                        return RedirectToAction("Index", "Home");
                    }
                    else if (customer != null && customer.AccountStatus == 0)
                    {
                        return RedirectToAction("Logout", "LoginPages");
                    }

                }
                else if (employee != null && employee.Status == 1)
                {
                    HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
                    HttpContext.Session.SetString("EmployeeName", employee.Name);
                    HttpContext.Session.SetInt32("EmployeeType", employee.EmployeeTypeId);
                    return Redirect("https://youtube.com");
                }
                else if (employee != null && employee.Status == 0)
                {
                    return RedirectToAction("Logout", "LoginPages");
                }
            }
            return RedirectToAction("Logout", "LoginPages");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("CustomerId");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Coin");
            HttpContext.Session.Remove("Avatar");

            HttpContext.Session.Remove("EmployeeId");
            HttpContext.Session.Remove("EmployeeName");
            HttpContext.Session.Remove("EmployeeType");

            // Redirect to home page or any other page
            return RedirectToAction("Index", "Home");
        }
    }
}
