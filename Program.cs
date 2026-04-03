using bruno_backend.Services;
using Serilog;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build())
    .CreateLogger();

try
{
    Log.Information("Starting Bruno Backend API");

    var builder = WebApplication.CreateBuilder(args);

    // Add Serilog
    builder.Host.UseSerilog();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient<IUtilsService, UtilsService>();
    builder.Services.AddHttpClient<IHomologatorService, HomologatorService>();
    builder.Services.AddHttpClient<IPriceService, PriceService>();
    builder.Services.AddHttpClient<IEmissionService, EmissionService>();
    builder.Services.AddHttpClient<IRecoveryService, RecoveryService>();

    var app = builder.Build();

    // PathBase configurable por ambiente
    var pathBase = builder.Configuration["AppSettings:PathBase"];
    if (!string.IsNullOrEmpty(pathBase))
    {
        app.UsePathBase(pathBase);
        app.UseRouting();
    }

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var swaggerPrefix = string.IsNullOrEmpty(pathBase) ? "" : pathBase;
        c.SwaggerEndpoint($"{swaggerPrefix}/swagger/v1/swagger.json", "Bruno Backend v1");
    });

    app.MapControllers();
    app.MapGet("/", () => "Hello World!");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
