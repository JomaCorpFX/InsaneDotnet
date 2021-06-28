ALTER DATABASE CHARACTER SET utf8mb4;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Identity.Organization` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `AddressLine1` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `AddresssLine2` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Phone` varchar(16) CHARACTER SET utf8mb4 NOT NULL,
    `LogoUri` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
    `Active` tinyint(1) NOT NULL,
    `Enabled` tinyint(1) NOT NULL,
    `ActiveUntil` datetime(6) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `P_Identity.Organization_Id_57941` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Identity.Platform` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Description` varchar(512) CHARACTER SET utf8mb4 NOT NULL,
    `SecretKey` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `LogoUri` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
    `Active` tinyint(1) NOT NULL,
    `Enabled` tinyint(1) NOT NULL,
    `ActiveUntil` datetime(6) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    CONSTRAINT `P_Identity.Platform_Id_536ad` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Identity.Role` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `Active` tinyint(1) NOT NULL,
    `Enabled` tinyint(1) NOT NULL,
    `ActiveUntil` datetime(6) NOT NULL,
    CONSTRAINT `P_Identity.Role_Id_8e181` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Identity.User` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `UniqueId` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Username` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `NormalizedUsername` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Password` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `NormalizedEmail` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Phone` varchar(16) CHARACTER SET utf8mb4 NULL,
    `Mobile` varchar(16) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `Enabled` tinyint(1) NOT NULL,
    `Active` tinyint(1) NOT NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `EmailConfirmationCode` longtext CHARACTER SET utf8mb4 NOT NULL,
    `EmailConfirmationDeadline` datetime(6) NOT NULL,
    `MobileConfirmed` tinyint(1) NOT NULL,
    `MobileConfirmationCode` longtext CHARACTER SET utf8mb4 NOT NULL,
    `MobileConfirmationDeadline` datetime(6) NOT NULL,
    `LoginFailCount` int NOT NULL,
    `LockoutUntil` datetime(6) NOT NULL,
    CONSTRAINT `P_Identity.User_Id_1c7f0` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Identity.Permission` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `UserId` bigint NOT NULL,
    `RoleId` bigint NOT NULL,
    `OrganizationId` bigint NOT NULL,
    `Active` tinyint(1) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ActiveUntil` datetime(6) NOT NULL,
    `Enabled` tinyint(1) NOT NULL,
    CONSTRAINT `P_Identity.Permission_Id_b4baa` PRIMARY KEY (`Id`),
    CONSTRAINT `F_Identity.Permission_OrganizationId_451b5` FOREIGN KEY (`OrganizationId`) REFERENCES `Identity.Organization` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `F_Identity.Permission_RoleId_c6b0c` FOREIGN KEY (`RoleId`) REFERENCES `Identity.Role` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `F_Identity.Permission_UserId_e506e` FOREIGN KEY (`UserId`) REFERENCES `Identity.User` (`Id`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE TABLE `Identity.Session` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `PlatformId` bigint NOT NULL,
    `PermissionId` bigint NOT NULL,
    `Jti` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `TokenHash` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `RefreshToken` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `Key` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `ClientUserAgent` varchar(512) CHARACTER SET utf8mb4 NOT NULL,
    `ClientFriendlyName` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `ClientOS` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `ClientIP` varchar(64) CHARACTER SET utf8mb4 NOT NULL,
    `ClientTimezone` int NOT NULL,
    `ClientLatitude` decimal(65,30) NOT NULL,
    `ClientLongitude` decimal(65,30) NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `ExpiresAt` datetime(6) NOT NULL,
    `Revoked` tinyint(1) NOT NULL,
    CONSTRAINT `P_Identity.Session_Id_056f1` PRIMARY KEY (`Id`),
    CONSTRAINT `F_Identity.Session_PermissionId_61c86` FOREIGN KEY (`PermissionId`) REFERENCES `Identity.Permission` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `F_Identity.Session_PlatformId_2b879` FOREIGN KEY (`PlatformId`) REFERENCES `Identity.Platform` (`Id`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE UNIQUE INDEX `U_Identity.Organization_Name_24069` ON `Identity.Organization` (`Name`);

CREATE INDEX `I_Identity.Permission_OrganizationId_60c77` ON `Identity.Permission` (`OrganizationId`);

CREATE INDEX `I_Identity.Permission_RoleId_ef15b` ON `Identity.Permission` (`RoleId`);

CREATE INDEX `I_Identity.Permission_UserId_8dda4` ON `Identity.Permission` (`UserId`);

CREATE UNIQUE INDEX `U_Identity.Permission_UserId_RoleId_OrganizationId_81337` ON `Identity.Permission` (`UserId`, `RoleId`, `OrganizationId`);

CREATE UNIQUE INDEX `U_Identity.Platform_Name_ce1d4` ON `Identity.Platform` (`Name`);

CREATE UNIQUE INDEX `U_Identity.Platform_SecretKey_fee4d` ON `Identity.Platform` (`SecretKey`);

CREATE UNIQUE INDEX `U_Identity.Role_Name_47a7c` ON `Identity.Role` (`Name`);

CREATE INDEX `I_Identity.Session_PermissionId_c39fd` ON `Identity.Session` (`PermissionId`);

CREATE INDEX `I_Identity.Session_PlatformId_9ffe5` ON `Identity.Session` (`PlatformId`);

CREATE UNIQUE INDEX `U_Identity.Session_Jti_991ea` ON `Identity.Session` (`Jti`);

CREATE UNIQUE INDEX `U_Identity.Session_Key_e3810` ON `Identity.Session` (`Key`);

CREATE UNIQUE INDEX `U_Identity.Session_RefreshToken_6c5e8` ON `Identity.Session` (`RefreshToken`);

CREATE UNIQUE INDEX `U_Identity.Session_TokenHash_48896` ON `Identity.Session` (`TokenHash`);

CREATE UNIQUE INDEX `U_Identity.User_Email_9daff` ON `Identity.User` (`Email`);

CREATE UNIQUE INDEX `U_Identity.User_Mobile_75662` ON `Identity.User` (`Mobile`);

CREATE UNIQUE INDEX `U_Identity.User_UniqueId_7de64` ON `Identity.User` (`UniqueId`);

CREATE UNIQUE INDEX `U_Identity.User_Username_123f3` ON `Identity.User` (`Username`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210628003620_Migration_Identity1MySqlDbContext_1', '5.0.7');

COMMIT;

