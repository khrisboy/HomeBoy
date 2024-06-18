namespace PhotoshopUtilities
{
	partial class IconExtractor
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
			this.components = new System.ComponentModel.Container();
			this.lvIcons = new System.Windows.Forms.ListView();
			this.theImageList = new System.Windows.Forms.ImageList(this.components);
			this.grpSource = new MyControls.MyGroupBox();
			this.bfd1 = new MyControls.BrowseForDirectory();
			this.bff1 = new MyControls.BrowseForFile();
			this.btnExtract = new System.Windows.Forms.Button();
			this.stIcons = new System.Windows.Forms.Label();
			this.grpSave = new MyControls.MyGroupBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.bfdSave = new MyControls.BrowseForDirectory();
			this.grpSource.SuspendLayout();
			this.grpSave.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvIcons
			// 
			this.lvIcons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvIcons.Location = new System.Drawing.Point(347, 34);
			this.lvIcons.Name = "lvIcons";
			this.lvIcons.Size = new System.Drawing.Size(200, 208);
			this.lvIcons.TabIndex = 0;
			this.lvIcons.UseCompatibleStateImageBehavior = false;
			// 
			// theImageList
			// 
			this.theImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.theImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.theImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// grpSource
			// 
			this.grpSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSource.Controls.Add(this.bfd1);
			this.grpSource.Controls.Add(this.bff1);
			this.grpSource.Location = new System.Drawing.Point(16, 16);
			this.grpSource.Name = "grpSource";
			this.grpSource.Size = new System.Drawing.Size(309, 140);
			this.grpSource.TabIndex = 1;
			this.grpSource.TabStop = false;
			this.grpSource.Text = "Source";
			// 
			// bfd1
			// 
			this.bfd1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bfd1.BrowseLabel = "...";
			this.bfd1.Directory = "";
			this.bfd1.Label = "&Directory";
			this.bfd1.Location = new System.Drawing.Point(16, 16);
			this.bfd1.Name = "bfd1";
			this.bfd1.Size = new System.Drawing.Size(272, 38);
			this.bfd1.TabIndex = 2;
			this.bfd1.OnMyTextChanged += new MyControls.BrowseForObject.MyTextChanged(this.bfd1_OnMyTextChanged);
			// 
			// bff1
			// 
			this.bff1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bff1.BrowseLabel = "...";
			this.bff1.CheckFileExists = false;
			this.bff1.DefaultExtension = null;
			this.bff1.DisplayFileNameOnly = false;
			this.bff1.FileName = "";
			this.bff1.FilesFilter = "All files (*.*)|*.*";
			this.bff1.FilterIndex = 1;
			this.bff1.InitialDirectory = "c:\\";
			this.bff1.Label = "&File";
			this.bff1.Location = new System.Drawing.Point(16, 77);
			this.bff1.Name = "bff1";
			this.bff1.Size = new System.Drawing.Size(272, 38);
			this.bff1.TabIndex = 1;
			// 
			// btnExtract
			// 
			this.btnExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExtract.Location = new System.Drawing.Point(408, 259);
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(75, 23);
			this.btnExtract.TabIndex = 2;
			this.btnExtract.Text = "Extract";
			this.btnExtract.UseVisualStyleBackColor = true;
			this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
			// 
			// stIcons
			// 
			this.stIcons.AutoSize = true;
			this.stIcons.Location = new System.Drawing.Point(344, 16);
			this.stIcons.Name = "stIcons";
			this.stIcons.Size = new System.Drawing.Size(33, 13);
			this.stIcons.TabIndex = 3;
			this.stIcons.Text = "Icons";
			// 
			// grpSave
			// 
			this.grpSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSave.Controls.Add(this.btnSave);
			this.grpSave.Controls.Add(this.bfdSave);
			this.grpSave.Location = new System.Drawing.Point(16, 177);
			this.grpSave.Name = "grpSave";
			this.grpSave.Size = new System.Drawing.Size(309, 119);
			this.grpSave.TabIndex = 4;
			this.grpSave.TabStop = false;
			this.grpSave.Text = "Save";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSave.Location = new System.Drawing.Point(111, 82);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// bfdSave
			// 
			this.bfdSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bfdSave.BrowseLabel = "...";
			this.bfdSave.Directory = "";
			this.bfdSave.Label = "Save Directory";
			this.bfdSave.Location = new System.Drawing.Point(16, 27);
			this.bfdSave.Name = "bfdSave";
			this.bfdSave.Size = new System.Drawing.Size(272, 38);
			this.bfdSave.TabIndex = 0;
			// 
			// IconExtractor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(567, 313);
			this.Controls.Add(this.grpSave);
			this.Controls.Add(this.stIcons);
			this.Controls.Add(this.btnExtract);
			this.Controls.Add(this.grpSource);
			this.Controls.Add(this.lvIcons);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "IconExtractor";
			this.Text = "Icon Extractor";
			this.Load += new System.EventHandler(this.IconExtractor_Load);
			this.grpSource.ResumeLayout(false);
			this.grpSave.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lvIcons;
		private System.Windows.Forms.ImageList theImageList;
		private MyControls.MyGroupBox grpSource;
		private MyControls.BrowseForDirectory bfd1;
		private MyControls.BrowseForFile bff1;
		private System.Windows.Forms.Button btnExtract;
		private System.Windows.Forms.Label stIcons;
		private MyControls.MyGroupBox grpSave;
		private MyControls.BrowseForDirectory bfdSave;
		private System.Windows.Forms.Button btnSave;
	}
}