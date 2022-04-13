using MMFinancial.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace MMFinancial.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MMFinancialEntityFrameworkCoreModule),
    typeof(MMFinancialApplicationContractsModule)
    )]
public class MMFinancialDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
