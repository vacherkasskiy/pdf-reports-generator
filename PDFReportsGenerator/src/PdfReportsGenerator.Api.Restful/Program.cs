using FluentValidation;
using FluentValidation.AspNetCore;
using PdfReportsGenerator.Dal;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Bll.Validators;
using PdfReportsGenerator.Dal.Entities;
using PdfReportsGenerator.Dal.Repositories;
using PdfReportsGenerator.Dal.Repositories.Interfaces;
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

builder.Services.AddScoped<PdfReportsGenerator.Bll.Validators.Interfaces.IValidator<Report>, ReportValidator>();
builder.Services.AddScoped<IReportTasksService, ReportTasksService>();
builder.Services.AddScoped<IRepository<CreateReportTask>, ReportTasksRepository>();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblies(new [] {typeof(Program).Assembly})
    .AddFluentValidationClientsideAdapters();

builder.Host.UseSerilog();

builder.WebHost.UseSentry();

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

app.Run();