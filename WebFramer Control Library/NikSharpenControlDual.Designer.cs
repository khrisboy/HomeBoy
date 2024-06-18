namespace WebFramerCS2ControlLibrary
{
	partial class NikSharpenControlDual
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.rbSharpenThumbs = new System.Windows.Forms.RadioButton();
			this.rbSharpenImages = new System.Windows.Forms.RadioButton();
			this.nikSharpenCtrlThumbs = new WebFramerCS2ControlLibrary.NikPrintOrDisplaySharpenCtrl();
			this.nikSharpenCtrlImages = new WebFramerCS2ControlLibrary.NikPrintOrDisplaySharpenCtrl();
			this.SuspendLayout();
			// 
			// rbSharpenThumbs
			// 
			this.rbSharpenThumbs.AutoSize = true;
			this.rbSharpenThumbs.Location = new System.Drawing.Point( 150, 116 );
			this.rbSharpenThumbs.Name = "rbSharpenThumbs";
			this.rbSharpenThumbs.Size = new System.Drawing.Size( 63, 17 );
			this.rbSharpenThumbs.TabIndex = 3;
			this.rbSharpenThumbs.TabStop = true;
			this.rbSharpenThumbs.Text = "&Thumbs";
			this.rbSharpenThumbs.UseVisualStyleBackColor = true;
			// 
			// rbSharpenImages
			// 
			this.rbSharpenImages.AutoSize = true;
			this.rbSharpenImages.Location = new System.Drawing.Point( 57, 116 );
			this.rbSharpenImages.Name = "rbSharpenImages";
			this.rbSharpenImages.Size = new System.Drawing.Size( 59, 17 );
			this.rbSharpenImages.TabIndex = 2;
			this.rbSharpenImages.TabStop = true;
			this.rbSharpenImages.Text = "&Images";
			this.rbSharpenImages.UseVisualStyleBackColor = true;
			this.rbSharpenImages.CheckedChanged += new System.EventHandler( this.rbSharpenImagesThumbs_CheckedChanged );
			// 
			// nikSharpenCtrlThumbs
			// 
			this.nikSharpenCtrlThumbs.Location = new System.Drawing.Point( 0, 0 );
			this.nikSharpenCtrlThumbs.Name = "nikSharpenCtrlThumbs";
			this.nikSharpenCtrlThumbs.PaperType = 4;
			this.nikSharpenCtrlThumbs.PrinterResolution = 1;
			this.nikSharpenCtrlThumbs.SharpenProfile = PhotoshopSupport.NikSharpenInfo.NikProfileType.John;
			this.nikSharpenCtrlThumbs.Size = new System.Drawing.Size( 270, 112 );
			this.nikSharpenCtrlThumbs.Strength = 75;
			this.nikSharpenCtrlThumbs.TabIndex = 5;
			// 
			// nikSharpenCtrlImages
			// 
			this.nikSharpenCtrlImages.Location = new System.Drawing.Point( 0, 0 );
			this.nikSharpenCtrlImages.Name = "nikSharpenCtrlImages";
			this.nikSharpenCtrlImages.PaperType = 4;
			this.nikSharpenCtrlImages.PrinterResolution = 1;
			this.nikSharpenCtrlImages.SharpenProfile = PhotoshopSupport.NikSharpenInfo.NikProfileType.John;
			this.nikSharpenCtrlImages.Size = new System.Drawing.Size( 270, 112 );
			this.nikSharpenCtrlImages.Strength = 75;
			this.nikSharpenCtrlImages.TabIndex = 4;
			// 
			// NikSharpenControlDual
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.nikSharpenCtrlThumbs );
			this.Controls.Add( this.nikSharpenCtrlImages );
			this.Controls.Add( this.rbSharpenThumbs );
			this.Controls.Add( this.rbSharpenImages );
			this.Name = "NikSharpenControlDual";
			this.Size = new System.Drawing.Size( 270, 140 );
			this.Load += new System.EventHandler( this.NikSharpenControlDual_Load );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton rbSharpenThumbs;
		private System.Windows.Forms.RadioButton rbSharpenImages;
		private NikPrintOrDisplaySharpenCtrl nikSharpenCtrlImages;
		private NikPrintOrDisplaySharpenCtrl nikSharpenCtrlThumbs;
	}
}
