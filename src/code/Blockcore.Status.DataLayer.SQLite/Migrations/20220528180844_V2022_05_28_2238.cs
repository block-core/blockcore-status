using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blockcore.status.DataLayer.SQLite.Migrations
{
    public partial class V2022_05_28_2238 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AppDataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FriendlyName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    XmlData = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    LogLevel = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Logger = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    Message = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    StateJson = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
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
                    table.PrimaryKey("PK_AppLogItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, collation: "NOCASE"),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, collation: "NOCASE"),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSqlCache",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 449, nullable: false, collation: "NOCASE"),
                    Value = table.Column<byte[]>(type: "BLOB", nullable: false),
                    ExpiresAtTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SlidingExpirationInSeconds = table.Column<long>(type: "INTEGER", nullable: true),
                    AbsoluteExpiration = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSqlCache", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true, collation: "NOCASE"),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true, collation: "NOCASE"),
                    PhotoFileName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true, collation: "NOCASE"),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastVisitDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsEmailPublic = table.Column<bool>(type: "INTEGER", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, collation: "NOCASE"),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, collation: "NOCASE"),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, collation: "NOCASE"),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true, collation: "NOCASE"),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

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
                name: "AppRoleClaims",
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
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
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
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE"),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    Name = table.Column<string>(type: "TEXT", nullable: false, collation: "NOCASE"),
                    CreatedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    CreatedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedByBrowserName = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true, collation: "NOCASE"),
                    ModifiedByIp = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true, collation: "NOCASE"),
                    ModifiedByUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true, collation: "NOCASE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserUsedPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HashedPassword = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false, collation: "NOCASE"),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_AppUserUsedPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserUsedPasswords_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
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
                name: "IX_AppDataProtectionKeys_FriendlyName",
                table: "AppDataProtectionKeys",
                column: "FriendlyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_RoleId",
                table: "AppRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_ExpiresAtTime",
                schema: "dbo",
                table: "AppSqlCache",
                column: "ExpiresAtTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_UserId",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserUsedPasswords_UserId",
                table: "AppUserUsedPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_GithubRepositoryId",
                table: "Releases",
                column: "GithubRepositoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_GithubOrganizationId",
                table: "Repositories",
                column: "GithubOrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDataProtectionKeys");

            migrationBuilder.DropTable(
                name: "AppLogItems");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppSqlCache",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "AppUserUsedPasswords");

            migrationBuilder.DropTable(
                name: "Releases");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
