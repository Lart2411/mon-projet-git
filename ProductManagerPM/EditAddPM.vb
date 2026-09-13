Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Form paramétrable d'ajout / modification d'un Product Manager.
''' - Mode Add   : ProdMgID (auto, non saisi) et codePM saisi une seule fois.
''' - Mode Edit  : ProdMgID et codePM affichés en lecture seule (non modifiables),
'''                seuls lastname, firestname, UserID et Email sont modifiables.
''' </summary>
Public Class EditAddPM
    Inherits Form

    Private ReadOnly _mode As PMFormMode
    Private ReadOnly _pm As ProductManager
    Private ReadOnly _dataAccess As New PMDataAccess()

    Private txtProdMgID As New TextBox()
    Private txtCodePM As New TextBox()
    Private txtLastName As New TextBox()
    Private txtFirstName As New TextBox()
    Private cboUserID As New ComboBox()
    Private txtEmail As New TextBox()
    Private WithEvents btnSave As New Button()
    Private WithEvents btnCancel As New Button()

    ''' <param name="mode">Add pour créer, Edit pour modifier.</param>
    ''' <param name="pm">Product Manager à modifier ; requis en mode Edit.</param>
    Public Sub New(mode As PMFormMode, Optional pm As ProductManager = Nothing)
        _mode = mode
        _pm = If(pm, New ProductManager())

        InitializeComponent()
        ChargerListeUser()
        InitialiserChamps()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = If(_mode = PMFormMode.Add, "Ajouter un Product Manager", "Modifier un Product Manager")
        Me.Width = 400
        Me.Height = 340
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        Const xLabel As Integer = 20
        Const xChamp As Integer = 150
        Const largeurChamp As Integer = 200
        Const espacement As Integer = 40
        Dim y As Integer = 20

        AjouterLigne("ProdMgID", txtProdMgID, xLabel, xChamp, y, largeurChamp)
        txtProdMgID.Enabled = False
        y += espacement

        AjouterLigne("Code PM", txtCodePM, xLabel, xChamp, y, largeurChamp)
        y += espacement

        AjouterLigne("Nom", txtLastName, xLabel, xChamp, y, largeurChamp)
        y += espacement

        AjouterLigne("Prénom", txtFirstName, xLabel, xChamp, y, largeurChamp)
        y += espacement

        Dim lblUserID As New Label With {.Text = "Utilisateur", .AutoSize = True, .Location = New Point(xLabel, y + 3)}
        cboUserID.Location = New Point(xChamp, y)
        cboUserID.Width = largeurChamp
        cboUserID.DropDownStyle = ComboBoxStyle.DropDownList
        Me.Controls.Add(lblUserID)
        Me.Controls.Add(cboUserID)
        y += espacement

        AjouterLigne("Email", txtEmail, xLabel, xChamp, y, largeurChamp)
        y += espacement

        btnSave.Text = "Enregistrer"
        btnSave.Location = New Point(xChamp, y + 10)
        btnSave.Width = 90

        btnCancel.Text = "Annuler"
        btnCancel.Location = New Point(xChamp + 100, y + 10)
        btnCancel.Width = 90

        Me.Controls.Add(btnSave)
        Me.Controls.Add(btnCancel)
        Me.AcceptButton = btnSave
    End Sub

    Private Sub AjouterLigne(texte As String, champ As TextBox, xLabel As Integer, xChamp As Integer, y As Integer, largeur As Integer)
        Dim lbl As New Label With {.Text = texte, .AutoSize = True, .Location = New Point(xLabel, y + 3)}
        champ.Location = New Point(xChamp, y)
        champ.Width = largeur
        Me.Controls.Add(lbl)
        Me.Controls.Add(champ)
    End Sub

    ''' <summary>Binding du combo UserID sur la requête ListUser (UserID, NameUser).</summary>
    Private Sub ChargerListeUser()
        Dim dt As DataTable = _dataAccess.GetListUser()
        cboUserID.DataSource = dt
        cboUserID.DisplayMember = "NameUser"
        cboUserID.ValueMember = "UserID"
        cboUserID.SelectedIndex = -1
    End Sub

    Private Sub InitialiserChamps()
        If _mode = PMFormMode.Edit Then
            ' ProdMgID et codePM affichés mais non modifiables (condition 4).
            txtProdMgID.Text = _pm.ProdMgID.ToString()
            txtCodePM.Text = _pm.CodePM
            txtCodePM.Enabled = False

            txtLastName.Text = _pm.LastName
            txtFirstName.Text = _pm.FirstName
            txtEmail.Text = _pm.Email
            cboUserID.SelectedValue = _pm.UserID
        Else
            txtProdMgID.Text = "(auto)"
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not ValiderSaisie() Then Return

        _pm.CodePM = txtCodePM.Text.Trim()
        _pm.LastName = txtLastName.Text.Trim()
        _pm.FirstName = txtFirstName.Text.Trim()
        _pm.Email = txtEmail.Text.Trim()
        _pm.UserID = CInt(cboUserID.SelectedValue)

        Try
            _dataAccess.SetPM(_pm, _mode)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur lors de l'enregistrement : " & ex.Message,
                             "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function ValiderSaisie() As Boolean
        If String.IsNullOrWhiteSpace(txtCodePM.Text) Then
            MessageBox.Show("Le code PM est obligatoire.")
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtLastName.Text) Then
            MessageBox.Show("Le nom est obligatoire.")
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtFirstName.Text) Then
            MessageBox.Show("Le prénom est obligatoire.")
            Return False
        End If
        If cboUserID.SelectedValue Is Nothing Then
            MessageBox.Show("Veuillez sélectionner un utilisateur.")
            Return False
        End If
        If String.IsNullOrWhiteSpace(txtEmail.Text) Then
            MessageBox.Show("L'email est obligatoire.")
            Return False
        End If
        Return True
    End Function

End Class
