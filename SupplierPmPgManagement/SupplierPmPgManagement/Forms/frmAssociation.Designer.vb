Partial Class frmAssociation
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

    Friend WithEvents lblSupplier As Label
    Friend WithEvents lblSupplierValue As Label
    Friend WithEvents lblPM As Label
    Friend WithEvents cboPM As ComboBox
    Friend WithEvents lblPG As Label
    Friend WithEvents cboPG As ComboBox
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button

    Private Sub InitializeComponent()
        Me.lblSupplier = New Label()
        Me.lblSupplierValue = New Label()
        Me.lblPM = New Label()
        Me.cboPM = New ComboBox()
        Me.lblPG = New Label()
        Me.cboPG = New ComboBox()
        Me.btnOK = New Button()
        Me.btnCancel = New Button()
        Me.SuspendLayout()
        '
        'lblSupplier
        '
        Me.lblSupplier.AutoSize = True
        Me.lblSupplier.Location = New Drawing.Point(16, 18)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.Size = New Drawing.Size(65, 13)
        Me.lblSupplier.Text = "Fournisseur :"
        '
        'lblSupplierValue
        '
        Me.lblSupplierValue.AutoSize = True
        Me.lblSupplierValue.Font = New Drawing.Font("Segoe UI", 9.0!, Drawing.FontStyle.Bold)
        Me.lblSupplierValue.Location = New Drawing.Point(120, 18)
        Me.lblSupplierValue.Name = "lblSupplierValue"
        Me.lblSupplierValue.Size = New Drawing.Size(16, 13)
        Me.lblSupplierValue.Text = "-"
        '
        'lblPM
        '
        Me.lblPM.AutoSize = True
        Me.lblPM.Location = New Drawing.Point(16, 54)
        Me.lblPM.Name = "lblPM"
        Me.lblPM.Size = New Drawing.Size(90, 13)
        Me.lblPM.Text = "Product Manager :"
        '
        'cboPM
        '
        Me.cboPM.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboPM.FormattingEnabled = True
        Me.cboPM.Location = New Drawing.Point(120, 51)
        Me.cboPM.Name = "cboPM"
        Me.cboPM.Size = New Drawing.Size(220, 21)
        '
        'lblPG
        '
        Me.lblPG.AutoSize = True
        Me.lblPG.Location = New Drawing.Point(16, 90)
        Me.lblPG.Name = "lblPG"
        Me.lblPG.Size = New Drawing.Size(80, 13)
        Me.lblPG.Text = "Product Group :"
        '
        'cboPG
        '
        Me.cboPG.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboPG.FormattingEnabled = True
        Me.cboPG.Location = New Drawing.Point(120, 87)
        Me.cboPG.Name = "cboPG"
        Me.cboPG.Size = New Drawing.Size(220, 21)
        '
        'btnOK
        '
        Me.btnOK.Location = New Drawing.Point(164, 130)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New Drawing.Size(85, 27)
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = DialogResult.Cancel
        Me.btnCancel.Location = New Drawing.Point(255, 130)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New Drawing.Size(85, 27)
        Me.btnCancel.Text = "Annuler"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmAssociation
        '
        Me.AcceptButton = Me.btnOK
        Me.CancelButton = Me.btnCancel
        Me.AutoScaleDimensions = New Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Drawing.Size(360, 172)
        Me.Controls.Add(Me.lblSupplier)
        Me.Controls.Add(Me.lblSupplierValue)
        Me.Controls.Add(Me.lblPM)
        Me.Controls.Add(Me.cboPM)
        Me.Controls.Add(Me.lblPG)
        Me.Controls.Add(Me.cboPG)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAssociation"
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "Association"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

End Class
