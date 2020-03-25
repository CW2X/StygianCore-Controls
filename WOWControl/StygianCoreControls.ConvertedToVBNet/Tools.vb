' 
' Type: Tools
' Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
' Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

Imports StygianCoreControls
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class Tools
	Public Shared Function TakeLastLines(text As RichTextBox, count As Integer) As List(Of String)
		Dim input As String = text.Text.ToString()
		Dim str As String = "(\n\[)"
		Dim stringList As New List(Of String)()
		Dim pattern As String = str
		Dim num As Integer = 66
		Dim match As Match = Regex.Match(input, pattern, CType(num, RegexOptions))
		While match.Success AndAlso stringList.Count < count
			stringList.Insert(0, match.Value)
			match = match.NextMatch()
		End While
		Return stringList
	End Function

	Public Shared Sub SaveLogFile(Log As RichTextBox)
		Dim path2 As String = DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") & ".txt"
		If String.IsNullOrEmpty(MainForm.AutoSaveFolder) Then
			MainForm.AutoSaveFolder = Application.StartupPath
		End If
		Dim path__1 As String = Path.Combine(MainForm.AutoSaveFolder, path2)
		Dim stringBuilder As New StringBuilder()
		For index As Integer = 0 To Log.Lines.Length - 1
			stringBuilder.AppendLine(Log.Lines(index))
		Next
		File.WriteAllText(path__1, stringBuilder.ToString())
		Dim num As Integer = CInt(MessageBox.Show("Event Log Saved" & vbLf & vbLf & path__1, "Event Log", MessageBoxButtons.OK, MessageBoxIcon.Asterisk))
	End Sub
End Class
