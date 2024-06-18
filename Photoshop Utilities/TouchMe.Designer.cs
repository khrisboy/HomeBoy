namespace PhotoshopUtilities
{
	partial class TouchMe
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
			this.bffFromFile = new MyControls.BrowseForFile();
			this.bffToFile = new MyControls.BrowseForFile();
			this.browseForDirectory1 = new MyControls.BrowseForDirectory();
			this.tbDateAndTime = new System.Windows.Forms.TextBox();
			this.lblFileDateAndTime = new System.Windows.Forms.Label();
			this.btnTouchMe = new System.Windows.Forms.Button();
			this.theToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// bffFromFile
			// 
			this.bffFromFile.BrowseLabel = "...";
			this.bffFromFile.CheckFileExists = true;
			this.bffFromFile.DefaultExtension = null;
			this.bffFromFile.DisplayFileNameOnly = true;
			this.bffFromFile.FileName = "";
			this.bffFromFile.FilesFilter = "All files (*.*)|*.*";
			this.bffFromFile.FilterIndex = 1;
			this.bffFromFile.InitialDirectory = "c:\\";
			this.bffFromFile.Label = "From File";
			this.bffFromFile.Location = new System.Drawing.Point(12, 70);
			this.bffFromFile.Name = "bffFromFile";
			this.bffFromFile.Size = new System.Drawing.Size(300, 38);
			this.bffFromFile.TabIndex = 0;
			this.bffFromFile.OnMyTextChanged += new MyControls.BrowseForObject.MyTextChanged(this.bffFromFile_OnMyTextChanged);
			// 
			// bffToFile
			// 
			this.bffToFile.BrowseLabel = "...";
			this.bffToFile.CheckFileExists = true;
			this.bffToFile.DefaultExtension = null;
			this.bffToFile.DisplayFileNameOnly = true;
			this.bffToFile.FileName = "";
			this.bffToFile.FilesFilter = "All files (*.*)|*.*";
			this.bffToFile.FilterIndex = 1;
			this.bffToFile.InitialDirectory = "c:\\";
			this.bffToFile.Label = "To File";
			this.bffToFile.Location = new System.Drawing.Point(340, 70);
			this.bffToFile.Name = "bffToFile";
			this.bffToFile.Size = new System.Drawing.Size(300, 38);
			this.bffToFile.TabIndex = 3;
			// 
			// browseForDirectory1
			// 
			this.browseForDirectory1.BrowseLabel = "...";
			this.browseForDirectory1.Directory = "";
			this.browseForDirectory1.Label = "Files Directory";
			this.browseForDirectory1.Location = new System.Drawing.Point(13, 13);
			this.browseForDirectory1.Name = "browseForDirectory1";
			this.browseForDirectory1.Size = new System.Drawing.Size(627, 38);
			this.browseForDirectory1.TabIndex = 4;
			// 
			// tbDateAndTime
			// 
			this.tbDateAndTime.Location = new System.Drawing.Point(13, 153);
			this.tbDateAndTime.Name = "tbDateAndTime";
			this.tbDateAndTime.Size = new System.Drawing.Size(175, 20);
			this.tbDateAndTime.TabIndex = 5;
			// 
			// lblFileDateAndTime
			// 
			this.lblFileDateAndTime.AutoSize = true;
			this.lblFileDateAndTime.Location = new System.Drawing.Point(12, 134);
			this.lblFileDateAndTime.Name = "lblFileDateAndTime";
			this.lblFileDateAndTime.Size = new System.Drawing.Size(84, 13);
			this.lblFileDateAndTime.TabIndex = 6;
			this.lblFileDateAndTime.Text = "File Date && Time";
			// 
			// btnTouchMe
			// 
			this.btnTouchMe.Location = new System.Drawing.Point(525, 153);
			this.btnTouchMe.Name = "btnTouchMe";
			this.btnTouchMe.Size = new System.Drawing.Size(75, 23);
			this.btnTouchMe.TabIndex = 7;
			this.btnTouchMe.Text = "Touch Me!";
			this.btnTouchMe.UseVisualStyleBackColor = true;
			this.btnTouchMe.Click += new System.EventHandler(this.btnTouchMe_Click);
			// 
			// TouchMe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(652, 217);
			this.ControlBox = false;
			this.Controls.Add(this.btnTouchMe);
			this.Controls.Add(this.lblFileDateAndTime);
			this.Controls.Add(this.tbDateAndTime);
			this.Controls.Add(this.browseForDirectory1);
			this.Controls.Add(this.bffToFile);
			this.Controls.Add(this.bffFromFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "TouchMe";
			this.Text = "Touch Me";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MyControls.BrowseForFile bffFromFile;
		private MyControls.BrowseForFile bffToFile;
		private MyControls.BrowseForDirectory browseForDirectory1;
		private System.Windows.Forms.TextBox tbDateAndTime;
		private System.Windows.Forms.Label lblFileDateAndTime;
		private System.Windows.Forms.Button btnTouchMe;
		private System.Windows.Forms.ToolTip theToolTip;
	}
}