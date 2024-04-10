using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Bll.ExceptionHandlers;
using PdfReportsGenerator.Bll.Services.Interfaces;
using ReportsServiceBll = PdfReportsGenerator.Bll.Services.ReportsService;
using PdfReportsGenerator.Bll.Validators;
using PdfReportsGenerator.Dal.Models;

namespace PdfReportsGenerator.Bll.Extensions.ServiceRegistrationExtensions;

public static class BllServicesExtension
{
    public static void AddBllServices(this IServiceCollection service)
    {
        service.AddExceptionHandler<InvalidReportFormatExceptionHandler>();
        service.AddExceptionHandler<ReportNotFoundExceptionHandler>();
        service.AddProblemDetails();
        
        service.AddScoped<IReportsService, ReportsServiceBll>();
        service.AddScoped<IValidator<ReportBody>, ReportValidator>();
    }
}