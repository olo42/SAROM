using Microsoft.EntityFrameworkCore.Migrations;

namespace Olo42.SAROM.WebApp.Data.Migrations
{
  public partial class AddOperationActions : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "OperationAction",
          columns: table => new
          {
            Id = table.Column<string>(nullable: false),
            Message = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_OperationAction", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "OperationAction");
    }
  }
}