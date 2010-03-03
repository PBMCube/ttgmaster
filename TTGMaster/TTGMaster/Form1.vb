Public Class MainForm

    Dim versionNum As String = "v0.1.0"

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim temp As String = "Tabletop Game Master " + versionNum + vbNewLine
        temp += "Created by Team Awesome"
        MsgBox(temp, MsgBoxStyle.OkOnly, "About")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click

    End Sub
End Class
