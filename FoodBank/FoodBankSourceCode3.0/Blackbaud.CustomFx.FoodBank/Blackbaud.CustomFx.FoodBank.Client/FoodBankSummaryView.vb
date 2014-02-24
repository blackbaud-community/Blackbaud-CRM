Imports Blackbaud.AppFx.XmlTypes
Imports Blackbaud.AppFx.Browser.DataForms

''' <summary>
''' A client-side UI implementation of a DataForm.  This component is referenced by a server-side data form spec and is loaded
''' and saved by implementations defined by the spec.
''' </summary>
Public Notinheritable Class FoodBankSummaryView

    'Note:  additional overrides and events are available (only the most typically used are listed here)

    ''' <summary>
    ''' Peform any necessary UI setup such as form sizing
    ''' </summary>
    Public Overrides Sub InitControlsFromMetaData()
        MyBase.InitControlsFromMetaData()

        'add UI setup code here

    End Sub

    ''' <summary>
    ''' Called when the form is loaded
    ''' </summary>
    ''' <param name="item">Contains the loaded field values returned from the Load implementation defined by the spec.</param>
    Public Overrides Sub LoadControlsFromDataFormItem(ByVal item As Blackbaud.AppFx.XmlTypes.DataForms.DataFormItem)
        MyBase.LoadControlsFromDataFormItem(item)
        
        'All mapped controls have been loaded via MyBase.LoadControlsFromDataFormItem.  At this point, we can handle any 
        'special controls that are loaded manually.
        'ex:  Dim s As String = CStr(GetFieldValue(item, "CUSTOMFIELD1"))

    End Sub

    ''' <summary>
    ''' Called when the form is saved
    ''' </summary>
    ''' <returns>A data form item containing the values from the form to be saved by the Save implementation defined by the spec.</returns>
    Public Overrides Function BuildDataFormItemFromControls() As Blackbaud.AppFx.XmlTypes.DataForms.DataFormItem
        Dim dfi As DataFormItem = MyBase.BuildDataFormItemFromControls()

        'All mapped controls have been packaged into dfi at this point.  Now we can package up any unmapped control contents
        'into dfi.
        'ex:  SetFieldValue(dfi, "CUSTOMFIELD1", s)

        Return dfi

    End Function

    ''' <summary>
    ''' Called when the Save button is clicked on a dataform.  This is used to provide any custom validation for the form.
    ''' </summary>
    ''' <returns>True or false indicating whether or not the form contents are valid.</returns>
    Public Overrides Function ValidateAllControlContents() As Boolean
        Dim bres As Boolean = MyBase.ValidateAllControlContents()

        If bres Then
            'All mapped controls have been validated at this point.  Add any custom validation here and set bres accordingly.
        End If

        Return bres

    End Function

    Private Sub lbl_caption_TOTALFOODDISTRIBUTED_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lbl_caption_DESCRIPTION_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lbl_PRIMARYADDRESS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_PRIMARYADDRESS.Click

    End Sub

    Private Sub lbl_TOTALFOODRECEIVEDAMOUNT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_TOTALFOODRECEIVEDAMOUNT.Click

    End Sub
End Class
