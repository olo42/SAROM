-- info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
--       Entity Framework Core 2.2.4-servicing-10062 initialized 'OperationContext' using provider 'Microsoft.EntityFrameworkCore.SqlServer' with options: None
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [OperationAction] (
    [Id] nvarchar(450) NOT NULL,
    [Message] nvarchar(max) NULL,
    CONSTRAINT [PK_OperationAction] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190721135910_AddOperationActions', N'2.2.4-servicing-10062');

GO

ALTER TABLE [OperationAction] ADD [OperationId] nvarchar(450) NULL;

GO

CREATE TABLE [Operation] (
    [Id] nvarchar(450) NOT NULL,
    [IsClosed] bit NOT NULL,
    [ClosingReport] nvarchar(max) NULL,
    CONSTRAINT [PK_Operation] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Unit] (
    [Id] nvarchar(450) NOT NULL,
    [GroupLeader] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [PagerNumber] nvarchar(max) NULL,
    [OperationId] nvarchar(450) NULL,
    CONSTRAINT [PK_Unit] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Unit_Operation_OperationId] FOREIGN KEY ([OperationId]) REFERENCES [Operation] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Person] (
    [Id] nvarchar(450) NOT NULL,
    [Dog] nvarchar(max) NULL,
    [Name] nvarchar(max) NULL,
    [Type] nvarchar(max) NULL,
    [UnitId] nvarchar(450) NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Person_Unit_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Unit] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_OperationAction_OperationId] ON [OperationAction] ([OperationId]);

GO

CREATE INDEX [IX_Person_UnitId] ON [Person] ([UnitId]);

GO

CREATE INDEX [IX_Unit_OperationId] ON [Unit] ([OperationId]);

GO

ALTER TABLE [OperationAction] ADD CONSTRAINT [FK_OperationAction_Operation_OperationId] FOREIGN KEY ([OperationId]) REFERENCES [Operation] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190721152440_AddPersonsUnitsOperations', N'2.2.4-servicing-10062');

GO

ALTER TABLE [Operation] ADD [AlertDate] nvarchar(max) NULL;

GO

ALTER TABLE [Operation] ADD [AlertTime] nvarchar(max) NULL;

GO

ALTER TABLE [Operation] ADD [Name] nvarchar(max) NULL;

GO

ALTER TABLE [Operation] ADD [State3] nvarchar(max) NULL;

GO

ALTER TABLE [Operation] ADD [State4] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190721153002_AddFieldsToOperations', N'2.2.4-servicing-10062');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190721202545_AddOperationsIdToOperationAction', N'2.2.4-servicing-10062');

GO

ALTER TABLE [Operation] ADD [Headquarter] nvarchar(max) NULL;

GO

ALTER TABLE [Operation] ADD [Number] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190725170348_AddMoreFieldsToOperation', N'2.2.4-servicing-10062');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190726130107_CreateMissingPerson', N'2.2.4-servicing-10062');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190726180750_AddIdToMissingPerson', N'2.2.4-servicing-10062');

GO

CREATE TABLE [MissingPerson] (
    [Id] nvarchar(450) NOT NULL,
    [Ailments] nvarchar(max) NULL,
    [Clothes] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [EyesColour] nvarchar(max) NULL,
    [FurtherInformation] nvarchar(max) NULL,
    [Gender] nvarchar(max) NULL,
    [HairColor] nvarchar(max) NULL,
    [KnownPlaces] nvarchar(max) NULL,
    [Medications] nvarchar(max) NULL,
    [MissingSince] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [Size] nvarchar(max) NULL,
    [SkinType] nvarchar(max) NULL,
    [SpecialCharacteristics] nvarchar(max) NULL,
    [Weight] nvarchar(max) NULL,
    CONSTRAINT [PK_MissingPerson] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190726181441_MissingPerson', N'2.2.4-servicing-10062');

GO

ALTER TABLE [MissingPerson] ADD [OperationId] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190726201538_AddOperationIDToMissingPerson', N'2.2.4-servicing-10062');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operation]') AND [c].[name] = N'State3');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Operation] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Operation] DROP COLUMN [State3];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operation]') AND [c].[name] = N'State4');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Operation] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Operation] DROP COLUMN [State4];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MissingPerson]') AND [c].[name] = N'Name');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [MissingPerson] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [MissingPerson] ALTER COLUMN [Name] nvarchar(max) NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729141022_RemoveStatusFromOperation', N'2.2.4-servicing-10062');

GO

ALTER TABLE [Operation] ADD [HeadquarterContact] nvarchar(max) NULL;

GO

ALTER TABLE [Operation] ADD [PoliceContact] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729141710_AddContactsToOperation', N'2.2.4-servicing-10062');

GO

ALTER TABLE [Operation] ADD [PoliceContactPhone] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729143355_AddPoliceContactPhoneToOperation', N'2.2.4-servicing-10062');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729151516_AddOperationIdToUnit', N'2.2.4-servicing-10062');

GO

ALTER TABLE [OperationAction] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190729214331_AddCreatedToOperationAction', N'2.2.4-servicing-10062');

GO

ALTER TABLE [Unit] ADD [AreaSeeker] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Unit] ADD [DebrisSearcher] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Unit] ADD [Helpers] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Unit] ADD [Mantrailer] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Unit] ADD [WaterLocators] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190803200010_AddSizesToUnits', N'2.2.4-servicing-10062');

GO

ALTER TABLE [OperationAction] ADD [Action] nvarchar(max) NULL;

GO

ALTER TABLE [OperationAction] ADD [UnitName] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190818204849_AddUnitNameAndActionToOperationAction', N'2.2.4-servicing-10062');

GO

ALTER TABLE [OperationAction] DROP CONSTRAINT [FK_OperationAction_Operation_OperationId];

GO

DROP INDEX [IX_OperationAction_OperationId] ON [OperationAction];
DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OperationAction]') AND [c].[name] = N'OperationId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [OperationAction] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [OperationAction] ALTER COLUMN [OperationId] nvarchar(450) NOT NULL;
CREATE INDEX [IX_OperationAction_OperationId] ON [OperationAction] ([OperationId]);

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MissingPerson]') AND [c].[name] = N'DateOfBirth');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [MissingPerson] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [MissingPerson] ALTER COLUMN [DateOfBirth] nvarchar(max) NULL;

GO

ALTER TABLE [OperationAction] ADD CONSTRAINT [FK_OperationAction_Operation_OperationId] FOREIGN KEY ([OperationId]) REFERENCES [Operation] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190820115424_changeDateOfBirthToString', N'2.2.4-servicing-10062');

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operation]') AND [c].[name] = N'AlertTime');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Operation] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Operation] ALTER COLUMN [AlertTime] nvarchar(max) NOT NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Operation]') AND [c].[name] = N'AlertDate');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Operation] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Operation] ALTER COLUMN [AlertDate] nvarchar(max) NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190820153245_MandatoryAlertDateAndTime', N'2.2.4-servicing-10062');

GO

ALTER TABLE [Operation] ADD [OperationLeader] nvarchar(max) NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MissingPerson]') AND [c].[name] = N'OperationId');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [MissingPerson] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [MissingPerson] ALTER COLUMN [OperationId] nvarchar(450) NULL;

GO

CREATE INDEX [IX_MissingPerson_OperationId] ON [MissingPerson] ([OperationId]);

GO

ALTER TABLE [MissingPerson] ADD CONSTRAINT [FK_MissingPerson_Operation_OperationId] FOREIGN KEY ([OperationId]) REFERENCES [Operation] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190823205953_AddMissingPeopleAndOperationLeaderToOperation', N'2.2.4-servicing-10062');

GO


