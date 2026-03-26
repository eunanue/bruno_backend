using bruno_backend.DTOs;

namespace bruno_backend.Services;

public interface IUtilsService
{
    Task<CoveragesResponseDto> GetCoveragesAsync(string packageId, string insurerId, string subLineBusinessId);
    Task<PackagesResponseDto> GetPackagesAsync(string subLineBusinessId);
    Task<SettlementsResponseDto> GetSettlementsByZipCodeAsync(string zipCode);
    Task<VinValidationResponseDto> ValidateVinAsync(string vin, string? token);
    Task<RfcGeneratorResponseDto> GenerateRfcAsync(RfcGeneratorRequestDto request);
    Task<FiscalRegimeResponseDto> GetFiscalRegimeAsync(string insurerId);
    Task<FiscalRegimeResponseDto> GetFiscalRegimeByPersonTypeAsync(string insurerId, string personType);
    Task<PdfQuoteResponseDto> GeneratePdfQuoteAsync(PdfQuoteRequestDto request);
}
