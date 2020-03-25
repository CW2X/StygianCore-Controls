' 
' Type: StygianCoreControls.Main
' Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
' Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms

Public Class MainForm
	Inherits Form
	Public text As New StringBuilder()
	Public logw As Integer = 730
	Public nonw As Integer = 418
	Public ext As Boolean
	Public Shared LastLogEvent As String
	Public Shared LogChecked As Boolean
	Private contextMenu1 As ContextMenu
	Public Shared world As Block
	Public Shared mysql As Block
	Public Shared logon As Block
	Public Shared apache As Block
	Private components As IContainer
	Public wpanel As Panel
	Public wbtn As PictureBox
	Public wflag As PictureBox
	Public groupBox1 As GroupBox
	Public groupBox2 As GroupBox
	Public panel1 As Panel
	Public lflag As PictureBox
	Public lbtn As PictureBox
	Public groupBox3 As GroupBox
	Public panel2 As Panel
	Public mflag As PictureBox
	Public mbtn As PictureBox
	Public groupBox4 As GroupBox
	Public panel3 As Panel
	Public aflag As PictureBox
	Public abtn As PictureBox
	Public wtime As Label
	Public ltime As Label
	Public atime As Label
	Public wckb As CheckBox
	Public lckb As CheckBox
	Public mckb As CheckBox
	Public ackb As CheckBox
	Public mtime As Label
	Private btnlog As PictureBox
	Private grplog As GroupBox
	Private tt As ToolTip
	Public log As RichTextBox
	Private timer As Timer
	Private pictureBox1 As PictureBox
	Private pb_autosave As PictureBox
	Private pb_hideproc As PictureBox
	Private pictureBox2 As PictureBox
	Private pictureBox6 As PictureBox
	Private pictureBox5 As PictureBox
	Private pictureBox4 As PictureBox
	Private pictureBox3 As PictureBox
	Private pictureBox7 As PictureBox
	Public ckbhide As CheckBox
	Public ckbsave As CheckBox
	Public btnsettings As PictureBox
	Private btnsave As PictureBox
	Private label1 As Label
	Private ctrlBtn As PictureBox
	Private pictureBox8 As PictureBox
	Private notifyIcon1 As NotifyIcon
	Private contextMenuStrip1 As ContextMenuStrip
	Private saveLogToolStripMenuItem As ToolStripMenuItem
	Private openToolStripMenuItem As ToolStripMenuItem
	Private sqlpic As PictureBox

	Public Sub New()
		Me.InitializeComponent()
	End Sub

	Public Shared Property AutoSaveInterval() As Integer
		Get
			Return m_AutoSaveInterval
		End Get
		Set
			m_AutoSaveInterval = Value
		End Set
	End Property
	Private Shared m_AutoSaveInterval As Integer

	Public Shared Property AutoSaveFolder() As String
		Get
			Return m_AutoSaveFolder
		End Get
		Set
			m_AutoSaveFolder = Value
		End Set
	End Property
	Private Shared m_AutoSaveFolder As String

	Public Shared Property AutoSaveFileName() As String
		Get
			Return m_AutoSaveFileName
		End Get
		Set
			m_AutoSaveFileName = Value
		End Set
	End Property
	Private Shared m_AutoSaveFileName As String

	Public Shared Property EventLogFile() As String
		Get
			Return m_EventLogFile
		End Get
		Set
			m_EventLogFile = Value
		End Set
	End Property
	Private Shared m_EventLogFile As String

	Public Shared Property AutoSaveFile() As StreamWriter
		Get
			Return m_AutoSaveFile
		End Get
		Set
			m_AutoSaveFile = Value
		End Set
	End Property
	Private Shared m_AutoSaveFile As StreamWriter

	Public Shadows Event MouseMove As MouseEventHandler

	Private Sub Main_Load(sender As Object, e As EventArgs)
		AddHandler Me.notifyIcon1.DoubleClick, New EventHandler(AddressOf Me.notifyIcon1_DoubleClick)
		AddHandler Me.notifyIcon1.Click, New EventHandler(AddressOf Me.notifyIcon1_Click)
		AddHandler Me.Resize, New EventHandler(AddressOf Me.Form1_Resize)
		Me.notifyIcon1.BalloonTipTitle = "StygianCore Controls"
		Me.notifyIcon1.BalloonTipText = "stygianthebest.github.io"
		Me.notifyIcon1.Text = "StygianCore Controls"
		Me.notifyIcon1.ContextMenu = Me.contextMenu1
		Me.notifyIcon1.Visible = False
		MainForm.AutoSaveInterval = StygianCoreControls.Properties.Settings.[Default].loginterval
		If MainForm.AutoSaveInterval >= 5000 AndAlso MainForm.AutoSaveInterval <= 60000 Then
			Me.timer.Interval = StygianCoreControls.Properties.Settings.[Default].loginterval
			MainForm.AutoSaveInterval = Me.timer.Interval
			Me.label1.Text = MainForm.AutoSaveInterval.ToString() & " ms"
			Me.timer.Start()
		Else
			StygianCoreControls.Properties.Settings.[Default].loginterval = 5000
			Me.timer.Interval = 5000
			MainForm.AutoSaveInterval = Me.timer.Interval
			Me.label1.Text = MainForm.AutoSaveInterval.ToString() & " ms"
			Me.timer.Start()
		End If
		If StygianCoreControls.Properties.Settings.[Default].autosave Then
			Me.ckbsave.Checked = StygianCoreControls.Properties.Settings.[Default].autosave
			Me.pb_autosave.Visible = True
		End If
		If StygianCoreControls.Properties.Settings.[Default].hide Then
			Me.ckbhide.Checked = StygianCoreControls.Properties.Settings.[Default].hide
			Me.pb_hideproc.Visible = True
		End If
		MainForm.AutoSaveFolder = If(Not String.IsNullOrEmpty(StygianCoreControls.Properties.Settings.[Default].logpath), StygianCoreControls.Properties.Settings.[Default].logpath, Application.StartupPath)
		MainForm.AutoSaveFileName = DateTime.Now.ToString("yyyy-MM-dd") & ".txt"
		MainForm.EventLogFile = Path.Combine(MainForm.AutoSaveFolder, MainForm.AutoSaveFileName)
		If File.Exists(MainForm.EventLogFile) AndAlso Not MainForm.LogChecked Then
			Me.text.AppendLine(File.ReadAllText(MainForm.EventLogFile))
			Me.text.AppendLine(":: Logging restarted on " & DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") & " ::")
			Me.log.Text = Me.text.ToString()
			MainForm.LogChecked = True
		Else
			MainForm.LogChecked = True
			Me.text.AppendLine(":: StygianCore Event Log :: Started: " & DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") & " :: " & Environment.NewLine & ":: Interval: " & Convert.ToString(DirectCast(MainForm.AutoSaveInterval, Object)) & Environment.NewLine & "---------------------------------------------------------------------------------------" & Environment.NewLine)
			Me.log.Text = Me.text.ToString()
		End If
		MainForm.world = New Block(StygianCoreControls.Properties.Settings.[Default].wpath, StygianCoreControls.Properties.Settings.[Default].wproc, Me.wbtn, Me.wflag, Me.wckb, Me.log, _
			"World", Me.wtime)
		MainForm.mysql = New Block(StygianCoreControls.Properties.Settings.[Default].mpath, StygianCoreControls.Properties.Settings.[Default].mproc, Me.mbtn, Me.mflag, Me.mckb, Me.log, _
			"Database", Me.mtime)
		MainForm.logon = New Block(StygianCoreControls.Properties.Settings.[Default].lpath, StygianCoreControls.Properties.Settings.[Default].lproc, Me.lbtn, Me.lflag, Me.lckb, Me.log, _
			"Auth", Me.ltime)
		MainForm.apache = New Block(StygianCoreControls.Properties.Settings.[Default].apath, StygianCoreControls.Properties.Settings.[Default].aproc, Me.abtn, Me.aflag, Me.ackb, Me.log, _
			"Web", Me.atime)
	End Sub

	Public Sub CheckButtons()
		If Not MainForm.world.running Then
			Me.wbtn.Cursor = Cursors.[Default]
		End If
		If Not MainForm.logon.running Then
			Me.lbtn.Cursor = Cursors.[Default]
		End If
		If Not MainForm.mysql.running Then
			Me.mbtn.Cursor = Cursors.[Default]
		End If
		If MainForm.apache.running Then
			Return
		End If
		Me.abtn.Cursor = Cursors.[Default]
	End Sub

	Private Sub btnlog_Click(sender As Object, e As EventArgs)
		If Me.ext Then
			Me.ext = False
			Me.Width = Me.nonw
			Me.grplog.Visible = False
		Else
			Me.ext = True
			Me.Width = Me.logw
			Me.grplog.Visible = True
		End If
	End Sub

	Private Sub wflag_Click(sender As Object, e As EventArgs)
		Dim settings As New Settings()
		settings.SetBlock(MainForm.world)
		Dim num As Integer = CInt(settings.ShowDialog())
	End Sub

	Private Sub lflag_Click(sender As Object, e As EventArgs)
		Dim settings As New Settings()
		settings.SetBlock(MainForm.logon)
		Dim num As Integer = CInt(settings.ShowDialog())
	End Sub

	Private Sub mflag_Click(sender As Object, e As EventArgs)
		Dim settings As New Settings()
		settings.SetBlock(MainForm.mysql)
		Dim num As Integer = CInt(settings.ShowDialog())
	End Sub

	Private Sub aflag_Click(sender As Object, e As EventArgs)
		Dim settings As New Settings()
		settings.SetBlock(MainForm.apache)
		Dim num As Integer = CInt(settings.ShowDialog())
	End Sub

	Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs)
        StygianCoreControls.Properties.Settings.[Default].wpath = MainForm.world.paths
        StygianCoreControls.Properties.Settings.[Default].wproc = MainForm.world.procname
        StygianCoreControls.Properties.Settings.[Default].lpath = MainForm.logon.paths
        StygianCoreControls.Properties.Settings.[Default].lproc = MainForm.logon.procname
        StygianCoreControls.Properties.Settings.[Default].mpath = MainForm.mysql.paths
        StygianCoreControls.Properties.Settings.[Default].mproc = MainForm.mysql.procname
        StygianCoreControls.Properties.Settings.[Default].apath = MainForm.apache.paths
        StygianCoreControls.Properties.Settings.[Default].aproc = MainForm.apache.procname
		StygianCoreControls.Properties.Settings.[Default].logpath = MainForm.AutoSaveFolder
		StygianCoreControls.Properties.Settings.[Default].loginterval = MainForm.AutoSaveInterval
		StygianCoreControls.Properties.Settings.[Default].autosave = Me.ckbsave.Checked
		StygianCoreControls.Properties.Settings.[Default].hide = Me.ckbhide.Checked
		StygianCoreControls.Properties.Settings.[Default].Save()
		StygianCoreControls.Properties.Settings.[Default].Reload()
		If File.Exists(MainForm.world.runpath) Then
			File.Delete(MainForm.world.runpath)
		End If
		If File.Exists(MainForm.logon.runpath) Then
			File.Delete(MainForm.logon.runpath)
		End If
		If File.Exists(MainForm.mysql.runpath) Then
			File.Delete(MainForm.mysql.runpath)
		End If
		If Not File.Exists(MainForm.apache.runpath) Then
			Return
		End If
		File.Delete(MainForm.apache.runpath)
	End Sub

	Private Sub log_TextChanged(sender As Object, e As EventArgs)
		Me.log.SelectionStart = Me.log.Text.Length
		Me.log.ScrollToCaret()
	End Sub

	Private Sub btnsettings_Click(sender As Object, e As EventArgs)
		Dim num As Integer = CInt(New LogSett().ShowDialog())
	End Sub

	Private Sub timer_Tick(sender As Object, e As EventArgs)
		Dim now As DateTime
		If Me.timer.Interval <> MainForm.AutoSaveInterval Then
			Me.timer.Interval = MainForm.AutoSaveInterval
			Me.label1.Text = MainForm.AutoSaveInterval.ToString() & " ms"
			Dim log As RichTextBox = Me.log
			Dim richTextBox As RichTextBox = log
			Dim objArray As Object() = New Object(6) {}
			objArray(0) = DirectCast(log.Text, Object)
			objArray(1) = DirectCast("[", Object)
			Dim index As Integer = 2
			now = DateTime.Now
			Dim str1 As String = now.ToString()
			objArray(index) = DirectCast(str1, Object)
			objArray(3) = DirectCast("] Interval Updated to ", Object)
			objArray(4) = DirectCast(MainForm.AutoSaveInterval, Object)
			objArray(5) = DirectCast(" ms", Object)
			objArray(6) = DirectCast(Environment.NewLine, Object)
			Dim str2 As String = String.Concat(objArray)
			richTextBox.Text = str2
		End If
		If Not Me.ckbsave.Checked Then
			Return
		End If
		If String.IsNullOrEmpty(MainForm.AutoSaveFolder) Then
			MainForm.AutoSaveFolder = Application.StartupPath
			Dim log As RichTextBox = Me.log
			Dim richTextBox As RichTextBox = log
			Dim strArray As String() = New String(4) {log.Text, "[", Nothing, Nothing, Nothing}
			Dim index As Integer = 2
			now = DateTime.Now
			Dim str1 As String = now.ToString()
			strArray(index) = str1
			strArray(3) = "] Repaired AutoSave Folder"
			strArray(4) = Environment.NewLine
			Dim str2 As String = String.Concat(strArray)
			richTextBox.Text = str2
		ElseIf String.IsNullOrEmpty(MainForm.AutoSaveFileName) Then
			now = DateTime.Now
			MainForm.AutoSaveFileName = now.ToString("yyyy-MM-dd@MM") & ".txt"
			Dim log As RichTextBox = Me.log
			Dim richTextBox As RichTextBox = log
			Dim strArray As String() = New String(4) {log.Text, "[", Nothing, Nothing, Nothing}
			Dim index As Integer = 2
			now = DateTime.Now
			Dim str1 As String = now.ToString()
			strArray(index) = str1
			strArray(3) = "] Repaired AutoSave File"
			strArray(4) = Environment.NewLine
			Dim str2 As String = String.Concat(strArray)
			richTextBox.Text = str2
		Else
			now = DateTime.Now
			If now.ToString("HH:mm") = "00:00" Then
				now = DateTime.Now
				MainForm.AutoSaveFileName = Path.Combine(MainForm.AutoSaveFolder, now.ToString("yyyy-MM-dd@MM") & ".txt")
				Dim autoSaveFileName As String = MainForm.AutoSaveFileName
				Dim objArray As Object() = New Object(9) {}
				objArray(0) = DirectCast(":: StygianCore Event Log :: Started: ", Object)
				Dim index As Integer = 1
				now = DateTime.Now
				Dim str As String = now.ToString("yyyy-MM-dd@HH.mm.ss")
				objArray(index) = DirectCast(str, Object)
				objArray(2) = DirectCast(" :: ", Object)
				objArray(3) = DirectCast(Environment.NewLine, Object)
				objArray(4) = DirectCast(":: Interval: ", Object)
				objArray(5) = DirectCast(MainForm.AutoSaveInterval, Object)
				objArray(6) = DirectCast(" :: stygianthebest.github.io ::  ", Object)
				objArray(7) = DirectCast(Environment.NewLine, Object)
				objArray(8) = DirectCast("---------------------------------------------------------------------------------------", Object)
				objArray(9) = DirectCast(Environment.NewLine, Object)
				Dim contents As String = String.Concat(objArray)
				File.WriteAllText(autoSaveFileName, contents)
			End If
			Dim path__1 As String = Path.Combine(MainForm.AutoSaveFolder, MainForm.AutoSaveFileName)
			Dim length As Integer = Me.log.Lines.Length
			Me.text.Clear()
			For index As Integer = 0 To length - 1
				Me.text.Append(Me.log.Lines(index) & Environment.NewLine)
			Next
			File.WriteAllText(path__1, Me.text.ToString())
		End If
	End Sub

	Private Sub btnsave_Click(sender As Object, e As EventArgs)
		Tools.SaveLogFile(Me.log)
	End Sub

	Private Sub pictureBox1_Click(sender As Object, e As EventArgs)
		Process.Start("http://bit.ly/app_drinkme")
		Me.pictureBox2.Visible = True
		Dim log As RichTextBox = Me.log
		log.Text = log.Text & "[" & DateTime.Now.ToString() & "] Drank the potion! " & Environment.NewLine
	End Sub

	Private Sub pictureBox2_Click(sender As Object, e As EventArgs)
		Dim log As RichTextBox = Me.log
		log.Text = log.Text & "[" & DateTime.Now.ToString() & "] Visit stygianthebest.github.io " & Environment.NewLine
	End Sub

	Private Sub ckbsave_CheckedChanged(sender As Object, e As EventArgs)
		If Not Me.btnsettings.Visible Then
			Me.btnsettings.Visible = True
			Me.pb_autosave.Visible = True
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] AutoSave Enabled " & Environment.NewLine
		Else
			Me.btnsettings.Visible = False
			Me.pb_autosave.Visible = False
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] AutoSave Disabled " & Environment.NewLine
		End If
	End Sub

	Private Sub ckbhide_CheckedChanged(sender As Object, e As EventArgs)
		If Not Me.ckbhide.Checked Then
			Me.pb_hideproc.Visible = False
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] Hide Processes Disabled " & Environment.NewLine
		Else
			Me.pb_hideproc.Visible = True
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] Hide Processes Enabled " & Environment.NewLine
		End If
	End Sub

	Private Sub ctrlBtn_Click(sender As Object, e As EventArgs)
		Try
			Dim str As String = String.Format(".")
            Dim pr As New Process() With {
                .StartInfo = New ProcessStartInfo With {.WorkingDirectory = str, .FileName = "StygianCoreTools.bat", .CreateNoWindow = False}
            }
            pr.Start()
            Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] Loaded StygianCoreTools " & Environment.NewLine
		Catch ex As Exception
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] StygianCoreTools.bat not found!" & Environment.NewLine
			Dim num As Integer = CInt(MessageBox.Show("StygianCoreTools.bat not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand))
		End Try
	End Sub

	Private Sub Form1_Resize(sender As Object, e As EventArgs)
		If Me.WindowState = FormWindowState.Minimized Then
			Me.Hide()
			Me.notifyIcon1.Visible = True
			Me.notifyIcon1.ShowBalloonTip(0)
		Else
			If Me.WindowState <> FormWindowState.Normal Then
				Return
			End If
			Me.notifyIcon1.Visible = False
		End If
	End Sub

	Private Sub notifyIcon1_DoubleClick(sender As Object, e As EventArgs)
		Me.Show()
		Me.notifyIcon1.Visible = False
		Me.WindowState = FormWindowState.Normal
		Me.Activate()
	End Sub

	Private Sub notifyIcon1_Click(sender As Object, e As EventArgs)
		Dim str1 As String = Me.wtime.Text
		Dim str2 As String = Me.mtime.Text
		Dim str3 As String = Me.ltime.Text
		Dim str4 As String = Me.atime.Text
		If str1 = "00:00:00:00" Then
			str1 = "Offline"
		End If
		If str2 = "00:00:00:00" Then
			str2 = "Offline"
		End If
		If str3 = "00:00:00:00" Then
			str3 = "Offline"
		End If
		If str4 = "00:00:00:00" Then
			str4 = "Offline"
		End If
		Me.notifyIcon1.BalloonTipText = ".: Server Uptime :." & Environment.NewLine & str1 & " - World" & Environment.NewLine & str2 & " - Database" & Environment.NewLine & str3 & " - Auth" & Environment.NewLine & str4 & " - Web" & Environment.NewLine
		Me.notifyIcon1.ShowBalloonTip(0)
	End Sub

	Private Sub saveLogToolStripMenuItem_Click(sender As Object, e As EventArgs)
		Tools.SaveLogFile(Me.log)
	End Sub

	Private Sub openToolStripMenuItem_Click(sender As Object, e As EventArgs)
		Me.Show()
		Me.notifyIcon1.Visible = False
		Me.WindowState = FormWindowState.Normal
		Me.Activate()
	End Sub

	Private Sub notifyIcon1_MouseMove(sender As Object, e As MouseEventArgs)
	End Sub

	Private Sub sqlpic_Click(sender As Object, e As EventArgs)
		Try
            Dim str As String = String.Format(".")
            Dim pr As New Process() With {
                 .StartInfo = New ProcessStartInfo With {.WorkingDirectory = str, .FileName = "start_sql.bat", .CreateNoWindow = False}
            }
            pr.Start()
            Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] Loaded SQL Tool " & Environment.NewLine
		Catch ex As Exception
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] start_sql.bat not found!" & Environment.NewLine
			Dim num As Integer = CInt(MessageBox.Show("start_sql.bat not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand))
		End Try
	End Sub

	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso Me.components IsNot Nothing Then
			Me.components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.wpanel = New System.Windows.Forms.Panel()
        Me.pictureBox6 = New System.Windows.Forms.PictureBox()
        Me.wckb = New System.Windows.Forms.CheckBox()
        Me.wtime = New System.Windows.Forms.Label()
        Me.wflag = New System.Windows.Forms.PictureBox()
        Me.wbtn = New System.Windows.Forms.PictureBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.pictureBox5 = New System.Windows.Forms.PictureBox()
        Me.lckb = New System.Windows.Forms.CheckBox()
        Me.ltime = New System.Windows.Forms.Label()
        Me.lflag = New System.Windows.Forms.PictureBox()
        Me.lbtn = New System.Windows.Forms.PictureBox()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.pictureBox4 = New System.Windows.Forms.PictureBox()
        Me.mckb = New System.Windows.Forms.CheckBox()
        Me.mtime = New System.Windows.Forms.Label()
        Me.mflag = New System.Windows.Forms.PictureBox()
        Me.mbtn = New System.Windows.Forms.PictureBox()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.ackb = New System.Windows.Forms.CheckBox()
        Me.atime = New System.Windows.Forms.Label()
        Me.aflag = New System.Windows.Forms.PictureBox()
        Me.abtn = New System.Windows.Forms.PictureBox()
        Me.log = New System.Windows.Forms.RichTextBox()
        Me.grplog = New System.Windows.Forms.GroupBox()
        Me.pictureBox7 = New System.Windows.Forms.PictureBox()
        Me.btnlog = New System.Windows.Forms.PictureBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pb_autosave = New System.Windows.Forms.PictureBox()
        Me.pb_hideproc = New System.Windows.Forms.PictureBox()
        Me.btnsettings = New System.Windows.Forms.PictureBox()
        Me.btnsave = New System.Windows.Forms.PictureBox()
        Me.ctrlBtn = New System.Windows.Forms.PictureBox()
        Me.pictureBox2 = New System.Windows.Forms.PictureBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.pictureBox8 = New System.Windows.Forms.PictureBox()
        Me.sqlpic = New System.Windows.Forms.PictureBox()
        Me.ckbhide = New System.Windows.Forms.CheckBox()
        Me.ckbsave = New System.Windows.Forms.CheckBox()
        Me.notifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.saveLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.tt = New System.Windows.Forms.ToolTip(Me.components)
        Me.timer = New System.Windows.Forms.Timer(Me.components)
        Me.wpanel.SuspendLayout()
        CType(Me.pictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wflag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wbtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox1.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.panel1.SuspendLayout()
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lflag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lbtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox3.SuspendLayout()
        Me.panel2.SuspendLayout()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mflag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.mbtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox4.SuspendLayout()
        Me.panel3.SuspendLayout()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.aflag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.abtn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grplog.SuspendLayout()
        CType(Me.pictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnlog, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_autosave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_hideproc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsettings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ctrlBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sqlpic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.contextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'wpanel
        '
        Me.wpanel.Controls.Add(Me.pictureBox6)
        Me.wpanel.Controls.Add(Me.wckb)
        Me.wpanel.Controls.Add(Me.wtime)
        Me.wpanel.Controls.Add(Me.wflag)
        Me.wpanel.Controls.Add(Me.wbtn)
        Me.wpanel.ForeColor = System.Drawing.Color.ForestGreen
        Me.wpanel.Location = New System.Drawing.Point(9, 10)
        Me.wpanel.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.wpanel.Name = "wpanel"
        Me.wpanel.Size = New System.Drawing.Size(101, 127)
        Me.wpanel.TabIndex = 0
        '
        'pictureBox6
        '
        Me.pictureBox6.Image = CType(resources.GetObject("pictureBox6.Image"), System.Drawing.Image)
        Me.pictureBox6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pictureBox6.Location = New System.Drawing.Point(12, 6)
        Me.pictureBox6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox6.Name = "pictureBox6"
        Me.pictureBox6.Size = New System.Drawing.Size(80, 16)
        Me.pictureBox6.TabIndex = 8
        Me.pictureBox6.TabStop = False
        '
        'wckb
        '
        Me.wckb.AutoSize = True
        Me.wckb.ForeColor = System.Drawing.Color.Crimson
        Me.wckb.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.wckb.Location = New System.Drawing.Point(12, 103)
        Me.wckb.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.wckb.Name = "wckb"
        Me.wckb.Size = New System.Drawing.Size(85, 19)
        Me.wckb.TabIndex = 4
        Me.wckb.Text = "Restart"
        Me.wckb.UseVisualStyleBackColor = True
        '
        'wtime
        '
        Me.wtime.ForeColor = System.Drawing.Color.Crimson
        Me.wtime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.wtime.Location = New System.Drawing.Point(0, 73)
        Me.wtime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.wtime.Name = "wtime"
        Me.wtime.Size = New System.Drawing.Size(97, 27)
        Me.wtime.TabIndex = 3
        Me.wtime.Text = "00:00:00:00"
        Me.wtime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'wflag
        '
        Me.wflag.Image = CType(resources.GetObject("wflag.Image"), System.Drawing.Image)
        Me.wflag.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.wflag.Location = New System.Drawing.Point(55, 32)
        Me.wflag.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.wflag.Name = "wflag"
        Me.wflag.Size = New System.Drawing.Size(43, 37)
        Me.wflag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.wflag.TabIndex = 1
        Me.wflag.TabStop = False
        Me.tt.SetToolTip(Me.wflag, "Config WorldServer")
        '
        'wbtn
        '
        Me.wbtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.wbtn.Image = CType(resources.GetObject("wbtn.Image"), System.Drawing.Image)
        Me.wbtn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.wbtn.Location = New System.Drawing.Point(4, 32)
        Me.wbtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.wbtn.Name = "wbtn"
        Me.wbtn.Size = New System.Drawing.Size(43, 37)
        Me.wbtn.TabIndex = 0
        Me.wbtn.TabStop = False
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.wpanel)
        Me.groupBox1.Location = New System.Drawing.Point(16, 1)
        Me.groupBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox1.Size = New System.Drawing.Size(119, 144)
        Me.groupBox1.TabIndex = 1
        Me.groupBox1.TabStop = False
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.panel1)
        Me.groupBox2.Location = New System.Drawing.Point(143, 1)
        Me.groupBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox2.Size = New System.Drawing.Size(119, 144)
        Me.groupBox2.TabIndex = 2
        Me.groupBox2.TabStop = False
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.pictureBox5)
        Me.panel1.Controls.Add(Me.lckb)
        Me.panel1.Controls.Add(Me.ltime)
        Me.panel1.Controls.Add(Me.lflag)
        Me.panel1.Controls.Add(Me.lbtn)
        Me.panel1.ForeColor = System.Drawing.Color.DarkOrange
        Me.panel1.Location = New System.Drawing.Point(9, 10)
        Me.panel1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(101, 127)
        Me.panel1.TabIndex = 0
        '
        'pictureBox5
        '
        Me.pictureBox5.Image = CType(resources.GetObject("pictureBox5.Image"), System.Drawing.Image)
        Me.pictureBox5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pictureBox5.Location = New System.Drawing.Point(17, 7)
        Me.pictureBox5.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox5.Name = "pictureBox5"
        Me.pictureBox5.Size = New System.Drawing.Size(67, 14)
        Me.pictureBox5.TabIndex = 7
        Me.pictureBox5.TabStop = False
        '
        'lckb
        '
        Me.lckb.AutoSize = True
        Me.lckb.ForeColor = System.Drawing.Color.DarkGreen
        Me.lckb.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lckb.Location = New System.Drawing.Point(12, 103)
        Me.lckb.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lckb.Name = "lckb"
        Me.lckb.Size = New System.Drawing.Size(85, 19)
        Me.lckb.TabIndex = 5
        Me.lckb.Text = "Restart"
        Me.lckb.UseVisualStyleBackColor = True
        '
        'ltime
        '
        Me.ltime.ForeColor = System.Drawing.Color.DarkGreen
        Me.ltime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ltime.Location = New System.Drawing.Point(0, 73)
        Me.ltime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.ltime.Name = "ltime"
        Me.ltime.Size = New System.Drawing.Size(97, 27)
        Me.ltime.TabIndex = 4
        Me.ltime.Text = "00:00:00:00"
        Me.ltime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lflag
        '
        Me.lflag.Image = CType(resources.GetObject("lflag.Image"), System.Drawing.Image)
        Me.lflag.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lflag.Location = New System.Drawing.Point(55, 32)
        Me.lflag.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lflag.Name = "lflag"
        Me.lflag.Size = New System.Drawing.Size(43, 37)
        Me.lflag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.lflag.TabIndex = 1
        Me.lflag.TabStop = False
        Me.tt.SetToolTip(Me.lflag, "Config AuthServer")
        '
        'lbtn
        '
        Me.lbtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbtn.Image = CType(resources.GetObject("lbtn.Image"), System.Drawing.Image)
        Me.lbtn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbtn.Location = New System.Drawing.Point(4, 32)
        Me.lbtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.lbtn.Name = "lbtn"
        Me.lbtn.Size = New System.Drawing.Size(43, 37)
        Me.lbtn.TabIndex = 0
        Me.lbtn.TabStop = False
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.panel2)
        Me.groupBox3.Location = New System.Drawing.Point(269, 1)
        Me.groupBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox3.Size = New System.Drawing.Size(119, 144)
        Me.groupBox3.TabIndex = 3
        Me.groupBox3.TabStop = False
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.pictureBox4)
        Me.panel2.Controls.Add(Me.mckb)
        Me.panel2.Controls.Add(Me.mtime)
        Me.panel2.Controls.Add(Me.mflag)
        Me.panel2.Controls.Add(Me.mbtn)
        Me.panel2.ForeColor = System.Drawing.Color.Teal
        Me.panel2.Location = New System.Drawing.Point(9, 10)
        Me.panel2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(101, 127)
        Me.panel2.TabIndex = 0
        '
        'pictureBox4
        '
        Me.pictureBox4.Image = CType(resources.GetObject("pictureBox4.Image"), System.Drawing.Image)
        Me.pictureBox4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pictureBox4.Location = New System.Drawing.Point(36, 7)
        Me.pictureBox4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox4.Name = "pictureBox4"
        Me.pictureBox4.Size = New System.Drawing.Size(32, 14)
        Me.pictureBox4.TabIndex = 6
        Me.pictureBox4.TabStop = False
        '
        'mckb
        '
        Me.mckb.AutoSize = True
        Me.mckb.ForeColor = System.Drawing.Color.RoyalBlue
        Me.mckb.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.mckb.Location = New System.Drawing.Point(12, 103)
        Me.mckb.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.mckb.Name = "mckb"
        Me.mckb.Size = New System.Drawing.Size(85, 19)
        Me.mckb.TabIndex = 5
        Me.mckb.Text = "Restart"
        Me.mckb.UseVisualStyleBackColor = True
        '
        'mtime
        '
        Me.mtime.ForeColor = System.Drawing.Color.RoyalBlue
        Me.mtime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.mtime.Location = New System.Drawing.Point(0, 73)
        Me.mtime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.mtime.Name = "mtime"
        Me.mtime.Size = New System.Drawing.Size(97, 27)
        Me.mtime.TabIndex = 4
        Me.mtime.Text = "00:00:00:00"
        Me.mtime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'mflag
        '
        Me.mflag.Image = CType(resources.GetObject("mflag.Image"), System.Drawing.Image)
        Me.mflag.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.mflag.Location = New System.Drawing.Point(55, 32)
        Me.mflag.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.mflag.Name = "mflag"
        Me.mflag.Size = New System.Drawing.Size(43, 37)
        Me.mflag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.mflag.TabIndex = 1
        Me.mflag.TabStop = False
        Me.tt.SetToolTip(Me.mflag, "Config Database")
        '
        'mbtn
        '
        Me.mbtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.mbtn.Image = CType(resources.GetObject("mbtn.Image"), System.Drawing.Image)
        Me.mbtn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.mbtn.Location = New System.Drawing.Point(4, 32)
        Me.mbtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.mbtn.Name = "mbtn"
        Me.mbtn.Size = New System.Drawing.Size(43, 37)
        Me.mbtn.TabIndex = 0
        Me.mbtn.TabStop = False
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.panel3)
        Me.groupBox4.Location = New System.Drawing.Point(396, 1)
        Me.groupBox4.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.groupBox4.Size = New System.Drawing.Size(119, 144)
        Me.groupBox4.TabIndex = 4
        Me.groupBox4.TabStop = False
        '
        'panel3
        '
        Me.panel3.Controls.Add(Me.pictureBox3)
        Me.panel3.Controls.Add(Me.ackb)
        Me.panel3.Controls.Add(Me.atime)
        Me.panel3.Controls.Add(Me.aflag)
        Me.panel3.Controls.Add(Me.abtn)
        Me.panel3.ForeColor = System.Drawing.Color.DarkOrchid
        Me.panel3.Location = New System.Drawing.Point(9, 10)
        Me.panel3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.panel3.Name = "panel3"
        Me.panel3.Size = New System.Drawing.Size(101, 127)
        Me.panel3.TabIndex = 0
        '
        'pictureBox3
        '
        Me.pictureBox3.Image = CType(resources.GetObject("pictureBox3.Image"), System.Drawing.Image)
        Me.pictureBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pictureBox3.Location = New System.Drawing.Point(27, 7)
        Me.pictureBox3.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(48, 14)
        Me.pictureBox3.TabIndex = 5
        Me.pictureBox3.TabStop = False
        '
        'ackb
        '
        Me.ackb.AutoSize = True
        Me.ackb.ForeColor = System.Drawing.Color.Black
        Me.ackb.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ackb.Location = New System.Drawing.Point(12, 103)
        Me.ackb.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ackb.Name = "ackb"
        Me.ackb.Size = New System.Drawing.Size(85, 19)
        Me.ackb.TabIndex = 5
        Me.ackb.Text = "Restart"
        Me.ackb.UseVisualStyleBackColor = True
        '
        'atime
        '
        Me.atime.ForeColor = System.Drawing.Color.Black
        Me.atime.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.atime.Location = New System.Drawing.Point(0, 73)
        Me.atime.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.atime.Name = "atime"
        Me.atime.Size = New System.Drawing.Size(97, 27)
        Me.atime.TabIndex = 4
        Me.atime.Text = "00:00:00:00"
        Me.atime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'aflag
        '
        Me.aflag.Image = CType(resources.GetObject("aflag.Image"), System.Drawing.Image)
        Me.aflag.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.aflag.Location = New System.Drawing.Point(55, 32)
        Me.aflag.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.aflag.Name = "aflag"
        Me.aflag.Size = New System.Drawing.Size(43, 37)
        Me.aflag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.aflag.TabIndex = 1
        Me.aflag.TabStop = False
        Me.tt.SetToolTip(Me.aflag, "Config Webserver")
        '
        'abtn
        '
        Me.abtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.abtn.Image = CType(resources.GetObject("abtn.Image"), System.Drawing.Image)
        Me.abtn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.abtn.Location = New System.Drawing.Point(4, 32)
        Me.abtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.abtn.Name = "abtn"
        Me.abtn.Size = New System.Drawing.Size(43, 37)
        Me.abtn.TabIndex = 0
        Me.abtn.TabStop = False
        '
        'log
        '
        Me.log.AccessibleName = ""
        Me.log.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.log.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!)
        Me.log.Location = New System.Drawing.Point(8, 36)
        Me.log.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.log.Name = "log"
        Me.log.ReadOnly = True
        Me.log.Size = New System.Drawing.Size(377, 101)
        Me.log.TabIndex = 7
        Me.log.Text = ""
        '
        'grplog
        '
        Me.grplog.Controls.Add(Me.pictureBox7)
        Me.grplog.Controls.Add(Me.log)
        Me.grplog.Location = New System.Drawing.Point(540, 1)
        Me.grplog.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.grplog.Name = "grplog"
        Me.grplog.Padding = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.grplog.Size = New System.Drawing.Size(395, 144)
        Me.grplog.TabIndex = 9
        Me.grplog.TabStop = False
        Me.grplog.Visible = False
        '
        'pictureBox7
        '
        Me.pictureBox7.Image = CType(resources.GetObject("pictureBox7.Image"), System.Drawing.Image)
        Me.pictureBox7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pictureBox7.Location = New System.Drawing.Point(131, 15)
        Me.pictureBox7.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox7.Name = "pictureBox7"
        Me.pictureBox7.Size = New System.Drawing.Size(133, 17)
        Me.pictureBox7.TabIndex = 9
        Me.pictureBox7.TabStop = False
        '
        'btnlog
        '
        Me.btnlog.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnlog.Image = CType(resources.GetObject("btnlog.Image"), System.Drawing.Image)
        Me.btnlog.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnlog.Location = New System.Drawing.Point(489, 151)
        Me.btnlog.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnlog.Name = "btnlog"
        Me.btnlog.Size = New System.Drawing.Size(21, 18)
        Me.btnlog.TabIndex = 6
        Me.btnlog.TabStop = False
        Me.tt.SetToolTip(Me.btnlog, "Event Log")
        '
        'pictureBox1
        '
        Me.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pictureBox1.Location = New System.Drawing.Point(17, 151)
        Me.pictureBox1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(21, 18)
        Me.pictureBox1.TabIndex = 15
        Me.pictureBox1.TabStop = False
        Me.tt.SetToolTip(Me.pictureBox1, "Drink Me!")
        '
        'pb_autosave
        '
        Me.pb_autosave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_autosave.Image = CType(resources.GetObject("pb_autosave.Image"), System.Drawing.Image)
        Me.pb_autosave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pb_autosave.Location = New System.Drawing.Point(413, 151)
        Me.pb_autosave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pb_autosave.Name = "pb_autosave"
        Me.pb_autosave.Size = New System.Drawing.Size(21, 18)
        Me.pb_autosave.TabIndex = 16
        Me.pb_autosave.TabStop = False
        Me.tt.SetToolTip(Me.pb_autosave, "AutoSaving")
        Me.pb_autosave.Visible = False
        '
        'pb_hideproc
        '
        Me.pb_hideproc.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pb_hideproc.Image = CType(resources.GetObject("pb_hideproc.Image"), System.Drawing.Image)
        Me.pb_hideproc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pb_hideproc.Location = New System.Drawing.Point(388, 151)
        Me.pb_hideproc.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pb_hideproc.Name = "pb_hideproc"
        Me.pb_hideproc.Size = New System.Drawing.Size(21, 18)
        Me.pb_hideproc.TabIndex = 17
        Me.pb_hideproc.TabStop = False
        Me.tt.SetToolTip(Me.pb_hideproc, "Hiding Processes")
        Me.pb_hideproc.Visible = False
        '
        'btnsettings
        '
        Me.btnsettings.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnsettings.Image = CType(resources.GetObject("btnsettings.Image"), System.Drawing.Image)
        Me.btnsettings.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnsettings.Location = New System.Drawing.Point(880, 151)
        Me.btnsettings.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnsettings.Name = "btnsettings"
        Me.btnsettings.Size = New System.Drawing.Size(21, 18)
        Me.btnsettings.TabIndex = 12
        Me.btnsettings.TabStop = False
        Me.tt.SetToolTip(Me.btnsettings, "AutoSave Options")
        Me.btnsettings.Visible = False
        '
        'btnsave
        '
        Me.btnsave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnsave.Image = CType(resources.GetObject("btnsave.Image"), System.Drawing.Image)
        Me.btnsave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnsave.Location = New System.Drawing.Point(911, 151)
        Me.btnsave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btnsave.Name = "btnsave"
        Me.btnsave.Size = New System.Drawing.Size(21, 18)
        Me.btnsave.TabIndex = 19
        Me.btnsave.TabStop = False
        Me.tt.SetToolTip(Me.btnsave, "Save Event Log")
        '
        'ctrlBtn
        '
        Me.ctrlBtn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ctrlBtn.Image = CType(resources.GetObject("ctrlBtn.Image"), System.Drawing.Image)
        Me.ctrlBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ctrlBtn.Location = New System.Drawing.Point(463, 151)
        Me.ctrlBtn.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ctrlBtn.Name = "ctrlBtn"
        Me.ctrlBtn.Size = New System.Drawing.Size(21, 18)
        Me.ctrlBtn.TabIndex = 21
        Me.ctrlBtn.TabStop = False
        Me.tt.SetToolTip(Me.ctrlBtn, "Launch Tools")
        '
        'pictureBox2
        '
        Me.pictureBox2.Image = CType(resources.GetObject("pictureBox2.Image"), System.Drawing.Image)
        Me.pictureBox2.Location = New System.Drawing.Point(48, 152)
        Me.pictureBox2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(324, 18)
        Me.pictureBox2.TabIndex = 18
        Me.pictureBox2.TabStop = False
        Me.tt.SetToolTip(Me.pictureBox2, "StygianCore Controls v2018.12.01")
        Me.pictureBox2.Visible = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.ForeColor = System.Drawing.Color.Green
        Me.label1.Location = New System.Drawing.Point(567, 152)
        Me.label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(47, 15)
        Me.label1.TabIndex = 20
        Me.label1.Text = "60000"
        Me.tt.SetToolTip(Me.label1, "milliseconds")
        '
        'pictureBox8
        '
        Me.pictureBox8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pictureBox8.Image = CType(resources.GetObject("pictureBox8.Image"), System.Drawing.Image)
        Me.pictureBox8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pictureBox8.Location = New System.Drawing.Point(544, 150)
        Me.pictureBox8.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.pictureBox8.Name = "pictureBox8"
        Me.pictureBox8.Size = New System.Drawing.Size(21, 18)
        Me.pictureBox8.TabIndex = 22
        Me.pictureBox8.TabStop = False
        Me.tt.SetToolTip(Me.pictureBox8, "AutoSave Interval")
        '
        'sqlpic
        '
        Me.sqlpic.Cursor = System.Windows.Forms.Cursors.Hand
        Me.sqlpic.Image = CType(resources.GetObject("sqlpic.Image"), System.Drawing.Image)
        Me.sqlpic.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.sqlpic.Location = New System.Drawing.Point(439, 150)
        Me.sqlpic.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.sqlpic.Name = "sqlpic"
        Me.sqlpic.Size = New System.Drawing.Size(21, 18)
        Me.sqlpic.TabIndex = 23
        Me.sqlpic.TabStop = False
        Me.tt.SetToolTip(Me.sqlpic, "Launch HeidiSQL")
        '
        'ckbhide
        '
        Me.ckbhide.AutoSize = True
        Me.ckbhide.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ckbhide.Location = New System.Drawing.Point(637, 151)
        Me.ckbhide.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ckbhide.Name = "ckbhide"
        Me.ckbhide.Size = New System.Drawing.Size(141, 19)
        Me.ckbhide.TabIndex = 13
        Me.ckbhide.Text = "Hide Processes"
        Me.ckbhide.UseVisualStyleBackColor = True
        '
        'ckbsave
        '
        Me.ckbsave.AutoSize = True
        Me.ckbsave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ckbsave.Location = New System.Drawing.Point(775, 151)
        Me.ckbsave.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.ckbsave.Name = "ckbsave"
        Me.ckbsave.Size = New System.Drawing.Size(93, 19)
        Me.ckbsave.TabIndex = 11
        Me.ckbsave.Text = "AutoSave"
        Me.ckbsave.UseVisualStyleBackColor = True
        '
        'notifyIcon1
        '
        Me.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.notifyIcon1.BalloonTipText = "stygianthebest.github.io"
        Me.notifyIcon1.BalloonTipTitle = "StygianCore Controls"
        Me.notifyIcon1.ContextMenuStrip = Me.contextMenuStrip1
        Me.notifyIcon1.Icon = CType(resources.GetObject("notifyIcon1.Icon"), System.Drawing.Icon)
        Me.notifyIcon1.Text = "notifyIcon1"
        '
        'contextMenuStrip1
        '
        Me.contextMenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.saveLogToolStripMenuItem, Me.openToolStripMenuItem})
        Me.contextMenuStrip1.Name = "contextMenuStrip1"
        Me.contextMenuStrip1.Size = New System.Drawing.Size(145, 52)
        '
        'saveLogToolStripMenuItem
        '
        Me.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem"
        Me.saveLogToolStripMenuItem.Size = New System.Drawing.Size(144, 24)
        Me.saveLogToolStripMenuItem.Text = "Save Log"
        '
        'openToolStripMenuItem
        '
        Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
        Me.openToolStripMenuItem.Size = New System.Drawing.Size(144, 24)
        Me.openToolStripMenuItem.Text = "Open"
        '
        'timer
        '
        Me.timer.Interval = 5000
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 178)
        Me.Controls.Add(Me.sqlpic)
        Me.Controls.Add(Me.pictureBox8)
        Me.Controls.Add(Me.ctrlBtn)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnsave)
        Me.Controls.Add(Me.btnsettings)
        Me.Controls.Add(Me.ckbsave)
        Me.Controls.Add(Me.ckbhide)
        Me.Controls.Add(Me.pb_hideproc)
        Me.Controls.Add(Me.pictureBox2)
        Me.Controls.Add(Me.pb_autosave)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.grplog)
        Me.Controls.Add(Me.btnlog)
        Me.Controls.Add(Me.groupBox4)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.groupBox2)
        Me.Controls.Add(Me.groupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen

        Me.wpanel.ResumeLayout(False)
        Me.wpanel.PerformLayout()
        CType(Me.pictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wflag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wbtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        CType(Me.pictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lflag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lbtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox3.ResumeLayout(False)
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mflag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.mbtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox4.ResumeLayout(False)
        Me.panel3.ResumeLayout(False)
        Me.panel3.PerformLayout()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.aflag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.abtn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grplog.ResumeLayout(False)
        CType(Me.pictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnlog, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_autosave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_hideproc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsettings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.btnsave, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ctrlBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sqlpic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.contextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

End Class
