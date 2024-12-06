using FluentValidation;
using PdfReportsGenerator.Application.Models;

namespace PdfReportsGenerator.Application.Validators;

public class ReportValidator : AbstractValidator<ReportBody>
{
    private class MarginValidator : AbstractValidator<Margin>
    {
        public MarginValidator()
        {
            RuleFor(x => x.Top)
                .InclusiveBetween(-100, 100)
                .When(x => x.Top.HasValue);

            RuleFor(x => x.Bottom)
                .InclusiveBetween(-100, 100)
                .When(x => x.Bottom.HasValue);

            RuleFor(x => x.Left)
                .InclusiveBetween(-100, 100)
                .When(x => x.Left.HasValue);

            RuleFor(x => x.Right)
                .InclusiveBetween(-100, 100)
                .When(x => x.Right.HasValue);
        }
    }

    private class BlockValidator : AbstractValidator<Block>
    {
        public BlockValidator()
        {
            RuleFor(x => x.Width)
                .NotNull()
                .NotEmpty()
                .DependentRules(() =>
                    {
                        RuleFor(x => x.Width)
                            .GreaterThanOrEqualTo(1)
                            .LessThanOrEqualTo(12);
                    }
                );

            RuleFor(x => x.Margin)
                .SetValidator(new MarginValidator()!)
                .When(x => x.Margin != null);

            RuleFor(x => x.Type)
                .NotNull()
                .Must(new[] { "text", "image", "table" }.Contains);

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
                        .Must(new[] { "left", "center", "right" }.Contains);
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
            RuleFor(x => x.Content)
                .NotNull()
                .NotEmpty();
            RuleForEach(x => x.Content)
                .NotNull()
                .NotEmpty();
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