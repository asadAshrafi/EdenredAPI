using Edenred.Model;

namespace Edenred.DataAccess.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task AddBalance(CreditModel creditModel);
        Task<decimal> AvailableBalance(int userId);
        Task DeductBalance(int userId, decimal deductionAmount);
        Task<bool> IsExistsAsync(int userId);
        Task UpdateUser(User user);
    }
}
