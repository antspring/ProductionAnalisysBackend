using Application.DTO.Requests.LessThanPerHour;
using Application.Services.LessThanPerHour;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductionAnalisysAPI.DTO.Requests.LessThanPerHour;

namespace ProductionAnalisysAPI.Endpoints;

public static class LessThanPerHourEndpoints
{
    public static void MapLessThanPerHourEndpoints(this WebApplication app)
    {
        var lessThanPerHourEndpoints = app.MapGroup("/lessThanPerHour");

        lessThanPerHourEndpoints.MapGet("/",
            async Task<IResult> (LessThanPerHourService service) =>
                Results.Ok(await service.GetAllThroughViewAsync()));

        lessThanPerHourEndpoints.MapGet("/{id:int}",
            async Task<IResult> (int id, LessThanPerHourService service) =>
            {
                var lessThanPerHour = await service.GetByIdAsync(id);
                return lessThanPerHour is null ? Results.NotFound() : Results.Ok(lessThanPerHour);
            });

        lessThanPerHourEndpoints.MapPost("/",
            async Task<IResult> ([FromBody] LessThanPerHourCreateRequest request, LessThanPerHourService service) =>
            Results.Ok(await service.CreateAsync(request.ToModel()))
        ).RequireAuthorization();

        lessThanPerHourEndpoints.MapPatch("/{id:int}", async Task<IResult> (int id,
            [FromBody] LessThanPerHourUpdateRequest request,
            LessThanPerHourService service) =>
        {
            var lessThanPerHour = await service.UpdateAsync(id, request);

            return lessThanPerHour is null ? Results.NotFound() : Results.Ok(lessThanPerHour);
        }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Master" });

        lessThanPerHourEndpoints.MapDelete("/{id:int}",
            async Task<IResult> (int id, LessThanPerHourService service) =>
            {
                await service.RemoveAsync(id);
                return Results.Ok();
            }).RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,Master" });
        
        lessThanPerHourEndpoints.MapGet("/excel", async Task<IResult> (LessThanPerHourService service) =>
        {
            var excelBytes = await service.GenerateExcel();
            return Results.File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "production_analysis.xlsx");
        });
        
        lessThanPerHourEndpoints.MapGet("/pdf", async Task<IResult> (LessThanPerHourService service) =>
        {
            var pdfBytes = await service.GeneratePdf();
            return Results.File(pdfBytes, "application/pdf", "production_analysis.pdf");
        });
    }
}