using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MMFinancial.Data;

/* This is used if database provider does't define
 * IMMFinancialDbSchemaMigrator implementation.
 */
public class NullMMFinancialDbSchemaMigrator : IMMFinancialDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
