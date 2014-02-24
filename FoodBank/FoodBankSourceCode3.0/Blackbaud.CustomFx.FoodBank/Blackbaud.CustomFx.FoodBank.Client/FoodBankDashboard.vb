Imports System.Data
Imports Blackbaud.AppFx.Browser
Imports Blackbaud.AppFx.Browser.Common
Imports Blackbaud.AppFx.Browser.DataForms
Imports Blackbaud.AppFx.Browser.ServiceProxy
Imports Microsoft.Reporting.WinForms

Public NotInheritable Class FoodBankDashboard

    Private Const FOODBANK_DASHBOARDID As String = "55309cbf-b743-4f57-a9cf-19fe3a765777"

    Private Const FOODBANKINFORMATION_DATALISTID As String = "44fdae36-69b9-4bdd-83b8-9bfe09bc607c"
    Private Const FOODBANKINFORMATION_XMLDATAITEMNAME As String = "FoodBankInformation"

    Private _constituentId As String
    Private _foodBankReport As New DataSet

    Private Sub CreateTables()
        Dim table As New DataTable(FOODBANKINFORMATION_XMLDATAITEMNAME)
        With table.Columns
            .Add("NAME", GetType(String))
            .Add("DESCRIPTION", GetType(String))
            .Add("MISSIONSTATEMENT", GetType(String))
            .Add("FOODBANKTYPE", GetType(String))
            .Add("PRIMARYADDRESS", GetType(String))
            .Add("TOTALFOODRECEIVED", GetType(Integer))
            .Add("TOTALFOODDISTRIBUTED", GetType(Integer))
        End With
        _foodBankReport.Tables.Add(table)
    End Sub

    Private Sub RefreshReport()
        MouseWait()
        Try
            'Load datalists
            rpDashboard.LocalReport.DataSources.Clear()

            Dim request As New DataListLoadRequest
            With request
                .DataListID = ToGuid(FOODBANKINFORMATION_DATALISTID)
                .ContextRecordID = _constituentId
                .IncludeMetaData = True
                .MaxRows = Integer.MaxValue
                .SecurityContext = GetRequestSecurityContext()
                .ClientAppInfo.TimeOutSeconds = 60
            End With

            rpDashboard.LocalReport.DataSources.Add(New ReportDataSource(FOODBANKINFORMATION_XMLDATAITEMNAME, _foodBankReport.Tables(FOODBANKINFORMATION_XMLDATAITEMNAME)))

            With _foodBankReport.Tables(FOODBANKINFORMATION_XMLDATAITEMNAME).Rows
                .Clear()

                Dim reply As DataListLoadReply = DataListLoad(request, request.ClientAppInfo.TimeOutSeconds)

                For Each row As DataListResultRow In reply.Rows
                    .Add(row.Values)
                Next
            End With

            rpDashboard.RefreshReport()
        Catch ex As Exception
            UIHelper.HandleException(ex)
        End Try

        MouseWaitStop()
    End Sub


    Private Function GetRequestSecurityContext() As RequestSecurityContext
        Dim ctx As New RequestSecurityContext()
        With ctx
            .SecurityFeatureType = SecurityFeatureType.Dashboard
            .SecurityFeatureID = ToGuid(FOODBANK_DASHBOARDID)
        End With
        Return ctx
    End Function

    Private Sub FoodBankDashboard_SectionInitialized(ByVal sender As System.Object, ByVal e As Blackbaud.AppFx.Browser.PageSectionInitArgs) Handles MyBase.SectionInitialized
        _constituentId = Page.ContextRecordID.ToString()
        MouseWait()
        CreateTables()
        RefreshReport()
        MouseWaitStop()
    End Sub
End Class
