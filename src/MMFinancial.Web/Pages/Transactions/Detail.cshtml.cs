using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MMFinancial.Transactions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using System.Linq;
using Volo.Abp.Data;
using Volo.Abp;

namespace MMFinancial.Web.Pages.Transactions
{
    public class DetailModel : PageModel
    {
        public string _Date;
        public TransactionDto Transaction;
        public IdentityUser _User;
        private readonly ITransactionAppService _transactionAppService;
        private readonly IRepository<IdentityUser> _userService;
        private readonly IDataFilter _dataFilter;
        public DetailModel(ITransactionAppService transactionAppService, IRepository<IdentityUser> userService, IDataFilter dataFilter)
        {
            _transactionAppService = transactionAppService;
            _userService = userService;
            _dataFilter = dataFilter;
        }
        public async Task<IActionResult> OnGetAsync(string date)
        {
            Transaction = await _transactionAppService.GetByDateAsync(date);
            if(Transaction != null)
            {
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    var queryable = await _userService.GetQueryableAsync();
                    _User = queryable.Where(x => x.Id == Transaction.CreatorId).First();
                }
                    
            }
            return Page();
        }
    }
}
