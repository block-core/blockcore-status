using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blockcore.status.DataLayer.SQLite.Migrations
{
    public partial class V2022_05_17_1358 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscribersCount",
                table: "Repositories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PushedAt",
                table: "Repositories",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PushedAt",
                table: "Repositories",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubscribersCount",
                table: "Repositories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
