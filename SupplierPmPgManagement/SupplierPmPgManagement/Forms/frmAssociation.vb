Imports System.Data

''' <summary>Dialogue modal pour ajouter ou modifier une association Fournisseur - Product Manager - Product Group.</summary>
Public Class frmAssociation

    Public Enum AssociationMode
        Add
        Edit
    End Enum

    Public ReadOnly Property Mode As AssociationMode
    Public ReadOnly Property SupplierId As Integer
    ''' <summary>Nothing en mode Ajout ; renseigné en mode Modification pour cibler la ligne à modifier.</summary>
    Public ReadOnly Property LinkId As Integer?

    Public Property SelectedPMId As Integer
    Public Property SelectedPGId As Integer

    Public Sub New(mode As AssociationMode, supplierId As Integer, supplierLabel As String,
                   Optional existingLinkId As Integer? = Nothing,
                   Optional existingPMId As Integer? = Nothing,
                   Optional existingPGId As Integer? = Nothing)

        InitializeComponent()

        Me.Mode = mode
        Me.SupplierId = supplierId
        Me.LinkId = existingLinkId

        lblSupplierValue.Text = supplierLabel
        Text = If(mode = AssociationMode.Add, "Ajouter une association", "Modifier l'association")

        LoadManagers()

        If mode = AssociationMode.Edit AndAlso existingPMId.HasValue Then
            SelectComboValue(cboPM, "PMId", existingPMId.Value)
            If existingPGId.HasValue Then
                SelectComboValue(cboPG, "PGId", existingPGId.Value)
            End If
        End If
    End Sub

    Private Sub LoadManagers()
        Dim table As DataTable = PmPgRepository.GetAllProductManagers()
        cboPM.DisplayMember = "CodePM"
        cboPM.ValueMember = "PMId"
        cboPM.DataSource = table
        cboPM.SelectedIndex = -1
    End Sub

    Private Sub LoadGroups(pmId As Integer)
        Dim table As DataTable = PmPgRepository.GetAllProductGroups(pmId)
        cboPG.DisplayMember = "CodePG"
        cboPG.ValueMember = "PGId"
        cboPG.DataSource = table
        cboPG.SelectedIndex = -1
    End Sub

    Private Sub SelectComboValue(combo As ComboBox, valueMember As String, value As Integer)
        For Each rowView As DataRowView In DirectCast(combo.DataSource, DataTable).DefaultView
            If Convert.ToInt32(rowView(valueMember)) = value Then
                combo.SelectedItem = rowView
                Exit For
            End If
        Next
    End Sub

    Private Sub cboPM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPM.SelectedIndexChanged
        If cboPM.SelectedIndex < 0 Then
            cboPG.DataSource = Nothing
            Return
        End If
        LoadGroups(CInt(cboPM.SelectedValue))
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If cboPM.SelectedIndex < 0 Then
            MessageBox.Show("Veuillez sélectionner un Product Manager.", "Association", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If cboPG.SelectedIndex < 0 Then
            MessageBox.Show("Veuillez sélectionner un Product Group.", "Association", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SelectedPMId = CInt(cboPM.SelectedValue)
        SelectedPGId = CInt(cboPG.SelectedValue)
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

End Class
