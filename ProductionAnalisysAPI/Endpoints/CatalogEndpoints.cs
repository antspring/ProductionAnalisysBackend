using Application.Repositories.Interfaces;
using Application.Services.Catalog;
using Application.Services.CatalogValue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests.Catalog;
using ProductionAnalisysAPI.DTO.Requests.CatalogValue;

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

        catalogEndpoints.MapGet("/{catalogId:int}/value/{Id:int}",
            async Task<IResult> (int catalogId, int id, CatalogValueService catalogValueService) =>
            {
                var catalogValue = await catalogValueService.GetAsync(catalogId, id);
                return catalogValue is null ? Results.NotFound() : Results.Ok(catalogValue);
            }
        );

        catalogEndpoints.MapPost("/{catalogId:int}/value",
            async Task<IResult> (int catalogId, [FromBody] CatalogValueCreateRequest catalogValueCreateRequest,
                CatalogValueService catalogValueService) =>
            {
                var catalogValue = await catalogValueService.CreateAsync(catalogId, catalogValueCreateRequest.Value);
                return catalogValue is null ? Results.NotFound() : Results.Ok(catalogValue);
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

        catalogEndpoints.MapPatch("/{catalogId:int}/value/{id:int}",
            async Task<IResult> (int catalogId, int id,
                [FromBody] CatalogValueUpdateRequest catalogValueUpdateRequest,
                CatalogValueService catalogValueService) =>
            {
                var catalogValue = await catalogValueService.UpdateAsync(
                    new Application.DTO.Requests.CatalogValue.CatalogValueUpdateRequest
                    {
                        Id = id,
                        CatalogId = catalogId,
                        Value = catalogValueUpdateRequest.Value,
                    });

                return catalogValue is null ? Results.NotFound() : Results.Ok(catalogValue);
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });

        catalogEndpoints.MapDelete("/{catalogId:int}/value/{id:int}",
                async Task<IResult> (int catalogId, int id, CatalogValueService catalogValueService) =>
                {
                    await catalogValueService.RemoveAsync(catalogId, id);
                    return Results.Ok();
                })
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" });
    }
}