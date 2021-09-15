using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Insane.WebApiLiveTest.Migrations.PostgreSql.IdentityPostgreSqlDbContext_
{
    public partial class Migration_IdentityPostgreSqlDbContext_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityOrganization",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'10000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    AddressLine1 = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    AddresssLine2 = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    LogoUri = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityOrganization_Id_DBB7E", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityPlatform",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'10000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    SecretKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LogoUri = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityPlatform_Id_A8230", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'10000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityRole_Id_5F896", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'10000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniqueId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Username = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    NormalizedUsername = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    Mobile = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    EmailConfirmationCode = table.Column<string>(type: "text", nullable: false),
                    EmailConfirmationDeadline = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MobileConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    MobileConfirmationCode = table.Column<string>(type: "text", nullable: false),
                    MobileConfirmationDeadline = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LoginFailCount = table.Column<int>(type: "integer", nullable: false),
                    LockoutUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityUser_Id_D06A2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityPermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'10000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "IdentitySession",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'10000', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlatformId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false),
                    Jti = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TokenHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ClientUserAgent = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ClientFriendlyName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ClientOS = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ClientIP = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ClientTimezone = table.Column<int>(type: "integer", nullable: false),
                    ClientLatitude = table.Column<decimal>(type: "numeric", nullable: false),
                    ClientLongitude = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<bool>(type: "boolean", nullable: false)
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
                });

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
