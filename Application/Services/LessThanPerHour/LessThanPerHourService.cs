using Application.DTO.Requests.LessThanPerHour;
using Application.DTO.Responses.LessThanPerHour;
using Application.UnitOfWork;
using Domain.Models.ProductionDownTime;

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
}