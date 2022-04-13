using System;
using System.Collections.Generic;
using System.Text;
using MMFinancial.Localization;
using Volo.Abp.Application.Services;

namespace MMFinancial;

/* Inherit your application services from this class.
 */
public abstract class MMFinancialAppService : ApplicationService
{
    protected MMFinancialAppService()
    {
        LocalizationResource = typeof(MMFinancialResource);
    }
}
