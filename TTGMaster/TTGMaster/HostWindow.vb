Public Class HostWindow
    Dim Thread1 As System.Threading.Thread

    Public Sub go(ByVal portNumber As Integer)
        'Begins the host thread
        Thread1 = New System.Threading.Thread(AddressOf HostGameModule.Main)
        Thread1.Start(portNumber)
    End Sub

    Public Sub stopit()
        'Attempts to end the host thread
        HostGameModule.keepGoing = False
        Thread1.Abort()
        Me.Close()
    End Sub
End Class