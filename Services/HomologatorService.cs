using bruno_backend.DTOs;
using System.Text.Json;
using System.Text;

namespace bruno_backend.Services;

public class HomologatorService : IHomologatorService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<HomologatorService> _logger;

    public HomologatorService(HttpClient httpClient, IConfiguration configuration, ILogger<HomologatorService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<CatalogResponseDto> GetMakesSubmakesAsync()
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new CatalogResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/alfred/chubb/makes_submakes";

            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
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
                return new CatalogResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Makes and submakes retrieved successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");
            return new CatalogResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetMakesSubmakesAsync when calling Bruno API");
            return new CatalogResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<CatalogResponseDto> GetVehicleTypesAsync(int makeId, int subMakeId, int model)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new CatalogResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/alfred/chubb/makes/{makeId}/submakes/{subMakeId}/vehicle_types?model={model}";

            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: makeId={makeId}, subMakeId={subMakeId}, model={model}");
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
                return new CatalogResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Vehicle types retrieved successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");
            return new CatalogResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetVehicleTypesAsync when calling Bruno API");
            return new CatalogResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<CatalogResponseDto> GetVehicleDescriptionsAsync(int vehicleTypeId, int model)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new CatalogResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/alfred/chubb/makes/submakes/vehicle_types/{vehicleTypeId}/vehicle_descriptions?model={model}";

            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: vehicleTypeId={vehicleTypeId}, model={model}");
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
                return new CatalogResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Vehicle descriptions retrieved successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");
            return new CatalogResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetVehicleDescriptionsAsync when calling Bruno API");
            return new CatalogResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<HomologationResponseDto> SearchHomologationAsync(HomologationSearchRequestDto request)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new HomologationResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/alfred/search";

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
                return new HomologationResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Homologation search completed successfully"
                };
            }

            _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {responseContent}");
            return new HomologationResponseDto
            {
                Success = false,
                Message = $"External API error: {response.StatusCode}"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SearchHomologationAsync when calling Bruno API");
            return new HomologationResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }
}
