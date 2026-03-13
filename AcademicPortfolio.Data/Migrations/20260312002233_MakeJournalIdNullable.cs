using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcademicPortfolio.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeJournalIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicWorks_Journals_JournalId",
                table: "AcademicWorks");

            migrationBuilder.DropColumn(
                name: "AuthorRole",
                table: "AcademicWorks");

            migrationBuilder.DropColumn(
                name: "ImpactFactor",
                table: "AcademicWorks");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "AcademicWorks");

            migrationBuilder.AlterColumn<int>(
                name: "JournalId",
                table: "AcademicWorks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicWorks_Journals_JournalId",
                table: "AcademicWorks",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicWorks_Journals_JournalId",
                table: "AcademicWorks");

            migrationBuilder.AlterColumn<int>(
                name: "JournalId",
                table: "AcademicWorks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorRole",
                table: "AcademicWorks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ImpactFactor",
                table: "AcademicWorks",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "AcademicWorks",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicWorks_Journals_JournalId",
                table: "AcademicWorks",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
