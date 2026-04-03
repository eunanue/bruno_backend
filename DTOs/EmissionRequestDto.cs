namespace bruno_backend.DTOs;

public class EmissionRequestDto
{
    public int MovementId { get; set; }
    public CustomerEmissionDto? Customer { get; set; }
    public AddressDto? Address { get; set; }
    public FiscalDataEmissionDto? FiscalData { get; set; }
    public int InsuranceCompanyId { get; set; }
    public int PersonType { get; set; }
    public string? Color { get; set; }
    public string? Engine { get; set; }
    public string? Plates { get; set; }
    public string? Reference { get; set; }
    public string? SerialNumber { get; set; }
    public string? Uuid { get; set; }
    public bool VehicleValueType { get; set; }
}

public class CustomerEmissionDto
{
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? FirstSurname { get; set; }
    public string? LastSurname { get; set; }
    public string? Birthdate { get; set; }
    public string? Rfc { get; set; }
    public string? SecondName { get; set; }
    public int Sex { get; set; }
}

public class AddressDto
{
    public string? ColonyCode { get; set; }
    public string? ExternalNumber { get; set; }
    public string? InternalNumber { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public string? SettlementType { get; set; }
}

public class FiscalDataEmissionDto
{
    public int RegFiscalId { get; set; }
    public string? RegFiscalName { get; set; }
}
