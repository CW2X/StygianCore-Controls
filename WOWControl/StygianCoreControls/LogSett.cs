// Decompiled with JetBrains decompiler
// Type: StygianCoreControls.LogSett
// Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
// Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

using StygianCoreControls.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StygianCoreControls
{
  public class LogSett : Form
  {
    public static string LogFolder;
    public static int IntervalTime;
    public static string AutoSaveFile;
    private IContainer components;
    private Label label1;
    private TextBox LogPath;
    private PictureBox btnbrowse;
    private Label label2;
    private NumericUpDown SaveInterval;
    private Label label3;
    private FolderBrowserDialog fbd;

    public LogSett()
    {
      this.InitializeComponent();
    }

    private void LogSett_Load(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(StygianCoreControls.Properties.Settings.Default.logpath))
      {
        this.LogPath.Text = Application.StartupPath;
        Main.AutoSaveFolder = Application.StartupPath;
      }
      else
        this.LogPath.Text = StygianCoreControls.Properties.Settings.Default.logpath;
      this.SaveInterval.Value = (Decimal) StygianCoreControls.Properties.Settings.Default.loginterval;
    }

    private void btnbrowse_Click(object sender, EventArgs e)
    {
      this.fbd.ShowNewFolderButton = true;
      this.fbd.RootFolder = Environment.SpecialFolder.MyComputer;
      this.fbd.SelectedPath = ".";
      int num = (int) this.fbd.ShowDialog();
      this.LogPath.Text = this.fbd.SelectedPath;
    }

    private void LogSett_FormClosing(object sender, FormClosingEventArgs e)
    {
      Main.AutoSaveFolder = this.LogPath.Text;
      Main.AutoSaveFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
      Main.AutoSaveInterval = Convert.ToInt32(this.SaveInterval.Value);
      LogSett.AutoSaveFile = Path.Combine(Main.AutoSaveFolder, Main.AutoSaveFileName);
      StygianCoreControls.Properties.Settings.Default.logpath = Main.AutoSaveFolder;
      StygianCoreControls.Properties.Settings.Default.loginterval = Main.AutoSaveInterval;
      StygianCoreControls.Properties.Settings.Default.Save();
      StygianCoreControls.Properties.Settings.Default.Reload();
      if (File.Exists(LogSett.AutoSaveFile))
        return;
      File.WriteAllText(LogSett.AutoSaveFile, ":: StygianCore Event Log :: Started: " + DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") + " :: " + Environment.NewLine + ":: Interval: " + (object) Convert.ToInt32(this.SaveInterval.Value) + " :: stygianthebest.github.io ::  \n--------------------------------------------------------------------------------------------" + Environment.NewLine);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.LogPath = new TextBox();
      this.btnbrowse = new PictureBox();
      this.label2 = new Label();
      this.SaveInterval = new NumericUpDown();
      this.label3 = new Label();
      this.fbd = new FolderBrowserDialog();
      ((ISupportInitialize) this.btnbrowse).BeginInit();
      this.SaveInterval.BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(8, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(41, 17);
      this.label1.TabIndex = 0;
      this.label1.Text = "Path:";
      this.LogPath.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.LogPath.Location = new Point(11, 29);
      this.LogPath.Name = "LogPath";
      this.LogPath.Size = new Size(133, 24);
      this.LogPath.TabIndex = 1;
      this.btnbrowse.Cursor = Cursors.Hand;
      this.btnbrowse.Image = (Image) Resources.folder_search;
      this.btnbrowse.Location = new Point(149, 34);
      this.btnbrowse.Name = "btnbrowse";
      this.btnbrowse.Size = new Size(16, 16);
      this.btnbrowse.TabIndex = 2;
      this.btnbrowse.TabStop = false;
      this.btnbrowse.Click += new EventHandler(this.btnbrowse_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(168, 9);
      this.label2.Name = "label2";
      this.label2.Size = new Size(58, 17);
      this.label2.TabIndex = 3;
      this.label2.Text = "Interval:";
      this.SaveInterval.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.SaveInterval.Increment = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.SaveInterval.Location = new Point(171, 29);
      this.SaveInterval.Maximum = new Decimal(new int[4]
      {
        60000,
        0,
        0,
        0
      });
      this.SaveInterval.Minimum = new Decimal(new int[4]
      {
        5000,
        0,
        0,
        0
      });
      this.SaveInterval.Name = "SaveInterval";
      this.SaveInterval.Size = new Size(87, 24);
      this.SaveInterval.TabIndex = 4;
      this.SaveInterval.Value = new Decimal(new int[4]
      {
        5000,
        0,
        0,
        0
      });
      this.label3.AutoSize = true;
      this.label3.Location = new Point(261, 33);
      this.label3.Name = "label3";
      this.label3.Size = new Size(26, 17);
      this.label3.TabIndex = 5;
      this.label3.Text = "ms";
      this.fbd.RootFolder = Environment.SpecialFolder.Personal;
      this.AutoScaleDimensions = new SizeF(8f, 17f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(297, 58);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.SaveInterval);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.btnbrowse);
      this.Controls.Add((Control) this.LogPath);
      this.Controls.Add((Control) this.label1);
      this.Font = new Font("Microsoft Sans Serif", 10.25f);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Margin = new Padding(4);
      this.Name = "LogSett";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "AutoSave Settings";
      this.FormClosing += new FormClosingEventHandler(this.LogSett_FormClosing);
      this.Load += new EventHandler(this.LogSett_Load);
      ((ISupportInitialize) this.btnbrowse).EndInit();
      this.SaveInterval.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
