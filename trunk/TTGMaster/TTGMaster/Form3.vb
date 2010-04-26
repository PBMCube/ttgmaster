Public Class Form3
    Dim Thread1 As System.Threading.Thread

    Public Sub go(ByVal portNumber As Integer)
        Thread1 = New System.Threading.Thread(AddressOf HostGameModule.Main)
        Thread1.Start(portNumber)
    End Sub

    Public Sub stopit()
        Thread1.Abort()
        Me.Close()
    End Sub

End Class