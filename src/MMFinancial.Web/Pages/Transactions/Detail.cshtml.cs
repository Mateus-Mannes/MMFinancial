using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MMFinancial.Transactions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace MMFinancial.Web.Pages.Transactions
{
    public class DetailModel : PageModel
    {
        public string _Date;
        public TransactionDto Transaction;
        public IdentityUserDto User;
        private readonly ITransactionAppService _transactionAppService;
        private readonly IIdentityUserAppService _userService;
        public DetailModel(ITransactionAppService transactionAppService, IIdentityUserAppService userService)
        {
            _transactionAppService = transactionAppService;
            _userService = userService;
        }
        public async Task<IActionResult> OnGetAsync(string date)
        {
            Transaction = await _transactionAppService.GetByDateAsync(date);
            if(Transaction != null)
            {
                User = await _userService.GetAsync(Transaction.CreatorId);
            }
            return Page();
        }
    }
}
