using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MMFinancial.Transactions;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace MMFinancial.Transactions
{
    public interface ITransactionAppService : IApplicationService
    {
        Task<TransactionDto> CreateTransactionAsync(CreateTransactionDto input);
        Task<bool> hasDate(DateTime date);

        Task<IEnumerable<UploadDto>> GetUploadsHistoryAsync();
        Task<List<TransactionDto>> GetByDateAsync(string date);
        Task<List<TransactionDto>> GetSuspectTransactions(int month, int year);
        Task<List<AccountMovimentationDto>> GetSuspectAccounts(int month, int year);
        Task<List<AgencyMovementsDto>> GetSuspectAgencies(int month, int year);
    }
}
