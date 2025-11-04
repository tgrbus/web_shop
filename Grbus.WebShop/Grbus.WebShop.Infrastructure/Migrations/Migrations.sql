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

CREATE TABLE [Basket] (
    [CustomerEmail] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Basket] PRIMARY KEY ([CustomerEmail])
);
GO

CREATE TABLE [Customer] (
    [Email] nvarchar(255) NOT NULL,
    [FirstName] nvarchar(100) NULL,
    [LastName] nvarchar(100) NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Email])
);
GO

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NOT NULL,
    [SKU] nvarchar(50) NOT NULL,
    [Description] nvarchar(1000) NULL,
    [Price] decimal(18,2) NOT NULL,
    [TaxPercentage] decimal(5,2) NOT NULL,
    [StockQuantity] int NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BasketHistory] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(255) NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [Timestamp] datetimeoffset NOT NULL,
    CONSTRAINT [PK_BasketHistory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BasketHistory_Basket_Email] FOREIGN KEY ([Email]) REFERENCES [Basket] ([CustomerEmail]) ON DELETE CASCADE,
    CONSTRAINT [FK_BasketHistory_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [BasketItem] (
    [Id] int NOT NULL IDENTITY,
    [BasketId] nvarchar(255) NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_BasketItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_BasketItem_Basket_BasketId] FOREIGN KEY ([BasketId]) REFERENCES [Basket] ([CustomerEmail]) ON DELETE CASCADE,
    CONSTRAINT [FK_BasketItem_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerEmail') AND [object_id] = OBJECT_ID(N'[Basket]'))
    SET IDENTITY_INSERT [Basket] ON;
INSERT INTO [Basket] ([CustomerEmail])
VALUES (N'tomislav.grbus@gmail.com');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerEmail') AND [object_id] = OBJECT_ID(N'[Basket]'))
    SET IDENTITY_INSERT [Basket] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Email', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Customer]'))
    SET IDENTITY_INSERT [Customer] ON;
INSERT INTO [Customer] ([Email], [FirstName], [LastName])
VALUES (N'tomislav.grbus@gmail.com', N'Tomislav', N'Grbus');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Email', N'FirstName', N'LastName') AND [object_id] = OBJECT_ID(N'[Customer]'))
    SET IDENTITY_INSERT [Customer] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name', N'Price', N'SKU', N'StockQuantity', N'TaxPercentage') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] ON;
INSERT INTO [Product] ([Id], [Description], [Name], [Price], [SKU], [StockQuantity], [TaxPercentage])
VALUES (1, N'This is a sample product.', N'Sample Product 01', 19.99, N'SKU001', 100, 25.0),
(2, N'This is another sample product.', N'Sample Product 02', 29.99, N'SKU002', 50, 25.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name', N'Price', N'SKU', N'StockQuantity', N'TaxPercentage') AND [object_id] = OBJECT_ID(N'[Product]'))
    SET IDENTITY_INSERT [Product] OFF;
GO

CREATE INDEX [IX_BasketHistory_Email] ON [BasketHistory] ([Email]);
GO

CREATE INDEX [IX_BasketHistory_ProductId] ON [BasketHistory] ([ProductId]);
GO

CREATE INDEX [IX_BasketItem_BasketId] ON [BasketItem] ([BasketId]);
GO

CREATE INDEX [IX_BasketItem_ProductId] ON [BasketItem] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251104192202_Pocetna_migracija', N'8.0.21');
GO

COMMIT;
GO

