using Microsoft.EntityFrameworkCore.Migrations;

namespace Olo42.SAROM.WebApp.Data.Migrations
{
  public partial class AddFieldsToOperations : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "AlertDate",
          table: "Operation",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "AlertTime",
          table: "Operation",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Name",
          table: "Operation",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "State3",
          table: "Operation",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "State4",
          table: "Operation",
          nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "AlertDate",
          table: "Operation");

      migrationBuilder.DropColumn(
          name: "AlertTime",
          table: "Operation");

      migrationBuilder.DropColumn(
          name: "Name",
          table: "Operation");

      migrationBuilder.DropColumn(
          name: "State3",
          table: "Operation");

      migrationBuilder.DropColumn(
          name: "State4",
          table: "Operation");
    }
  }
}