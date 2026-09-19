Partial Class frmMain
    Inherits System.Windows.Forms.Form

    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    Friend WithEvents pnlSearch As Panel
    Friend WithEvents lblSupplierCode As Label
    Friend WithEvents txtSupplierCode As TextBox
    Friend WithEvents lblSuppliers As Label
    Friend WithEvents cboListSuppliers As ComboBox
    Friend WithEvents lblManagers As Label
    Friend WithEvents cboListManagers As ComboBox
    Friend WithEvents lblProductGroup As Label
    Friend WithEvents cboListProductGroup As ComboBox
    Friend WithEvents btnInit As Button
    Friend WithEvents btnChercher As Button

    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents grpSupplier As GroupBox
    Friend WithEvents dgvSupplier As DataGridView
    Friend WithEvents grpPmPgr As GroupBox
    Friend WithEvents dgvPmPgr As DataGridView
    Friend WithEvents pnlAssocButtons As Panel
    Friend WithEvents btnAjouter As Button
    Friend WithEvents btnModifier As Button
    Friend WithEvents btnSupprimer As Button

    Friend WithEvents pnlExportImport As Panel
    Friend WithEvents btnExportAll As Button
    Friend WithEvents btnExportPM As Button
    Friend WithEvents btnExportPG As Button
    Friend WithEvents btnImportPMPG As Button

    Friend WithEvents colSupplierCode As DataGridViewTextBoxColumn
    Friend WithEvents colSupplierName As DataGridViewTextBoxColumn
    Friend WithEvents colSupplierAdress As DataGridViewTextBoxColumn
    Friend WithEvents colDefaultPM As DataGridViewTextBoxColumn
    Friend WithEvents colByerSupplier As DataGridViewTextBoxColumn
    Friend WithEvents colCountrySupplier As DataGridViewTextBoxColumn
    Friend WithEvents colEmalSupplier As DataGridViewTextBoxColumn
    Friend WithEvents colSupplierId As DataGridViewTextBoxColumn

    Friend WithEvents colProdMgrId As DataGridViewTextBoxColumn
    Friend WithEvents colCodePM As DataGridViewTextBoxColumn
    Friend WithEvents colCodePG As DataGridViewTextBoxColumn
    Friend WithEvents colDescriptions As DataGridViewTextBoxColumn
    Friend WithEvents colLastNamePM As DataGridViewTextBoxColumn
    Friend WithEvents colFirestNamePM As DataGridViewTextBoxColumn
    Friend WithEvents colEmailPM As DataGridViewTextBoxColumn
    Friend WithEvents colCreatedUser As DataGridViewTextBoxColumn
    Friend WithEvents colCreatedDate As DataGridViewTextBoxColumn
    Friend WithEvents colLastUpdatedUser As DataGridViewTextBoxColumn
    Friend WithEvents colLastUpdatedDate As DataGridViewTextBoxColumn

    Private Sub InitializeComponent()
        Me.pnlSearch = New Panel()
        Me.lblSupplierCode = New Label()
        Me.txtSupplierCode = New TextBox()
        Me.lblSuppliers = New Label()
        Me.cboListSuppliers = New ComboBox()
        Me.lblManagers = New Label()
        Me.cboListManagers = New ComboBox()
        Me.lblProductGroup = New Label()
        Me.cboListProductGroup = New ComboBox()
        Me.btnInit = New Button()
        Me.btnChercher = New Button()

        Me.splitMain = New SplitContainer()
        Me.grpSupplier = New GroupBox()
        Me.dgvSupplier = New DataGridView()
        Me.grpPmPgr = New GroupBox()
        Me.dgvPmPgr = New DataGridView()
        Me.pnlAssocButtons = New Panel()
        Me.btnAjouter = New Button()
        Me.btnModifier = New Button()
        Me.btnSupprimer = New Button()

        Me.pnlExportImport = New Panel()
        Me.btnExportAll = New Button()
        Me.btnExportPM = New Button()
        Me.btnExportPG = New Button()
        Me.btnImportPMPG = New Button()

        Me.colSupplierCode = New DataGridViewTextBoxColumn()
        Me.colSupplierName = New DataGridViewTextBoxColumn()
        Me.colSupplierAdress = New DataGridViewTextBoxColumn()
        Me.colDefaultPM = New DataGridViewTextBoxColumn()
        Me.colByerSupplier = New DataGridViewTextBoxColumn()
        Me.colCountrySupplier = New DataGridViewTextBoxColumn()
        Me.colEmalSupplier = New DataGridViewTextBoxColumn()
        Me.colSupplierId = New DataGridViewTextBoxColumn()

        Me.colProdMgrId = New DataGridViewTextBoxColumn()
        Me.colCodePM = New DataGridViewTextBoxColumn()
        Me.colCodePG = New DataGridViewTextBoxColumn()
        Me.colDescriptions = New DataGridViewTextBoxColumn()
        Me.colLastNamePM = New DataGridViewTextBoxColumn()
        Me.colFirestNamePM = New DataGridViewTextBoxColumn()
        Me.colEmailPM = New DataGridViewTextBoxColumn()
        Me.colCreatedUser = New DataGridViewTextBoxColumn()
        Me.colCreatedDate = New DataGridViewTextBoxColumn()
        Me.colLastUpdatedUser = New DataGridViewTextBoxColumn()
        Me.colLastUpdatedDate = New DataGridViewTextBoxColumn()

        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.grpSupplier.SuspendLayout()
        CType(Me.dgvSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPmPgr.SuspendLayout()
        CType(Me.dgvPmPgr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAssocButtons.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlExportImport.SuspendLayout()
        Me.SuspendLayout()
        '
        ' pnlSearch
        '
        Me.pnlSearch.Dock = DockStyle.Top
        Me.pnlSearch.Height = 84
        Me.pnlSearch.Controls.Add(Me.lblSupplierCode)
        Me.pnlSearch.Controls.Add(Me.txtSupplierCode)
        Me.pnlSearch.Controls.Add(Me.lblSuppliers)
        Me.pnlSearch.Controls.Add(Me.cboListSuppliers)
        Me.pnlSearch.Controls.Add(Me.lblManagers)
        Me.pnlSearch.Controls.Add(Me.cboListManagers)
        Me.pnlSearch.Controls.Add(Me.lblProductGroup)
        Me.pnlSearch.Controls.Add(Me.cboListProductGroup)
        Me.pnlSearch.Controls.Add(Me.btnInit)
        Me.pnlSearch.Controls.Add(Me.btnChercher)
        '
        ' lblSupplierCode
        '
        Me.lblSupplierCode.AutoSize = True
        Me.lblSupplierCode.Location = New Drawing.Point(12, 15)
        Me.lblSupplierCode.Text = "Code fournisseur :"
        '
        ' txtSupplierCode
        '
        Me.txtSupplierCode.Location = New Drawing.Point(140, 12)
        Me.txtSupplierCode.Size = New Drawing.Size(150, 20)
        '
        ' lblSuppliers
        '
        Me.lblSuppliers.AutoSize = True
        Me.lblSuppliers.Location = New Drawing.Point(310, 15)
        Me.lblSuppliers.Text = "Fournisseur :"
        '
        ' cboListSuppliers
        '
        Me.cboListSuppliers.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboListSuppliers.Location = New Drawing.Point(400, 12)
        Me.cboListSuppliers.Size = New Drawing.Size(180, 21)
        '
        ' lblManagers
        '
        Me.lblManagers.AutoSize = True
        Me.lblManagers.Location = New Drawing.Point(12, 50)
        Me.lblManagers.Text = "Product Manager :"
        '
        ' cboListManagers
        '
        Me.cboListManagers.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboListManagers.Location = New Drawing.Point(140, 47)
        Me.cboListManagers.Size = New Drawing.Size(150, 21)
        '
        ' lblProductGroup
        '
        Me.lblProductGroup.AutoSize = True
        Me.lblProductGroup.Location = New Drawing.Point(310, 50)
        Me.lblProductGroup.Text = "Product Group :"
        '
        ' cboListProductGroup
        '
        Me.cboListProductGroup.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboListProductGroup.Location = New Drawing.Point(400, 47)
        Me.cboListProductGroup.Size = New Drawing.Size(180, 21)
        '
        ' btnInit
        '
        Me.btnInit.Location = New Drawing.Point(610, 46)
        Me.btnInit.Size = New Drawing.Size(90, 27)
        Me.btnInit.Text = "Initialiser"
        Me.btnInit.UseVisualStyleBackColor = True
        '
        ' btnChercher
        '
        Me.btnChercher.Location = New Drawing.Point(710, 46)
        Me.btnChercher.Size = New Drawing.Size(90, 27)
        Me.btnChercher.Text = "Chercher"
        Me.btnChercher.UseVisualStyleBackColor = True
        '
        ' splitMain
        '
        Me.splitMain.Dock = DockStyle.Fill
        Me.splitMain.Orientation = Orientation.Horizontal
        Me.splitMain.SplitterDistance = 230
        Me.splitMain.Panel1.Controls.Add(Me.grpSupplier)
        Me.splitMain.Panel2.Controls.Add(Me.grpPmPgr)
        '
        ' grpSupplier
        '
        Me.grpSupplier.Dock = DockStyle.Fill
        Me.grpSupplier.Text = "Fournisseurs"
        Me.grpSupplier.Controls.Add(Me.dgvSupplier)
        '
        ' dgvSupplier
        '
        Me.dgvSupplier.Dock = DockStyle.Fill
        Me.dgvSupplier.AllowUserToAddRows = False
        Me.dgvSupplier.AllowUserToDeleteRows = False
        Me.dgvSupplier.ReadOnly = True
        Me.dgvSupplier.AutoGenerateColumns = False
        Me.dgvSupplier.RowHeadersVisible = False
        Me.dgvSupplier.MultiSelect = False
        Me.dgvSupplier.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvSupplier.Columns.AddRange(New DataGridViewColumn() {
            Me.colSupplierCode,
            Me.colSupplierName,
            Me.colSupplierAdress,
            Me.colDefaultPM,
            Me.colByerSupplier,
            Me.colCountrySupplier,
            Me.colEmalSupplier,
            Me.colSupplierId
        })
        '
        ' colSupplierCode
        '
        Me.colSupplierCode.Name = "colSupplierCode"
        Me.colSupplierCode.DataPropertyName = "SupplierCode"
        Me.colSupplierCode.HeaderText = "Code"
        Me.colSupplierCode.Width = 90
        '
        ' colSupplierName
        '
        Me.colSupplierName.Name = "colSupplierName"
        Me.colSupplierName.DataPropertyName = "SupplierName"
        Me.colSupplierName.HeaderText = "Nom"
        Me.colSupplierName.Width = 180
        '
        ' colSupplierAdress
        '
        Me.colSupplierAdress.Name = "colSupplierAdress"
        Me.colSupplierAdress.DataPropertyName = "SupplierAddress"
        Me.colSupplierAdress.HeaderText = "Adresse"
        Me.colSupplierAdress.Width = 200
        '
        ' colDefaultPM
        '
        Me.colDefaultPM.Name = "colDefaultPM"
        Me.colDefaultPM.DataPropertyName = "DefaultPM"
        Me.colDefaultPM.HeaderText = "PM par défaut"
        Me.colDefaultPM.Width = 100
        '
        ' colByerSupplier
        '
        Me.colByerSupplier.Name = "colByerSupplier"
        Me.colByerSupplier.DataPropertyName = "BuyerSupplier"
        Me.colByerSupplier.HeaderText = "Acheteur"
        Me.colByerSupplier.Width = 110
        '
        ' colCountrySupplier
        '
        Me.colCountrySupplier.Name = "colCountrySupplier"
        Me.colCountrySupplier.DataPropertyName = "CountrySupplier"
        Me.colCountrySupplier.HeaderText = "Pays"
        Me.colCountrySupplier.Width = 100
        '
        ' colEmalSupplier
        '
        Me.colEmalSupplier.Name = "colEmalSupplier"
        Me.colEmalSupplier.DataPropertyName = "EmailSupplier"
        Me.colEmalSupplier.HeaderText = "Email"
        Me.colEmalSupplier.Width = 160
        '
        ' colSupplierId (technique, masquée)
        '
        Me.colSupplierId.Name = "colSupplierId"
        Me.colSupplierId.DataPropertyName = "SupplierId"
        Me.colSupplierId.Visible = False
        '
        ' grpPmPgr
        '
        Me.grpPmPgr.Dock = DockStyle.Fill
        Me.grpPmPgr.Text = "Associations Product Manager / Product Group"
        Me.grpPmPgr.Controls.Add(Me.dgvPmPgr)
        Me.grpPmPgr.Controls.Add(Me.pnlAssocButtons)
        '
        ' dgvPmPgr
        '
        Me.dgvPmPgr.Dock = DockStyle.Fill
        Me.dgvPmPgr.AllowUserToAddRows = False
        Me.dgvPmPgr.AllowUserToDeleteRows = False
        Me.dgvPmPgr.ReadOnly = True
        Me.dgvPmPgr.AutoGenerateColumns = False
        Me.dgvPmPgr.RowHeadersVisible = False
        Me.dgvPmPgr.MultiSelect = False
        Me.dgvPmPgr.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvPmPgr.Columns.AddRange(New DataGridViewColumn() {
            Me.colProdMgrId,
            Me.colCodePM,
            Me.colCodePG,
            Me.colDescriptions,
            Me.colLastNamePM,
            Me.colFirestNamePM,
            Me.colEmailPM,
            Me.colCreatedUser,
            Me.colCreatedDate,
            Me.colLastUpdatedUser,
            Me.colLastUpdatedDate
        })
        '
        ' colProdMgrId (technique, masquée) : porte l'identifiant de la ligne d'association (LinkId),
        ' utilisé par Modifier/Supprimer pour cibler précisément une association.
        '
        Me.colProdMgrId.Name = "colProdMgrId"
        Me.colProdMgrId.DataPropertyName = "LinkId"
        Me.colProdMgrId.Visible = False
        '
        ' colCodePM
        '
        Me.colCodePM.Name = "colCodePM"
        Me.colCodePM.DataPropertyName = "CodePM"
        Me.colCodePM.HeaderText = "Code PM"
        Me.colCodePM.Width = 90
        '
        ' colCodePG
        '
        Me.colCodePG.Name = "colCodePG"
        Me.colCodePG.DataPropertyName = "CodePG"
        Me.colCodePG.HeaderText = "Code PG"
        Me.colCodePG.Width = 90
        '
        ' colDescriptions
        '
        Me.colDescriptions.Name = "colDescriptions"
        Me.colDescriptions.DataPropertyName = "Description"
        Me.colDescriptions.HeaderText = "Description"
        Me.colDescriptions.Width = 180
        '
        ' colLastNamePM
        '
        Me.colLastNamePM.Name = "colLastNamePM"
        Me.colLastNamePM.DataPropertyName = "LastNamePM"
        Me.colLastNamePM.HeaderText = "Nom PM"
        Me.colLastNamePM.Width = 110
        '
        ' colFirestNamePM
        '
        Me.colFirestNamePM.Name = "colFirestNamePM"
        Me.colFirestNamePM.DataPropertyName = "FirstNamePM"
        Me.colFirestNamePM.HeaderText = "Prénom PM"
        Me.colFirestNamePM.Width = 110
        '
        ' colEmailPM
        '
        Me.colEmailPM.Name = "colEmailPM"
        Me.colEmailPM.DataPropertyName = "EmailPM"
        Me.colEmailPM.HeaderText = "Email PM"
        Me.colEmailPM.Width = 150
        '
        ' colCreatedUser
        '
        Me.colCreatedUser.Name = "colCreatedUser"
        Me.colCreatedUser.DataPropertyName = "CreatedUser"
        Me.colCreatedUser.HeaderText = "Créé par"
        Me.colCreatedUser.Width = 90
        '
        ' colCreatedDate
        '
        Me.colCreatedDate.Name = "colCreatedDate"
        Me.colCreatedDate.DataPropertyName = "CreatedDate"
        Me.colCreatedDate.HeaderText = "Créé le"
        Me.colCreatedDate.Width = 110
        '
        ' colLastUpdatedUser
        '
        Me.colLastUpdatedUser.Name = "colLastUpdatedUser"
        Me.colLastUpdatedUser.DataPropertyName = "LastUpdatedUser"
        Me.colLastUpdatedUser.HeaderText = "Modifié par"
        Me.colLastUpdatedUser.Width = 90
        '
        ' colLastUpdatedDate
        '
        Me.colLastUpdatedDate.Name = "colLastUpdatedDate"
        Me.colLastUpdatedDate.DataPropertyName = "LastUpdatedDate"
        Me.colLastUpdatedDate.HeaderText = "Modifié le"
        Me.colLastUpdatedDate.Width = 110
        '
        ' pnlAssocButtons
        '
        Me.pnlAssocButtons.Dock = DockStyle.Right
        Me.pnlAssocButtons.Width = 120
        Me.pnlAssocButtons.Controls.Add(Me.btnAjouter)
        Me.pnlAssocButtons.Controls.Add(Me.btnModifier)
        Me.pnlAssocButtons.Controls.Add(Me.btnSupprimer)
        '
        ' btnAjouter
        '
        Me.btnAjouter.Location = New Drawing.Point(10, 16)
        Me.btnAjouter.Size = New Drawing.Size(100, 27)
        Me.btnAjouter.Text = "Ajouter"
        Me.btnAjouter.UseVisualStyleBackColor = True
        '
        ' btnModifier
        '
        Me.btnModifier.Location = New Drawing.Point(10, 49)
        Me.btnModifier.Size = New Drawing.Size(100, 27)
        Me.btnModifier.Text = "Modifier"
        Me.btnModifier.UseVisualStyleBackColor = True
        '
        ' btnSupprimer
        '
        Me.btnSupprimer.Location = New Drawing.Point(10, 82)
        Me.btnSupprimer.Size = New Drawing.Size(100, 27)
        Me.btnSupprimer.Text = "Supprimer"
        Me.btnSupprimer.UseVisualStyleBackColor = True
        '
        ' pnlExportImport
        '
        Me.pnlExportImport.Dock = DockStyle.Bottom
        Me.pnlExportImport.Height = 46
        Me.pnlExportImport.Controls.Add(Me.btnExportAll)
        Me.pnlExportImport.Controls.Add(Me.btnExportPM)
        Me.pnlExportImport.Controls.Add(Me.btnExportPG)
        Me.pnlExportImport.Controls.Add(Me.btnImportPMPG)
        '
        ' btnExportAll
        '
        Me.btnExportAll.Location = New Drawing.Point(12, 9)
        Me.btnExportAll.Size = New Drawing.Size(110, 27)
        Me.btnExportAll.Text = "Export All"
        Me.btnExportAll.UseVisualStyleBackColor = True
        '
        ' btnExportPM
        '
        Me.btnExportPM.Location = New Drawing.Point(130, 9)
        Me.btnExportPM.Size = New Drawing.Size(110, 27)
        Me.btnExportPM.Text = "Export PM"
        Me.btnExportPM.UseVisualStyleBackColor = True
        '
        ' btnExportPG
        '
        Me.btnExportPG.Location = New Drawing.Point(248, 9)
        Me.btnExportPG.Size = New Drawing.Size(110, 27)
        Me.btnExportPG.Text = "Export PG"
        Me.btnExportPG.UseVisualStyleBackColor = True
        '
        ' btnImportPMPG
        '
        Me.btnImportPMPG.Location = New Drawing.Point(366, 9)
        Me.btnImportPMPG.Size = New Drawing.Size(130, 27)
        Me.btnImportPMPG.Text = "Import PM-PG"
        Me.btnImportPMPG.UseVisualStyleBackColor = True
        '
        ' frmMain
        '
        Me.AutoScaleDimensions = New Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Drawing.Size(1000, 650)
        Me.MinimumSize = New Drawing.Size(900, 550)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.pnlExportImport)
        Me.Controls.Add(Me.pnlSearch)
        Me.Name = "frmMain"
        Me.Text = "Gestion des associations Fournisseur / Product Manager / Product Group"
        Me.StartPosition = FormStartPosition.CenterScreen

        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.grpSupplier.ResumeLayout(False)
        CType(Me.dgvSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPmPgr.ResumeLayout(False)
        CType(Me.dgvPmPgr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAssocButtons.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlExportImport.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

End Class
