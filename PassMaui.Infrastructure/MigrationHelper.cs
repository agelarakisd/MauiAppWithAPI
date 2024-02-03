using System.Reflection;
using Migr8;

namespace PassMaui.Infrastructure;

public abstract class MigrationHelper
{
    public static void Migrate(Assembly migrationAssembly, string connectionString)
    {
        var migrations = Migr8.Migrations.FromAssembly(migrationAssembly);
        try
        {
            Database.Migrate(connectionString, migrations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}