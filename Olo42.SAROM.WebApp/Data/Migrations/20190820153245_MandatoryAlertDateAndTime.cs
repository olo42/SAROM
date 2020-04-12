using Microsoft.EntityFrameworkCore.Migrations;

namespace Olo42.SAROM.WebApp.Migrations.Operation
{
  public partial class MandatoryAlertDateAndTime : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
          name: "AlertTime",
          table: "Operation",
          nullable: false,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "AlertDate",
          table: "Operation",
          nullable: false,
          oldClrType: typeof(string),
          oldNullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
          name: "AlertTime",
          table: "Operation",
          nullable: true,
          oldClrType: typeof(string));

      migrationBuilder.AlterColumn<string>(
          name: "AlertDate",
          table: "Operation",
          nullable: true,
          oldClrType: typeof(string));
    }
  }
}