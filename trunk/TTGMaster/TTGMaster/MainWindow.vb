Imports System.Net.Sockets
Imports System.Text
Imports System.IO


Public Class MainWindow

    Dim versionNum As String = "v1.0.0"
    Dim clientSocket As New System.Net.Sockets.TcpClient()
    Dim serverStream As NetworkStream
    Dim readData As String
    Dim ctThread As Threading.Thread
    Public endThread As Boolean = False
    Public isHost As Boolean = False
    Dim boardLocked As Boolean = False

    Dim dragX As Integer = 0
    Dim dragY As Integer = 0
    Dim dragged As Control
    Dim dragOrigLeft As Integer = 0
    Dim dragOrigTop As Integer = 0

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
        ConnectWindow.Show()
    End Sub

    Public Sub goConnect(ByVal inName As String, ByVal IPAddress As String, ByVal portNumber As Integer)
        'Connects to the host
        readData = "Connected to Chat Server ..."
        msg()
        Try
            clientSocket.Connect(IPAddress, portNumber)
            serverStream = clientSocket.GetStream()
        Catch ex As Exception
            ConnectWindow.Show()
            ConnectWindow.Focus()
            ConnectWindow.CouldNotConnect.Visible = True
            Return
        End Try
        ConnectWindow.CouldNotConnect.Visible = False

        'Broadcasts entrance message
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(inName + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()

        'Starts connection thread
        endThread = False
        ctThread = New Threading.Thread(AddressOf Me.getMessage)
        ctThread.Start()

        'Enables form functions after connection has been established
        ChatInputBox.Enabled = True
        ConnectToolStripMenuItem.Enabled = False
        AddPieceToolStripMenuItem.Enabled = True
        LoadBoardToolStripMenuItem.Enabled = True
        AddDieToolStripMenuItem.Enabled = True
        If isHost Then
            LockBoardToolStripMenuItem.Enabled = True
        End If

    End Sub

    Private Sub ChatSendButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChatSendButton.Click
        ' Sends message
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(ChatInputBox.Text + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()

        'Resets chat input
        ChatInputBox.Text = ""
        ChatSendButton.Enabled = False
    End Sub

    Sub msg()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf msg))
        Else
            Dim actionText As Boolean = False

            If String.Compare(readData(0), "@") = 0 Then
                '@ is used for adding pieces
                Dim newPiece As String
                newPiece = Path.Combine(Directory.GetCurrentDirectory.ToString(), readData.Substring(1).Replace(vbNewLine, "").Replace(vbNullChar, ""))
                createNewPiece(newPiece, 0)
                actionText = True

            ElseIf String.Compare(readData(0), "&") = 0 Then
                '& is used for adding a die
                Dim newPiece As String
                newPiece = Path.Combine(Directory.GetCurrentDirectory.ToString(), readData.Substring(3).Replace(vbNewLine, "").Replace(vbNullChar, ""))
                createNewPiece(newPiece, Convert.ToInt32(readData.Substring(1, 2)))
                actionText = True

            ElseIf String.Compare(readData(0), "#") = 0 Then
                '# is used for changing the board image
                Dim bg As String
                bg = Path.Combine(Directory.GetCurrentDirectory.ToString(), readData.Substring(1).Replace(vbNewLine, "").Replace(vbNullChar, ""))
                changeBackground(bg)
                actionText = True

            ElseIf String.Compare(readData(0), "*") = 0 Then
                '* is used for moving pieces
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
                'dispose commands are used for deleting pieces
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
                'Lock commands are used for locking or unlocking the board
                Try
                    If (String.Compare(readData.Substring(0, 9), "lockboard") = 0) Then
                        LockBoard()
                    Else
                        UnlockBoard()
                    End If
                Catch ex As Exception
                End Try
                actionText = True
            ElseIf (isKILLCommand(readData)) Then
                'KILL command kills the game
                ChatBox.Text = ChatBox.Text + Environment.NewLine + "The game has ended.  Please restart TTGMaster to play again!"
                ChatBox.SelectionStart = ChatBox.TextLength
                ChatBox.ScrollToCaret()
                'System.Threading.Thread.Sleep(2000)
                disconnect()
                PlayArea.Enabled = False
                ChatInputBox.Enabled = False
                ChatSendButton.Enabled = False
                LockBoardToolStripMenuItem.Enabled = False
                AddDieToolStripMenuItem.Enabled = False
                AddPieceToolStripMenuItem.Enabled = False
                LoadBoardToolStripMenuItem.Enabled = False

                actionText = True
            End If

            'Displays a message publicly
            If (actionText = False) Or (actionText And ShowActionTextToolStripMenuItem.Checked) Then
                ChatBox.Text = ChatBox.Text + Environment.NewLine + " >> " + readData
                ChatBox.SelectionStart = ChatBox.TextLength
                ChatBox.ScrollToCaret()
            End If
        End If
    End Sub

    Private Function isKillCommand(ByVal inMsg As String)
        If inMsg.Length < 5 Then
            Return False
        End If

        If Not (String.Compare(inMsg.Substring(0, 5), "~KILL") = 0) Then
            Return False
        End If

        Return True
    End Function

    Private Function isDisposeCommand(ByVal inMsg As String)
        'Determines if a message is a dispose command

        If inMsg.Length <= 8 Then
            Return False
        End If

        If Not (String.Compare(inMsg.Substring(0, 8), "dispose ") = 0) Then
            Return False
        End If

        Return True
    End Function

    Private Function isLockCommand(ByVal inMsg As String)
        'Determines if a message is a lock or unlock command

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
        'Receives a message from the socket

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


    Private Sub PieceMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Starts dragging a piece
        dragX = e.X
        dragY = e.Y
        dragged = sender
        dragOrigLeft = dragged.Left
        dragOrigTop = dragged.Top
    End Sub


    Private Sub PieceDrag(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Moving the mouse while dragging a piece
        If (e.Button = MouseButtons.Left) Then
            dragged.Left += (e.X - dragX)
            dragged.Top += (e.Y - dragY)
        End If
    End Sub

    Private Sub PieceRelease(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Releases a piece

        If Not (boardLocked And Not isHost) Then
            If (e.Button = MouseButtons.Left) And Not ((dragged.Left = dragOrigLeft) And (dragged.Top = dragOrigTop)) Then
                Dim pieceNum As Integer
                Try
                    'If allowed, sends a movement message to the host
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
            If ImageDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Sends a board-changing message to the host
                Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("#" + ImageDialog.SafeFileName + "$")
                serverStream.Write(outStream, 0, outStream.Length)
                serverStream.Flush()
            End If
        End If
    End Sub

    Private Sub AddPieceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPieceToolStripMenuItem.Click
        addNewPiece()
    End Sub

    Public Sub addNewDie(ByVal sides As String)
        If Not (boardLocked And Not isHost) Then
            Dim imgFile As String = String.Concat("d", sides, ".png")
            'Sends a die-adding message to the host
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("&" + sides + imgFile + "$")
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
        End If
    End Sub

    Private Sub addNewPiece()
        If Not (boardLocked And Not isHost) Then
            If ImageDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Sends a piece-adding message to the host
                Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("@" + ImageDialog.SafeFileName + "$")
                serverStream.Write(outStream, 0, outStream.Length)
                serverStream.Flush()
            End If
        End If
    End Sub

    Private Sub createNewPiece(ByVal piecefile As String, ByVal sides As Integer)

        'Creates a new PictureBox object to represent the piece
        Dim PB As New PictureBox
        With PB
            .Name = "PiecePic" + pieceCount.ToString
            .SizeMode = PictureBoxSizeMode.AutoSize
            .Location = New System.Drawing.Point(50, 50)
            .ImageLocation = piecefile
        End With
        pieceCount += 1

        'Adds piece to the playarea
        PlayArea.Controls.Add(PB)
        BoardImage.SendToBack()

        ' Wire this control up to an appropriate event handler
        AddHandler PB.MouseDown, AddressOf PieceMouseDown
        AddHandler PB.MouseMove, AddressOf PieceDrag
        AddHandler PB.MouseUp, AddressOf PieceRelease
        AddHandler PB.MouseClick, AddressOf OpenContextMenu

        'Adds event to die if applicable
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
        'Enables or disables the chat send button based on whether there are characters typed

        If ChatInputBox.Text.Length > 0 Then
            ChatSendButton.Enabled = True
        Else
            ChatSendButton.Enabled = False
        End If
    End Sub

    Private Sub ShowMyIPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowMyIPToolStripMenuItem.Click
        'Displays the user's IP address

        Dim strIPAddress As String = ConnectWindow.GetIPAddress()
        MessageBox.Show("IP Address: " & strIPAddress)
    End Sub

    Private Sub AddDieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddDieToolStripMenuItem.Click
        DieWindow.Show()
    End Sub

    Private Sub OpenContextMenu(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Opens the piece removal context menu

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
                'Sends a piece-disposing message to the host
                pieceNum = Convert.ToInt32(dragged.Name.Substring(8))
                Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("dispose " + pieceNum.ToString + "$")
                serverStream.Write(outStream, 0, outStream.Length)
                serverStream.Flush()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub LockBoardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LockBoardToolStripMenuItem.Click
        'Sends a board-locking or board-unlocking message to the host

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
        'Disables board functionality if locked

        boardLocked = True

        If Not isHost Then
            PlayArea.Enabled = False
        End If
    End Sub

    Private Sub UnlockBoard()
        'Enables board functionality if unlocked

        boardLocked = False

        If Not isHost Then
            PlayArea.Enabled = True
        End If
    End Sub

    Private Sub disconnect()
        'Disconnects from the host

        Try
            'Sends a disconnection message to the host
            If isHost Then
                Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("~KILL" + "$")
                serverStream.Write(outStream, 0, outStream.Length)
                serverStream.Flush()
            Else
                Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes("%" + "$")
                serverStream.Write(outStream, 0, outStream.Length)
                serverStream.Flush()
            End If

            'Waits for thread to end ... scratch that, just kill it
            If ctThread.IsAlive Then
                Me.clientSocket.Close()
                ctThread.Abort()
                'While ctThread.IsAlive

                'End While
            End If
        Catch ex As Exception

        End Try

        'Stops hosting if applicable
        If isHost Then
            HostWindow.stopit()
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        disconnect()
    End Sub

End Class
