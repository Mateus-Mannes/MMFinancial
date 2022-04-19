using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.Account.Web;
using Volo.Abp.Account.Web.Pages.Account;

public class MyLoginModel : LoginModel
{
    public MyLoginModel(
        IAuthenticationSchemeProvider schemeProvider,
        IOptions<AbpAccountOptions> accountOptions,
        IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> identityOptions
        ) : base(schemeProvider, accountOptions, identityOptions)
    {

    }

    public override Task<IActionResult> OnPostAsync(string action)
    {
        //TODO: Add logic
        return base.OnPostAsync(action);
    }

    //TODO: Add new methods and properties...
}
