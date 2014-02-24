Option Strict On

Imports Blackbaud.AppFx.XmlTypes
Imports Blackbaud.AppFx.Browser.DataForms

''' <summary>
''' A client-side event handler for batch
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class FoodItemAddBatchEventHandler
    Inherits Browser.Batch.BatchEntryHandler

    Private Const SPELLCHECK_BUTTON_KEY As String = "SPELLCHECKKEY"

    Private Const SPELLCHECKTOGGLE_BUTTON_KEY As String = "SPELLCHECKTOGGLEKEY"

    Private Const SPELLCHECK_IMAGE_KEY As String = "RES:rename"

    Private Const SPELLCHECKTOGGLE_IMAGE_KEY As String = "RES:tv_checked"

    Private _buttons As New Browser.Batch.BatchButtonCollection(Me)
    Private _menus As New Browser.Batch.BatchMenuCollection(Me)
    Private _togglestate As Boolean = True

    ''' <summary>
    ''' Returns a collection of buttons to be added to the batch data entry form toolbar
    ''' </summary>
    Public Overrides Function GetBatchButtons() As Browser.Batch.BatchButtonCollection

        If _buttons.Count = 0 Then
            _buttons.Add(SPELLCHECK_BUTTON_KEY, Browser.GetImageFromImageKey(SPELLCHECK_IMAGE_KEY), "Spell Check")
            _buttons.Add(SPELLCHECKTOGGLE_BUTTON_KEY, Browser.GetImageFromImageKey(SPELLCHECKTOGGLE_IMAGE_KEY), "Toggle Spell Check")
            _togglestate = True
        End If
        Return _buttons

    End Function



    ''' <summary>
    ''' Returns a collection of menus to be added to the batch data entry form menu
    ''' </summary>
    Public Overrides Function GetBatchMenus() As Browser.Batch.BatchMenuCollection

        If _menus.Count = 0 Then
            _menus.Add(SPELLCHECK_BUTTON_KEY, "Spell Check", Browser.GetImageFromImageKey(SPELLCHECK_IMAGE_KEY), "&Spell Check", _
                       Windows.Forms.Keys.Control Or Windows.Forms.Keys.J)

        End If
        Return _menus
    End Function

    ''' <summary>
    ''' Occurs when a button is clicked.
    ''' </summary>
    Private Sub FoodItemAddBatchEventHandler_ButtonClicked(ByVal sender As Object, _
                                                           ByVal e As Browser.Batch.BatchButtonEventArgs) _
                                                           Handles Me.ButtonClicked
        If e.Button.Key = SPELLCHECK_BUTTON_KEY Then
            If _togglestate = True Then
                SpellCheckGrid()
            End If


        ElseIf e.Button.Key = SPELLCHECKTOGGLE_BUTTON_KEY Then
            _togglestate = Not _togglestate
            _buttons(0).Enabled = _togglestate
        End If

    End Sub

    ''' <summary>
    ''' Occurs when a menu is clicked
    ''' </summary>
    Private Sub FoodItemAddBatchEventHandler_MenuClicked(ByVal sender As Object, _
                                                         ByVal e As Browser.Batch.BatchMenuEventArgs) _
                                                         Handles Me.MenuClicked
        If e.MenuItem.Key = SPELLCHECK_BUTTON_KEY Then
            If _togglestate = True Then
                SpellCheckGrid()
            End If

        End If
    End Sub

    Private Sub FoodItemAddBatch_LeaveCell(ByVal sender As Object, _
                                           ByVal e As Browser.Batch.BatchLeaveCellEventArgs) _
                                           Handles Me.LeaveCell
        'Our FoodItemAddBatchEventHandler class inherits from the 
        'Blackbaud.AppFx.Browser.Batch.BatchEntryHandler, which 
        'we will simply call BatchEntryHandler.   
        'BatchEntryHandler emits an event named LeaveCell.  
        'By handling this event within our FoodItemAddBatchEventHandler class, 
        'we can utilize the Blackbaud.AppFx.Browser.Batch.BatchLeaveCellEventArgs 
        'to uncover the column, row, and value of the cell that was vacated.

        If _togglestate = True Then
            If Not IsDBNull(e.Value) Then
                If Me.GetColumnIndexFromFieldID("NAME") = e.Column OrElse Me.GetColumnIndexFromFieldID("DESCRIPTION") = e.Column Then

                    Dim TextNeedingSpellChecking As String = CType(e.Value, String)
                    Dim SpellCheckResults As String = ""
                    'We can pass the value of the cell into the SpellCheck() function 
                    'to spell check the text.  Once the spell check is complete, 
                    'we can update the grid with the corrected spellings.  
                    SpellCheckResults = SpellCheck(TextNeedingSpellChecking)
                    'UpdateCell requires the coordinates of the row and column to 
                    'update which are provided by the event arguments.  
                    Me.UpdateCell(e.Row, e.Column, SpellCheckResults, SpellCheckResults)
                End If
            End If
        End If
    End Sub

    Private Function SpellCheck(ByVal TextBody As String) As String
        'The spell check function is used to spell check a series of words.  
        'The text body containing the words is checked using the CheckTextBodyV2 
        'operation of the spell checker’s check.asmx web service.   
        Dim spellchecker As New SpellCheckSvc.check
        Dim spellcheckresults As SpellCheckSvc.DocumentSummary
        Dim MisspelledWords() As SpellCheckSvc.Words

        'The results of the spell check come in the form of a 
        'DocumentSummary class provided by the web service.  
        spellcheckresults = spellchecker.CheckTextBodyV2(TextBody)

        'Within DocumentSummary you will find an array of misspelled words and 
        'associated spelling suggestions.  
        If spellcheckresults.MisspelledWordCount > 0 Then
            MisspelledWords = spellcheckresults.MisspelledWord
            For x = 0 To MisspelledWords.Length - 1
                Dim MisspelledWord As String = MisspelledWords(x).word
                Dim Suggestions() As String = MisspelledWords(x).Suggestions

                'For each misspelled word found within the text block, 
                'a custom windows form named SpellCheckForm.vb is used 
                'to display the misspelled word and the suggested corrections.    
                'Within the windows form, the end user must either 
                'correct the misspelling manually or select one of the suggested corrections.   
                Dim SpellCheckForm As New SpellCheckForm(MisspelledWord, Suggestions)
                SpellCheckForm.ShowDialog()

                'After the user closes the form, the misspelled word is replaced 
                'by the corrected word within the text body.  
                Dim selectedword As String = SpellCheckForm.SelectedSuggestedWord
                If selectedword.Length > 0 Then
                    TextBody = Replace(TextBody, MisspelledWord, selectedword)
                End If
            Next
        End If
        'Once all misspelled words have been dealt with, 
        'the corrected text body is returned from the function to the calling procedure.  
        Return TextBody
    End Function

    Private Sub SpellCheckGrid()
        Dim NameColumnNumber, DescColumnNumber As Integer

        NameColumnNumber = Me.GetColumnIndexFromFieldID("NAME")
        DescColumnNumber = Me.GetColumnIndexFromFieldID("DESCRIPTION")

        For x As Integer = 0 To Me.RowCount - 1
            Dim dfi As DataFormItem
            dfi = Me.GetRow(x)

            'Check the NAME column
            Dim TextNeedingSpellChecking As String
            Dim SpellCheckResults As String = ""
            If Not IsDBNull(dfi.Values("NAME").Value) Then
                TextNeedingSpellChecking = CType(dfi.Values("NAME").Value, String)

                SpellCheckResults = SpellCheck(TextNeedingSpellChecking)
                Me.UpdateCell(x, NameColumnNumber, SpellCheckResults, SpellCheckResults)
            End If

            If Not IsDBNull(dfi.Values("DESCRIPTION").Value) Then
                'Check the DESCRIPTION column
                TextNeedingSpellChecking = ""
                TextNeedingSpellChecking = CType(dfi.Values("DESCRIPTION").Value, String)
                SpellCheckResults = ""
                SpellCheckResults = SpellCheck(TextNeedingSpellChecking)
                Me.UpdateCell(x, DescColumnNumber, SpellCheckResults, SpellCheckResults)
            End If
        Next
        MsgBox("Spell Check Complete", MsgBoxStyle.OkOnly, "Spell Check")
    End Sub


End Class







