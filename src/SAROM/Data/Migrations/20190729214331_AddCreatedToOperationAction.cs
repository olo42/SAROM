using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Olo42.SAROM.WebApp.Migrations.Operation
{
    public partial class AddCreatedToOperationAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "OperationAction",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "OperationAction");
        }
    }
}
