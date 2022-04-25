using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MMFinancial.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMFinancial.Web.Pages.Transactions
{
    
    public class SuspectModel : PageModel
    {
        private readonly ITransactionAppService _transactionAppService;
        private readonly ITransactionRepository _transactionRepository;
        public List<TransactionDto> Transactions;
        public List<AccountMovimentationDto> Accounts;
        public List<AgencyMovementsDto> Agencies;
        [BindProperty]
        [Display(Name = "Month")]
        [Required]
        public DateTime _Date { get; set; } = DateTime.Now;
        public bool NoTransaction;
        public SuspectModel(ITransactionAppService transactionAppService, ITransactionRepository transactionRepository)
        {
            _transactionAppService = transactionAppService;
            _transactionRepository = transactionRepository;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            IQueryable<Transaction> queryable = await _transactionRepository.GetQueryableAsync();
            if(!queryable.Any(x => x._DateTime.Month == _Date.Month && x._DateTime.Year == _Date.Year))
            {
                NoTransaction = true;
                return Page();
            }
            Transactions = await _transactionAppService.GetSuspectTransactions(_Date.Month, _Date.Year);
            Accounts = await _transactionAppService.GetSuspectAccounts(_Date.Month, _Date.Year);
            Agencies = await _transactionAppService.GetSuspectAgencies(_Date.Month, _Date.Year);
            return Page();
        }
    }

   
}
