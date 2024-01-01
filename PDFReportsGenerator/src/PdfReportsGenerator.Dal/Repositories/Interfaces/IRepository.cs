namespace PdfReportsGenerator.Dal.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<T?> Get(long entityId);
    Task<T> Add(T entity);
    Task Delete(long entityId);
}