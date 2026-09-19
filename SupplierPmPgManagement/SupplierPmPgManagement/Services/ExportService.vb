Imports System.Data
Imports ClosedXML.Excel

''' <summary>Export des grilles de recherche vers un classeur Excel (.xlsx), via ClosedXML.</summary>
Public NotInheritable Class ExportService

    Private Sub New()
    End Sub

    Public Shared Sub ExportAll(supplierTable As DataTable, pmPgTable As DataTable, filePath As String)
        Using workbook As New XLWorkbook()
            workbook.Worksheets.Add(supplierTable, "Fournisseurs")
            workbook.Worksheets.Add(pmPgTable, "Associations PM-PG")
            workbook.SaveAs(filePath)
        End Using
    End Sub

    Public Shared Sub ExportProductManagers(pmPgTable As DataTable, filePath As String)
        Dim distinctPm As DataTable = pmPgTable.DefaultView.ToTable(True, "CodePM", "LastNamePM", "FirstNamePM", "EmailPM")
        Using workbook As New XLWorkbook()
            workbook.Worksheets.Add(distinctPm, "Product Managers")
            workbook.SaveAs(filePath)
        End Using
    End Sub

    Public Shared Sub ExportProductGroups(pmPgTable As DataTable, filePath As String)
        Dim distinctPg As DataTable = pmPgTable.DefaultView.ToTable(True, "CodePG", "Description", "CodePM")
        Using workbook As New XLWorkbook()
            workbook.Worksheets.Add(distinctPg, "Product Groups")
            workbook.SaveAs(filePath)
        End Using
    End Sub

End Class
