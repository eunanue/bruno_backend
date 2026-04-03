namespace bruno_backend.DTOs;

public class HomologationRequestDto
{
    public string? Year { get; set; }
    public string? Make { get; set; }
    public string? SubMake { get; set; }
    public string? Model { get; set; }
    public string? Version { get; set; }
}


public class HomologationSearchRequestDto
{
    public string? YEAR { get; set; }
    public string? MAKE { get; set; }
    public string? SUBMAKE { get; set; }
    public string? MODEL { get; set; }
    public string? VERSION { get; set; }
}
