Partial Public NotInheritable Class PhoneAddAddIn
    Private Sub OnInit()
        'This method is called when the UI model is created to allow any initialization to be performed.
    End Sub

    Private Sub ModelValidated(ByVal Sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.ValidatedEventArgs) Handles MODEL.Validated
        Select Case Me.PHONETYPECODEID.GetDescription
            Case "Bank"
                e.InvalidFieldName = "PHONETYPECODEID"
                e.InvalidReason = "Can not select Bank as the phone type."
                e.Valid = False
            Case Else
        End Select
    End Sub

   


    Private Sub PHONETYPECODEID_ValueChanged(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.ValueChangedEventArgs) Handles PHONETYPECODEID.ValueChanged
        RequireControls()
    End Sub

    Private Sub RequireControls()
        Dim IsRequired As Boolean = False

        If Me.PHONETYPECODEID.HasValue = False Then
            IsRequired = False
        Else
            Select Case Me.PHONETYPECODEID.GetDescription
                Case "Bank"
                    IsRequired = True
                Case Else
                    IsRequired = False
            End Select
        End If

        If PRIMARY.HasValue Then
            If PRIMARY.Value = True Then
                IsRequired = True
            End If
        End If

        If DONOTCALL.HasValue Then
            If DONOTCALL.Value = True Then
                IsRequired = True
            End If
        End If
        Me.INFOSOURCECODEID.Required = IsRequired
        Me.INFOSOURCECOMMENTS.Required = IsRequired
    End Sub

    Private Sub PRIMARY_ValueChanged(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.ValueChangedEventArgs) Handles PRIMARY.ValueChanged
        RequireControls()
    End Sub

    Private Sub DONOTCALL_ValueChanged(ByVal sender As Object, ByVal e As AppFx.UIModeling.Core.ValueChangedEventArgs) Handles DONOTCALL.ValueChanged
        RequireControls()
    End Sub
End Class

