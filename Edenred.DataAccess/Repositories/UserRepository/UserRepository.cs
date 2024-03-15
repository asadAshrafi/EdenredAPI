using Edenred.DataAccess.Context;
using Edenred.Model;
using Microsoft.EntityFrameworkCore;

namespace Edenred.DataAccess.Repositories
{

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly EdenredDbContext _context;
        public UserRepository(EdenredDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<bool> IsExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(e => e.Id == userId);
        }
        public async Task<decimal> AvailableBalance(int userId)
        {
            var balance = await _context.Users.Where(e => e.Id==userId).Select(e => e.Balance).FirstOrDefaultAsync();
            return balance;
        }
        public async Task DeductBalance(int userId,decimal deductionAmount)
        {
            
            
                var userInDb = _context.Users.FirstOrDefault(p => p.Id == userId);
                userInDb.Balance -= deductionAmount;
                await _context.SaveChangesAsync();
            
        }
        public async Task UpdateUser(User user)
        {
            var userInDb = await _context.Users.FirstOrDefaultAsync(e => e.Id == user.Id);
            userInDb.Name = user.Name;
            userInDb.IsVerified = user.IsVerified;
            await _context.SaveChangesAsync();
        }
        public async Task AddBalance(CreditModel creditModel)
        {
            var userInDb = await _context.Users.FirstOrDefaultAsync(e => e.Id == creditModel.userId);
            userInDb.Balance += creditModel.Amount;
            await _context.SaveChangesAsync();
        }
    }
}
