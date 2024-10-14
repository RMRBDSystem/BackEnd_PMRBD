using BusinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Employee")]
    [ApiController]
    public class EmployeeController : ODataController
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeController()
        {
            employeeRepository = new EmployeeRepository();
        }

        
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var list = await employeeRepository.GetAllEmployees();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById([FromODataUri] int id)
        {
            var employee = await employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                await employeeRepository.AddEmployee(employee);
                return Created(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromODataUri] int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeeToUpdate = await employeeRepository.GetEmployeeById(id);
            if (employeeToUpdate == null)
            {
                return NotFound();
            }
            employee.EmployeeId = employeeToUpdate.EmployeeId;
            await employeeRepository.UpdateEmployee(employee);
            return Updated(employee);
        }

        public class LoginRequest
        {
            public string GoogleId { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Kiểm tra GoogleID trong bảng Employee thông qua DAO
            var employee = await EmployeeDAO.Instance.GetEmployeeByGoogleId(request.GoogleId);

            if (employee != null)
            {
                if (employee.Status == 1)
                {
                    // Employee tồn tại và trạng thái là Active
                    return Ok(new { message = "Logged in as Employee", role = "Employee" });
                }
                else
                {
                    // Tài khoản Employee bị chặn
                    return BadRequest(new { message = "Employee account is blocked" });
                }
            }

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
