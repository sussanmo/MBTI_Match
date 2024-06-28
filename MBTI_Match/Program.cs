using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using OpenTelemetry.Exporter;
// Import the Azure.Monitor.OpenTelemetry.AspNetCore namespace.
using Azure.Monitor.OpenTelemetry.AspNetCore;
using System.Diagnostics.Metrics;
using Azure.Monitor.OpenTelemetry.Exporter;
using MBTI_Match.Pages;

var builder = WebApplication.CreateBuilder(args);

// Define the service name and version
var serviceName = "MBTI_Match_Service";
var serviceVersion = "1.0.0";

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(logging =>
    {
        logging.AddConsoleExporter();
    });
});

// Add the OpenTelemetry telemetry service to the application.
// This service will collect and send telemetry data to Azure Monitor.
//builder.Services.AddOpenTelemetry().UseAzureMonitor(options => {
//options.ConnectionString = "InstrumentationKey=063a6317-ba7d-44c9-a8d8-4ea607d45301;IngestionEndpoint=https://westus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://westus.livediagnostics.monitor.azure.com/;ApplicationId=c2f5e73f-5b25-4d71-9736-ef308e914569";
//});


// Configure the OpenTelemetry meter provider to add a meter named "OTel.AzureMonitor.Demo".
builder.Services.ConfigureOpenTelemetryMeterProvider((sp, builder) => builder.AddMeter("mbti_match").AddAzureMonitorMetricExporter(options =>
//AddMeter("Otel.AzureMonitor.demo") // Make sure this matches with the Meter name you will use
{
    options.ConnectionString = "InstrumentationKey=063a6317-ba7d-44c9-a8d8-4ea607d45301;IngestionEndpoint=https://westus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://westus.livediagnostics.monitor.azure.com/;ApplicationId=c2f5e73f-5b25-4d71-9736-ef308e914569";
}));
// Add OpenTelemetry services
builder.Services.AddOpenTelemetry().UseAzureMonitor(options =>
//AddMeter("Otel.AzureMonitor.demo") // Make sure this matches with the Meter name you will use
         {
             options.ConnectionString = "InstrumentationKey=063a6317-ba7d-44c9-a8d8-4ea607d45301;IngestionEndpoint=https://westus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://westus.livediagnostics.monitor.azure.com/;ApplicationId=c2f5e73f-5b25-4d71-9736-ef308e914569";
         });

builder.Logging.AddOpenTelemetry(options => options
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
        serviceName: serviceName,
        serviceVersion: serviceVersion))
    .AddConsoleExporter());

builder.Services.AddControllers();


// Add services to the container.
builder.Services.AddRazorPages();

var meter = new Meter("mbti_match");

// Create a new counter metric named "MyFruitCounter".
//Counter<long> mbtiCounter = meter.CreateCounter<long>("mbtiCounter");

// Create counters for the button clicks, personality type, and activity
Counter<long> clickCounter = meter.CreateCounter<long>("ButtonClicks");
Counter<long> personalityTypeCounter = meter.CreateCounter<long>("PersonalityType");
Counter<long> activityCounter = meter.CreateCounter<long>("Activity");

// Add counters and MetricsService to the DI container to access them in other classes
builder.Services.AddSingleton(clickCounter);
builder.Services.AddSingleton(personalityTypeCounter);
builder.Services.AddSingleton(activityCounter);
builder.Services.AddSingleton<MetricsService>();

clickCounter.Add(20);
/*
// Update metrics method
public static void UpdateMetrics(string personalityType, string activity)
{
    // Example usage: incrementing the counters
    
    personalityTypeCounter.Add(1, new KeyValuePair<string, object?>("PersonalityType", personalityType));
    activityCounter.Add(1, new KeyValuePair<string, object?>("Activity", activity));
}
*/

var app = builder.Build();

/*
// Example usage: incrementing the counters
clickCounter.Add(1, new KeyValuePair<string, object?>("UserAction", "ButtonClicked"));
personalityTypeCounter.Add(1, new KeyValuePair<string, object?>("PersonalityType", "INTJ"));
activityCounter.Add(1, new KeyValuePair<string, object?>("Activity", "UserLogin"));
*/
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
/*
app.UseEndpoints(endpoints =>
    {
        endpoints.MapRazorPages();
    });
*/
app.MapRazorPages();

app.Run();

/*
Sdk.CreateMeterProviderBuilder()
    .AddMeter("MBTI_Match")
    .AddAzureMonitorMetricExporter(options =>
    {
        options.ConnectionString = "InstrumentationKey=063a6317-ba7d-44c9-a8d8-4ea607d45301;IngestionEndpoint=https://westus-0.in.applicationinsights.azure.com/;LiveEndpoint=https://westus.livediagnostics.monitor.azure.com/;ApplicationId=c2f5e73f-5b25-4d71-9736-ef308e914569";
    })
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
        serviceName: serviceName,
        serviceVersion: serviceVersion))
    .Build();
/*

// Add OpenTelemetry services
builder.Services.AddOpenTelemetry().UseAzureMonitor()
    .ConfigureResource(resource => resource.AddService(
        serviceName: serviceName,
        serviceVersion: serviceVersion))

    .WithMetrics(metrics => metrics
        .AddMeter(serviceName)
        .AddConsoleExporter());

*/