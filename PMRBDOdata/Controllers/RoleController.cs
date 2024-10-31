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
    [Route("odata/Role")]
    [ApiController]
    public class RoleController : ODataController
    {
        private readonly IRoleRepository roleRepository;
        public RoleController()
        {
            roleRepository = new RoleRepository();
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            var list = await roleRepository.GetAllRoles();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRoleById([FromODataUri] int id)
        {
            var role = await roleRepository.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task AddRole([FromBody] Role role)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await roleRepository.AddRole(role);
                //return Created(role);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Role>> UpdateRole([FromODataUri] int id, [FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var roleToUpdate = await roleRepository.GetRoleById(id);
            if (roleToUpdate == null)
            {
                return NotFound();
            }
            role.RoleId = roleToUpdate.RoleId;
            await roleRepository.UpdateRole(role);
            return Updated(role);
        }
    }
}
