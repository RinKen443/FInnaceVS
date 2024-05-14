<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class loadinglogin
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(loadinglogin))
        Label82 = New Label()
        Panel1 = New Panel()
        Label1 = New Label()
        Panel2 = New Panel()
        Panel3 = New Panel()
        Timer1 = New Timer(components)
        SuspendLayout()
        ' 
        ' Label82
        ' 
        Label82.AutoSize = True
        Label82.BorderStyle = BorderStyle.FixedSingle
        Label82.Font = New Font("Segoe UI", 65F, FontStyle.Bold)
        Label82.ForeColor = Color.Black
        Label82.Location = New Point(127, 74)
        Label82.Name = "Label82"
        Label82.Size = New Size(96, 118)
        Label82.TabIndex = 6
        Label82.Text = "F"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Black
        Panel1.Location = New Point(-10, 328)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(405, 95)
        Panel1.TabIndex = 7
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        Label1.Location = New Point(147, 205)
        Label1.Name = "Label1"
        Label1.Size = New Size(62, 20)
        Label1.TabIndex = 8
        Label1.Text = "Finance"
        ' 
        ' Panel2
        ' 
        Panel2.Location = New Point(72, 244)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(211, 24)
        Panel2.TabIndex = 9
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.Black
        Panel3.Location = New Point(72, 244)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(28, 24)
        Panel3.TabIndex = 10
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 15
        ' 
        ' loadinglogin
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(359, 395)
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Label1)
        Controls.Add(Panel1)
        Controls.Add(Label82)
        FormBorderStyle = FormBorderStyle.None
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "loadinglogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "loadinglogin"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label82 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Timer1 As Timer
End Class
