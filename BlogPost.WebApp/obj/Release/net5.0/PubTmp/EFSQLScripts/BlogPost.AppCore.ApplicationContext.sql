IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    IF SCHEMA_ID(N'Post') IS NULL EXEC(N'CREATE SCHEMA [Post];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Identity].[Role] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [NormalizedName] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Identity].[RoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(max) NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Identity].[User] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(max) NULL,
        [NormalizedUserName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [NormalizedEmail] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [FirstName] nvarchar(100) NULL,
        [LastName] nvarchar(100) NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Identity].[UserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Identity].[UserLogins] (
        [LoginProvider] nvarchar(max) NULL,
        [ProviderKey] nvarchar(max) NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(max) NULL
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Identity].[UserRoles] (
        [UserId] nvarchar(max) NULL,
        [RoleId] nvarchar(max) NULL
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Identity].[UserTokens] (
        [UserId] nvarchar(max) NULL,
        [LoginProvider] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Value] nvarchar(max) NULL
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Post].[Post] (
        [Id] int NOT NULL IDENTITY,
        [PostMessage] nvarchar(max) NULL,
        [AuthorId] uniqueidentifier NOT NULL,
        [StatusId] int NOT NULL,
        [PublishedDate] datetime2 NOT NULL,
        [LastChangedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Post] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE TABLE [Post].[PostStatus] (
        [Id] int NOT NULL IDENTITY,
        [Status] int NOT NULL,
        [StatusDescription] nvarchar(max) NULL,
        [Comment] nvarchar(max) NULL,
        [StatusDate] datetime2 NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [PostId] int NOT NULL,
        CONSTRAINT [PK_PostStatus] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PostStatus_Post_PostId] FOREIGN KEY ([PostId]) REFERENCES [Post].[Post] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    CREATE INDEX [IX_PostStatus_PostId] ON [Post].[PostStatus] ([PostId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208053759_DatabaseCreation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210208053759_DatabaseCreation', N'3.1.5');
END;

GO

