using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDbWithASP.Migrations
{
    /// <inheritdoc />
    public partial class Nukingdbagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 8, 19, 29, 18, 383, DateTimeKind.Local).AddTicks(4510));

            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 11, 8, 19, 29, 18, 383, DateTimeKind.Local).AddTicks(4540));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 8, 14, 59, 17, 394, DateTimeKind.Local).AddTicks(9820));

            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 11, 8, 14, 59, 17, 394, DateTimeKind.Local).AddTicks(9840));
        }
    }
}
