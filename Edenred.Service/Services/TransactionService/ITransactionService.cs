using Edenred.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.Service.Services
{
    public interface ITransactionService
    {
        public Task<Response> DebitAsync(DebitModel debitModel);
        public Task<Response> CreditAsync(CreditModel creditModel);
    }
}
