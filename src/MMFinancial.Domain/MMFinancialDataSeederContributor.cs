using MMFinancial.Transactions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace MMFinancial
{
    public class MMFinancialDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Upload, Guid> _uploadRepository;
        private readonly IRepository<Transaction, Guid> _transactionRepository;
        private readonly IRepository<IdentityUser, Guid> _userRepository;
        public MMFinancialDataSeederContributor(
            IRepository<Upload, Guid> uploadRepository,
            IRepository<Transaction, Guid> transactionRepository,
            IRepository<IdentityUser, Guid> userRepository)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _uploadRepository = uploadRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _uploadRepository.GetCountAsync() > 0)
            {
                return;
            }

            var admin = await _userRepository.GetAsync(x => x.Email == "admin@abp.io");

            var uploadJan2020 = await _uploadRepository.InsertAsync(
                new Upload(new DateTime(2020, 01,01), DateTime.Now, admin.Id, "seed.csv")
            );

            var uploadFev2020 = await _uploadRepository.InsertAsync(
                new Upload(new DateTime(2020, 02, 01), DateTime.Now, admin.Id, "seed2.csv")
            );

            var uploadMar2020 = await _uploadRepository.InsertAsync(
                new Upload(new DateTime(2020, 03, 01), DateTime.Now, admin.Id, "seed3.csv")
            );

            await _transactionRepository.InsertAsync(
                new Transaction(
                    admin.Id,
                    new Guid(),
                    "BRADESCO",
                    "01",
                    "11",
                    "NUBANK",
                    "01",
                    "11",
                    1000,
                    new DateTime(2020, 01, 01),
                    uploadJan2020.Id
                    ),
                autoSave: true
            );

            await _transactionRepository.InsertAsync(
                new Transaction(
                    admin.Id,
                    new Guid(),
                    "BRADESCO",
                    "01",
                    "11",
                    "NUBANK",
                    "01",
                    "11",
                    TransactionsConsts.MaxNotSuspectTransaction + 1,
                    new DateTime(2020, 01, 01),
                    uploadJan2020.Id
                    ),
                autoSave: true
            );

            await _transactionRepository.InsertAsync(
                new Transaction(
                    admin.Id,
                    new Guid(),
                    "BRADESCO",
                    "01",
                    "11",
                    "NUBANK",
                    "01",
                    "11",
                    1000,
                    new DateTime(2020, 02, 01),
                    uploadFev2020.Id
                    ),
                autoSave: true
            );

            await _transactionRepository.InsertAsync(
                new Transaction(
                    admin.Id,
                    new Guid(),
                    "BRADESCO",
                    "01",
                    "22",
                    "NUBANK",
                    "01",
                    "22",
                    TransactionsConsts.MaxNotSuspectAccount + 1,
                    new DateTime(2020, 02, 01),
                    uploadFev2020.Id
                    ),
                autoSave: true
            );

            await _transactionRepository.InsertAsync(
                new Transaction(
                    admin.Id,
                    new Guid(),
                    "BRADESCO",
                    "01",
                    "11",
                    "NUBANK",
                    "01",
                    "11",
                    1000,
                    new DateTime(2020, 03, 01),
                    uploadMar2020.Id
                    ),
                autoSave: true
            );

            await _transactionRepository.InsertAsync(
                new Transaction(
                    admin.Id,
                    new Guid(),
                    "BRADESCO",
                    "44",
                    "11",
                    "NUBANK",
                    "44",
                    "11",
                    TransactionsConsts.MaxNotSuspectAgency + 1,
                    new DateTime(2020, 03, 01),
                    uploadMar2020.Id
                    ),
                autoSave: true
            );
        }
    }
}