using PdfReportsGenerator.Application.Configurations;
using Serilog;
using ReportsService = PdfReportsGenerator.Gateway.Grpc.Services.V1.ReportsService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.ConfigureApplication();

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

app.MapGrpcService<ReportsService>();
app.UseSerilogRequestLogging();
app.Run();