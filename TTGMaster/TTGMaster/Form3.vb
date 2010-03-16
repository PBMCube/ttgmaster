Public Class Form3
    Dim Thread1 As New System.Threading.Thread(AddressOf HostGameModule.Main)

    Public Sub go()
        Thread1.Start()
    End Sub

    Public Sub stopit()
        Thread1.Abort()
        Me.Close()
    End Sub
End Class