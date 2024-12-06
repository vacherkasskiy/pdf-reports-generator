using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PdfReportsGenerator.Application.Configurations;
using PdfReportsGenerator.Gateway.Rest.Configurations;
using PdfReportsGenerator.Infrastructure.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureRestGateway();
builder.Services.ConfigureApplication();
builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Host.UseSerilog();

// builder.WebHost.UseSentry(options => options.SetBeforeSend((sentryEvent, _) =>
// {
//     if (sentryEvent.Exception
//         is InvalidReportFormatException
//         or ReportNotFoundException)
//     {
//         return null;
//     }
//
//     return sentryEvent;
// }));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

app.ApplyRestGatewaySettings();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSentryTracing();
app.UseExceptionHandler();
app.Run();