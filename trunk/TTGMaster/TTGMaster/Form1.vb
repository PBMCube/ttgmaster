Imports System.Net.Sockets
Imports System.Text


Public Class Form1

    Dim versionNum As String = "v0.1.0"
    Dim clientSocket As New System.Net.Sockets.TcpClient()
    Dim serverStream As NetworkStream
    Dim readData As String
    Dim ctThread As Threading.Thread
    Dim endThread As Boolean = False

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
        ctThread = New Threading.Thread(AddressOf getMessage)
        ctThread.Start()

        ChatInputBox.Enabled = True
        ChatSendButton.Enabled = True
        ConnectToolStripMenuItem.Enabled = False
        DisconnectToolStripMenuItem.Enabled = True

    End Sub

    Private Sub ChatSendButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChatSendButton.Click
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(ChatInputBox.Text + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
        ChatInputBox.Text = ""
    End Sub

    Sub msg()
        If Me.InvokeRequired Then
            Me.Invoke(New MethodInvoker(AddressOf msg))
        Else
            ChatBox.Text = ChatBox.Text + Environment.NewLine + " >> " + readData
        End If
    End Sub

    Private Sub getMessage()
        Dim keepGoing As Boolean = True
        While keepGoing
            serverStream = clientSocket.GetStream()
            Dim buffSize As Integer
            Dim inStream(10024) As Byte
            buffSize = clientSocket.ReceiveBufferSize
            serverStream.Read(inStream, 0, buffSize)
            Dim returndata As String = _
            System.Text.Encoding.ASCII.GetString(inStream)
            readData = "" + returndata
            msg()
            If endThread Then
                keepGoing = False
            End If
        End While
        clientSocket.Close()
        MsgBox("Disconnection Successful!", MsgBoxStyle.Information)
    End Sub



    Private Sub ChatInputBox_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ChatInputBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            ChatSendButton_Click(Me, EventArgs.Empty)
        End If

    End Sub

    Private Sub DisconnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectToolStripMenuItem.Click
        If ctThread.IsAlive Then
            endThread = True
            MsgBox("Assumably aborted...", MsgBoxStyle.Exclamation)
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

    Private Sub LoadBoardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadBoardToolStripMenuItem.Click
        ImageDialog.ShowDialog()
        BoardImage.ImageLocation = ImageDialog.FileName

    End Sub

    Private Sub AddPieceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddPieceToolStripMenuItem.Click
        addNewPiece()
    End Sub

    Private Sub addNewPiece()
        ImageDialog.ShowDialog()
        Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(ImageDialog.FileName + "$")
        serverStream.Write(outStream, 0, outStream.Length)
        serverStream.Flush()
        ChatInputBox.Text = ""
    End Sub

    Private Sub createNewPiece(ByVal piecefile As String)
        Dim PB As New PictureBox
        With PB
            .Name = "PiecePic" + pieceCount.ToString
            .SizeMode = PictureBoxSizeMode.AutoSize
            .BorderStyle = BorderStyle.FixedSingle
            .Location = New System.Drawing.Point(50, 50)
            .ImageLocation = piecefile
            '  Note you can set more of the PicBox's Properties here
        End With

        '  This is the line that sometimes catches people out!
        PlayArea.Controls.Add(PB)
        BoardImage.SendToBack()

        '  Wire this control up to an appropriate event handler
        AddHandler PB.MouseDown, AddressOf PieceMouseDown
        AddHandler PB.MouseMove, AddressOf PieceDrag
    End Sub

End Class
