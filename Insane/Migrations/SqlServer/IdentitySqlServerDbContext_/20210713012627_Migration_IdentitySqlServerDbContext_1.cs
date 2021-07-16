using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insane.Migrations.SqlServer.IdentitySqlServerDbContext_
{
    public partial class Migration_IdentitySqlServerDbContext_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityOrganization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    AddresssLine2 = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    LogoUri = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityOrganization_Id_dbb7e", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityPlatform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    LogoUri = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityPlatform_Id_a8230", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityRole_Id_5f896", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    UniqueId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NormalizedUsername = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    EmailConfirmationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmationDeadline = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    MobileConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    MobileConfirmationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileConfirmationDeadline = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LoginFailCount = table.Column<int>(type: "int", nullable: false),
                    LockoutUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityUser_Id_d06a2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityPermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityPermission_Id_aa4e4", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentityPermission_OrganizationId_98097",
                        column: x => x.OrganizationId,
                        principalTable: "IdentityOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityPermission_RoleId_63ea2",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityPermission_UserId_c3dfe",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentitySession",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "10000, 1"),
                    PlatformId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    Jti = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TokenHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ClientUserAgent = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    ClientFriendlyName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ClientOS = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ClientTimezone = table.Column<int>(type: "int", nullable: false),
                    ClientLatitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientLongitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Revoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentitySession_Id_ecb1d", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentitySession_PermissionId_68abc",
                        column: x => x.PermissionId,
                        principalTable: "IdentityPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentitySession_PlatformId_83f1d",
                        column: x => x.PlatformId,
                        principalTable: "IdentityPlatform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "U_IdentityOrganization_Name_f13bc",
                table: "IdentityOrganization",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_OrganizationId_ef38a",
                table: "IdentityPermission",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_RoleId_c1b82",
                table: "IdentityPermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityPermission_UserId_a10ab",
                table: "IdentityPermission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityPermission_UserId_RoleId_OrganizationId_800d6",
                table: "IdentityPermission",
                columns: new[] { "UserId", "RoleId", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityPlatform_Name_423b5",
                table: "IdentityPlatform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityPlatform_SecretKey_e10ba",
                table: "IdentityPlatform",
                column: "SecretKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityRole_Name_36689",
                table: "IdentityRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentitySession_PermissionId_eb569",
                table: "IdentitySession",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "I_IdentitySession_PlatformId_0a8ea",
                table: "IdentitySession",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_Jti_b9210",
                table: "IdentitySession",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_Key_45f50",
                table: "IdentitySession",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_RefreshToken_bc11c",
                table: "IdentitySession",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentitySession_TokenHash_15499",
                table: "IdentitySession",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Email_a5252",
                table: "IdentityUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_Mobile_b52fb",
                table: "IdentityUser",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityUser_UniqueId_e083b",
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
