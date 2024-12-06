using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfReportsGenerator.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkColumnToReportsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Reports");
        }
    }
}
