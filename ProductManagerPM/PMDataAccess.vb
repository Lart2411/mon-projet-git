Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Mode d'ouverture de EditAddPM : détermine si setPM fait un INSERT ou un UPDATE.
''' </summary>
Public Enum PMFormMode
    Add
    Edit
End Enum

''' <summary>
''' Accès aux données pour la table Product_Manager via getPM / ListUser / setPM.
''' </summary>
Public Class PMDataAccess

    Private ReadOnly _connectionString As String =
        ConfigurationManager.ConnectionStrings("Product_Manager").ConnectionString

    ''' <summary>Charge les données pour dgvPM.</summary>
    Public Function GetPM() As DataTable
        Dim dt As New DataTable()
        Using cn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand("getPM", cn) With {.CommandType = CommandType.StoredProcedure}
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ''' <summary>Charge la liste d'utilisateurs pour le binding du combo UserID.</summary>
    Public Function GetListUser() As DataTable
        Dim dt As New DataTable()
        Using cn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand("ListUser", cn) With {.CommandType = CommandType.StoredProcedure}
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ''' <summary>
    ''' Ajoute (mode Add) ou met à jour (mode Edit) un Product Manager.
    ''' En Edit, ProdMgID et codePM ne sont pas envoyés comme modifiables : setPM les ignore.
    ''' </summary>
    Public Sub SetPM(pm As ProductManager, mode As PMFormMode)
        Using cn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand("setPM", cn) With {.CommandType = CommandType.StoredProcedure}
                cmd.Parameters.Add("@Action", SqlDbType.VarChar, 10).Value = If(mode = PMFormMode.Add, "INSERT", "UPDATE")
                cmd.Parameters.Add("@ProdMgID", SqlDbType.Int).Value = pm.ProdMgID
                cmd.Parameters.Add("@codePM", SqlDbType.VarChar, 50).Value = pm.CodePM
                cmd.Parameters.Add("@lastname", SqlDbType.VarChar, 100).Value = pm.LastName
                cmd.Parameters.Add("@firestname", SqlDbType.VarChar, 100).Value = pm.FirstName
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = pm.UserID
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = pm.Email

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
