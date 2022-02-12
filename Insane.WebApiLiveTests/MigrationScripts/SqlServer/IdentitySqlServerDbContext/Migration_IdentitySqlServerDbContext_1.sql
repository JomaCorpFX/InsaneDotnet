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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentityLog] (
        [Id] nvarchar(36) NOT NULL,
        [Level] int NOT NULL,
        [Type] int NOT NULL,
        [Message] nvarchar(max) NULL,
        [RelatedData] nvarchar(max) NULL,
        [RelatedExceptionStacktrace] nvarchar(max) NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        CONSTRAINT [PK_IdentityLog] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentityRole] (
        [Id] nvarchar(36) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Description] nvarchar(512) NULL,
        [LogoUri] nvarchar(2000) NULL,
        [Enabled] bit NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [ActiveUntil] datetimeoffset NULL,
        CONSTRAINT [P_IdentityIdentityRole_Id_B8DAC] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentityUser] (
        [Id] nvarchar(36) NOT NULL,
        [Username] nvarchar(128) NOT NULL,
        [NormalizedUsername] nvarchar(128) NOT NULL,
        [Password] varchar(1536) NOT NULL,
        [Email] nvarchar(128) NOT NULL,
        [NormalizedEmail] nvarchar(128) NOT NULL,
        [Phone] varchar(16) NOT NULL,
        [Mobile] varchar(16) NOT NULL,
        [EmailConfirmed] bit NOT NULL,
        [MobileConfirmed] bit NOT NULL,
        [AccessFailCount] int NOT NULL,
        [LockoutDeadline] datetimeoffset NULL,
        [Summary] nvarchar(1024) NULL,
        [ProfilePictureUri] nvarchar(2000) NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [Enabled] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [TwoFactorSecretKey] varchar(128) NOT NULL,
        [NormalActionSecretKey] varchar(128) NOT NULL,
        [SecurityActionSecretKey] varchar(128) NOT NULL,
        [ActiveUntil] datetimeoffset NULL,
        CONSTRAINT [P_IdentityIdentityUser_Id_F4081] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentityAccess] (
        [Id] nvarchar(36) NOT NULL,
        [UserId] nvarchar(36) NOT NULL,
        [RoleId] nvarchar(36) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [Active] bit NOT NULL,
        [Enabled] bit NOT NULL,
        [ActiveUntil] datetimeoffset NULL,
        CONSTRAINT [P_IdentityIdentityAccess_Id_AB31D] PRIMARY KEY ([Id]),
        CONSTRAINT [F_IdentityIdentityAccess_RoleId_652C0] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[IdentityRole] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [F_IdentityIdentityAccess_UserId_6435A] FOREIGN KEY ([UserId]) REFERENCES [Identity].[IdentityUser] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentityPlatform] (
        [Id] nvarchar(36) NOT NULL,
        [AdminUserId] nvarchar(36) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Description] nvarchar(max) NULL,
        [ApiKey] varchar(900) NOT NULL,
        [LogoUri] nvarchar(2000) NULL,
        [Type] int NOT NULL,
        [ContactEmail] nvarchar(128) NULL,
        [InDevelopment] bit NOT NULL,
        [IsServerSide] bit NOT NULL,
        [RevokeTokenWhenLogout] bit NOT NULL,
        [RememberDeviceStrategy] int NOT NULL,
        [Enabled] bit NOT NULL,
        [ActiveUntil] datetimeoffset NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityIdentityPlatform_Id_0A89D] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_IdentityPlatform_IdentityUser_AdminUserId] FOREIGN KEY ([AdminUserId]) REFERENCES [Identity].[IdentityUser] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentityUserClaim] (
        [Id] nvarchar(36) NOT NULL,
        [UserId] nvarchar(36) NOT NULL,
        [Type] varchar(256) NOT NULL,
        [Value] nvarchar(512) NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [Active] bit NOT NULL,
        [Enabled] bit NOT NULL,
        [ActiveUntil] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityIdentityUserClaim_Id_B38F2] PRIMARY KEY ([Id]),
        CONSTRAINT [F_IdentityIdentityUserClaim_UserId_11415] FOREIGN KEY ([UserId]) REFERENCES [Identity].[IdentityUser] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentityUserRecoveryCode] (
        [Id] nvarchar(36) NOT NULL,
        [UserId] nvarchar(36) NOT NULL,
        [Value] nvarchar(8) NOT NULL,
        [Enabled] bit NOT NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityIdentityUserRecoveryCode_Id_7BE30] PRIMARY KEY ([Id]),
        CONSTRAINT [F_IdentityIdentityUserRecoveryCode_UserId_45B90] FOREIGN KEY ([UserId]) REFERENCES [Identity].[IdentityUser] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE TABLE [Identity].[IdentitySession] (
        [Id] nvarchar(36) NOT NULL,
        [PlatformId] nvarchar(36) NOT NULL,
        [UserId] nvarchar(36) NOT NULL,
        [Jti] varchar(128) NOT NULL,
        [JwtHash] varchar(128) NOT NULL,
        [RefreshToken] varchar(128) NOT NULL,
        [SessionKey] varchar(64) NOT NULL,
        [ClientDeviceUid] varchar(128) NOT NULL,
        [ClientUserAgent] nvarchar(1024) NOT NULL,
        [ClientFriendlyName] nvarchar(128) NOT NULL,
        [ClientOS] nvarchar(128) NOT NULL,
        [ClientIP] nvarchar(45) NULL,
        [ClientTimezone] int NOT NULL,
        [ClientLatitude] decimal(18,2) NULL,
        [ClientLongitude] decimal(18,2) NULL,
        [CreatedAt] datetimeoffset NOT NULL,
        [ExpiresAt] datetimeoffset NOT NULL,
        [Revoked] bit NOT NULL,
        [Confirmed] bit NOT NULL,
        [ActiveUntil] datetimeoffset NULL,
        [UpdatedAt] datetimeoffset NOT NULL,
        CONSTRAINT [P_IdentityIdentitySession_Id_0F348] PRIMARY KEY ([Id]),
        CONSTRAINT [F_IdentityIdentitySession_PlatformId_F7913] FOREIGN KEY ([PlatformId]) REFERENCES [Identity].[IdentityPlatform] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [F_IdentityIdentitySession_UserId_C4F37] FOREIGN KEY ([UserId]) REFERENCES [Identity].[IdentityUser] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityIdentityAccess_RoleId_EF364] ON [Identity].[IdentityAccess] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityIdentityAccess_UserId_FDC64] ON [Identity].[IdentityAccess] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityAccess_UserId_RoleId_841D3] ON [Identity].[IdentityAccess] ([UserId], [RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityIdentityPlatform_AdminUserId_CAD20] ON [Identity].[IdentityPlatform] ([AdminUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityPlatform_ApiKey_452A7] ON [Identity].[IdentityPlatform] ([ApiKey]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityPlatform_Name_D45B0] ON [Identity].[IdentityPlatform] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityRole_Name_319E0] ON [Identity].[IdentityRole] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityIdentitySession_PlatformId_55045] ON [Identity].[IdentitySession] ([PlatformId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityIdentitySession_UserId_39419] ON [Identity].[IdentitySession] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentitySession_Jti_45260] ON [Identity].[IdentitySession] ([Jti]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentitySession_JwtHash_9EA00] ON [Identity].[IdentitySession] ([JwtHash]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentitySession_RefreshToken_32CD0] ON [Identity].[IdentitySession] ([RefreshToken]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentitySession_SessionKey_2222A] ON [Identity].[IdentitySession] ([SessionKey]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityUser_Email_FB231] ON [Identity].[IdentityUser] ([Email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityUser_Mobile_E8AA6] ON [Identity].[IdentityUser] ([Mobile]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityUser_NormalizedUsername_5DB6D] ON [Identity].[IdentityUser] ([NormalizedUsername]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityUser_Password_3679C] ON [Identity].[IdentityUser] ([Password]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityUser_Username_3635F] ON [Identity].[IdentityUser] ([Username]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityIdentityUserClaim_UserId_B6704] ON [Identity].[IdentityUserClaim] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityUserClaim_UserId_Type_Value_EE3A5] ON [Identity].[IdentityUserClaim] ([UserId], [Type], [Value]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE INDEX [I_IdentityIdentityUserRecoveryCode_UserId_283F8] ON [Identity].[IdentityUserRecoveryCode] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    CREATE UNIQUE INDEX [U_IdentityIdentityUserRecoveryCode_UserId_Value_C42A6] ON [Identity].[IdentityUserRecoveryCode] ([UserId], [Value]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220212101750_Migration_IdentitySqlServerDbContext_1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220212101750_Migration_IdentitySqlServerDbContext_1', N'6.0.2');
END;
GO

COMMIT;
GO

