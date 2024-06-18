using System.Collections.Generic;

namespace PhotoshopUtilities
{
	partial class FileChecker
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileChecker));
			this.btnRunCancel = new System.Windows.Forms.Button();
			this.lbBadFiles = new System.Windows.Forms.ListBox();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.theStatusStrip = new System.Windows.Forms.StatusStrip();
			this.theStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.theProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.lblBadFiles = new System.Windows.Forms.Label();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.theFilesGatherer = new MyControls.FilesGatherer();
			this.theStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnRunCancel
			// 
			this.btnRunCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRunCancel.Location = new System.Drawing.Point(455, 36);
			this.btnRunCancel.Name = "btnRunCancel";
			this.btnRunCancel.Size = new System.Drawing.Size(75, 23);
			this.btnRunCancel.TabIndex = 2;
			this.btnRunCancel.Text = "Check Files";
			this.btnRunCancel.UseVisualStyleBackColor = true;
			this.btnRunCancel.Click += new System.EventHandler(this.btnCheckFiles_Click);
			// 
			// lbBadFiles
			// 
			this.lbBadFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbBadFiles.FormattingEnabled = true;
			this.lbBadFiles.Location = new System.Drawing.Point(12, 348);
			this.lbBadFiles.Name = "lbBadFiles";
			this.lbBadFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbBadFiles.Size = new System.Drawing.Size(528, 121);
			this.lbBadFiles.TabIndex = 4;
			this.lbBadFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.BadFilesListBox_KeyUp);
			// 
			// theStatusStrip
			// 
			this.theStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.theStatusStripLabel,
            this.theProgressBar});
			this.theStatusStrip.Location = new System.Drawing.Point(0, 484);
			this.theStatusStrip.Name = "theStatusStrip";
			this.theStatusStrip.Size = new System.Drawing.Size(556, 22);
			this.theStatusStrip.TabIndex = 7;
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
			// lblBadFiles
			// 
			this.lblBadFiles.AutoSize = true;
			this.lblBadFiles.Location = new System.Drawing.Point(9, 331);
			this.lblBadFiles.Name = "lblBadFiles";
			this.lblBadFiles.Size = new System.Drawing.Size(50, 13);
			this.lblBadFiles.TabIndex = 8;
			this.lblBadFiles.Text = "Bad Files";
			// 
			// btnPauseResume
			// 
			this.btnPauseResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPauseResume.Location = new System.Drawing.Point(455, 75);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.Size = new System.Drawing.Size(75, 23);
			this.btnPauseResume.TabIndex = 9;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.UseVisualStyleBackColor = true;
			this.btnPauseResume.Visible = false;
			this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Enabled = false;
			this.btnCopy.Location = new System.Drawing.Point(455, 319);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 10;
			this.btnCopy.Text = "Copy";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// theFilesGatherer
			// 
			this.theFilesGatherer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.theFilesGatherer.FilesFilter = resources.GetString("theFilesGatherer.FilesFilter");
			this.theFilesGatherer.InitialDirectory = "F:\\Digital Files";
			this.theFilesGatherer.Label = "Files";
			this.theFilesGatherer.Location = new System.Drawing.Point(12, 12);
			this.theFilesGatherer.MultiSelection = false;
			this.theFilesGatherer.Name = "theFilesGatherer";
			this.theFilesGatherer.RootDirectory = "";
			this.theFilesGatherer.Size = new System.Drawing.Size(420, 300);
			this.theFilesGatherer.Sorting = MyControls.FilesListView.SortDirection.Asc;
			this.theFilesGatherer.TabIndex = 3;
			this.theFilesGatherer.Title = "Image Files";
			this.theFilesGatherer.ValidFileTypes = ((System.Collections.Generic.List<string>)(resources.GetObject("theFilesGatherer.ValidFileTypes")));
			// 
			// FileChecker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(556, 506);
			this.ControlBox = false;
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnPauseResume);
			this.Controls.Add(this.lblBadFiles);
			this.Controls.Add(this.theStatusStrip);
			this.Controls.Add(this.lbBadFiles);
			this.Controls.Add(this.theFilesGatherer);
			this.Controls.Add(this.btnRunCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "FileChecker";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "NEF File Checker";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Resize += new System.EventHandler(this.FileChecker_Resize);
			this.theStatusStrip.ResumeLayout(false);
			this.theStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRunCancel;
		private MyControls.FilesGatherer theFilesGatherer;
		private System.Windows.Forms.ListBox lbBadFiles;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.StatusStrip theStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel theStatusStripLabel;
		private System.Windows.Forms.ToolStripProgressBar theProgressBar;
		private System.Windows.Forms.Label lblBadFiles;
		private System.Windows.Forms.Button btnPauseResume;
		private System.Windows.Forms.Button btnCopy;
	}
}