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
    [Route("odata/EmployeeType")]
    [ApiController]
    public class EmployeeTypeController : ODataController
    {
        private readonly IEmployeeTypeRepository employeeTypeRepository;
        public EmployeeTypeController()
        {
            employeeTypeRepository = new EmployeeTypeRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeType>>> GetAllEmployeeTypes()
        {
            var list = await employeeTypeRepository.GetAllEmployeeTypes();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeType>> GetEmployeeTypeById([FromODataUri] int id)
        {
            var employeetype = await employeeTypeRepository.GetEmployeeTypeById(id);
            if (employeetype == null)
            {
                return NotFound();
            }
            return Ok(employeetype);
        }

        [HttpPost]
        public async Task AddEmployeeType([FromBody] EmployeeType employeetype)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                     BadRequest(ModelState);
                }
                await employeeTypeRepository.AddEmployeeType(employeetype);
                //return Created(employeetype);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeType>> UpdateEmployeeType([FromODataUri] int id, [FromBody] EmployeeType employeetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var employeetypeToUpdate = await employeeTypeRepository.GetEmployeeTypeById(id);
            if (employeetypeToUpdate == null)
            {
                return NotFound();
            }
            employeetype.EmployeeTypeId = employeetypeToUpdate.EmployeeTypeId;
            await employeeTypeRepository.UpdateEmployeeType(employeetype);
            return Updated(employeetype);
        }
    }
}
