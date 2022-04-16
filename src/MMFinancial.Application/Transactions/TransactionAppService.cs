using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Application.Services;

namespace MMFinancial.Transactions
{
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
