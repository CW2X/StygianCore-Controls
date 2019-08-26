// 
// Type: StygianCoreControls.Block
// Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
// Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

using StygianCoreControls.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StygianCoreControls
{
  public class Block
  {
    public string runpath = "";
    public string lastlog = "";

    public Block(string pa, string p, ref PictureBox b, ref PictureBox f, ref CheckBox c, ref RichTextBox r, string n, ref Label l)
    {
      this.timer = new Timer();
      this.ckb = c;
      this.log = r;
      this.name = n;
      this.procname = p;
      this.path = pa;
      this.seconds = 0;
      this.running = false;
      this.timer.Interval = 1000;
      this.timer.Tick += new EventHandler(this.timer_tick);
      this.button = b;
      this.button.Click += new EventHandler(this.btn_click);
      this.flag = f;
      this.timer.Start();
      this.time = l;
      new StringBuilder().Clear();
      Main.EventLogFile = Path.Combine(Main.AutoSaveFolder, Main.AutoSaveFileName);
      if (this.ckb.Checked && !this.running && this.path != "")
      {
        if (this.start() != null)
          this.write("Start Failed: " + this.start().Message, false);
        else
          this.write("Started Successfully", false);
      }
      else
      {
        if (!(this.path == ""))
          return;
        this.write("Please specify a valid path!", false);
      }
    }

    public Main main { get; set; }

    public int seconds { get; set; }

    public bool running { get; set; }

    public string path { get; set; }

    public string procname { get; set; }

    public string name { get; set; }

    public CheckBox ckb { get; set; }

    public RichTextBox log { get; set; }

    public PictureBox button { get; set; }

    public PictureBox flag { get; set; }

    public Label time { get; set; }

    public Timer timer { get; set; }

    public bool lasttick { get; set; }

    public bool juststarted { get; set; }

    public bool juststopped { get; set; }

    public void timer_tick(object sender, EventArgs e)
    {
      if (!this.running)
      {
        this.seconds = 0;
        this.button.Image = (Image) Resources.control_play_blue;
      }
      else
      {
        ++this.seconds;
        this.button.Image = (Image) Resources.control_stop_blue;
      }
      int seconds = this.seconds;
      int num1 = this.seconds / 60;
      int num2 = num1 / 60;
      int num3 = num2 / 24;
      int num4 = seconds - 60 * num1;
      int num5 = num1 - 60 * num2;
      int num6 = num2 - 24 * num3;
      string str1 = num4 >= 10 ? num4.ToString() : "0" + num4.ToString();
      string str2 = num5 >= 10 ? num5.ToString() : "0" + num5.ToString();
      string str3 = num6 >= 10 ? num6.ToString() : "0" + num6.ToString();
      this.time.Text = (num3 >= 10 ? num3.ToString() : "0" + num3.ToString()) + ":" + str3 + ":" + str2 + ":" + str1;
      foreach (Process process in Process.GetProcesses())
      {
        if (process.ProcessName == this.procname)
        {
          this.running = true;
          this.lasttick = true;
          this.write("Process Detected", false);
          if (!process.Responding)
          {
            this.write("No Response - Terminating", false);
            process.Kill();
            break;
          }
          break;
        }
        this.running = false;
      }
      if (this.lasttick && !this.running)
      {
        if (!this.juststopped)
        {
          this.juststopped = false;
          this.write("Crash Detected!", false);
          if (this.ckb.Checked)
            this.start();
        }
        else
        {
          this.juststopped = false;
          this.write("Process Stopped", false);
          this.lasttick = false;
        }
      }
      this.flag.Image = !this.running ? (File.Exists(this.path) ? (Image) Resources.icon_4672 : (Image) Resources.icon_4670) : (Image) Resources.icon_4671;
      this.juststarted = false;
    }

    public void btn_click(object sender, EventArgs e)
    {
      if (this.path == "")
      {
        int num1 = (int) MessageBox.Show("No path specified for " + this.name + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Settings settings = new Settings();
        if (this.name == "Web")
          settings.SetBlock(ref Main.apache);
        else if (this.name == "Database")
          settings.SetBlock(ref Main.mysql);
        else if (this.name == "World")
          settings.SetBlock(ref Main.world);
        else
          settings.SetBlock(ref Main.logon);
        int num2 = (int) settings.ShowDialog();
      }
      else if (!this.running)
      {
        if (this.start() != null)
          this.write("Start Failed: " + this.start().Message, true);
        else
          this.juststarted = true;
      }
      else
      {
        foreach (Process process1 in Process.GetProcesses())
        {
          if (process1.ProcessName == this.procname)
          {
            this.write("Process Stopped", false);
            this.juststopped = true;
            if (this.procname == "mysqld")
            {
              ProcessStartInfo startInfo = new ProcessStartInfo();
              startInfo.WorkingDirectory = Environment.CurrentDirectory;
              startInfo.FileName = Environment.CurrentDirectory + "\\Server\\MySQL\\bin\\mysqladmin.exe";
              startInfo.Arguments = " --defaults-extra-file=Server\\MySQL\\my.cnf shutdown";
              string str = startInfo.FileName + startInfo.Arguments;
              startInfo.WindowStyle = ProcessWindowStyle.Normal;
              startInfo.CreateNoWindow = false;
              using (Process process2 = Process.Start(startInfo))
              {
                process2.WaitForExit();
                int exitCode = process2.ExitCode;
                break;
              }
            }
            else
            {
              process1.Kill();
              break;
            }
          }
        }
      }
    }

    public Exception start()
    {
      try
      {
        int index = this.path.Length - 1;
        while (this.path[index] != '\\')
          --index;
        string str = this.path.Substring(0, index + 1);
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = this.path;
        startInfo.WorkingDirectory = str;
        if (Program.main.ckbhide.Checked)
          startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        Process.Start(startInfo);
        return (Exception) null;
      }
      catch (Exception ex)
      {
        return ex;
      }
    }

    public void write(string str, bool force = false)
    {
      if (this.lastlog != str && !force)
      {
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] " + this.name + " : " + str + Environment.NewLine;
      }
      else if (this.lastlog == str & force)
      {
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] " + this.name + " : " + str + Environment.NewLine;
      }
      this.lastlog = str;
    }
  }
}
