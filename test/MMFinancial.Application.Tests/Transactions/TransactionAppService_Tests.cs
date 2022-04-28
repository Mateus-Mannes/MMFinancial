using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;
using MMFinancial;

namespace MMFinancial.Transactions
{
    public class TransactionAppService_Tests : MMFinancialApplicationTestBase
    {
        private readonly ITransactionAppService _transactionAppService;

        public TransactionAppService_Tests()
        {
            _transactionAppService = GetRequiredService<ITransactionAppService>();
        }

        [Fact]
        public async Task Shoul_Get_Suspect_Transaction()
        {
            //Act
            var result = await _transactionAppService.GetSuspectTransactions(01, 2020);

            //Assert
            result.Count.ShouldBe(1);
            result.ShouldContain(x => x.Value > TransactionsConsts.MaxNotSuspectTransaction);
        }

        [Fact]
        public async Task Shoul_Get_Suspect_Account()
        {
            //Act
            var result = await _transactionAppService.GetSuspectAccounts(02, 2020);

            //Assert
            result.Count.ShouldBe(2);
            result.ShouldContain(x => x.ValueMoved > TransactionsConsts.MaxNotSuspectAccount);
        }

        [Fact]
        public async Task Shoul_Get_Suspect_Agency()
        {
            //Act
            var result = await _transactionAppService.GetSuspectAgencies(03, 2020);

            //Assert
            result.Count.ShouldBe(2);
            result.ShouldContain(x => x.ValueMoved > TransactionsConsts.MaxNotSuspectAgency);
        }
    }
}