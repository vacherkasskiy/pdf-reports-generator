using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Application.ExceptionHandlers;
using PdfReportsGenerator.Application.Models;
using PdfReportsGenerator.Application.Services;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Application.Validators;

namespace PdfReportsGenerator.Application.Configurations;

public static class ServiceCollectionExtension
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddExceptionHandler<InvalidReportFormatExceptionHandler>();
        services.AddExceptionHandler<ReportNotFoundExceptionHandler>();
        services.AddProblemDetails();
        
        services.AddScoped<IReportTaskService, ReportTaskService>();
        services.AddScoped<IValidator<ReportBody>, ReportValidator>();
    }
}