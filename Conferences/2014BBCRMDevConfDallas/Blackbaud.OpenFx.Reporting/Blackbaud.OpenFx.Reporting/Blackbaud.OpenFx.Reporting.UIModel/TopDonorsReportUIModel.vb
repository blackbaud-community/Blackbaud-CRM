Public Class TopDonorsReportUIModel

    Private Sub TopDonorsReportUIModel_Loaded(ByVal sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded
        Me.DateRangeHandler = New Blackbaud.AppFx.UIModeling.Core.Utilities.ReportDateRangeHandler(Me, DATETYPE, STARTDATE, ENDDATE)
    End Sub

End Class