-- Procédures stockées du module "Gestion des associations Fournisseur / Product Manager / Product Group"

CREATE OR ALTER PROCEDURE dbo.GetAllSupp
    @SupplierCode NVARCHAR(20) = NULL,
    @PMId         INT = NULL,
    @PGId         INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        s.SupplierId,
        s.SupplierCode,
        s.SupplierName,
        s.SupplierAddress,
        pm.CodePM AS DefaultPM,
        s.BuyerSupplier,
        s.CountrySupplier,
        s.EmailSupplier
    FROM dbo.Supplier s
    LEFT JOIN dbo.ProductManager pm ON pm.PMId = s.DefaultPMId
    WHERE (@SupplierCode IS NULL OR s.SupplierCode = @SupplierCode)
      AND (
            (@PMId IS NULL AND @PGId IS NULL)
            OR EXISTS (
                SELECT 1
                FROM dbo.SupplierPmPgLink l
                WHERE l.SupplierId = s.SupplierId
                  AND (@PMId IS NULL OR l.PMId = @PMId)
                  AND (@PGId IS NULL OR l.PGId = @PGId)
            )
          )
    ORDER BY s.SupplierCode;
END
GO

CREATE OR ALTER PROCEDURE dbo.GetPMPG
    @NoSupplier INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        l.LinkId,
        pm.PMId,
        pm.CodePM,
        pm.LastNamePM,
        pm.FirstNamePM,
        pm.EmailPM,
        pg.PGId,
        pg.CodePG,
        pg.Description,
        l.CreatedUser,
        l.CreatedDate,
        l.LastUpdatedUser,
        l.LastUpdatedDate
    FROM dbo.SupplierPmPgLink l
    JOIN dbo.ProductManager pm ON pm.PMId = l.PMId
    JOIN dbo.ProductGroup pg ON pg.PGId = l.PGId
    WHERE l.SupplierId = @NoSupplier
    ORDER BY pm.CodePM, pg.CodePG;
END
GO

-- @LinkId identifie précisément la ligne à modifier/supprimer (nécessaire car un fournisseur
-- peut avoir plusieurs associations : NoSupplier/NoPM/NoPG seuls ne suffisent pas à cibler
-- une ligne existante lors d'une modification du couple PM/PG).
CREATE OR ALTER PROCEDURE dbo.SetPGPM
    @NoSupplier INT,
    @NoPM       INT,
    @NoPG       INT,
    @Action     CHAR(1),
    @User       NVARCHAR(50),
    @LinkId     INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @Action = 'I'
    BEGIN
        IF EXISTS (SELECT 1 FROM dbo.ProductGroup WHERE PGId = @NoPG AND PMId <> @NoPM)
        BEGIN
            RAISERROR('Ce Product Group appartient déjà à un autre Product Manager.', 16, 1);
            RETURN;
        END
        IF EXISTS (SELECT 1 FROM dbo.SupplierPmPgLink WHERE SupplierId = @NoSupplier AND PGId = @NoPG)
        BEGIN
            RAISERROR('Cette association Fournisseur / Product Group existe déjà.', 16, 1);
            RETURN;
        END

        INSERT INTO dbo.SupplierPmPgLink (SupplierId, PMId, PGId, CreatedUser, CreatedDate)
        VALUES (@NoSupplier, @NoPM, @NoPG, @User, GETDATE());
    END
    ELSE IF @Action = 'U'
    BEGIN
        IF @LinkId IS NULL
        BEGIN
            RAISERROR('LinkId requis pour une modification.', 16, 1);
            RETURN;
        END
        IF EXISTS (SELECT 1 FROM dbo.ProductGroup WHERE PGId = @NoPG AND PMId <> @NoPM)
        BEGIN
            RAISERROR('Ce Product Group appartient déjà à un autre Product Manager.', 16, 1);
            RETURN;
        END
        IF EXISTS (
            SELECT 1 FROM dbo.SupplierPmPgLink
            WHERE SupplierId = @NoSupplier AND PGId = @NoPG AND LinkId <> @LinkId
        )
        BEGIN
            RAISERROR('Cette association Fournisseur / Product Group existe déjà.', 16, 1);
            RETURN;
        END

        UPDATE dbo.SupplierPmPgLink
        SET PMId = @NoPM,
            PGId = @NoPG,
            LastUpdatedUser = @User,
            LastUpdatedDate = GETDATE()
        WHERE LinkId = @LinkId AND SupplierId = @NoSupplier;
    END
    ELSE IF @Action = 'D'
    BEGIN
        IF @LinkId IS NULL
        BEGIN
            RAISERROR('LinkId requis pour une suppression.', 16, 1);
            RETURN;
        END

        DELETE FROM dbo.SupplierPmPgLink
        WHERE LinkId = @LinkId AND SupplierId = @NoSupplier;
    END
    ELSE
    BEGIN
        RAISERROR('Action invalide : attendu I, U ou D.', 16, 1);
    END
END
GO

-- Procédures complémentaires (non listées dans la demande initiale mais nécessaires pour
-- alimenter les combos de recherche et le dialogue Ajouter/Modifier une association).
CREATE OR ALTER PROCEDURE dbo.GetAllPM
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PMId, CodePM, LastNamePM, FirstNamePM, EmailPM
    FROM dbo.ProductManager
    ORDER BY CodePM;
END
GO

CREATE OR ALTER PROCEDURE dbo.GetAllPG
    @PMId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PGId, CodePG, Description, PMId
    FROM dbo.ProductGroup
    WHERE (@PMId IS NULL OR PMId = @PMId)
    ORDER BY CodePG;
END
GO
