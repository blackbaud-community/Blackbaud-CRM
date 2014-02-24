Public Class SpellCheckForm
    Private _misspelledWord As String
    Private _suggestedWords() As String
    Private _selectedSuggestedWord As String

    Public Sub New(ByVal MisspelledWord As String, ByVal Suggestions() As String)
        _misspelledWord = MisspelledWord
        _suggestedWords = Suggestions

        InitializeComponent()
        PopulateSuggestedWords()

        Me.txtManual.Text = MisspelledWord
        Me.Text = "Suggested Spellings for " & MisspelledWord
        _selectedSuggestedWord = ""
    End Sub

    Public Property SelectedSuggestedWord() As String
        Get
            Return _selectedSuggestedWord
        End Get
        Set(ByVal value As String)
            _selectedSuggestedWord = value
        End Set
    End Property

    Private Sub PopulateSuggestedWords()

        If _suggestedWords Is Nothing Then
            lstSuggestions.Items.Add("No spelling suggestions")
            lstSuggestions.Enabled = False
        Else
            If _suggestedWords.Length = 0 Then
                lstSuggestions.Items.Add("No spelling suggestions")
                lstSuggestions.Enabled = False
            Else
                Dim IsUpper As Boolean = Char.IsUpper(_misspelledWord(0))

                If IsUpper = True Then
                    For Each word As String In _suggestedWords
                        Dim firstchar As String = word(0)
                        firstchar = firstchar.ToUpper()
                        word = word.Remove(0, 1)
                        word = firstchar & word
                        lstSuggestions.Items.Add(word)
                    Next
                Else
                    lstSuggestions.Items.AddRange(_suggestedWords)
                End If
            End If
        End If
    End Sub

    Private Sub txtManual_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtManual.TextChanged
        Me.cmdChange.Enabled = txtManual.TextLength > 0
    End Sub

    Private Sub lstSuggestions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSuggestions.SelectedIndexChanged
        _selectedSuggestedWord = lstSuggestions.SelectedItem.ToString
        Me.Close()
    End Sub

    Private Sub cmdChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdChange.Click
        _selectedSuggestedWord = Me.txtManual.Text.ToString
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        _selectedSuggestedWord = Me.txtManual.Text.ToString
        Me.Close()
    End Sub

    Private Sub SpellCheckForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class