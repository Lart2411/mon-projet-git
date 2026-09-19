Imports System.Data
Imports System.Linq
Imports ClosedXML.Excel

''' <summary>Résultat détaillé d'un import PM-PG : compteurs et motifs de rejet ligne par ligne.</summary>
Public Class ImportResult
    Public Property TotalRows As Integer
    Public Property ImportedRows As Integer
    Public Property Errors As New List(Of String)

    Public ReadOnly Property HasErrors As Boolean
        Get
            Return Errors.Count > 0
        End Get
    End Property
End Class

''' <summary>
''' Import du fichier Excel Import PM-PG (colonnes attendues : CodeSupplier, CodePM, CodePG).
''' Un CodeSupplier / CodePM / CodePG inconnu du référentiel, ou une association déjà existante,
''' fait rejeter la ligne (aucune création automatique de référentiel).
''' </summary>
Public NotInheritable Class ImportService

    Private Sub New()
    End Sub

    Public Shared Function ImportProductManagerGroups(filePath As String, currentUser As String) As ImportResult
        Dim result As New ImportResult()

        Dim supplierIdByCode = ToLookup(SupplierRepository.GetAllSuppliers(), "SupplierCode", "SupplierId")
        Dim pmTable = PmPgRepository.GetAllProductManagers()
        Dim pmIdByCode = ToLookup(pmTable, "CodePM", "PMId")
        Dim pgTable = PmPgRepository.GetAllProductGroups()
        Dim pgIdByCode = ToLookup(pgTable, "CodePG", "PGId")
        Dim pmIdByPgCode = ToLookup(pgTable, "CodePG", "PMId")

        Using workbook As New XLWorkbook(filePath)
            Dim worksheet = workbook.Worksheet(1)
            Dim dataRows = worksheet.RangeUsed().RowsUsed().Skip(1) ' la 1ère ligne est l'en-tête

            For Each row In dataRows
                result.TotalRows += 1
                Dim rowNumber = row.RowNumber()
                Dim supplierCode = row.Cell(1).GetString().Trim()
                Dim codePM = row.Cell(2).GetString().Trim()
                Dim codePG = row.Cell(3).GetString().Trim()

                If Not supplierIdByCode.ContainsKey(supplierCode) Then
                    result.Errors.Add($"Ligne {rowNumber} : fournisseur '{supplierCode}' inconnu.")
                    Continue For
                End If
                If Not pmIdByCode.ContainsKey(codePM) Then
                    result.Errors.Add($"Ligne {rowNumber} : Product Manager '{codePM}' inconnu.")
                    Continue For
                End If
                If Not pgIdByCode.ContainsKey(codePG) Then
                    result.Errors.Add($"Ligne {rowNumber} : Product Group '{codePG}' inconnu.")
                    Continue For
                End If
                If pmIdByPgCode(codePG) <> pmIdByCode(codePM) Then
                    result.Errors.Add($"Ligne {rowNumber} : le Product Group '{codePG}' appartient à un autre Product Manager que '{codePM}'.")
                    Continue For
                End If

                Try
                    PmPgRepository.SaveAssociation(supplierIdByCode(supplierCode), pmIdByCode(codePM), pgIdByCode(codePG), "I"c, currentUser)
                    result.ImportedRows += 1
                Catch ex As Exception
                    result.Errors.Add($"Ligne {rowNumber} : {ex.Message}")
                End Try
            Next
        End Using

        Return result
    End Function

    Private Shared Function ToLookup(table As DataTable, keyColumn As String, valueColumn As String) As Dictionary(Of String, Integer)
        Dim dict As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        For Each dataRow As DataRow In table.Rows
            dict(dataRow(keyColumn).ToString()) = Convert.ToInt32(dataRow(valueColumn))
        Next
        Return dict
    End Function

End Class
