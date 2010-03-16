Imports System.Net.Sockets
Imports System.Text

Public Class Form2

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HostRadio.CheckedChanged
        HostGroup.Enabled = True
        JoinGroup.Enabled = False
        checkForEnable()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JoinRadio.CheckedChanged
        HostGroup.Enabled = False
        JoinGroup.Enabled = True
        checkForEnable()
    End Sub

    Private Sub ConnectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectButton.Click
        Dim n As String = UserName.Text.ToString

        If JoinRadio.Checked Then
            joinGame(n)
        Else
            hostGame()
            joinGame(n)
        End If
    End Sub

    Private Sub hostGame()
        Form3.Show()
        Form3.go()
        Me.Close()
        Form1.Focus()
    End Sub

    Private Sub joinGame(ByVal inName As String)
        Form1.goConnect(inName)
    End Sub

    Private Sub UserName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserName.TextChanged
        checkForEnable()
    End Sub

    Private Sub PortNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        checkForEnable()
    End Sub

    Private Sub checkForEnable()
        If (UserName.Text.Length > 0) And ((HostRadio.Checked) Or (JoinRadio.Checked)) Then
            ConnectButton.Enabled = True
        Else
            ConnectButton.Enabled = False
        End If
    End Sub
End Class