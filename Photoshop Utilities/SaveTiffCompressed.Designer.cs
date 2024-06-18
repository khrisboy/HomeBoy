namespace PhotoshopUtilities
{
	partial class SaveTiffCompressed
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
			if ( disposing && (components != null) )
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveTiffCompressed));
			this.theFilesListView = new MyControls.FilesListView();
			this.theBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.btnPauseResume = new System.Windows.Forms.Button();
			this.btnRunCancel = new System.Windows.Forms.Button();
			this.theStatusStrip = new System.Windows.Forms.StatusStrip();
			this.theStatusStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.theProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.labelProcessingFile = new System.Windows.Forms.Label();
			this.theStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// theFilesListView
			// 
			this.theFilesListView.FilesFilter = "TIFF Files (*.TIF, *.tiff)|*.tif;*.tiff";
			this.theFilesListView.InitialDirectory = "c:\\";
			this.theFilesListView.Label = "Files";
			this.theFilesListView.Location = new System.Drawing.Point(12, 12);
			this.theFilesListView.MultiSelection = false;
			this.theFilesListView.Name = "theFilesListView";
			this.theFilesListView.Size = new System.Drawing.Size(420, 200);
			this.theFilesListView.Sorting = MyControls.FilesListView.SortDirection.Asc;
			this.theFilesListView.TabIndex = 0;
			this.theFilesListView.Title = "TIFF Files";
			this.theFilesListView.ValidFileTypes = ((System.Collections.Generic.List<string>)(resources.GetObject("theFilesListView.ValidFileTypes")));
			// 
			// btnPauseResume
			// 
			this.btnPauseResume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPauseResume.Location = new System.Drawing.Point(175, 273);
			this.btnPauseResume.Name = "btnPauseResume";
			this.btnPauseResume.Size = new System.Drawing.Size(95, 23);
			this.btnPauseResume.TabIndex = 11;
			this.btnPauseResume.Text = "Pause";
			this.btnPauseResume.UseVisualStyleBackColor = true;
			this.btnPauseResume.Visible = false;
			this.btnPauseResume.Click += new System.EventHandler(this.btnPauseResume_Click);
			// 
			// btnRunCancel
			// 
			this.btnRunCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRunCancel.Location = new System.Drawing.Point(175, 231);
			this.btnRunCancel.Name = "btnRunCancel";
			this.btnRunCancel.Size = new System.Drawing.Size(95, 23);
			this.btnRunCancel.TabIndex = 10;
			this.btnRunCancel.Text = "Compress Files";
			this.btnRunCancel.UseVisualStyleBackColor = true;
			this.btnRunCancel.Click += new System.EventHandler(this.btnCompressFiles_Click);
			// 
			// theStatusStrip
			// 
			this.theStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.theStatusStripLabel,
            this.theProgressBar});
			this.theStatusStrip.Location = new System.Drawing.Point(0, 357);
			this.theStatusStrip.Name = "theStatusStrip";
			this.theStatusStrip.Size = new System.Drawing.Size(445, 22);
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
			// labelProcessingFile
			// 
			this.labelProcessingFile.AutoSize = true;
			this.labelProcessingFile.Location = new System.Drawing.Point(12, 333);
			this.labelProcessingFile.Name = "labelProcessingFile";
			this.labelProcessingFile.Size = new System.Drawing.Size(0, 13);
			this.labelProcessingFile.TabIndex = 12;
			// 
			// SaveTiffCompressed
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(445, 379);
			this.Controls.Add(this.labelProcessingFile);
			this.Controls.Add(this.theStatusStrip);
			this.Controls.Add(this.btnPauseResume);
			this.Controls.Add(this.btnRunCancel);
			this.Controls.Add(this.theFilesListView);
			this.Name = "SaveTiffCompressed";
			this.Text = "SaveTiffCompressed";
			this.theStatusStrip.ResumeLayout(false);
			this.theStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MyControls.FilesListView theFilesListView;
		private System.ComponentModel.BackgroundWorker theBackgroundWorker;
		private System.Windows.Forms.Button btnPauseResume;
		private System.Windows.Forms.Button btnRunCancel;
		private System.Windows.Forms.StatusStrip theStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel theStatusStripLabel;
		private System.Windows.Forms.ToolStripProgressBar theProgressBar;
		private System.Windows.Forms.Label labelProcessingFile;
	}
}