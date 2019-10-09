using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
  public partial class AddSizesToUnits : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<int>(
          name: "AreaSeeker",
          table: "Unit",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "DebrisSearcher",
          table: "Unit",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "Helpers",
          table: "Unit",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "Mantrailer",
          table: "Unit",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.AddColumn<int>(
          name: "WaterLocators",
          table: "Unit",
          nullable: false,
          defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "AreaSeeker",
          table: "Unit");

      migrationBuilder.DropColumn(
          name: "DebrisSearcher",
          table: "Unit");

      migrationBuilder.DropColumn(
          name: "Helpers",
          table: "Unit");

      migrationBuilder.DropColumn(
          name: "Mantrailer",
          table: "Unit");

      migrationBuilder.DropColumn(
          name: "WaterLocators",
          table: "Unit");
    }
  }
}