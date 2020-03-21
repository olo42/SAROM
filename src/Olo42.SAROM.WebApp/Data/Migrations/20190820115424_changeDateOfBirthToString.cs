using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Olo42.SAROM.WebApp.Migrations.Operation
{
  public partial class changeDateOfBirthToString : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_OperationAction_Operation_OperationId",
          table: "OperationAction");

      migrationBuilder.AlterColumn<string>(
          name: "OperationId",
          table: "OperationAction",
          nullable: false,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "DateOfBirth",
          table: "MissingPerson",
          nullable: true,
          oldClrType: typeof(DateTime));

      migrationBuilder.AddForeignKey(
          name: "FK_OperationAction_Operation_OperationId",
          table: "OperationAction",
          column: "OperationId",
          principalTable: "Operation",
          principalColumn: "Id",
          onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_OperationAction_Operation_OperationId",
          table: "OperationAction");

      migrationBuilder.AlterColumn<string>(
          name: "OperationId",
          table: "OperationAction",
          nullable: true,
          oldClrType: typeof(string));

      migrationBuilder.AlterColumn<DateTime>(
          name: "DateOfBirth",
          table: "MissingPerson",
          nullable: false,
          oldClrType: typeof(string),
          oldNullable: true);

      migrationBuilder.AddForeignKey(
          name: "FK_OperationAction_Operation_OperationId",
          table: "OperationAction",
          column: "OperationId",
          principalTable: "Operation",
          principalColumn: "Id",
          onDelete: ReferentialAction.Restrict);
    }
  }
}