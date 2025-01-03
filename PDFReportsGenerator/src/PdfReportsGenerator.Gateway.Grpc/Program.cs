using PdfReportsGenerator.Application.Configurations;
using PdfReportsGenerator.Gateway.Grpc.Configurations;
using PdfReportsGenerator.Gateway.Grpc.Services.V1;
using PdfReportsGenerator.Infrastructure.Configurations;
using PdfReportsGenerator.Infrastructure.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.ConfigureGrpcGateway();
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.ConfigureInfrastructure(builder.Configuration);

builder.Host.ConfigureLogging(builder.Configuration);

var app = builder.Build();

app.MapGrpcService<ReportsService>();
app.UseSerilogRequestLogging();
app.AddPrometheus();
app.UseExceptionHandler();
app.MigrateDb();
app.Run();