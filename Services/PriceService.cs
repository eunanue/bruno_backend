using bruno_backend.DTOs;
using System.Text.Json;
using System.Text;

namespace bruno_backend.Services;

public class PriceService : IPriceService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PriceService> _logger;

    public PriceService(HttpClient httpClient, IConfiguration configuration, ILogger<PriceService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<PrimaryUdiResponseDto> GetPrimaryUdiAsync(string merchantId)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new PrimaryUdiResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/insurers/primary_udi?merchantId={merchantId}";

            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: merchantId={merchantId}");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

            var content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {content}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(content);
                return new PrimaryUdiResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Primary UDI configuration retrieved successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");
            return new PrimaryUdiResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetPrimaryUdiAsync when calling Bruno API");
            return new PrimaryUdiResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<QuoteResponseDto> CreateQuoteAsync(QuoteRequestDto request)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new QuoteResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }



            var url = $"{host}api/alfred/quote";

            var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: POST");
            _logger.LogInformation($"Request Body: {jsonContent}");
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
                return new QuoteResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Quote created successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {responseContent}");
            return new QuoteResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateQuoteAsync when calling Bruno API");
            return new QuoteResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }
}
