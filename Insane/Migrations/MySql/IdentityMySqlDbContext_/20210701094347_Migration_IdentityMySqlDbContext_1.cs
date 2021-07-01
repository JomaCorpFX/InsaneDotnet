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
                    table.PrimaryKey("P_Identity.Organization_Id_57941", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
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
                    table.PrimaryKey("P_Identity.Platform_Id_536ad", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
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
                    table.PrimaryKey("P_Identity.Role_Id_8e181", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
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
                    table.PrimaryKey("P_Identity.User_Id_1c7f0", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
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
                    table.PrimaryKey("P_Identity.Permission_Id_b4baa", x => x.Id);
                    table.ForeignKey(
                        name: "F_Identity.Permission_OrganizationId_451b5",
                        column: x => x.OrganizationId,
                        principalTable: "Identity.Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_Identity.Permission_RoleId_c6b0c",
                        column: x => x.RoleId,
                        principalTable: "Identity.Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_Identity.Permission_UserId_e506e",
                        column: x => x.UserId,
                        principalTable: "Identity.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Insane:AutoIncrement", 10000)
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
                    table.PrimaryKey("P_Identity.Session_Id_056f1", x => x.Id);
                    table.ForeignKey(
                        name: "F_Identity.Session_PermissionId_61c86",
                        column: x => x.PermissionId,
                        principalTable: "Identity.Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_Identity.Session_PlatformId_2b879",
                        column: x => x.PlatformId,
                        principalTable: "Identity.Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "U_Identity.Organization_Name_24069",
                table: "Identity.Organization",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_Identity.Permission_OrganizationId_60c77",
                table: "Identity.Permission",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "I_Identity.Permission_RoleId_ef15b",
                table: "Identity.Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "I_Identity.Permission_UserId_8dda4",
                table: "Identity.Permission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_Identity.Permission_UserId_RoleId_OrganizationId_81337",
                table: "Identity.Permission",
                columns: new[] { "UserId", "RoleId", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.Platform_Name_ce1d4",
                table: "Identity.Platform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.Platform_SecretKey_fee4d",
                table: "Identity.Platform",
                column: "SecretKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.Role_Name_47a7c",
                table: "Identity.Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_Identity.Session_PermissionId_c39fd",
                table: "Identity.Session",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "I_Identity.Session_PlatformId_9ffe5",
                table: "Identity.Session",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "U_Identity.Session_Jti_991ea",
                table: "Identity.Session",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.Session_Key_e3810",
                table: "Identity.Session",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.Session_RefreshToken_6c5e8",
                table: "Identity.Session",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.Session_TokenHash_48896",
                table: "Identity.Session",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.User_Email_9daff",
                table: "Identity.User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.User_Mobile_75662",
                table: "Identity.User",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.User_UniqueId_7de64",
                table: "Identity.User",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity.User_Username_123f3",
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
