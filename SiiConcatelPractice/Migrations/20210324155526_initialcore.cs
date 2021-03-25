using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiiConcatelPractice.Migrations
{
    public partial class initialcore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rebeldes");

            migrationBuilder.CreateTable(
                name: "Rebels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Planet = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rebels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rebels",
                columns: new[] { "Id", "Date", "Name", "Planet" },
                values: new object[] { 1, new DateTime(2021, 3, 24, 16, 55, 25, 658, DateTimeKind.Local).AddTicks(9810), "Rebel 1", "Mars" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rebels");

            migrationBuilder.CreateTable(
                name: "Rebeldes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Planeta = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rebeldes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Rebeldes",
                columns: new[] { "Id", "Fecha", "Nombre", "Planeta" },
                values: new object[] { 1, new DateTime(2021, 3, 24, 14, 1, 43, 627, DateTimeKind.Local).AddTicks(4556), "Rebelde 1", "Marte" });
        }
    }
}
