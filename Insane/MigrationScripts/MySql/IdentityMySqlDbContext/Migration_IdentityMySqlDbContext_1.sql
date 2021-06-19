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
    CONSTRAINT `PIdentity.Organization__Id` PRIMARY KEY (`Id`)
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
    CONSTRAINT `PIdentity.Platform__Id` PRIMARY KEY (`Id`)
) CHARACTER SET utf8mb4;

CREATE TABLE `Identity.Role` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `Active` tinyint(1) NOT NULL,
    `Enabled` tinyint(1) NOT NULL,
    `ActiveUntil` datetime(6) NOT NULL,
    CONSTRAINT `PIdentity.Role__Id` PRIMARY KEY (`Id`)
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
    CONSTRAINT `PIdentity.User__Id` PRIMARY KEY (`Id`)
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
    CONSTRAINT `PIdentity.Permission__Id` PRIMARY KEY (`Id`),
    CONSTRAINT `FIdentity.Permission__OrganizationId` FOREIGN KEY (`OrganizationId`) REFERENCES `Identity.Organization` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FIdentity.Permission__RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Identity.Role` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FIdentity.Permission__UserId` FOREIGN KEY (`UserId`) REFERENCES `Identity.User` (`Id`) ON DELETE RESTRICT
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
    CONSTRAINT `PIdentity.Session__Id` PRIMARY KEY (`Id`),
    CONSTRAINT `FIdentity.Session__PermissionId` FOREIGN KEY (`PermissionId`) REFERENCES `Identity.Permission` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FIdentity.Session__PlatformId` FOREIGN KEY (`PlatformId`) REFERENCES `Identity.Platform` (`Id`) ON DELETE RESTRICT
) CHARACTER SET utf8mb4;

CREATE UNIQUE INDEX `UIdentity.Organization__Name` ON `Identity.Organization` (`Name`);

CREATE INDEX `IIdentity.Permission__OrganizationId` ON `Identity.Permission` (`OrganizationId`);

CREATE INDEX `IIdentity.Permission__RoleId` ON `Identity.Permission` (`RoleId`);

CREATE INDEX `IIdentity.Permission__UserId` ON `Identity.Permission` (`UserId`);

CREATE UNIQUE INDEX `UIdentity.Permission__UserId_RoleId_OrganizationId` ON `Identity.Permission` (`UserId`, `RoleId`, `OrganizationId`);

CREATE UNIQUE INDEX `UIdentity.Platform__Name` ON `Identity.Platform` (`Name`);

CREATE UNIQUE INDEX `UIdentity.Platform__SecretKey` ON `Identity.Platform` (`SecretKey`);

CREATE UNIQUE INDEX `UIdentity.Role__Name` ON `Identity.Role` (`Name`);

CREATE INDEX `IIdentity.Session__PermissionId` ON `Identity.Session` (`PermissionId`);

CREATE INDEX `IIdentity.Session__PlatformId` ON `Identity.Session` (`PlatformId`);

CREATE UNIQUE INDEX `UIdentity.Session__Jti` ON `Identity.Session` (`Jti`);

CREATE UNIQUE INDEX `UIdentity.Session__Key` ON `Identity.Session` (`Key`);

CREATE UNIQUE INDEX `UIdentity.Session__RefreshToken` ON `Identity.Session` (`RefreshToken`);

CREATE UNIQUE INDEX `UIdentity.Session__TokenHash` ON `Identity.Session` (`TokenHash`);

CREATE UNIQUE INDEX `UIdentity.User__Email` ON `Identity.User` (`Email`);

CREATE UNIQUE INDEX `UIdentity.User__Mobile` ON `Identity.User` (`Mobile`);

CREATE UNIQUE INDEX `UIdentity.User__UniqueId` ON `Identity.User` (`UniqueId`);

CREATE UNIQUE INDEX `UIdentity.User__Username` ON `Identity.User` (`Username`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210619110801_Migration_IdentityMySqlDbContext_1', '5.0.7');

COMMIT;

