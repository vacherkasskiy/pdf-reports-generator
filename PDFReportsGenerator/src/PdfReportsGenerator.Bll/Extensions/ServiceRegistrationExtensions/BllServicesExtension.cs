using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.Bll.BackgroundServices;
using PdfReportsGenerator.Bll.Configurations;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Services;
using PdfReportsGenerator.Bll.Services.Interfaces;
using ReportsServiceBll = PdfReportsGenerator.Bll.Services.ReportsService;
using PdfReportsGenerator.Bll.Validators;

namespace PdfReportsGenerator.Bll.Extensions.ServiceRegistrationExtensions;

public static class BllServicesExtension
{
    public static void AddBllServices(this IServiceCollection service, ConfigurationManager configuration)
    {
        service.AddScoped<IReportsService, ReportsServiceBll>();
        service.AddScoped<IValidator<Report>, ReportValidator>();
        service.AddScoped<IKafkaProducer, ReportKafkaProducer>();
        service.AddHostedService<ConsumeKafkaRecordsBackgroundService>();
        
        service.Configure<KafkaConfiguration>(
            configuration.GetSection(KafkaConfiguration.SectionName));
    }
}