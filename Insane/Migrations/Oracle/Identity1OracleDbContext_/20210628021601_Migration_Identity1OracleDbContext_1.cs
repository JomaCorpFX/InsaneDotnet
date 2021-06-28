using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insane.Migrations.Oracle.Identity1OracleDbContext_
{
    public partial class Migration_Identity1OracleDbContext_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "10000, 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    AddressLine1 = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    AddresssLine2 = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    LogoUri = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_Identity_Organization_Id_58009", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "10000, 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(512)", maxLength: 512, nullable: false),
                    SecretKey = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    LogoUri = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_Identity_Platform_Id_dffd7", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "10000, 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    Active = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_Identity_Role_Id_0808f", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "10000, 1"),
                    UniqueId = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Username = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    NormalizedUsername = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: true),
                    Mobile = table.Column<string>(type: "NVARCHAR2(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    Active = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    EmailConfirmationCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EmailConfirmationDeadline = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    MobileConfirmed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    MobileConfirmationCode = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    MobileConfirmationDeadline = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    LoginFailCount = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LockoutUntil = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_Identity_User_Id_8d3bc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "10000, 1"),
                    UserId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    RoleId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    OrganizationId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Active = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    Enabled = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_Identity_Permission_Id_a73aa", x => x.Id);
                    table.ForeignKey(
                        name: "F_Identity_Permission_OrganizationId_abfa1",
                        column: x => x.OrganizationId,
                        principalSchema: "Identity",
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_Identity_Permission_RoleId_3b3a4",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_Identity_Permission_UserId_7b5da",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "10000, 1"),
                    PlatformId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PermissionId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    Jti = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    TokenHash = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    RefreshToken = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    Key = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    ClientUserAgent = table.Column<string>(type: "NVARCHAR2(512)", maxLength: 512, nullable: false),
                    ClientFriendlyName = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    ClientOS = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
                    ClientIP = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false),
                    ClientTimezone = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ClientLatitude = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    ClientLongitude = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: false),
                    Revoked = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_Identity_Session_Id_c4735", x => x.Id);
                    table.ForeignKey(
                        name: "F_Identity_Session_PermissionId_16c18",
                        column: x => x.PermissionId,
                        principalSchema: "Identity",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_Identity_Session_PlatformId_45747",
                        column: x => x.PlatformId,
                        principalSchema: "Identity",
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "U_Identity_Organization_Name_9b40f",
                schema: "Identity",
                table: "Organization",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_Identity_Permission_OrganizationId_29aa7",
                schema: "Identity",
                table: "Permission",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "I_Identity_Permission_RoleId_19e55",
                schema: "Identity",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "I_Identity_Permission_UserId_777e2",
                schema: "Identity",
                table: "Permission",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_Identity_Permission_UserId_RoleId_OrganizationId_79cd0",
                schema: "Identity",
                table: "Permission",
                columns: new[] { "UserId", "RoleId", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_Platform_Name_5a64e",
                schema: "Identity",
                table: "Platform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_Platform_SecretKey_0823c",
                schema: "Identity",
                table: "Platform",
                column: "SecretKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_Role_Name_d6d1e",
                schema: "Identity",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_Identity_Session_PermissionId_bd69d",
                schema: "Identity",
                table: "Session",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "I_Identity_Session_PlatformId_3f620",
                schema: "Identity",
                table: "Session",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "U_Identity_Session_Jti_2617e",
                schema: "Identity",
                table: "Session",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_Session_Key_8e0f1",
                schema: "Identity",
                table: "Session",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_Session_RefreshToken_16772",
                schema: "Identity",
                table: "Session",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_Session_TokenHash_f7bdb",
                schema: "Identity",
                table: "Session",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_User_Email_aad66",
                schema: "Identity",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_User_Mobile_192e6",
                schema: "Identity",
                table: "User",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_User_UniqueId_86495",
                schema: "Identity",
                table: "User",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_Identity_User_Username_66295",
                schema: "Identity",
                table: "User",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Platform",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");
        }
    }
}
