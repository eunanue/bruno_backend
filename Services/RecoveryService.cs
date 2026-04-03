using bruno_backend.DTOs;

namespace bruno_backend.Services;

public class RecoveryService : IRecoveryService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<RecoveryService> _logger;

    public RecoveryService(HttpClient httpClient, IConfiguration configuration, ILogger<RecoveryService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<DownloadPolicyResponseDto> DownloadPolicyAsync(string policyNumber)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new DownloadPolicyResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }



            var url = $"{host}api/cloud/download-policy?policyNumber={policyNumber}";

            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: POST");
            _logger.LogInformation($"Parameters: policyNumber={policyNumber}");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.PostAsync(url,null);
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content Length: {responseContent.Length} characters");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = System.Text.Json.JsonSerializer.Deserialize<object>(responseContent);
                return new DownloadPolicyResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Policy PDF retrieved successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {responseContent}");
            return new DownloadPolicyResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DownloadPolicyAsync when calling Bruno API");
            return new DownloadPolicyResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }
}
