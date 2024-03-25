using PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;
using PdfReportsGenerator.Dal.Entities;
using Reports.V1;

namespace PdfReportsGenerator.Api.Grpc.Parsers;

public class ProtoStatusParser : IParser<ReportStatus, GetReportResponse.Types.Status>
{
    public GetReportResponse.Types.Status Parse(ReportStatus status)
    {
        switch (status)
        {
            case ReportStatus.NotStarted:
                return GetReportResponse.Types.Status.NotStarted;
            case ReportStatus.Processing:
                return GetReportResponse.Types.Status.Processing;
            case ReportStatus.Ready:
                return GetReportResponse.Types.Status.Ready;
            case ReportStatus.Error:
                return GetReportResponse.Types.Status.Error;
            default:
                throw new Exception("Unknown status provided.");
        }
    }
}