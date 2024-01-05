using AutoBogus;
using Bogus;
using PdfReportsGenerator.Bll.Models;

namespace UnitTests.Fakers;

public static class ReportFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<Report> Faker = new AutoFaker<Report>()
        .RuleFor(x => x.Name, f => f.Lorem.Word())
        .RuleFor(x => x.Blocks, _ => new Block[] {});

    public static IEnumerable<Report> Generate(int count = 1)
    {
        lock (Lock)
        {
            var reports = new List<Report>();
            for (int i = 0; i < count; i++)
            {
                reports.Add(Faker.Generate());
            }

            return reports;
        }
    }
}