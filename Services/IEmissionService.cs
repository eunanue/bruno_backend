using bruno_backend.DTOs;

namespace bruno_backend.Services;

public interface IEmissionService
{
    Task<EmissionResponseDto> SignPolicyAsync(EmissionRequestDto request);
}
