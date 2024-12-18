using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PdfReportsGenerator.BackgroundGenerator;

public static class Program
{
    public static void Main()
    {
        //setup our DI
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        //configure console logging
        serviceProvider
            .GetService<ILoggerFactory>()
            .AddConsole(LogLevel.Debug);

        var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();

        logger.LogDebug("Starting application");

        //do the actual work here
        var bar = serviceProvider.GetService<IBarService>();
        bar.DoSomeRealWork();

        logger.LogDebug("All done!");
    }
}