﻿Imports System.Net.Sockets
Imports System.Text

Module HostGameModule
    Dim clientsList As New Hashtable
    Sub Main(ByVal portNumber As Integer)
        Dim serverSocket As New TcpListener(portNumber)
        Dim clientSocket As TcpClient
        Dim counter As Integer

        serverSocket.Start()
        msg("Chat Server Started ....")
        counter = 0

        While (True)
            counter += 1
            clientSocket = serverSocket.AcceptTcpClient()

            Dim bytesFrom(10024) As Byte
            Dim dataFromClient As String

            Dim networkStream As NetworkStream = _
            clientSocket.GetStream()
            networkStream.Read(bytesFrom, 0, CInt(clientSocket.ReceiveBufferSize))
            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom)
            dataFromClient = _
            dataFromClient.Substring(0, dataFromClient.IndexOf("$"))

            clientsList(dataFromClient) = clientSocket

            broadcast("Joined: " + dataFromClient, dataFromClient, False)

            msg(dataFromClient + " joined the game")
            Dim client As New handleClinet
            client.startClient(clientSocket, dataFromClient, clientsList)
        End While

        clientSocket.Close()
        serverSocket.Stop()
        msg("exit")
        Console.ReadLine()
    End Sub

    Sub msg(ByVal mesg As String)
        mesg.Trim()
        Console.WriteLine(" >> " + mesg)
    End Sub
    Private Sub broadcast(ByVal msg As String, _
    ByVal uName As String, ByVal flag As Boolean)
        Dim Item As DictionaryEntry
        For Each Item In clientsList
            Dim broadcastSocket As TcpClient
            broadcastSocket = CType(Item.Value, TcpClient)
            Dim broadcastStream As NetworkStream = _
                    broadcastSocket.GetStream()
            Dim broadcastBytes As [Byte]()

            If flag = True Then
                If (String.Compare(msg(0), "@") = 0) Or (String.Compare(msg(0), "#") = 0) Or (String.Compare(msg(0), "*") = 0) Or (String.Compare(msg(0), "^") = 0) Then
                    broadcastBytes = Encoding.ASCII.GetBytes(msg)
                ElseIf (String.Compare(msg.Substring(0, 4), "roll") = 0) Then
                    broadcastBytes = Encoding.ASCII.GetBytes(rollDie(msg.Substring(5)))
                Else
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg)
                End If
            Else
                broadcastBytes = Encoding.ASCII.GetBytes(msg)
            End If

            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length)
            broadcastStream.Flush()
        Next
    End Sub

    Private Function rollDie(ByVal msg As String)
        Dim splitS As String() = msg.Split("d")
        If Not splitS.Length = 2 Then
            Return msg
        End If

        Dim num As Integer = Convert.ToInt32(splitS(0))
        Dim die As Integer = Convert.ToInt32(splitS(1))
        Dim r As New Random(System.DateTime.Now.Millisecond)

        If (num < 1) Or (die < 2) Then
            Return msg
        End If

        Dim result As Integer
        Dim total As Integer = 0
        Dim i As Integer

        For i = 1 To num
            result = r.Next(1, die + 1)
            total += result
        Next

        Return "Rolled " + msg + ": " + total.ToString
    End Function

    Public Class handleClinet
        Dim clientSocket As TcpClient
        Dim clNo As String
        Dim clientsList As Hashtable

        Public Sub startClient(ByVal inClientSocket As TcpClient, _
        ByVal clineNo As String, ByVal cList As Hashtable)
            Me.clientSocket = inClientSocket
            Me.clNo = clineNo
            Me.clientsList = cList
            Dim ctThread As Threading.Thread = New Threading.Thread(AddressOf doChat)
            ctThread.Start()
        End Sub

        Private Sub doChat()
            'Dim infiniteCounter As Integer
            Dim requestCount As Integer
            Dim bytesFrom(10024) As Byte
            Dim dataFromClient As String
            Dim sendBytes As [Byte]()
            Dim serverResponse As String
            Dim rCount As String
            requestCount = 0
            Dim checker As Boolean = True

            While (checker)
                Try
                    requestCount = requestCount + 1
                    Dim networkStream As NetworkStream = _
                            clientSocket.GetStream()
                    networkStream.Read(bytesFrom, 0, CInt(clientSocket.ReceiveBufferSize))
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom)
                    dataFromClient = _
            dataFromClient.Substring(0, dataFromClient.IndexOf("$"))
                    msg("From client - " + clNo + " : " + dataFromClient)
                    rCount = Convert.ToString(requestCount)

                    broadcast(dataFromClient, clNo, True)
                Catch ex As Exception
                    checker = False
                End Try
            End While
        End Sub

    End Class
End Module

