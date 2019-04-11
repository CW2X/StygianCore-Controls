// Decompiled with JetBrains decompiler
// Type: StygianCoreControls.Properties.Settings
// Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
// Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace StygianCoreControls.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.9.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue(".\\start_world.bat")]
    public string wpath
    {
      get
      {
        return (string) this[nameof (wpath)];
      }
      set
      {
        this[nameof (wpath)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("worldserver")]
    public string wproc
    {
      get
      {
        return (string) this[nameof (wproc)];
      }
      set
      {
        this[nameof (wproc)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue(".\\start_auth.bat")]
    public string lpath
    {
      get
      {
        return (string) this[nameof (lpath)];
      }
      set
      {
        this[nameof (lpath)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("authserver")]
    public string lproc
    {
      get
      {
        return (string) this[nameof (lproc)];
      }
      set
      {
        this[nameof (lproc)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue(".\\start_database.bat")]
    public string mpath
    {
      get
      {
        return (string) this[nameof (mpath)];
      }
      set
      {
        this[nameof (mpath)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("mysqld")]
    public string mproc
    {
      get
      {
        return (string) this[nameof (mproc)];
      }
      set
      {
        this[nameof (mproc)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue(".\\start_web.bat")]
    public string apath
    {
      get
      {
        return (string) this[nameof (apath)];
      }
      set
      {
        this[nameof (apath)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("httpd")]
    public string aproc
    {
      get
      {
        return (string) this[nameof (aproc)];
      }
      set
      {
        this[nameof (aproc)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool autosave
    {
      get
      {
        return (bool) this[nameof (autosave)];
      }
      set
      {
        this[nameof (autosave)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue(".\\Server\\Core\\logs\\")]
    public string logpath
    {
      get
      {
        return (string) this[nameof (logpath)];
      }
      set
      {
        this[nameof (logpath)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("5000")]
    public int loginterval
    {
      get
      {
        return (int) this[nameof (loginterval)];
      }
      set
      {
        this[nameof (loginterval)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool hide
    {
      get
      {
        return (bool) this[nameof (hide)];
      }
      set
      {
        this[nameof (hide)] = (object) value;
      }
    }
  }
}
