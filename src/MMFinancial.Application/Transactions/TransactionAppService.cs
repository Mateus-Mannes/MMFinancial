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

        public async Task<List<AgencyMovementsDto>> GetSuspectAgencies(int month, int year)
        {
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            List<AgencyMovementsDto> agenciesFrom = new List<AgencyMovementsDto>();
            agenciesFrom = queryable
                .Where(x => x._DateTime.Month == month && x._DateTime.Year == year)
                .GroupBy(x => new { x.BankFrom, x.AgencyFrom })
                .Select(x => new AgencyMovementsDto
                {
                    Bank = x.First().BankFrom,
                    Agency = x.First().AgencyFrom,
                    ValueMoved = x.Sum(y => y.Value),
                    Type = "Entry"
                }).Where(x => x.ValueMoved > 1000000000).ToList();

            List<AgencyMovementsDto> agenciesTo = new List<AgencyMovementsDto>();
            agenciesTo = queryable
                .Where(x => x._DateTime.Month == month && x._DateTime.Year == year)
                .GroupBy(x => new { x.BankTo, x.AgencyTo })
                .Select(x => new AgencyMovementsDto
                {
                    Bank = x.First().BankTo,
                    Agency = x.First().AgencyTo,
                    ValueMoved = x.Sum(y => y.Value),
                    Type = "Out"
                }).Where(x => x.ValueMoved > 1000000000).ToList();

            return agenciesFrom.Concat(agenciesTo).ToList();
        }

        public async Task<List<AccountMovimentationDto>> GetSuspectAccounts(int month, int year)
        {
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            List<AccountMovimentationDto> accountsFrom = new List<AccountMovimentationDto>();
            accountsFrom = queryable
                .Where(x => x._DateTime.Month == month && x._DateTime.Year == year)
                .GroupBy(x => new {x.BankFrom, x.AgencyFrom, x.AccountFrom})
                .Select(x => new AccountMovimentationDto { 
                    Bank = x.First().BankFrom, 
                    Agency = x.First().AgencyFrom, 
                    Account = x.First().AccountFrom, 
                    ValueMoved = x.Sum(y => y.Value),
                    Type = "Entry"
                }).Where(x => x.ValueMoved > 1000000).ToList();

            List<AccountMovimentationDto> accountsTo = new List<AccountMovimentationDto>();
            accountsTo = queryable
                .Where(x => x._DateTime.Month == month && x._DateTime.Year == year)
                .GroupBy(x => new { x.BankTo, x.AgencyTo, x.AccounTo })
                .Select(x => new AccountMovimentationDto
                {
                    Bank = x.First().BankTo,
                    Agency = x.First().AgencyTo,
                    Account = x.First().AccounTo,
                    ValueMoved = x.Sum(y => y.Value),
                    Type = "Out"
                }).Where(x => x.ValueMoved > 1000000).ToList();
            
            return accountsFrom.Concat(accountsTo).ToList();
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
