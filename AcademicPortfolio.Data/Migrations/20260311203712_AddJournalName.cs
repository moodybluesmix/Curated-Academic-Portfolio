using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJournalName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JournalName",
                table: "AcademicWorks",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JournalName",
                table: "AcademicWorks");
        }
    }
}
