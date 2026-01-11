using Application.Repositories.Interfaces;
using Application.Services.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests.Catalog;

namespace ProductionAnalisysAPI.Endpoints;

public static class CatalogEndpoints
{
    public static void MapCatalogEndpoints(this WebApplication app)
    {
        var catalogEndpoints = app.MapGroup("/catalog");

        catalogEndpoints.MapGet("/",
            async Task<IResult> (CatalogService catalogService) =>
                Results.Ok(await catalogService.GetAllAsync())
        );

        catalogEndpoints.MapGet("/{id:int}",
            async (int id, CatalogService catalogService) =>
            {
                var catalog = await catalogService.GetWithValuesAsync(id);
                return catalog is null ? Results.NotFound() : Results.Ok(catalog);
            }
        );

        catalogEndpoints.MapPost("/",
            async Task<IResult> ([FromBody] CatalogCreateRequest catalogCreateRequest,
                CatalogService catalogService) =>
            {
                var catalog =
                    await catalogService.CreateAsync(catalogCreateRequest.Title, catalogCreateRequest.Values);
                return Results.Ok(catalog);
            }
        ).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

        catalogEndpoints.MapPatch("/{id:int}",
            async Task<IResult> (int id, [FromBody] CatalogUpdateRequest catalogUpdateRequest,
                CatalogService catalogService) =>
            {
                var catalog = await catalogService.UpdateAsync(id, catalogUpdateRequest.Title);
                return catalog is null ? Results.NotFound() : Results.Ok(catalog);
            }
        ).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

        catalogEndpoints.MapDelete("/{id:int}", async Task<IResult> (int id, CatalogService catalogService) =>
            {
                await catalogService.RemoveAsync(id);
                return Results.Ok();
            }
        ).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
    }
}