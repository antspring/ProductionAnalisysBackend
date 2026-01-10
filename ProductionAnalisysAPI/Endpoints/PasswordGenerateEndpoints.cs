using ProductionAnalisysAPI.Identity;
using Services.Services.Interfaces;

namespace ProductionAnalisysAPI.Endpoints;

public static class PasswordGenerateEndpoints
{
    public static void MapPasswordGenerateEndpoints(this WebApplication app)
    {
        app.MapGet("/generatePassword", (IPasswordGenerateService passwordGenerator, CustomUserManager userManager) =>
        {
            var password = passwordGenerator.Generate(userManager.Options.Password.RequiredLength);
            
            return Results.Ok(new
            {
                Password = password,
            });
        });
    }
}