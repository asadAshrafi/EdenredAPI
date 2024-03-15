using Edenred.Model;

namespace Edenred.DataAccess.Repositories
{
    public interface IBeneficiaryRepository : IBaseRepository<Beneficiary>
    {
        Task<List<BeneficiaryList>> GetAllBeneficiaryByUserIdAsync(int userId);
        Task<bool> IsExistsAsync(int beneficiaryId, int userId);
        Task<int> UserBeneficiaryCountAsync(int userId);
        Task UpdateBeneficiary(Beneficiary beneficiary);
        Task<bool> IsBeneficiaryNameExists(int userId, string name);
    }
}
