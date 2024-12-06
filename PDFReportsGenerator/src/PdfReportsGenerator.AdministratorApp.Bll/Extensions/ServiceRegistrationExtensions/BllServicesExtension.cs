using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.AdministratorApp.Bll.Services;
using PdfReportsGenerator.AdministratorApp.Bll.Services.Interfaces;

namespace PdfReportsGenerator.AdministratorApp.Bll.Extensions.ServiceRegistrationExtensions;

public static class BllServicesExtension
{
    public static void AddBllServices(this IServiceCollection service)
    {
        service.AddScoped<IReportsService, ReportsService>();
    }
}