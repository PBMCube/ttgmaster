Imports System.Net.Sockets
Imports System.Text

Public Class Form2

    Public Function GetIPAddress() As String
        Dim strHostName As String
        Dim strIPAddress As String = ""
        strHostName = System.Net.Dns.GetHostName()
        Dim i As Integer
        Dim myBound As Integer = System.Net.Dns.GetHostByName(strHostName).AddressList.Length() - 1
        For i = 0 To myBound
            strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(i).ToString()
            Dim strIPseg As String() = strIPAddress.Split(".")
            Dim seg1 As Integer = Convert.ToInt32(strIPseg(0))
            Dim seg2 As Integer = Convert.ToInt32(strIPseg(1))

            If seg1 = 0 Then
                Continue For
            ElseIf seg1 = 10 Then
                Continue For
            ElseIf seg1 = 169 And seg2 = 254 Then
                Continue For
            ElseIf seg1 = 192 And seg2 = 168 Then
                Continue For
            Else
                Exit For
            End If
        Next i

        If i > myBound Then
            Return "ERROR"
        End If
        Return strIPAddress
    End Function

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HostRadio.CheckedChanged
        HostGroup.Enabled = True
        JoinGroup.Enabled = False
        IPAddressBox.Text = GetIPAddress()
        IPAddressBox.Enabled = False
        GameNameBox.Enabled = True
        checkForEnable()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JoinRadio.CheckedChanged
        HostGroup.Enabled = False
        JoinGroup.Enabled = True
        IPAddressBox.Enabled = True
        IPAddressBox.Text = ""
        GameNameBox.Enabled = False
        checkForEnable()
    End Sub

    Private Sub ConnectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectButton.Click
        Dim n As String = UserName.Text.ToString

        If JoinRadio.Checked Then
            Form1.setIsHost(False)
            joinGame(n, IPAddressBox.Text.ToString, PortNumber.Value)
        Else
            Form1.setIsHost(True)
            hostGame(PortNumber.Value)
            joinGame(n, "127.0.0.1", PortNumber.Value)
        End If
    End Sub

    Private Sub hostGame(ByVal portNumber As Integer)
        Form3.Show()
        Form3.go(portNumber)
        Me.Close()
        Form1.Focus()
    End Sub

    Private Sub joinGame(ByVal inName As String, ByVal IPAddress As String, ByVal portNumber As Integer)
        Form1.goConnect(inName, IPAddress, portNumber)
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