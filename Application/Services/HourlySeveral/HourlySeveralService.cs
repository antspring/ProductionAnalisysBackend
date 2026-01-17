using Application.DTO.Requests.HourlySeveral;
using Application.DTO.Responses.HourlySeveral;
using Application.UnitOfWork;
using ClosedXML.Excel;
using Domain.Models.ProductionDownTime;

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
        ws.Cell(1, 5).Value = "Смена";
        ws.Cell(1, 6).Value = "Тц изд. 1, сек";
        ws.Cell(1, 7).Value = "Тц изд. 2, сек";
        ws.Cell(1, 8).Value = "Суточный темп изд. 1, шт";
        ws.Cell(1, 9).Value = "Суточный темп изд. 1, шт";
        ws.Cell(1, 10).Value = "Время работы, час";
        ws.Cell(1, 11).Value = "План, шт.";
        ws.Cell(1, 12).Value = "План накопительный, шт.";
        ws.Cell(1, 13).Value = "Факт, шт.";
        ws.Cell(1, 14).Value = "Факт накопительный, шт.";
        ws.Cell(1, 15).Value = "Отклонен";
        ws.Cell(1, 16).Value = "Отклонение накопительный, шт.";
        ws.Cell(1, 17).Value = "Итого факт, шт.";
        ws.Cell(1, 18).Value = "Итого план, шт.";
        ws.Cell(1, 19).Value = "Переналадка, мин";

        var row = 2;

        foreach (var item in analysis)
        {
            ws.Cell(row, 1).Value = item.Product1.Value;
            ws.Cell(row, 2).Value = item.Product2.Value;
            ws.Cell(row, 3).Value = item.Department.Value;
            ws.Cell(row, 4).Value = item.Performer.Value;
            ws.Cell(row, 5).Value = item.Shift.Value;
            ws.Cell(row, 6).Value = item.CycleTime1;
            ws.Cell(row, 7).Value = item.CycleTime2;
            ws.Cell(row, 8).Value = item.DailyRate1;
            ws.Cell(row, 9).Value = item.DailyRate2;
            ws.Cell(row, 10).Value = item.WorkHour.Value;
            ws.Cell(row, 11).Value = item.Plan;
            ws.Cell(row, 12).Value = item.PlanCumulative;
            ws.Cell(row, 13).Value = item.Fact;
            ws.Cell(row, 14).Value = item.FactCumulative;
            ws.Cell(row, 15).Value = item.Deviation;
            ws.Cell(row, 16).Value = item.DeviationCumulative;
            ws.Cell(row, 17).Value = item.TotalFact;
            ws.Cell(row, 18).Value = item.TotalPlan;
            ws.Cell(row, 19).Value = item.Changeover;

            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }
}