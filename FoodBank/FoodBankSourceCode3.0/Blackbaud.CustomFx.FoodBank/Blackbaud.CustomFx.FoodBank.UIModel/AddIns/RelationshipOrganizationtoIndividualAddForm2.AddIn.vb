
Partial Public NotInheritable Class RelationshipOrganizationtoIndividualAddForm2AddIn

    Private Sub OnInit()
        'This method is called when the UI model is created to allow any initialization to be performed.


    End Sub

    Private Sub RELATIONSHIPTYPECODEID_ValueChanged(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.ValueChangedEventArgs) _
    Handles RELATIONSHIPTYPECODEID.ValueChanged

        ' RELATIONSHIPTYPECODEID_ValueChanged Handles the RELATIONSHIPTYPECODEID.ValueChanged event
        ' Evaluate whether the value "Employee" or "Employer" is selected within either the relationship drop down or
        ' the Reciprocal drop down.
        ' Makes a call to RequireEmploymentInfo helper sub which takes a boolean value makes the employment info fields
        ' required or not required.
        If Me.RELATIONSHIPTYPECODEID.Value <> Guid.Empty And Me.RECIPROCALTYPECODEID.Value <> Guid.Empty Then
            RequireEmploymentInfo((Me.RELATIONSHIPTYPECODEID.GetItemFromValue(Me.RELATIONSHIPTYPECODEID.Value).ToString = "Employee" _
                             Or Me.RELATIONSHIPTYPECODEID.GetItemFromValue(Me.RELATIONSHIPTYPECODEID.Value).ToString = "Employer") _
                             Or (Me.RECIPROCALTYPECODEID.GetItemFromValue(Me.RECIPROCALTYPECODEID.Value).ToString = "Employee" _
                            Or Me.RECIPROCALTYPECODEID.GetItemFromValue(Me.RECIPROCALTYPECODEID.Value).ToString = "Employer"))
        End If


    End Sub

    Private Sub RECIPROCALTYPECODEID_ValueChanged(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.ValueChangedEventArgs) _
    Handles RECIPROCALTYPECODEID.ValueChanged

        If Me.RELATIONSHIPTYPECODEID.Value <> Guid.Empty _
        And Me.RECIPROCALTYPECODEID.Value <> Guid.Empty Then
            RequireEmploymentInfo((Me.RELATIONSHIPTYPECODEID.GetItemFromValue(Me.RELATIONSHIPTYPECODEID.Value).ToString = "Employee" _
                             Or Me.RELATIONSHIPTYPECODEID.GetItemFromValue(Me.RELATIONSHIPTYPECODEID.Value).ToString = "Employer") _
                             Or (Me.RECIPROCALTYPECODEID.GetItemFromValue(Me.RECIPROCALTYPECODEID.Value).ToString = "Employee" _
                            Or Me.RECIPROCALTYPECODEID.GetItemFromValue(Me.RECIPROCALTYPECODEID.Value).ToString = "Employer"))
        End If

    End Sub

    Private Sub RequireEmploymentInfo(ByVal EnableField As Boolean)
        Me.POSITION.Required = EnableField
        Me.JOBCATEGORYCODEID.Required = EnableField
        Me.CAREERLEVELCODEID.Required = EnableField
        Me.JOBSCHEDULECODEID.Required = EnableField
        Me.JOBDIVISION.Required = EnableField
        Me.JOBDEPARTMENT.Required = EnableField
    End Sub



End Class

