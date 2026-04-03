using bruno_backend.DTOs;

namespace bruno_backend.Services;

public interface IHomologatorService
{
    Task<ChubbCatalogResponseDto> GetMakesSubmakesAsync();
    Task<ChubbCatalogResponseDto> GetVehicleTypesAsync(int makeId, int subMakeId, int model);
    Task<ChubbCatalogResponseDto> GetVehicleDescriptionsAsync(int vehicleTypeId, int model);
    Task<HomologationResponseDto> SearchHomologationAsync(HomologationSearchRequestDto request);
}
