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
using System;

namespace RMRBDClient.Controllers
{
    public class LoginPagesController : BaseController
    {
        public LoginPagesController(IConfiguration configuration) : base(configuration) { }

        // Đăng nhập với Google

        public IActionResult LoginGoogle() => Challenge(new AuthenticationProperties { RedirectUri = Url.Action("Login", "LoginPages") }, "Google");

        // Sử lý đăng nhập

        [Authorize]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {

                // Đăng nhập nhân viên

                string GoogleID = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                HttpResponseMessage response = await _httpClient.GetAsync($"{EmployeeUrl}?$filter=GoogleId eq '{GoogleID}' and Status eq 1");
                response.Headers.Add("Token", "123-abc");
                response.EnsureSuccessStatusCode();
                var employee = JsonConvert.DeserializeObject<List<Employee>>(await response.Content.ReadAsStringAsync()).FirstOrDefault();

                if (employee == null)
                {

                    // Đăng nhập Customer

                    response = await _httpClient.GetAsync($"{CustomerUrl}?$filter=GoogleId eq '{GoogleID}'");
                    response.Headers.Add("Token", "123-abc");
                    response.EnsureSuccessStatusCode();
                    var customer = JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync()).FirstOrDefault();

                    if (customer == null)
                    {

                        // Đăng ký Customer => đăng nhập

                        Customer newCustomer = new Customer();
                        newCustomer.Email = User.FindFirst(ClaimTypes.Email)?.Value;
                        newCustomer.GoogleId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                        newCustomer.UserName = User.FindFirst(ClaimTypes.GivenName)?.Value + " " + User.FindFirst(ClaimTypes.Surname)?.Value;
                        newCustomer.Avatar = "/Image/Avatar/NullAvatar.jpg";

                        var model = new
                        {
                            newCustomer.Email,
                            newCustomer.GoogleId,
                            newCustomer.UserName,
                            newCustomer.Avatar
                        };

                        var json = JsonConvert.SerializeObject(model);
                        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        try
                        {
                            response = await _httpClient.PostAsync($"{CustomerUrl}", content);
                            response.Headers.Add("Token", "123-abc");
                            response.EnsureSuccessStatusCode();

                            response = await _httpClient.GetAsync($"{CustomerUrl}?$filter=GoogleId eq '{GoogleID}'");
                            response.Headers.Add("Token", "123-abc");
                            response.EnsureSuccessStatusCode();
                            var customerIn = JsonConvert.DeserializeObject<List<Customer>>(await response.Content.ReadAsStringAsync()).FirstOrDefault();

                            HttpContext.Session.SetInt32("CustomerId", customerIn.CustomerId);
                            HttpContext.Session.SetString("UserName", customerIn.UserName);
                            HttpContext.Session.SetInt32("Coin", customerIn.Coin);
                            HttpContext.Session.SetString("Avatar", customerIn.Avatar);
                            HttpContext.Session.SetInt32("SellerStatus", customerIn.SellerStatus);

                            ViewData["CustomerId"] = customerIn.CustomerId;
                            ViewData["UserName"] = customerIn.UserName;
                            ViewData["Coin"] = customerIn.Coin;
                            ViewData["Avatar"] = customerIn.Avatar;
                            ViewData["SellerStatus"] = customerIn.SellerStatus;

                            return RedirectToAction("HomePage", "Home");
                        }catch(Exception ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");
                        }                       
                    }
                    else if (customer != null && customer.AccountStatus == 1)
                    {

                        // Đăng nhập Customer thành công

                        HttpContext.Session.SetInt32("CustomerId", customer.CustomerId);
                        HttpContext.Session.SetString("UserName", customer.UserName);
                        HttpContext.Session.SetInt32("Coin", customer.Coin);
                        HttpContext.Session.SetString("Avatar", customer.Avatar);
                        HttpContext.Session.SetInt32("SellerStatus", customer.SellerStatus);

                        ViewData["CustomerId"] = customer.CustomerId;
                        ViewData["UserName"] = customer.UserName;
                        ViewData["Coin"] = customer.Coin;
                        ViewData["Avatar"] = customer.Avatar;
                        ViewData["SellerStatus"] = customer.SellerStatus;

                        return RedirectToAction("HomePage", "Home");
                    }
                    else if (customer != null && customer.AccountStatus == 0)
                    {

                        // Tài khoản customer bị khóa

                        return RedirectToAction("Logout", "LoginPages");
                    }

                }
                else if (employee != null && employee.EmployeeTypeId == 1)
                {
                    // Kiểm duyệt viên

                    HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
                    HttpContext.Session.SetString("EmployeeName", employee.Name);
                    HttpContext.Session.SetInt32("EmployeeType", employee.EmployeeTypeId);
                    return Redirect("https://youtube.com");
                }
                else if (employee != null && employee.EmployeeTypeId == 2)
                {
                    // Quản lý đơn hàng

                    HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
                    HttpContext.Session.SetString("EmployeeName", employee.Name);
                    HttpContext.Session.SetInt32("EmployeeType", employee.EmployeeTypeId);
                    return Redirect("https://youtube.com");
                }else if (employee != null && employee.EmployeeTypeId == 3)
                {
                    // Admin

                    HttpContext.Session.SetInt32("EmployeeId", employee.EmployeeId);
                    HttpContext.Session.SetString("EmployeeName", employee.Name);
                    HttpContext.Session.SetInt32("EmployeeType", employee.EmployeeTypeId);
                    return Redirect("https://youtube.com");
                }
                else { }
            }
            return RedirectToAction("Logout", "LoginPages");
        }

        // Đăng xuất

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

            return RedirectToAction("LandingPage", "Home");
        }
    }
}
