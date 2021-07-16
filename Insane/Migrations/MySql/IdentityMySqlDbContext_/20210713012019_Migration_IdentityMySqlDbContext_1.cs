using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insane.Migrations.MySql.IdentityMySqlDbContext_
{
    public partial class Migration_IdentityMySqlDbContext_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Insane");

            migrationBuilder.EnsureSchema(
                name: "Z");

            migrationBuilder.EnsureSchema(
                name: "SUPER");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityOrganization",
                schema: "Insane",
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
                    table.PrimaryKey("P_IdentityOrganization_Id_dbb7e", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityPlatform",
                schema: "Insane",
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
                    table.PrimaryKey("P_IdentityPlatform_Id_a8230", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                schema: "Z",
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
                    table.PrimaryKey("P_IdentityRole_Id_5f896", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                schema: "SUPER",
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
                    table.PrimaryKey("P_IdentityUser_Id_d06a2", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityPermission",
                schema: "Insane",
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
                    table.PrimaryKey("P_IdentityPermission_Id_aa4e4", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentityPermission_OrganizationId_98097",
                        column: x => x.OrganizationId,
                        principalSchema: "Insane",
                        principalTable: "IdentityOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityPermission_RoleId_63ea2",
                        column: x => x.RoleId,
                        principalSchema: "Z",
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityPermission_UserId_c3dfe",
                        column: x => x.UserId,
                        principalSchema: "SUPER",
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentitySession",
                schema: "Insane",
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
                    table.PrimaryKey("P_IdentitySession_Id_ecb1d", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentitySession_PermissionId_68abc",
                        column: x => x.PermissionId,
                        principalSchema: "Insane",
                        principalTable: "IdentityPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentitySession_PlatformId_83f1d",
                        column: x => x.PlatformId,
                        principalSchema: "Insane",
                        principalTable: "IdentityPlatform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "U_IdentityOrganization_Name_f13bc",
                schema: "Insane",
                table: "IdentityOrganization",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_OrganizationId_ef38a",
                schema: "Insane",
                table: "IdentityPermission",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_RoleId_c1b82",
                schema: "Insane",
                table: "IdentityPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_UserId_a10ab",
                schema: "Insane",
                table: "IdentityPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityPermission_UserId_RoleId_OrganizationId_800d6",
                schema: "Insane",
                table: "IdentityPermission",
                columns: new[] { "UserId", "RoleId", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityPlatform_Name_423b5",
                schema: "Insane",
                table: "IdentityPlatform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityPlatform_SecretKey_e10ba",
                schema: "Insane",
                table: "IdentityPlatform",
                column: "SecretKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityRole_Name_36689",
                schema: "Z",
                table: "IdentityRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentitySession_PermissionId_eb569",
                schema: "Insane",
                table: "IdentitySession",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "I_IdentitySession_PlatformId_0a8ea",
                schema: "Insane",
                table: "IdentitySession",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_Jti_b9210",
                schema: "Insane",
                table: "IdentitySession",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_Key_45f50",
                schema: "Insane",
                table: "IdentitySession",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_RefreshToken_bc11c",
                schema: "Insane",
                table: "IdentitySession",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_TokenHash_15499",
                schema: "Insane",
                table: "IdentitySession",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Email_a5252",
                schema: "SUPER",
                table: "IdentityUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Mobile_b52fb",
                schema: "SUPER",
                table: "IdentityUser",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_UniqueId_e083b",
                schema: "SUPER",
                table: "IdentityUser",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Username_85643",
                schema: "SUPER",
                table: "IdentityUser",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentitySession",
                schema: "Insane");

            migrationBuilder.DropTable(
                name: "IdentityPermission",
                schema: "Insane");

            migrationBuilder.DropTable(
                name: "IdentityPlatform",
                schema: "Insane");

            migrationBuilder.DropTable(
                name: "IdentityOrganization",
                schema: "Insane");

            migrationBuilder.DropTable(
                name: "IdentityRole",
                schema: "Z");

            migrationBuilder.DropTable(
                name: "IdentityUser",
                schema: "SUPER");
        }
    }
}
