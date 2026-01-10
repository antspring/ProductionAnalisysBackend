using Application.Repositories.Interfaces;
using Infrastructure.Identity;
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

        userEndpoints.MapPatch("/changeStatus",
            async ([FromBody] ChangeStatusRequest request, CustomUserManager userManager,
                IStatusRepository statusRepository) =>
            {
                var user = await userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return Results.NotFound("User not found");

                if (!await statusRepository.ExistAsync(status => status.Id == request.StatusId))
                {
                    return Results.NotFound("Status not found");
                }

                user.StatusId = request.StatusId;

                var result = await userManager.UpdateAsync(user);

                if (!result.Succeeded)
                    return Results.BadRequest(result.Errors);

                return Results.Ok();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

        userEndpoints.MapGet("/", async (IUserRepository userRepository) =>
            await userRepository.GetAllAsync()
        ).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
    }
}