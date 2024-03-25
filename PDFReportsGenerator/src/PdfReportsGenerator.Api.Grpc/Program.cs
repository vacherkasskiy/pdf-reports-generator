using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Api.Grpc.Parsers;
using PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;
using PdfReportsGenerator.Bll.Extensions.ServiceRegistrationExtensions;
using PdfReportsGenerator.Dal;
using PdfReportsGenerator.Dal.Entities;
using Reports.V1;
using Serilog;
using Report = PdfReportsGenerator.Bll.Models.Report;
using ReportProto = Reports.V1.CreateReportRequest;
using ReportsService = PdfReportsGenerator.Api.Grpc.Services.V1.ReportsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IParser<ReportProto, Report>, ReportsParser>();
builder.Services.AddScoped<IParser<ReportStatus, GetReportResponse.Types.Status>, ProtoStatusParser>();
builder.Services.AddBllServices(builder.Configuration);

builder.Host.UseSerilog();
builder.WebHost.UseSentry();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ReportsService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseSerilogRequestLogging();
app.UseSentryTracing();
app.Run();