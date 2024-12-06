using Microsoft.EntityFrameworkCore;
using PdfReportsGenerator.Application.Services;
using PdfReportsGenerator.Application.Services.Interfaces;
using PdfReportsGenerator.Application.Validators;
using PdfReportsGenerator.Core;

namespace PdfReportsGenerator.BackgroundWorker;

public class ReportsServiceProvider
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ReportValidator _validator;

    public ReportsServiceProvider()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString =
            "User ID=postgres;Password=root;Host=192.168.49.2;Port=30001;Database=postgres;Pooling=true;";
        optionsBuilder.UseNpgsql(connectionString);

        _dbContext = new ApplicationDbContext(optionsBuilder.Options);
        _validator = new ReportValidator();
    }

    public IReportsService GetReportsService()
    {
        return new ReportsService(
            _validator,
            _dbContext);
    }
}