using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MMFinancial.Transactions
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionAppService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto input)
        {
            Transaction newTransaction = new Transaction
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
