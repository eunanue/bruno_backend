using bruno_backend.DTOs;

namespace bruno_backend.Services;

public interface IRecoveryService
{
    Task<DownloadPolicyResponseDto> DownloadPolicyAsync(string policyNumber);
}
