using Microsoft.AspNetCore.Mvc;
using bruno_backend.Services;
using bruno_backend.DTOs;

namespace bruno_backend.Controllers;

[ApiController]
[Route("api/alfred")]
public class EmissionController : ControllerBase
{
    private readonly IEmissionService _emissionService;
    private readonly ILogger<EmissionController> _logger;

    public EmissionController(IEmissionService emissionService, ILogger<EmissionController> logger)
    {
        _emissionService = emissionService;
        _logger = logger;
    }

    /// <summary>
    /// Sign (emit) a policy after a valid quote
    /// </summary>
    /// <param name="request">Emission request with movementId, customer, address, fiscal data and vehicle details</param>
    /// <returns>Emitted policy details with policy number, premium breakdown and coverages</returns>
    [HttpPost("sign")]
    public async Task<IActionResult> SignPolicy([FromBody] EmissionRequestDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Request body is required" });
        }

        if (request.MovementId == 0 ||
            request.InsuranceCompanyId == 0 ||
            string.IsNullOrEmpty(request.SerialNumber) ||
            string.IsNullOrEmpty(request.Uuid) ||
            request.Customer == null ||
            request.Address == null ||
            request.FiscalData == null)
        {
            return BadRequest(new { message = "Required fields are missing (movementId, insuranceCompanyId, serialNumber, uuid, customer, address, fiscalData)" });
        }

        if (string.IsNullOrEmpty(request.Customer.Email))
        {
            return BadRequest(new { message = "Customer email is required" });
        }

        var result = await _emissionService.SignPolicyAsync(request);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }
}
