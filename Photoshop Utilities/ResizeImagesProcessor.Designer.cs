namespace PhotoshopUtilities
{
	partial class ResizeImagesProcessor
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
			this.rcCtrl = new WebFramerCS2ControlLibrary.ResizeConstraintsCtrl();
			this.chkResizeImages = new System.Windows.Forms.CheckBox();
			this.chkResizeThumbnails = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// rcCtrl
			// 
			this.rcCtrl.IsVisible = true;
			this.rcCtrl.Label = "Resize Constraints";
			this.rcCtrl.Location = new System.Drawing.Point( 12, 12 );
			this.rcCtrl.Name = "rcCtrl";
			this.rcCtrl.PanoramaLabel = "Panorama";
			this.rcCtrl.RegularLabel = "Regular";
			this.rcCtrl.ShowSubGroups = true;
			this.rcCtrl.Size = new System.Drawing.Size( 430, 134 );
			this.rcCtrl.TabIndex = 0;
			// 
			// chkResizeImages
			// 
			this.chkResizeImages.AutoSize = true;
			this.chkResizeImages.Location = new System.Drawing.Point( 457, 55 );
			this.chkResizeImages.Name = "chkResizeImages";
			this.chkResizeImages.Size = new System.Drawing.Size( 95, 17 );
			this.chkResizeImages.TabIndex = 1;
			this.chkResizeImages.Text = "Resize Images";
			this.chkResizeImages.UseVisualStyleBackColor = true;
			// 
			// chkResizeThumbnails
			// 
			this.chkResizeThumbnails.AutoSize = true;
			this.chkResizeThumbnails.Location = new System.Drawing.Point( 457, 92 );
			this.chkResizeThumbnails.Name = "chkResizeThumbnails";
			this.chkResizeThumbnails.Size = new System.Drawing.Size( 115, 17 );
			this.chkResizeThumbnails.TabIndex = 2;
			this.chkResizeThumbnails.Text = "Resize Thumbnails";
			this.chkResizeThumbnails.UseVisualStyleBackColor = true;
			// 
			// ResizeImagesProcessor
			// 
			this.ClientSize = new System.Drawing.Size( 578, 224 );
			this.Controls.Add( this.chkResizeThumbnails );
			this.Controls.Add( this.chkResizeImages );
			this.Controls.Add( this.rcCtrl );
			this.Name = "ResizeImagesProcessor";
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private WebFramerCS2ControlLibrary.ResizeConstraintsCtrl rcCtrl;
		private System.Windows.Forms.CheckBox chkResizeImages;
		private System.Windows.Forms.CheckBox chkResizeThumbnails;

	}
}
