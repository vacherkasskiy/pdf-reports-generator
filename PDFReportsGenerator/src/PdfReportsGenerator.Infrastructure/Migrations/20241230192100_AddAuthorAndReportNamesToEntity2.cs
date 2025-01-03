using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfReportsGenerator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorAndReportNamesToEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "ReportTasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportName",
                table: "ReportTasks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "ReportTasks");

            migrationBuilder.DropColumn(
                name: "ReportName",
                table: "ReportTasks");
        }
    }
}
