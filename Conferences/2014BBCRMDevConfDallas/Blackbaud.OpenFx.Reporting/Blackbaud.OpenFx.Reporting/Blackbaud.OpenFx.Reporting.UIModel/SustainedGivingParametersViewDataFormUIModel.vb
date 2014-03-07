Imports Blackbaud.AppFx.UIModeling.Core.Utilities

Public Class SustainedGivingParametersViewDataFormUIModel

    Private _dateRangeHandler As ReportDateRangeHandler

    Private Sub SustainedGivingParametersViewDataFormUIModel_Loaded(ByVal sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded

    End Sub

    Private Sub OnCreated()
        _dateRangeHandler = New Blackbaud.AppFx.UIModeling.Core.Utilities.ReportDateRangeHandler(Me, DATETYPE, STARTDATE, ENDDATE)
    End Sub

End Class