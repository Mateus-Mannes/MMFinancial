using Volo.Abp.Modularity;

namespace MMFinancial;

[DependsOn(
    typeof(MMFinancialApplicationModule),
    typeof(MMFinancialDomainTestModule)
    )]
public class MMFinancialApplicationTestModule : AbpModule
{

}
