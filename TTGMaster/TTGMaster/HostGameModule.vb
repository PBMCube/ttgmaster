﻿Imports System.Net.Sockets
Imports System.Text

Module HostGameModule
    Public clientSocket As TcpClient
    Public keepGoing As Boolean = True

    Dim clientsList As New Hashtable
    Sub Main(ByVal portNumber As Integer)
        Dim serverSocket As New TcpListener(portNumber)
        Dim counter As Integer

        serverSocket.Start()
        msg("Chat Server Started ....")

        'Main loop
        While (keepGoing)
            If serverSocket.Pending() Then
                clientSocket = serverSocket.AcceptTcpClient()

                Dim bytesFrom(10024) As Byte
                Dim dataFromClient As String

                'Waits for new connections
                Dim networkStream As NetworkStream = clientSocket.GetStream()

                networkStream.Read(bytesFrom, 0, CInt(clientSocket.ReceiveBufferSize))
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom)
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"))

                clientsList(dataFromClient) = clientSocket

                broadcast(dataFromClient + " Joined ", dataFromClient, False)

                msg(dataFromClient + " Joined chat room ")
                Dim client As New handleClient
                client.startClient(clientSocket, dataFromClient, clientsList)
            End If
        End While

        clientSocket.Close()
        serverSocket.Stop()
    End Sub

    Sub msg(ByVal mesg As String)
        mesg.Trim()
        Console.WriteLine(" >> " + mesg)
    End Sub

    Private Sub broadcast(ByVal msg As String, _
    ByVal uName As String, ByVal flag As Boolean)
        'Broadcasts a message to all users.  Acts on special commands.

        'Rolls die once if message is a dice roll command
        Dim diceRollMsg As String = "MissingNo."
        If isDiceCommand(msg) Then
            diceRollMsg = rollDie(msg.Substring(5), uName)
        End If
        Dim Item As DictionaryEntry

        'Acts on disconnection message
        If (String.Compare(msg(0), "%") = 0) Then
            For Each Item In clientsList
                If (String.Compare(Item.Key, uName) = 0) Then
                    Dim closeSocket As TcpClient = CType(Item.Value, TcpClient)
                    closeSocket.Close()
                    clientsList.Remove(Item.Key)
                    Exit For
                End If
            Next
        End If

        'Sends message to each client
        For Each Item In clientsList
            Dim broadcastSocket As TcpClient
            broadcastSocket = CType(Item.Value, TcpClient)
            Dim broadcastStream As NetworkStream = _
                    broadcastSocket.GetStream()
            Dim broadcastBytes As [Byte]()

            If flag = True Then
                'Sets the message for special commands in non-formated manner
                If (String.Compare(msg(0), "@") = 0) Or (String.Compare(msg(0), "#") = 0) _
                Or (String.Compare(msg(0), "*") = 0) Or (String.Compare(msg(0), "&") = 0) _
                Or (isDisposeCommand(msg)) Or (isLockCommand(msg)) Or (isKillCommand(msg)) Then

                    broadcastBytes = Encoding.ASCII.GetBytes(msg)
                ElseIf isDiceCommand(msg) Then
                    broadcastBytes = Encoding.ASCII.GetBytes(diceRollMsg)
                ElseIf String.Compare(msg(0), "%") = 0 Then
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " disconnects.")
                Else
                    'Sets a non-command message
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg)
                End If
            Else
                broadcastBytes = Encoding.ASCII.GetBytes(msg)
            End If

            'Sends the message
            broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length)
            broadcastStream.Flush()
        Next
    End Sub

    Private Function isDiceCommand(ByVal inMsg As String)
        If inMsg.Length <= 5 Then
            Return False
        End If

        If Not (String.Compare(inMsg.Substring(0, 5), "roll ") = 0) Then
            Return False
        End If

        Return True
    End Function

    Private Function isDisposeCommand(ByVal inMsg As String)
        If inMsg.Length <= 8 Then
            Return False
        End If

        If Not (String.Compare(inMsg.Substring(0, 8), "dispose ") = 0) Then
            Return False
        End If

        Return True
    End Function

    Private Function isLockCommand(ByVal inMsg As String)
        If inMsg.Length < 9 Then
            Return False
        End If

        If (String.Compare(inMsg.Substring(0, 9), "lockboard") = 0) Then
            Return True
        End If

        If inMsg.Length < 11 Then
            Return False
        End If

        If (String.Compare(inMsg.Substring(0, 11), "unlockboard") = 0) Then
            Return True
        End If

        Return False

    End Function

    Private Function isKillCommand(ByVal inMsg As String)
        If inMsg.Length < 5 Then
            Return False
        End If

        If Not (String.Compare(inMsg.Substring(0, 5), "~KILL") = 0) Then
            Return False
        End If

        Return True
    End Function

    Private Function rollDie(ByVal inMsg As String, ByVal userName As String)
        'Rolls virtual dice

        Dim splitS As String() = inMsg.Split("d")
        If Not splitS.Length = 2 Then
            Return "Invalid Roll"
        End If

        Dim num As Integer = Convert.ToInt32(splitS(0))
        Dim die As Integer = Convert.ToInt32(splitS(1))
        Dim r As New Random(System.DateTime.Now.Millisecond)

        If (num < 1) Or (die < 2) Then
            Return "Invalid Roll"
        End If

        Dim result As Integer
        Dim total As Integer = 0
        Dim i As Integer

        For i = 1 To num
            result = r.Next(1, die + 1)
            total += result
        Next

        Dim final As String = (userName + " rolls " + inMsg + ": " + total.ToString).ToString()
        Return final
    End Function

    Public Class handleClient
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
            'Code that is called by the main connection thread

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

