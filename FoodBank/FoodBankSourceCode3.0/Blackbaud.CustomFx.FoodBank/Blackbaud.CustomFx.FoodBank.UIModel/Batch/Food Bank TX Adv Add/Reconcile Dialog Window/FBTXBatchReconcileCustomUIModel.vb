Imports Blackbaud.AppFx.Server
Imports Blackbaud.AppFx.XmlTypes.DataForms

Imports Blackbaud.AppFx.BatchUI

Public Class FBTXBatchReconcileCustomUIModel

    Private Sub FBTXBatchReconcileCustomUIModel_Loaded(ByVal sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded

        BatchEntryHandler.SetupStandardDialogGrid(Me.FOODITEMS, True, True, False)

        ReconcileBatchAgainstFoodBankCurrentInventory()
    End Sub

    Private Sub _foodbankid_ValueChanged(sender As Object, e As AppFx.UIModeling.Core.ValueChangedEventArgs) Handles _foodbankid.ValueChanged
        ReconcileBatchAgainstFoodBankCurrentInventory()
    End Sub

    Private Sub ReconcileBatchAgainstFoodBankCurrentInventory()

        Me.FOODITEMS.Value.Clear()
        Dim SelectedFoodBankID As Guid

        If Me.FOODBANKID.HasValue = False Then
            Return
        End If

        SelectedFoodBankID = Me.FOODBANKID.Value

        Dim BATCHID As Guid = Me.BATCHID.Value

        Dim DLReply As DataListLoadReply = GetReconcileDataList(SelectedFoodBankID, BATCHID, Me.GetRequestContext)
        Dim FoodItemReconcileRow As FBTXBatchReconcileCustomFOODITEMSUIModel
        If Not DLReply Is Nothing Then
            If DLReply.Rows.Count > 0 Then
                For Each DLResultRow In DLReply.Rows
                    FoodItemReconcileRow = New FBTXBatchReconcileCustomFOODITEMSUIModel
                    FoodItemReconcileRow.FOODBANKNAME.Value = DLResultRow.Values(2)
                    FoodItemReconcileRow.BATCHTXFOODITEMNAME.Value = DLResultRow.Values(4)
                    FoodItemReconcileRow.CURRENTPRODINV.Value = DLResultRow.Values(5)
                    FoodItemReconcileRow.BATCHTXINV.Value = DLResultRow.Values(6)

                    If DLResultRow.Values(7) < 0 Then
                        FoodItemReconcileRow.POTENTIALRECONCILEDPRODINV.ToolTipText = "Potential Inventory Shortage"
                    End If

                    FoodItemReconcileRow.POTENTIALRECONCILEDPRODINV.Value = DLResultRow.Values(7)
                    Me.FOODITEMS.Value.Add(FoodItemReconcileRow)
                Next
            Else
                FoodItemReconcileRow = New FBTXBatchReconcileCustomFOODITEMSUIModel
                FoodItemReconcileRow.BATCHTXFOODITEMNAME.Value = "No Food Items"
                FoodItemReconcileRow.BATCHTXFOODITEMNAME.ToolTipText = "No related food items in food bank inventory"
                Me.FOODITEMS.Value.Add(FoodItemReconcileRow)
            End If
        Else
            FoodItemReconcileRow = New FBTXBatchReconcileCustomFOODITEMSUIModel
            FoodItemReconcileRow.BATCHTXFOODITEMNAME.Value = "No Food Items"
            FoodItemReconcileRow.BATCHTXFOODITEMNAME.ToolTipText = "No related food items in food bank inventory"
            Me.FOODITEMS.Value.Add(FoodItemReconcileRow)
        End If
    End Sub


    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId:="System.String.Format(System.String,System.Object)")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Friend Function GetReconcileDataList(ByVal FOODBANKID As Guid, ByVal batchID As Guid, ByVal requestContext As RequestContext) As DataListLoadReply

        Const DATALISTID As String = "e999e2b9-a6a2-48c6-8746-5c6cdd2c53eb"

        Try
            Dim request As New DataListLoadRequest
            With request
                .DataListID = New Guid(DATALISTID)
                .Parameters = New DataFormItem
                .Parameters.SetValue("FOODBANKID", FOODBANKID)

                '*****
                'Dim parameterDFI As New DataFormItem
                'parameterDFI.SetValue("CONSTITUENTONEID", RecordId)
                'parameterDFI.SetValue("CONSTITUENTTWOID", IDstring)
                '*****

                'BATCHID is the context for the data list.
                .ContextRecordID = batchID.ToString

                'Allows a request to indicate that the request is in the context of another 
                'operation and the security should be applied in that context
                'If a request is being made in the context of another operation then the request 
                'will be granted if the other operation implies that this request is part of that operation.
                Dim securityContext As New RequestSecurityContext
                securityContext.SecurityFeatureType = SecurityFeatureType.Batch
                securityContext.SecurityFeatureID = batchID
                .SecurityContext = securityContext
            End With

            Dim reply As DataListLoadReply = ServiceMethods.DataListLoad(request, requestContext)
            If reply IsNot Nothing AndAlso reply.Rows.Length > 0 Then
                Return reply
            End If
        Catch
            ' Swallow exception
        End Try


        Return Nothing
    End Function

End Class


