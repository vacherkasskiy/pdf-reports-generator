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
    public static void ConfigureApplication(this IServiceCollection service)
    {
        service.AddExceptionHandler<InvalidReportFormatExceptionHandler>();
        service.AddExceptionHandler<ReportNotFoundExceptionHandler>();
        service.AddProblemDetails();
        
        service.AddScoped<IReportsService, ReportsService>();
        service.AddScoped<IValidator<ReportBody>, ReportValidator>();
    }
}