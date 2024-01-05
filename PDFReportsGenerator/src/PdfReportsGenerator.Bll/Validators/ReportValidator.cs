using FluentValidation;
using PdfReportsGenerator.Bll.Models;

namespace PdfReportsGenerator.Bll.Validators;

public class ReportValidator : AbstractValidator<Report>
{
    private class BlockValidator : AbstractValidator<Block>
    {
        public BlockValidator()
        {
            RuleFor(x => x.Location)
                .NotNull()
                .NotEmpty()
                .DependentRules(() =>
                    {
                        RuleFor(x => x.Location!.Left)
                            .GreaterThanOrEqualTo(1)
                            .LessThanOrEqualTo(12)
                            .LessThanOrEqualTo(x => x.Location!.Right);
                        RuleFor(x => x.Location!.Right)
                            .GreaterThanOrEqualTo(1)
                            .LessThanOrEqualTo(12)
                            .GreaterThanOrEqualTo(x => x.Location!.Left);
                    }
                );
            RuleFor(x => x.Type)
                .NotNull()
                .Must(new[] {"text", "image", "table"}.Contains);

            When(x => x.Type == "text", () =>
                RuleFor(x => x as TextBlock).SetValidator(new TextBlockValidator()!));

            When(x => x.Type == "image", () =>
                RuleFor(x => x as ImageBlock).SetValidator(new ImageBlockValidator()!));

            When(x => x.Type == "table", () =>
                RuleFor(x => x as TableBlock).SetValidator(new TableBlockValidator()!));
        }
    }

    private class TextBlockValidator : AbstractValidator<TextBlock>
    {
        public TextBlockValidator()
        {
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Style)
                .NotNull()
                .NotEmpty()
                .DependentRules(() =>
                {
                    RuleFor(x => x.Style!.Size)
                        .GreaterThanOrEqualTo(1)
                        .LessThanOrEqualTo(6);
                    RuleFor(x => x.Style!.Position)
                        .NotNull()
                        .Must(new[] {"left", "center", "right"}.Contains);
                });
        }
    }

    private class ImageBlockValidator : AbstractValidator<ImageBlock>
    {
        public ImageBlockValidator()
        {
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty();
        }
    }

    private class TableBlockValidator : AbstractValidator<TableBlock>
    {
        public TableBlockValidator()
        {
            RuleFor(x => x.Content).NotNull();
            RuleForEach(x => x.Content).NotNull();
        }
    }

    public ReportValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256);
        RuleFor(x => x.Blocks).NotNull();
        RuleForEach(x => x.Blocks)
            .NotNull()
            .SetValidator(new BlockValidator()!);
    }
}