# SupplierPmPgManagement

Application WinForms (VB.NET, .NET Framework 4.8) de gestion des associations
**Fournisseur / Product Manager / Product Group**.

## Règles métier

- Un fournisseur peut avoir plusieurs associations (Product Manager, Product Group).
- Un Product Manager peut être lié à plusieurs Product Group.
- Un Product Group appartient à un seul Product Manager.

## Prérequis

- Visual Studio 2019/2022 avec la charge de travail ".NET desktop development".
- SQL Server accessible, avec les tables et procédures stockées du dossier [`Database/`](Database).
- Package NuGet `ClosedXML` (restauré automatiquement à l'ouverture du projet).

## Mise en route

1. Exécuter dans l'ordre les scripts SQL :
   - `Database/01_Tables.sql`
   - `Database/02_StoredProcedures.sql`
2. Adapter la chaîne de connexion `SupplierDb` dans
   `SupplierPmPgManagement/App.config`.
3. Ouvrir `SupplierPmPgManagement.sln`, restaurer les packages NuGet, compiler.

## Structure

- `Forms/` : `frmMain` (écran principal) et `frmAssociation` (dialogue Ajouter/Modifier une association).
- `Data/` : accès ADO.NET aux procédures stockées (`DbHelper`, `SupplierRepository`, `PmPgRepository`).
- `Models/` : classes métier (Supplier, ProductManager, ProductGroup, SupplierPmPgAssociation).
- `Services/` : `ExportService` (Export All / Export PM / Export PG vers Excel) et `ImportService` (Import PM-PG).
- `Database/` : scripts SQL (tables + procédures stockées).

## Fonctionnement de l'écran principal

- **Critères de recherche** : code fournisseur (saisie libre), et listes déroulantes
  Fournisseur / Product Manager / Product Group (option "Tous" par défaut).
- **Initialiser** : réinitialise les critères de recherche et vide les grilles.
- **Chercher** : remplit `dgvSupplier` selon les critères, puis `dgvPmPgr` avec les
  associations du fournisseur sélectionné.
- Changer la ligne sélectionnée dans `dgvSupplier` recharge automatiquement `dgvPmPgr`.
- **Ajouter / Modifier / Supprimer** : gèrent les associations du fournisseur sélectionné
  via la procédure stockée `SetPGPM`.
- **Export All / Export PM / Export PG** : génèrent des fichiers Excel (.xlsx).
- **Import PM-PG** : importe un fichier Excel (colonnes `CodeSupplier`, `CodePM`, `CodePG`) ;
  toute ligne référençant un code fournisseur/PM/PG inconnu, ou une association déjà
  existante, est rejetée et listée avec son motif dans le rapport d'import (aucune
  création automatique de Product Manager/Product Group).

## Limites connues

- Ce code source n'a pas pu être compilé dans l'environnement de génération (pas de
  toolchain .NET Framework/WinForms disponible côté serveur) : à valider dans Visual Studio.
- `GetAllPM` et `GetAllPG` sont deux procédures stockées complémentaires (non demandées
  initialement) nécessaires pour alimenter les listes déroulantes et le dialogue
  Ajouter/Modifier une association.
