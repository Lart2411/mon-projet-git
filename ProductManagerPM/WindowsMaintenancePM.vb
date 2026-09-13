Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Form de maintenance des Product Managers : grille dgvPM + boutons Add / Modify.
''' </summary>
Public Class WindowsMaintenancePM
    Inherits Form

    Private WithEvents dgvPM As New DataGridView()
    Private WithEvents btnAdd As New Button()
    Private WithEvents btnModify As New Button()

    Private ReadOnly _dataAccess As New PMDataAccess()

    Public Sub New()
        InitializeComponent()
        ChargerGrille()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Maintenance des Product Managers"
        Me.Width = 700
        Me.Height = 450

        dgvPM.Dock = DockStyle.Top
        dgvPM.Height = 350
        dgvPM.AutoGenerateColumns = True
        dgvPM.ReadOnly = True
        dgvPM.AllowUserToAddRows = False
        dgvPM.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPM.MultiSelect = False

        btnAdd.Text = "Add"
        btnAdd.Location = New Point(20, 370)
        btnAdd.Width = 100

        btnModify.Text = "Modify"
        btnModify.Location = New Point(140, 370)
        btnModify.Width = 100

        Me.Controls.Add(dgvPM)
        Me.Controls.Add(btnAdd)
        Me.Controls.Add(btnModify)
    End Sub

    ''' <summary>Recharge dgvPM via la procédure getPM.</summary>
    Private Sub ChargerGrille()
        dgvPM.DataSource = _dataAccess.GetPM()
    End Sub

    ''' <summary>Ouvre EditAddPM en mode Add pour créer un nouveau Product Manager.</summary>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim frm As New EditAddPM(PMFormMode.Add)
        If frm.ShowDialog(Me) = DialogResult.OK Then
            ChargerGrille()
        End If
    End Sub

    ''' <summary>Ouvre EditAddPM en mode Edit pour la ligne sélectionnée dans dgvPM.</summary>
    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click
        If dgvPM.CurrentRow Is Nothing Then
            MessageBox.Show("Veuillez sélectionner un Product Manager dans la grille.",
                             "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim row As DataGridViewRow = dgvPM.CurrentRow
        Dim pm As New ProductManager With {
            .ProdMgID = CInt(row.Cells("ProdMgID").Value),
            .CodePM = row.Cells("codePM").Value.ToString(),
            .LastName = row.Cells("lastname").Value.ToString(),
            .FirstName = row.Cells("firestname").Value.ToString(),
            .UserID = CInt(row.Cells("UserID").Value),
            .Email = row.Cells("Email").Value.ToString()
        }

        Dim frm As New EditAddPM(PMFormMode.Edit, pm)
        If frm.ShowDialog(Me) = DialogResult.OK Then
            ChargerGrille()
        End If
    End Sub

End Class
