using Microsoft.AspNetCore.Mvc;
using bruno_backend.Services;
using bruno_backend.DTOs;

namespace bruno_backend.Controllers;

[ApiController]
[Route("api/alfred")]
public class HomologatorController : ControllerBase
{
    private readonly IHomologatorService _homologatorService;
    private readonly ILogger<HomologatorController> _logger;

    public HomologatorController(IHomologatorService homologatorService, ILogger<HomologatorController> logger)
    {
        _homologatorService = homologatorService;
        _logger = logger;
    }

    /// <summary>
    /// Get makes and submakes catalog
    /// </summary>
    /// <returns>List of makes and submakes</returns>
    [HttpGet("chubb/makes_submakes")]
    public async Task<IActionResult> GetMakesSubmakes()
    {
        var result = await _homologatorService.GetMakesSubmakesAsync();

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get vehicle types (models) by makeId, subMakeId, and year
    /// </summary>
    /// <param name="makeId">Make ID</param>
    /// <param name="subMakeId">SubMake ID</param>
    /// <param name="model">Vehicle year</param>
    /// <returns>List of vehicle types</returns>
    [HttpGet("chubb/makes/{makeId}/submakes/{subMakeId}/vehicle_types")]
    public async Task<IActionResult> GetVehicleTypes(
        [FromRoute] int makeId,
        [FromRoute] int subMakeId,
        [FromQuery] int model)
    {
        if (model == 0)
        {
            return BadRequest(new { message = "model (year) query parameter is required" });
        }

        var result = await _homologatorService.GetVehicleTypesAsync(makeId, subMakeId, model);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get vehicle descriptions (versions) by vehicleTypeId and year
    /// </summary>
    /// <param name="id">Vehicle type ID</param>
    /// <param name="model">Vehicle year</param>
    /// <returns>List of vehicle descriptions</returns>
    [HttpGet("chubb/makes/submakes/vehicle_types/{id}/vehicle_descriptions")]
    public async Task<IActionResult> GetVehicleDescriptions(
        [FromRoute] int id,
        [FromQuery] int model)
    {
        if (model == 0)
        {
            return BadRequest(new { message = "model (year) query parameter is required" });
        }

        var result = await _homologatorService.GetVehicleDescriptionsAsync(id, model);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Search homologation across insurance companies
    /// </summary>
    /// <param name="request">Vehicle details: year, make, submake, model, version</param>
    /// <returns>Homologation results per insurance company</returns>
    [HttpPost("search")]
    public async Task<IActionResult> SearchHomologation([FromBody] HomologationSearchRequestDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Request body is required" });
        }

        if (string.IsNullOrEmpty(request.YEAR) ||
            string.IsNullOrEmpty(request.MAKE) ||
            string.IsNullOrEmpty(request.SUBMAKE) ||
            string.IsNullOrEmpty(request.MODEL) ||
            string.IsNullOrEmpty(request.VERSION))
        {
            return BadRequest(new { message = "All fields (year, make, subMake, model, version) are required" });
        }

        var result = await _homologatorService.SearchHomologationAsync(request);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }
}
