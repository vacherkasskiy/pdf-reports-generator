using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PdfReportsGenerator.Dal.Migrations
{
    /// <inheritdoc />
    public partial class UseStatusInsteadOfBodyInReportsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Reports");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reports",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reports");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
