using Application.DTO.Requests.HourlyByPower;
using Application.DTO.Responses.HourlyByPower;
using Application.UnitOfWork;
using Domain.Models.ProductionDownTime;

namespace Application.Services.HourlyByPower;

public class HourlyByPowerService
{
    private IUnitOfWork _uow { get; set; }

    public HourlyByPowerService(IUnitOfWork unitOfWork)
    {
        _uow = unitOfWork;
    }

    public async Task<IEnumerable<HourlyByPowerResponse>> GetAllThroughViewAsync()
    {
        var hourlyByPowerViews = await _uow.HourlyByPower.GetAllThroughViewAsync();

        var hourlyByPowerResponses = hourlyByPowerViews
            .Select(v => new HourlyByPowerResponse(v));
        return hourlyByPowerResponses;
    }

    public async Task<HourlyByPowerResponse?> GetByIdAsync(int id)
    {
        var hourlyByPower = await _uow.HourlyByPower.GetByIdThroughViewAsync(id);

        if (hourlyByPower is null)
            return null;

        return new HourlyByPowerResponse(hourlyByPower);
    }

    public async Task<HourlyByPowerResponse> CreateAsync(
        Domain.Models.ProductionAnalysis.HourlyByPower.HourlyByPower hourlyByPower)
    {
        var productionDocument = new ProductionDocument
        {
            DocumentType = ProductionDocumentType.HourlyByPower
        };

        hourlyByPower.AddProductionDocument(productionDocument);
        hourlyByPower = await _uow.HourlyByPower.CreateAsync(hourlyByPower);
        await _uow.SaveChangesAsync();

        var hourlyByPowerView = await _uow.HourlyByPower.GetByIdThroughViewAsync(hourlyByPower.Id);

        return new HourlyByPowerResponse(hourlyByPowerView);
    }

    public async Task<HourlyByPowerResponse?> UpdateAsync(int id, HourlyByPowerUpdateRequest request)
    {
        var hourlyByPower = await _uow.HourlyByPower.GetByIdAsync(id);

        if (hourlyByPower is null)
            return null;

        _uow.HourlyByPower.Update(hourlyByPower, request);
        await _uow.SaveChangesAsync();

        return new
            HourlyByPowerResponse(await _uow.HourlyByPower.GetByIdThroughViewAsync(id));
    }

    public Task RemoveAsync(int id)
    {
        return _uow.HourlyByPower.RemoveAsync(id);
    }
}