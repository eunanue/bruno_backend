using Microsoft.AspNetCore.Mvc;
using bruno_backend.Services;
using bruno_backend.DTOs;

namespace bruno_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UtilsController : ControllerBase
{
    private readonly IUtilsService _utilsService;
    private readonly ILogger<UtilsController> _logger;

    public UtilsController(IUtilsService utilsService, ILogger<UtilsController> logger)
    {
        _utilsService = utilsService;
        _logger = logger;
    }

    /// <summary>
    /// Get coverages by packageId, insurerId, and subLineBusinessId
    /// </summary>
    /// <param name="packageId">Package ID</param>
    /// <param name="insurerId">Insurer ID</param>
    /// <param name="subLineBusinessId">Sub Line Business ID</param>
    /// <returns>Coverages data</returns>
    [HttpGet("sublinebusiness/coverages")]
    public async Task<IActionResult> GetCoverages(
        [FromQuery] string packageId,
        [FromQuery] string insurerId,
        [FromQuery] string subLineBusinessId)
    {
        if (string.IsNullOrEmpty(packageId) || string.IsNullOrEmpty(insurerId) || string.IsNullOrEmpty(subLineBusinessId))
        {
            return BadRequest(new { message = "All parameters (packageId, insurerId, subLineBusinessId) are required" });
        }

        var result = await _utilsService.GetCoveragesAsync(packageId, insurerId, subLineBusinessId);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get packages by subLineBusinessId
    /// </summary>
    /// <param name="subLineBusinessId">Sub Line Business ID</param>
    /// <returns>Packages data</returns>
    [HttpGet("sublinebusiness/{subLineBusinessId}/packages")]
    public async Task<IActionResult> GetPackages([FromRoute] string subLineBusinessId)
    {
        if (string.IsNullOrEmpty(subLineBusinessId))
        {
            return BadRequest(new { message = "subLineBusinessId is required" });
        }

        var result = await _utilsService.GetPackagesAsync(subLineBusinessId);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get settlements by zip code
    /// </summary>
    /// <param name="zipCode">Zip Code</param>
    /// <returns>Settlements data</returns>
    [HttpGet("settlements/zipcode/{zipCode}")]
    public async Task<IActionResult> GetSettlementsByZipCode([FromRoute] string zipCode)
    {
        if (string.IsNullOrEmpty(zipCode))
        {
            return BadRequest(new { message = "zipCode is required" });
        }

        var result = await _utilsService.GetSettlementsByZipCodeAsync(zipCode);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Validate VIN (Vehicle Identification Number)
    /// </summary>
    /// <param name="vin">Vehicle Identification Number</param>
    /// <returns>VIN validation result</returns>
    [HttpGet("workorders/vin")]
    public async Task<IActionResult> ValidateVin([FromQuery] string vin)
    {
        if (string.IsNullOrEmpty(vin))
        {
            return BadRequest(new { message = "VIN is required" });
        }

        // Get token from request headers
        if (!Request.Headers.TryGetValue("token", out var token) || string.IsNullOrEmpty(token))
        {
            return Unauthorized(new { message = "Authentication token is required" });
        }

        var result = await _utilsService.ValidateVinAsync(vin, token);

        if (!result.Success)
        {
            if (result.Message?.Contains("token") == true || result.Message?.Contains("Authentication") == true)
            {
                return Unauthorized(result);
            }
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Generate RFC (Registro Federal de Contribuyentes)
    /// </summary>
    /// <param name="request">RFC generation request with name, primary last name, secondary last name, and birth date</param>
    /// <returns>Generated RFC</returns>
    [HttpPost("users/rfc")]
    public async Task<IActionResult> GenerateRfc([FromBody] RfcGeneratorRequestDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Request body is required" });
        }

        if (string.IsNullOrEmpty(request.Name) ||
            string.IsNullOrEmpty(request.PrimaryLastName) ||
            string.IsNullOrEmpty(request.SecondaryLastName) ||
            string.IsNullOrEmpty(request.BirthDate))
        {
            return BadRequest(new { message = "All fields (name, primaryLastName, secondaryLastName, birthDate) are required" });
        }

        var result = await _utilsService.GenerateRfcAsync(request);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }
}
