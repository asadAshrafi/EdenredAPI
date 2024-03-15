using Edenred.Model;
using Edenred.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace TransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;

        }
        [HttpPost]
        [Route("Debit")]
        public async Task<IActionResult> Debit(DebitModel debitModel)
        {

            var response = await _transactionService.DebitAsync(debitModel);
            return Ok(response);


        }
        [HttpPost]
        [Route("Credit")]
        public async Task<IActionResult> Credit(CreditModel creditModel)
        {
            var response = await _transactionService.CreditAsync(creditModel);
            return Ok(response);
        }
    }
}
