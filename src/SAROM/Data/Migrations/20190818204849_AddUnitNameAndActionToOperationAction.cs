using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
    public partial class AddUnitNameAndActionToOperationAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "OperationAction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "OperationAction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "OperationAction");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "OperationAction");
        }
    }
}
