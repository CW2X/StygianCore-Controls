' 
' Type: StygianCoreControls.Properties.Settings
' Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
' Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

Imports System.CodeDom.Compiler
Imports System.Configuration
Imports System.Diagnostics
Imports System.Runtime.CompilerServices

Namespace Properties
	<CompilerGenerated> _
	<GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")> _
	Friend NotInheritable Class Settings
		Inherits ApplicationSettingsBase
		Private Shared defaultInstance As Settings = DirectCast(SettingsBase.Synchronized(DirectCast(New Settings(), SettingsBase)), Settings)

		Public Shared ReadOnly Property [Default]() As Settings
			Get
				Return Settings.defaultInstance
			End Get
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue(".\start_world.bat")> _
		Public Property wpath() As String
			Get
				Return DirectCast(Me(nameof(wpath)), String)
			End Get
			Set
				Me(nameof(wpath)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue("worldserver")> _
		Public Property wproc() As String
			Get
				Return DirectCast(Me(nameof(wproc)), String)
			End Get
			Set
				Me(nameof(wproc)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue(".\start_auth.bat")> _
		Public Property lpath() As String
			Get
				Return DirectCast(Me(nameof(lpath)), String)
			End Get
			Set
				Me(nameof(lpath)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue("authserver")> _
		Public Property lproc() As String
			Get
				Return DirectCast(Me(nameof(lproc)), String)
			End Get
			Set
				Me(nameof(lproc)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue(".\start_database.bat")> _
		Public Property mpath() As String
			Get
				Return DirectCast(Me(nameof(mpath)), String)
			End Get
			Set
				Me(nameof(mpath)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue("mysqld")> _
		Public Property mproc() As String
			Get
				Return DirectCast(Me(nameof(mproc)), String)
			End Get
			Set
				Me(nameof(mproc)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue(".\start_web.bat")> _
		Public Property apath() As String
			Get
				Return DirectCast(Me(nameof(apath)), String)
			End Get
			Set
				Me(nameof(apath)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue("httpd")> _
		Public Property aproc() As String
			Get
				Return DirectCast(Me(nameof(aproc)), String)
			End Get
			Set
				Me(nameof(aproc)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue("False")> _
		Public Property autosave() As Boolean
			Get
				Return CBool(Me(nameof(autosave)))
			End Get
			Set
				Me(nameof(autosave)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue(".\Server\Core\logs\")> _
		Public Property logpath() As String
			Get
				Return DirectCast(Me(nameof(logpath)), String)
			End Get
			Set
				Me(nameof(logpath)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue("5000")> _
		Public Property loginterval() As Integer
			Get
				Return CInt(Me(nameof(loginterval)))
			End Get
			Set
				Me(nameof(loginterval)) = DirectCast(value, Object)
			End Set
		End Property

		<UserScopedSetting> _
		<DebuggerNonUserCode> _
		<DefaultSettingValue("True")> _
		Public Property hide() As Boolean
			Get
				Return CBool(Me(nameof(hide)))
			End Get
			Set
				Me(nameof(hide)) = DirectCast(value, Object)
			End Set
		End Property
	End Class
End Namespace
