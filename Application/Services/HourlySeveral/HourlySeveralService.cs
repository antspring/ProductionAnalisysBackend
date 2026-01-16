using Application.DTO.Requests.HourlySeveral;
using Application.DTO.Responses.HourlySeveral;
using Application.UnitOfWork;
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
}