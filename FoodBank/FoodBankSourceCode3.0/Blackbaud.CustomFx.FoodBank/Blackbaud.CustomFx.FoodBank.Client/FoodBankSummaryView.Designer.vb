<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FoodBankSummaryView
    Inherits Browser.Controls.DataFormControl

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
        Dim tlpLayout As System.Windows.Forms.TableLayoutPanel
        Dim lbl_caption_TOTALFOODDISTRIBUTED As System.Windows.Forms.Label
        Dim lbl_caption_NAME As System.Windows.Forms.Label
        Dim lbl_caption_TOTALFOODRECEIVED As System.Windows.Forms.Label
        Dim lbl_caption_FOODBANKTYPE As System.Windows.Forms.Label
        Dim lbl_caption_PRIMARYADDRESS As System.Windows.Forms.Label
        Dim lbl_caption_DESCRIPTION As System.Windows.Forms.Label
        Dim lbl_caption_MISSIONSTATEMENT As System.Windows.Forms.Label
        Dim lbl_caption_TOTALFOODRECEIVEDAMOUNT As System.Windows.Forms.Label
        Dim lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT As System.Windows.Forms.Label
        Dim lbl_caption_TOTALFOODRECEIVEDWEIGHT As System.Windows.Forms.Label
        Dim lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT As System.Windows.Forms.Label
        Me.lbl_NAME = New System.Windows.Forms.Label
        Me.lbl_TOTALFOODRECEIVED = New System.Windows.Forms.Label
        Me.lbl_TOTALFOODDISTRIBUTED = New System.Windows.Forms.Label
        Me.lbl_FOODBANKTYPE = New System.Windows.Forms.Label
        Me.lbl_PRIMARYADDRESS = New System.Windows.Forms.Label
        Me.txt_DESCRIPTION = New System.Windows.Forms.TextBox
        Me.txt_MISSIONSTATEMENt = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.lbl_TOTALFOODRECEIVEDAMOUNT = New System.Windows.Forms.Label
        Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT = New System.Windows.Forms.Label
        Me.lbl_TOTALFOODRECEIVEDWEIGHT = New System.Windows.Forms.Label
        Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT = New System.Windows.Forms.Label
        tlpLayout = New System.Windows.Forms.TableLayoutPanel
        lbl_caption_TOTALFOODDISTRIBUTED = New System.Windows.Forms.Label
        lbl_caption_NAME = New System.Windows.Forms.Label
        lbl_caption_TOTALFOODRECEIVED = New System.Windows.Forms.Label
        lbl_caption_FOODBANKTYPE = New System.Windows.Forms.Label
        lbl_caption_PRIMARYADDRESS = New System.Windows.Forms.Label
        lbl_caption_DESCRIPTION = New System.Windows.Forms.Label
        lbl_caption_MISSIONSTATEMENT = New System.Windows.Forms.Label
        lbl_caption_TOTALFOODRECEIVEDAMOUNT = New System.Windows.Forms.Label
        lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT = New System.Windows.Forms.Label
        lbl_caption_TOTALFOODRECEIVEDWEIGHT = New System.Windows.Forms.Label
        lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT = New System.Windows.Forms.Label
        tlpLayout.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tlpLayout
        '
        tlpLayout.AutoSize = True
        tlpLayout.ColumnCount = 4
        tlpLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        tlpLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        tlpLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        tlpLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        tlpLayout.Controls.Add(Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT, 3, 6)
        tlpLayout.Controls.Add(lbl_caption_TOTALFOODDISTRIBUTED, 2, 1)
        tlpLayout.Controls.Add(lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT, 2, 6)
        tlpLayout.Controls.Add(lbl_caption_TOTALFOODRECEIVED, 2, 0)
        tlpLayout.Controls.Add(Me.lbl_TOTALFOODRECEIVEDWEIGHT, 3, 5)
        tlpLayout.Controls.Add(Me.lbl_TOTALFOODRECEIVED, 3, 0)
        tlpLayout.Controls.Add(lbl_caption_TOTALFOODRECEIVEDWEIGHT, 2, 5)
        tlpLayout.Controls.Add(Me.lbl_TOTALFOODDISTRIBUTED, 3, 1)
        tlpLayout.Controls.Add(Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT, 3, 4)
        tlpLayout.Controls.Add(lbl_caption_TOTALFOODRECEIVEDAMOUNT, 2, 3)
        tlpLayout.Controls.Add(lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT, 2, 4)
        tlpLayout.Controls.Add(Me.lbl_TOTALFOODRECEIVEDAMOUNT, 3, 3)
        tlpLayout.Location = New System.Drawing.Point(396, 3)
        tlpLayout.Name = "tlpLayout"
        tlpLayout.RowCount = 7
        tlpLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        tlpLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        tlpLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        tlpLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        tlpLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        tlpLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        tlpLayout.RowStyles.Add(New System.Windows.Forms.RowStyle)
        tlpLayout.Size = New System.Drawing.Size(393, 83)
        tlpLayout.TabIndex = 0
        '
        'lbl_caption_TOTALFOODDISTRIBUTED
        '
        lbl_caption_TOTALFOODDISTRIBUTED.AutoSize = True
        lbl_caption_TOTALFOODDISTRIBUTED.Location = New System.Drawing.Point(3, 13)
        lbl_caption_TOTALFOODDISTRIBUTED.Name = "lbl_caption_TOTALFOODDISTRIBUTED"
        lbl_caption_TOTALFOODDISTRIBUTED.Size = New System.Drawing.Size(105, 13)
        lbl_caption_TOTALFOODDISTRIBUTED.TabIndex = 10
        lbl_caption_TOTALFOODDISTRIBUTED.Text = "Total Distributed TX:"
        AddHandler lbl_caption_TOTALFOODDISTRIBUTED.Click, AddressOf Me.lbl_caption_TOTALFOODDISTRIBUTED_Click
        '
        'lbl_NAME
        '
        Me.lbl_NAME.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_NAME, "NAME")
        Me.SetFieldMappingLabel(Me.lbl_NAME, lbl_caption_NAME)
        Me.lbl_NAME.Location = New System.Drawing.Point(120, 0)
        Me.lbl_NAME.Name = "lbl_NAME"
        Me.lbl_NAME.Size = New System.Drawing.Size(49, 13)
        Me.lbl_NAME.TabIndex = 1
        Me.lbl_NAME.Text = "<name>"
        Me.SetUseDefaultCaptionColor(Me.lbl_NAME, True)
        '
        'lbl_caption_NAME
        '
        lbl_caption_NAME.AutoSize = True
        lbl_caption_NAME.Location = New System.Drawing.Point(3, 0)
        lbl_caption_NAME.Name = "lbl_caption_NAME"
        lbl_caption_NAME.Size = New System.Drawing.Size(38, 13)
        lbl_caption_NAME.TabIndex = 0
        lbl_caption_NAME.Text = "Name:"
        '
        'lbl_caption_TOTALFOODRECEIVED
        '
        lbl_caption_TOTALFOODRECEIVED.AutoSize = True
        lbl_caption_TOTALFOODRECEIVED.Location = New System.Drawing.Point(3, 0)
        lbl_caption_TOTALFOODRECEIVED.Name = "lbl_caption_TOTALFOODRECEIVED"
        lbl_caption_TOTALFOODRECEIVED.Size = New System.Drawing.Size(77, 13)
        lbl_caption_TOTALFOODRECEIVED.TabIndex = 8
        lbl_caption_TOTALFOODRECEIVED.Text = "Total Rcvd TX:"
        '
        'lbl_TOTALFOODRECEIVED
        '
        Me.lbl_TOTALFOODRECEIVED.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_TOTALFOODRECEIVED, "TOTALFOODRECEIVED")
        Me.SetFieldMappingLabel(Me.lbl_TOTALFOODRECEIVED, lbl_caption_TOTALFOODRECEIVED)
        Me.lbl_TOTALFOODRECEIVED.Location = New System.Drawing.Point(139, 0)
        Me.lbl_TOTALFOODRECEIVED.Name = "lbl_TOTALFOODRECEIVED"
        Me.lbl_TOTALFOODRECEIVED.Size = New System.Drawing.Size(100, 13)
        Me.lbl_TOTALFOODRECEIVED.TabIndex = 9
        Me.lbl_TOTALFOODRECEIVED.Text = "<# food received>"
        Me.SetUseDefaultCaptionColor(Me.lbl_TOTALFOODRECEIVED, True)
        '
        'lbl_TOTALFOODDISTRIBUTED
        '
        Me.lbl_TOTALFOODDISTRIBUTED.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_TOTALFOODDISTRIBUTED, "TOTALFOODDISTRIBUTED")
        Me.SetFieldMappingLabel(Me.lbl_TOTALFOODDISTRIBUTED, lbl_caption_TOTALFOODDISTRIBUTED)
        Me.lbl_TOTALFOODDISTRIBUTED.Location = New System.Drawing.Point(139, 13)
        Me.lbl_TOTALFOODDISTRIBUTED.Name = "lbl_TOTALFOODDISTRIBUTED"
        Me.lbl_TOTALFOODDISTRIBUTED.Size = New System.Drawing.Size(110, 13)
        Me.lbl_TOTALFOODDISTRIBUTED.TabIndex = 11
        Me.lbl_TOTALFOODDISTRIBUTED.Text = "<# food distributed>"
        Me.SetUseDefaultCaptionColor(Me.lbl_TOTALFOODDISTRIBUTED, True)
        '
        'lbl_caption_FOODBANKTYPE
        '
        lbl_caption_FOODBANKTYPE.AutoSize = True
        lbl_caption_FOODBANKTYPE.Location = New System.Drawing.Point(3, 22)
        lbl_caption_FOODBANKTYPE.Name = "lbl_caption_FOODBANKTYPE"
        lbl_caption_FOODBANKTYPE.Size = New System.Drawing.Size(88, 13)
        lbl_caption_FOODBANKTYPE.TabIndex = 4
        lbl_caption_FOODBANKTYPE.Text = "Food Bank Type:"
        '
        'lbl_FOODBANKTYPE
        '
        Me.lbl_FOODBANKTYPE.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_FOODBANKTYPE, "FOODBANKTYPE")
        Me.SetFieldMappingLabel(Me.lbl_FOODBANKTYPE, lbl_caption_FOODBANKTYPE)
        Me.lbl_FOODBANKTYPE.Location = New System.Drawing.Point(120, 22)
        Me.lbl_FOODBANKTYPE.Name = "lbl_FOODBANKTYPE"
        Me.lbl_FOODBANKTYPE.Size = New System.Drawing.Size(45, 13)
        Me.lbl_FOODBANKTYPE.TabIndex = 5
        Me.lbl_FOODBANKTYPE.Text = "<type>"
        Me.SetUseDefaultCaptionColor(Me.lbl_FOODBANKTYPE, True)
        '
        'lbl_caption_PRIMARYADDRESS
        '
        lbl_caption_PRIMARYADDRESS.AutoSize = True
        lbl_caption_PRIMARYADDRESS.Location = New System.Drawing.Point(3, 44)
        lbl_caption_PRIMARYADDRESS.Name = "lbl_caption_PRIMARYADDRESS"
        lbl_caption_PRIMARYADDRESS.Size = New System.Drawing.Size(89, 13)
        lbl_caption_PRIMARYADDRESS.TabIndex = 6
        lbl_caption_PRIMARYADDRESS.Text = "Primary Address:"
        '
        'lbl_PRIMARYADDRESS
        '
        Me.lbl_PRIMARYADDRESS.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_PRIMARYADDRESS, "PRIMARYADDRESS")
        Me.SetFieldMappingLabel(Me.lbl_PRIMARYADDRESS, lbl_caption_PRIMARYADDRESS)
        Me.lbl_PRIMARYADDRESS.Location = New System.Drawing.Point(120, 44)
        Me.lbl_PRIMARYADDRESS.Name = "lbl_PRIMARYADDRESS"
        Me.lbl_PRIMARYADDRESS.Size = New System.Drawing.Size(61, 13)
        Me.lbl_PRIMARYADDRESS.TabIndex = 7
        Me.lbl_PRIMARYADDRESS.Text = "<address>"
        Me.SetUseDefaultCaptionColor(Me.lbl_PRIMARYADDRESS, True)
        '
        'lbl_caption_DESCRIPTION
        '
        lbl_caption_DESCRIPTION.AutoSize = True
        lbl_caption_DESCRIPTION.Location = New System.Drawing.Point(399, 124)
        lbl_caption_DESCRIPTION.Name = "lbl_caption_DESCRIPTION"
        lbl_caption_DESCRIPTION.Size = New System.Drawing.Size(64, 13)
        lbl_caption_DESCRIPTION.TabIndex = 2
        lbl_caption_DESCRIPTION.Text = "Description:"
        AddHandler lbl_caption_DESCRIPTION.Click, AddressOf Me.lbl_caption_DESCRIPTION_Click
        '
        'lbl_caption_MISSIONSTATEMENT
        '
        lbl_caption_MISSIONSTATEMENT.AutoSize = True
        lbl_caption_MISSIONSTATEMENT.Location = New System.Drawing.Point(6, 124)
        lbl_caption_MISSIONSTATEMENT.Name = "lbl_caption_MISSIONSTATEMENT"
        lbl_caption_MISSIONSTATEMENT.Size = New System.Drawing.Size(98, 13)
        lbl_caption_MISSIONSTATEMENT.TabIndex = 7
        lbl_caption_MISSIONSTATEMENT.Text = "Mission Statement:"
        '
        'txt_DESCRIPTION
        '
        Me.txt_DESCRIPTION.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SetFieldMappingFieldID(Me.txt_DESCRIPTION, "DESCRIPTION")
        Me.SetFieldMappingLabel(Me.txt_DESCRIPTION, lbl_caption_DESCRIPTION)
        Me.txt_DESCRIPTION.Location = New System.Drawing.Point(469, 124)
        Me.txt_DESCRIPTION.Multiline = True
        Me.txt_DESCRIPTION.Name = "txt_DESCRIPTION"
        Me.txt_DESCRIPTION.ReadOnly = True
        Me.txt_DESCRIPTION.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_DESCRIPTION.Size = New System.Drawing.Size(320, 93)
        Me.txt_DESCRIPTION.TabIndex = 4
        Me.txt_DESCRIPTION.Text = "description"
        '
        'txt_MISSIONSTATEMENt
        '
        Me.txt_MISSIONSTATEMENt.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SetFieldMappingFieldID(Me.txt_MISSIONSTATEMENt, "MISSIONSTATEMENT")
        Me.SetFieldMappingLabel(Me.txt_MISSIONSTATEMENt, lbl_caption_MISSIONSTATEMENT)
        Me.txt_MISSIONSTATEMENt.Location = New System.Drawing.Point(110, 124)
        Me.txt_MISSIONSTATEMENt.Multiline = True
        Me.txt_MISSIONSTATEMENt.Name = "txt_MISSIONSTATEMENt"
        Me.txt_MISSIONSTATEMENt.ReadOnly = True
        Me.txt_MISSIONSTATEMENt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_MISSIONSTATEMENt.Size = New System.Drawing.Size(280, 91)
        Me.txt_MISSIONSTATEMENt.TabIndex = 6
        Me.txt_MISSIONSTATEMENt.Text = "mission statement"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.49096!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.50904!))
        Me.TableLayoutPanel1.Controls.Add(lbl_caption_NAME, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_NAME, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_FOODBANKTYPE, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(lbl_caption_FOODBANKTYPE, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(lbl_caption_PRIMARYADDRESS, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lbl_PRIMARYADDRESS, 1, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(387, 111)
        Me.TableLayoutPanel1.TabIndex = 9
        '
        'lbl_caption_TOTALFOODRECEIVEDAMOUNT
        '
        lbl_caption_TOTALFOODRECEIVEDAMOUNT.AutoSize = True
        lbl_caption_TOTALFOODRECEIVEDAMOUNT.Location = New System.Drawing.Point(3, 26)
        lbl_caption_TOTALFOODRECEIVEDAMOUNT.Name = "lbl_caption_TOTALFOODRECEIVEDAMOUNT"
        lbl_caption_TOTALFOODRECEIVEDAMOUNT.Size = New System.Drawing.Size(122, 13)
        lbl_caption_TOTALFOODRECEIVEDAMOUNT.TabIndex = 10
        lbl_caption_TOTALFOODRECEIVEDAMOUNT.Text = "Total Received Amount:"
        '
        'lbl_TOTALFOODRECEIVEDAMOUNT
        '
        Me.lbl_TOTALFOODRECEIVEDAMOUNT.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_TOTALFOODRECEIVEDAMOUNT, "TOTALFOODRECEIVEDAMOUNT")
        Me.SetFieldMappingLabel(Me.lbl_TOTALFOODRECEIVEDAMOUNT, lbl_caption_TOTALFOODRECEIVEDAMOUNT)
        Me.lbl_TOTALFOODRECEIVEDAMOUNT.Location = New System.Drawing.Point(139, 26)
        Me.lbl_TOTALFOODRECEIVEDAMOUNT.Name = "lbl_TOTALFOODRECEIVEDAMOUNT"
        Me.lbl_TOTALFOODRECEIVEDAMOUNT.Size = New System.Drawing.Size(110, 13)
        Me.lbl_TOTALFOODRECEIVEDAMOUNT.TabIndex = 11
        Me.lbl_TOTALFOODRECEIVEDAMOUNT.Text = " <Received Amount>"
        Me.SetUseDefaultCaptionColor(Me.lbl_TOTALFOODRECEIVEDAMOUNT, True)
        '
        'lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT
        '
        lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT.AutoSize = True
        lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT.Location = New System.Drawing.Point(3, 39)
        lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT.Name = "lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT"
        lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT.Size = New System.Drawing.Size(130, 13)
        lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT.TabIndex = 12
        lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT.Text = "Total Distributed Amount:"
        '
        'lbl_TOTALFOODDISTRIBUTEDAMOUNT
        '
        Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT, "TOTALFOODDISTRIBUTEDAMOUNT")
        Me.SetFieldMappingLabel(Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT, lbl_caption_TOTALFOODDISTRIBUTEDAMOUNT)
        Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT.Location = New System.Drawing.Point(139, 39)
        Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT.Name = "lbl_TOTALFOODDISTRIBUTEDAMOUNT"
        Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT.Size = New System.Drawing.Size(115, 13)
        Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT.TabIndex = 13
        Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT.Text = "<Distributed Amount>"
        Me.SetUseDefaultCaptionColor(Me.lbl_TOTALFOODDISTRIBUTEDAMOUNT, True)
        '
        'lbl_caption_TOTALFOODRECEIVEDWEIGHT
        '
        lbl_caption_TOTALFOODRECEIVEDWEIGHT.AutoSize = True
        lbl_caption_TOTALFOODRECEIVEDWEIGHT.Location = New System.Drawing.Point(3, 52)
        lbl_caption_TOTALFOODRECEIVEDWEIGHT.Name = "lbl_caption_TOTALFOODRECEIVEDWEIGHT"
        lbl_caption_TOTALFOODRECEIVEDWEIGHT.Size = New System.Drawing.Size(101, 13)
        lbl_caption_TOTALFOODRECEIVEDWEIGHT.TabIndex = 14
        lbl_caption_TOTALFOODRECEIVEDWEIGHT.Text = "Total Received Lbs:"
        '
        'lbl_TOTALFOODRECEIVEDWEIGHT
        '
        Me.lbl_TOTALFOODRECEIVEDWEIGHT.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_TOTALFOODRECEIVEDWEIGHT, "TOTALFOODRECEIVEDWEIGHT")
        Me.SetFieldMappingLabel(Me.lbl_TOTALFOODRECEIVEDWEIGHT, lbl_caption_TOTALFOODRECEIVEDWEIGHT)
        Me.lbl_TOTALFOODRECEIVEDWEIGHT.Location = New System.Drawing.Point(139, 52)
        Me.lbl_TOTALFOODRECEIVEDWEIGHT.Name = "lbl_TOTALFOODRECEIVEDWEIGHT"
        Me.lbl_TOTALFOODRECEIVEDWEIGHT.Size = New System.Drawing.Size(86, 13)
        Me.lbl_TOTALFOODRECEIVEDWEIGHT.TabIndex = 15
        Me.lbl_TOTALFOODRECEIVEDWEIGHT.Text = "<Received Lbs>"
        Me.SetUseDefaultCaptionColor(Me.lbl_TOTALFOODRECEIVEDWEIGHT, True)
        '
        'lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT
        '
        lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT.AutoSize = True
        lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT.Location = New System.Drawing.Point(3, 65)
        lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT.Name = "lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT"
        lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT.Size = New System.Drawing.Size(109, 13)
        lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT.TabIndex = 16
        lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT.Text = "Total Distributed Lbs:"
        '
        'lbl_TOTALFOODDISTRIBUTEDWEIGHT
        '
        Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT.AutoSize = True
        Me.SetFieldMappingFieldID(Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT, "TOTALFOODDISTRIBUTEDWEIGHT")
        Me.SetFieldMappingLabel(Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT, lbl_caption_TOTALFOODDISTRIBUTEDWEIGHT)
        Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT.Location = New System.Drawing.Point(139, 65)
        Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT.Name = "lbl_TOTALFOODDISTRIBUTEDWEIGHT"
        Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT.Size = New System.Drawing.Size(94, 13)
        Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT.TabIndex = 17
        Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT.Text = "<Distributed Lbs>"
        Me.SetUseDefaultCaptionColor(Me.lbl_TOTALFOODDISTRIBUTEDWEIGHT, True)
        '
        'FoodBankSummaryView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.txt_MISSIONSTATEMENt)
        Me.Controls.Add(tlpLayout)
        Me.Controls.Add(Me.txt_DESCRIPTION)
        Me.Controls.Add(lbl_caption_MISSIONSTATEMENT)
        Me.Controls.Add(lbl_caption_DESCRIPTION)
        Me.Name = "FoodBankSummaryView"
        Me.Size = New System.Drawing.Size(792, 220)
        tlpLayout.ResumeLayout(False)
        tlpLayout.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents lbl_NAME As System.Windows.Forms.Label
    Private WithEvents lbl_TOTALFOODDISTRIBUTED As System.Windows.Forms.Label
    Private WithEvents lbl_FOODBANKTYPE As System.Windows.Forms.Label
    Private WithEvents lbl_TOTALFOODRECEIVED As System.Windows.Forms.Label
    Private WithEvents lbl_PRIMARYADDRESS As System.Windows.Forms.Label
    Friend WithEvents txt_DESCRIPTION As System.Windows.Forms.TextBox
    Friend WithEvents txt_MISSIONSTATEMENt As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Private WithEvents lbl_TOTALFOODDISTRIBUTEDAMOUNT As System.Windows.Forms.Label
    Private WithEvents lbl_TOTALFOODRECEIVEDAMOUNT As System.Windows.Forms.Label
    Private WithEvents lbl_TOTALFOODDISTRIBUTEDWEIGHT As System.Windows.Forms.Label
    Private WithEvents lbl_TOTALFOODRECEIVEDWEIGHT As System.Windows.Forms.Label

End Class
