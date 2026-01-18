using Application.DTO.Requests.HourlyByPower;
using Application.Services.HourlyByPower;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests.HourlyByPower;

namespace ProductionAnalisysAPI.Endpoints;

public static class HourlyByPowerEndpoints
{
    public static void MapHourlyByPowerEndpoints(this WebApplication app)
    {
        var hourlyByPowerEndpoints = app.MapGroup("/hourlyByPower");

        hourlyByPowerEndpoints.MapGet("/",
            async Task<IResult> (HourlyByPowerService service) =>
                Results.Ok(await service.GetAllThroughViewAsync()));

        hourlyByPowerEndpoints.MapGet("/{id:int}",
            async Task<IResult> (int id, HourlyByPowerService service) =>
            {
                var hourlyByPower = await service.GetByIdAsync(id);
                return hourlyByPower is null ? Results.NotFound() : Results.Ok(hourlyByPower);
            });

        hourlyByPowerEndpoints.MapPost("/",
            async Task<IResult> ([FromBody] HourlyByPowerCreateRequest request, HourlyByPowerService service) =>
            Results.Ok(await service.CreateAsync(request.ToModel()))
        ).RequireAuthorization();

        hourlyByPowerEndpoints.MapPatch("/{id:int}", async Task<IResult> (int id,
            [FromBody] HourlyByPowerUpdateRequest request,
            HourlyByPowerService service) =>
        {
            var hourlyByPower = await service.UpdateAsync(id, request);

            return hourlyByPower is null ? Results.NotFound() : Results.Ok(hourlyByPower);
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Master" });

        hourlyByPowerEndpoints.MapDelete("/{id:int}",
            async Task<IResult> (int id, HourlyByPowerService service) =>
            {
                await service.RemoveAsync(id);
                return Results.Ok();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Master" });

        hourlyByPowerEndpoints.MapGet("/excel", async Task<IResult> (HourlyByPowerService service) =>
        {
            var excelBytes = await service.GenerateExcel();
            return Results.File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "production_analysis.xlsx");
        });
        
        hourlyByPowerEndpoints.MapGet("/pdf", async Task<IResult> (HourlyByPowerService service) =>
        {
            var pdfBytes = await service.GeneratePdf();
            return Results.File(pdfBytes, "application/pdf", "production_analysis.pdf");
        });
    }
}