namespace PhotoshopUtilities
{
	partial class CompareFiles
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnRunCancel = new System.Windows.Forms.Button();
			this.lblDiffFiles = new System.Windows.Forms.Label();
			this.lbFilesNotInSource = new System.Windows.Forms.ListBox();
			this.browseForDirectory1 = new MyControls.BrowseForDirectory();
			this.browseForDirectory2 = new MyControls.BrowseForDirectory();
			this.chkIncludeSubdirectories = new System.Windows.Forms.CheckBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.theStatusStrip = new System.Windows.Forms.StatusStrip();
			this.theStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.theProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabNotInSource = new System.Windows.Forms.TabPage();
			this.btnDeleteNotInSource = new System.Windows.Forms.Button();
			this.tabFilesNotInCopy = new System.Windows.Forms.TabPage();
			this.lbFilesNotInCopy = new System.Windows.Forms.ListBox();
			this.tabFilesDifferent = new System.Windows.Forms.TabPage();
			this.lbFilesDifferent = new System.Windows.Forms.ListBox();
			this.theStatusStrip.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabNotInSource.SuspendLayout();
			this.tabFilesNotInCopy.SuspendLayout();
			this.tabFilesDifferent.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnRunCancel
			// 
			this.btnRunCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRunCancel.Location = new System.Drawing.Point(676, 412);
			this.btnRunCancel.Name = "btnRunCancel";
			this.btnRunCancel.Size = new System.Drawing.Size(75, 23);
			this.btnRunCancel.TabIndex = 0;
			this.btnRunCancel.Text = "Compare";
			this.btnRunCancel.UseVisualStyleBackColor = true;
			this.btnRunCancel.Click += new System.EventHandler(this.btnCompare_Click);
			// 
			// lblDiffFiles
			// 
			this.lblDiffFiles.AutoSize = true;
			this.lblDiffFiles.Location = new System.Drawing.Point(8, 90);
			this.lblDiffFiles.Name = "lblDiffFiles";
			this.lblDiffFiles.Size = new System.Drawing.Size(28, 13);
			this.lblDiffFiles.TabIndex = 10;
			this.lblDiffFiles.Text = "Diffs";
			// 
			// lbFilesNotInSource
			// 
			this.lbFilesNotInSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbFilesNotInSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbFilesNotInSource.FormattingEnabled = true;
			this.lbFilesNotInSource.Location = new System.Drawing.Point(6, 6);
			this.lbFilesNotInSource.Name = "lbFilesNotInSource";
			this.lbFilesNotInSource.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbFilesNotInSource.Size = new System.Drawing.Size(639, 251);
			this.lbFilesNotInSource.TabIndex = 9;
			this.lbFilesNotInSource.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbDiffFiles_DrawItem);
			// 
			// browseForDirectory1
			// 
			this.browseForDirectory1.BrowseLabel = "...";
			this.browseForDirectory1.Directory = "";
			this.browseForDirectory1.Label = "Source Directory 1";
			this.browseForDirectory1.Location = new System.Drawing.Point(6, 12);
			this.browseForDirectory1.Name = "browseForDirectory1";
			this.browseForDirectory1.Size = new System.Drawing.Size(350, 38);
			this.browseForDirectory1.TabIndex = 11;
			// 
			// browseForDirectory2
			// 
			this.browseForDirectory2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.browseForDirectory2.BrowseLabel = "...";
			this.browseForDirectory2.Directory = "";
			this.browseForDirectory2.Label = "Source Directory 2";
			this.browseForDirectory2.Location = new System.Drawing.Point(399, 12);
			this.browseForDirectory2.Name = "browseForDirectory2";
			this.browseForDirectory2.Size = new System.Drawing.Size(350, 38);
			this.browseForDirectory2.TabIndex = 12;
			// 
			// chkIncludeSubdirectories
			// 
			this.chkIncludeSubdirectories.AutoSize = true;
			this.chkIncludeSubdirectories.Location = new System.Drawing.Point(225, 56);
			this.chkIncludeSubdirectories.Name = "chkIncludeSubdirectories";
			this.chkIncludeSubdirectories.Size = new System.Drawing.Size(131, 17);
			this.chkIncludeSubdirectories.TabIndex = 21;
			this.chkIncludeSubdirectories.Text = "&Include Subdirectories";
			this.chkIncludeSubdirectories.UseVisualStyleBackColor = true;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// theStatusStrip
			// 
			this.theStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.theStatusStripLabel,
            this.theProgressBar,
            this.toolStripStatusLabel1});
			this.theStatusStrip.Location = new System.Drawing.Point(0, 448);
			this.theStatusStrip.Name = "theStatusStrip";
			this.theStatusStrip.Size = new System.Drawing.Size(764, 22);
			this.theStatusStrip.TabIndex = 22;
			this.theStatusStrip.Resize += new System.EventHandler(this.theStatusStrip_Resize);
			// 
			// theStatusStripLabel
			// 
			this.theStatusStripLabel.AutoSize = false;
			this.theStatusStripLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.theStatusStripLabel.Name = "theStatusStripLabel";
			this.theStatusStripLabel.Size = new System.Drawing.Size(260, 17);
			this.theStatusStripLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// theProgressBar
			// 
			this.theProgressBar.AutoSize = false;
			this.theProgressBar.ForeColor = System.Drawing.Color.MediumSpringGreen;
			this.theProgressBar.Name = "theProgressBar";
			this.theProgressBar.Size = new System.Drawing.Size(300, 16);
			this.theProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// btnPauseResume
			// 
			this.btnPauseResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPauseResume.Location = new System.Drawing.Point(584, 412);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.Size = new System.Drawing.Size(75, 23);
			this.btnPauseResume.TabIndex = 23;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.UseVisualStyleBackColor = true;
			this.btnPauseResume.Visible = false;
			this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabNotInSource);
			this.tabControl1.Controls.Add(this.tabFilesNotInCopy);
			this.tabControl1.Controls.Add(this.tabFilesDifferent);
			this.tabControl1.Location = new System.Drawing.Point(10, 108);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(743, 290);
			this.tabControl1.TabIndex = 24;
			// 
			// tabNotInSource
			// 
			this.tabNotInSource.Controls.Add(this.btnDeleteNotInSource);
			this.tabNotInSource.Controls.Add(this.lbFilesNotInSource);
			this.tabNotInSource.Location = new System.Drawing.Point(4, 22);
			this.tabNotInSource.Name = "tabNotInSource";
			this.tabNotInSource.Padding = new System.Windows.Forms.Padding(3);
			this.tabNotInSource.Size = new System.Drawing.Size(735, 264);
			this.tabNotInSource.TabIndex = 0;
			this.tabNotInSource.Text = "Not in Source";
			this.tabNotInSource.ToolTipText = "Files not in Source";
			this.tabNotInSource.UseVisualStyleBackColor = true;
			// 
			// btnDeleteNotInSource
			// 
			this.btnDeleteNotInSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDeleteNotInSource.Location = new System.Drawing.Point(651, 25);
			this.btnDeleteNotInSource.Name = "btnDeleteNotInSource";
			this.btnDeleteNotInSource.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteNotInSource.TabIndex = 10;
			this.btnDeleteNotInSource.Text = "Delete";
			this.btnDeleteNotInSource.UseVisualStyleBackColor = true;
			this.btnDeleteNotInSource.Click += new System.EventHandler(this.btnDeleteNotInSource_Click);
			// 
			// tabFilesNotInCopy
			// 
			this.tabFilesNotInCopy.Controls.Add(this.lbFilesNotInCopy);
			this.tabFilesNotInCopy.Location = new System.Drawing.Point(4, 22);
			this.tabFilesNotInCopy.Name = "tabFilesNotInCopy";
			this.tabFilesNotInCopy.Padding = new System.Windows.Forms.Padding(3);
			this.tabFilesNotInCopy.Size = new System.Drawing.Size(735, 264);
			this.tabFilesNotInCopy.TabIndex = 1;
			this.tabFilesNotInCopy.Text = "Not in Copy";
			this.tabFilesNotInCopy.UseVisualStyleBackColor = true;
			// 
			// lbFilesNotInCopy
			// 
			this.lbFilesNotInCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbFilesNotInCopy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbFilesNotInCopy.FormattingEnabled = true;
			this.lbFilesNotInCopy.Location = new System.Drawing.Point(6, 7);
			this.lbFilesNotInCopy.Name = "lbFilesNotInCopy";
			this.lbFilesNotInCopy.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbFilesNotInCopy.Size = new System.Drawing.Size(723, 251);
			this.lbFilesNotInCopy.TabIndex = 10;
			this.lbFilesNotInCopy.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbFilesNotInCopy_DrawItem);
			// 
			// tabFilesDifferent
			// 
			this.tabFilesDifferent.Controls.Add(this.lbFilesDifferent);
			this.tabFilesDifferent.Location = new System.Drawing.Point(4, 22);
			this.tabFilesDifferent.Name = "tabFilesDifferent";
			this.tabFilesDifferent.Size = new System.Drawing.Size(735, 264);
			this.tabFilesDifferent.TabIndex = 2;
			this.tabFilesDifferent.Text = "Files Different";
			this.tabFilesDifferent.UseVisualStyleBackColor = true;
			// 
			// lbFilesDifferent
			// 
			this.lbFilesDifferent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbFilesDifferent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lbFilesDifferent.FormattingEnabled = true;
			this.lbFilesDifferent.Location = new System.Drawing.Point(6, 7);
			this.lbFilesDifferent.Name = "lbFilesDifferent";
			this.lbFilesDifferent.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbFilesDifferent.Size = new System.Drawing.Size(723, 251);
			this.lbFilesDifferent.TabIndex = 10;
			this.lbFilesDifferent.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbFilesDifferent_DrawItem);
			// 
			// CompareFiles
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(764, 470);
			this.ControlBox = false;
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnPauseResume);
			this.Controls.Add(this.theStatusStrip);
			this.Controls.Add(this.chkIncludeSubdirectories);
			this.Controls.Add(this.browseForDirectory2);
			this.Controls.Add(this.browseForDirectory1);
			this.Controls.Add(this.lblDiffFiles);
			this.Controls.Add(this.btnRunCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(764, 470);
			this.Name = "CompareFiles";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.theStatusStrip.ResumeLayout(false);
			this.theStatusStrip.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabNotInSource.ResumeLayout(false);
			this.tabFilesNotInCopy.ResumeLayout(false);
			this.tabFilesDifferent.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRunCancel;
		private System.Windows.Forms.Label lblDiffFiles;
		private System.Windows.Forms.ListBox lbFilesNotInSource;
		private MyControls.BrowseForDirectory browseForDirectory1;
		private MyControls.BrowseForDirectory browseForDirectory2;
		protected System.Windows.Forms.CheckBox chkIncludeSubdirectories;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.StatusStrip theStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel theStatusStripLabel;
		private System.Windows.Forms.ToolStripProgressBar theProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Button btnPauseResume;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabNotInSource;
		private System.Windows.Forms.TabPage tabFilesNotInCopy;
		private System.Windows.Forms.TabPage tabFilesDifferent;
		private System.Windows.Forms.ListBox lbFilesNotInCopy;
		private System.Windows.Forms.ListBox lbFilesDifferent;
		private System.Windows.Forms.Button btnDeleteNotInSource;
	}
}
