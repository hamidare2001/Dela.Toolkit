using Dela.Toolkit.ServiceHost;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

var configuration = builder.Environment.GetConfiguration();
builder.Configuration.AddConfiguration(configuration);

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

app.UseHttpsRedirection();
 