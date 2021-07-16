ALTER DATABASE CHARACTER SET utf8mb4;
CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `Insane.IdentityOrganization` (
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
        CONSTRAINT `P_IdentityOrganization_Id_dbb7e` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4 AUTO_INCREMENT 10000;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `Insane.IdentityPlatform` (
        `Id` bigint NOT NULL AUTO_INCREMENT,
        `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `Description` varchar(512) CHARACTER SET utf8mb4 NOT NULL,
        `SecretKey` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `LogoUri` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
        `Active` tinyint(1) NOT NULL,
        `Enabled` tinyint(1) NOT NULL,
        `ActiveUntil` datetime(6) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        CONSTRAINT `P_IdentityPlatform_Id_a8230` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4 AUTO_INCREMENT 10000;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `Z.IdentityRole` (
        `Id` bigint NOT NULL AUTO_INCREMENT,
        `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `Active` tinyint(1) NOT NULL,
        `Enabled` tinyint(1) NOT NULL,
        `ActiveUntil` datetime(6) NOT NULL,
        CONSTRAINT `P_IdentityRole_Id_5f896` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4 AUTO_INCREMENT 10000;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `SUPER.IdentityUser` (
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
        CONSTRAINT `P_IdentityUser_Id_d06a2` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4 AUTO_INCREMENT 10000;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `Insane.IdentityPermission` (
        `Id` bigint NOT NULL AUTO_INCREMENT,
        `UserId` bigint NOT NULL,
        `RoleId` bigint NOT NULL,
        `OrganizationId` bigint NOT NULL,
        `Active` tinyint(1) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `ActiveUntil` datetime(6) NOT NULL,
        `Enabled` tinyint(1) NOT NULL,
        CONSTRAINT `P_IdentityPermission_Id_aa4e4` PRIMARY KEY (`Id`),
        CONSTRAINT `F_IdentityPermission_OrganizationId_98097` FOREIGN KEY (`OrganizationId`) REFERENCES `Insane.IdentityOrganization` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `F_IdentityPermission_RoleId_63ea2` FOREIGN KEY (`RoleId`) REFERENCES `Z.IdentityRole` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `F_IdentityPermission_UserId_c3dfe` FOREIGN KEY (`UserId`) REFERENCES `SUPER.IdentityUser` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4 AUTO_INCREMENT 10000;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `Insane.IdentitySession` (
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
        CONSTRAINT `P_IdentitySession_Id_ecb1d` PRIMARY KEY (`Id`),
        CONSTRAINT `F_IdentitySession_PermissionId_68abc` FOREIGN KEY (`PermissionId`) REFERENCES `Insane.IdentityPermission` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `F_IdentitySession_PlatformId_83f1d` FOREIGN KEY (`PlatformId`) REFERENCES `Insane.IdentityPlatform` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4 AUTO_INCREMENT 10000;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityOrganization_Name_f13bc` ON `Insane.IdentityOrganization` (`Name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentityPermission_OrganizationId_ef38a` ON `Insane.IdentityPermission` (`OrganizationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentityPermission_RoleId_c1b82` ON `Insane.IdentityPermission` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentityPermission_UserId_a10ab` ON `Insane.IdentityPermission` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityPermission_UserId_RoleId_OrganizationId_800d6` ON `Insane.IdentityPermission` (`UserId`, `RoleId`, `OrganizationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityPlatform_Name_423b5` ON `Insane.IdentityPlatform` (`Name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityPlatform_SecretKey_e10ba` ON `Insane.IdentityPlatform` (`SecretKey`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityRole_Name_36689` ON `Z.IdentityRole` (`Name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentitySession_PermissionId_eb569` ON `Insane.IdentitySession` (`PermissionId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentitySession_PlatformId_0a8ea` ON `Insane.IdentitySession` (`PlatformId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_Jti_b9210` ON `Insane.IdentitySession` (`Jti`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_Key_45f50` ON `Insane.IdentitySession` (`Key`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_RefreshToken_bc11c` ON `Insane.IdentitySession` (`RefreshToken`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_TokenHash_15499` ON `Insane.IdentitySession` (`TokenHash`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_Email_a5252` ON `SUPER.IdentityUser` (`Email`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_Mobile_b52fb` ON `SUPER.IdentityUser` (`Mobile`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_UniqueId_e083b` ON `SUPER.IdentityUser` (`UniqueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_Username_85643` ON `SUPER.IdentityUser` (`Username`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210713012019_Migration_IdentityMySqlDbContext_1') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20210713012019_Migration_IdentityMySqlDbContext_1', '5.0.7');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

