using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelAPI.Migrations
{
    /// <inheritdoc />
    public partial class FillHotelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Capacity", "City", "CreationDate", "Detail", "ImageURL", "Name", "Price", "UpdateDate" },
                values: new object[,]
                {
                    { 1, 100, "Medellin", new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(271), "Detail Test", "", "Test", 10000.5, new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(282) },
                    { 2, 200, "Bogotá", new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(285), "Detail Test", "", "Test 2", 20000.5, new DateTime(2024, 1, 25, 23, 49, 48, 861, DateTimeKind.Local).AddTicks(286) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
