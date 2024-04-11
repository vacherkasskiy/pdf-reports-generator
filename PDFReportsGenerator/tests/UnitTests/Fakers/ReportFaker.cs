using AutoBogus;
using Bogus;
using PdfReportsGenerator.Dal.Models;

namespace UnitTests.Fakers;

public static class ReportFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<ReportBody> Faker = new AutoFaker<ReportBody>()
        .RuleFor(x => x.Name, f => f.Lorem.Word())
        .RuleFor(x => x.Blocks, _ => new Block[] {});

    public static IEnumerable<ReportBody> Generate(int count = 1)
    {
        lock (Lock)
        {
            var reports = new List<ReportBody>();
            for (int i = 0; i < count; i++)
            {
                reports.Add(Faker.Generate());
            }

            return reports;
        }
    }
}