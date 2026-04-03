using bruno_backend.DTOs;
using System.Text.Json;
using System.Text;

namespace bruno_backend.Services;

public class EmissionService : IEmissionService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmissionService> _logger;

    public EmissionService(HttpClient httpClient, IConfiguration configuration, ILogger<EmissionService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<EmissionResponseDto> SignPolicyAsync(EmissionRequestDto request)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new EmissionResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/alfred/sign";

            var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: POST");
            _logger.LogInformation($"Request Body: {jsonContent}");
            _logger.LogInformation($"API Key: {apiKey.Substring(0, Math.Min(10, apiKey.Length))}...");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.PostAsync(url, content);
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

            var responseContent = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {responseContent}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(responseContent);
                return new EmissionResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Policy signed successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {responseContent}");
            return new EmissionResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SignPolicyAsync when calling Bruno API");
            return new EmissionResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }
}
