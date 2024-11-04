using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using DataAccess.DAO;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Login")]
    [ApiController]
    public class LoginController : ODataController
    {
        public class LoginRequest
        {
            public string GoogleId { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Check GoogleID in Employee table through DAO
            var checkAccount = await AccountDAO.Instance.GetAccountByGoogleId(request.GoogleId);

            if (checkAccount != null && checkAccount.AccountStatus == 1)
            {
                // Assign role based on EmployeeTypeId
                string role;

                switch (checkAccount.RoleId)
                {
                    case 1: // Customer
                        role = "Customer";
                        break;
                    case 2: // Seller
                        role = "Seller";
                        break;
                    case 3: // Moderator
                        role = "Moderator";
                        break;
                    case 4: // Order Distributor
                        role = "Order Distributor";
                        break;
                    case 5: // Admin
                        role = "Admin";
                        break;
                    default:
                        return Unauthorized(new { message = "Invalid role" });
                }

                HttpContext.Session.SetString("UserRole", role);
                HttpContext.Session.SetString("UserName", checkAccount.UserName);
                return Ok(new { message = $"Logged in as {role}", role });
            }
                    // Create a new Customer account if not found
                    var newCustomer = new Account
                    {
                        GoogleId = request.GoogleId,
                        Email = request.Email,
                        UserName = request.UserName,
                        Coin = 0,
                        RoleId = 1,
                        AccountStatus = 1
                    };

                    await AccountDAO.Instance.AddAccount(newCustomer);
                    HttpContext.Session.SetString("UserRole", "Customer");
                    HttpContext.Session.SetString("UserName", newCustomer.UserName);
                    return Ok(new { message = "New Customer created and logged in", role = "Customer" });   
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // Xóa toàn bộ session
            return Ok(); // Trả về phản hồi thành công
        }
    }
}
