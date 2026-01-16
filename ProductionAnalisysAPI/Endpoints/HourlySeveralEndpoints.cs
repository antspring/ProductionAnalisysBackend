using Application.DTO.Requests.HourlySeveral;
using Application.Services.HourlySeveral;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests.HourlySeveral;

namespace ProductionAnalisysAPI.Endpoints;

public static class HourlySeveralEndpoints
{
    public static void MapHourlySeveralEndpoints(this WebApplication app)
    {
        var hourlySeveralEndpoints = app.MapGroup("/hourlySeveral");

        hourlySeveralEndpoints.MapGet("/",
            async Task<IResult> (HourlySeveralService service) =>
                Results.Ok(await service.GetAllThroughViewAsync()));

        hourlySeveralEndpoints.MapGet("/{id:int}",
            async Task<IResult> (int id, HourlySeveralService service) =>
            {
                var hourlySeveral = await service.GetByIdAsync(id);
                return hourlySeveral is null ? Results.NotFound() : Results.Ok(hourlySeveral);
            });

        hourlySeveralEndpoints.MapPost("/",
            async Task<IResult> ([FromBody] HourlySeveralCreateRequest request, HourlySeveralService service) =>
            Results.Ok(await service.CreateAsync(request.ToModel()))
        ).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Operator" });

        hourlySeveralEndpoints.MapPatch("/{id:int}", async Task<IResult> (int id,
            [FromBody] HourlySeveralUpdateRequest request,
            HourlySeveralService service) =>
        {
            var hourlySeveral = await service.UpdateAsync(id, request);

            return hourlySeveral is null ? Results.NotFound() : Results.Ok(hourlySeveral);
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Operator" });

        hourlySeveralEndpoints.MapDelete("/{id:int}",
            async Task<IResult> (int id, HourlySeveralService service) =>
            {
                await service.RemoveAsync(id);
                return Results.Ok();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Operator" });
    }
}