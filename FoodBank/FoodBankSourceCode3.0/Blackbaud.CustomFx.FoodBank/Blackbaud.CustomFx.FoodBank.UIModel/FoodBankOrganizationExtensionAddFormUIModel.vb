Public Class FoodBankOrganizationExtensionAddFormUIModel
    Private Sub FoodBankOrganizationExtensionAddFormUIModel_Loaded(ByVal sender As Object, _
            ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded
        EvaluateControls()
    End Sub

    Private Sub _addfoodbank_ValueChanged(ByVal sender As Object, _
        ByVal e As AppFx.UIModeling.Core.ValueChangedEventArgs) Handles _addfoodbank.ValueChanged
        EvaluateControls()
    End Sub

    Private Sub EvaluateControls()
        Dim EnableFoodBankFields As Boolean = False

        EnableFoodBankFields = Me.ADDFOODBANK.Value
        If EnableFoodBankFields = False Then
            Me.FOODBANKTYPECODEID.Required = False
        Else
            Me.FOODBANKTYPECODEID.Required = True
        End If

        Me.DESCRIPTION.Enabled = EnableFoodBankFields
        Me.MISSIONSTATEMENT.Enabled = EnableFoodBankFields
        Me.FOODBANKTYPECODEID.Enabled = EnableFoodBankFields
    End Sub

    Private Sub ParentOrgNameChangedHandler(ByVal sender As Object, _
            ByVal e As ValueChangedEventArgs)
        Try
            Dim OldValue As String = ""

            If e.OldValue IsNot Nothing Then
                OldValue = e.OldValue.ToString
            End If

            Me.DESCRIPTION.Value = "The Name changed and the old value was " & OldValue & " and the new value is: " & e.NewValue.ToString & " - " & Me.DESCRIPTION.Value

        Catch ex As Exception
            Throw ex.InnerException
        End Try
    End Sub

    Private Sub FoodBankOrganizationExtensionAddFormUIModel_HostModelChanged(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.HostModelChangedEventArgs) Handles Me.HostModelChanged
        Dim ParentModel = TryCast(Me.HostModel, Blackbaud.AppFx.Constituent.UIModel.Organization.OrganizationAddFormUIModel)

        If ParentModel IsNot Nothing Then
            AddHandler ParentModel.NAME.ValueChanged, AddressOf Me.ParentOrgNameChangedHandler


        End If
    End Sub
    Private Sub OnCreated()
    End Sub
End Class