Imports Blackbaud.AppFx.Server
Imports System.Data.SqlClient

Public NotInheritable Class ConstituentConstituencySimpleDataList
    Inherits AppCatalog.AppSimpleDataList

    Public Overrides Function GetListResults() As Blackbaud.AppFx.Server.AppCatalog.AppSimpleDataListResult

        Dim resultRows As New List(Of SimpleDataListResultRow)()
        Dim row = Blackbaud.AppFx.SpWrap.USP_OLAPDATASOURCE_GETSERVERDATABASE.ExecuteRow(RequestContext.OpenAppDBConnection(), "BBDW")

        Dim olapDataSourceID = row.OLAPDATASOURCEID
        Dim olapinfo As New OlapDataSourceConnectionInfo(olapDataSourceID, RequestContext.OpenAppDBConnection(), True)

        Using reportDataSourceConnection = New SqlClient.SqlConnection(olapinfo.GetSQLConnectionString)

            Using scope As New OlapImpersonationScope(olapinfo)

                reportDataSourceConnection.Open()

                Using cmd As SqlCommand = reportDataSourceConnection.CreateCommand
                    With cmd
                        .CommandText = "select [CONSTITUENCY] as [VALUE] from BBDW.[DIM_CONSTITUENCY] where [CONSTITUENCYDIMID] > 0 order by [CONSTITUENCY]"
                        Dim reader As SqlDataReader = cmd.ExecuteReader

                        If reader.HasRows Then
                            Do While reader.Read()
                                Dim resultRow As New SimpleDataListResultRow
                                resultRow.Value = reader.GetString(0)
                                resultRows.Add(resultRow)
                            Loop
                        End If

                        reader.Close()

                    End With
                End Using

            End Using

        End Using

        Return New AppCatalog.AppSimpleDataListResult(resultRows)

    End Function

    Public Overrides Function HasLabel() As Boolean
        Return False
    End Function

    Public Overrides Function ValueDataType() As Blackbaud.AppFx.XmlTypes.FormFieldDataType

    End Function

End Class
