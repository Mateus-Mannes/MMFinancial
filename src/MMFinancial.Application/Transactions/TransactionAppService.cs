using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Services;
using MMFinancial.Permissions;

namespace MMFinancial.Transactions
{
    [Authorize(MMFinancialPermissions.UserPermission)]
    public class TransactionAppService : ApplicationService, ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionAppService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<bool> hasDate(DateTime date)
        {
            return await _transactionRepository.AnyAsync(x => x._DateTime.Date == date.Date);
        }

        public async Task<IEnumerable<UploadDto>> GetUploadsHistoryAsync()
        {
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            IEnumerable<UploadDto> uploads = queryable
                .Select(x => new UploadDto { TransactionDate = x._DateTime.Date, UploadDate = x.CreationTime.Date })
                .OrderBy(x => x.TransactionDate).Distinct();
            return uploads;

        }

        public async Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto input)
        {
            Transaction newTransaction = new Transaction(GuidGenerator.Create(), input.BankFrom, input.AgencyFrom, input.AccountFrom, input.BankTo, input.AgencyTo, input.AccounTo, input.Value, input._DateTime
                );
             await _transactionRepository.InsertAsync(newTransaction);
            return new TransactionDto
            {
                BankFrom = input.BankFrom,
                AgencyFrom = input.AgencyFrom,
                AccountFrom = input.AccountFrom,
                BankTo = input.BankTo,
                AgencyTo = input.AgencyTo,
                AccounTo = input.AccounTo,
                Value = input.Value,
                _DateTime = input._DateTime
            };
        }
    }
}
