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
    [Route("odata/Account")]
    [ApiController]
    public class AccountController : ODataController
    {
        private readonly IAccountRepository accountRepository;
        public AccountController()
        {
            accountRepository = new AccountRepository();
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            var list = await accountRepository.GetAllAccounts();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById([FromODataUri] int id)
        {
            var account = await accountRepository.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public async Task AddAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await accountRepository.AddAccount(account);
                //return Created(account);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Account>> UpdateAccount([FromODataUri] int id, [FromBody] Account account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var accountToUpdate = await accountRepository.GetAccountById(id);
            if (accountToUpdate == null)
            {
                return NotFound();
            }
            account.AccountId = accountToUpdate.AccountId;
            await accountRepository.UpdateAccount(account);
            return Updated(account);
        }
    }
}
