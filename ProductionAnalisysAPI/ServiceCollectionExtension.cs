using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Services.Implementations;
using Services.Services.Interfaces;

namespace ProductionAnalisysAPI;

public static class ServiceCollectionExtension
{
    public static void AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnectionString")));
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityApiEndpoints<ApplicationUser>(options => options.Password.RequiredLength = 8)
            .AddRoles<IdentityRole>()
            .AddUserManager<CustomUserManager>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }

    public static void AddDependencyInjectionServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordGenerateService, PasswordGenerateService>();
    }
}