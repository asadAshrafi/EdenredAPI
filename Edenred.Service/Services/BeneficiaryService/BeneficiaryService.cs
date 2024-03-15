using Azure.Core;
using Edenred.DataAccess.Repositories;
using Edenred.Model;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace Edenred.Service.Services
{
    public class BeneficiaryService : BaseService, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IUserRepository _userRepository;
        private readonly CallAPIService _callAPIService;
        private const int _userBeneficiaryLimit = 5;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IUserRepository userRepository, CallAPIService callAPIService)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _userRepository = userRepository;
            _callAPIService = callAPIService;
        }

        public async Task<Response> AddBeneficiaryAsync(BeneficiaryDTO beneficiaryDTO)
        {
            try
            {
                Beneficiary beneficiary =new Beneficiary()
                {
                    Id = beneficiaryDTO.Id,
                    NickName = beneficiaryDTO.Nickname,
                    UserId = beneficiaryDTO.UserId,
                };
                bool isUserExists = await _userRepository.IsExistsAsync(beneficiary.UserId);
                if (isUserExists)
                {
                    if (beneficiary.Id == 0)
                    {
                        int userBeneficiaryCount = await _beneficiaryRepository.UserBeneficiaryCountAsync(beneficiary.UserId);

                        if (userBeneficiaryCount < _userBeneficiaryLimit)
                        {
                            bool beneficiaryNameExists = await _beneficiaryRepository.IsBeneficiaryNameExists(beneficiary.UserId, beneficiary.NickName);
                            if (!beneficiaryNameExists)
                            {
                                var response = await _beneficiaryRepository.AddAsync(beneficiary);
                                return SuccessResponse();
                            }
                            else
                            {
                               return FaultResponse(resultText: "Beneficiary already exists with the given Name.");
                            }
                            
                        }
                        else
                        {
                            return FaultResponse(resultText: "You have reached to the max Limit of beneficiaries i.e. 5.");
                        }
                    }
                    else
                    {
                        bool isBeneficiaryExists = await _beneficiaryRepository.IsExistsAsync(beneficiary.Id, beneficiary.UserId);
                        if (isBeneficiaryExists)
                        {
                            bool beneficiaryNameExists = await _beneficiaryRepository.IsBeneficiaryNameExists(beneficiary.UserId, beneficiary.NickName);
                            if (!beneficiaryNameExists)
                            {
                               await _beneficiaryRepository.UpdateBeneficiary(beneficiary);
                                return SuccessResponse();
                            }
                            else
                            {
                                return FaultResponse(resultText: "Beneficiary already exists with the given Name.");
                            }

                        }
                        else
                        {
                            return FaultResponse(resultText: "beneficiary not found against the given beneficiary Id");
                        }
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
        public async Task<Response> PayAgainstBeneficiary(DebitModel debitModel)
        {
            try
            {
                bool isBeneficiaryExists = await _beneficiaryRepository.IsExistsAsync(debitModel.BeneficiaryId, debitModel.UserId);
                if (isBeneficiaryExists)
                {
                    string jsonString = JsonConvert.SerializeObject(debitModel);
                    string url = "https://localhost:7010/api/Transaction/Debit/";
                    string stringResponse = await _callAPIService.PostDataToApi(url, jsonString);
                    Response response = JsonConvert.DeserializeObject<Response>(stringResponse);

                    return response;
                }
                else
                {
                    return FaultResponse(resultText: "Beneficiary not found!");
                }
            }
            catch (Exception ex)
            {
                return FaultResponse(resultText: "Something went wrong!");
                throw;
            }

         
        }
        public async Task<List<BeneficiaryList>> GetAllBeneficiaryByUserIdAsync(int userId)
        {
            var response = await _beneficiaryRepository.GetAllBeneficiaryByUserIdAsync(userId);
            return response;
        }

    }
}
