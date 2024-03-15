using Edenred.DataAccess.Context;
using Edenred.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.DataAccess.Repositories
{
    public class ExpenditureRepository : BaseRepository<Expenditure>, IExpenditureRepository
    {
        private readonly EdenredDbContext _context;
        public ExpenditureRepository(EdenredDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<decimal> GetCurrentMonthTransactionsSumAsync(int userId)
        {
            var sum = await _context.Expenditures.Where(e =>
            e.UserId == userId &&
            e.MonthYear == UtilityHelper.ConcateCurrentMonthAndYear()).SumAsync(e => e.Amount);
            return sum;
        }

        /// <summary>
        /// Returns current month beneficiary expenditure of the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="beneficiaryId"></param>
        /// <returns></returns>
        public async Task<decimal> GetCurrentMonthBeneficiaryExpenditureAsync(int userId, int beneficiaryId)
        {
            var sum = await _context.Expenditures.Where(e =>
            e.UserId == userId &&
            e.BeneficiaryId == beneficiaryId &&
            e.MonthYear == UtilityHelper.ConcateCurrentMonthAndYear()).SumAsync(e => e.Amount);
            return sum;
        }
    }
}