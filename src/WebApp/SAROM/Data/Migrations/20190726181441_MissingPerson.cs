using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SAROM.Migrations.Operation
{
    public partial class MissingPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissingPerson",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Ailments = table.Column<string>(nullable: true),
                    Clothes = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    EyesColour = table.Column<string>(nullable: true),
                    FurtherInformation = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    HairColor = table.Column<string>(nullable: true),
                    KnownPlaces = table.Column<string>(nullable: true),
                    Medications = table.Column<string>(nullable: true),
                    MissingSince = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    SkinType = table.Column<string>(nullable: true),
                    SpecialCharacteristics = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissingPerson", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissingPerson");
        }
    }
}
