// Decompiled with JetBrains decompiler
// Type: StygianCoreControls.Main
// Assembly: StygianCoreControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5EFC93AA-1C3F-4A5D-8CDA-0294962306B4
// Assembly location: E:\StygianCore_v2019.03.10\StygianCoreControls.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StygianCoreControls
{
  public class Main : Form
  {
    public StringBuilder text = new StringBuilder();
    public int logw = 730;
    public int nonw = 418;
    public bool ext;
    public static string LastLogEvent;
    public static bool LogChecked;
    private ContextMenu contextMenu1;
    public static Block world;
    public static Block mysql;
    public static Block logon;
    public static Block apache;
    private IContainer components;
    public Panel wpanel;
    public PictureBox wbtn;
    public PictureBox wflag;
    public GroupBox groupBox1;
    public GroupBox groupBox2;
    public Panel panel1;
    public PictureBox lflag;
    public PictureBox lbtn;
    public GroupBox groupBox3;
    public Panel panel2;
    public PictureBox mflag;
    public PictureBox mbtn;
    public GroupBox groupBox4;
    public Panel panel3;
    public PictureBox aflag;
    public PictureBox abtn;
    public Label wtime;
    public Label ltime;
    public Label atime;
    public CheckBox wckb;
    public CheckBox lckb;
    public CheckBox mckb;
    public CheckBox ackb;
    public Label mtime;
    private PictureBox btnlog;
    private GroupBox grplog;
    private ToolTip tt;
    public RichTextBox log;
    private Timer timer;
    private PictureBox pictureBox1;
    private PictureBox pb_autosave;
    private PictureBox pb_hideproc;
    private PictureBox pictureBox2;
    private PictureBox pictureBox6;
    private PictureBox pictureBox5;
    private PictureBox pictureBox4;
    private PictureBox pictureBox3;
    private PictureBox pictureBox7;
    public CheckBox ckbhide;
    public CheckBox ckbsave;
    public PictureBox btnsettings;
    private PictureBox btnsave;
    private Label label1;
    private PictureBox ctrlBtn;
    private PictureBox pictureBox8;
    private NotifyIcon notifyIcon1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem saveLogToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private PictureBox sqlpic;

    public Main()
    {
      this.InitializeComponent();
    }

    public static int AutoSaveInterval { get; set; }

    public static string AutoSaveFolder { get; set; }

    public static string AutoSaveFileName { get; set; }

    public static string EventLogFile { get; set; }

    public static StreamWriter AutoSaveFile { get; set; }

    public new event MouseEventHandler MouseMove;

    private void Main_Load(object sender, EventArgs e)
    {
      this.notifyIcon1.DoubleClick += new EventHandler(this.notifyIcon1_DoubleClick);
      this.notifyIcon1.Click += new EventHandler(this.notifyIcon1_Click);
      this.Resize += new EventHandler(this.Form1_Resize);
      this.notifyIcon1.BalloonTipTitle = "StygianCore Controls";
      this.notifyIcon1.BalloonTipText = "stygianthebest.github.io";
      this.notifyIcon1.Text = "StygianCore Controls";
      this.notifyIcon1.ContextMenu = this.contextMenu1;
      this.notifyIcon1.Visible = false;
      Main.AutoSaveInterval = StygianCoreControls.Properties.Settings.Default.loginterval;
      if (Main.AutoSaveInterval >= 5000 && Main.AutoSaveInterval <= 60000)
      {
        this.timer.Interval = StygianCoreControls.Properties.Settings.Default.loginterval;
        Main.AutoSaveInterval = this.timer.Interval;
        this.label1.Text = Main.AutoSaveInterval.ToString() + " ms";
        this.timer.Start();
      }
      else
      {
        StygianCoreControls.Properties.Settings.Default.loginterval = 5000;
        this.timer.Interval = 5000;
        Main.AutoSaveInterval = this.timer.Interval;
        this.label1.Text = Main.AutoSaveInterval.ToString() + " ms";
        this.timer.Start();
      }
      if (StygianCoreControls.Properties.Settings.Default.autosave)
      {
        this.ckbsave.Checked = StygianCoreControls.Properties.Settings.Default.autosave;
        this.pb_autosave.Visible = true;
      }
      if (StygianCoreControls.Properties.Settings.Default.hide)
      {
        this.ckbhide.Checked = StygianCoreControls.Properties.Settings.Default.hide;
        this.pb_hideproc.Visible = true;
      }
      Main.AutoSaveFolder = !string.IsNullOrEmpty(StygianCoreControls.Properties.Settings.Default.logpath) ? StygianCoreControls.Properties.Settings.Default.logpath : Application.StartupPath;
      Main.AutoSaveFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
      Main.EventLogFile = Path.Combine(Main.AutoSaveFolder, Main.AutoSaveFileName);
      if (File.Exists(Main.EventLogFile) && !Main.LogChecked)
      {
        this.text.AppendLine(File.ReadAllText(Main.EventLogFile));
        this.text.AppendLine(":: Logging restarted on " + DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") + " ::");
        this.log.Text = this.text.ToString();
        Main.LogChecked = true;
      }
      else
      {
        Main.LogChecked = true;
        this.text.AppendLine(":: StygianCore Event Log :: Started: " + DateTime.Now.ToString("yyyy-MM-dd@HH.mm.ss") + " :: " + Environment.NewLine + ":: Interval: " + (object) Main.AutoSaveInterval + Environment.NewLine + "---------------------------------------------------------------------------------------" + Environment.NewLine);
        this.log.Text = this.text.ToString();
      }
      Main.world = new Block(StygianCoreControls.Properties.Settings.Default.wpath, StygianCoreControls.Properties.Settings.Default.wproc, ref this.wbtn, ref this.wflag, ref this.wckb, ref this.log, "World", ref this.wtime);
      Main.mysql = new Block(StygianCoreControls.Properties.Settings.Default.mpath, StygianCoreControls.Properties.Settings.Default.mproc, ref this.mbtn, ref this.mflag, ref this.mckb, ref this.log, "Database", ref this.mtime);
      Main.logon = new Block(StygianCoreControls.Properties.Settings.Default.lpath, StygianCoreControls.Properties.Settings.Default.lproc, ref this.lbtn, ref this.lflag, ref this.lckb, ref this.log, "Auth", ref this.ltime);
      Main.apache = new Block(StygianCoreControls.Properties.Settings.Default.apath, StygianCoreControls.Properties.Settings.Default.aproc, ref this.abtn, ref this.aflag, ref this.ackb, ref this.log, "Web", ref this.atime);
    }

    public void CheckButtons()
    {
      if (!Main.world.running)
        this.wbtn.Cursor = Cursors.Default;
      if (!Main.logon.running)
        this.lbtn.Cursor = Cursors.Default;
      if (!Main.mysql.running)
        this.mbtn.Cursor = Cursors.Default;
      if (Main.apache.running)
        return;
      this.abtn.Cursor = Cursors.Default;
    }

    private void btnlog_Click(object sender, EventArgs e)
    {
      if (this.ext)
      {
        this.ext = false;
        this.Width = this.nonw;
        this.grplog.Visible = false;
      }
      else
      {
        this.ext = true;
        this.Width = this.logw;
        this.grplog.Visible = true;
      }
    }

    private void wflag_Click(object sender, EventArgs e)
    {
      Settings settings = new Settings();
      settings.SetBlock(ref Main.world);
      int num = (int) settings.ShowDialog();
    }

    private void lflag_Click(object sender, EventArgs e)
    {
      Settings settings = new Settings();
      settings.SetBlock(ref Main.logon);
      int num = (int) settings.ShowDialog();
    }

    private void mflag_Click(object sender, EventArgs e)
    {
      Settings settings = new Settings();
      settings.SetBlock(ref Main.mysql);
      int num = (int) settings.ShowDialog();
    }

    private void aflag_Click(object sender, EventArgs e)
    {
      Settings settings = new Settings();
      settings.SetBlock(ref Main.apache);
      int num = (int) settings.ShowDialog();
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
      StygianCoreControls.Properties.Settings.Default.wpath = Main.world.path;
      StygianCoreControls.Properties.Settings.Default.wproc = Main.world.procname;
      StygianCoreControls.Properties.Settings.Default.lpath = Main.logon.path;
      StygianCoreControls.Properties.Settings.Default.lproc = Main.logon.procname;
      StygianCoreControls.Properties.Settings.Default.mpath = Main.mysql.path;
      StygianCoreControls.Properties.Settings.Default.mproc = Main.mysql.procname;
      StygianCoreControls.Properties.Settings.Default.apath = Main.apache.path;
      StygianCoreControls.Properties.Settings.Default.aproc = Main.apache.procname;
      StygianCoreControls.Properties.Settings.Default.logpath = Main.AutoSaveFolder;
      StygianCoreControls.Properties.Settings.Default.loginterval = Main.AutoSaveInterval;
      StygianCoreControls.Properties.Settings.Default.autosave = this.ckbsave.Checked;
      StygianCoreControls.Properties.Settings.Default.hide = this.ckbhide.Checked;
      StygianCoreControls.Properties.Settings.Default.Save();
      StygianCoreControls.Properties.Settings.Default.Reload();
      if (File.Exists(Main.world.runpath))
        File.Delete(Main.world.runpath);
      if (File.Exists(Main.logon.runpath))
        File.Delete(Main.logon.runpath);
      if (File.Exists(Main.mysql.runpath))
        File.Delete(Main.mysql.runpath);
      if (!File.Exists(Main.apache.runpath))
        return;
      File.Delete(Main.apache.runpath);
    }

    private void log_TextChanged(object sender, EventArgs e)
    {
      this.log.SelectionStart = this.log.Text.Length;
      this.log.ScrollToCaret();
    }

    private void btnsettings_Click(object sender, EventArgs e)
    {
      int num = (int) new LogSett().ShowDialog();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      DateTime now;
      if (this.timer.Interval != Main.AutoSaveInterval)
      {
        this.timer.Interval = Main.AutoSaveInterval;
        this.label1.Text = Main.AutoSaveInterval.ToString() + " ms";
        RichTextBox log = this.log;
        RichTextBox richTextBox = log;
        object[] objArray = new object[7];
        objArray[0] = (object) log.Text;
        objArray[1] = (object) "[";
        int index = 2;
        now = DateTime.Now;
        string str1 = now.ToString();
        objArray[index] = (object) str1;
        objArray[3] = (object) "] Interval Updated to ";
        objArray[4] = (object) Main.AutoSaveInterval;
        objArray[5] = (object) " ms";
        objArray[6] = (object) Environment.NewLine;
        string str2 = string.Concat(objArray);
        richTextBox.Text = str2;
      }
      if (!this.ckbsave.Checked)
        return;
      if (string.IsNullOrEmpty(Main.AutoSaveFolder))
      {
        Main.AutoSaveFolder = Application.StartupPath;
        RichTextBox log = this.log;
        RichTextBox richTextBox = log;
        string[] strArray = new string[5]
        {
          log.Text,
          "[",
          null,
          null,
          null
        };
        int index = 2;
        now = DateTime.Now;
        string str1 = now.ToString();
        strArray[index] = str1;
        strArray[3] = "] Repaired AutoSave Folder";
        strArray[4] = Environment.NewLine;
        string str2 = string.Concat(strArray);
        richTextBox.Text = str2;
      }
      else if (string.IsNullOrEmpty(Main.AutoSaveFileName))
      {
        now = DateTime.Now;
        Main.AutoSaveFileName = now.ToString("yyyy-MM-dd@MM") + ".txt";
        RichTextBox log = this.log;
        RichTextBox richTextBox = log;
        string[] strArray = new string[5]
        {
          log.Text,
          "[",
          null,
          null,
          null
        };
        int index = 2;
        now = DateTime.Now;
        string str1 = now.ToString();
        strArray[index] = str1;
        strArray[3] = "] Repaired AutoSave File";
        strArray[4] = Environment.NewLine;
        string str2 = string.Concat(strArray);
        richTextBox.Text = str2;
      }
      else
      {
        now = DateTime.Now;
        if (now.ToString("HH:mm") == "00:00")
        {
          now = DateTime.Now;
          Main.AutoSaveFileName = Path.Combine(Main.AutoSaveFolder, now.ToString("yyyy-MM-dd@MM") + ".txt");
          string autoSaveFileName = Main.AutoSaveFileName;
          object[] objArray = new object[10];
          objArray[0] = (object) ":: StygianCore Event Log :: Started: ";
          int index = 1;
          now = DateTime.Now;
          string str = now.ToString("yyyy-MM-dd@HH.mm.ss");
          objArray[index] = (object) str;
          objArray[2] = (object) " :: ";
          objArray[3] = (object) Environment.NewLine;
          objArray[4] = (object) ":: Interval: ";
          objArray[5] = (object) Main.AutoSaveInterval;
          objArray[6] = (object) " :: stygianthebest.github.io ::  ";
          objArray[7] = (object) Environment.NewLine;
          objArray[8] = (object) "---------------------------------------------------------------------------------------";
          objArray[9] = (object) Environment.NewLine;
          string contents = string.Concat(objArray);
          File.WriteAllText(autoSaveFileName, contents);
        }
        string path = Path.Combine(Main.AutoSaveFolder, Main.AutoSaveFileName);
        int length = this.log.Lines.Length;
        this.text.Clear();
        for (int index = 0; index < length; ++index)
          this.text.Append(this.log.Lines[index] + Environment.NewLine);
        File.WriteAllText(path, this.text.ToString());
      }
    }

    private void btnsave_Click(object sender, EventArgs e)
    {
      Tools.SaveLogFile(this.log);
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
      Process.Start("http://bit.ly/app_drinkme");
      this.pictureBox2.Visible = true;
      RichTextBox log = this.log;
      log.Text = log.Text + "[" + DateTime.Now.ToString() + "] Drank the potion! " + Environment.NewLine;
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {
      RichTextBox log = this.log;
      log.Text = log.Text + "[" + DateTime.Now.ToString() + "] Visit stygianthebest.github.io " + Environment.NewLine;
    }

    private void ckbsave_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.btnsettings.Visible)
      {
        this.btnsettings.Visible = true;
        this.pb_autosave.Visible = true;
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] AutoSave Enabled " + Environment.NewLine;
      }
      else
      {
        this.btnsettings.Visible = false;
        this.pb_autosave.Visible = false;
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] AutoSave Disabled " + Environment.NewLine;
      }
    }

    private void ckbhide_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.ckbhide.Checked)
      {
        this.pb_hideproc.Visible = false;
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] Hide Processes Disabled " + Environment.NewLine;
      }
      else
      {
        this.pb_hideproc.Visible = true;
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] Hide Processes Enabled " + Environment.NewLine;
      }
    }

    private void ctrlBtn_Click(object sender, EventArgs e)
    {
      try
      {
        string str = string.Format(".");
        new Process()
        {
          StartInfo = {
            WorkingDirectory = str,
            FileName = "StygianCoreTools.bat",
            CreateNoWindow = false
          }
        }.Start();
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] Loaded StygianCoreTools " + Environment.NewLine;
      }
      catch (Exception ex)
      {
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] StygianCoreTools.bat not found!" + Environment.NewLine;
        int num = (int) MessageBox.Show("StygianCoreTools.bat not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        this.Hide();
        this.notifyIcon1.Visible = true;
        this.notifyIcon1.ShowBalloonTip(0);
      }
      else
      {
        if (this.WindowState != FormWindowState.Normal)
          return;
        this.notifyIcon1.Visible = false;
      }
    }

    private void notifyIcon1_DoubleClick(object sender, EventArgs e)
    {
      this.Show();
      this.notifyIcon1.Visible = false;
      this.WindowState = FormWindowState.Normal;
      this.Activate();
    }

    private void notifyIcon1_Click(object sender, EventArgs e)
    {
      string str1 = this.wtime.Text;
      string str2 = this.mtime.Text;
      string str3 = this.ltime.Text;
      string str4 = this.atime.Text;
      if (str1 == "00:00:00:00")
        str1 = "Offline";
      if (str2 == "00:00:00:00")
        str2 = "Offline";
      if (str3 == "00:00:00:00")
        str3 = "Offline";
      if (str4 == "00:00:00:00")
        str4 = "Offline";
      this.notifyIcon1.BalloonTipText = ".: Server Uptime :." + Environment.NewLine + str1 + " - World" + Environment.NewLine + str2 + " - Database" + Environment.NewLine + str3 + " - Auth" + Environment.NewLine + str4 + " - Web" + Environment.NewLine;
      this.notifyIcon1.ShowBalloonTip(0);
    }

    private void saveLogToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Tools.SaveLogFile(this.log);
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Show();
      this.notifyIcon1.Visible = false;
      this.WindowState = FormWindowState.Normal;
      this.Activate();
    }

    private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
    {
    }

    private void sqlpic_Click(object sender, EventArgs e)
    {
      try
      {
        string str = string.Format(".");
        new Process()
        {
          StartInfo = {
            WorkingDirectory = str,
            FileName = "start_sql.bat",
            CreateNoWindow = false
          }
        }.Start();
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] Loaded SQL Tool " + Environment.NewLine;
      }
      catch (Exception ex)
      {
        RichTextBox log = this.log;
        log.Text = log.Text + "[" + DateTime.Now.ToString() + "] start_sql.bat not found!" + Environment.NewLine;
        int num = (int) MessageBox.Show("start_sql.bat not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager resources = new ComponentResourceManager(typeof (Main));
      this.wpanel = new Panel();
      this.pictureBox6 = new PictureBox();
      this.wckb = new CheckBox();
      this.wtime = new Label();
      this.wflag = new PictureBox();
      this.wbtn = new PictureBox();
      this.groupBox1 = new GroupBox();
      this.groupBox2 = new GroupBox();
      this.panel1 = new Panel();
      this.pictureBox5 = new PictureBox();
      this.lckb = new CheckBox();
      this.ltime = new Label();
      this.lflag = new PictureBox();
      this.lbtn = new PictureBox();
      this.groupBox3 = new GroupBox();
      this.panel2 = new Panel();
      this.pictureBox4 = new PictureBox();
      this.mckb = new CheckBox();
      this.mtime = new Label();
      this.mflag = new PictureBox();
      this.mbtn = new PictureBox();
      this.groupBox4 = new GroupBox();
      this.panel3 = new Panel();
      this.pictureBox3 = new PictureBox();
      this.ackb = new CheckBox();
      this.atime = new Label();
      this.aflag = new PictureBox();
      this.abtn = new PictureBox();
      this.log = new RichTextBox();
      this.grplog = new GroupBox();
      this.pictureBox7 = new PictureBox();
      this.tt = new ToolTip(this.components);
      this.btnlog = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.pb_autosave = new PictureBox();
      this.pb_hideproc = new PictureBox();
      this.btnsettings = new PictureBox();
      this.btnsave = new PictureBox();
      this.ctrlBtn = new PictureBox();
      this.pictureBox2 = new PictureBox();
      this.label1 = new Label();
      this.pictureBox8 = new PictureBox();
      this.sqlpic = new PictureBox();
      this.ckbhide = new CheckBox();
      this.timer = new Timer(this.components);
      this.ckbsave = new CheckBox();
      this.notifyIcon1 = new NotifyIcon(this.components);
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.saveLogToolStripMenuItem = new ToolStripMenuItem();
      this.openToolStripMenuItem = new ToolStripMenuItem();
      this.wpanel.SuspendLayout();
      ((ISupportInitialize) this.pictureBox6).BeginInit();
      ((ISupportInitialize) this.wflag).BeginInit();
      ((ISupportInitialize) this.wbtn).BeginInit();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox5).BeginInit();
      ((ISupportInitialize) this.lflag).BeginInit();
      ((ISupportInitialize) this.lbtn).BeginInit();
      this.groupBox3.SuspendLayout();
      this.panel2.SuspendLayout();
      ((ISupportInitialize) this.pictureBox4).BeginInit();
      ((ISupportInitialize) this.mflag).BeginInit();
      ((ISupportInitialize) this.mbtn).BeginInit();
      this.groupBox4.SuspendLayout();
      this.panel3.SuspendLayout();
      ((ISupportInitialize) this.pictureBox3).BeginInit();
      ((ISupportInitialize) this.aflag).BeginInit();
      ((ISupportInitialize) this.abtn).BeginInit();
      this.grplog.SuspendLayout();
      ((ISupportInitialize) this.pictureBox7).BeginInit();
      ((ISupportInitialize) this.btnlog).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.pb_autosave).BeginInit();
      ((ISupportInitialize) this.pb_hideproc).BeginInit();
      ((ISupportInitialize) this.btnsettings).BeginInit();
      ((ISupportInitialize) this.btnsave).BeginInit();
      ((ISupportInitialize) this.ctrlBtn).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      ((ISupportInitialize) this.pictureBox8).BeginInit();
      ((ISupportInitialize) this.sqlpic).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.wpanel.Controls.Add((Control) this.pictureBox6);
      this.wpanel.Controls.Add((Control) this.wckb);
      this.wpanel.Controls.Add((Control) this.wtime);
      this.wpanel.Controls.Add((Control) this.wflag);
      this.wpanel.Controls.Add((Control) this.wbtn);
      this.wpanel.ForeColor = Color.ForestGreen;
      resources.ApplyResources((object) this.wpanel, "wpanel");
      this.wpanel.Name = "wpanel";
      resources.ApplyResources((object) this.pictureBox6, "pictureBox6");
      this.pictureBox6.Name = "pictureBox6";
      this.pictureBox6.TabStop = false;
      resources.ApplyResources((object) this.wckb, "wckb");
      this.wckb.ForeColor = Color.Crimson;
      this.wckb.Name = "wckb";
      this.wckb.UseVisualStyleBackColor = true;
      this.wtime.ForeColor = Color.Crimson;
      resources.ApplyResources((object) this.wtime, "wtime");
      this.wtime.Name = "wtime";
      resources.ApplyResources((object) this.wflag, "wflag");
      this.wflag.Name = "wflag";
      this.wflag.TabStop = false;
      this.tt.SetToolTip((Control) this.wflag, resources.GetString("wflag.ToolTip"));
      this.wflag.Click += new EventHandler(this.wflag_Click);
      this.wbtn.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.wbtn, "wbtn");
      this.wbtn.Name = "wbtn";
      this.wbtn.TabStop = false;
      this.groupBox1.Controls.Add((Control) this.wpanel);
      resources.ApplyResources((object) this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      this.groupBox2.Controls.Add((Control) this.panel1);
      resources.ApplyResources((object) this.groupBox2, "groupBox2");
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      this.panel1.Controls.Add((Control) this.pictureBox5);
      this.panel1.Controls.Add((Control) this.lckb);
      this.panel1.Controls.Add((Control) this.ltime);
      this.panel1.Controls.Add((Control) this.lflag);
      this.panel1.Controls.Add((Control) this.lbtn);
      this.panel1.ForeColor = Color.DarkOrange;
      resources.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      resources.ApplyResources((object) this.pictureBox5, "pictureBox5");
      this.pictureBox5.Name = "pictureBox5";
      this.pictureBox5.TabStop = false;
      resources.ApplyResources((object) this.lckb, "lckb");
      this.lckb.ForeColor = Color.DarkGreen;
      this.lckb.Name = "lckb";
      this.lckb.UseVisualStyleBackColor = true;
      this.ltime.ForeColor = Color.DarkGreen;
      resources.ApplyResources((object) this.ltime, "ltime");
      this.ltime.Name = "ltime";
      resources.ApplyResources((object) this.lflag, "lflag");
      this.lflag.Name = "lflag";
      this.lflag.TabStop = false;
      this.tt.SetToolTip((Control) this.lflag, resources.GetString("lflag.ToolTip"));
      this.lflag.Click += new EventHandler(this.lflag_Click);
      this.lbtn.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.lbtn, "lbtn");
      this.lbtn.Name = "lbtn";
      this.lbtn.TabStop = false;
      this.groupBox3.Controls.Add((Control) this.panel2);
      resources.ApplyResources((object) this.groupBox3, "groupBox3");
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.TabStop = false;
      this.panel2.Controls.Add((Control) this.pictureBox4);
      this.panel2.Controls.Add((Control) this.mckb);
      this.panel2.Controls.Add((Control) this.mtime);
      this.panel2.Controls.Add((Control) this.mflag);
      this.panel2.Controls.Add((Control) this.mbtn);
      this.panel2.ForeColor = Color.Teal;
      resources.ApplyResources((object) this.panel2, "panel2");
      this.panel2.Name = "panel2";
      resources.ApplyResources((object) this.pictureBox4, "pictureBox4");
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.TabStop = false;
      resources.ApplyResources((object) this.mckb, "mckb");
      this.mckb.ForeColor = Color.RoyalBlue;
      this.mckb.Name = "mckb";
      this.mckb.UseVisualStyleBackColor = true;
      this.mtime.ForeColor = Color.RoyalBlue;
      resources.ApplyResources((object) this.mtime, "mtime");
      this.mtime.Name = "mtime";
      resources.ApplyResources((object) this.mflag, "mflag");
      this.mflag.Name = "mflag";
      this.mflag.TabStop = false;
      this.tt.SetToolTip((Control) this.mflag, resources.GetString("mflag.ToolTip"));
      this.mflag.Click += new EventHandler(this.mflag_Click);
      this.mbtn.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.mbtn, "mbtn");
      this.mbtn.Name = "mbtn";
      this.mbtn.TabStop = false;
      this.groupBox4.Controls.Add((Control) this.panel3);
      resources.ApplyResources((object) this.groupBox4, "groupBox4");
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.TabStop = false;
      this.panel3.Controls.Add((Control) this.pictureBox3);
      this.panel3.Controls.Add((Control) this.ackb);
      this.panel3.Controls.Add((Control) this.atime);
      this.panel3.Controls.Add((Control) this.aflag);
      this.panel3.Controls.Add((Control) this.abtn);
      this.panel3.ForeColor = Color.DarkOrchid;
      resources.ApplyResources((object) this.panel3, "panel3");
      this.panel3.Name = "panel3";
      resources.ApplyResources((object) this.pictureBox3, "pictureBox3");
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.TabStop = false;
      resources.ApplyResources((object) this.ackb, "ackb");
      this.ackb.ForeColor = Color.Black;
      this.ackb.Name = "ackb";
      this.ackb.UseVisualStyleBackColor = true;
      this.atime.ForeColor = Color.Black;
      resources.ApplyResources((object) this.atime, "atime");
      this.atime.Name = "atime";
      resources.ApplyResources((object) this.aflag, "aflag");
      this.aflag.Name = "aflag";
      this.aflag.TabStop = false;
      this.tt.SetToolTip((Control) this.aflag, resources.GetString("aflag.ToolTip"));
      this.aflag.Click += new EventHandler(this.aflag_Click);
      this.abtn.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.abtn, "abtn");
      this.abtn.Name = "abtn";
      this.abtn.TabStop = false;
      resources.ApplyResources((object) this.log, "log");
      this.log.Cursor = Cursors.IBeam;
      this.log.Name = "log";
      this.log.ReadOnly = true;
      this.log.TextChanged += new EventHandler(this.log_TextChanged);
      this.grplog.Controls.Add((Control) this.pictureBox7);
      this.grplog.Controls.Add((Control) this.log);
      resources.ApplyResources((object) this.grplog, "grplog");
      this.grplog.Name = "grplog";
      this.grplog.TabStop = false;
      resources.ApplyResources((object) this.pictureBox7, "pictureBox7");
      this.pictureBox7.Name = "pictureBox7";
      this.pictureBox7.TabStop = false;
      this.btnlog.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.btnlog, "btnlog");
      this.btnlog.Name = "btnlog";
      this.btnlog.TabStop = false;
      this.tt.SetToolTip((Control) this.btnlog, resources.GetString("btnlog.ToolTip"));
      this.btnlog.Click += new EventHandler(this.btnlog_Click);
      this.pictureBox1.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.pictureBox1, "pictureBox1");
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.TabStop = false;
      this.tt.SetToolTip((Control) this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
      this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
      this.pb_autosave.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.pb_autosave, "pb_autosave");
      this.pb_autosave.Name = "pb_autosave";
      this.pb_autosave.TabStop = false;
      this.tt.SetToolTip((Control) this.pb_autosave, resources.GetString("pb_autosave.ToolTip"));
      this.pb_hideproc.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.pb_hideproc, "pb_hideproc");
      this.pb_hideproc.Name = "pb_hideproc";
      this.pb_hideproc.TabStop = false;
      this.tt.SetToolTip((Control) this.pb_hideproc, resources.GetString("pb_hideproc.ToolTip"));
      this.btnsettings.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.btnsettings, "btnsettings");
      this.btnsettings.Name = "btnsettings";
      this.btnsettings.TabStop = false;
      this.tt.SetToolTip((Control) this.btnsettings, resources.GetString("btnsettings.ToolTip"));
      this.btnsettings.Click += new EventHandler(this.btnsettings_Click);
      this.btnsave.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.btnsave, "btnsave");
      this.btnsave.Name = "btnsave";
      this.btnsave.TabStop = false;
      this.tt.SetToolTip((Control) this.btnsave, resources.GetString("btnsave.ToolTip"));
      this.btnsave.Click += new EventHandler(this.btnsave_Click);
      this.ctrlBtn.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.ctrlBtn, "ctrlBtn");
      this.ctrlBtn.Name = "ctrlBtn";
      this.ctrlBtn.TabStop = false;
      this.tt.SetToolTip((Control) this.ctrlBtn, resources.GetString("ctrlBtn.ToolTip"));
      this.ctrlBtn.Click += new EventHandler(this.ctrlBtn_Click);
      resources.ApplyResources((object) this.pictureBox2, "pictureBox2");
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.TabStop = false;
      this.tt.SetToolTip((Control) this.pictureBox2, resources.GetString("pictureBox2.ToolTip"));
      this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
      resources.ApplyResources((object) this.label1, "label1");
      this.label1.ForeColor = Color.Green;
      this.label1.Name = "label1";
      this.tt.SetToolTip((Control) this.label1, resources.GetString("label1.ToolTip"));
      this.pictureBox8.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.pictureBox8, "pictureBox8");
      this.pictureBox8.Name = "pictureBox8";
      this.pictureBox8.TabStop = false;
      this.tt.SetToolTip((Control) this.pictureBox8, resources.GetString("pictureBox8.ToolTip"));
      this.sqlpic.Cursor = Cursors.Hand;
      resources.ApplyResources((object) this.sqlpic, "sqlpic");
      this.sqlpic.Name = "sqlpic";
      this.sqlpic.TabStop = false;
      this.tt.SetToolTip((Control) this.sqlpic, resources.GetString("sqlpic.ToolTip"));
      this.sqlpic.Click += new EventHandler(this.sqlpic_Click);
      resources.ApplyResources((object) this.ckbhide, "ckbhide");
      this.ckbhide.Name = "ckbhide";
      this.ckbhide.UseVisualStyleBackColor = true;
      this.ckbhide.CheckedChanged += new EventHandler(this.ckbhide_CheckedChanged);
      this.timer.Interval = 5000;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      resources.ApplyResources((object) this.ckbsave, "ckbsave");
      this.ckbsave.Name = "ckbsave";
      this.ckbsave.UseVisualStyleBackColor = true;
      this.ckbsave.CheckedChanged += new EventHandler(this.ckbsave_CheckedChanged);
      this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
      resources.ApplyResources((object) this.notifyIcon1, "notifyIcon1");
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.saveLogToolStripMenuItem,
        (ToolStripItem) this.openToolStripMenuItem
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      resources.ApplyResources((object) this.contextMenuStrip1, "contextMenuStrip1");
      this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
      resources.ApplyResources((object) this.saveLogToolStripMenuItem, "saveLogToolStripMenuItem");
      this.saveLogToolStripMenuItem.Click += new EventHandler(this.saveLogToolStripMenuItem_Click);
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      resources.ApplyResources((object) this.openToolStripMenuItem, "openToolStripMenuItem");
      this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
      resources.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.sqlpic);
      this.Controls.Add((Control) this.pictureBox8);
      this.Controls.Add((Control) this.ctrlBtn);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.btnsave);
      this.Controls.Add((Control) this.btnsettings);
      this.Controls.Add((Control) this.ckbsave);
      this.Controls.Add((Control) this.ckbhide);
      this.Controls.Add((Control) this.pb_hideproc);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.pb_autosave);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.grplog);
      this.Controls.Add((Control) this.btnlog);
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Main";
      this.FormClosing += new FormClosingEventHandler(this.Main_FormClosing);
      this.Load += new EventHandler(this.Main_Load);
      this.wpanel.ResumeLayout(false);
      this.wpanel.PerformLayout();
      ((ISupportInitialize) this.pictureBox6).EndInit();
      ((ISupportInitialize) this.wflag).EndInit();
      ((ISupportInitialize) this.wbtn).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      ((ISupportInitialize) this.pictureBox5).EndInit();
      ((ISupportInitialize) this.lflag).EndInit();
      ((ISupportInitialize) this.lbtn).EndInit();
      this.groupBox3.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      ((ISupportInitialize) this.pictureBox4).EndInit();
      ((ISupportInitialize) this.mflag).EndInit();
      ((ISupportInitialize) this.mbtn).EndInit();
      this.groupBox4.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      ((ISupportInitialize) this.pictureBox3).EndInit();
      ((ISupportInitialize) this.aflag).EndInit();
      ((ISupportInitialize) this.abtn).EndInit();
      this.grplog.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox7).EndInit();
      ((ISupportInitialize) this.btnlog).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.pb_autosave).EndInit();
      ((ISupportInitialize) this.pb_hideproc).EndInit();
      ((ISupportInitialize) this.btnsettings).EndInit();
      ((ISupportInitialize) this.btnsave).EndInit();
      ((ISupportInitialize) this.ctrlBtn).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      ((ISupportInitialize) this.pictureBox8).EndInit();
      ((ISupportInitialize) this.sqlpic).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
