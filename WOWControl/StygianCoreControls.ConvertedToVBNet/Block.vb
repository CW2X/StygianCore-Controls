' 
' Type: StygianCoreControls.Block
' Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
' Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

Imports StygianCoreControls.Properties
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports MainForm

Public Class Block
	Public runpath As String = ""
	Public lastlog As String = ""

	Public Sub New(pa As String, p As String, ByRef b As PictureBox, ByRef f As PictureBox, ByRef c As CheckBox, ByRef r As RichTextBox, _
		n As String, ByRef l As Label)
		Me.timer = New Timer()
		Me.ckb = c
		Me.log = r
		Me.name = n
		Me.procname = p
        Me.paths = pa
        Me.seconds = 0
		Me.running = False
		Me.timer.Interval = 1000
		AddHandler Me.timer.Tick, New EventHandler(AddressOf Me.timer_tick)
		Me.button = b
		AddHandler Me.button.Click, New EventHandler(AddressOf Me.btn_click)
		Me.flag = f
		Me.timer.Start()
		Me.time = l

        MainForm.EventLogFile = Path.Combine(MainForm.AutoSaveFolder, MainForm.AutoSaveFileName)
        If Me.ckb.Checked AndAlso Not Me.running AndAlso Me.paths <> "" Then
            If Me.start() IsNot Nothing Then
                Me.write("Start Failed: " & Me.start().Message, False)
            Else
                Me.write("Started Successfully", False)
            End If
        Else
            If Not (Me.paths = "") Then
                Return
            End If
            Me.write("Please specify a valid path!", False)
		End If
	End Sub

	Public Property main() As MainForm
		Get
			Return m_main
		End Get
		Set
			m_main = Value
		End Set
	End Property
	Private m_main As MainForm

	Public Property seconds() As Integer
		Get
			Return m_seconds
		End Get
		Set
			m_seconds = Value
		End Set
	End Property
	Private m_seconds As Integer

	Public Property running() As Boolean
		Get
			Return m_running
		End Get
		Set
			m_running = Value
		End Set
	End Property
	Private m_running As Boolean

    Public Property paths() As String
        Get
            Return m_path
        End Get
        Set
            m_path = Value
        End Set
    End Property
    Private m_path As String

	Public Property procname() As String
		Get
			Return m_procname
		End Get
		Set
			m_procname = Value
		End Set
	End Property
	Private m_procname As String

	Public Property name() As String
		Get
			Return m_name
		End Get
		Set
			m_name = Value
		End Set
	End Property
	Private m_name As String

	Public Property ckb() As CheckBox
		Get
			Return m_ckb
		End Get
		Set
			m_ckb = Value
		End Set
	End Property
	Private m_ckb As CheckBox

	Public Property log() As RichTextBox
		Get
			Return m_log
		End Get
		Set
			m_log = Value
		End Set
	End Property
	Private m_log As RichTextBox

	Public Property button() As PictureBox
		Get
			Return m_button
		End Get
		Set
			m_button = Value
		End Set
	End Property
	Private m_button As PictureBox

	Public Property flag() As PictureBox
		Get
			Return m_flag
		End Get
		Set
			m_flag = Value
		End Set
	End Property
	Private m_flag As PictureBox

	Public Property time() As Label
		Get
			Return m_time
		End Get
		Set
			m_time = Value
		End Set
	End Property
	Private m_time As Label

	Public Property timer() As Timer
		Get
			Return m_timer
		End Get
		Set
			m_timer = Value
		End Set
	End Property
	Private m_timer As Timer

	Public Property lasttick() As Boolean
		Get
			Return m_lasttick
		End Get
		Set
			m_lasttick = Value
		End Set
	End Property
	Private m_lasttick As Boolean

	Public Property juststarted() As Boolean
		Get
			Return m_juststarted
		End Get
		Set
			m_juststarted = Value
		End Set
	End Property
	Private m_juststarted As Boolean

	Public Property juststopped() As Boolean
		Get
			Return m_juststopped
		End Get
		Set
			m_juststopped = Value
		End Set
	End Property
	Private m_juststopped As Boolean

	Public Sub timer_tick(sender As Object, e As EventArgs)
		If Not Me.running Then
			Me.seconds = 0
			Me.button.Image = DirectCast(Resources.control_play_blue, Image)
		Else
			Me.seconds += 1
			Me.button.Image = DirectCast(Resources.control_stop_blue, Image)
		End If
		Dim seconds As Integer = Me.seconds
		Dim num1 As Integer = Me.seconds \ 60
		Dim num2 As Integer = num1 \ 60
		Dim num3 As Integer = num2 \ 24
		Dim num4 As Integer = seconds - 60 * num1
		Dim num5 As Integer = num1 - 60 * num2
		Dim num6 As Integer = num2 - 24 * num3
		Dim str1 As String = If(num4 >= 10, num4.ToString(), "0" & num4.ToString())
		Dim str2 As String = If(num5 >= 10, num5.ToString(), "0" & num5.ToString())
		Dim str3 As String = If(num6 >= 10, num6.ToString(), "0" & num6.ToString())
		Me.time.Text = (If(num3 >= 10, num3.ToString(), "0" & num3.ToString())) & ":" & str3 & ":" & str2 & ":" & str1
		For Each process__1 As Process In Process.GetProcesses()
			If process__1.ProcessName = Me.procname Then
				Me.running = True
				Me.lasttick = True
				Me.write("Process Detected", False)
				If Not process__1.Responding Then
					Me.write("No Response - Terminating", False)
					process__1.Kill()
					Exit For
				End If
				Exit For
			End If
			Me.running = False
		Next
		If Me.lasttick AndAlso Not Me.running Then
			If Not Me.juststopped Then
				Me.juststopped = False
				Me.write("Crash Detected!", False)
				If Me.ckb.Checked Then
					Me.start()
				End If
			Else
				Me.juststopped = False
				Me.write("Process Stopped", False)
				Me.lasttick = False
			End If
		End If
        Me.flag.Image = If(Not Me.running, (If(File.Exists(Me.paths), DirectCast(Resources.icon_4672, Image), DirectCast(Resources.icon_4670, Image))), DirectCast(Resources.icon_4671, Image))
        Me.juststarted = False
	End Sub

	Public Sub btn_click(sender As Object, e As EventArgs)
        If Me.paths = "" Then
            Dim num1 As Integer = CInt(MessageBox.Show("No path specified for " & Me.name & ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand))
            Dim settings As New Settings()
            If Me.name = "Web" Then
                settings.SetBlock(MainForm.apache)
            ElseIf Me.name = "Database" Then
                settings.SetBlock(MainForm.mysql)
            ElseIf Me.name = "World" Then
                settings.SetBlock(MainForm.world)
            Else
                settings.SetBlock(MainForm.logon)
            End If
            Dim num2 As Integer = CInt(settings.ShowDialog())
        ElseIf Not Me.running Then
            If Me.start() IsNot Nothing Then
                Me.write("Start Failed: " & Me.start().Message, True)
            Else
                Me.juststarted = True
            End If
        Else
            For Each process1 As Process In Process.GetProcesses()
				If process1.ProcessName = Me.procname Then
					Me.write("Process Stopped", False)
					Me.juststopped = True
					If Me.procname = "mysqld" Then
						Dim startInfo As New ProcessStartInfo()
						startInfo.WorkingDirectory = Environment.CurrentDirectory
						startInfo.FileName = Environment.CurrentDirectory & "\Server\MySQL\bin\mysqladmin.exe"
						startInfo.Arguments = " --defaults-extra-file=Server\MySQL\my.cnf shutdown"
						Dim str As String = startInfo.FileName & startInfo.Arguments
						startInfo.WindowStyle = ProcessWindowStyle.Normal
						startInfo.CreateNoWindow = False
						Using process2 As Process = Process.Start(startInfo)
							process2.WaitForExit()
							Dim exitCode As Integer = process2.ExitCode
							Exit For
						End Using
					Else
						process1.Kill()
						Exit For
					End If
				End If
			Next
		End If
	End Sub

	Public Function start() As Exception
		Try
            Dim index As Integer = Me.paths.Length - 1
            While Me.paths(index) <> "\"c
                index -= 1
			End While
            Dim str As String = Me.paths.Substring(0, index + 1)
            Dim startInfo As New ProcessStartInfo()
            startInfo.FileName = Me.paths
            startInfo.WorkingDirectory = str
            If MainForm.ckbhide.Checked Then
                startInfo.WindowStyle = ProcessWindowStyle.Hidden
            End If
            Process.Start(startInfo)
			Return DirectCast(Nothing, Exception)
		Catch ex As Exception
			Return ex
		End Try
	End Function

	Public Sub write(str As String, Optional force As Boolean = False)
		If Me.lastlog <> str AndAlso Not force Then
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] " & Me.name & " : " & str & Environment.NewLine
		ElseIf Me.lastlog = str And force Then
			Dim log As RichTextBox = Me.log
			log.Text = log.Text & "[" & DateTime.Now.ToString() & "] " & Me.name & " : " & str & Environment.NewLine
		End If
		Me.lastlog = str
	End Sub
End Class
