using Microsoft.EntityFrameworkCore.Migrations;

namespace Olo42.SAROM.WebApp.Migrations.Operation
{
  public partial class AddMissingPeopleAndOperationLeaderToOperation : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "OperationLeader",
          table: "Operation",
          nullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "OperationId",
          table: "MissingPerson",
          nullable: true,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.CreateIndex(
          name: "IX_MissingPerson_OperationId",
          table: "MissingPerson",
          column: "OperationId");

      migrationBuilder.AddForeignKey(
          name: "FK_MissingPerson_Operation_OperationId",
          table: "MissingPerson",
          column: "OperationId",
          principalTable: "Operation",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_MissingPerson_Operation_OperationId",
          table: "MissingPerson");

      migrationBuilder.DropIndex(
          name: "IX_MissingPerson_OperationId",
          table: "MissingPerson");

      migrationBuilder.DropColumn(
          name: "OperationLeader",
          table: "Operation");

      migrationBuilder.AlterColumn<string>(
          name: "OperationId",
          table: "MissingPerson",
          nullable: true,
          oldClrType: typeof(string),
          oldNullable: true);
    }
  }
}