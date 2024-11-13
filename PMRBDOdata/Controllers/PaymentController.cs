using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Identity.Client;
using Net.payOS;
using Net.payOS.Types;
using Repository.IRepository;
using Repository.Repository;

namespace PMRBDOdata.Controllers
{
    [Route("odata/Payment")]
    [ApiController]
    public class PaymentController : ODataController
    {
        private readonly PayOS _payOS;
        private readonly IAccountRepository _accountRepository;
        private readonly ICoinTransactionRepository _coinTransactionRepository;

        public PaymentController(PayOS payOS)
        {
            _payOS = payOS;
            _accountRepository = new AccountRepository();
            _coinTransactionRepository = new CoinTransactionRepository();
        }

        [HttpGet("{AccountID}/{Coin}")]
        public async Task<IActionResult> Payment([FromODataUri] int AccountID, [FromODataUri] decimal Coin)
        {
            try
            {
                var account = await _accountRepository.GetAccountById(AccountID);
                ItemData item = new ItemData("Coin", 1, (int)Coin);
                List<ItemData> items = new List<ItemData>();

                var paymentLinkRequest = new PaymentData(
                orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                amount: (int)Coin,
                description: "Nap Coin: " + account.UserName,
                items: items,
                returnUrl: "http://localhost:5173/Payment-Success",
                cancelUrl: "http://localhost:5173/Payment-Failed"
                );
                var response = await _payOS.createPaymentLink(paymentLinkRequest);

                //Response.Headers.Add("Access-Control-Allow-Origin", "*");
                //Response.Headers.Append("Location", response.checkoutUrl);
                return Ok(response.checkoutUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{AccountID}/{Coin}")]
        public async Task<IActionResult> PaymentSuccess([FromODataUri] int AccountID, [FromODataUri] decimal Coin)
        {
            try
            {
                var account = await _accountRepository.GetAccountById(AccountID);
                account.Coin += Coin;

                var coinTransaction = new CoinTransaction()
                {
                    CustomerId = account.AccountId,
                    CoinFluctuations = Coin,
                    MoneyFluctuations = Coin,
                    Date = DateTime.Now,
                    Status = 1
                };           
                await _accountRepository.UpdateAccount(account);
                await _coinTransactionRepository.AddCoinTransaction(coinTransaction);               
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

