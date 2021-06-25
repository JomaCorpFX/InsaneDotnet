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

IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
GO

CREATE TABLE [Identity].[Organization] (
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
    CONSTRAINT [P_Identity_Organization__Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[Platform] (
    [Id] bigint NOT NULL IDENTITY(10000, 1),
    [Name] nvarchar(128) NOT NULL,
    [Description] nvarchar(512) NOT NULL,
    [SecretKey] nvarchar(128) NOT NULL,
    [LogoUri] nvarchar(256) NOT NULL,
    [Active] bit NOT NULL,
    [Enabled] bit NOT NULL,
    [ActiveUntil] datetimeoffset NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [P_Identity_Platform__Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[Role] (
    [Id] bigint NOT NULL IDENTITY(10000, 1),
    [Name] nvarchar(128) NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [Active] bit NOT NULL,
    [Enabled] bit NOT NULL,
    [ActiveUntil] datetimeoffset NOT NULL,
    CONSTRAINT [P_Identity_Role__Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[User] (
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
    CONSTRAINT [P_Identity_User__Id] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[Permission] (
    [Id] bigint NOT NULL IDENTITY(10000, 1),
    [UserId] bigint NOT NULL,
    [RoleId] bigint NOT NULL,
    [OrganizationId] bigint NOT NULL,
    [Active] bit NOT NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [ActiveUntil] datetimeoffset NOT NULL,
    [Enabled] bit NOT NULL,
    CONSTRAINT [P_Identity_Permission__Id] PRIMARY KEY ([Id]),
    CONSTRAINT [F_Identity_Permission__OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [Identity].[Organization] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [F_Identity_Permission__RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Role] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [F_Identity_Permission__UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Identity].[Session] (
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
    CONSTRAINT [P_Identity_Session__Id] PRIMARY KEY ([Id]),
    CONSTRAINT [F_Identity_Session__PermissionId] FOREIGN KEY ([PermissionId]) REFERENCES [Identity].[Permission] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [F_Identity_Session__PlatformId] FOREIGN KEY ([PlatformId]) REFERENCES [Identity].[Platform] ([Id]) ON DELETE NO ACTION
);
GO

CREATE UNIQUE INDEX [U_Identity_Organization__Name] ON [Identity].[Organization] ([Name]);
GO

CREATE INDEX [I_Identity_Permission__OrganizationId] ON [Identity].[Permission] ([OrganizationId]);
GO

CREATE INDEX [I_Identity_Permission__RoleId] ON [Identity].[Permission] ([RoleId]);
GO

CREATE INDEX [I_Identity_Permission__UserId] ON [Identity].[Permission] ([UserId]);
GO

CREATE UNIQUE INDEX [U_Identity_Permission__UserId_RoleId_OrganizationId] ON [Identity].[Permission] ([UserId], [RoleId], [OrganizationId]);
GO

CREATE UNIQUE INDEX [U_Identity_Platform__Name] ON [Identity].[Platform] ([Name]);
GO

CREATE UNIQUE INDEX [U_Identity_Platform__SecretKey] ON [Identity].[Platform] ([SecretKey]);
GO

CREATE UNIQUE INDEX [U_Identity_Role__Name] ON [Identity].[Role] ([Name]);
GO

CREATE INDEX [I_Identity_Session__PermissionId] ON [Identity].[Session] ([PermissionId]);
GO

CREATE INDEX [I_Identity_Session__PlatformId] ON [Identity].[Session] ([PlatformId]);
GO

CREATE UNIQUE INDEX [U_Identity_Session__Jti] ON [Identity].[Session] ([Jti]);
GO

CREATE UNIQUE INDEX [U_Identity_Session__Key] ON [Identity].[Session] ([Key]);
GO

CREATE UNIQUE INDEX [U_Identity_Session__RefreshToken] ON [Identity].[Session] ([RefreshToken]);
GO

CREATE UNIQUE INDEX [U_Identity_Session__TokenHash] ON [Identity].[Session] ([TokenHash]);
GO

CREATE UNIQUE INDEX [U_Identity_User__Email] ON [Identity].[User] ([Email]);
GO

CREATE UNIQUE INDEX [U_Identity_User__Mobile] ON [Identity].[User] ([Mobile]);
GO

CREATE UNIQUE INDEX [U_Identity_User__UniqueId] ON [Identity].[User] ([UniqueId]);
GO

CREATE UNIQUE INDEX [U_Identity_User__Username] ON [Identity].[User] ([Username]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210625081916_Migration_IdentitySqlServerDbContext_1', N'5.0.7');
GO

COMMIT;
GO

