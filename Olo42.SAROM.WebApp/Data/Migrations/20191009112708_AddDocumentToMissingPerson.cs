using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
    public partial class AddDocumentToMissingPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MissingPersonId",
                table: "Document",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_MissingPersonId",
                table: "Document",
                column: "MissingPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_MissingPerson_MissingPersonId",
                table: "Document",
                column: "MissingPersonId",
                principalTable: "MissingPerson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_MissingPerson_MissingPersonId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_MissingPersonId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "MissingPersonId",
                table: "Document");
        }
    }
}
