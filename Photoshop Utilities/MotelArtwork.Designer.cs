using System.Collections.Generic;

namespace PhotoshopUtilities
{
	partial class MotelArtwork
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnGetTextLayersInfo = new System.Windows.Forms.Button();
			this.theTabControl = new System.Windows.Forms.TabControl();
			this.tabFiles = new System.Windows.Forms.TabPage();
			this.theFilesListView = new MyControls.FilesListView();
			this.tabTitles = new System.Windows.Forms.TabPage();
			this.stTitlesFiles = new System.Windows.Forms.Label();
			this.chkTitlesPerFile = new System.Windows.Forms.CheckBox();
			this.cbTitlesFiles = new System.Windows.Forms.ComboBox();
			this.tabPromoBlurb = new System.Windows.Forms.TabPage();
			this.tabWebsite = new System.Windows.Forms.TabPage();
			this.btnProcess = new System.Windows.Forms.Button();
			this.tiTitle = new Photoshopper.TextItemCtrl();
			this.tiPromoBlurb = new Photoshopper.TextItemCtrl();
			this.tiWebsite = new Photoshopper.TextItemCtrl();
			this.theTabControl.SuspendLayout();
			this.tabFiles.SuspendLayout();
			this.tabTitles.SuspendLayout();
			this.tabPromoBlurb.SuspendLayout();
			this.tabWebsite.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnGetTextLayersInfo
			// 
			this.btnGetTextLayersInfo.Location = new System.Drawing.Point(38, 424);
			this.btnGetTextLayersInfo.Name = "btnGetTextLayersInfo";
			this.btnGetTextLayersInfo.Size = new System.Drawing.Size(75, 23);
			this.btnGetTextLayersInfo.TabIndex = 0;
			this.btnGetTextLayersInfo.Text = "Info";
			this.btnGetTextLayersInfo.UseVisualStyleBackColor = true;
			this.btnGetTextLayersInfo.Click += new System.EventHandler(this.Test_Click);
			// 
			// theTabControl
			// 
			this.theTabControl.Controls.Add(this.tabFiles);
			this.theTabControl.Controls.Add(this.tabTitles);
			this.theTabControl.Controls.Add(this.tabPromoBlurb);
			this.theTabControl.Controls.Add(this.tabWebsite);
			this.theTabControl.Location = new System.Drawing.Point(8, 8);
			this.theTabControl.Name = "theTabControl";
			this.theTabControl.SelectedIndex = 0;
			this.theTabControl.Size = new System.Drawing.Size(479, 396);
			this.theTabControl.TabIndex = 1;
			// 
			// tabFiles
			// 
			this.tabFiles.Controls.Add(this.theFilesListView);
			this.tabFiles.Location = new System.Drawing.Point(4, 22);
			this.tabFiles.Name = "tabFiles";
			this.tabFiles.Padding = new System.Windows.Forms.Padding(3);
			this.tabFiles.Size = new System.Drawing.Size(471, 370);
			this.tabFiles.TabIndex = 0;
			this.tabFiles.Text = "Files";
			this.tabFiles.UseVisualStyleBackColor = true;
			// 
			// theFilesListView
			// 
			this.theFilesListView.FilesFilter = "Image Files(*.TIF;*.PSD;*.PSB;*.JPG;*.BMP;*.PNG)|*.TIF;*.PSD;*.PSB;*.JPG;*.BMP|Ph" +
				 "otoshop Files (*PSD;*.PSB)|*.PSD;*.PSB|TIFF Files (*.TIF)|*.TIF|JPEG Files (*.JP" +
				 "G)|*.JPG|All files (*.*)|*.*";
			this.theFilesListView.InitialDirectory = "c:\\";
			this.theFilesListView.Location = new System.Drawing.Point(23, 19);
			this.theFilesListView.MultiSelection = true;
			this.theFilesListView.Name = "theFilesListView";
			this.theFilesListView.Size = new System.Drawing.Size(420, 200);
			this.theFilesListView.TabIndex = 0;
			this.theFilesListView.Title = "Image Files";
			// 
			// tabTitles
			// 
			this.tabTitles.Controls.Add(this.stTitlesFiles);
			this.tabTitles.Controls.Add(this.chkTitlesPerFile);
			this.tabTitles.Controls.Add(this.cbTitlesFiles);
			this.tabTitles.Controls.Add(this.tiTitle);
			this.tabTitles.Location = new System.Drawing.Point(4, 22);
			this.tabTitles.Name = "tabTitles";
			this.tabTitles.Padding = new System.Windows.Forms.Padding(3);
			this.tabTitles.Size = new System.Drawing.Size(471, 370);
			this.tabTitles.TabIndex = 1;
			this.tabTitles.Text = "Titles";
			this.tabTitles.UseVisualStyleBackColor = true;
			this.tabTitles.Enter += new System.EventHandler(this.tabTitles_Enter);
			this.tabTitles.Leave += new System.EventHandler(this.tabTitles_Leave);
			// 
			// stTitlesFiles
			// 
			this.stTitlesFiles.AutoSize = true;
			this.stTitlesFiles.Location = new System.Drawing.Point(108, 27);
			this.stTitlesFiles.Name = "stTitlesFiles";
			this.stTitlesFiles.Size = new System.Drawing.Size(28, 13);
			this.stTitlesFiles.TabIndex = 6;
			this.stTitlesFiles.Text = "Files";
			this.stTitlesFiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chkTitlesPerFile
			// 
			this.chkTitlesPerFile.AutoSize = true;
			this.chkTitlesPerFile.Location = new System.Drawing.Point(26, 26);
			this.chkTitlesPerFile.Name = "chkTitlesPerFile";
			this.chkTitlesPerFile.Size = new System.Drawing.Size(61, 17);
			this.chkTitlesPerFile.TabIndex = 5;
			this.chkTitlesPerFile.Text = "Per File";
			this.chkTitlesPerFile.UseVisualStyleBackColor = true;
			this.chkTitlesPerFile.CheckedChanged += new System.EventHandler(this.chkTitlesPerFile_CheckedChanged);
			// 
			// cbTitlesFiles
			// 
			this.cbTitlesFiles.FormattingEnabled = true;
			this.cbTitlesFiles.Location = new System.Drawing.Point(142, 24);
			this.cbTitlesFiles.Name = "cbTitlesFiles";
			this.cbTitlesFiles.Size = new System.Drawing.Size(316, 21);
			this.cbTitlesFiles.TabIndex = 4;
			this.cbTitlesFiles.SelectedIndexChanged += new System.EventHandler(this.chkTitlesPerFile_SelectedIndexChanged);
			// 
			// tabPromoBlurb
			// 
			this.tabPromoBlurb.Controls.Add(this.tiPromoBlurb);
			this.tabPromoBlurb.Location = new System.Drawing.Point(4, 22);
			this.tabPromoBlurb.Name = "tabPromoBlurb";
			this.tabPromoBlurb.Size = new System.Drawing.Size(471, 370);
			this.tabPromoBlurb.TabIndex = 3;
			this.tabPromoBlurb.Text = "Promo Blurb";
			this.tabPromoBlurb.UseVisualStyleBackColor = true;
			// 
			// tabWebsite
			// 
			this.tabWebsite.Controls.Add(this.tiWebsite);
			this.tabWebsite.Location = new System.Drawing.Point(4, 22);
			this.tabWebsite.Name = "tabWebsite";
			this.tabWebsite.Size = new System.Drawing.Size(471, 370);
			this.tabWebsite.TabIndex = 2;
			this.tabWebsite.Text = "Website";
			this.tabWebsite.UseVisualStyleBackColor = true;
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point(374, 424);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(75, 23);
			this.btnProcess.TabIndex = 3;
			this.btnProcess.Text = "Process";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// tiTitle
			// 
			this.tiTitle.Contents = "";
			this.tiTitle.Location = new System.Drawing.Point(8, 62);
			this.tiTitle.Name = "tiTitle";
			this.tiTitle.Size = new System.Drawing.Size(450, 270);
			this.tiTitle.Style = "";
			this.tiTitle.TabIndex = 3;
			// 
			// tiPromoBlurb
			// 
			this.tiPromoBlurb.Contents = "";
			this.tiPromoBlurb.Location = new System.Drawing.Point(8, 8);
			this.tiPromoBlurb.Name = "tiPromoBlurb";
			this.tiPromoBlurb.Size = new System.Drawing.Size(450, 270);
			this.tiPromoBlurb.Style = "";
			this.tiPromoBlurb.TabIndex = 4;
			// 
			// tiWebsite
			// 
			this.tiWebsite.Contents = "";
			this.tiWebsite.Location = new System.Drawing.Point(8, 8);
			this.tiWebsite.Name = "tiWebsite";
			this.tiWebsite.Size = new System.Drawing.Size(450, 270);
			this.tiWebsite.Style = "";
			this.tiWebsite.TabIndex = 4;
			// 
			// MotelArtwork
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 529);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.theTabControl);
			this.Controls.Add(this.btnGetTextLayersInfo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MotelArtwork";
			this.Text = "Motel Artwork";
			this.Load += new System.EventHandler(this.MotelArtwork_Load);
			this.theTabControl.ResumeLayout(false);
			this.tabFiles.ResumeLayout(false);
			this.tabTitles.ResumeLayout(false);
			this.tabTitles.PerformLayout();
			this.tabPromoBlurb.ResumeLayout(false);
			this.tabWebsite.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.Button btnGetTextLayersInfo;
		public Photoshopper.TextItemCtrl tiTitle;
		public Photoshopper.TextItemCtrl tiPromoBlurb;
		public Photoshopper.TextItemCtrl tiWebsite;
		public System.Windows.Forms.TabControl theTabControl;
		public System.Windows.Forms.TabPage tabFiles;
		public System.Windows.Forms.TabPage tabTitles;
		public MyControls.FilesListView theFilesListView;
		public System.Windows.Forms.TabPage tabPromoBlurb;
		public System.Windows.Forms.TabPage tabWebsite;
		public System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.ComboBox cbTitlesFiles;
		private System.Windows.Forms.Label stTitlesFiles;
		private System.Windows.Forms.CheckBox chkTitlesPerFile;
	}
}