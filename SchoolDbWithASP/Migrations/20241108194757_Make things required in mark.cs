using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDbWithASP.Migrations
{
    /// <inheritdoc />
    public partial class Makethingsrequiredinmark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 8, 19, 47, 57, 637, DateTimeKind.Local).AddTicks(6880));

            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 11, 8, 19, 47, 57, 637, DateTimeKind.Local).AddTicks(6890));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 11, 8, 19, 42, 16, 426, DateTimeKind.Local).AddTicks(9860));

            migrationBuilder.UpdateData(
                table: "Marks",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 11, 8, 19, 42, 16, 426, DateTimeKind.Local).AddTicks(9870));
        }
    }
}
