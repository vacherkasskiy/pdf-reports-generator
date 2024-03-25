namespace PdfReportsGenerator.Api.Grpc.Parsers.Interfaces;

public interface IParser<in T1, out T2>
{
    T2 Parse(T1 obj);
}