using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Data.Migrations
{
    public partial class AddPersonsUnitsOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OperationId",
                table: "OperationAction",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    ClosingReport = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    GroupLeader = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PagerNumber = table.Column<string>(nullable: true),
                    OperationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_Operation_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Dog = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UnitId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperationAction_OperationId",
                table: "OperationAction",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UnitId",
                table: "Person",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_OperationId",
                table: "Unit",
                column: "OperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationAction_Operation_OperationId",
                table: "OperationAction",
                column: "OperationId",
                principalTable: "Operation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationAction_Operation_OperationId",
                table: "OperationAction");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_OperationAction_OperationId",
                table: "OperationAction");

            migrationBuilder.DropColumn(
                name: "OperationId",
                table: "OperationAction");
        }
    }
}
