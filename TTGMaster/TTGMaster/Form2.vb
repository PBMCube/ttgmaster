Public Class Form2

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        HostGroup.Enabled = True
        JoinGroup.Enabled = False
        ConnectButton.Enabled = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        HostGroup.Enabled = False
        JoinGroup.Enabled = True
        ConnectButton.Enabled = True
    End Sub
End Class