IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [IdentityOrganization] (
        [Id] bigint NOT NULL IDENTITY(10000, 1),
        [Name] nvarchar(128) NOT NULL,
        [AddressLine1] nvarchar(128) NOT NULL,
        [AddresssLine2] nvarchar(128) NOT NULL,
        [Email] nvarchar(128) NOT NULL,
        [Phone] nvarchar(16) NOT NULL,
        [LogoUri] nvarchar(256) NOT NULL,
        [Active] bit NOT NULL,
        [Enabled] bit NOT NULL,
        [ActiveUntil] datetimeoffset NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityOrganization_Id_dbb7e] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [IdentityPlatform] (
        [Id] bigint NOT NULL IDENTITY(10000, 1),
        [Name] nvarchar(128) NOT NULL,
        [Description] nvarchar(512) NOT NULL,
        [SecretKey] nvarchar(128) NOT NULL,
        [LogoUri] nvarchar(256) NOT NULL,
        [Active] bit NOT NULL,
        [Enabled] bit NOT NULL,
        [ActiveUntil] datetimeoffset NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityPlatform_Id_a8230] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [IdentityRole] (
        [Id] bigint NOT NULL IDENTITY(10000, 1),
        [Name] nvarchar(128) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [Active] bit NOT NULL,
        [Enabled] bit NOT NULL,
        [ActiveUntil] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityRole_Id_5f896] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [IdentityUser] (
        [Id] bigint NOT NULL IDENTITY(10000, 1),
        [UniqueId] nvarchar(128) NOT NULL,
        [Username] nvarchar(128) NOT NULL,
        [NormalizedUsername] nvarchar(128) NOT NULL,
        [Password] nvarchar(128) NOT NULL,
        [Email] nvarchar(128) NOT NULL,
        [NormalizedEmail] nvarchar(128) NOT NULL,
        [Phone] nvarchar(16) NULL,
        [Mobile] nvarchar(16) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [Enabled] bit NOT NULL,
        [Active] bit NOT NULL,
        [EmailConfirmed] bit NOT NULL,
        [EmailConfirmationCode] nvarchar(max) NOT NULL,
        [EmailConfirmationDeadline] datetimeoffset NOT NULL,
        [MobileConfirmed] bit NOT NULL,
        [MobileConfirmationCode] nvarchar(max) NOT NULL,
        [MobileConfirmationDeadline] datetimeoffset NOT NULL,
        [LoginFailCount] int NOT NULL,
        [LockoutUntil] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityUser_Id_d06a2] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [IdentityPermission] (
        [Id] bigint NOT NULL IDENTITY(10000, 1),
        [UserId] bigint NOT NULL,
        [RoleId] bigint NOT NULL,
        [OrganizationId] bigint NOT NULL,
        [Active] bit NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [ActiveUntil] datetimeoffset NOT NULL,
        [Enabled] bit NOT NULL,
        CONSTRAINT [P_IdentityPermission_Id_aa4e4] PRIMARY KEY ([Id]),
        CONSTRAINT [F_IdentityPermission_OrganizationId_98097] FOREIGN KEY ([OrganizationId]) REFERENCES [IdentityOrganization] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [F_IdentityPermission_RoleId_63ea2] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRole] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [F_IdentityPermission_UserId_c3dfe] FOREIGN KEY ([UserId]) REFERENCES [IdentityUser] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [IdentitySession] (
        [Id] bigint NOT NULL IDENTITY(10000, 1),
        [PlatformId] bigint NOT NULL,
        [PermissionId] bigint NOT NULL,
        [Jti] nvarchar(128) NOT NULL,
        [TokenHash] nvarchar(128) NOT NULL,
        [RefreshToken] nvarchar(128) NOT NULL,
        [Key] nvarchar(128) NOT NULL,
        [ClientUserAgent] nvarchar(512) NOT NULL,
        [ClientFriendlyName] nvarchar(128) NOT NULL,
        [ClientOS] nvarchar(128) NOT NULL,
        [ClientIP] nvarchar(64) NOT NULL,
        [ClientTimezone] int NOT NULL,
        [ClientLatitude] decimal(18,2) NOT NULL,
        [ClientLongitude] decimal(18,2) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [ExpiresAt] datetimeoffset NOT NULL,
        [Revoked] bit NOT NULL,
        CONSTRAINT [P_IdentitySession_Id_ecb1d] PRIMARY KEY ([Id]),
        CONSTRAINT [F_IdentitySession_PermissionId_68abc] FOREIGN KEY ([PermissionId]) REFERENCES [IdentityPermission] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [F_IdentitySession_PlatformId_83f1d] FOREIGN KEY ([PlatformId]) REFERENCES [IdentityPlatform] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityOrganization_Name_f13bc] ON [IdentityOrganization] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityPermission_OrganizationId_ef38a] ON [IdentityPermission] ([OrganizationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityPermission_RoleId_c1b82] ON [IdentityPermission] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityPermission_UserId_a10ab] ON [IdentityPermission] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityPermission_UserId_RoleId_OrganizationId_800d6] ON [IdentityPermission] ([UserId], [RoleId], [OrganizationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityPlatform_Name_423b5] ON [IdentityPlatform] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityPlatform_SecretKey_e10ba] ON [IdentityPlatform] ([SecretKey]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityRole_Name_36689] ON [IdentityRole] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentitySession_PermissionId_eb569] ON [IdentitySession] ([PermissionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentitySession_PlatformId_0a8ea] ON [IdentitySession] ([PlatformId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentitySession_Jti_b9210] ON [IdentitySession] ([Jti]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentitySession_Key_45f50] ON [IdentitySession] ([Key]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentitySession_RefreshToken_bc11c] ON [IdentitySession] ([RefreshToken]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentitySession_TokenHash_15499] ON [IdentitySession] ([TokenHash]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityUser_Email_a5252] ON [IdentityUser] ([Email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityUser_Mobile_b52fb] ON [IdentityUser] ([Mobile]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityUser_UniqueId_e083b] ON [IdentityUser] ([UniqueId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityUser_Username_85643] ON [IdentityUser] ([Username]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210713012627_Migration_IdentitySqlServerDbContext_1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210713012627_Migration_IdentitySqlServerDbContext_1', N'5.0.7');
END;
GO

COMMIT;
GO

