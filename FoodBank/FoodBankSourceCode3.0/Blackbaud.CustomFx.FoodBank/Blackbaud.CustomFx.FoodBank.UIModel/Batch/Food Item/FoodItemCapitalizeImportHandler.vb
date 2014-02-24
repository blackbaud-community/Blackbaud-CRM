Imports Blackbaud.AppFx.Platform.Catalog
Imports System.Text
Imports Blackbaud.AppFx.Server
Imports System.Globalization
Imports Blackbaud.AppFx.DataHygiene.UIModel


Public Class FoodItemCapitalizeImportHandler
    Inherits ImportProcessHandler

    Public CapitalizationFields As String
    Private _shouldCapitalize As Boolean = True
    Private capitalFields As String() = Nothing

    Private Sub PrepCapitalizationFields()
        If Not String.IsNullOrEmpty(CapitalizationFields) Then
            capitalFields = Split(CapitalizationFields, ",")
        End If
    End Sub

    Public Overrides Function BeforeFileImport() As Platform.Catalog.ImportProcessHandlerResult
        Return MyBase.BeforeFileImport()
    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")> _
    Public Overrides Function BeforeRowImport(ByVal importRowNumber As Integer, ByRef importBatchRow As XmlTypes.DataForms.DataFormItem) As ImportProcessHandlerResult
        Dim nonCriticalErrors As New StringBuilder()

        PrepCapitalizationFields()

        Try
            importBatchRow = ApplyCapitalization(importBatchRow, _shouldCapitalize, Me.GetRequestContext, Me.GetRequestSecurityContext)
        Catch ex As Exception
            nonCriticalErrors.AppendLine(ex.Message)
        End Try


        If nonCriticalErrors.Length = 0 Then
            Return MyBase.BeforeRowImport(importRowNumber, importBatchRow)
        Else
            Return New ImportProcessHandlerResult(ImportProcessHandlerResult.Results.NonCriticalError, nonCriticalErrors.ToString)
        End If

    End Function

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2201:DoNotCatchGeneralExceptionTypes")> _
    Friend Function ApplyCapitalization(ByVal importBatchRow As XmlTypes.DataForms.DataFormItem, ByVal alsoCapitalize As Boolean, ByVal requestContext As RequestContext, ByVal requestSecurityContext As RequestSecurityContext) As XmlTypes.DataForms.DataFormItem

        If CapitalizationFields IsNot Nothing AndAlso alsoCapitalize Then
            For Each value As String In capitalFields
                If FieldExists(importBatchRow, value) AndAlso alsoCapitalize Then
                    Try
                        If alsoCapitalize Then
                            Dim capitalValue As String = CapitalizationHandler.ApplyCapitalization(CStr(importBatchRow.Values(value).Value), requestContext, requestSecurityContext)
                            If Not String.IsNullOrEmpty(capitalValue) Then
                                capitalValue = RTrim(LTrim(capitalValue))
                            End If
                            importBatchRow.SetValueIfNotNull(value, capitalValue)
                        End If
                    Catch ex As Exception
                        Throw New Exception(String.Format(CultureInfo.CurrentCulture, My.Resources.Content.CapitalizationFailed, value, CStr(importBatchRow.Values(value).Value)))
                    End Try
                End If
            Next
        End If

        Return importBatchRow

    End Function

    Friend Shared Function FieldExists(ByVal row As XmlTypes.DataForms.DataFormItem, ByVal field As String) As Boolean
        Return row.TryGetValue(field, New XmlTypes.DataForms.DataFormFieldValue())
    End Function
End Class

