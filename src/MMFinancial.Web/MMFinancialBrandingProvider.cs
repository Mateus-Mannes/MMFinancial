using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace MMFinancial.Web;

[Dependency(ReplaceServices = true)]
public class MMFinancialBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MMFinancial";
}
