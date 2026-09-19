''' <summary>
''' Écran de gestion des associations Fournisseur / Product Manager / Product Group.
''' dgvSupplier liste les fournisseurs correspondant aux critères ; dgvPmPgr liste, pour le
''' fournisseur sélectionné, ses associations PM-PG (gérées via les boutons Ajouter/Modifier/Supprimer).
''' </summary>
Public Class frmMain

    ''' <summary>Option d'un combo de recherche ; Id = Nothing représente "Tous".</summary>
    Private NotInheritable Class ComboOption
        Public ReadOnly Property Id As Integer?
        Public ReadOnly Property Label As String

        Public Sub New(id As Integer?, label As String)
            Me.Id = id
            Me.Label = label
        End Sub

        Public Overrides Function ToString() As String
            Return Label
        End Function
    End Class

    Private _supplierTable As DataTable
    Private _pmPgTable As DataTable
    Private ReadOnly _currentUser As String = Environment.UserName

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadComboSuppliers()
        LoadComboManagers()
        LoadComboProductGroups()
    End Sub

#Region "Chargement des listes déroulantes"

    Private Sub LoadComboSuppliers()
        cboListSuppliers.Items.Clear()
        cboListSuppliers.Items.Add(New ComboOption(Nothing, "Tous"))
        For Each row As DataRow In SupplierRepository.GetAllSuppliers().Rows
            cboListSuppliers.Items.Add(New ComboOption(Convert.ToInt32(row("SupplierId")), row("SupplierCode").ToString()))
        Next
        cboListSuppliers.SelectedIndex = 0
    End Sub

    Private Sub LoadComboManagers()
        cboListManagers.Items.Clear()
        cboListManagers.Items.Add(New ComboOption(Nothing, "Tous"))
        For Each row As DataRow In PmPgRepository.GetAllProductManagers().Rows
            cboListManagers.Items.Add(New ComboOption(Convert.ToInt32(row("PMId")), row("CodePM").ToString()))
        Next
        cboListManagers.SelectedIndex = 0
    End Sub

    Private Sub LoadComboProductGroups()
        cboListProductGroup.Items.Clear()
        cboListProductGroup.Items.Add(New ComboOption(Nothing, "Tous"))
        For Each row As DataRow In PmPgRepository.GetAllProductGroups().Rows
            cboListProductGroup.Items.Add(New ComboOption(Convert.ToInt32(row("PGId")), row("CodePG").ToString()))
        Next
        cboListProductGroup.SelectedIndex = 0
    End Sub

    Private Function SelectedIdOf(combo As ComboBox) As Integer?
        Dim selected = TryCast(combo.SelectedItem, ComboOption)
        Return If(selected Is Nothing, Nothing, selected.Id)
    End Function

#End Region

#Region "Critères de recherche"

    Private Sub btnInit_Click(sender As Object, e As EventArgs) Handles btnInit.Click
        ResetSearchCriteria()
    End Sub

    Private Sub ResetSearchCriteria()
        txtSupplierCode.Text = String.Empty
        If cboListSuppliers.Items.Count > 0 Then cboListSuppliers.SelectedIndex = 0
        If cboListManagers.Items.Count > 0 Then cboListManagers.SelectedIndex = 0
        If cboListProductGroup.Items.Count > 0 Then cboListProductGroup.SelectedIndex = 0

        _supplierTable = Nothing
        _pmPgTable = Nothing
        dgvSupplier.DataSource = Nothing
        dgvPmPgr.DataSource = Nothing
    End Sub

    Private Sub btnChercher_Click(sender As Object, e As EventArgs) Handles btnChercher.Click
        Search()
    End Sub

    Private Sub Search()
        ' Si un fournisseur précis est choisi dans cboListSuppliers, il prime sur le texte saisi.
        Dim selectedSupplier = TryCast(cboListSuppliers.SelectedItem, ComboOption)
        Dim codeFilter As String = Nothing
        If selectedSupplier IsNot Nothing AndAlso selectedSupplier.Id.HasValue Then
            codeFilter = selectedSupplier.Label
        ElseIf txtSupplierCode.Text.Trim().Length > 0 Then
            codeFilter = txtSupplierCode.Text.Trim()
        End If

        Dim pmId = SelectedIdOf(cboListManagers)
        Dim pgId = SelectedIdOf(cboListProductGroup)

        _supplierTable = SupplierRepository.GetAllSuppliers(codeFilter, pmId, pgId)
        dgvSupplier.DataSource = _supplierTable

        LoadAssociationsForSelectedSupplier()
    End Sub

#End Region

#Region "Grilles"

    Private Sub dgvSupplier_SelectionChanged(sender As Object, e As EventArgs) Handles dgvSupplier.SelectionChanged
        LoadAssociationsForSelectedSupplier()
    End Sub

    Private Sub LoadAssociationsForSelectedSupplier()
        Dim supplierId = CurrentSupplierId()
        If Not supplierId.HasValue Then
            _pmPgTable = Nothing
            dgvPmPgr.DataSource = Nothing
            Return
        End If

        _pmPgTable = PmPgRepository.GetBySupplier(supplierId.Value)
        dgvPmPgr.DataSource = _pmPgTable
    End Sub

    Private Function CurrentSupplierId() As Integer?
        Dim rowView = CurrentSupplierRowView()
        If rowView Is Nothing Then Return Nothing
        Return Convert.ToInt32(rowView("SupplierId"))
    End Function

    Private Function CurrentSupplierRowView() As DataRowView
        If dgvSupplier.CurrentRow Is Nothing Then Return Nothing
        Return TryCast(dgvSupplier.CurrentRow.DataBoundItem, DataRowView)
    End Function

    Private Function CurrentSupplierLabel() As String
        Dim rowView = CurrentSupplierRowView()
        If rowView Is Nothing Then Return String.Empty
        Return $"{rowView("SupplierCode")} - {rowView("SupplierName")}"
    End Function

    Private Function CurrentAssociationRowView() As DataRowView
        If dgvPmPgr.CurrentRow Is Nothing Then Return Nothing
        Return TryCast(dgvPmPgr.CurrentRow.DataBoundItem, DataRowView)
    End Function

#End Region

#Region "Gestion des associations (Ajouter / Modifier / Supprimer)"

    Private Sub btnAjouter_Click(sender As Object, e As EventArgs) Handles btnAjouter.Click
        Dim supplierId = CurrentSupplierId()
        If Not supplierId.HasValue Then
            MessageBox.Show("Veuillez sélectionner un fournisseur.", "Association", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Using dlg As New frmAssociation(frmAssociation.AssociationMode.Add, supplierId.Value, CurrentSupplierLabel())
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Try
                    PmPgRepository.SaveAssociation(supplierId.Value, dlg.SelectedPMId, dlg.SelectedPGId, "I"c, _currentUser)
                    LoadAssociationsForSelectedSupplier()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnModifier_Click(sender As Object, e As EventArgs) Handles btnModifier.Click
        Dim supplierId = CurrentSupplierId()
        Dim associationRow = CurrentAssociationRowView()
        If Not supplierId.HasValue OrElse associationRow Is Nothing Then
            MessageBox.Show("Veuillez sélectionner une association à modifier.", "Association", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim linkId = Convert.ToInt32(associationRow("LinkId"))
        Dim currentPMId = Convert.ToInt32(associationRow("PMId"))
        Dim currentPGId = Convert.ToInt32(associationRow("PGId"))

        Using dlg As New frmAssociation(frmAssociation.AssociationMode.Edit, supplierId.Value, CurrentSupplierLabel(), linkId, currentPMId, currentPGId)
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Try
                    PmPgRepository.SaveAssociation(supplierId.Value, dlg.SelectedPMId, dlg.SelectedPGId, "U"c, _currentUser, linkId)
                    LoadAssociationsForSelectedSupplier()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) Handles btnSupprimer.Click
        Dim supplierId = CurrentSupplierId()
        Dim associationRow = CurrentAssociationRowView()
        If Not supplierId.HasValue OrElse associationRow Is Nothing Then
            MessageBox.Show("Veuillez sélectionner une association à supprimer.", "Association", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Supprimer cette association ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            Return
        End If

        Dim linkId = Convert.ToInt32(associationRow("LinkId"))
        Dim pmId = Convert.ToInt32(associationRow("PMId"))
        Dim pgId = Convert.ToInt32(associationRow("PGId"))

        Try
            PmPgRepository.SaveAssociation(supplierId.Value, pmId, pgId, "D"c, _currentUser, linkId)
            LoadAssociationsForSelectedSupplier()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Export / Import"

    Private Sub btnExportAll_Click(sender As Object, e As EventArgs) Handles btnExportAll.Click
        If _supplierTable Is Nothing OrElse _supplierTable.Rows.Count = 0 Then
            MessageBox.Show("Aucune donnée à exporter. Lancez une recherche au préalable.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using dlg As New SaveFileDialog() With {.Filter = "Classeur Excel (*.xlsx)|*.xlsx", .FileName = "Export_All.xlsx"}
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Try
                    ExportService.ExportAll(_supplierTable, If(_pmPgTable, New DataTable()), dlg.FileName)
                    MessageBox.Show("Export terminé.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnExportPM_Click(sender As Object, e As EventArgs) Handles btnExportPM.Click
        If _pmPgTable Is Nothing OrElse _pmPgTable.Rows.Count = 0 Then
            MessageBox.Show("Aucune association à exporter pour ce fournisseur.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using dlg As New SaveFileDialog() With {.Filter = "Classeur Excel (*.xlsx)|*.xlsx", .FileName = "Export_PM.xlsx"}
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Try
                    ExportService.ExportProductManagers(_pmPgTable, dlg.FileName)
                    MessageBox.Show("Export terminé.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnExportPG_Click(sender As Object, e As EventArgs) Handles btnExportPG.Click
        If _pmPgTable Is Nothing OrElse _pmPgTable.Rows.Count = 0 Then
            MessageBox.Show("Aucune association à exporter pour ce fournisseur.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Using dlg As New SaveFileDialog() With {.Filter = "Classeur Excel (*.xlsx)|*.xlsx", .FileName = "Export_PG.xlsx"}
            If dlg.ShowDialog(Me) = DialogResult.OK Then
                Try
                    ExportService.ExportProductGroups(_pmPgTable, dlg.FileName)
                    MessageBox.Show("Export terminé.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End Using
    End Sub

    Private Sub btnImportPMPG_Click(sender As Object, e As EventArgs) Handles btnImportPMPG.Click
        Using dlg As New OpenFileDialog() With {.Filter = "Classeur Excel (*.xlsx)|*.xlsx"}
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return

            Try
                Dim result = ImportService.ImportProductManagerGroups(dlg.FileName, _currentUser)

                Dim message As New Text.StringBuilder()
                message.AppendLine($"{result.ImportedRows} / {result.TotalRows} ligne(s) importée(s).")
                If result.HasErrors Then
                    message.AppendLine()
                    message.AppendLine("Lignes rejetées :")
                    For Each errorMessage In result.Errors
                        message.AppendLine("- " & errorMessage)
                    Next
                End If

                MessageBox.Show(message.ToString(), "Import PM-PG", MessageBoxButtons.OK,
                                 If(result.HasErrors, MessageBoxIcon.Warning, MessageBoxIcon.Information))

                LoadComboManagers()
                LoadComboProductGroups()
                LoadAssociationsForSelectedSupplier()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using
    End Sub

#End Region

End Class
