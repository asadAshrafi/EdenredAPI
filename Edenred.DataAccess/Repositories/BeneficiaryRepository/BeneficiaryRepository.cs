using Edenred.DataAccess.Context;
using Edenred.Model;
using Microsoft.EntityFrameworkCore;

namespace Edenred.DataAccess.Repositories
{
    public class BeneficiaryRepository : BaseRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly EdenredDbContext _context;
        public BeneficiaryRepository(EdenredDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<BeneficiaryList>> GetAllBeneficiaryByUserIdAsync(int userId)
        {
           var beneficiaryList = await _context.Beneficiaries.Where(e=>e.UserId == userId)
                                                             .Select(e => new BeneficiaryList {
                                                                 Id = e.Id,
                                                             Name = e.NickName
                                                             }).ToListAsync();
            return beneficiaryList;
        }
        public async Task<int> UserBeneficiaryCountAsync(int userId)
        {
            var beneficiaryCount = await _context.Beneficiaries.Where(e=>e.UserId==userId).CountAsync();
            return beneficiaryCount;
        }
        public async Task<bool> IsExistsAsync(int beneficiaryId,int userId)
        {
            return await _context.Beneficiaries.AnyAsync(e => e.Id == beneficiaryId && e.UserId==userId);
        }

        public async Task UpdateBeneficiary(Beneficiary beneficiary)
        {
                var beneficiaryInDb = await _context.Beneficiaries.FirstOrDefaultAsync(e=>e.Id==beneficiary.Id);
                beneficiaryInDb.NickName = beneficiary.NickName;
               await _context.SaveChangesAsync(); 
        }
        public async Task<bool> IsBeneficiaryNameExists(int userId,string name)
        {
            bool IsExists = await _context.Beneficiaries.AnyAsync(e=>e.UserId==userId && e.NickName==name); 
            return IsExists;
        } 

    }
}
