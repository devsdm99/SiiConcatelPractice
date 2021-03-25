using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiiConcatelPractice.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rebeldes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Planeta = table.Column<string>(type: "TEXT", nullable: true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rebeldes");
        }
    }
}
