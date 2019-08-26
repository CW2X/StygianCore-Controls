// 
// Type: StygianCoreControls.Program
// Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
// Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

using System;
using System.Windows.Forms;

namespace StygianCoreControls
{
  internal static class Program
  {
    public static Main main;

    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Program.main = new Main();
      Application.Run((Form) Program.main);
    }
  }
}
