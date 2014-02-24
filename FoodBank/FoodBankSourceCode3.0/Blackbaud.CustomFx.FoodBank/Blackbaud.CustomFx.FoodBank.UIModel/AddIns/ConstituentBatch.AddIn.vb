Imports Blackbaud.AppFx.BatchUI

Public NotInheritable Class ConstituentBatchBatchEntryHandler
	Inherits Blackbaud.AppFx.BatchUI.BatchEntryHandler   

    'Let's establish a FieldChangedHandler for the Address type code column in the batch ui.
    'If the user selected "Business" from the Address Type drop down list within the batch ui grid
    ' then default the Country field to 'Australia'

    ' The AfterLoad event fires after the event handler is loaded.  This event is commonly 
    ' used to define business rules on the fields within the Webshell user interface grid.  
    ' For example, FieldChangedHandlers can be added to respond to a change to a field.
    ' (Docs: http://bit.ly/ZILk87)
    Private Sub ConstituentBatchBatchEntryHandler_AfterLoad(sender As Object, e As System.EventArgs) Handles Me.AfterLoad

        'Not all batches, which are created from a batch template, 
        ' will contain every field defined within the batch type. 
        ' Therefore, the developer can use FieldIsDefined to check and 
        ' see if the field exists within the batch prior to coding something 
        ' that depends on the field’s existence. (Docs:  http://bit.ly/11WbDeF)
        If FieldIsDefined("ADDRESS_ADDRESSTYPECODEID") Then

            'FieldChangedHandlers has an Add procedure which is used to add a FieldEventHandler 
            ' for a particular field within the batch grid user interface.  
            ' When the assigned field changes, the AddressOf operator is used to 
            ' designate a delegate to handle the event. (Docs: http://bit.ly/ZIKEPP)
            Me.FieldChangedHandlers.Add("ADDRESS_ADDRESSTYPECODEID", AddressOf DefaultBusinessAddressDetails)
        End If

    End Sub

    'Here is the delegate to handle the event.
    Private Sub DefaultBusinessAddressDetails(ByVal e As FieldEventArgBase)

        'Grab the value from the ADDRESS_ADDRESSTYPECODEID field via e.Field.Name
        Dim ID = DirectCast(GetValueTranslationFromFieldID(e.Model, e.Field.Name, ""), String)

        If ID.Equals(String.Empty) Then Return

        'If the user selected "Business" from the Address Type drop down list within the batch ui grid
        ' then default the Country field to 'Australia' by using the appropriate primary key/ID column
        ' value from the Country table. 
        If ID = "Business" Then
            If Me.FieldIsDefined("ADDRESS_COUNTRYID") Then
                TrySetValueForFieldID(e.Model, "ADDRESS_COUNTRYID", "F189F24C-2538-46A1-8458-1E3F3967B843")
            End If
        End If
    End Sub
End Class

