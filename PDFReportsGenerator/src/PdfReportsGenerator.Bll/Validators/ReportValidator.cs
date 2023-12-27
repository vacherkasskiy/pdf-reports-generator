using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Validators.Interfaces;

namespace PdfReportsGenerator.Bll.Validators;

public class ReportValidator : IValidator<Report>
{
    public bool IsValid(Report model)
    {
        if (model.Name == null) return false;
        if (model.Name.Length > 256) return false;

        if (model.Blocks == null) return false;
        foreach (var block in model.Blocks)
        {
            if (block == null) return false;

            var types = new[] { "text", "image", "table"};
            if (block.Type == null) return false;
            if (!types.Contains(block.Type)) return false;
            if (block.Location == null) return false;
            if (block.Location.Left < 1 ||
                block.Location.Left > 12) return false;
            if (block.Location.Right < 1 ||
                block.Location.Right > 12) return false;
            if (block.Location.Left > block.Location.Right) return false;

            switch (block)
            {
                case TextBlock textBlock
                    when !IsTextBlockValid(textBlock):
                case ImageBlock imageBlock
                    when !IsImageBlockValid(imageBlock):
                case TableBlock tableBlock
                    when !IsTableBlockValid(tableBlock):
                    return false;
            }
        }

        return true;
    }

    private bool IsTextBlockValid(TextBlock block)
    {
        if (block.Content == null) return false;
        
        if (block.Style == null) return false;
        if (block.Style.Size <= 0) return false;
        if (block.Style.Size > 6) return false;

        var positions = new[] {"left", "center", "right"};
        if (block.Style.Position == null) return false;
        if (!positions.Contains(block.Style.Position)) return false;

        return true;
    }

    private bool IsImageBlockValid(ImageBlock block)
    {
        if (block.Content == null) return false;

        return true;
    }

    private bool IsTableBlockValid(TableBlock block)
    {
        if (block.Content == null) return false;

        return true;
    }
}