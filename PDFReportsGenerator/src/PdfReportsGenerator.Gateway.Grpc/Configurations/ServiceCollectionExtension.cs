using AutoMapper;
using PdfReportsGenerator.Application.Builders;
using PdfReportsGenerator.Gateway.Grpc.Builders;

namespace PdfReportsGenerator.Gateway.Grpc.Configurations;

public static class ServiceCollectionExtension
{
    public static void ConfigureGrpcGateway(this IServiceCollection services)
    {
        services.ConfigureMapper();
    }

    private static void ConfigureMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new ReportBuilder());
            mc.AddProfile(new ReportRequestsBuilder());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}