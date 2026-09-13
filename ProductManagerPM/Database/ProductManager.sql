-- =====================================================================
-- Base Product_Manager : table + procédures stockées getPM, ListUser, setPM
-- =====================================================================

CREATE TABLE Product_Manager (
    ProdMgID   INT IDENTITY(1,1) PRIMARY KEY,
    codePM     VARCHAR(50)  NOT NULL UNIQUE,
    lastname   VARCHAR(100) NOT NULL,
    firestname VARCHAR(100) NOT NULL,
    UserID     INT          NOT NULL,
    Email      VARCHAR(150) NOT NULL
);
GO

-- Alimente la grille dgvPM
CREATE PROCEDURE getPM
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ProdMgID, codePM, lastname, firestname, UserID, Email
    FROM Product_Manager
    ORDER BY lastname, firestname;
END
GO

-- Alimente le binding du combo UserID de EditAddPM
-- (suppose une table Users(UserID, NameUser) déjà existante)
CREATE PROCEDURE ListUser
AS
BEGIN
    SET NOCOUNT ON;
    SELECT UserID, NameUser
    FROM Users
    ORDER BY NameUser;
END
GO

-- Ajout ou modification d'un Product Manager.
-- En UPDATE, ProdMgID et codePM ne sont volontairement pas modifiés (condition 4).
CREATE PROCEDURE setPM
    @Action     VARCHAR(10),   -- 'INSERT' ou 'UPDATE'
    @ProdMgID   INT,           -- ignoré en INSERT (identity)
    @codePM     VARCHAR(50),   -- utilisé seulement en INSERT
    @lastname   VARCHAR(100),
    @firestname VARCHAR(100),
    @UserID     INT,
    @Email      VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Action = 'INSERT'
    BEGIN
        INSERT INTO Product_Manager (codePM, lastname, firestname, UserID, Email)
        VALUES (@codePM, @lastname, @firestname, @UserID, @Email);

        SELECT SCOPE_IDENTITY() AS NewProdMgID;
    END
    ELSE
    BEGIN
        UPDATE Product_Manager
        SET lastname   = @lastname,
            firestname = @firestname,
            UserID     = @UserID,
            Email      = @Email
        WHERE ProdMgID = @ProdMgID;
    END
END
GO
