namespace PdfReportsGenerator.Bll.Services.Interfaces;

public interface IKafkaProducer
{
    public Task Produce(string message);
}