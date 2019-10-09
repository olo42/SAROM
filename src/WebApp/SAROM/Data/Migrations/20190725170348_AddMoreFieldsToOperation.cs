using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
  public partial class AddMoreFieldsToOperation : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "Headquarter",
          table: "Operation",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Number",
          table: "Operation",
          nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Headquarter",
          table: "Operation");

      migrationBuilder.DropColumn(
          name: "Number",
          table: "Operation");
    }
  }
}