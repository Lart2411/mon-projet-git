-- Tables du module "Gestion des associations Fournisseur / Product Manager / Product Group"
-- Suppliers, ProductManager et ProductGroup sont supposés être des référentiels déjà en place.
-- Les scripts ci-dessous les créent au cas où ils n'existeraient pas encore.

IF OBJECT_ID('dbo.ProductManager', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProductManager (
        PMId        INT IDENTITY(1,1) PRIMARY KEY,
        CodePM      NVARCHAR(20)  NOT NULL UNIQUE,
        LastNamePM  NVARCHAR(100) NOT NULL,
        FirstNamePM NVARCHAR(100) NOT NULL,
        EmailPM     NVARCHAR(200) NULL
    );
END
GO

IF OBJECT_ID('dbo.ProductGroup', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProductGroup (
        PGId        INT IDENTITY(1,1) PRIMARY KEY,
        CodePG      NVARCHAR(20)  NOT NULL UNIQUE,
        Description NVARCHAR(300) NULL,
        -- Un Product Group appartient à un seul Product Manager.
        PMId        INT NOT NULL REFERENCES dbo.ProductManager(PMId)
    );
END
GO

IF OBJECT_ID('dbo.Supplier', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Supplier (
        SupplierId      INT IDENTITY(1,1) PRIMARY KEY,
        SupplierCode    NVARCHAR(20)  NOT NULL UNIQUE,
        SupplierName    NVARCHAR(200) NOT NULL,
        SupplierAddress NVARCHAR(300) NULL,
        DefaultPMId     INT NULL REFERENCES dbo.ProductManager(PMId),
        BuyerSupplier   NVARCHAR(100) NULL,
        CountrySupplier NVARCHAR(100) NULL,
        EmailSupplier   NVARCHAR(200) NULL
    );
END
GO

IF OBJECT_ID('dbo.SupplierPmPgLink', 'U') IS NULL
BEGIN
    -- Table de liaison : chaque ligne = une association Fournisseur - Product Manager - Product Group.
    -- Un fournisseur peut avoir plusieurs lignes ; l'unicité porte sur (SupplierId, PGId)
    -- puisqu'un Product Group n'appartient qu'à un seul Product Manager.
    CREATE TABLE dbo.SupplierPmPgLink (
        LinkId          INT IDENTITY(1,1) PRIMARY KEY,
        SupplierId      INT NOT NULL REFERENCES dbo.Supplier(SupplierId),
        PMId            INT NOT NULL REFERENCES dbo.ProductManager(PMId),
        PGId            INT NOT NULL REFERENCES dbo.ProductGroup(PGId),
        CreatedUser     NVARCHAR(50) NOT NULL,
        CreatedDate     DATETIME NOT NULL DEFAULT GETDATE(),
        LastUpdatedUser NVARCHAR(50) NULL,
        LastUpdatedDate DATETIME NULL,
        CONSTRAINT UQ_SupplierPmPgLink_Supplier_PG UNIQUE (SupplierId, PGId)
    );
END
GO
