namespace bruno_backend.DTOs;

public class PdfQuoteRequestDto
{
    public int PackageId { get; set; }
    public int WayToPay { get; set; }
    public string ModelString { get; set; } = string.Empty;
    public string Birthdate { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string CirculationZipCode { get; set; } = string.Empty;
    public string Uuid { get; set; } = string.Empty;
    public int MerchantId { get; set; }
    public bool DownloadImage { get; set; }
    public bool InvoiceValue { get; set; }
}
