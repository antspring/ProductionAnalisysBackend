using Application.DTO.Requests.HourlyByTactTime;
using Application.Services.HourlyByTactTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests.HourlyByTactTime;

namespace ProductionAnalisysAPI.Endpoints;

public static class HourlyByTactTimeEndpoints
{
    public static void MapHourlyByTactTimeEndpoints(this WebApplication app)
    {
        var hourlyByTactTimeEndpoints = app.MapGroup("/hourlyByTactTime");

        hourlyByTactTimeEndpoints.MapGet("/",
            async Task<IResult> (HourlyByTactTimeService service) =>
                Results.Ok(await service.GetAllThroughViewAsync()));

        hourlyByTactTimeEndpoints.MapGet("/{id:int}",
            async Task<IResult> (int id, HourlyByTactTimeService service) =>
            {
                var hourlyByTactTime = await service.GetByIdAsync(id);
                return hourlyByTactTime is null ? Results.NotFound() : Results.Ok(hourlyByTactTime);
            });

        hourlyByTactTimeEndpoints.MapPost("/",
            async Task<IResult> ([FromBody] HourlyByTactTimeCreateRequest request, HourlyByTactTimeService service) =>
            Results.Ok(await service.CreateAsync(request.ToModel()))
        ).RequireAuthorization();

        hourlyByTactTimeEndpoints.MapPatch("/{id:int}", async Task<IResult> (int id,
            [FromBody] HourlyByTactTimeUpdateRequest request,
            HourlyByTactTimeService service) =>
        {
            var hourlyByTactTime = await service.UpdateAsync(id, request);

            return hourlyByTactTime is null ? Results.NotFound() : Results.Ok(hourlyByTactTime);
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Master" });

        hourlyByTactTimeEndpoints.MapDelete("/{id:int}",
            async Task<IResult> (int id, HourlyByTactTimeService service) =>
            {
                await service.RemoveAsync(id);
                return Results.Ok();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Master" });

        hourlyByTactTimeEndpoints.MapGet("/excel", async Task<IResult> (HourlyByTactTimeService service) =>
        {
            var excelBytes = await service.GenerateExcel();
            return Results.File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "production_analysis.xlsx");
        });

        hourlyByTactTimeEndpoints.MapGet("/pdf", async Task<IResult> (HourlyByTactTimeService service) =>
        {
            var pdfBytes = await service.GeneratePdf();
            return Results.File(pdfBytes, "application/pdf", "production_analysis.pdf");
        });
    }
}