using Application.DTO.Requests.HourlyByTactTimeUpdateRequest;
using Application.DTO.Responses.HourlyByTactTime;
using Application.UnitOfWork;
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
}