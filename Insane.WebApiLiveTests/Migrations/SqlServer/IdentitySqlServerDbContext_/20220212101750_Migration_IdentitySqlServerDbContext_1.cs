using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insane.WebApiLiveTests.Migrations.SqlServer.IdentitySqlServerDbContext_
{
    public partial class Migration_IdentitySqlServerDbContext_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "IdentityLog",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedExceptionStacktrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    LogoUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityIdentityRole_Id_B8DAC", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NormalizedUsername = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "varchar(1536)", unicode: false, maxLength: 1536, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Mobile = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    MobileConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailCount = table.Column<int>(type: "int", nullable: false),
                    LockoutDeadline = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ProfilePictureUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorSecretKey = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    NormalActionSecretKey = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    SecurityActionSecretKey = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityIdentityUser_Id_F4081", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityAccess",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityIdentityAccess_Id_AB31D", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentityIdentityAccess_RoleId_652C0",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityIdentityAccess_UserId_6435A",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityPlatform",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    AdminUserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiKey = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    LogoUri = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    InDevelopment = table.Column<bool>(type: "bit", nullable: false),
                    IsServerSide = table.Column<bool>(type: "bit", nullable: false),
                    RevokeTokenWhenLogout = table.Column<bool>(type: "bit", nullable: false),
                    RememberDeviceStrategy = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityIdentityPlatform_Id_0A89D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityPlatform_IdentityUser_AdminUserId",
                        column: x => x.AdminUserId,
                        principalSchema: "Identity",
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Type = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityIdentityUserClaim_Id_B38F2", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentityIdentityUserClaim_UserId_11415",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRecoveryCode",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityIdentityUserRecoveryCode_Id_7BE30", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentityIdentityUserRecoveryCode_UserId_45B90",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentitySession",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PlatformId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Jti = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    JwtHash = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    SessionKey = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    ClientDeviceUid = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    ClientUserAgent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ClientFriendlyName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ClientOS = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ClientTimezone = table.Column<int>(type: "int", nullable: false),
                    ClientLatitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ClientLongitude = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Revoked = table.Column<bool>(type: "bit", nullable: false),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    ActiveUntil = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("P_IdentityIdentitySession_Id_0F348", x => x.Id);
                    table.ForeignKey(
                        name: "F_IdentityIdentitySession_PlatformId_F7913",
                        column: x => x.PlatformId,
                        principalSchema: "Identity",
                        principalTable: "IdentityPlatform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "F_IdentityIdentitySession_UserId_C4F37",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "I_IdentityIdentityAccess_RoleId_EF364",
                schema: "Identity",
                table: "IdentityAccess",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityIdentityAccess_UserId_FDC64",
                schema: "Identity",
                table: "IdentityAccess",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityAccess_UserId_RoleId_841D3",
                schema: "Identity",
                table: "IdentityAccess",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentityIdentityPlatform_AdminUserId_CAD20",
                schema: "Identity",
                table: "IdentityPlatform",
                column: "AdminUserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityPlatform_ApiKey_452A7",
                schema: "Identity",
                table: "IdentityPlatform",
                column: "ApiKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityPlatform_Name_D45B0",
                schema: "Identity",
                table: "IdentityPlatform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityRole_Name_319E0",
                schema: "Identity",
                table: "IdentityRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentityIdentitySession_PlatformId_55045",
                schema: "Identity",
                table: "IdentitySession",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "I_IdentityIdentitySession_UserId_39419",
                schema: "Identity",
                table: "IdentitySession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentitySession_Jti_45260",
                schema: "Identity",
                table: "IdentitySession",
                column: "Jti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentitySession_JwtHash_9EA00",
                schema: "Identity",
                table: "IdentitySession",
                column: "JwtHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentitySession_RefreshToken_32CD0",
                schema: "Identity",
                table: "IdentitySession",
                column: "RefreshToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentitySession_SessionKey_2222A",
                schema: "Identity",
                table: "IdentitySession",
                column: "SessionKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityUser_Email_FB231",
                schema: "Identity",
                table: "IdentityUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityUser_Mobile_E8AA6",
                schema: "Identity",
                table: "IdentityUser",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityUser_NormalizedUsername_5DB6D",
                schema: "Identity",
                table: "IdentityUser",
                column: "NormalizedUsername",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityUser_Password_3679C",
                schema: "Identity",
                table: "IdentityUser",
                column: "Password",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityUser_Username_3635F",
                schema: "Identity",
                table: "IdentityUser",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentityIdentityUserClaim_UserId_B6704",
                schema: "Identity",
                table: "IdentityUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityUserClaim_UserId_Type_Value_EE3A5",
                schema: "Identity",
                table: "IdentityUserClaim",
                columns: new[] { "UserId", "Type", "Value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "I_IdentityIdentityUserRecoveryCode_UserId_283F8",
                schema: "Identity",
                table: "IdentityUserRecoveryCode",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "U_IdentityIdentityUserRecoveryCode_UserId_Value_C42A6",
                schema: "Identity",
                table: "IdentityUserRecoveryCode",
                columns: new[] { "UserId", "Value" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityAccess",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "IdentityLog",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "IdentitySession",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "IdentityUserClaim",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "IdentityUserRecoveryCode",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "IdentityRole",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "IdentityPlatform",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "IdentityUser",
                schema: "Identity");
        }
    }
}
