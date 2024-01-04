using PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;
using PdfReportsGenerator.Bll.Models;
using Reports.V1;

namespace PdfReportsGenerator.Api.Grpc.Parsers;

public class ReportsParser : IReportsParser
{
    public Report ParseReport(CreateReportRequest request)
    {
        throw new NotImplementedException();
    }
}