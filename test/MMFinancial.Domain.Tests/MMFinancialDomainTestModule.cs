using MMFinancial.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MMFinancial;

[DependsOn(
    typeof(MMFinancialEntityFrameworkCoreTestModule)
    )]
public class MMFinancialDomainTestModule : AbpModule
{

}
