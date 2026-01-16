using Application.Repositories.Interfaces;
using Application.Services.Catalog;
using Application.Services.CatalogValue;
using Application.Services.HourlyByPower;
using Application.Services.HourlyByTactTime;
using Application.Services.HourlySeveral;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Application.UnitOfWork;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Repositories.Implementations;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
        services.AddScoped<ICatalogRepository, CatalogRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<CatalogService>();
        services.AddScoped<ICatalogValueRepository, CatalogValueRepository>();
        services.AddScoped<CatalogValueService>();
        services.AddScoped<IHourlyByTactTimeRepository, HourlyByTactTimeRepository>();
        services.AddScoped<HourlyByTactTimeService>();
        services.AddScoped<IHourlyByPowerRepository, HourlyByPowerRepository>();
        services.AddScoped<HourlyByPowerService>();
        services.AddScoped<IHourlySeveralRepository, HourlySeveralRepository>();
        services.AddScoped<HourlySeveralService>();
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