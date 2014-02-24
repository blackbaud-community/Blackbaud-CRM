Public Class FoodBankBreakdownReportUIModel

    Private Sub FoodBankBreakdownReportUIModel_Loaded(ByVal sender As Object, ByVal e As Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs) Handles Me.Loaded
    Me.CHOSENITEMS.SelectionMode = SelectionMode.Single
    Me.CHOSENITEMS.AllowAdd = False
    Me.CHOSENITEMS.AllowDelete = False
    Me.CHOSENITEMS.DisplayReadOnly = True

    Me.AVAILABLEITEMS.DisplayReadOnly = True
    Me.AVAILABLEITEMS.SelectionMode = SelectionMode.Single

    Me.ADDITEM.DisplayStyle = ValueDisplayStyle.MoveRightImageOnly
    Me.REMOVEITEM.DisplayStyle = ValueDisplayStyle.MoveLeftImageOnly
    Me.MOVEITEMDOWN.DisplayStyle = ValueDisplayStyle.MoveDownImageOnly
    Me.MOVEITEMUP.DisplayStyle = ValueDisplayStyle.MoveUpImageOnly
    Me.MOVEDATAUP.DisplayStyle = ValueDisplayStyle.MoveUpImageOnly
    Me.MOVEDATADOWN.DisplayStyle = ValueDisplayStyle.MoveDownImageOnly

    For Each item In _allgroups.DataSource.OrderBy(Function(ag) ag.Translation)
      If item.Value <> "FOODBANKTYPE" Then
        Dim availableitem = Me.AVAILABLEITEMS.Value.AddNew()
        availableitem.ID.Value = item.Value.ToString
        availableitem.NAME.Value = item.Translation
      End If
    Next

    Me.AVAILABLEDATA.AllowAdd = False
    Me.AVAILABLEDATA.AllowDelete = False

    Me.AVAILABLEDATA.SelectionMode = SelectionMode.Single

    'Include add totals by default
    For Each item In _alldata.DataSource
      Dim availabledataitem = Me.AVAILABLEDATA.Value.AddNew()
      availabledataitem.ID.Value = item.Value.ToString
      availabledataitem.NAME.Value = item.Translation
      availabledataitem.INCLUDED.Value = True
    Next

    'Add Food Bank Type as default grouping
    Dim defaultItem As New FoodBankBreakdownReportCHOSENITEMSUIModel()
    defaultItem.ID.Value = "FOODBANKTYPE"
    defaultItem.NAME.Value = "Food Bank Type"

    Me.CHOSENITEMS.Value.Add(defaultItem)

    UpdateButtonState()
    UpdateGroupParameters()
    UpdateDataButtonState()
    UpdateDataParameters()

    End Sub

  Private Sub UpdateGroupParameters()

    GROUP1.Value = ""
    GROUP2.Value = ""
    GROUP3.Value = ""

    For Each item In Me.CHOSENITEMS.Value

      Dim selectedIndex = Me.CHOSENITEMS.Value.IndexOf(item)

      If selectedIndex = 0 Then
        GROUP1.Value = item.ID.Value
      ElseIf selectedIndex = 1 Then
        GROUP2.Value = item.ID.Value
      ElseIf selectedIndex = 2 Then
        GROUP3.Value = item.ID.Value
      End If

    Next

  End Sub

  Private Sub SortAvailableGroups()

    Me.AVAILABLEITEMS.Value.Clear()

    Dim selected As Boolean

    For Each item In _allgroups.DataSource.OrderBy(Function(ag) ag.Translation)

      selected = False

      For Each group In Me.CHOSENITEMS.Value
        If group.ID.Value.ToString = item.Value.ToString Then
          selected = True
        End If
      Next

      If selected = False Then
        Dim availableitem = Me.AVAILABLEITEMS.Value.AddNew()
        availableitem.ID.Value = item.Value.ToString
        availableitem.NAME.Value = item.Translation
      End If

    Next

  End Sub

  Private Sub UpdateDataParameters()

    DATA1.Value = ""
    DATA2.Value = ""
    DATA3.Value = ""
    DATA4.Value = ""
    DATA5.Value = ""
    DATA6.Value = ""
    DATA7.Value = ""
    DATA8.Value = ""

    For Each dataitem In Me.AVAILABLEDATA.Value

      Dim selectedDataIndex = Me.AVAILABLEDATA.Value.IndexOf(dataitem)

      If dataitem.INCLUDED.Value = True Then

        If selectedDataIndex = 0 Then
          DATA1.Value = dataitem.ID.Value
        ElseIf selectedDataIndex = 1 Then
          DATA2.Value = dataitem.ID.Value
        ElseIf selectedDataIndex = 2 Then
          DATA3.Value = dataitem.ID.Value
        ElseIf selectedDataIndex = 3 Then
          DATA4.Value = dataitem.ID.Value
        ElseIf selectedDataIndex = 4 Then
          DATA5.Value = dataitem.ID.Value
        ElseIf selectedDataIndex = 5 Then
          DATA6.Value = dataitem.ID.Value
        ElseIf selectedDataIndex = 6 Then
          DATA7.Value = dataitem.ID.Value
        ElseIf selectedDataIndex = 7 Then
          DATA8.Value = dataitem.ID.Value
        End If

      End If
    Next

  End Sub

  Private Sub UpdateButtonState()

    Dim chosenItemSelected = Me.CHOSENITEMS.SelectedItems.Count > 0

    Me.REMOVEITEM.Enabled = chosenItemSelected
    Me.MOVEITEMUP.Enabled = chosenItemSelected
    Me.MOVEITEMDOWN.Enabled = chosenItemSelected

    'Disable Add Item button if 3 are already selected
    If Me.CHOSENITEMS.Value.Count > 2 Then
      Me.ADDITEM.Enabled = False
    Else
      Me.ADDITEM.Enabled = True
    End If

    For Each selectedItem In Me.CHOSENITEMS.SelectedItems

      Dim selectedIndex = Me.CHOSENITEMS.Value.IndexOf(selectedItem)

      If selectedIndex = 0 Then
        Me.MOVEITEMUP.Enabled = False
      End If

      If selectedIndex = Me.CHOSENITEMS.Value.Count - 1 Then
        Me.MOVEITEMDOWN.Enabled = False
      End If

    Next

  End Sub

  Private Sub UpdateDataButtonState()

    Dim availableDataSelected = Me.AVAILABLEDATA.SelectedItems.Count > 0
    Me.MOVEDATAUP.Enabled = availableDataSelected
    Me.MOVEDATADOWN.Enabled = availableDataSelected

    For Each selecteddata In Me.AVAILABLEDATA.SelectedItems

      Dim selecteddataindex = Me.AVAILABLEDATA.Value.IndexOf(selecteddata)

      If selecteddataindex = 0 Then
        Me.MOVEDATAUP.Enabled = False
      End If

      If selecteddataindex = Me.AVAILABLEDATA.Value.Count - 1 Then
        Me.MOVEDATADOWN.Enabled = False
      End If

    Next

  End Sub


  Private Sub _availableitems_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs) Handles _availableitems.SelectionChanged
    UpdateButtonState()
  End Sub

  Private Sub _chosenitems_SelectionChanged(ByVal sender As Object, ByVal e As SelectionChangedEventArgs) Handles _chosenitems.SelectionChanged
    UpdateButtonState()
  End Sub

  Private Sub _additem_InvokeAction(ByVal sender As Object, ByVal e As InvokeActionEventArgs) Handles _additem.InvokeAction

    For Each selectedItem In Me.AVAILABLEITEMS.SelectedItems

      Dim item As New FoodBankBreakdownReportCHOSENITEMSUIModel()
      item.ID.Value = selectedItem.ID.Value
      item.NAME.Value = selectedItem.NAME.Value

      Me.CHOSENITEMS.Value.Add(item)
      Me.CHOSENITEMS.SelectItem(item, True)

      Dim selectedIndex = Me.AVAILABLEITEMS.Value.IndexOf(selectedItem)

      Me.AVAILABLEITEMS.Value.Remove(selectedItem)

      Dim newSelectedIndex = Math.Min(selectedIndex, Me.AVAILABLEITEMS.Value.Count - 1)

      If newSelectedIndex >= 0 Then
        Me.AVAILABLEITEMS.SelectItem(newSelectedIndex, True)
      End If

    Next

    UpdateButtonState()
    UpdateGroupParameters()

  End Sub

  Private Sub _removeitem_InvokeAction(ByVal sender As Object, ByVal e As InvokeActionEventArgs) Handles _removeitem.InvokeAction

    For Each selectedItem In Me.CHOSENITEMS.SelectedItems

      Dim selectedIndex = Me.CHOSENITEMS.Value.IndexOf(selectedItem)

      Me.CHOSENITEMS.Value.Remove(selectedItem)

      Dim newSelectedIndex = Math.Min(selectedIndex, Me.CHOSENITEMS.Value.Count - 1)

      If newSelectedIndex >= 0 Then
        Me.CHOSENITEMS.SelectItem(newSelectedIndex, True)
      End If

    Next

    SortAvailableGroups()
    UpdateButtonState()
    UpdateGroupParameters()

  End Sub

  Private Sub _moveitemdown_InvokeAction(ByVal sender As Object, ByVal e As InvokeActionEventArgs) Handles _moveitemdown.InvokeAction

    For Each selectedItem In Me.CHOSENITEMS.SelectedItems

      Dim selectedIndex = Me.CHOSENITEMS.Value.IndexOf(selectedItem)

      If selectedIndex < Me.CHOSENITEMS.Value.Count - 1 Then
        Me.CHOSENITEMS.Value.Remove(selectedItem)
        Me.CHOSENITEMS.Value.Insert(selectedIndex + 1, selectedItem)
        Me.CHOSENITEMS.SelectItem(selectedItem, True)
      End If

    Next

    UpdateButtonState()
    UpdateGroupParameters()

  End Sub

  Private Sub _moveitemup_InvokeAction(ByVal sender As Object, ByVal e As InvokeActionEventArgs) Handles _moveitemup.InvokeAction

    For Each selectedItem In Me.CHOSENITEMS.SelectedItems

      Dim selectedIndex = Me.CHOSENITEMS.Value.IndexOf(selectedItem)

      If selectedIndex > 0 Then
        Me.CHOSENITEMS.Value.Remove(selectedItem)
        Me.CHOSENITEMS.Value.Insert(selectedIndex - 1, selectedItem)
        Me.CHOSENITEMS.SelectItem(selectedItem, True)
      End If

    Next

    UpdateButtonState()
    UpdateGroupParameters()

  End Sub

  Private Sub _availabledata_SelectionChanged(sender As Object, e As Blackbaud.AppFx.UIModeling.Core.SelectionChangedEventArgs) Handles _availabledata.SelectionChanged

    UpdateDataButtonState()
    UpdateDataParameters()

  End Sub

  Private Sub _movedataup_InvokeAction(sender As Object, e As Blackbaud.AppFx.UIModeling.Core.InvokeActionEventArgs) Handles _movedataup.InvokeAction

    For Each selectedItem In Me.AVAILABLEDATA.SelectedItems

      Dim selectedIndex = Me.AVAILABLEDATA.Value.IndexOf(selectedItem)

      If selectedIndex > 0 Then
        Me.AVAILABLEDATA.Value.Remove(selectedItem)
        Me.AVAILABLEDATA.Value.Insert(selectedIndex - 1, selectedItem)
      End If

    Next

    UpdateDataButtonState()
    UpdateDataParameters()

  End Sub

  Private Sub _movedatadown_InvokeAction(sender As Object, e As Blackbaud.AppFx.UIModeling.Core.InvokeActionEventArgs) Handles _movedatadown.InvokeAction

    For Each selectedItem In Me.AVAILABLEDATA.SelectedItems

      Dim selectedIndex = Me.AVAILABLEDATA.Value.IndexOf(selectedItem)

      If selectedIndex < Me.AVAILABLEDATA.Value.Count - 1 Then
        Me.AVAILABLEDATA.Value.Remove(selectedItem)
        Me.AVAILABLEDATA.Value.Insert(selectedIndex + 1, selectedItem)
      End If

    Next

    UpdateDataButtonState()
    UpdateDataParameters()

  End Sub

  Private Sub OnCreated()
    DateRangeHandler = New Blackbaud.AppFx.UIModeling.Core.Utilities.ReportDateRangeHandler(Me, DATETYPE, STARTDATE, ENDDATE)
  End Sub

  Private Sub FoodBankBreakdownReportUIModel_DefaultValuesLoaded(sender As Object, e As UIModeling.Core.DefaultValuesLoadedEventArgs) Handles Me.DefaultValuesLoaded
    UpdateDataParameters()
    UpdateGroupParameters()
  End Sub

End Class

Public Class [FoodBankBreakdownReportAVAILABLEDATAUIModel]

  Private Sub OnCreated()
    Me.NAME.Enabled = False
  End Sub

End Class