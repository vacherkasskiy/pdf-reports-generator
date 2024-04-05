using PdfReportsGenerator.Dal;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PdfReportsGenerator.Bll.Exceptions;
using PdfReportsGenerator.Bll.Extensions.ServiceRegistrationExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty));

builder.Services.AddBllServices(builder.Configuration);

builder.Host.UseSerilog();

builder.WebHost.UseSentry(options => options.SetBeforeSend((sentryEvent, _) =>
{
    if (sentryEvent.Exception != null &&
        (sentryEvent.Exception is InvalidReportFormatException ||
         sentryEvent.Exception is ReportNotFoundException))
    {
        return null;
    }

    return sentryEvent;
}));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
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