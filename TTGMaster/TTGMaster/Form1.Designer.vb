<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ChatInputBox = New System.Windows.Forms.TextBox
        Me.ChatSendButton = New System.Windows.Forms.Button
        Me.PlayerList = New System.Windows.Forms.TextBox
        Me.ChatBox = New System.Windows.Forms.TextBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DisconnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadBoardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddPieceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PlayArea = New System.Windows.Forms.GroupBox
        Me.BoardImage = New System.Windows.Forms.PictureBox
        Me.ImageDialog = New System.Windows.Forms.OpenFileDialog
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ShowActionTextToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        Me.PlayArea.SuspendLayout()
        CType(Me.BoardImage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChatInputBox
        '
        Me.ChatInputBox.Enabled = False
        Me.ChatInputBox.Location = New System.Drawing.Point(194, 632)
        Me.ChatInputBox.Name = "ChatInputBox"
        Me.ChatInputBox.Size = New System.Drawing.Size(297, 20)
        Me.ChatInputBox.TabIndex = 0
        '
        'ChatSendButton
        '
        Me.ChatSendButton.Enabled = False
        Me.ChatSendButton.Location = New System.Drawing.Point(497, 630)
        Me.ChatSendButton.Name = "ChatSendButton"
        Me.ChatSendButton.Size = New System.Drawing.Size(75, 23)
        Me.ChatSendButton.TabIndex = 1
        Me.ChatSendButton.Text = "Send"
        Me.ChatSendButton.UseVisualStyleBackColor = True
        '
        'PlayerList
        '
        Me.PlayerList.Location = New System.Drawing.Point(12, 451)
        Me.PlayerList.Multiline = True
        Me.PlayerList.Name = "PlayerList"
        Me.PlayerList.ReadOnly = True
        Me.PlayerList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.PlayerList.Size = New System.Drawing.Size(176, 201)
        Me.PlayerList.TabIndex = 2
        '
        'ChatBox
        '
        Me.ChatBox.Location = New System.Drawing.Point(194, 451)
        Me.ChatBox.Multiline = True
        Me.ChatBox.Name = "ChatBox"
        Me.ChatBox.ReadOnly = True
        Me.ChatBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ChatBox.Size = New System.Drawing.Size(378, 173)
        Me.ChatBox.TabIndex = 3
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.GameToolStripMenuItem, Me.ViewToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(584, 24)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConnectToolStripMenuItem, Me.DisconnectToolStripMenuItem, Me.ToolStripMenuItem2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'ConnectToolStripMenuItem
        '
        Me.ConnectToolStripMenuItem.Name = "ConnectToolStripMenuItem"
        Me.ConnectToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ConnectToolStripMenuItem.Text = "Connect"
        '
        'DisconnectToolStripMenuItem
        '
        Me.DisconnectToolStripMenuItem.Enabled = False
        Me.DisconnectToolStripMenuItem.Name = "DisconnectToolStripMenuItem"
        Me.DisconnectToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DisconnectToolStripMenuItem.Text = "Disconnect"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(149, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'GameToolStripMenuItem
        '
        Me.GameToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddPieceToolStripMenuItem, Me.LoadBoardToolStripMenuItem})
        Me.GameToolStripMenuItem.Name = "GameToolStripMenuItem"
        Me.GameToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.GameToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.GameToolStripMenuItem.Text = "&Game"
        '
        'LoadBoardToolStripMenuItem
        '
        Me.LoadBoardToolStripMenuItem.Enabled = False
        Me.LoadBoardToolStripMenuItem.Name = "LoadBoardToolStripMenuItem"
        Me.LoadBoardToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.LoadBoardToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.LoadBoardToolStripMenuItem.Text = "Load &Board"
        '
        'AddPieceToolStripMenuItem
        '
        Me.AddPieceToolStripMenuItem.Enabled = False
        Me.AddPieceToolStripMenuItem.Name = "AddPieceToolStripMenuItem"
        Me.AddPieceToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.AddPieceToolStripMenuItem.Size = New System.Drawing.Size(175, 22)
        Me.AddPieceToolStripMenuItem.Text = "Add &Piece"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'PlayArea
        '
        Me.PlayArea.Controls.Add(Me.BoardImage)
        Me.PlayArea.Location = New System.Drawing.Point(12, 27)
        Me.PlayArea.Name = "PlayArea"
        Me.PlayArea.Size = New System.Drawing.Size(412, 418)
        Me.PlayArea.TabIndex = 6
        Me.PlayArea.TabStop = False
        '
        'BoardImage
        '
        Me.BoardImage.Location = New System.Drawing.Point(6, 9)
        Me.BoardImage.Name = "BoardImage"
        Me.BoardImage.Size = New System.Drawing.Size(400, 400)
        Me.BoardImage.TabIndex = 6
        Me.BoardImage.TabStop = False
        '
        'ImageDialog
        '
        Me.ImageDialog.FileName = "OpenFileDialog1"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowActionTextToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'ShowActionTextToolStripMenuItem
        '
        Me.ShowActionTextToolStripMenuItem.CheckOnClick = True
        Me.ShowActionTextToolStripMenuItem.Name = "ShowActionTextToolStripMenuItem"
        Me.ShowActionTextToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.ShowActionTextToolStripMenuItem.Text = "Show Action Text"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 664)
        Me.Controls.Add(Me.PlayArea)
        Me.Controls.Add(Me.ChatBox)
        Me.Controls.Add(Me.PlayerList)
        Me.Controls.Add(Me.ChatSendButton)
        Me.Controls.Add(Me.ChatInputBox)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.Text = "TTGMaster"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.PlayArea.ResumeLayout(False)
        CType(Me.BoardImage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ChatInputBox As System.Windows.Forms.TextBox
    Friend WithEvents ChatSendButton As System.Windows.Forms.Button
    Friend WithEvents PlayerList As System.Windows.Forms.TextBox
    Friend WithEvents ChatBox As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisconnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PlayArea As System.Windows.Forms.GroupBox
    Friend WithEvents GameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadBoardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BoardImage As System.Windows.Forms.PictureBox
    Friend WithEvents ImageDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AddPieceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowActionTextToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
