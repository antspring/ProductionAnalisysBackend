using DataAccess.Seeders;

namespace ProductionAnalisysAPI;

public class SeederManager
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await RoleSeeder.SeedAsync(scope.ServiceProvider);
    }
}