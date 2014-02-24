Option Strict On

Imports Blackbaud.AppFx.XmlTypes
Imports Core = Blackbaud.AppFx.UIModeling.Core
Imports Blackbaud.AppFx.Server
Imports Blackbaud.AppFx.XmlTypes.DataForms
Imports Blackbaud.AppFx.BatchUI
'Imports Blackbaud.AppFx.BatchUI.DeferredConditions
Imports Blackbaud.AppFx.BatchUI.BatchShowCustomFormAction

Friend Enum TXTypeCodes As Integer
    Receive = 0
    Distribute = 1
End Enum

Public NotInheritable Class FoodBankTransactionAddBatchHandler
    Inherits Blackbaud.AppFx.BatchUI.BatchEntryHandler

#Region " Constants and Memvars"
    Private Const PRIMARYCONTEXTRECORDID As String = "PRIMARYCONTEXTRECORDID"
    Private Const CONSTITUENTID As String = "CONSTITUENTID"
    Private Const FOODBANKTXTYPECODE As String = "FOODBANKTXTYPECODE"
    Private Const TRANSACTIONDATE As String = "TXDATE"
    Private Const RECEIPTPRINTED As String = "RECEIPTPRINTED"
    Private Const FOODITEMS_FIELDID As String = "FOODITEMS"
    Private Shared _FOODBANKTXFIELDS As String() = New String() { _
                        CONSTITUENTID, _
                        FOODBANKTXTYPECODE, _
                        TRANSACTIONDATE, _
                        RECEIPTPRINTED, _
                        FOODITEMS_FIELDID}

    'Reconcile Batch Food Items Custom UI Model Dialog Screen constants and memvar
    Private Const DATAFORMID_FOODITEMINFO As String = "6F80152D-4BD4-43fa-9F73-14AB44A90954"
    Private Const FBTXBATCHVSCURINVRECONCILE_ASSEMBLYNAME As String = "Blackbaud.CustomFx.FoodBank.UIModel.dll"
    Private Const FBTXBATCHVSCURINVRECONCILE_CLASSNAME As String = "Blackbaud.CustomFx.FoodBank.UIModel.FBTXBatchReconcileCustomUIModel"
    Private Const FBTXBATCHVSCURINVRECONCILE_BUTTONKEY As String = "FBTXBATCHVSCURINVRECONCILEBUTTONKEY"
    Private Const FBTXBATCHVSCURINVRECONCILE_IMAGEKEY As String = "RES:reportspec"
    Private _FBTXBatchVsCurInvReconcileAction As BatchShowCustomFormAction = Nothing

    'Food Items Child Custom UI Model Dialog Screen constants and memvar
    Private Const FBTXBATCHFOODITEMS_ASSEMBLYNAME As String = "Blackbaud.CustomFx.FoodBank.UIModel.dll"
    Private Const FBTXBATCHFOODITEMS_CLASSNAME As String = "Blackbaud.CustomFx.FoodBank.UIModel.FBTXBatchFoodItemsCustomUIModel"
    Private Const FBTXBATCHFOODITEMS_BUTTONKEY As String = "FBTXBATCHFOODITEMSBUTTONKEY"
    Private Const FBTXBATCHFOODITEMS_IMAGEKEY As String = "RES:reportspec"
    Private _FBTXBatchFoodItemsAction As BatchShowCustomFormAction = Nothing

#End Region

#Region "Events"

    '  Inheriting the Blackbaud.AppFx.BatchUI.BatchEntryHandler base class enables you to handle 
    '  the AfterLoad event and thereby the opportunity to add a “field handlers” to the batch.  
    '  The event handler collections (FieldChangeHandlers, ValidateFieldHandlers, LeaveCellHandlers, EnterCellHandlers) 
    '  are simply a means to simplify the code by allowing the batch handler to be specific in its response to the events.  
    Private Sub FoodBankTransactionAddBatchHandler_AfterLoad(sender As Object, e As System.EventArgs) Handles Me.AfterLoad
        '  The BatchEntryHandler base class’ ValidateFieldHandlers property allows you to add a ValidateFieldHandler 
        '  for a particular field within the batch grid user interface.  When the assigned field changes, 
        '  the AddressOf operator is used to designate a delegate to handle the event.    
        '  The AddressOf operator creates a function delegate that points to a function specified by the procedurename.
        '  A ValidateFieldHandler is being added to the TRANSACTIONDATE field.  When the event fires, 
        '  a function delegate named ValidateDate will be called to examine the date and add an annotation (message) 
        '  to the field, if needed. The annotation will warn the end user if the date value is in the future.
        Me.ValidateFieldHandlers.Add(TRANSACTIONDATE, AddressOf ValidateDate)

        With Me.FieldChangedHandlers
            '  Similar to the ValidateFieldHandlers property, the FieldChangedHandlers property allows you 
            '  to add a FieldEventHandler for a particular field within the batch grid user interface.  
            '  When the assigned field changes, the AddressOf operator is used to designate a delegate to 
            '  handle the event.   The code below adds a FieldChangedHandler to the batch.  
            '  When the food item id drop down value changes within the transaction detail child grid, 
            '  the GetFoodItemDetails delegate will retrieve default data using the DataFormLoad function.
            If FieldIsDefined("FOODITEMS") Then .Add("FOODITEMS\FOODITEMID", AddressOf GetFoodItemDetails)
            'A common duty for a handler is to clear out field values when a field changes. 
            'Add a handler within the AfterLoad event, just like the past two examples.  
            'In the code below a field changed hander is being added for the PRIMARYCONTEXTRECORDID 
            'form field which represents the food bank.  
            'When a bank is selected, the ClearFieldsOnFoodBankContextChange delegate 
            'function is called which clears the field values for all the form fields listed by 
            'the _FOODBANKTXFIELDS string array.
            .Add(PRIMARYCONTEXTRECORDID, AddressOf ClearFieldsOnFoodBankContextChange)
        End With
    End Sub

    Private Sub FoodBankTransactionAddBatchHandler_DefineFieldRules(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Me.DefineFieldRules
        With Me.FieldRules.ValidInContextRules
            'We can add a field rule to the FieldRules.ValidInContextRules collection that 
            'will be used to determine if the field is valid in the given context by associating 
            'the Receipt Printed field within the batch with a deferred condition object.  
            'When a field becomes not valid in context, then the field’s value is cleared, 
            'the field is disabled, and the field is marked as not required. 
            'When a field becomes valid in context then the field will be enabled 
            'and be populated with the default value.
            'When the Transaction Type is set to Distribute, the Receipt Printed field 
            'will be cleared and disabled. 
            .Add(RECEIPTPRINTED, FieldValue(FOODBANKTXTYPECODE) = TXTypeCodes.Receive)
        End With
    End Sub



    'With Me.FieldRules.VisibleRules
    '    .Add(RECEIPTPRINTED, FieldValue(FOODBANKTXTYPECODE) = TXTypeCodes.Receive)
    '    .Add(TRANSACTIONDATE, FieldValue(FOODBANKTXTYPECODE) = TXTypeCodes.Receive)
    'End With
    Private Sub FoodBankTransactionAddBatchHandler_DefineActions(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DefineActions
        If Me.Actions.Count = 0 Then
            'Dim actiongroup = New BatchActionGroup("FBTX_GRP", "Transactions")

            'actiongroup.Add(FBTXBatchVsCurInvReconcileAction)
            'actiongroup.Add(FBTXBatchFoodItemsAction)
            'Me.Actions.Add(actiongroup)

            '''' Comment out the code above and uncomment the code below 
            '''' to add the action to an action group within an action region.
            Dim actionregion As New BatchActionRegion("FBTX_REGION", "Tx Region") With {.ShortKey = "K"}
            Dim actiongroup = New BatchActionGroup("FBTX_GRP", "Transactions")
            'actiongroup.Add(FBTXBatchVsCurInvReconcileAction)
            actiongroup.Add(FBTXBatchFoodItemsAction)
            actionregion.Add(actiongroup)
            Me.Actions.Add(actionregion)
        End If
    End Sub

    Private Sub FoodBankTransactionAddBatchHandler_ActionInvoked(ByVal sender As Object, ByVal e As BatchActionEventArgs) Handles Me.ActionInvoked
        If e.Action Is _FBTXBatchVsCurInvReconcileAction Then
            Select Case e.InvokeArgs.OriginatingEventName
                Case EVENT_INVOKEACTION
                    Dim args As ShowCustomFormEventArgs = TryCast(e.InvokeArgs.OriginatingEventArgs, ShowCustomFormEventArgs)
                    If args IsNot Nothing Then
                        SetEditableState(DirectCast(args.Model, CustomUIModel))

                        ReconcileActionInvokeHandler(e.Model, args.Model)
                    End If

            End Select
        ElseIf e.Action Is _FBTXBatchFoodItemsAction Then
            Select Case e.InvokeArgs.OriginatingEventName
                Case EVENT_INVOKEACTION
                    Dim args As ShowCustomFormEventArgs = TryCast(e.InvokeArgs.OriginatingEventArgs, ShowCustomFormEventArgs)
                    If args IsNot Nothing Then
                        SetEditableState(DirectCast(args.Model, CustomUIModel))
                        FoodItemsActionInvokeHandler(e.Model, args.Model)
                    End If
                Case EVENT_CUSTOMFORMCONFIRMED
                    Dim args As CustomFormConfirmedEventArgs = TryCast(e.InvokeArgs.OriginatingEventArgs, CustomFormConfirmedEventArgs)
                    If args IsNot Nothing Then
                        FoodItemsActionConfirmedHandler(e.Model, args.Model)
                    End If
            End Select
        End If
    End Sub

    '''''' Uncomment this event handler to see an example of handling the SearchComplete Event.  
    ''''''Private Sub FoodBankTransactionAddBatchHandler_SearchComplete(sender As Object, e As AppFx.BatchUI.SearchCompleteEventArgs) Handles Me.SearchComplete

    ''''''    If FieldIsOneOf(e.Field, PRIMARYCONTEXTRECORDID, CONSTITUENTID) Then
    ''''''        Me.AddFieldAnnotation(e.Field, AnnotationCategory.BusinessRuleError, _
    ''''''                       "The " & e.Field.Name & " search just occured!", _
    ''''''                        True)
    ''''''    End If
    ''''''End Sub

#End Region

#Region "Actions and Handlers"

    'Private ReadOnly Property FBTXBatchVsCurInvReconcileAction() As BatchShowCustomFormAction
    '    Get
    '        If _FBTXBatchVsCurInvReconcileAction Is Nothing Then
    '            _FBTXBatchVsCurInvReconcileAction = New BatchShowCustomFormAction(FBTXBATCHVSCURINVRECONCILE_BUTTONKEY, "Reconcile",
    '                FBTXBATCHVSCURINVRECONCILE_IMAGEKEY, FBTXBATCHVSCURINVRECONCILE_ASSEMBLYNAME, FBTXBATCHVSCURINVRECONCILE_CLASSNAME)


    '            'If the food bank (PRIMARYCONTEXTRECORDID) column has a value then enable the action button.
    '            _FBTXBatchVsCurInvReconcileAction.EnabledRule = FieldHasValue("PRIMARYCONTEXTRECORDID")

    '            _FBTXBatchVsCurInvReconcileAction.ShortKey = "R"
    '        End If
    '        Return _FBTXBatchVsCurInvReconcileAction
    '    End Get
    'End Property

    Private ReadOnly Property FBTXBatchFoodItemsAction() As BatchShowCustomFormAction
        Get
            If _FBTXBatchFoodItemsAction Is Nothing Then
                _FBTXBatchFoodItemsAction = New BatchShowCustomFormAction(FBTXBATCHFOODITEMS_BUTTONKEY, "Food Items",
                    FBTXBATCHFOODITEMS_IMAGEKEY, FBTXBATCHFOODITEMS_ASSEMBLYNAME, FBTXBATCHFOODITEMS_CLASSNAME)

                'If the food bank (PRIMARYCONTEXTRECORDID) column has a value then enable the action button.
                _FBTXBatchFoodItemsAction.EnabledRule = FieldHasValue("PRIMARYCONTEXTRECORDID")
                _FBTXBatchFoodItemsAction.ShortKey = "F"
            End If
            Return _FBTXBatchFoodItemsAction
        End Get
    End Property

    Private Sub ReconcileActionInvokeHandler(ByVal batchModel As Core.UIModel, ByVal reconcileUIModel As Core.UIModel)

        TrySetValueForFieldID(reconcileUIModel, "BATCHID", Me.BatchID)
        TrySetValueForFieldID(reconcileUIModel, "FOODBANKID", DirectCast(GetValueFromFieldID(batchModel, "PRIMARYCONTEXTRECORDID", Guid.Empty), Guid))

    End Sub

    Private Sub FoodItemsActionInvokeHandler(ByVal batchModel As Core.UIModel, ByVal FoodItemsCustomUIModelDialogFormModel As Core.UIModel)
        TrySetValueForFieldID(FoodItemsCustomUIModelDialogFormModel, FOODITEMS_FIELDID, GetValueFromFieldID(batchModel, FOODITEMS_FIELDID, Nothing))
    End Sub

    Private Shared Sub FoodItemsActionConfirmedHandler(ByVal batchModel As Core.UIModel, ByVal FoodItemsCustomUIModelDialogFormModel As Core.UIModel)
        TrySetValueForFieldID(batchModel, FOODITEMS_FIELDID, GetValueFromFieldID(FoodItemsCustomUIModelDialogFormModel, FOODITEMS_FIELDID, Nothing))
    End Sub

#End Region

#Region " Helper Functions "

    Private Sub ValidateDate(ByVal e As BatchUI.BatchValidateFieldArgs)
        If IsTransactionDateInFuture(e.Model, e.Field.Name) = True Then
            Me.AddFieldAnnotation(e.Field, AnnotationCategory.Warning, _
                               "The transaction cannot be in the future.", _
                                True)
        End If
    End Sub

    Private Sub GetFoodItemDetails(ByVal e As FieldEventArgBase)
        '  GetFoodItemDetails will programmatically call upon a Data Form as a mechanism to retrieve 
        '  data from the database and into the batch handler. 
        'Also note the use of the CreateChangeSuppressor which is used when you do not want other 
        'event handlers or ‘listeners’ to respond to changes that your code is making.  
        'While this suppressor object is alive, field changes on the model specified will be ignored.  
        'When the object suppressor is disposed of, all the fields that were changed during its’ lifetime 
        'will be validated, but no field changed events will be fired.


        'Whenever the user selects a food item in the drop down list, 
        'we will default the value of the food item cost and quantity
        'Leverage the FoodItem.Edit.xml edit data form to retrieve the values for the selected food item
        '  The record to retrieve is identified by the ID variable.
        Dim ID = DirectCast(GetValueFromFieldID(e.Model, e.Field.Name, Guid.Empty), Guid)

        If ID.Equals(Guid.Empty) Then Return

        '  A request is created and ultimately passed to DataFormLoad
        '  The FormID represents the DataFormInstanceID of the data form spec
        '  The RecordID represents the primary key value of the row you want the 
        '    data form to retrieve
        Dim request As New DataFormLoadRequest() With { _
           .FormID = New System.Guid(DATAFORMID_FOODITEMINFO), _
           .RecordID = ID.ToString, _
           .IncludeMetaData = True}

        Dim reply As DataFormLoadReply = Nothing

        Try
            ' DataFormLoad is called to retrieve data from a feature represented by DATAFORMID_FOODITEMINFO.  
            reply = Blackbaud.AppFx.Server.DataFormLoad(request, Me.RequestContext)
        Catch ex As ServiceException When _
                ex.DataFormErrorInfo IsNot Nothing AndAlso _
                ex.DataFormErrorInfo.ErrorCode = DataFormErrorCode.RecordNotFound
            ' The food item doesn't exist
            Return
        End Try

        If reply Is Nothing Then Return

        '  Note the use of the CreateChangeSuppressor which is used when you do not want other event handlers 
        '  or ‘listeners’ to respond to changes that your code is making.  While this suppressor object is alive, 
        '  field changes on the model specified will be ignored.  When the object suppressor is disposed of, 
        '  all the fields that were changed during its’ lifetime will be validated, but no field changed events will be fired.
        Using BatchGridRowUIModel.CreateChangeSuppressor(e.Model)
            '  Once the data is retrieved, the DataFormItem object, which represents the data payload retrieved 
            '  from the database, is used within a call to TrySetValuesFromDFI which sets the matching form fields 
            '  from the DataFormItem to the batch row.  
            '  The batch row is represented by e.Model. 
            TrySetValuesFromDFI(e.Model, reply.DataFormItem, "FOODITEMAMOUNT", "CURRENTCOST")
            TrySetValueForFieldID(e.Model, "QUANTITY", "1")
        End Using
    End Sub

    Private Sub SetEditableState(ByVal model As Core.CustomUIModel)
        If model IsNot Nothing AndAlso Not Me.BatchCanBeSaved Then model.Mode = UIModeling.Core.DataFormMode.View
    End Sub

    Private Sub ClearFieldsOnFoodBankContextChange(ByVal e As FieldEventArgBase)
        Blackbaud.AppFx.BatchUI.BatchEntryHandler.ClearFieldValue(e.Model, _FOODBANKTXFIELDS)
    End Sub

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.String.Format(System.String,System.Object)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Friend Function IsTransactionDateInFuture(ByVal model As Core.UIModel, ByVal TXDateField As String) As Boolean
        Dim f As UIField = Nothing
        If Not TryGetField(model, TXDateField, f) OrElse f.FieldType <> UIFieldType.Date Then Return Nothing

        Dim fd = DirectCast(f, DateField)

        Try
            If f.HasValue Then
                If fd.Value > Date.Now Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As System.Exception
            'Eat any errors
        End Try

        Return Nothing
    End Function

#End Region

#Region "Old Code"
    'Private Sub ValidateDate(ByVal e As BatchUI.BatchValidateFieldArgs)
    '    Dim annot = GetDateAnnotation(e.Model, e.Field.Name)
    '    If annot IsNot Nothing Then
    '        annot.Field = e.Field
    '        e.Annotations.Add(annot)
    '    End If
    'End Sub

    '<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.String.Format(System.String,System.Object)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    'Friend Function GetDateAnnotation(ByVal model As Core.UIModel, ByVal TXDateField As String) As BatchUI.ValidationAnnotation
    '    Dim f As UIField = Nothing
    '    If Not TryGetField(model, TXDateField, f) OrElse f.FieldType <> UIFieldType.Date Then Return Nothing

    '    Dim fd = DirectCast(f, DateField)

    '    Try
    '        If f.HasValue Then
    '            If fd.Value > Date.Now Then Return New BatchUI.ValidationAnnotation With {.Category = BatchUI.AnnotationCategory.Warning, .Text = "The transaction cannot be in the future."}
    '        End If
    '    Catch ex As System.Exception
    '        'Eat any errors
    '    End Try

    '    Return Nothing
    'End Function


    'Private Sub ValidateDateRowAnnotation(ByVal e As BatchUI.BatchValidateFieldArgs)

    '    If IsTransactionDateInFuture(e.Model, e.Field.Name) = True Then

    '        'Me.SetRowError(e.Model, "Set Row Error from validate field hander. " & _
    '        '                   "The transaction date cannot be in the future.")

    '        Me.AddRowAnnotation(e.Model, _
    '                            AnnotationCategory.BusinessRuleError, _
    '                            "Row annotation set from validate field hander. " & _
    '                            "The transaction date cannot be in the future.", _
    '                            True, _
    '                            BatchMessageType.GeneralError)
    '    End If
    'End Sub


#End Region



End Class
