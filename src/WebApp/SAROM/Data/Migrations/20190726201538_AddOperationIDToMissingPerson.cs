using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
  public partial class AddOperationIDToMissingPerson : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "OperationId",
          table: "MissingPerson",
          nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "OperationId",
          table: "MissingPerson");
    }
  }
}