using bruno_backend.DTOs;

namespace bruno_backend.Services;

public interface IPriceService
{
    Task<PrimaryUdiResponseDto> GetPrimaryUdiAsync(string merchantId);
    Task<QuoteResponseDto> CreateQuoteAsync(QuoteRequestDto request);
}
