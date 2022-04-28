using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MMFinancial.Web.Pages.Account
{
    public class RegisteredModel : PageModel
    {
        public bool userNotRegistered;
        public void OnGet(string erro)
        {
            if(erro == "erro")
            {
                userNotRegistered = true;
            }
        }
    }
}
