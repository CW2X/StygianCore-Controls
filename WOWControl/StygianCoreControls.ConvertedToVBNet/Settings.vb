' 
' Type: StygianCoreControls.Settings
' Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
' Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

Imports StygianCoreControls.Properties
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class Settings
	Inherits Form
	Public block As Block
	Private components As IContainer
	Private label1 As Label
	Private path As TextBox
	Private btnbrowse As PictureBox
	Private procname As TextBox
	Private label2 As Label
	Private ofd As OpenFileDialog

	Public Sub New()
		Me.InitializeComponent()
	End Sub

	Public Sub Form2_Load(sender As Object, e As EventArgs)
		Me.Text = Me.block.name & " Settings"
		Me.procname.Text = Me.block.procname
        Me.path.Text = Me.block.paths
    End Sub

	Public Sub SetBlock(ByRef b As Block)
		Me.block = b
	End Sub

	Private Sub Settings_FormClosing(sender As Object, e As FormClosingEventArgs)
        Me.block.paths = Me.path.Text
        Me.block.procname = Me.procname.Text
        If File.Exists(Me.block.paths) Then
            Me.block.write("Path Not Specified!", False)
        End If
        If Not (Me.block.procname <> "") Then
			Return
		End If
		Me.block.write("Process Not Specified!", False)
	End Sub

	Private Sub btnbrowse_Click(sender As Object, e As EventArgs)
		Dim num As Integer = CInt(Me.ofd.ShowDialog())
		Me.path.Text = Me.ofd.FileName
	End Sub

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso Me.components IsNot Nothing Then
			Me.components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub InitializeComponent()
		Me.label1 = New Label()
		Me.path = New TextBox()
		Me.procname = New TextBox()
		Me.label2 = New Label()
		Me.ofd = New OpenFileDialog()
		Me.btnbrowse = New PictureBox()
		DirectCast(Me.btnbrowse, ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		Me.label1.AutoSize = True
		Me.label1.Location = New Point(8, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New Size(41, 17)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Path:"
		Me.path.Location = New Point(11, 29)
		Me.path.Name = "path"
		Me.path.Size = New Size(133, 23)
		Me.path.TabIndex = 1
		Me.procname.Location = New Point(11, 75)
		Me.procname.Name = "procname"
		Me.procname.Size = New Size(154, 23)
		Me.procname.TabIndex = 4
		Me.label2.AutoSize = True
		Me.label2.Location = New Point(8, 55)
		Me.label2.Name = "label2"
		Me.label2.Size = New Size(104, 17)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Process Name:"
		Me.ofd.Filter = "All files|*.*"
		Me.btnbrowse.Cursor = Cursors.Hand
        '		Me.btnbrowse.Image = DirectCast(Resources.folder_search, Image)
        Me.btnbrowse.Location = New Point(149, 34)
		Me.btnbrowse.Name = "btnbrowse"
		Me.btnbrowse.Size = New Size(16, 16)
		Me.btnbrowse.TabIndex = 2
		Me.btnbrowse.TabStop = False
		AddHandler Me.btnbrowse.Click, New EventHandler(AddressOf Me.btnbrowse_Click)
		Me.AutoScaleDimensions = New SizeF(8F, 17F)
		Me.AutoScaleMode = AutoScaleMode.Font
		Me.ClientSize = New Size(175, 107)
		Me.Controls.Add(DirectCast(Me.procname, Control))
		Me.Controls.Add(DirectCast(Me.label2, Control))
		Me.Controls.Add(DirectCast(Me.btnbrowse, Control))
		Me.Controls.Add(DirectCast(Me.path, Control))
		Me.Controls.Add(DirectCast(Me.label1, Control))
		Me.Font = New Font("Microsoft Sans Serif", 10.25F)
		Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
		Me.Margin = New Padding(4)
		Me.Name = "Settings"
		Me.StartPosition = FormStartPosition.CenterParent
		Me.Text = "Settings"
		AddHandler Me.FormClosing, New FormClosingEventHandler(AddressOf Me.Settings_FormClosing)
		AddHandler Me.Load, New EventHandler(AddressOf Me.Form2_Load)
		DirectCast(Me.btnbrowse, ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
End Class
