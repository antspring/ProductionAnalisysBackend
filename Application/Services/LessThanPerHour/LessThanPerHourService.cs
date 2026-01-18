using System.Globalization;
using Application.DTO.Requests.LessThanPerHour;
using Application.DTO.Responses.LessThanPerHour;
using Application.UnitOfWork;
using ClosedXML.Excel;
using Domain.Models.ProductionDownTime;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services.LessThanPerHour;

public class LessThanPerHourService
{
    private IUnitOfWork _uow { get; set; }

    public LessThanPerHourService(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<IEnumerable<LessThanPerHourResponse>> GetAllThroughViewAsync()
    {
        var lessThanPerHourViews = await _uow.LessThanPerHour.GetAllThroughViewAsync();

        var lessThanPerHourResponses = lessThanPerHourViews
            .Select(v => new LessThanPerHourResponse(v));
        return lessThanPerHourResponses;
    }

    public async Task<LessThanPerHourResponse?> GetByIdAsync(int id)
    {
        var lessThanPerHour = await _uow.LessThanPerHour.GetByIdThroughViewAsync(id);

        if (lessThanPerHour is null)
            return null;

        return new LessThanPerHourResponse(lessThanPerHour);
    }

    public async Task<LessThanPerHourResponse> CreateAsync(
        Domain.Models.ProductionAnalysis.LessThanPerHour.LessThanPerHour lessThanPerHour)
    {
        var productionDocument = new ProductionDocument
        {
            DocumentType = ProductionDocumentType.LessThanPerHour
        };

        lessThanPerHour.AddProductionDocument(productionDocument);
        lessThanPerHour = await _uow.LessThanPerHour.CreateAsync(lessThanPerHour);
        await _uow.SaveChangesAsync();

        var lessThanPerHourView = await _uow.LessThanPerHour.GetByIdThroughViewAsync(lessThanPerHour.Id);

        return new LessThanPerHourResponse(lessThanPerHourView);
    }

    public async Task<LessThanPerHourResponse?> UpdateAsync(int id, LessThanPerHourUpdateRequest request)
    {
        var lessThanPerHour = await _uow.LessThanPerHour.GetByIdAsync(id);

        if (lessThanPerHour is null)
            return null;

        _uow.LessThanPerHour.Update(lessThanPerHour, request);
        await _uow.SaveChangesAsync();

        return new
            LessThanPerHourResponse(await _uow.LessThanPerHour.GetByIdThroughViewAsync(id));
    }

    public Task RemoveAsync(int id)
    {
        return _uow.LessThanPerHour.RemoveAsync(id);
    }

    public async Task<byte[]> GenerateExcel()
    {
        var analysis = await _uow.LessThanPerHour.GetAllThroughViewAsync();

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Анализ");

        ws.Cell(1, 1).Value = "Подразделение";
        ws.Cell(1, 2).Value = "ФИО заполняющего";
        ws.Cell(1, 3).Value = "Дата";
        ws.Cell(1, 4).Value = "Смена";
        ws.Cell(1, 5).Value = "Время работы, час";
        ws.Cell(1, 6).Value = "Наименование операции";
        ws.Cell(1, 7).Value = "Время начала план";
        ws.Cell(1, 8).Value = "Время начала факт";
        ws.Cell(1, 9).Value = "Время окончания план";
        ws.Cell(1, 10).Value = "Время окончания факт";
        ws.Cell(1, 11).Value = "План, шт.";
        ws.Cell(1, 12).Value = "План накопительный, шт.";
        ws.Cell(1, 13).Value = "Факт, шт.";
        ws.Cell(1, 14).Value = "Факт накопительный, шт.";
        ws.Cell(1, 15).Value = "Отклонен";
        ws.Cell(1, 16).Value = "Отклонение накопительный, шт.";
        ws.Cell(1, 17).Value = "Статус";

        var row = 2;

        foreach (var item in analysis)
        {
            ws.Cell(row, 1).Value = item.Department.Value;
            ws.Cell(row, 2).Value = item.Performer.Value;
            ws.Cell(row, 3).Value = item.Date.ToString("yyyy-MM-dd");
            ws.Cell(row, 4).Value = item.Shift.Value;
            ws.Cell(row, 5).Value = item.WorkHour.Value;
            ws.Cell(row, 6).Value = item.OperationName.Value;
            ws.Cell(row, 7).Value = item.StartTimePlan.ToString("mm:ss");
            ws.Cell(row, 8).Value = item.StartTimeFact.ToString("mm:ss");
            ws.Cell(row, 9).Value = item.EndTimePlan.ToString("mm:ss");
            ws.Cell(row, 10).Value = item.EndTimeFact.ToString("mm:ss");
            ws.Cell(row, 11).Value = item.Plan;
            ws.Cell(row, 12).Value = item.PlanCumulative;
            ws.Cell(row, 13).Value = item.Fact;
            ws.Cell(row, 14).Value = item.FactCumulative;
            ws.Cell(row, 15).Value = item.Deviation;
            ws.Cell(row, 16).Value = item.DeviationCumulative;
            ws.Cell(row, 17).Value = item.Status;

            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }
    
    public async Task<byte[]> GeneratePdf()
    {
        var analysis = await _uow.LessThanPerHour.GetAllThroughViewAsync();
        
        QuestPDF.Settings.License = LicenseType.Community; 

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A1.Landscape());
                page.Margin(20);
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Подразделение");
                        header.Cell().Text("ФИО заполняющего");
                        header.Cell().Text("Дата");
                        header.Cell().Text("Смена");
                        header.Cell().Text("Время работы, час");
                        header.Cell().Text("Наименование операции");
                        header.Cell().Text("Время начала план");
                        header.Cell().Text("Время начала факт");
                        header.Cell().Text("Время окончания план");
                        header.Cell().Text("Время окончания факт");
                        header.Cell().Text("План, шт");
                        header.Cell().Text("План накопительный, шт");
                        header.Cell().Text("Факт, шт");
                        header.Cell().Text("Факт накопительный, шт");
                        header.Cell().Text("Отклонен, шт");
                        header.Cell().Text("Отклонение, накопительный");
                        header.Cell().Text("Статус");
                    });

                    foreach (var item in analysis)
                    {
                        table.Cell().Text(item.Department.Value);
                        table.Cell().Text(item.Performer.Value);
                        table.Cell().Text(item.Date.ToString("yyyy-MM-dd"));
                        table.Cell().Text(item.Shift.Value);
                        table.Cell().Text(item.WorkHour.Value);
                        table.Cell().Text(item.OperationName.Value);
                        table.Cell().Text(item.StartTimePlan.ToString("mm:ss"));
                        table.Cell().Text(item.StartTimeFact.ToString("mm:ss"));
                        table.Cell().Text(item.EndTimePlan.ToString("mm:ss"));
                        table.Cell().Text(item.EndTimeFact.ToString("mm:ss"));
                        table.Cell().Text(item.Plan.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.PlanCumulative.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.Fact.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.FactCumulative.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.Deviation.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.DeviationCumulative.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.Status);
                    }
                });
            });
        }).GeneratePdf();
    }
}