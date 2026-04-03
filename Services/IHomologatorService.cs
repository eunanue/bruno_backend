using bruno_backend.DTOs;

namespace bruno_backend.Services;

public interface IHomologatorService
{
    Task<CatalogResponseDto> GetMakesSubmakesAsync();
    Task<CatalogResponseDto> GetVehicleTypesAsync(int makeId, int subMakeId, int model);
    Task<CatalogResponseDto> GetVehicleDescriptionsAsync(int vehicleTypeId, int model);
    Task<HomologationResponseDto> SearchHomologationAsync(HomologationSearchRequestDto request);
}
