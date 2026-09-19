Imports System.Data
Imports System.Data.SqlClient

Public NotInheritable Class PmPgRepository

    Private Sub New()
    End Sub

    ''' <summary>Appelle GetPMPG : liste des associations PM-PG du fournisseur donné.</summary>
    Public Shared Function GetBySupplier(supplierId As Integer) As DataTable
        Dim supplierParam As New SqlParameter("@NoSupplier", SqlDbType.Int) With {.Value = supplierId}
        Return DbHelper.ExecuteDataTable("GetPMPG", supplierParam)
    End Function

    ''' <summary>
    ''' Appelle SetPGPM. action = "I"c (ajout), "U"c (modification) ou "D"c (suppression).
    ''' linkId est requis pour "U" et "D" afin de cibler précisément la ligne à modifier/supprimer.
    ''' </summary>
    Public Shared Sub SaveAssociation(supplierId As Integer,
                                       pmId As Integer,
                                       pgId As Integer,
                                       action As Char,
                                       currentUser As String,
                                       Optional linkId As Integer? = Nothing)

        Dim supplierParam As New SqlParameter("@NoSupplier", SqlDbType.Int) With {.Value = supplierId}
        Dim pmParam As New SqlParameter("@NoPM", SqlDbType.Int) With {.Value = pmId}
        Dim pgParam As New SqlParameter("@NoPG", SqlDbType.Int) With {.Value = pgId}
        Dim actionParam As New SqlParameter("@Action", SqlDbType.Char, 1) With {.Value = action.ToString()}
        Dim userParam As New SqlParameter("@User", SqlDbType.NVarChar, 50) With {.Value = currentUser}
        Dim linkIdParam As New SqlParameter("@LinkId", SqlDbType.Int) With {
            .Value = If(linkId.HasValue, CType(linkId.Value, Object), DBNull.Value)
        }

        DbHelper.ExecuteNonQuery("SetPGPM", supplierParam, pmParam, pgParam, actionParam, userParam, linkIdParam)
    End Sub

    ''' <summary>Référentiel complet des Product Managers (alimente les combos et le dialogue d'association).</summary>
    Public Shared Function GetAllProductManagers() As DataTable
        Return DbHelper.ExecuteDataTable("GetAllPM")
    End Function

    ''' <summary>Référentiel des Product Groups, optionnellement filtré par Product Manager.</summary>
    Public Shared Function GetAllProductGroups(Optional pmId As Integer? = Nothing) As DataTable
        Dim pmParam As New SqlParameter("@PMId", SqlDbType.Int) With {
            .Value = If(pmId.HasValue, CType(pmId.Value, Object), DBNull.Value)
        }
        Return DbHelper.ExecuteDataTable("GetAllPG", pmParam)
    End Function

End Class
