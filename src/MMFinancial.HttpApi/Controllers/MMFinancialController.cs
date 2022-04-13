using MMFinancial.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MMFinancial.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MMFinancialController : AbpControllerBase
{
    protected MMFinancialController()
    {
        LocalizationResource = typeof(MMFinancialResource);
    }
}
