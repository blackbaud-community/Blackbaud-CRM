Public Class FoodBankTransactionHeaderAddForm2UIModel

    Private Sub FoodBankTransactionHeaderAddForm2UIModel_Loaded(ByVal sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded
        'Ensure We Can Only Select 1 Food Item Row at a Time
        Me.FOODITEMS.SelectionMode = SelectionMode.Single
    End Sub

    Private Sub _addfooditem_DataFormSaved(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.DataFormSavedEventArgs) Handles _addfooditem.DataFormSaved
        'Using the UIAction xml tag within the spec to open the FoodItem.Add.xml add data form via the "+" button
        '<c:UIAction ActionID="ADDFOODITEM" Caption="+" Description="Add Food Item">
        '    <c:ShowAddForm DataFormInstanceID="b911e219-42f2-4b64-9f02-d9e2ff899799" />
        '</c:UIAction>
        'Selecting the "+" button opens the Food Item Add Data Form which
        'Enables a user to add a new food item "on the fly"
        'After the user saves the new food item, we catch the event here and update the
        'data source on each simple datalist within the grid to reflect the new food item
        For Each FoodBankTransactionHeaderAddForm2FOODITEMSUIModel In Me.FOODITEMS.Value
            FoodBankTransactionHeaderAddForm2FOODITEMSUIModel.FOODITEMID.ResetDataSource()
        Next

    End Sub

    Private Sub _fooditems_SelectionChanged(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.SelectionChangedEventArgs) Handles _fooditems.SelectionChanged
        'IF someone adds a new row to the grid then
        'syncronize the value changed event on the simple data list (FoodItemID) to a 
        'handler named FoodItemSimpleDataListChangedHandler
        'Whenever the user selects a food item in the drop down list, we will default the value of the food item

        For Each FoodBankTransactionHeaderAddForm2FOODITEMSUIModel In Me.FOODITEMS.Value
            AddHandler FoodBankTransactionHeaderAddForm2FOODITEMSUIModel.FOODITEMID.ValueChanged, AddressOf FoodItemSimpleDataListChangedHandler
        Next
    End Sub

    Private Sub FoodItemSimpleDataListChangedHandler(ByVal sender As Object, _
            ByVal e As ValueChangedEventArgs)
        'Whenever the user selects a food item in the drop down list, we will default the value of the food item
        'Leverage the FoodItem.Edit.xml edit data form to retrieve the default amount for the selected food item
        'GetFoodItemDefaultValues accepts the dataform instance id of the data form and the selected value (GUID)
        ' form the data list.  GetFoodItemDefaultValues returns a reply that contains the values of the food item record.
        'Dim reply = GetFoodItemDefaultValues("6F80152D-4BD4-43fa-9F73-14AB44A90954", e.NewValue.ToString)
        Dim reply = GetDefaultValues("6F80152D-4BD4-43fa-9F73-14AB44A90954", e.NewValue.ToString)


        Me.FOODITEMS.SelectedItems(0).FOODITEMAMOUNT.Value = reply.DataFormItem.Values("CURRENTCOST").Value.ToString
        Me.FOODITEMS.SelectedItems(0).QUANTITY.Value = "1"
    End Sub


    'Private Function GetFoodItemDefaultValues(ByVal DataFormInstanceID As String, ByVal RecordID As String) As Blackbaud.AppFx.Server.DataFormLoadReply
    '    'Fetch data using a DataFormLoadRequest.
    '    'The DataFormInstanceID indentifies a data form
    '    'RecordID identifies a record that the data form will retrieve.

    '    Dim args = New DataFormLoadArgs
    '    args.Context = Me.GetRequestContext()
    '    args.DataFormInstanceId = New Guid(DataFormInstanceID)
    '    args.RecordId = RecordID
    '    args.SecurityContext = Me.GetRequestSecurityContext()
    '    args.ExcludeValueTranslations = True
    '    Dim reply = UIModelUtility.GetDataFormValues(args)



    '    If reply IsNot Nothing Then
    '        Return reply
    '    Else
    '        Return Nothing
    '    End If
    'End Function

    Private Function GetDefaultValues(ByVal DataFormInstanceID As String, ByVal RecordID As String) As Blackbaud.AppFx.Server.DataFormLoadReply
        'Fetch data using a DataFormLoadRequest.
        'The DataFormInstanceID indentifies a data form
        'RecordID identifies a record that the data form will retrieve.

        Dim request As New Blackbaud.AppFx.Server.DataFormLoadRequest
        Dim reply As Blackbaud.AppFx.Server.DataFormLoadReply = Nothing

        Try
            request.FormID = New Guid(DataFormInstanceID)
            request.SecurityContext = Me.GetRequestSecurityContext
            request.RecordID = RecordID
            request.ExcludeValueTranslations = True


            ' When making calls to features within the UIModel code, 
            ' go through an instance of the AppFxWebService class.
            ' Note that when you go through the AppFxWebService class you are not making 
            ' a SOAP or HTTP call, you are still just invoking methods on a .net class directly, 
            ' so there is very little, almost immeasurable overhead. 
            ' Note even though this is not a web service call, you will be going through 
            ' the same code paths that any code would go through that called the webservice 
            ' from a client application
            ' Using the AppFxWebService class includes the raising of certain WebHealth events and 
            ' logging requests to the dbo.WSREQUESTLOG table (if configured via web.config)
            Dim svc = New Blackbaud.AppFx.Server.AppFxWebService(Me.GetRequestContext())

            'reply = Blackbaud.AppFx.Server.DataFormLoad(request, Me.GetRequestContext, True)
            reply = svc.DataFormLoad(request)


        Catch eat As Exception
            Throw
        End Try

        If Not reply Is Nothing Then
            Return reply
        Else
            Return Nothing
        End If

    End Function


End Class