using Application.DTO.Requests.HourlyByTactTime;
using Application.DTO.Responses.HourlyByTactTime;
using Application.UnitOfWork;
using ClosedXML.Excel;
using Domain.Models.ProductionDownTime;

namespace Application.Services.HourlyByTactTime;

public class HourlyByTactTimeService
{
    private IUnitOfWork _uow { get; set; }

    public HourlyByTactTimeService(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<IEnumerable<HourlyByTactTimeResponse>> GetAllThroughViewAsync()
    {
        var hourlyByTactTimeViews = await _uow.HourlyByTactTime.GetAllThroughViewAsync();

        var hourlyByTactTimeResponses = hourlyByTactTimeViews
            .Select(v => new HourlyByTactTimeResponse(v));
        return hourlyByTactTimeResponses;
    }

    public async Task<HourlyByTactTimeResponse?> GetByIdAsync(int id)
    {
        var hourlyByTactTime = await _uow.HourlyByTactTime.GetByIdThroughViewAsync(id);

        if (hourlyByTactTime is null)
            return null;

        return new HourlyByTactTimeResponse(hourlyByTactTime);
    }

    public async Task<HourlyByTactTimeResponse> CreateAsync(
        Domain.Models.ProductionAnalysis.HourlyByTactTime.HourlyByTactTime hourlyByTactTime)
    {
        var productionDocument = new ProductionDocument
        {
            DocumentType = ProductionDocumentType.HourlyByTactTime
        };

        hourlyByTactTime.AddProductionDocument(productionDocument);
        hourlyByTactTime = await _uow.HourlyByTactTime.CreateAsync(hourlyByTactTime);
        await _uow.SaveChangesAsync();

        var hourlyByTactTimeView = await _uow.HourlyByTactTime.GetByIdThroughViewAsync(hourlyByTactTime.Id);

        return new HourlyByTactTimeResponse(hourlyByTactTimeView);
    }

    public async Task<HourlyByTactTimeResponse?> UpdateAsync(int id, HourlyByTactTimeUpdateRequest request)
    {
        var hourlyByTactTime = await _uow.HourlyByTactTime.GetByIdAsync(id);

        if (hourlyByTactTime is null)
            return null;

        _uow.HourlyByTactTime.Update(hourlyByTactTime, request);
        await _uow.SaveChangesAsync();

        return new
            HourlyByTactTimeResponse(await _uow.HourlyByTactTime.GetByIdThroughViewAsync(id));
    }

    public Task RemoveAsync(int id)
    {
        return _uow.HourlyByTactTime.RemoveAsync(id);
    }

    public async Task<byte[]> GenerateExcel()
    {
        var analysis = await _uow.HourlyByTactTime.GetAllThroughViewAsync();

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Анализ");

        ws.Cell(1, 1).Value = "Наименование продукции";
        ws.Cell(1, 2).Value = "Подразделение";
        ws.Cell(1, 3).Value = "ФИО заполняющего";
        ws.Cell(1, 4).Value = "Дата";
        ws.Cell(1, 5).Value = "Смена";
        ws.Cell(1, 6).Value = "Время такта, сек";
        ws.Cell(1, 7).Value = "Суточный темп, шт";
        ws.Cell(1, 8).Value = "Время работы, час";
        ws.Cell(1, 9).Value = "План, шт";
        ws.Cell(1, 10).Value = "План накопительный, шт";
        ws.Cell(1, 11).Value = "Факт, шт";
        ws.Cell(1, 12).Value = "Факт накопительный, шт";
        ws.Cell(1, 13).Value = "Отклонен, шт";
        ws.Cell(1, 14).Value = "Отклонение накопительный, шт";
        ws.Cell(1, 15).Value = "Итого факт, шт";
        ws.Cell(1, 16).Value = "Итого план, шт";

        var row = 2;

        foreach (var item in analysis)
        {
            ws.Cell(row, 1).Value = item.NameOfProduct.Value;
            ws.Cell(row, 2).Value = item.Department.Value;
            ws.Cell(row, 3).Value = item.Performer.Value;
            ws.Cell(row, 4).Value = item.Date.ToString("yyyy-MM-dd");
            ws.Cell(row, 5).Value = item.Shift.Value;
            ws.Cell(row, 6).Value = item.TactTime;
            ws.Cell(row, 7).Value = item.DailyRate;
            ws.Cell(row, 8).Value = item.WorkHour.Value;
            ws.Cell(row, 9).Value = item.Plan;
            ws.Cell(row, 10).Value = item.PlanCumulative;
            ws.Cell(row, 11).Value = item.Fact;
            ws.Cell(row, 12).Value = item.FactCumulative;
            ws.Cell(row, 13).Value = item.Deviation;
            ws.Cell(row, 14).Value = item.DeviationCumulative;
            ws.Cell(row, 15).Value = item.TotalFact;
            ws.Cell(row, 16).Value = item.TotalPlan;

            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }
}