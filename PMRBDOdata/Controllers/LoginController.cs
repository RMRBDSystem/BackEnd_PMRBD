using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PMRBDOdata.Controllers
{
    [Route("Login")]
    [ApiController]
    public class LoginController : ControllerBase
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
            // 1. Kiểm tra GoogleID trong bảng Employee thông qua DAO
            var employee = await EmployeeDAO.Instance.GetEmployeeByGoogleId(request.GoogleId);

            if (employee != null && employee.Status == 1)
            {
                // Employee tồn tại và trạng thái là Active
                return Ok(new { message = "Logged in as Employee", role = "Employee" });
            }
            else
            {
                // 2. Kiểm tra GoogleID trong bảng Customer nếu không tìm thấy Employee
                var customer = await CustomerDAO.Instance.GetCustomerByGoogleId(request.GoogleId);

                if (customer == null)
                {
                    // 3. Tạo tài khoản mới cho Customer nếu chưa tồn tại
                    var newCustomer = new Customer
                    {
                        GoogleId = request.GoogleId,
                        Email = request.Email,
                        UserName = request.UserName,
                        Coin = 0,
                        SellerStatus = 0,
                        AccountStatus = 1
                    };

                    await CustomerDAO.Instance.AddCustomer(newCustomer);

                    // Đăng nhập thành công với vai trò Customer
                    return Ok(new { message = "New Customer created and logged in", role = "Customer" });
                }
                // 5. Đăng nhập thành công với vai trò Customer
                return Ok(new { message = "Logged in as Customer", role = "Customer" });
            }
        }
    }
}
