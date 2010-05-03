Public Class DieWindow
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox1.SelectedItem = "" Then
            Return
        End If

        MainWindow.addNewDie(ComboBox1.SelectedItem)
        Close()
    End Sub
End Class