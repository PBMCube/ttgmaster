Imports System.Net.Sockets
Imports System.Text
Imports System.IO


Public Class Form1

    Dim versionNum As String = "v0.7.0"
    Dim clientSocket As New System.Net.Sockets.TcpClient()
    Dim serverStream As NetworkStream
    Dim readData As String
    Dim ctThread As Threading.Thread
    Dim endThread As Boolean = False
    Dim isHost As Boolean = False
    Dim boardLocked As Boolean = False

    Dim dragX As Integer = 0
    Dim dragY As Integer = 0
    Dim dragged As Control

    Dim pieceCount = 0
    Dim pieces As List(Of Control)



    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim temp As String = "Tabletop Game Master " + versionNum + vbNewLine
        temp += "Created by Team Awesome"
        MsgBox(temp, MsgBoxStyle.OkOnly, "About")
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Public Sub goConnect(ByVal inName As String, ByVal IPAddress As String, ByVal portNumber As Integer)
        readData = "Connected to Chat Server ..."
        msg()
        Try
            clientSocket.Connect(IPAddress, portNumber)
            serverStream = clientSocket.GetStream()
        Catch ex As Exception
            Form2.Show()
            Form2.Focus()
            Form2.CouldNotConnect.Visible = True
            Return
        End Try
        Form2.CouldNotConnect.Visible = False


        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(inName + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()

        endThread = False
        ctThread = New Threading.Thread(AddressOf Me.getMessage)
        ctThread.Start()

        ChatInputBox.Enabled = True
        ConnectToolStripMenuItem.Enabled = False
        DisconnectToolStripMenuItem.Enabled = True
        AddPieceToolStripMenuItem.Enabled = True
        LoadBoardToolStripMenuItem.Enabled = True
        AddDieToolStripMenuItem.Enabled = True
        If isHost Then
            LockBoardToolStripMenuItem.Enabled = True
        End If

    End Sub

    Private Sub ChatSendButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChatSendButton.Click
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(ChatInputBox.Text + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
        ChatInputBox.Text = ""
        ChatSendButton.Enabled = False
    End Sub

    Sub msg()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf msg))
        Else
            Dim actionText As Boolean = False

            If String.Compare(readData(0), "@") = 0 Then
                Dim newPiece As String
                newPiece = Path.Combine(Directory.GetCurrentDirectory.ToString(), readData.Substring(1).Replace(vbNewLine, "").Replace(vbNullChar, ""))
                createNewPiece(newPiece, 0)
                actionText = True
            ElseIf String.Compare(readData(0), "&") = 0 Then
                Dim newPiece As String
                newPiece = Path.Combine(Directory.GetCurrentDirectory.ToString(), readData.Substring(3).Replace(vbNewLine, "").Replace(vbNullChar, ""))
                createNewPiece(newPiece, Convert.ToInt32(readData.Substring(1, 2)))
                actionText = True
            ElseIf String.Compare(readData(0), "#") = 0 Then
                Dim bg As String
                bg = Path.Combine(Directory.GetCurrentDirectory.ToString(), readData.Substring(1).Replace(vbNewLine, "").Replace(vbNullChar, ""))
                changeBackground(bg)
                actionText = True
            ElseIf String.Compare(readData(0), "*") = 0 Then
                Try
                    Dim splitS As String() = (readData.Substring(1)).Split("*")
                    Dim pieceNum As Integer = Convert.ToInt32(splitS(0))
                    Dim xPos As Integer = Convert.ToInt32(splitS(1))
                    Dim yPos As Integer = Convert.ToInt32(splitS(2))
                    Dim pieceArray As Control() = PlayArea.Controls.Find("PiecePic" + pieceNum.ToString(), True)
                    Dim thePiece = pieceArray(0)
                    thePiece.Left = xPos
                    thePiece.Top = yPos
                Catch ex As Exception
                End Try
                actionText = True
            ElseIf (isDisposeCommand(readData)) Then
                Try
                    Dim pieceNumS = readData.Substring(8)
                    Dim pieceNum As Integer = Convert.ToInt32(pieceNumS)
                    Dim pieceArray As Control() = PlayArea.Controls.Find("PiecePic" + pieceNum.ToString(), True)
                    Dim thePiece = pieceArray(0)
                    thePiece.Dispose()
                Catch ex As Exception
                End Try
                actionText = True
            ElseIf (isLockCommand(readData)) Then
                Try
                    If (String.Compare(readData.Substring(0, 9), "lockboard") = 0) Then
                        LockBoard()
                    Else
                        UnlockBoard()
                    End If
                Catch ex As Exception
                End Try
                actionText = True
            End If

            If (actionText = False) Or (actionText And ShowActionTextToolStripMenuItem.Checked) Then
                ChatBox.Text = ChatBox.Text + Environment.NewLine + " >> " + readData
                ChatBox.SelectionStart = ChatBox.TextLength
                ChatBox.ScrollToCaret()
            End If
        End If
    End Sub

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

    Private Sub getMessage()
        While endThread = False
            serverStream = clientSocket.GetStream()
            Dim buffSize As Integer
            Dim inStream(10024) As Byte
            buffSize = clientSocket.ReceiveBufferSize
            Try
                serverStream.Read(inStream, 0, buffSize)
                Dim returndata As String = _
                System.Text.Encoding.ASCII.GetString(inStream)
                readData = "" + returndata
                msg()
            Catch
                endThread = True
            End Try
        End While
    End Sub



    Private Sub ChatInputBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChatInputBox.KeyDown
        If e.KeyCode = Keys.Enter And ChatSendButton.Enabled Then
            ChatSendButton_Click(Me, EventArgs.Empty)
        End If

    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        If ctThread.IsAlive Then
            Me.clientSocket.Close()
            While ctThread.IsAlive

            End While
            MsgBox("Assumably aborted...", MsgBoxStyle.Exclamation)
        End If

        ChatInputBox.Enabled = False
        ChatSendButton.Enabled = False
        ConnectToolStripMenuItem.Enabled = True
        DisconnectToolStripMenuItem.Enabled = False
        AddPieceToolStripMenuItem.Enabled = False
        LoadBoardToolStripMenuItem.Enabled = False
        AddDieToolStripMenuItem.Enabled = False
        LockBoardToolStripMenuItem.Enabled = False

        If isHost Then
            Form3.stopit()
        End If
    End Sub

    Private Sub PieceMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        dragX = e.X
        dragY = e.Y
        dragged = sender
    End Sub


    Private Sub PieceDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If (e.Button = MouseButtons.Left) Then
            dragged.Left += (e.X - dragX)
            dragged.Top += (e.Y - dragY)
        End If
    End Sub

    Private Sub PieceRelease(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If Not (boardLocked And Not isHost) Then
            If (e.Button = MouseButtons.Left) Then
                Dim pieceNum As Integer
                Try
                    pieceNum = Convert.ToInt32(dragged.Name.Substring(8))
                    Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("*" + pieceNum.ToString + "*" + dragged.Left.ToString + "*" + dragged.Top.ToString + "$")
                    serverStream.Write(outStream, 0, outStream.Length)
                    serverStream.Flush()
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

    Private Sub LoadBoardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadBoardToolStripMenuItem.Click
        If Not (boardLocked And Not isHost) Then
            ImageDialog.ShowDialog()
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("#" + ImageDialog.SafeFileName + "$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
        End If
    End Sub

    Private Sub AddPieceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPieceToolStripMenuItem.Click
        addNewPiece()
    End Sub

    Public Sub addNewDie(ByVal sides As String)
        If Not (boardLocked And Not isHost) Then
            Dim imgFile As String = String.Concat("d", sides, ".png")
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("&" + sides + imgFile + "$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
        End If
    End Sub

    Private Sub addNewPiece()
        If Not (boardLocked And Not isHost) Then
            ImageDialog.ShowDialog()
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("@" + ImageDialog.SafeFileName + "$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
        End If
    End Sub

    Private Sub createNewPiece(ByVal piecefile As String, ByVal sides As Integer)
        Dim PB As New PictureBox
        With PB
            .Name = "PiecePic" + pieceCount.ToString
            .SizeMode = PictureBoxSizeMode.AutoSize
            .Location = New System.Drawing.Point(50, 50)
            .ImageLocation = piecefile
        End With
        pieceCount += 1

        '  This is the line that sometimes catches people out!
        PlayArea.Controls.Add(PB)
        BoardImage.SendToBack()

        '  Wire this control up to an appropriate event handler
        AddHandler PB.MouseDown, AddressOf PieceMouseDown
        AddHandler PB.MouseMove, AddressOf PieceDrag
        AddHandler PB.MouseUp, AddressOf PieceRelease
        AddHandler PB.MouseClick, AddressOf OpenContextMenu

        If sides = 4 Then
            AddHandler PB.DoubleClick, AddressOf rollDie4
        ElseIf sides = 2 Then
            AddHandler PB.DoubleClick, AddressOf rollDie2
        ElseIf sides = 6 Then
            AddHandler PB.DoubleClick, AddressOf rollDie6
        ElseIf sides = 8 Then
            AddHandler PB.DoubleClick, AddressOf rollDie8
        ElseIf sides = 10 Then
            AddHandler PB.DoubleClick, AddressOf rollDie10
        ElseIf sides = 12 Then
            AddHandler PB.DoubleClick, AddressOf rollDie12
        ElseIf sides = 20 Then
            AddHandler PB.DoubleClick, AddressOf rollDie20
        End If
    End Sub

    Private Sub rollDie4()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("roll 1d4" + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
    End Sub

    Private Sub rollDie2()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("roll 1d2" + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
    End Sub

    Private Sub rollDie6()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("roll 1d6" + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
    End Sub

    Private Sub rollDie8()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("roll 1d8" + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
    End Sub

    Private Sub rollDie10()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("roll 1d10" + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
    End Sub

    Private Sub rollDie12()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("roll 1d12" + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
    End Sub

    Private Sub rollDie20()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("roll 1d20" + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
    End Sub

    Private Sub changeBackground(ByVal bgfile As String)
        BoardImage.ImageLocation = bgfile
    End Sub

    Public Sub setIsHost(ByVal v)
        isHost = v
    End Sub

    Private Sub ChatInputBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChatInputBox.TextChanged
        If ChatInputBox.Text.Length > 0 Then
            ChatSendButton.Enabled = True
        Else
            ChatSendButton.Enabled = False
        End If
    End Sub

    Private Sub ShowMyIPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowMyIPToolStripMenuItem.Click
        Dim strIPAddress As String = Form2.GetIPAddress()
        MessageBox.Show("IP Address: " & strIPAddress)
    End Sub

    Private Sub AddDieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddDieToolStripMenuItem.Click
        Form4.Show()
    End Sub

    Private Sub OpenContextMenu(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If (e.Button = MouseButtons.Right) Then
            PieceContextMenu.Show()
            PieceContextMenu.Left = Me.Left + dragged.Left
            PieceContextMenu.Top = Me.Top + dragged.Top + 25
        End If

    End Sub

    Private Sub RemovePieceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemovePieceToolStripMenuItem.Click
        If Not (boardLocked And Not isHost) Then
            Dim pieceNum As Integer
            Try
                pieceNum = Convert.ToInt32(dragged.Name.Substring(8))
                Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("dispose " + pieceNum.ToString + "$")
                serverStream.Write(outStream, 0, outStream.Length)
                serverStream.Flush()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub LockBoardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockBoardToolStripMenuItem.Click
        If boardLocked Then
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("unlockboard" + "$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
        Else
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("lockboard" + "$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
        End If
    End Sub

    Private Sub LockBoard()
        boardLocked = True

        If Not isHost Then
            PlayArea.Enabled = False
        End If
    End Sub

    Private Sub UnlockBoard()
        boardLocked = False

        If Not isHost Then
            PlayArea.Enabled = True
        End If
    End Sub
End Class
