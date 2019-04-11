// Decompiled with JetBrains decompiler
// Type: StygianCoreControls.Settings
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
  public class Settings : Form
  {
    public Block block;
    private IContainer components;
    private Label label1;
    private TextBox path;
    private PictureBox btnbrowse;
    private TextBox procname;
    private Label label2;
    private OpenFileDialog ofd;

    public Settings()
    {
      this.InitializeComponent();
    }

    public void Form2_Load(object sender, EventArgs e)
    {
      this.Text = this.block.name + " Settings";
      this.procname.Text = this.block.procname;
      this.path.Text = this.block.path;
    }

    public void SetBlock(ref Block b)
    {
      this.block = b;
    }

    private void Settings_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.block.path = this.path.Text;
      this.block.procname = this.procname.Text;
      if (File.Exists(this.block.path))
        this.block.write("Path Not Specified!", false);
      if (!(this.block.procname != ""))
        return;
      this.block.write("Process Not Specified!", false);
    }

    private void btnbrowse_Click(object sender, EventArgs e)
    {
      int num = (int) this.ofd.ShowDialog();
      this.path.Text = this.ofd.FileName;
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
      this.path = new TextBox();
      this.procname = new TextBox();
      this.label2 = new Label();
      this.ofd = new OpenFileDialog();
      this.btnbrowse = new PictureBox();
      ((ISupportInitialize) this.btnbrowse).BeginInit();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(8, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(41, 17);
      this.label1.TabIndex = 0;
      this.label1.Text = "Path:";
      this.path.Location = new Point(11, 29);
      this.path.Name = "path";
      this.path.Size = new Size(133, 23);
      this.path.TabIndex = 1;
      this.procname.Location = new Point(11, 75);
      this.procname.Name = "procname";
      this.procname.Size = new Size(154, 23);
      this.procname.TabIndex = 4;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(8, 55);
      this.label2.Name = "label2";
      this.label2.Size = new Size(104, 17);
      this.label2.TabIndex = 3;
      this.label2.Text = "Process Name:";
      this.ofd.Filter = "All files|*.*";
      this.btnbrowse.Cursor = Cursors.Hand;
      this.btnbrowse.Image = (Image) Resources.folder_search;
      this.btnbrowse.Location = new Point(149, 34);
      this.btnbrowse.Name = "btnbrowse";
      this.btnbrowse.Size = new Size(16, 16);
      this.btnbrowse.TabIndex = 2;
      this.btnbrowse.TabStop = false;
      this.btnbrowse.Click += new EventHandler(this.btnbrowse_Click);
      this.AutoScaleDimensions = new SizeF(8f, 17f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(175, 107);
      this.Controls.Add((Control) this.procname);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.btnbrowse);
      this.Controls.Add((Control) this.path);
      this.Controls.Add((Control) this.label1);
      this.Font = new Font("Microsoft Sans Serif", 10.25f);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Margin = new Padding(4);
      this.Name = "Settings";
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Settings";
      this.FormClosing += new FormClosingEventHandler(this.Settings_FormClosing);
      this.Load += new EventHandler(this.Form2_Load);
      ((ISupportInitialize) this.btnbrowse).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
