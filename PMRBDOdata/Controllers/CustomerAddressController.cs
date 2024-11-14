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
    [Route("odata/CustomerAddress")]
    [ApiController]
    public class CustomerAddressController : ODataController
    {
        private readonly ICustomerAddressRepository customerAddressRepository;
        public CustomerAddressController()
        {
            customerAddressRepository = new CustomerAddressRepository();
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> GetAllCustomerAddresss()
        {
            var list = await customerAddressRepository.GetAllCustomerAddress();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> GetCustomerAddressById([FromODataUri] int id)
        {
            var customerAddress = await customerAddressRepository.GetCustomerAddressById(id);
            if (customerAddress == null)
            {
                return NotFound();
            }
            return Ok(customerAddress);
        }

        [HttpPost]
        public async Task AddCustomerAddress([FromBody] CustomerAddress customerAddress)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await customerAddressRepository.AddCustomerAddress(customerAddress);
                //return Created(customerAddress);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerAddress>> UpdateCustomerAddress([FromODataUri] int id, [FromBody] CustomerAddress customerAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerAddressToUpdate = await customerAddressRepository.GetCustomerAddressById(id);
            if (customerAddressToUpdate == null)
            {
                return NotFound();
            }
            customerAddress.AddressId = customerAddressToUpdate.AddressId;
            await customerAddressRepository.UpdateCustomerAddress(customerAddress);
            return Updated(customerAddress);
        }
    }
}
