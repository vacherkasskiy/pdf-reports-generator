using AutoBogus;
using Bogus;
using PdfReportsGenerator.Application.Models;

namespace UnitTests.Fakers;

public static class ReportFaker
{
    private static readonly object Lock = new();

    private static readonly Faker<ReportObject> Faker = new AutoFaker<ReportObject>()
        .RuleFor(x => x.AuthorName, f => f.Lorem.Word())
        .RuleFor(x => x.ReportName, f => f.Lorem.Word())
        .RuleFor(x => x.Blocks, _ => []);

    public static IEnumerable<ReportObject> Generate(int count = 1)
    {
        lock (Lock)
        {
            var reports = new List<ReportObject>();
            for (int i = 0; i < count; i++)
            {
                reports.Add(Faker.Generate());
            }

            return reports;
        }
    }
}