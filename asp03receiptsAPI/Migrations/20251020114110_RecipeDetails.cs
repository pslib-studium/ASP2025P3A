using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp03receiptsAPI.Migrations
{
    /// <inheritdoc />
    public partial class RecipeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2025, 10, 20, 11, 41, 9, 343, DateTimeKind.Utc).AddTicks(7998), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Recipes");
        }
    }
}
