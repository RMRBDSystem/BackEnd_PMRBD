using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Withdraw")]
    [ApiController]
    public class WithdrawController : ODataController
    {
        private readonly IWithdrawRepository _withdrawRepository;
        public WithdrawController()
        {
            _withdrawRepository = new WithdrawRepository();
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> GetAllWithdraws()
        {
            return Ok(await _withdrawRepository.GetAllWithdraws());
        }

        [HttpGet("{withdrawId}")]
        public async Task<IActionResult> GetWithdrawByWithdrawId([FromODataUri] int withdrawId)
        {
            var withdraw = await _withdrawRepository.GetWithdrawByWithdrawId(withdrawId);
            if (withdraw == null)
            {
                return NotFound();
            }
            return Ok(withdraw);
        }

        [HttpGet("account/{accountId}")]
        public async Task<IActionResult> GetWithdrawByAccountId([FromODataUri] int accountId)
        {
            var withdraw = await _withdrawRepository.GetWithdrawByAccountId(accountId);
            if (withdraw == null)
            {
                return NotFound();
            }
            return Ok(withdraw);
        }

        [HttpPost]
        public async Task AddWithdraw([FromBody] Withdraw withdraw)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BadRequest(ModelState);
                }
                await _withdrawRepository.AddWithdraw(withdraw);
                //return Created(withdraw);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }

        [HttpPut("{withdrawId}")]
        public async Task<IActionResult> UpdateWithdraw([FromODataUri] int withdrawId, [FromBody] Withdraw withdraw)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existingWithdraw = await _withdrawRepository.GetWithdrawByWithdrawId(withdrawId);
                if (existingWithdraw == null)
                {
                    return NotFound();
                }
                withdraw.WithdrawId = existingWithdraw.WithdrawId;
                await _withdrawRepository.UpdateWithdraw(withdraw);
                return Ok(withdraw);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
