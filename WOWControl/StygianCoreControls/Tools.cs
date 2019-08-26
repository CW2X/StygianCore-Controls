// 
// Type: Tools
// Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
// Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

using StygianCoreControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class Tools
{
  public static List<string> TakeLastLines(RichTextBox text, int count)
  {
    string input = text.Text.ToString();
    string str = "(\\n\\[)";
    List<string> stringList = new List<string>();
    string pattern = str;
    int num = 66;
    for (Match match = Regex.Match(input, pattern, (RegexOptions) num); match.Success && stringList.Count < count; match = match.NextMatch())
      stringList.Insert(0, match.Value);
    return stringList;
  }

  public static void SaveLogFile(RichTextBox Log)
  {
    string path2 = DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") + ".txt";
    if (string.IsNullOrEmpty(Main.AutoSaveFolder))
      Main.AutoSaveFolder = Application.StartupPath;
    string path = Path.Combine(Main.AutoSaveFolder, path2);
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < Log.Lines.Length; ++index)
      stringBuilder.AppendLine(Log.Lines[index]);
    File.WriteAllText(path, stringBuilder.ToString());
    int num = (int) MessageBox.Show("Event Log Saved\n\n" + path, "Event Log", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }
}
