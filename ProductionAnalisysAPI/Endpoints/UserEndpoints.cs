using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests;

namespace ProductionAnalisysAPI.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        var userEndpoints = app.MapGroup("/user");

        userEndpoints.MapGet("/info", async (CustomUserManager userManager, HttpContext httpContext) =>
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            if (user == null)
                return Results.Unauthorized();

            var roles = await userManager.GetRolesAsync(user);

            return Results.Ok(new
            {
                email = user.Email,
                roles
            });
        }).RequireAuthorization();


        userEndpoints.MapPatch("/changeRole",
            async ([FromBody] ChangeRoleRequest request,
                CustomUserManager userManager, RoleManager<IdentityRole> roleManager) =>
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return Results.Unauthorized();

                if (!await roleManager.RoleExistsAsync(request.Role))
                {
                    return Results.NotFound("Role not found");
                }

                var currentRoles = await userManager.GetRolesAsync(user);

                var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeResult.Succeeded)
                    return Results.BadRequest(removeResult.Errors);

                var addResult = await userManager.AddToRoleAsync(user, request.Role);
                if (!addResult.Succeeded)
                    return Results.BadRequest(addResult.Errors);

                return Results.Ok();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
    }
}