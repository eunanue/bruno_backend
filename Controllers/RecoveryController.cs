using Microsoft.AspNetCore.Mvc;
using bruno_backend.Services;

namespace bruno_backend.Controllers;

[ApiController]
[Route("api/cloud")]
public class RecoveryController : ControllerBase
{
    private readonly IRecoveryService _recoveryService;
    private readonly ILogger<RecoveryController> _logger;

    public RecoveryController(IRecoveryService recoveryService, ILogger<RecoveryController> logger)
    {
        _recoveryService = recoveryService;
        _logger = logger;
    }

    /// <summary>
    /// Download the PDF of an emitted policy in Base64 format
    /// </summary>
    /// <param name="policyNumber">Policy number returned by the insurer after emission</param>
    /// <returns>PDF file in Base64 format with its filename</returns>
    [HttpPost("download-policy")]
    public async Task<IActionResult> DownloadPolicy([FromQuery] string policyNumber)
    {
        if (string.IsNullOrEmpty(policyNumber))
        {
            return BadRequest(new { message = "policyNumber is required" });
        }



        var result = await _recoveryService.DownloadPolicyAsync(policyNumber);

        if (!result.Success)
        {

            return StatusCode(500, result);
        }

        return Ok(result);
    }
}
