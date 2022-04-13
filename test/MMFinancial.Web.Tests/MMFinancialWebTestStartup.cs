using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace MMFinancial;

public class MMFinancialWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<MMFinancialWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
