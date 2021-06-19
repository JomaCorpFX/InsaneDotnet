using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insane.Migrations.MySql.IdentityMySqlDbContext_
{
    public partial class Migration_IdentityMySqlDbContext_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Identity.Organization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddressLine1 = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AddresssLine2 = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoUri = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PIdentity.Organization__Id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Identity.Platform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecretKey = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoUri = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PIdentity.Platform__Id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Identity.Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PIdentity.Role__Id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Identity.User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UniqueId = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUsername = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Mobile = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EmailConfirmationCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmationDeadline = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    MobileConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MobileConfirmationCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileConfirmationDeadline = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LoginFailCount = table.Column<int>(type: "int", nullable: false),
                    LockoutUntil = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PIdentity.User__Id", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Identity.Permission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PIdentity.Permission__Id", x => x.Id);
                    table.ForeignKey(
                        name: "FIdentity.Permission__OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Identity.Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FIdentity.Permission__RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity.Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FIdentity.Permission__UserId",
                        column: x => x.UserId,
                        principalTable: "Identity.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Identity.Session",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PlatformId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    Jti = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TokenHash = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RefreshToken = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Key = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientUserAgent = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientFriendlyName = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientOS = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientIP = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientTimezone = table.Column<int>(type: "int", nullable: false),
                    ClientLatitude = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ClientLongitude = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Revoked = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PIdentity.Session__Id", x => x.Id);
                    table.ForeignKey(
                        name: "FIdentity.Session__PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Identity.Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FIdentity.Session__PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Identity.Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "UIdentity.Organization__Name",
                table: "Identity.Organization",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IIdentity.Permission__OrganizationId",
                table: "Identity.Permission",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IIdentity.Permission__RoleId",
                table: "Identity.Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IIdentity.Permission__UserId",
                table: "Identity.Permission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UIdentity.Permission__UserId_RoleId_OrganizationId",
                table: "Identity.Permission",
                columns: new[] { "UserId", "RoleId", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.Platform__Name",
                table: "Identity.Platform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.Platform__SecretKey",
                table: "Identity.Platform",
                column: "SecretKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.Role__Name",
                table: "Identity.Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IIdentity.Session__PermissionId",
                table: "Identity.Session",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IIdentity.Session__PlatformId",
                table: "Identity.Session",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "UIdentity.Session__Jti",
                table: "Identity.Session",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.Session__Key",
                table: "Identity.Session",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.Session__RefreshToken",
                table: "Identity.Session",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.Session__TokenHash",
                table: "Identity.Session",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.User__Email",
                table: "Identity.User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.User__Mobile",
                table: "Identity.User",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.User__UniqueId",
                table: "Identity.User",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIdentity.User__Username",
                table: "Identity.User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity.Session");

            migrationBuilder.DropTable(
                name: "Identity.Permission");

            migrationBuilder.DropTable(
                name: "Identity.Platform");

            migrationBuilder.DropTable(
                name: "Identity.Organization");

            migrationBuilder.DropTable(
                name: "Identity.Role");

            migrationBuilder.DropTable(
                name: "Identity.User");
        }
    }
}
