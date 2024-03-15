using Edenred.Model;

namespace Edenred.Service.Services
{
    public interface IBeneficiaryService
    {
        public Task<Response> AddBeneficiaryAsync(BeneficiaryDTO beneficiaryDTO);
        Task<List<BeneficiaryList>> GetAllBeneficiaryByUserIdAsync(int userId);
        public Task<Response> PayAgainstBeneficiary(DebitModel debitModel);
    }
}
