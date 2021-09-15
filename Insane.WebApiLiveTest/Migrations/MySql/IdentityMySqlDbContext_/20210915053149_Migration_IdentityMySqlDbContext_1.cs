using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insane.WebApiLiveTest.Migrations.MySql.IdentityMySqlDbContext_
{
    public partial class Migration_IdentityMySqlDbContext_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityOrganization",
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
                    table.PrimaryKey("P_IdentityOrganization_Id_DBB7E", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityPlatform",
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
                    table.PrimaryKey("P_IdentityPlatform_Id_A8230", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityRole",
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
                    table.PrimaryKey("P_IdentityRole_Id_5F896", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityUser",
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
                    table.PrimaryKey("P_IdentityUser_Id_D06A2", x => x.Id);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentityPermission",
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
                    table.PrimaryKey("P_IdentityPermission_Id_AA4E4", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentityPermission_OrganizationId_98097",
                        column: x => x.OrganizationId,
                        principalTable: "IdentityOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityPermission_RoleId_63EA2",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityPermission_UserId_C3DFE",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IdentitySession",
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
                    table.PrimaryKey("P_IdentitySession_Id_ECB1D", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentitySession_PermissionId_68ABC",
                        column: x => x.PermissionId,
                        principalTable: "IdentityPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentitySession_PlatformId_83F1D",
                        column: x => x.PlatformId,
                        principalTable: "IdentityPlatform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Insane:AutoIncrement", 10000)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "U_IdentityOrganization_Name_F13BC",
                table: "IdentityOrganization",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_OrganizationId_EF38A",
                table: "IdentityPermission",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_RoleId_C1B82",
                table: "IdentityPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_UserId_A10AB",
                table: "IdentityPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityPermission_UserId_RoleId_OrganizationId_800D6",
                table: "IdentityPermission",
                columns: new[] { "UserId", "RoleId", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityPlatform_Name_423B5",
                table: "IdentityPlatform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityPlatform_SecretKey_E10BA",
                table: "IdentityPlatform",
                column: "SecretKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityRole_Name_36689",
                table: "IdentityRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentitySession_PermissionId_EB569",
                table: "IdentitySession",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "I_IdentitySession_PlatformId_0A8EA",
                table: "IdentitySession",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_Jti_B9210",
                table: "IdentitySession",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_Key_45F50",
                table: "IdentitySession",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_RefreshToken_BC11C",
                table: "IdentitySession",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_TokenHash_15499",
                table: "IdentitySession",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Email_A5252",
                table: "IdentityUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Mobile_B52FB",
                table: "IdentityUser",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_UniqueId_E083B",
                table: "IdentityUser",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Username_85643",
                table: "IdentityUser",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentitySession");

            migrationBuilder.DropTable(
                name: "IdentityPermission");

            migrationBuilder.DropTable(
                name: "IdentityPlatform");

            migrationBuilder.DropTable(
                name: "IdentityOrganization");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
