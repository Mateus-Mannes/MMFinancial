using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using MMFinancial.Transactions;

namespace MMFinancial.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
    }
}
