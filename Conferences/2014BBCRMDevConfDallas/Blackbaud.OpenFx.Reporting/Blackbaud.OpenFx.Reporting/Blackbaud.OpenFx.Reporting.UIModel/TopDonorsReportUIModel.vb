Public Class TopDonorsReportUIModel

    Private Sub TopDonorsReportUIModel_Loaded(ByVal sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded
        Me.DateRangeHandler = New Blackbaud.AppFx.UIModeling.Core.Utilities.ReportDateRangeHandler(Me, DATETYPE, STARTDATE, ENDDATE)
        Me.MAXROWS.Value = 50
        HideFilters()
    End Sub

    Private Sub _showadvancedfilters_ValueChanged(sender As Object, e As ValueChangedEventArgs) Handles _showadvancedfilters.ValueChanged
        If Me.SHOWADVANCEDFILTERS.Value = True Then
            ShowFilters()
        Else
            HideFilters()
        End If
    End Sub

    Private Sub ShowFilters()

        Me.ADVANCEDFILTERSTAB.Visible = True
        Me.INCLUDEANONYMOUS.Value = True
        Me.INCLUDEANONYMOUS.Visible = True
        Me.INCLUDENOTPOSTED.Value = True
        Me.INCLUDENOTPOSTED.Visible = True
        Me.INCLUDEPLEDGES.Value = True
        Me.INCLUDEPLEDGES.Visible = True

    End Sub

    Private Sub HideFilters()

        Me.ADVANCEDFILTERSTAB.Visible = False
        Me.INCLUDEANONYMOUS.Value = True
        Me.INCLUDEANONYMOUS.Visible = False
        Me.INCLUDENOTPOSTED.Value = True
        Me.INCLUDENOTPOSTED.Visible = False
        Me.INCLUDEPLEDGES.Value = True
        Me.INCLUDEPLEDGES.Visible = False

    End Sub

End Class