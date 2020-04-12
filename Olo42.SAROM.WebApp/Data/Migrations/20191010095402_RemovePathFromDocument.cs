using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
    public partial class RemovePathFromDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Document");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Document",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Document",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Document");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Document",
                nullable: false,
                defaultValue: "");
        }
    }
}
