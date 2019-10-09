using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
  public partial class AddContactsToOperation : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "HeadquarterContact",
          table: "Operation",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "PoliceContact",
          table: "Operation",
          nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "HeadquarterContact",
          table: "Operation");

      migrationBuilder.DropColumn(
          name: "PoliceContact",
          table: "Operation");
    }
  }
}