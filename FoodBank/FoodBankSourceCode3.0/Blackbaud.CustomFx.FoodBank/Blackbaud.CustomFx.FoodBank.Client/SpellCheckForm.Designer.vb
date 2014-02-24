<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpellCheckForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtManual = New System.Windows.Forms.TextBox()
        Me.cmdChange = New System.Windows.Forms.Button()
        Me.lblSuggSpellingText = New System.Windows.Forms.Label()
        Me.lstSuggestions = New System.Windows.Forms.ListBox()
        Me.lblSuggestedSpellings = New System.Windows.Forms.Label()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtManual
        '
        Me.txtManual.Location = New System.Drawing.Point(12, 25)
        Me.txtManual.Multiline = True
        Me.txtManual.Name = "txtManual"
        Me.txtManual.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtManual.Size = New System.Drawing.Size(312, 66)
        Me.txtManual.TabIndex = 2
        '
        'cmdChange
        '
        Me.cmdChange.Enabled = False
        Me.cmdChange.Location = New System.Drawing.Point(330, 174)
        Me.cmdChange.Name = "cmdChange"
        Me.cmdChange.Size = New System.Drawing.Size(108, 23)
        Me.cmdChange.TabIndex = 3
        Me.cmdChange.Text = "Change"
        Me.cmdChange.UseVisualStyleBackColor = True
        '
        'lblSuggSpellingText
        '
        Me.lblSuggSpellingText.AutoSize = True
        Me.lblSuggSpellingText.Location = New System.Drawing.Point(12, 9)
        Me.lblSuggSpellingText.Name = "lblSuggSpellingText"
        Me.lblSuggSpellingText.Size = New System.Drawing.Size(88, 13)
        Me.lblSuggSpellingText.TabIndex = 4
        Me.lblSuggSpellingText.Text = "Not in Dictionary:"
        '
        'lstSuggestions
        '
        Me.lstSuggestions.FormattingEnabled = True
        Me.lstSuggestions.Location = New System.Drawing.Point(12, 118)
        Me.lstSuggestions.Name = "lstSuggestions"
        Me.lstSuggestions.ScrollAlwaysVisible = True
        Me.lstSuggestions.Size = New System.Drawing.Size(312, 108)
        Me.lstSuggestions.TabIndex = 5
        '
        'lblSuggestedSpellings
        '
        Me.lblSuggestedSpellings.AutoSize = True
        Me.lblSuggestedSpellings.Location = New System.Drawing.Point(12, 102)
        Me.lblSuggestedSpellings.Name = "lblSuggestedSpellings"
        Me.lblSuggestedSpellings.Size = New System.Drawing.Size(68, 13)
        Me.lblSuggestedSpellings.TabIndex = 6
        Me.lblSuggestedSpellings.Text = "Suggestions:"
        '
        'cmdCancel
        '
        Me.cmdCancel.Location = New System.Drawing.Point(330, 203)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(108, 23)
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'SpellCheckForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(449, 238)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.lblSuggestedSpellings)
        Me.Controls.Add(Me.txtManual)
        Me.Controls.Add(Me.cmdChange)
        Me.Controls.Add(Me.lstSuggestions)
        Me.Controls.Add(Me.lblSuggSpellingText)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SpellCheckForm"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Suggested Spellings for "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtManual As System.Windows.Forms.TextBox
    Friend WithEvents cmdChange As System.Windows.Forms.Button
    Friend WithEvents lblSuggSpellingText As System.Windows.Forms.Label
    Friend WithEvents lstSuggestions As System.Windows.Forms.ListBox
    Friend WithEvents lblSuggestedSpellings As System.Windows.Forms.Label
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
End Class
