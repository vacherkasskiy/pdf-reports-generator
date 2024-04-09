using Microsoft.Extensions.DependencyInjection;
using PdfReportsGenerator.App.Bll.Services;
using PdfReportsGenerator.App.Bll.Services.Interfaces;

namespace PdfReportsGenerator.App.Bll.Extensions.ServiceRegistrationExtensions;

public static class BllServicesExtension
{
    public static void AddBllServices(this IServiceCollection service)
    {
        service.AddScoped<IReportsService, ReportsService>();
    }
}