using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp03receiptsAPI.Migrations
{
    /// <inheritdoc />
    public partial class RecipeDetails3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 20, 13, 46, 34, 857, DateTimeKind.Local).AddTicks(5106));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 10, 20, 13, 43, 26, 949, DateTimeKind.Local).AddTicks(3903));
        }
    }
}
