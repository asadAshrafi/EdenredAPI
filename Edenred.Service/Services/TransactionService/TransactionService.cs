using Edenred.DataAccess.Repositories;
using Edenred.Model;

namespace Edenred.Service.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IExpenditureRepository _expenditureRepository;
        public TransactionService(IUserRepository userRepository, IExpenditureRepository expenditureRepository)
        {
            _userRepository = userRepository;
            _expenditureRepository = expenditureRepository;
        }
        public async Task<Response> CreditAsync(CreditModel creditModel)
        {
            try
            {
                bool isUserExists = await _userRepository.IsExistsAsync(creditModel.userId);
                if (isUserExists)
                {
                   await _userRepository.AddBalance(creditModel);
                    return SuccessResponse();
                }
                else
                {
                    return FaultResponse(resultText: "User not found!");
                }
                }
            catch (Exception ex)
            {
                return FaultResponse(resultText: "something went wrong!");

                throw;
            }
        }

        public async Task<Response> DebitAsync(DebitModel debitModel)
        {
            try
            {
                decimal amountToBeDeducted = debitModel.Amount + RatesConstants.TransactionCharge;
                bool isUserExists = await _userRepository.IsExistsAsync(debitModel.UserId);
                if (isUserExists)
                {
                    var userBalance = await _userRepository.AvailableBalance(debitModel.UserId);
                    if (userBalance >= amountToBeDeducted)
                    {
                        var currentMonthdebit = await _expenditureRepository.GetCurrentMonthTransactionsSumAsync(debitModel.UserId);
                        if (currentMonthdebit + debitModel.Amount <= RatesConstants.PerMonthDebitLimit)
                        {
                            var user = await _userRepository.GetByIdAsync(debitModel.UserId);
                            var beneficiaryExpenditure = await _expenditureRepository.GetCurrentMonthBeneficiaryExpenditureAsync(debitModel.UserId, debitModel.BeneficiaryId);


                            if (user.IsVerified)
                            {
                                if (beneficiaryExpenditure + debitModel.Amount + RatesConstants.TransactionCharge  <= RatesConstants.PerMonthBeneficiaryLimitVerified)
                                {

                                    await DeductBalance(debitModel.UserId, debitModel.BeneficiaryId, amountToBeDeducted); 
                                    return SuccessResponse();
                                }
                                else
                                {
                                    return FaultResponse(resultText: $"You have exceeded per month per beneficiary transaction limit by ${(beneficiaryExpenditure + amountToBeDeducted) - RatesConstants.PerMonthBeneficiaryLimitVerified} inclusive of transaction charges that is ${RatesConstants.TransactionCharge}.");
                                }
                            }
                            else
                            {
                                if (beneficiaryExpenditure + debitModel.Amount + +RatesConstants.TransactionCharge <= RatesConstants.PerMonthBeneficiaryLimitNonVerified)
                                {
                                    await DeductBalance(debitModel.UserId, debitModel.BeneficiaryId, amountToBeDeducted);
                                    return SuccessResponse();
                                }
                                else
                                {
                                    return FaultResponse(resultText: $"You have exceeded per month per beneficiary transaction limit by ${(beneficiaryExpenditure + amountToBeDeducted) - RatesConstants.PerMonthBeneficiaryLimitNonVerified} inclusive of transaction charges that is ${RatesConstants.TransactionCharge}.");
                                }
                            }

                        }
                        else
                        {
                            return FaultResponse(resultText: $"You have exceeded monthly limit by ${(currentMonthdebit + amountToBeDeducted) - RatesConstants.PerMonthDebitLimit} inclusive of transaction charges that is ${RatesConstants.TransactionCharge}.");
                        }
                    }
                    else
                    {
                        return FaultResponse(resultText: "You have insufficient balance for this transaction.");
                    }

                }
                else
                {
                    return FaultResponse(resultText: "User not found");
                }
            }
            catch (Exception)
            {
                return FaultResponse(resultText: "something went wrong!");
                throw;
            }
        }

        private async Task DeductBalance(int userId,int beneficiaryId, decimal amount)
        {
            Expenditure expenditure = new Expenditure()
            {
                Id = 0,
                Amount = amount,
                BeneficiaryId = beneficiaryId,
                UserId = userId,
                MonthYear = UtilityHelper.ConcateCurrentMonthAndYear()
            };
            await _userRepository.DeductBalance(userId, amount);
            await _expenditureRepository.AddAsync(expenditure);
        }

    }
}
