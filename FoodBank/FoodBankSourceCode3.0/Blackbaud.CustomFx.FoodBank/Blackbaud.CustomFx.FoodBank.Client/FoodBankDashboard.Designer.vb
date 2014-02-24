<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FoodBankDashboard
    Inherits Browser.Controls.RdlcDashboard

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rpDashboard = New Blackbaud.AppFx.Controls.ReportViewer
        Me.SuspendLayout()
        '
        'rpDashboard
        '
        Me.rpDashboard.BackColor = System.Drawing.SystemColors.Window
        Me.rpDashboard.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rpDashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rpDashboard.Location = New System.Drawing.Point(0, 0)
        Me.rpDashboard.Name = "rvDashboard"
        Me.rpDashboard.Size = New System.Drawing.Size(618, 142)
        Me.rpDashboard.TabIndex = 1
        Me.rpDashboard.ToolStripGripStyle = System.Windows.Forms.ToolStripGripStyle.Visible
        Me.rpDashboard.ToolStripRenderStyle = Blackbaud.AppFx.Controls.FlatToolStripRenderer.ToolStripRenderStyle.MenuBar
        '
        'FoodBankDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.rpDashboard)
        Me.Name = "FoodBankDashboard"
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents rpDashboard As Blackbaud.AppFx.Controls.ReportViewer

End Class
