using BusinessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    
    [Route("odata/Customer")]
    [ApiController]
    public class CustomerController : ODataController
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerController()
        {
            customerRepository = new CustomerRepository();
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var list = await customerRepository.GetAllCustomers();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById([FromODataUri] int id)
        {
            var customer = await customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task AddCustomer([FromBody] Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await customerRepository.AddCustomer(customer);
                //return Created(customer);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer([FromODataUri] int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerToUpdate = await customerRepository.GetCustomerById(id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }
            customer.CustomerId = customerToUpdate.CustomerId;
            await customerRepository.UpdateCustomer(customer);
            return Updated(customer);
        }

    }
}
