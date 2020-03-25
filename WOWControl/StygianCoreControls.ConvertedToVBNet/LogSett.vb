' 
' Type: StygianCoreControls.LogSett
' Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
' Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

Imports StygianCoreControls.Properties
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class LogSett
	Inherits Form
	Public Shared LogFolder As String
	Public Shared IntervalTime As Integer
	Public Shared AutoSaveFile As String
	Private components As IContainer
	Private label1 As Label
	Private LogPath As TextBox
	Private btnbrowse As PictureBox
	Private label2 As Label
	Private SaveInterval As NumericUpDown
	Private label3 As Label
	Private fbd As FolderBrowserDialog

	Public Sub New()
		Me.InitializeComponent()
	End Sub

	Private Sub LogSett_Load(sender As Object, e As EventArgs)
		If String.IsNullOrEmpty(StygianCoreControls.Properties.Settings.[Default].logpath) Then
			Me.LogPath.Text = Application.StartupPath
			MainForm.AutoSaveFolder = Application.StartupPath
		Else
			Me.LogPath.Text = StygianCoreControls.Properties.Settings.[Default].logpath
		End If
		Me.SaveInterval.Value = CType(StygianCoreControls.Properties.Settings.[Default].loginterval, [Decimal])
	End Sub

	Private Sub btnbrowse_Click(sender As Object, e As EventArgs)
		Me.fbd.ShowNewFolderButton = True
		Me.fbd.RootFolder = Environment.SpecialFolder.MyComputer
		Me.fbd.SelectedPath = "."
		Dim num As Integer = CInt(Me.fbd.ShowDialog())
		Me.LogPath.Text = Me.fbd.SelectedPath
	End Sub

	Private Sub LogSett_FormClosing(sender As Object, e As FormClosingEventArgs)
		MainForm.AutoSaveFolder = Me.LogPath.Text
		MainForm.AutoSaveFileName = DateTime.Now.ToString("yyyy-MM-dd") & ".txt"
		MainForm.AutoSaveInterval = Convert.ToInt32(Me.SaveInterval.Value)
		LogSett.AutoSaveFile = Path.Combine(MainForm.AutoSaveFolder, MainForm.AutoSaveFileName)
		StygianCoreControls.Properties.Settings.[Default].logpath = MainForm.AutoSaveFolder
		StygianCoreControls.Properties.Settings.[Default].loginterval = MainForm.AutoSaveInterval
		StygianCoreControls.Properties.Settings.[Default].Save()
		StygianCoreControls.Properties.Settings.[Default].Reload()
		If File.Exists(LogSett.AutoSaveFile) Then
			Return
		End If
		File.WriteAllText(LogSett.AutoSaveFile, ":: StygianCore Event Log :: Started: " & DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") & " :: " & Environment.NewLine & ":: Interval: " & Convert.ToString(DirectCast(Convert.ToInt32(Me.SaveInterval.Value), Object)) & " :: stygianthebest.github.io ::  " & vbLf & "--------------------------------------------------------------------------------------------" & Environment.NewLine)
	End Sub

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso Me.components IsNot Nothing Then
			Me.components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub InitializeComponent()
		Me.label1 = New Label()
		Me.LogPath = New TextBox()
		Me.btnbrowse = New PictureBox()
		Me.label2 = New Label()
		Me.SaveInterval = New NumericUpDown()
		Me.label3 = New Label()
		Me.fbd = New FolderBrowserDialog()
		DirectCast(Me.btnbrowse, ISupportInitialize).BeginInit()
		Me.SaveInterval.BeginInit()
		Me.SuspendLayout()
		Me.label1.AutoSize = True
		Me.label1.Location = New Point(8, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New Size(41, 17)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Path:"
		Me.LogPath.Font = New Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Me.LogPath.Location = New Point(11, 29)
		Me.LogPath.Name = "LogPath"
		Me.LogPath.Size = New Size(133, 24)
		Me.LogPath.TabIndex = 1
		Me.btnbrowse.Cursor = Cursors.Hand
		Me.btnbrowse.Image = DirectCast(Resources.folder_search, Image)
		Me.btnbrowse.Location = New Point(149, 34)
		Me.btnbrowse.Name = "btnbrowse"
		Me.btnbrowse.Size = New Size(16, 16)
		Me.btnbrowse.TabIndex = 2
		Me.btnbrowse.TabStop = False
		AddHandler Me.btnbrowse.Click, New EventHandler(AddressOf Me.btnbrowse_Click)
		Me.label2.AutoSize = True
		Me.label2.Location = New Point(168, 9)
		Me.label2.Name = "label2"
		Me.label2.Size = New Size(58, 17)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Interval:"
		Me.SaveInterval.Font = New Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
		Me.SaveInterval.Increment = New [Decimal](New Integer(3) {1000, 0, 0, 0})
		Me.SaveInterval.Location = New Point(171, 29)
		Me.SaveInterval.Maximum = New [Decimal](New Integer(3) {60000, 0, 0, 0})
		Me.SaveInterval.Minimum = New [Decimal](New Integer(3) {5000, 0, 0, 0})
		Me.SaveInterval.Name = "SaveInterval"
		Me.SaveInterval.Size = New Size(87, 24)
		Me.SaveInterval.TabIndex = 4
		Me.SaveInterval.Value = New [Decimal](New Integer(3) {5000, 0, 0, 0})
		Me.label3.AutoSize = True
		Me.label3.Location = New Point(261, 33)
		Me.label3.Name = "label3"
		Me.label3.Size = New Size(26, 17)
		Me.label3.TabIndex = 5
		Me.label3.Text = "ms"
		Me.fbd.RootFolder = Environment.SpecialFolder.Personal
		Me.AutoScaleDimensions = New SizeF(8F, 17F)
		Me.AutoScaleMode = AutoScaleMode.Font
		Me.ClientSize = New Size(297, 58)
		Me.Controls.Add(DirectCast(Me.label3, Control))
		Me.Controls.Add(DirectCast(Me.SaveInterval, Control))
		Me.Controls.Add(DirectCast(Me.label2, Control))
		Me.Controls.Add(DirectCast(Me.btnbrowse, Control))
		Me.Controls.Add(DirectCast(Me.LogPath, Control))
		Me.Controls.Add(DirectCast(Me.label1, Control))
		Me.Font = New Font("Microsoft Sans Serif", 10.25F)
		Me.FormBorderStyle = FormBorderStyle.FixedToolWindow
		Me.Margin = New Padding(4)
		Me.Name = "LogSett"
		Me.StartPosition = FormStartPosition.CenterParent
		Me.Text = "AutoSave Settings"
		AddHandler Me.FormClosing, New FormClosingEventHandler(AddressOf Me.LogSett_FormClosing)
		AddHandler Me.Load, New EventHandler(AddressOf Me.LogSett_Load)
		DirectCast(Me.btnbrowse, ISupportInitialize).EndInit()
		Me.SaveInterval.EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
End Class
