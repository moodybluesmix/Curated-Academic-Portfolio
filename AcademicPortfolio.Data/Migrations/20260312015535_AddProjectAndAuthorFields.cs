using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectAndAuthorFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorRole",
                table: "AcademicWorks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "AcademicWorks",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorRole",
                table: "AcademicWorks");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "AcademicWorks");
        }
    }
}
