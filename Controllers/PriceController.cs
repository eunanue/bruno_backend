using Microsoft.AspNetCore.Mvc;
using bruno_backend.Services;
using bruno_backend.DTOs;

namespace bruno_backend.Controllers;

[ApiController]
[Route("api")]
public class PriceController : ControllerBase
{
    private readonly IPriceService _priceService;
    private readonly ILogger<PriceController> _logger;

    public PriceController(IPriceService priceService, ILogger<PriceController> logger)
    {
        _priceService = priceService;
        _logger = logger;
    }

    /// <summary>
    /// Get primary UDI configuration for a merchant
    /// </summary>
    /// <param name="merchantId">Merchant ID</param>
    /// <returns>UDI configuration with alliance and discount identifiers per insurer</returns>
    [HttpGet("insurers/primary_udi")]
    public async Task<IActionResult> GetPrimaryUdi([FromQuery] string merchantId)
    {
        if (string.IsNullOrEmpty(merchantId))
        {
            return BadRequest(new { message = "merchantId is required" });
        }

        var result = await _priceService.GetPrimaryUdiAsync(merchantId);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Create a quote for a specific insurer, package and payment method
    /// </summary>
    /// <param name="request">Quote request with vehicle, driver and policy details</param>
    /// <returns>Quote result with premium breakdown and coverages</returns>
    [HttpPost("alfred/quote")]
    public async Task<IActionResult> CreateQuote([FromBody] QuoteRequestDto request)
    {
        if (request == null)
        {
            return BadRequest(new { message = "Request body is required" });
        }



        if (request.PackageId == 0 ||
            request.WayToPay == 0 ||
            request.InsuranceCompanyId == 0 ||
            string.IsNullOrEmpty(request.MakeId) ||
            string.IsNullOrEmpty(request.ModelId) ||
            string.IsNullOrEmpty(request.ModelString) ||
            string.IsNullOrEmpty(request.CirculationZipCode) ||
            string.IsNullOrEmpty(request.StartDate) ||
            string.IsNullOrEmpty(request.Uuid) ||
            request.Driver == null)
        {
            return BadRequest(new { message = "Required fields are missing (packageId, wayToPay, insuranceCompanyId, makeId, modelId, modelString, circulationZipCode, startDate, uuid, driver)" });
        }

        var result = await _priceService.CreateQuoteAsync(request);

        if (!result.Success)
        {
            return StatusCode(500, result);
        }

        return Ok(result);
    }
}
