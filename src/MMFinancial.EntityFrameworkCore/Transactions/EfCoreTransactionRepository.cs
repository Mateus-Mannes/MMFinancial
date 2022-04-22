using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MMFinancial.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MMFinancial.Transactions
{
    public class EfCoreTransactionRepository
        : EfCoreRepository<MMFinancialDbContext, Transaction, Guid>,
            ITransactionRepository
    {
        public EfCoreTransactionRepository(
            IDbContextProvider<MMFinancialDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

    }
}