Imports System.Data
Imports System.Data.SqlClient

Public NotInheritable Class SupplierRepository

    Private Sub New()
    End Sub

    ''' <summary>Appelle GetAllSupp. Tout paramètre laissé à Nothing n'est pas filtré (recherche large).</summary>
    Public Shared Function GetAllSuppliers(Optional supplierCode As String = Nothing,
                                            Optional pmId As Integer? = Nothing,
                                            Optional pgId As Integer? = Nothing) As DataTable

        Dim codeParam As New SqlParameter("@SupplierCode", SqlDbType.NVarChar, 20) With {
            .Value = If(String.IsNullOrWhiteSpace(supplierCode), CType(DBNull.Value, Object), supplierCode)
        }
        Dim pmParam As New SqlParameter("@PMId", SqlDbType.Int) With {
            .Value = If(pmId.HasValue, CType(pmId.Value, Object), DBNull.Value)
        }
        Dim pgParam As New SqlParameter("@PGId", SqlDbType.Int) With {
            .Value = If(pgId.HasValue, CType(pgId.Value, Object), DBNull.Value)
        }

        Return DbHelper.ExecuteDataTable("GetAllSupp", codeParam, pmParam, pgParam)
    End Function

End Class
