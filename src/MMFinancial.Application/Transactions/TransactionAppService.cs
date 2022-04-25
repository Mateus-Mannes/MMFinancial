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

        public async Task<List<AccountMovimentationDto>> GetSuspectAccounts(int month, int year)
        {
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            List<AccountMovimentationDto> accounts = new List<AccountMovimentationDto>();
            accounts = queryable
                .Where(x => x._DateTime.Month == month && x._DateTime.Year == year)
                .GroupBy(x => new {x.BankFrom, x.AgencyFrom, x.AccountFrom})
                .Select(y => new AccountMovimentationDto { 
                    Bank = y.First().BankFrom, 
                    Agency = y.First().AgencyFrom, 
                    Account = y.First().AccountFrom, 
                    ValueMoved = y.Sum(k => k.Value),
                    Type = "Entry"
                }).Where(h => h.ValueMoved > 1000000).ToList();
            return accounts;
        }


        public async Task<List<TransactionDto>> GetSuspectTransactions(int month, int year)
        {
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            List<Transaction> transactions = new List<Transaction>();
            transactions = queryable.Where(x => x.Value > 100000 && x._DateTime.Month == month && x._DateTime.Year == year).ToList();
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
        }

        public async Task<bool> hasDate(DateTime date)
        {
            return await _transactionRepository.AnyAsync(x => x._DateTime.Date == date.Date);
        }

        public async Task<List<TransactionDto>> GetByDateAsync(string date)
        {
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            DateTime _date = DateTime.Parse(date).Date;
            List<Transaction> transactions = new List<Transaction>();
            if (await hasDate(_date))
            {
                transactions = queryable.Where(x => x._DateTime.Date == _date).ToList();
            } 
            return ObjectMapper.Map<List<Transaction>, List<TransactionDto>>(transactions);
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
            return ObjectMapper.Map<Transaction, TransactionDto>(newTransaction);
        }
    }
}
