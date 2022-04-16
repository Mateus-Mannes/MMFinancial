using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MMFinancial.Transactions;

namespace MMFinancial.Web.Pages.Transactions
{
    public class testeModel : PageModel
    {
        private readonly ITransactionAppService _transactionAppService;
        public testeModel(ITransactionAppService transactionAppService)
        {
            _transactionAppService = transactionAppService;
        }
        public void OnGet()
        {
        }
    }
}
