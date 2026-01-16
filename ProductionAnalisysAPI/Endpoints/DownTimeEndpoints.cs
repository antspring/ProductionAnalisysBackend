using Application.Services.DownTime;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests.DownTime;

namespace ProductionAnalisysAPI.Endpoints;

public static class DownTimeEndpoints
{
    public static void MapDownTimeEndpoints(this WebApplication app)
    {
        var downTimeEndpoints = app.MapGroup("/downtime");

        downTimeEndpoints.MapGet("/{documentId:int}",
            async (int documentId, DownTimeService service) =>
                Results.Ok((object?)await service.GetByDocumentIdAsync(documentId)));

        downTimeEndpoints
            .MapPost("/",
                async ([FromBody] DownTimeCreateRequest request, DownTimeService service) =>
                await service.CreateAsync(request.ToModel()))
            .RequireAuthorization();
    }
}