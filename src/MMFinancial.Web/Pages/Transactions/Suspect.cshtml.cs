using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MMFinancial.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MMFinancial.Web.Pages.Transactions
{
    
    public class SuspectModel : PageModel
    {
        private readonly ITransactionAppService _transactionAppService;
        public List<TransactionDto> Transactions;
        [BindProperty]
        [Display(Name = "Month")]
        [Required]
        public DateTime _Date { get; set; } = DateTime.Now;
        public SuspectModel(ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Transactions = await _transactionAppService.GetSuspectTransactions(_Date.Month, _Date.Year);
            return Page();
        }
    }

   
}
