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
    [Route("odata/AccountProfile")]
    [ApiController]
    public class AccountProfileController : ODataController
    {
        private readonly IAccountProfileRepository accountProfileRepository;
        public AccountProfileController()
        {
            accountProfileRepository = new AccountProfileRepository();
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<AccountProfile>>> GetAllAccountProfiles()
        {
            var list = await accountProfileRepository.GetAllAccountProfiles();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountProfile>> GetAccountProfileById([FromODataUri] int id)
        {
            var accountProfile = await accountProfileRepository.GetAccountProfileByAccountId(id);
            if (accountProfile == null)
            {
                return NotFound();
            }
            return Ok(accountProfile);
        }

        [HttpPost]
        public async Task AddAccountProfile([FromBody] AccountProfile accountProfile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await accountProfileRepository.AddAccountProfile(accountProfile);
                //return Created(accountProfile);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AccountProfile>> UpdateAccountProfile([FromODataUri] int id, [FromBody] AccountProfile accountProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountProfileToUpdate = await accountProfileRepository.GetAccountProfileByAccountId(id);
            if (accountProfileToUpdate == null)
            {
                return NotFound();
            }
            accountProfile.AccountId = accountProfileToUpdate.AccountId;
            await accountProfileRepository.UpdateAccountProfile(accountProfile);
            return Updated(accountProfile);
        }
    }
}
