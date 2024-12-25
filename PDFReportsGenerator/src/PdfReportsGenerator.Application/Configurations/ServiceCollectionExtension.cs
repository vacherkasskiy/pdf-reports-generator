using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfReportsGenerator.Application.ExceptionHandlers;
using PdfReportsGenerator.Application.Helpers;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services;
using PdfReportsGenerator.Application.Services.Interfaces;
using Serilog;

namespace PdfReportsGenerator.Application.Configurations;

public static class ServiceCollectionExtension
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddExceptionHandler<InvalidReportFormatExceptionHandler>();
        services.AddExceptionHandler<ReportNotFoundExceptionHandler>();
        services.AddProblemDetails();

        services.AddScoped<IReportTaskService, ReportTaskService>();
        services.AddScoped<IValidator<ReportObject>, ReportObjectValidator>();
    }

    public static void ConfigureLogging(
        this IHostBuilder builder,
        IConfiguration configuration)
    {
        builder.UseSerilog();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}