namespace bruno_backend.DTOs;

public class PdfQuoteResponseDto
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }
}
