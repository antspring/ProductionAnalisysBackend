using System.Globalization;
using Application.DTO.Requests.HourlySeveral;
using Application.DTO.Responses.HourlySeveral;
using Application.UnitOfWork;
using ClosedXML.Excel;
using Domain.Models.ProductionDownTime;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services.HourlySeveral;

public class HourlySeveralService
{
    private IUnitOfWork _uow { get; set; }

    public HourlySeveralService(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<IEnumerable<HourlySeveralResponse>> GetAllThroughViewAsync()
    {
        var hourlySeveralViews = await _uow.HourlySeveral.GetAllThroughViewAsync();

        var hourlySeveralResponses = hourlySeveralViews
            .Select(v => new HourlySeveralResponse(v));
        return hourlySeveralResponses;
    }

    public async Task<HourlySeveralResponse?> GetByIdAsync(int id)
    {
        var hourlySeveral = await _uow.HourlySeveral.GetByIdThroughViewAsync(id);

        if (hourlySeveral is null)
            return null;

        return new HourlySeveralResponse(hourlySeveral);
    }

    public async Task<HourlySeveralResponse> CreateAsync(
        Domain.Models.ProductionAnalysis.HourlySeveral.HourlySeveral hourlySeveral)
    {
        var productionDocument = new ProductionDocument
        {
            DocumentType = ProductionDocumentType.HourlySeveral
        };

        hourlySeveral.AddProductionDocument(productionDocument);
        hourlySeveral = await _uow.HourlySeveral.CreateAsync(hourlySeveral);
        await _uow.SaveChangesAsync();

        var hourlySeveralView = await _uow.HourlySeveral.GetByIdThroughViewAsync(hourlySeveral.Id);

        return new HourlySeveralResponse(hourlySeveralView);
    }

    public async Task<HourlySeveralResponse?> UpdateAsync(int id, HourlySeveralUpdateRequest request)
    {
        var hourlySeveral = await _uow.HourlySeveral.GetByIdAsync(id);

        if (hourlySeveral is null)
            return null;

        _uow.HourlySeveral.Update(hourlySeveral, request);
        await _uow.SaveChangesAsync();

        return new
            HourlySeveralResponse(await _uow.HourlySeveral.GetByIdThroughViewAsync(id));
    }

    public Task RemoveAsync(int id)
    {
        return _uow.HourlySeveral.RemoveAsync(id);
    }

    public async Task<byte[]> GenerateExcel()
    {
        var analysis = await _uow.HourlySeveral.GetAllThroughViewAsync();

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Анализ");

        ws.Cell(1, 1).Value = "Наименование изд. 1";
        ws.Cell(1, 2).Value = "Наименование изд. 2";
        ws.Cell(1, 3).Value = "Подразделение";
        ws.Cell(1, 4).Value = "ФИО заполняющего";
        ws.Cell(1, 5).Value = "Дата";
        ws.Cell(1, 6).Value = "Смена";
        ws.Cell(1, 7).Value = "Тц изд. 1, сек";
        ws.Cell(1, 8).Value = "Тц изд. 2, сек";
        ws.Cell(1, 9).Value = "Суточный темп изд. 1, шт";
        ws.Cell(1, 10).Value = "Суточный темп изд. 1, шт";
        ws.Cell(1, 11).Value = "Время работы, час";
        ws.Cell(1, 12).Value = "План, шт.";
        ws.Cell(1, 13).Value = "План накопительный, шт.";
        ws.Cell(1, 14).Value = "Факт, шт.";
        ws.Cell(1, 15).Value = "Факт накопительный, шт.";
        ws.Cell(1, 16).Value = "Отклонен";
        ws.Cell(1, 17).Value = "Отклонение накопительный, шт.";
        ws.Cell(1, 18).Value = "Итого факт, шт.";
        ws.Cell(1, 19).Value = "Итого план, шт.";
        ws.Cell(1, 20).Value = "Переналадка, мин";

        var row = 2;

        foreach (var item in analysis)
        {
            ws.Cell(row, 1).Value = item.Product1.Value;
            ws.Cell(row, 2).Value = item.Product2.Value;
            ws.Cell(row, 3).Value = item.Department.Value;
            ws.Cell(row, 4).Value = item.Performer.Value;
            ws.Cell(row, 5).Value = item.Date.ToString("yyyy-MM-dd");
            ws.Cell(row, 6).Value = item.Shift.Value;
            ws.Cell(row, 7).Value = item.CycleTime1;
            ws.Cell(row, 8).Value = item.CycleTime2;
            ws.Cell(row, 9).Value = item.DailyRate1;
            ws.Cell(row, 10).Value = item.DailyRate2;
            ws.Cell(row, 11).Value = item.WorkHour.Value;
            ws.Cell(row, 12).Value = item.Plan;
            ws.Cell(row, 13).Value = item.PlanCumulative;
            ws.Cell(row, 14).Value = item.Fact;
            ws.Cell(row, 15).Value = item.FactCumulative;
            ws.Cell(row, 16).Value = item.Deviation;
            ws.Cell(row, 17).Value = item.DeviationCumulative;
            ws.Cell(row, 18).Value = item.TotalFact;
            ws.Cell(row, 19).Value = item.TotalPlan;
            ws.Cell(row, 20).Value = item.Changeover;

            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }

    public async Task<byte[]> GeneratePdf()
    {
        var analysis = await _uow.HourlySeveral.GetAllThroughViewAsync();

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
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Наименование изд. 1");
                        header.Cell().Text("Наименование изд. 2");
                        header.Cell().Text("Подразделение");
                        header.Cell().Text("ФИО заполняющего");
                        header.Cell().Text("Дата");
                        header.Cell().Text("Смена");
                        header.Cell().Text("Тц изд. 1, сек");
                        header.Cell().Text("Тц изд. 2, сек");
                        header.Cell().Text("Суточный темп изд. 1, шт");
                        header.Cell().Text("Суточный темп изд. 2, шт");
                        header.Cell().Text("Вермя работы, час");
                        header.Cell().Text("План, шт.");
                        header.Cell().Text("План накопительный, шт.");
                        header.Cell().Text("Факт, шт.");
                        header.Cell().Text("Факт накопительный, шт.");
                        header.Cell().Text("Отклонен, шт.");
                        header.Cell().Text("Отклонение, накопительный, шт.");
                        header.Cell().Text("Итого факт, шт.");
                        header.Cell().Text("Итого план, шт.");
                        header.Cell().Text("Переналадка, мин.");
                    });

                    foreach (var item in analysis)
                    {
                        table.Cell().Text(item.Product1.Value);
                        table.Cell().Text(item.Product2.Value);
                        table.Cell().Text(item.Department.Value);
                        table.Cell().Text(item.Performer.Value);
                        table.Cell().Text(item.Date.ToString("yyyy-MM-dd"));
                        table.Cell().Text(item.Shift.Value);
                        table.Cell().Text(item.CycleTime1.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.CycleTime2.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.DailyRate1.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.DailyRate2.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.WorkHour.Value);
                        table.Cell().Text(item.Plan.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.PlanCumulative.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.Fact.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.FactCumulative.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.Deviation.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.DeviationCumulative.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.TotalFact.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.TotalPlan.ToString(CultureInfo.InvariantCulture));
                        table.Cell().Text(item.Changeover.ToString(CultureInfo.InvariantCulture));
                    }
                });
            });
        }).GeneratePdf();
    }
}