using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MMFinancial.Data;
using Volo.Abp.DependencyInjection;

namespace MMFinancial.EntityFrameworkCore;

public class EntityFrameworkCoreMMFinancialDbSchemaMigrator
    : IMMFinancialDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreMMFinancialDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the MMFinancialDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<MMFinancialDbContext>()
            .Database
            .MigrateAsync();
    }
}
