using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiiConcatelPractice.Migrations
{
    public partial class initialcoree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rebels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 3, 24, 16, 55, 41, 684, DateTimeKind.Local).AddTicks(1499));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rebels",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2021, 3, 24, 16, 55, 25, 658, DateTimeKind.Local).AddTicks(9810));
        }
    }
}
