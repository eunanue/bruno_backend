namespace bruno_backend.DTOs;

public class EmissionResponseDto
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }
}
