using PdfReportsGenerator.Application.Configurations;
using Serilog;
using ReportsService = PdfReportsGenerator.Gateway.Grpc.Services.V1.ReportsService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
// TODO
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<IParser<ReportProto, ReportBody>, ReportsParser>();
builder.Services.ConfigureApplication();

builder.Host.UseSerilog();
builder.WebHost.UseSentry();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

app.MapGrpcService<ReportsService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.UseSerilogRequestLogging();
//app.UseSentryTracing();
app.Run();