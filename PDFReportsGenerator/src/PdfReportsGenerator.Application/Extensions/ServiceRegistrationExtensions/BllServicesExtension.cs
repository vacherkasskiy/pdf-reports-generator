using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Application.ExceptionHandlers;
using PdfReportsGenerator.Application.Services;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Application.Validators;
using ReportsServiceBll = PdfReportsGenerator.Application.Services.ReportsService;
using PdfReportsGenerator.Core.Models;

namespace PdfReportsGenerator.Application.Extensions.ServiceRegistrationExtensions;

public static class BllServicesExtension
{
    public static void AddBllServices(this IServiceCollection service)
    {
        service.AddExceptionHandler<InvalidReportFormatExceptionHandler>();
        service.AddExceptionHandler<ReportNotFoundExceptionHandler>();
        service.AddProblemDetails();
        
        service.AddScoped<IReportsService, ReportsService>();
        service.AddScoped<IValidator<ReportBody>, ReportValidator>();
    }
}