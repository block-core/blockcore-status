using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blockcore.status.DataLayer.SQLite.Migrations
{
    public partial class V2022_05_15_1727 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrganizationName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RepositoryName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OrganizationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repositories_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_OrganizationId",
                table: "Repositories",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
