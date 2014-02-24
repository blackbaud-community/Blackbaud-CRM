Imports System.Text
Imports System.Data.SqlClient
Imports Blackbaud.AppFx.Server

Public NotInheritable Class InventoryProcessBusinessProcess
    Inherits AppCatalog.AppBusinessProcess
    Private _parameters As InventoryParameters = Nothing

    'Validate gets called first and if all goes well, it gets our parameters too.
    Public Overrides Sub Validate()
        MyBase.Validate()
        ' Get our business process parameters
        _parameters = New InventoryParameters(RequestArgs.ParameterSetID, Me.RequestContext)
        If _parameters Is Nothing Then
            Throw New Exception("No parameters found with the given parameter")
        End If


    End Sub

    ' This is a class used for the inventory process instance parameters.
    Private Class InventoryParameters
        Public ReadOnly OUTPUTVIEWID As Guid = Guid.Empty
        Public ReadOnly IDSETID As Guid = Guid.Empty

        Public Sub New(ByVal parameterSetID As Guid, ByRef requestContext As RequestContext)
            Using con As SqlConnection = New SqlConnection(requestContext.AppDBConnectionString)
                Using command As SqlCommand = con.CreateCommand()
                    Try
                        command.CommandText = "USR_USP_INVENTORYPROCESS_GETPARAMETERS"

                        command.CommandType = CommandType.StoredProcedure
                        command.Parameters.AddWithValue("@ID", parameterSetID)
                        con.Open()
                        Using reader As SqlDataReader = command.ExecuteReader()
                            reader.Read()
                            Me.IDSETID = reader.GetGuid(reader.GetOrdinal("IDSETREGISTERID"))
                            Me.OUTPUTVIEWID = reader.GetGuid(reader.GetOrdinal("OUTPUTVIEWID"))
                            reader.Close()
                        End Using
                        con.Close()
                    Catch
                        Throw New Exception("Unable to get parameter set found for the given Id")
                    End Try
                End Using
            End Using
        End Sub
    End Class

    ' The core of the buisness process
    Public Overrides Function StartBusinessProcess() As Blackbaud.AppFx.Server.AppCatalog.AppBusinessProcessResult
        ' Our return object 
        Dim result As New AppCatalog.AppBusinessProcessResult()

        ' Declare some constants for the output table 
        Const OUTPUT_TABLE_PREFIX As String = "USR_INVENTORYPROCESS"
        Const OUTPUT_TABLE_KEY As String = "OUTPUT"

        ' Declare local variables 
        Dim outputTableName As String = String.Empty
        Dim exportView As String = String.Empty
        Dim viewColumns() As AppCatalog.TableColumn = Nothing
        Dim inventoryIDField As String = String.Empty

        ' Get the export view's name and the field to join on 
        With Me.OutputView(_parameters.OUTPUTVIEWID)
            exportView = .ExportViewName
            inventoryIDField = .JoinField
        End With

        ' This will create the  output table called 
        'USR_INVENTORYPROCESS_<guid> and get the viewColumns
        outputTableName = Me.CreateOutputTableFromView(exportView, OUTPUT_TABLE_PREFIX, OUTPUT_TABLE_KEY, viewColumns)

        ' Now we need to populate the output table
        Dim sql As New StringBuilder()

        With sql
            .AppendFormat("insert into dbo.[{0}]", outputTableName)
            .AppendFormat("({0})", Me.CreateFieldsList(viewColumns))
            ' Now the select part of the query
            .AppendLine("select ")

            ' OUTPUTVIEW here is the table name field prefix i.e. TABLE.FIELD
            .AppendFormat("{0} ", Me.CreateFieldsList(viewColumns, "OUTPUTVIEW"))
            .AppendLine("from dbo.USR_FOODBANK as FB")
            .AppendFormat("join dbo.[{0}] as OUTPUTVIEW on FB.[ID] = OUTPUTVIEW.[{1}]", exportView, inventoryIDField)
            ' Now we add the join clause to filter based on our selection parameter
            ' Note: I used a standard product UFN to read the GUIDs from a IDSET given an ID
            .AppendFormat("join dbo.[UFN_IDSETREADER_GETRESULTS]('{0}') IDSET on OUTPUTVIEW.ID = IDSET.ID", _parameters.IDSETID.ToString())
        End With
     
        ' Execute the command to populate the table
        Using con As SqlConnection = New SqlConnection(Me.RequestContext.AppDBConnectionString)
            con.Open()
            Using command As SqlCommand = con.CreateCommand()
                With command
                    .CommandText = sql.ToString()
                    .CommandTimeout = Me.ProcessCommandTimeout
                    .ExecuteNonQuery()
                End With
            End Using
            ' Now get the record count for the BP status
            Using command As SqlCommand = con.CreateCommand()
                With command
                    .CommandText = String.Format("select count(*) from dbo.[{0}]", outputTableName)
                    .CommandTimeout = Me.ProcessCommandTimeout
                    result.NumberSuccessfullyProcessed = CType(.ExecuteScalar(), Integer)
                End With
            End Using
            con.Close()
        End Using
        Return result
    End Function
End Class
