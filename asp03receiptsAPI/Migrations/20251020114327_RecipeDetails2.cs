using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asp03receiptsAPI.Migrations
{
    /// <inheritdoc />
    public partial class RecipeDetails2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2025, 10, 20, 13, 43, 26, 949, DateTimeKind.Local).AddTicks(3903), "Jednoduchý recept na míchaná vajíčka." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Description" },
                values: new object[] { new DateTime(2025, 10, 20, 11, 41, 9, 343, DateTimeKind.Utc).AddTicks(7998), null });
        }
    }
}
