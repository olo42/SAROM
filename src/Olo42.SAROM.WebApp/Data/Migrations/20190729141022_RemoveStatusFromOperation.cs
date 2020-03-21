using Microsoft.EntityFrameworkCore.Migrations;

namespace Olo42.SAROM.WebApp.Migrations.Operation
{
  public partial class RemoveStatusFromOperation : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "State3",
          table: "Operation");

      migrationBuilder.DropColumn(
          name: "State4",
          table: "Operation");

      migrationBuilder.AlterColumn<string>(
          name: "Name",
          table: "MissingPerson",
          nullable: false,
          oldClrType: typeof(string),
          oldNullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "State3",
          table: "Operation",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "State4",
          table: "Operation",
          nullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "Name",
          table: "MissingPerson",
          nullable: true,
          oldClrType: typeof(string));
    }
  }
}