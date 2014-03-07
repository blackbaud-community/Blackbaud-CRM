Public Class SustainedGivingSummaryViewDataFormUIModel

    Private Sub SustainedGivingSummaryViewDataFormUIModel_Loaded(ByVal sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded

        If Me.STARTDATE Is Nothing Then
            Me.STARTDATE.Value = "12/2/2011"
        End If
    End Sub

End Class