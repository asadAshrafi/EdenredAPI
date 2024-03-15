using Edenred.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.DataAccess.Repositories
{
    public interface IExpenditureRepository : IBaseRepository<Expenditure>
    {
        Task<decimal> GetCurrentMonthBeneficiaryExpenditureAsync(int userId, int beneficiaryId);
        Task<decimal> GetCurrentMonthTransactionsSumAsync(int userId);
    }
}
