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
    [Route("odata/PhoneNumber")]
    [ApiController]
    public class PhoneNumberController : ODataController
    {
        private readonly IPhoneNumberRepository phoneNumberRepository;
        public PhoneNumberController()
        {
            phoneNumberRepository = new PhoneNumberRepository();
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<PhoneNumber>>> GetAllPhoneNumbers()
        {
            var list = await phoneNumberRepository.GetAllPhoneNumbers();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneNumber>> GetPhoneNumberById([FromODataUri] int id)
        {
            var phoneNumber = await phoneNumberRepository.GetPhoneNumberById(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            return Ok(phoneNumber);
        }

        [HttpPost]
        public async Task AddPhoneNumber([FromBody] PhoneNumber phoneNumber)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await phoneNumberRepository.AddPhoneNumber(phoneNumber);
                //return Created(phoneNumber);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PhoneNumber>> UpdatePhoneNumber([FromODataUri] int id, [FromBody] PhoneNumber phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var phoneNumberToUpdate = await phoneNumberRepository.GetPhoneNumberById(id);
            if (phoneNumberToUpdate == null)
            {
                return NotFound();
            }
            phoneNumber.PhoneNumberId = phoneNumberToUpdate.PhoneNumberId;
            await phoneNumberRepository.UpdatePhoneNumber(phoneNumber);
            return Updated(phoneNumber);
        }
    }
}
