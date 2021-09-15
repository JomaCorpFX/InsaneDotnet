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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `IdentityOrganization` (
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
        CONSTRAINT `P_IdentityOrganization_Id_DBB7E` PRIMARY KEY (`Id`)
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `IdentityPlatform` (
        `Id` bigint NOT NULL AUTO_INCREMENT,
        `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `Description` varchar(512) CHARACTER SET utf8mb4 NOT NULL,
        `SecretKey` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `LogoUri` varchar(256) CHARACTER SET utf8mb4 NOT NULL,
        `Active` tinyint(1) NOT NULL,
        `Enabled` tinyint(1) NOT NULL,
        `ActiveUntil` datetime(6) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        CONSTRAINT `P_IdentityPlatform_Id_A8230` PRIMARY KEY (`Id`)
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `IdentityRole` (
        `Id` bigint NOT NULL AUTO_INCREMENT,
        `Name` varchar(128) CHARACTER SET utf8mb4 NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `Active` tinyint(1) NOT NULL,
        `Enabled` tinyint(1) NOT NULL,
        `ActiveUntil` datetime(6) NOT NULL,
        CONSTRAINT `P_IdentityRole_Id_5F896` PRIMARY KEY (`Id`)
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `IdentityUser` (
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
        CONSTRAINT `P_IdentityUser_Id_D06A2` PRIMARY KEY (`Id`)
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `IdentityPermission` (
        `Id` bigint NOT NULL AUTO_INCREMENT,
        `UserId` bigint NOT NULL,
        `RoleId` bigint NOT NULL,
        `OrganizationId` bigint NOT NULL,
        `Active` tinyint(1) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `ActiveUntil` datetime(6) NOT NULL,
        `Enabled` tinyint(1) NOT NULL,
        CONSTRAINT `P_IdentityPermission_Id_AA4E4` PRIMARY KEY (`Id`),
        CONSTRAINT `F_IdentityPermission_OrganizationId_98097` FOREIGN KEY (`OrganizationId`) REFERENCES `IdentityOrganization` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `F_IdentityPermission_RoleId_63EA2` FOREIGN KEY (`RoleId`) REFERENCES `IdentityRole` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `F_IdentityPermission_UserId_C3DFE` FOREIGN KEY (`UserId`) REFERENCES `IdentityUser` (`Id`) ON DELETE RESTRICT
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE TABLE `IdentitySession` (
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
        CONSTRAINT `P_IdentitySession_Id_ECB1D` PRIMARY KEY (`Id`),
        CONSTRAINT `F_IdentitySession_PermissionId_68ABC` FOREIGN KEY (`PermissionId`) REFERENCES `IdentityPermission` (`Id`) ON DELETE RESTRICT,
        CONSTRAINT `F_IdentitySession_PlatformId_83F1D` FOREIGN KEY (`PlatformId`) REFERENCES `IdentityPlatform` (`Id`) ON DELETE RESTRICT
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityOrganization_Name_F13BC` ON `IdentityOrganization` (`Name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentityPermission_OrganizationId_EF38A` ON `IdentityPermission` (`OrganizationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentityPermission_RoleId_C1B82` ON `IdentityPermission` (`RoleId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentityPermission_UserId_A10AB` ON `IdentityPermission` (`UserId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityPermission_UserId_RoleId_OrganizationId_800D6` ON `IdentityPermission` (`UserId`, `RoleId`, `OrganizationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityPlatform_Name_423B5` ON `IdentityPlatform` (`Name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityPlatform_SecretKey_E10BA` ON `IdentityPlatform` (`SecretKey`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityRole_Name_36689` ON `IdentityRole` (`Name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentitySession_PermissionId_EB569` ON `IdentitySession` (`PermissionId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE INDEX `I_IdentitySession_PlatformId_0A8EA` ON `IdentitySession` (`PlatformId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_Jti_B9210` ON `IdentitySession` (`Jti`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_Key_45F50` ON `IdentitySession` (`Key`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_RefreshToken_BC11C` ON `IdentitySession` (`RefreshToken`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentitySession_TokenHash_15499` ON `IdentitySession` (`TokenHash`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_Email_A5252` ON `IdentityUser` (`Email`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_Mobile_B52FB` ON `IdentityUser` (`Mobile`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_UniqueId_E083B` ON `IdentityUser` (`UniqueId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    CREATE UNIQUE INDEX `U_IdentityUser_Username_85643` ON `IdentityUser` (`Username`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;


DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20210915053149_Migration_IdentityMySqlDbContext_1') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20210915053149_Migration_IdentityMySqlDbContext_1', '5.0.10');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

