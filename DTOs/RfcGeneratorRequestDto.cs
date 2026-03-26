namespace bruno_backend.DTOs;

public class RfcGeneratorRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string PrimaryLastName { get; set; } = string.Empty;
    public string SecondaryLastName { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
}
