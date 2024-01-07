using FluentAssertions;
using FluentValidation;
using PdfReportsGenerator.Bll.Models;
using PdfReportsGenerator.Bll.Validators;
using UnitTests.Fakers;
using Xunit;

namespace UnitTests.ValidatorTests;

public class ReportValidatorTests
{
    private readonly IValidator<Report> _validator = new ReportValidator();

    [Fact]
    public void Valid_ShouldSuccess()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        report.Blocks = new Block?[]
        {
            BlockFaker.GenerateTextBlocks().Single(),
            BlockFaker.GenerateImageBlocks().Single(),
            BlockFaker.GenerateTableBlocks().Single(),
        };

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeTrue();
    }

    [Fact]
    public void WithEmptyBlocks_ShouldSuccess()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        report.Blocks = new Block?[] { };

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeTrue();
    }

    [Fact]
    public void WithNullBlocks_ShouldFail()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        report.Blocks = null;

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeFalse();
    }

    [Fact]
    public void WithNullName_ShouldFail()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        report.Name = null;

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeFalse();
    }

    [Fact]
    public void WithNullBlock_ShouldFail()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        report.Blocks = new Block?[] {null};

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void WithNullType_ShouldFail()
    {
        // Arrange
        var textBlockReport = ReportFaker.Generate().Single();
        var imageBlockReport = ReportFaker.Generate().Single();
        var tableBlockReport = ReportFaker.Generate().Single();

        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        var imageBlock = BlockFaker.GenerateImageBlocks().Single();
        var tableBlock = BlockFaker.GenerateTableBlocks().Single();

        textBlock.Type = null;
        imageBlock.Type = null;
        tableBlock.Type = null;

        textBlockReport.Blocks = new Block[] {textBlock};
        imageBlockReport.Blocks = new Block[] {imageBlock};
        tableBlockReport.Blocks = new Block[] {tableBlock};

        // Act
        var result =
            _validator.Validate(textBlockReport).IsValid ||
            _validator.Validate(imageBlockReport).IsValid ||
            _validator.Validate(tableBlockReport).IsValid;

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WithInvalidType_ShouldFail()
    {
        // Arrange
        var textBlockReport = ReportFaker.Generate().Single();
        var imageBlockReport = ReportFaker.Generate().Single();
        var tableBlockReport = ReportFaker.Generate().Single();

        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        var imageBlock = BlockFaker.GenerateImageBlocks().Single();
        var tableBlock = BlockFaker.GenerateTableBlocks().Single();

        textBlock.Type = "invalid";
        imageBlock.Type = "invalid";
        tableBlock.Type = "invalid";

        textBlockReport.Blocks = new Block[] {textBlock};
        imageBlockReport.Blocks = new Block[] {imageBlock};
        tableBlockReport.Blocks = new Block[] {tableBlock};

        // Act
        var result =
            _validator.Validate(textBlockReport).IsValid ||
            _validator.Validate(imageBlockReport).IsValid ||
            _validator.Validate(tableBlockReport).IsValid;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void WithNullContent_ShouldFail()
    {
        // Arrange
        var textBlockReport = ReportFaker.Generate().Single();
        var imageBlockReport = ReportFaker.Generate().Single();
        var tableBlockReport = ReportFaker.Generate().Single();

        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        var imageBlock = BlockFaker.GenerateImageBlocks().Single();
        var tableBlock = BlockFaker.GenerateTableBlocks().Single();

        textBlock.Content = null;
        imageBlock.Content = null;
        tableBlock.Content = null;

        textBlockReport.Blocks = new Block[] {textBlock};
        imageBlockReport.Blocks = new Block[] {imageBlock};
        tableBlockReport.Blocks = new Block[] {tableBlock};

        // Act
        var result =
            _validator.Validate(textBlockReport).IsValid ||
            _validator.Validate(imageBlockReport).IsValid ||
            _validator.Validate(tableBlockReport).IsValid;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void WithNullLocation_ShouldFail()
    {
        // Arrange
        var textBlockReport = ReportFaker.Generate().Single();
        var imageBlockReport = ReportFaker.Generate().Single();
        var tableBlockReport = ReportFaker.Generate().Single();

        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        var imageBlock = BlockFaker.GenerateImageBlocks().Single();
        var tableBlock = BlockFaker.GenerateTableBlocks().Single();

        textBlock.Location = null;
        imageBlock.Location = null;
        tableBlock.Location = null;

        textBlockReport.Blocks = new Block[] {textBlock};
        imageBlockReport.Blocks = new Block[] {imageBlock};
        tableBlockReport.Blocks = new Block[] {tableBlock};

        // Act
        var result =
            _validator.Validate(textBlockReport).IsValid ||
            _validator.Validate(imageBlockReport).IsValid ||
            _validator.Validate(tableBlockReport).IsValid;

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WithInvalidLocation_ShouldFail()
    {
        // Arrange
        var textBlockReport = ReportFaker.Generate().Single();
        var imageBlockReport = ReportFaker.Generate().Single();
        var tableBlockReport = ReportFaker.Generate().Single();

        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        var imageBlock = BlockFaker.GenerateImageBlocks().Single();
        var tableBlock = BlockFaker.GenerateTableBlocks().Single();

        textBlock.Location = new Location
        {
            Left = -1,
            Right = 5
        };
        imageBlock.Location = new Location
        {
            Left = 2,
            Right = 16
        };
        tableBlock.Location = new Location
        {
            Left = 8,
            Right = 4
        };

        textBlockReport.Blocks = new Block[] {textBlock};
        imageBlockReport.Blocks = new Block[] {imageBlock};
        tableBlockReport.Blocks = new Block[] {tableBlock};

        // Act
        var result =
            _validator.Validate(textBlockReport).IsValid ||
            _validator.Validate(imageBlockReport).IsValid ||
            _validator.Validate(tableBlockReport).IsValid;

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WithNullStyle_ShouldFail()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        textBlock.Style = null;
        report.Blocks = new Block?[] {textBlock};

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void WithNullPosition_ShouldFail()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        textBlock.Style!.Position = null;
        report.Blocks = new Block?[] {textBlock};

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void WithInvalidPosition_ShouldFail()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        var textBlock = BlockFaker.GenerateTextBlocks().Single();
        textBlock.Style!.Position = "invalid";
        report.Blocks = new Block?[] {textBlock};

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void WithInvalidSize_ShouldFail()
    {
        // Arrange
        var lessThanOneReport = ReportFaker.Generate().Single();
        var greaterThanSixReport = ReportFaker.Generate().Single();
        
        var lessThanOneTextBlock = BlockFaker.GenerateTextBlocks().Single();
        var greaterThanSixTextBlock = BlockFaker.GenerateTextBlocks().Single();

        lessThanOneTextBlock.Style!.Size = 0;
        greaterThanSixTextBlock.Style!.Size = 7;
        
        lessThanOneReport.Blocks = new Block?[] {lessThanOneTextBlock};
        greaterThanSixReport.Blocks = new Block?[] {greaterThanSixTextBlock};

        // Act
        var result =
            _validator.Validate(lessThanOneReport).IsValid ||
            _validator.Validate(greaterThanSixReport).IsValid;
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void WithNullTableRow_ShouldFail()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        var tableBlock = BlockFaker.GenerateTableBlocks().Single();
        tableBlock.Content![0] = null;
        report.Blocks = new Block?[] {tableBlock};

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void WithNullTableCell_ShouldSuccess()
    {
        // Arrange
        var report = ReportFaker.Generate().Single();
        var tableBlock = BlockFaker.GenerateTableBlocks().Single();
        tableBlock.Content![0][0] = null;
        report.Blocks = new Block?[] {tableBlock};

        // Act & Assert
        _validator.Validate(report).IsValid.Should().BeTrue();
    }
}