namespace bruno_backend.DTOs;

public class QuoteRequestDto
{
    public int PackageId { get; set; }
    public int WayToPay { get; set; }
    public int InsuranceCompanyId { get; set; }
    public string? MakeString { get; set; }
    public string? MakeId { get; set; }
    public int SubMakeId { get; set; }
    public string? SubMakeString { get; set; }
    public string? TypeModelId { get; set; }
    public int Year { get; set; }
    public string? ModelId { get; set; }
    public string? ModelString { get; set; }
    public string? CirculationZipCode { get; set; }
    public string? VehicleTypeId { get; set; }
    public string? TypeId { get; set; }
    public int Occupants { get; set; }
    public string? StartDate { get; set; }
    public DriverDto? Driver { get; set; }
    public string? Uuid { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? SalesExecutiveId { get; set; }
    public bool VehicleValueType { get; set; }
    public int UdiId { get; set; }
    public List<int>? Msi { get; set; }
    public InvoiceValuePolicyDto? InvoiceValuePolicy { get; set; }
}

public class DriverDto
{
    public string? Birthdate { get; set; }
    public string? FirstName { get; set; }
    public string? FirstSurname { get; set; }
    public string? LastSurname { get; set; }
    public int Sex { get; set; }
    public string? Rfc { get; set; }
    public string? SecondName { get; set; }
}

public class InvoiceValuePolicyDto
{
    public string? InvoiceDate { get; set; }
    public string? InvoiceNumber { get; set; }
    public decimal InvoiceValue { get; set; }
}
