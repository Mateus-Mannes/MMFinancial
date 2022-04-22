using MMFinancial.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MMFinancial.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class MMFinancialPageModel : AbpPageModel
{
    protected MMFinancialPageModel()
    {
        LocalizationResourceType = typeof(MMFinancialResource);


    }
}
