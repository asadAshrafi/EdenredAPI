using Edenred.Model;
using Edenred.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdenredAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly decimal[] _AcceptableDebit;
        public BeneficiaryController(IBeneficiaryService beneficiaryService)
        {
            _beneficiaryService = beneficiaryService;
            _AcceptableDebit = new[] { 5M, 10, 20, 30, 50, 75, 100 };
        }
        [HttpPost]
        [Route("AddBeneficiary")]
        public async Task<IActionResult> AddBeneficiary(BeneficiaryDTO beneficiary)
        {
            var response = await _beneficiaryService.AddBeneficiaryAsync(beneficiary);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetBeneficiary")]
        public async Task<IActionResult> GetBeneficiary(int userId)
        {
            var response = await _beneficiaryService.GetAllBeneficiaryByUserIdAsync(userId);
            return Ok(response);
        }

        [HttpPost]
        [Route("PayAgainstBeneficiary")]
        public async Task<IActionResult> PayAgainstBeneficiary(DebitModel debitModel)
        {
            bool isAcceptable = false;
            for (int i = 0; i < _AcceptableDebit.Length; i++)
            {
                if (_AcceptableDebit[i] == debitModel.Amount)
                {
                    isAcceptable = true;
                }
            }
            if (isAcceptable)
            {
                var response = await _beneficiaryService.PayAgainstBeneficiary(debitModel);
                return Ok(response);
            }
            string acceptedValuesString = string.Join(", ", _AcceptableDebit);
            return BadRequest($"Invalid value. Accepted rates are: {acceptedValuesString}");
            
        }
    }
}
