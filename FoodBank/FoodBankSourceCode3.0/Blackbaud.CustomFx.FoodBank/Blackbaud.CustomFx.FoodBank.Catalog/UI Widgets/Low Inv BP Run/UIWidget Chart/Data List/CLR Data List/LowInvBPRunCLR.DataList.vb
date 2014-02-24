Imports Blackbaud.AppFx.Server
Imports System.Data.SqlClient

Public NotInheritable Class LowInvBPRunCLRDataList
    Inherits AppCatalog.AppDataList

    Public BUSINESSPROCESSOUTPUTID As Guid

    Public Overrides Function GetListResults() As Blackbaud.AppFx.Server.AppCatalog.AppDataListResult

        Dim command As New SqlCommand("USR_USP_DATALIST_TopFoodItemShortByBPRunCLR")
        command.CommandType = CommandType.StoredProcedure
        command.Parameters.AddWithValue("@BUSINESSPROCESSOUTPUTID", BUSINESSPROCESSOUTPUTID)

        Dim reader As SqlDataReader
        Dim resultList As New Generic.List(Of DataListResultRow)

        Using conn As SqlClient.SqlConnection = Me.RequestContext.OpenAppDBConnection

            command.Connection = conn
            reader = command.ExecuteReader(CommandBehavior.CloseConnection)

            If reader.HasRows Then
                Do While reader.Read
                    Dim result As New DataListResultRow

                    Dim valueList As New Generic.List(Of String)
                    valueList.Add(reader.GetString(0))
                    valueList.Add(reader.GetInt32(1).ToString)
                    result.Values = valueList.ToArray

                    resultList.Add(result)
                Loop
            End If
        End Using

        Return New AppCatalog.AppDataListResult(resultList)
    End Function

End Class


