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

        [HttpGet("session")]
        private IActionResult GetSessionInfo()
        {
            // Kiểm tra nếu session đã được thiết lập
            var userRole = HttpContext.Session.GetString("UserRole");
            var userName = HttpContext.Session.GetString("UserName");
            var userId = HttpContext.Session.GetInt32("UserId");
            var coin = HttpContext.Session.GetString("Coin");

            // Kiểm tra xem session có tồn tại không
            if (string.IsNullOrEmpty(userRole) || string.IsNullOrEmpty(userName) || userId == null)
            {
                return Unauthorized(new { message = "Session not found or expired" });
            }

            // Trả về thông tin session
            return Ok(new
            {
                UserRole = userRole,
                UserName = userName,
                UserId = userId,
                Coin = coin
            });
        }

        private void SetSession(Account account, string role)
        {
            HttpContext.Session.SetString("UserRole", role);
            HttpContext.Session.SetString("UserName", account.UserName);
            HttpContext.Session.SetInt32("UserId", account.AccountId);
            HttpContext.Session.SetString("Coin", account.Coin.ToString());
        }



        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            
            // Check GoogleID in Employee table through DAO
            var checkAccount = await AccountDAO.Instance.GetAccountByGoogleId(request.GoogleId);

            if (checkAccount != null && checkAccount.AccountStatus == 0)

            {
                return Unauthorized(new { message = "Tài khoản của bạn đã bị khóa!" });
            }
            if (checkAccount != null && checkAccount.AccountStatus == 1)
            {
                // Gán role dựa trên EmployeeTypeId
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


                // Gọi hàm để set session
                SetSession(checkAccount, role);


                // Trả về thông tin session sau khi đăng nhập
                return GetSessionInfo();
            }

            // Tạo mới tài khoản Customer nếu không tìm thấy
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
            int newCustomerId = (await AccountDAO.Instance.GetAccountByGoogleId(request.GoogleId)).AccountId;


            // Gọi hàm để set session cho Customer mới
            SetSession(newCustomer, "Customer");

            // Trả về thông tin session sau khi tạo tài khoản và đăng nhập

            return GetSessionInfo();
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
