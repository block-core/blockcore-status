using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blockcore.status.DataLayer.SQLite.Migrations
{
    public partial class V2022_05_17_2013 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    GithubOrganizationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ReposUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    EventsUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    HooksUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    IssuesUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    MembersUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    PublicMembersUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Description = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Name = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Company = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Blog = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Location = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Email = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasOrganizationProjects = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasRepositoryProjects = table.Column<bool>(type: "INTEGER", nullable: false),
                    PublicRepos = table.Column<int>(type: "INTEGER", nullable: false),
                    PublicGists = table.Column<int>(type: "INTEGER", nullable: false),
                    Followers = table.Column<int>(type: "INTEGER", nullable: false),
                    Following = table.Column<int>(type: "INTEGER", nullable: false),
                    HtmlUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    LatestDataUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.GithubOrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    GithubRepositoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    HtmlUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CloneUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    GitUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    SshUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    SvnUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    MirrorUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    NodeId = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Name = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    FullName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Description = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Homepage = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Language = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ForksCount = table.Column<int>(type: "INTEGER", nullable: false),
                    StargazersCount = table.Column<int>(type: "INTEGER", nullable: false),
                    WatchersCount = table.Column<int>(type: "INTEGER", nullable: false),
                    DefaultBranch = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    OpenIssuesCount = table.Column<int>(type: "INTEGER", nullable: false),
                    PushedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HasIssues = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasWiki = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasDownloads = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasPages = table.Column<bool>(type: "INTEGER", nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSelect = table.Column<bool>(type: "INTEGER", nullable: false),
                    LatestDataUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GithubOrganizationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.GithubRepositoryId);
                    table.ForeignKey(
                        name: "FK_Repositories_Organizations_GithubOrganizationId",
                        column: x => x.GithubOrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "GithubOrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Releases",
                columns: table => new
                {
                    GithubReleaseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    AssetsUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    UploadUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    HtmlUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    NodeId = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    TagName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    TargetCommitish = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Name = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Draft = table.Column<bool>(type: "INTEGER", nullable: false),
                    Prerelease = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TarballUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ZipballUrl = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Body = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    LatestDataUpdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GithubRepositoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.GithubReleaseId);
                    table.ForeignKey(
                        name: "FK_Releases_Repositories_GithubRepositoryId",
                        column: x => x.GithubRepositoryId,
                        principalTable: "Repositories",
                        principalColumn: "GithubRepositoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Releases_GithubRepositoryId",
                table: "Releases",
                column: "GithubRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_GithubOrganizationId",
                table: "Repositories",
                column: "GithubOrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Releases");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false, collation: "NOCASE"),
                    Title = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false, collation: "NOCASE"),
                    Price = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }
    }
}
