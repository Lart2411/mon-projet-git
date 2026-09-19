Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

''' <summary>Wrapper ADO.NET générique pour l'appel des procédures stockées.</summary>
Public NotInheritable Class DbHelper

    Private Sub New()
    End Sub

    Private Shared ReadOnly ConnectionStringValue As String =
        ConfigurationManager.ConnectionStrings("SupplierDb").ConnectionString

    Public Shared Function ExecuteDataTable(storedProcedureName As String, ParamArray parameters As SqlParameter()) As DataTable
        Dim table As New DataTable()
        Using connection As New SqlConnection(ConnectionStringValue)
            Using command As New SqlCommand(storedProcedureName, connection)
                command.CommandType = CommandType.StoredProcedure
                If parameters IsNot Nothing Then
                    command.Parameters.AddRange(parameters)
                End If
                Using adapter As New SqlDataAdapter(command)
                    adapter.Fill(table)
                End Using
            End Using
        End Using
        Return table
    End Function

    Public Shared Sub ExecuteNonQuery(storedProcedureName As String, ParamArray parameters As SqlParameter())
        Using connection As New SqlConnection(ConnectionStringValue)
            Using command As New SqlCommand(storedProcedureName, connection)
                command.CommandType = CommandType.StoredProcedure
                If parameters IsNot Nothing Then
                    command.Parameters.AddRange(parameters)
                End If
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

End Class
