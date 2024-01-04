using PdfReportsGenerator.Bll.Models;
using Reports.V1;

namespace PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;

public interface IReportsParser
{
    Report ParseReport(CreateReportRequest report);
}