using bruno_backend.DTOs;
using System.Text.Json;
using System.Text;

namespace bruno_backend.Services;

public class UtilsService : IUtilsService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UtilsService> _logger;

    public UtilsService(HttpClient httpClient, IConfiguration configuration, ILogger<UtilsService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<CoveragesResponseDto> GetCoveragesAsync(string packageId, string insurerId, string subLineBusinessId)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new CoveragesResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/sublinebusiness/coverages?packageId={packageId}&insurerId={insurerId}&subLineBusinessId={subLineBusinessId}";

            // Log request details
            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: packageId={packageId}, insurerId={insurerId}, subLineBusinessId={subLineBusinessId}");
            _logger.LogInformation($"API Key: {apiKey.Substring(0, Math.Min(10, apiKey.Length))}...");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var content = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {content}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(content);

                return new CoveragesResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Coverages retrieved successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");

                return new CoveragesResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetCoveragesAsync when calling Bruno API");
            return new CoveragesResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<PackagesResponseDto> GetPackagesAsync(string subLineBusinessId)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new PackagesResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/sublinebusiness/{subLineBusinessId}/packages";

            // Log request details
            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: subLineBusinessId={subLineBusinessId}");
            _logger.LogInformation($"API Key: {apiKey.Substring(0, Math.Min(10, apiKey.Length))}...");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var content = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {content}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(content);

                return new PackagesResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Packages retrieved successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");

                return new PackagesResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetPackagesAsync when calling Bruno API");
            return new PackagesResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<SettlementsResponseDto> GetSettlementsByZipCodeAsync(string zipCode)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new SettlementsResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/settlements/zipcode/{zipCode}";

            // Log request details
            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: zipCode={zipCode}");
            _logger.LogInformation($"API Key: {apiKey.Substring(0, Math.Min(10, apiKey.Length))}...");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var content = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {content}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(content);

                return new SettlementsResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Settlements retrieved successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");

                return new SettlementsResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetSettlementsByZipCodeAsync when calling Bruno API");
            return new SettlementsResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<VinValidationResponseDto> ValidateVinAsync(string vin, string? token)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new VinValidationResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Authentication token is required for VIN validation");
                return new VinValidationResponseDto
                {
                    Success = false,
                    Message = "Authentication token is required"
                };
            }

            var url = $"{host}api/workorders/vin?vin={vin}";

            // Log request details
            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: vin={vin}");
            _logger.LogInformation($"Token: {token.Substring(0, Math.Min(10, token.Length))}...");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("token", token);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var content = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {content}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(content);

                return new VinValidationResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "VIN validated successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");

                return new VinValidationResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ValidateVinAsync when calling Bruno API");
            return new VinValidationResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<RfcGeneratorResponseDto> GenerateRfcAsync(RfcGeneratorRequestDto request)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new RfcGeneratorResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/users/rfc";

            // Serialize request body
            var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // Log request details
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
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var responseContent = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {responseContent}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(responseContent);

                return new RfcGeneratorResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "RFC generated successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {responseContent}");

                return new RfcGeneratorResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GenerateRfcAsync when calling Bruno API");
            return new RfcGeneratorResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<FiscalRegimeResponseDto> GetFiscalRegimeAsync(string insurerId)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];
            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new FiscalRegimeResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Authentication token is required for fiscal regime catalog");
                return new FiscalRegimeResponseDto
                {
                    Success = false,
                    Message = "Authentication token is required"
                };
            }

            var url = $"{host}api/insurers/fiscal/{insurerId}";

            // Log request details
            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: insurerId={insurerId}");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var content = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {content}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(content);

                return new FiscalRegimeResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Fiscal regime catalog retrieved successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");

                return new FiscalRegimeResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetFiscalRegimeAsync when calling Bruno API");
            return new FiscalRegimeResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<FiscalRegimeResponseDto> GetFiscalRegimeByPersonTypeAsync(string insurerId, string personType)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new FiscalRegimeResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Authentication token is required for fiscal regime catalog");
                return new FiscalRegimeResponseDto
                {
                    Success = false,
                    Message = "Authentication token is required"
                };
            }

            var url = $"{host}api/insurers/fiscal/{insurerId}/person-type/{personType}";

            // Log request details
            _logger.LogInformation("========== BRUNO API REQUEST ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"URL: {url}");
            _logger.LogInformation($"Method: GET");
            _logger.LogInformation($"Parameters: insurerId={insurerId}, personType={personType}");
            _logger.LogInformation("=======================================");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url);
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var content = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content: {content}");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(content);

                return new FiscalRegimeResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "Fiscal regime catalog retrieved successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {content}");

                return new FiscalRegimeResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetFiscalRegimeByPersonTypeAsync when calling Bruno API");
            return new FiscalRegimeResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }

    public async Task<PdfQuoteResponseDto> GeneratePdfQuoteAsync(PdfQuoteRequestDto request)
    {
        try
        {
            var host = _configuration["BrunoApi:Host"];
            var apiKey = _configuration["BrunoApi:ApiKey"];

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(apiKey))
            {
                _logger.LogError("Bruno API configuration is missing in appsettings.json");
                return new PdfQuoteResponseDto
                {
                    Success = false,
                    Message = "API configuration is missing"
                };
            }

            var url = $"{host}api/alfred/pdfquote";

            // Serialize request body
            var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            // Log request details
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
            var endTime = DateTime.UtcNow;
            var duration = (endTime - startTime).TotalMilliseconds;

            var responseContent = await response.Content.ReadAsStringAsync();

            // Log response details
            _logger.LogInformation("========== BRUNO API RESPONSE ==========");
            _logger.LogInformation($"Host: {host}");
            _logger.LogInformation($"Status Code: {(int)response.StatusCode} - {response.StatusCode}");
            _logger.LogInformation($"Duration: {duration}ms");
            _logger.LogInformation($"Response Content Length: {responseContent.Length} characters");
            _logger.LogInformation("========================================");

            if (response.IsSuccessStatusCode)
            {
                var data = JsonSerializer.Deserialize<object>(responseContent);

                return new PdfQuoteResponseDto
                {
                    Success = true,
                    Data = data,
                    Message = "PDF/Image quote generated successfully"
                };
            }
            else
            {
                _logger.LogError($"Bruno API Error - Status: {response.StatusCode}, Content: {responseContent}");

                return new PdfQuoteResponseDto
                {
                    Success = false,
                    Message = $"External API error: {response.StatusCode}"
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GeneratePdfQuoteAsync when calling Bruno API");
            return new PdfQuoteResponseDto
            {
                Success = false,
                Message = $"Internal error: {ex.Message}"
            };
        }
    }
}
