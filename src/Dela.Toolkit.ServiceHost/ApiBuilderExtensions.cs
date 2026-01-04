 
using Microsoft.OpenApi;
using Serilog;

namespace Dela.Toolkit.ServiceHost;
 
public static class ApiBuilderExtensions
{
    private static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        var configuration = builder.Environment.GetConfiguration();
        builder.Configuration.AddConfiguration(configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();


        builder.Host.UseSerilog();
        builder.Services.AddHttpClient();
 
    }
    
    public static void AddSwagger(this WebApplicationBuilder builder, string appName, string appVersion, string swaggerVersion = "v1")
    {
        builder.Services.AddSwaggerGen(options => { options.SwaggerDoc(swaggerVersion, new OpenApiInfo { Title = $"{appName} {swaggerVersion}", Version = appVersion }); });
    }

    public static void CreateSerilogFileLogger(this WebApplicationBuilder builder, string appName, string appVersion)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.File($"logs/{appName}_v{appVersion}_.log", rollingInterval: RollingInterval.Day) // ذخیره در فایل با rotation روزانه 
            .CreateLogger();
    }
    
    

    public static void AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(MyAllowSpecificOrigins, policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()   // includes PUT, POST, OPTIONS
                    .AllowAnyHeader();
            });
        });
    }

    public static WebApplication CreateApp(this WebApplicationBuilder builder, bool addCors = false, bool appSwagger = false)
    {
        var app = builder.Build();

        app.UseSerilogRequestLogging();
        if (appSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        
        if (addCors)
            app.UseCors(MyAllowSpecificOrigins);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
    
    public static void Run(this WebApplication app)
    {
        Log.Information("Starting web application");
        try
        {
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
    }
}