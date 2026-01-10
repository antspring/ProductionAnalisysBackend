using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductionAnalisysAPI.Identity;
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

    public static void AddIdentity(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddAuthentication(IdentityConstants.BearerScheme)
            .AddBearerToken(IdentityConstants.BearerScheme,
                options =>
                {
                    options.BearerTokenExpiration =
                        TimeSpan.FromMinutes(configuration.GetValue<int>("BearerTokenExpirationInMinutes"));
                    options.RefreshTokenExpiration =
                        TimeSpan.FromDays(configuration.GetValue<int>("RefreshTokenExpirationInDays"));
                });


        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters =
                    "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя ";
            })
            .AddRoles<IdentityRole>()
            .AddUserManager<CustomUserManager>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }

    public static void AddDependencyInjectionServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordGenerateService, PasswordGenerateService>();

        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<CustomSignInManager>();
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });
    }
}