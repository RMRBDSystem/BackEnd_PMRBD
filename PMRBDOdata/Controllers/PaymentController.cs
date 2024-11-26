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
        private readonly IConfiguration _configuration;
        private readonly string domain;

        public PaymentController(PayOS payOS, IConfiguration configuration)
        {
            _payOS = payOS;
            _accountRepository = new AccountRepository();
            _coinTransactionRepository = new CoinTransactionRepository();
            _configuration = configuration;
            domain = _configuration["FrontEnd:Domain"];
        }

        [HttpGet("{AccountID}/{Price}")]
        public async Task<IActionResult> Payment([FromODataUri] int AccountID, [FromODataUri] decimal Price)
        {
            try
            {
                var account = await _accountRepository.GetAccountById(AccountID);
                ItemData item = new ItemData("Coin", 1, (int)Price);
                List<ItemData> items = new List<ItemData>();

                var paymentLinkRequest = new PaymentData(
                orderCode: int.Parse(DateTimeOffset.Now.ToString("ffffff")),
                amount: (int)Price,
                description: "Nap Coin: " + account.UserName,
                items: items,
                returnUrl: domain + "/payment-success",
                cancelUrl: domain + "/Payment-Failed"
                );
                var response = await _payOS.createPaymentLink(paymentLinkRequest);
                return Ok(response.checkoutUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{AccountID}/{Coin}/{Price}")]
        public async Task<IActionResult> PaymentSuccess([FromODataUri] int AccountID, [FromODataUri] decimal Coin, [FromODataUri] decimal Price)
        {
            try
            {
                var account = await _accountRepository.GetAccountById(AccountID);
                account.Coin += Coin;

                var coinTransaction = new CoinTransaction()
                {
                    CustomerId = account.AccountId,
                    CoinFluctuations = Coin,
                    MoneyFluctuations = Price,
                    Date = DateTime.Now,
                    Detail = "Nạp Coin",
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

