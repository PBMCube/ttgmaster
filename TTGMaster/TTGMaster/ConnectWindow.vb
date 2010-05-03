Imports System.Net.Sockets
Imports System.Text

Public Class ConnectWindow

    Public Function GetIPAddress() As String
        'Gets the user's current IP address

        Dim strHostName As String
        Dim strIPAddress As String = ""
        strHostName = System.Net.Dns.GetHostName()
        Dim i As Integer
        Dim myBound As Integer = System.Net.Dns.GetHostByName(strHostName).AddressList.Length() - 1

        'Iterates through all possible IP addresses
        For i = 0 To myBound
            strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(i).ToString()
            Dim strIPseg As String() = strIPAddress.Split(".")
            Dim seg1 As Integer = Convert.ToInt32(strIPseg(0))
            Dim seg2 As Integer = Convert.ToInt32(strIPseg(1))

            'Bypasses invalid addresses
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
            Return "Error: may be behind a router"
        End If
        Return strIPAddress
    End Function

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HostRadio.CheckedChanged
        'Enables and disables features based on selecting the "Host Game" option

        HostGroup.Enabled = True
        JoinGroup.Enabled = False
        IPAddressBox.Text = GetIPAddress()
        IPAddressBox.Enabled = False
        GameNameBox.Enabled = True
        checkForEnable()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JoinRadio.CheckedChanged
        'Enables and disables features based on selecting the "Join Game" option

        HostGroup.Enabled = False
        JoinGroup.Enabled = True
        IPAddressBox.Enabled = True
        IPAddressBox.Text = ""
        GameNameBox.Enabled = False
        checkForEnable()
    End Sub

    Private Sub ConnectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectButton.Click
        'The connect button is clicked

        'Checks to make sure the button should be enabled
        checkForEnable()
        If ConnectButton.Enabled Then
            Dim name As String = UserName.Text.ToString

            If JoinRadio.Checked Then
                'Joins game as client
                MainWindow.setIsHost(False)
                joinGame(name, IPAddressBox.Text.ToString, PortNumber.Value)
            Else
                'Hosts game and also joins self as client
                MainWindow.setIsHost(True)
                hostGame(PortNumber.Value)
                joinGame(name, "127.0.0.1", PortNumber.Value)
            End If
        End If
    End Sub

    Private Sub hostGame(ByVal portNumber As Integer)
        HostWindow.Show()
        HostWindow.go(portNumber)
    End Sub

    Private Sub joinGame(ByVal inName As String, ByVal IPAddress As String, ByVal portNumber As Integer)
        MainWindow.goConnect(inName, IPAddress, portNumber)
        Me.Close()
        MainWindow.Focus()
    End Sub

    Private Sub UserName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserName.TextChanged
        checkForEnable()
    End Sub

    Private Sub PortNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        checkForEnable()
    End Sub

    Private Sub checkForEnable()
        'Checks to see if the Connect Button should be enabled

        If (UserName.Text.Length > 0) And ((HostRadio.Checked) Or (JoinRadio.Checked)) And (PortNumber.Value > 0) Then
            ConnectButton.Enabled = True
        Else
            ConnectButton.Enabled = False
        End If
    End Sub

    Private Sub PortNumber_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PortNumber.ValueChanged
        checkForEnable()
    End Sub
End Class