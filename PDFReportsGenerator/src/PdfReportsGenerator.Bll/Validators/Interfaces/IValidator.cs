namespace PdfReportsGenerator.Bll.Validators.Interfaces;

public interface IValidator<T>
{
    bool IsValid(T model);
}