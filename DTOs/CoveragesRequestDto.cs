namespace bruno_backend.DTOs;

public class CoveragesRequestDto
{
    public string PackageId { get; set; } = string.Empty;
    public string InsurerId { get; set; } = string.Empty;
    public string SubLineBusinessId { get; set; } = string.Empty;
}
