using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlockcoreStatus.DataLayer.SQLite.Migrations
{
    public partial class V2022_06_19_1332 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockcoreIndexers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Country = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Region = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    RegionName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    City = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Zip = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Lat = table.Column<double>(type: "REAL", nullable: false),
                    Lon = table.Column<double>(type: "REAL", nullable: false),
                    Timezone = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Isp = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Org = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Query = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    FailedPings = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockcoreIndexers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockcoreIndexers");
        }
    }
}
