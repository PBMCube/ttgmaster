<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.JoinGroup = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.IPAddressBox = New System.Windows.Forms.TextBox
        Me.HostRadio = New System.Windows.Forms.RadioButton
        Me.JoinRadio = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.PortNumber = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GamePassword = New System.Windows.Forms.TextBox
        Me.UserName = New System.Windows.Forms.TextBox
        Me.HostGroup = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GameNameBox = New System.Windows.Forms.TextBox
        Me.ConnectButton = New System.Windows.Forms.Button
        Me.CouldNotConnect = New System.Windows.Forms.Label
        Me.JoinGroup.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PortNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.HostGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'JoinGroup
        '
        Me.JoinGroup.Controls.Add(Me.Label3)
        Me.JoinGroup.Controls.Add(Me.IPAddressBox)
        Me.JoinGroup.Enabled = False
        Me.JoinGroup.Location = New System.Drawing.Point(179, 35)
        Me.JoinGroup.Name = "JoinGroup"
        Me.JoinGroup.Size = New System.Drawing.Size(143, 71)
        Me.JoinGroup.TabIndex = 0
        Me.JoinGroup.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(17, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "IP"
        '
        'IPAddressBox
        '
        Me.IPAddressBox.Location = New System.Drawing.Point(6, 38)
        Me.IPAddressBox.Name = "IPAddressBox"
        Me.IPAddressBox.Size = New System.Drawing.Size(131, 20)
        Me.IPAddressBox.TabIndex = 6
        '
        'HostRadio
        '
        Me.HostRadio.AutoSize = True
        Me.HostRadio.Location = New System.Drawing.Point(12, 12)
        Me.HostRadio.Name = "HostRadio"
        Me.HostRadio.Size = New System.Drawing.Size(78, 17)
        Me.HostRadio.TabIndex = 1
        Me.HostRadio.TabStop = True
        Me.HostRadio.Text = "Host Game"
        Me.HostRadio.UseVisualStyleBackColor = True
        '
        'JoinRadio
        '
        Me.JoinRadio.AutoSize = True
        Me.JoinRadio.Location = New System.Drawing.Point(181, 12)
        Me.JoinRadio.Name = "JoinRadio"
        Me.JoinRadio.Size = New System.Drawing.Size(108, 17)
        Me.JoinRadio.TabIndex = 2
        Me.JoinRadio.TabStop = True
        Me.JoinRadio.Text = "Connect to Game"
        Me.JoinRadio.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(81, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Port"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.PortNumber)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.GamePassword)
        Me.GroupBox2.Controls.Add(Me.UserName)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 112)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(310, 102)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'PortNumber
        '
        Me.PortNumber.Location = New System.Drawing.Point(113, 46)
        Me.PortNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.PortNumber.Name = "PortNumber"
        Me.PortNumber.Size = New System.Drawing.Size(91, 20)
        Me.PortNumber.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Password (Optional)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(47, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "User Name"
        '
        'GamePassword
        '
        Me.GamePassword.Location = New System.Drawing.Point(113, 71)
        Me.GamePassword.Name = "GamePassword"
        Me.GamePassword.Size = New System.Drawing.Size(191, 20)
        Me.GamePassword.TabIndex = 6
        '
        'UserName
        '
        Me.UserName.Location = New System.Drawing.Point(113, 19)
        Me.UserName.Name = "UserName"
        Me.UserName.Size = New System.Drawing.Size(191, 20)
        Me.UserName.TabIndex = 4
        '
        'HostGroup
        '
        Me.HostGroup.Controls.Add(Me.Label5)
        Me.HostGroup.Controls.Add(Me.GameNameBox)
        Me.HostGroup.Enabled = False
        Me.HostGroup.Location = New System.Drawing.Point(12, 35)
        Me.HostGroup.Name = "HostGroup"
        Me.HostGroup.Size = New System.Drawing.Size(143, 71)
        Me.HostGroup.TabIndex = 8
        Me.HostGroup.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Game Name"
        '
        'GameNameBox
        '
        Me.GameNameBox.Location = New System.Drawing.Point(6, 38)
        Me.GameNameBox.Name = "GameNameBox"
        Me.GameNameBox.Size = New System.Drawing.Size(131, 20)
        Me.GameNameBox.TabIndex = 6
        '
        'ConnectButton
        '
        Me.ConnectButton.Enabled = False
        Me.ConnectButton.Location = New System.Drawing.Point(247, 220)
        Me.ConnectButton.Name = "ConnectButton"
        Me.ConnectButton.Size = New System.Drawing.Size(75, 32)
        Me.ConnectButton.TabIndex = 9
        Me.ConnectButton.Text = "Connect"
        Me.ConnectButton.UseVisualStyleBackColor = True
        '
        'CouldNotConnect
        '
        Me.CouldNotConnect.AutoSize = True
        Me.CouldNotConnect.ForeColor = System.Drawing.Color.Red
        Me.CouldNotConnect.Location = New System.Drawing.Point(138, 230)
        Me.CouldNotConnect.Name = "CouldNotConnect"
        Me.CouldNotConnect.Size = New System.Drawing.Size(103, 13)
        Me.CouldNotConnect.TabIndex = 10
        Me.CouldNotConnect.Text = "Could not connect..."
        Me.CouldNotConnect.Visible = False
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 264)
        Me.Controls.Add(Me.CouldNotConnect)
        Me.Controls.Add(Me.ConnectButton)
        Me.Controls.Add(Me.HostGroup)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.JoinRadio)
        Me.Controls.Add(Me.HostRadio)
        Me.Controls.Add(Me.JoinGroup)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form2"
        Me.ShowInTaskbar = False
        Me.Text = "Connnect"
        Me.TopMost = True
        Me.JoinGroup.ResumeLayout(False)
        Me.JoinGroup.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PortNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.HostGroup.ResumeLayout(False)
        Me.HostGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents JoinGroup As System.Windows.Forms.GroupBox
    Friend WithEvents HostRadio As System.Windows.Forms.RadioButton
    Friend WithEvents JoinRadio As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents IPAddressBox As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GamePassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UserName As System.Windows.Forms.TextBox
    Friend WithEvents HostGroup As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GameNameBox As System.Windows.Forms.TextBox
    Friend WithEvents ConnectButton As System.Windows.Forms.Button
    Friend WithEvents PortNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents CouldNotConnect As System.Windows.Forms.Label
End Class
