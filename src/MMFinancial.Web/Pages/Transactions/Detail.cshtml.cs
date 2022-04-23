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
using System.Collections.Generic;

namespace MMFinancial.Web.Pages.Transactions
{
    public class DetailModel : PageModel
    {
        public string _Date;
        public List<TransactionDto> Transactions;
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
            Transactions = await _transactionAppService.GetByDateAsync(date);
            if(Transactions.Count > 0)
            {
                using (_dataFilter.Disable<ISoftDelete>())
                {
                    var queryable = await _userService.GetQueryableAsync();
                    _User = queryable.Where(x => x.Id == Transactions.First().CreatorId).First();
                }
                    
            }
            return Page();
        }
    }
}
