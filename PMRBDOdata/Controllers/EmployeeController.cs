using BusinessObject.Models;
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
    }
}
