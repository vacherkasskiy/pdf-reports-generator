using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PdfReportsGenerator.Bll.Configurations;
using PdfReportsGenerator.Bll.Services;
using PdfReportsGenerator.Bll.Services.Interfaces;
using PdfReportsGenerator.Bll.Validators;
using PdfReportsGenerator.Dal;

namespace PdfReportsGenerator.BackgroundWorker;

public class ReportsServiceProvider
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ReportValidator _validator;
    private readonly ReportKafkaProducer _kafkaProducer;

    public ReportsServiceProvider()
    {
        var kafkaConfiguration = new Configurations.KafkaConfiguration();
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString =
            "User ID=postgres;Password=root;Host=192.168.49.2;Port=30001;Database=postgres;Pooling=true;";
        optionsBuilder.UseNpgsql(connectionString);

        _dbContext = new ApplicationDbContext(optionsBuilder.Options);
        _validator = new ReportValidator();
        var options = Options.Create(new KafkaConfiguration
        {
            SaslUsername = kafkaConfiguration.SaslUsername,
            SaslPassword = kafkaConfiguration.SaslPassword,
            KafkaExternalAddress = kafkaConfiguration.KafkaExternalAddress
        });
        
        _kafkaProducer = new ReportKafkaProducer(options);
    }

    public IReportsService GetReportsService()
    {
        return new ReportsService(
            _validator,
            _dbContext,
            _kafkaProducer);
    }
}