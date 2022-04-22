using System.Threading.Tasks;

namespace MMFinancial.Data;

public interface IMMFinancialDbSchemaMigrator
{
    Task MigrateAsync();
}
