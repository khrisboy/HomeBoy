using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using PhotoshopSupport;
using MyControls;

namespace WebFramerCS2ControlLibrary
{
	/// <summary>
	/// Summary description for FrameCtrl.
	/// </summary>
	public class FrameCtrl : System.Windows.Forms.UserControl
	{
		private PhotoshopSupport.FrameInfo		imagesFrameInfo;
		private PhotoshopSupport.FrameInfo		thumbsFrameInfo;

		private MyControls.MyGroupBox		grpFrame;
		private TabControl		theTabControl;
		private TabPage			tabImages;
		private CheckBox			chkFrameMakeFrameImages;
		public CheckBox			chkFrameSliceAndSaveImages;
		public BackgroundCtrl	ctrlBackgroundImages;
		public BorderCtrl			ctrlBorderImages;
		public ShadowCtrl			ctrlShadowImages;
		private TabPage			tabThumbs;
		public BackgroundCtrl	ctrlBackgroundThumbs;
		public BorderCtrl			ctrlBorderThumbs;
		public ShadowCtrl			ctrlShadowThumbs;
		public CheckBox			chkFrameSliceAndSaveThumbs;
		private CheckBox			chkFrameMakeFrameThumbs;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrameCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			imagesFrameInfo	=  new PhotoshopSupport.FrameInfo();
			thumbsFrameInfo	=  new PhotoshopSupport.FrameInfo();
		}

		public FrameInfo ImagesFrameInfo
		{
			get
			{
				return ( LoadFrameDataForImages() );
			}
		}

		public FrameInfo ThumbsFrameInfo
		{
			get
			{
				return ( LoadFrameDataForThumbnails() );
			}
		}

		public new bool Enabled
		{
			set
			{
				// By passing this along to the MyGroupBox we can get this to work for everyone.
				grpFrame.Enabled		=  value;
				base.Enabled			=  value;
			}
		}

		public void Init()
		{
			EventArgs	ea	=  new EventArgs();

			chkFrameMakeFrameImages_CheckedChanged( this, ea );

			//if ( !chkFrameMakeFrameThumbs.Checked )
			//{
				EventArgs ee = new EventArgs();

				chkFrameMakeFrameThumbs_CheckedChanged( this, ee );
			//}
		}

		private FrameInfo LoadFrameDataForImages()
		{
			imagesFrameInfo.makeFrame			=  chkFrameMakeFrameImages.Checked;
			imagesFrameInfo.sliceAndSave		=  chkFrameSliceAndSaveImages.Checked;

			imagesFrameInfo.borderWidth		=  ctrlBorderImages.BorderWidth;
			imagesFrameInfo.borderColor		=  ctrlBorderImages.Color;

			imagesFrameInfo.backgroundColor	=  ctrlBackgroundImages.Color;
			imagesFrameInfo.backgroundWidth	=  ctrlBackgroundImages.BackgroundWidth;
			imagesFrameInfo.shadowWidth		=  ctrlShadowImages.ShadowWidth;
			imagesFrameInfo.shadowBlur			=  ctrlShadowImages.Blur;
			imagesFrameInfo.shadowSoftness	=  ctrlShadowImages.Softness;
			imagesFrameInfo.shadowRight		=  ctrlShadowImages.ShadowIsRight;
			imagesFrameInfo.shadowDown			=  ctrlShadowImages.ShadowIsDown;
			imagesFrameInfo.shadowColor		=  ctrlShadowImages.Color;

			return ( imagesFrameInfo );
		}
	
		private FrameInfo LoadFrameDataForThumbnails()
		{
			thumbsFrameInfo.makeFrame			=  chkFrameMakeFrameThumbs.Checked;
			thumbsFrameInfo.sliceAndSave		=  chkFrameSliceAndSaveThumbs.Checked;

			thumbsFrameInfo.borderWidth		=  ctrlBorderThumbs.BorderWidth;
			thumbsFrameInfo.borderColor		=  ctrlBorderThumbs.Color;

			thumbsFrameInfo.backgroundColor	=  ctrlBackgroundThumbs.Color;
			thumbsFrameInfo.backgroundWidth	=  ctrlBackgroundThumbs.BackgroundWidth;
			thumbsFrameInfo.shadowWidth		=  ctrlShadowImages.ShadowWidth;
			thumbsFrameInfo.shadowBlur			=  ctrlShadowImages.Blur;
			thumbsFrameInfo.shadowSoftness	=  ctrlShadowImages.Softness;
			thumbsFrameInfo.shadowRight		=  ctrlShadowImages.ShadowIsRight;
			thumbsFrameInfo.shadowDown			=  ctrlShadowImages.ShadowIsDown;
			thumbsFrameInfo.shadowColor		=  ctrlShadowImages.Color;

			return ( thumbsFrameInfo );
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null )
				{
					components.Dispose();
				}
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
			MyClasses.RgbColor rgbColor2 = new MyClasses.RgbColor();
			MyClasses.RgbColor rgbColor1 = new MyClasses.RgbColor();
			MyClasses.RgbColor rgbColor3 = new MyClasses.RgbColor();
			MyClasses.RgbColor rgbColor4 = new MyClasses.RgbColor();
			MyClasses.RgbColor rgbColor5 = new MyClasses.RgbColor();
			MyClasses.RgbColor rgbColor6 = new MyClasses.RgbColor();
			this.grpFrame = new MyControls.MyGroupBox();
			this.theTabControl = new System.Windows.Forms.TabControl();
			this.tabImages = new System.Windows.Forms.TabPage();
			this.chkFrameMakeFrameImages = new System.Windows.Forms.CheckBox();
			this.chkFrameSliceAndSaveImages = new System.Windows.Forms.CheckBox();
			this.ctrlShadowImages = new WebFramerCS2ControlLibrary.ShadowCtrl();
			this.ctrlBorderImages = new WebFramerCS2ControlLibrary.BorderCtrl();
			this.ctrlBackgroundImages = new WebFramerCS2ControlLibrary.BackgroundCtrl();
			this.tabThumbs = new System.Windows.Forms.TabPage();
			this.ctrlShadowThumbs = new WebFramerCS2ControlLibrary.ShadowCtrl();
			this.chkFrameMakeFrameThumbs = new System.Windows.Forms.CheckBox();
			this.chkFrameSliceAndSaveThumbs = new System.Windows.Forms.CheckBox();
			this.ctrlBorderThumbs = new WebFramerCS2ControlLibrary.BorderCtrl();
			this.ctrlBackgroundThumbs = new WebFramerCS2ControlLibrary.BackgroundCtrl();
			this.grpFrame.SuspendLayout();
			this.theTabControl.SuspendLayout();
			this.tabImages.SuspendLayout();
			this.tabThumbs.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpFrame
			// 
			this.grpFrame.Controls.Add( this.theTabControl );
			this.grpFrame.Location = new System.Drawing.Point( 0, 0 );
			this.grpFrame.Name = "grpFrame";
			this.grpFrame.Size = new System.Drawing.Size( 616, 348 );
			this.grpFrame.TabIndex = 0;
			this.grpFrame.TabStop = false;
			this.grpFrame.Text = "Frame";
			// 
			// theTabControl
			// 
			this.theTabControl.Controls.Add( this.tabImages );
			this.theTabControl.Controls.Add( this.tabThumbs );
			this.theTabControl.Location = new System.Drawing.Point( 16, 24 );
			this.theTabControl.Name = "theTabControl";
			this.theTabControl.SelectedIndex = 0;
			this.theTabControl.Size = new System.Drawing.Size( 584, 306 );
			this.theTabControl.TabIndex = 4;
			// 
			// tabImages
			// 
			this.tabImages.Controls.Add( this.ctrlBorderImages );
			this.tabImages.Controls.Add( this.chkFrameMakeFrameImages );
			this.tabImages.Controls.Add( this.chkFrameSliceAndSaveImages );
			this.tabImages.Controls.Add( this.ctrlShadowImages );
			this.tabImages.Controls.Add( this.ctrlBackgroundImages );
			this.tabImages.Location = new System.Drawing.Point( 4, 22 );
			this.tabImages.Name = "tabImages";
			this.tabImages.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabImages.Size = new System.Drawing.Size( 576, 280 );
			this.tabImages.TabIndex = 0;
			this.tabImages.Text = "Images";
			this.tabImages.UseVisualStyleBackColor = true;
			this.tabImages.Click += new System.EventHandler( this.tabImages_Click );
			// 
			// chkFrameMakeFrameImages
			// 
			this.chkFrameMakeFrameImages.Location = new System.Drawing.Point( 40, 20 );
			this.chkFrameMakeFrameImages.Name = "chkFrameMakeFrameImages";
			this.chkFrameMakeFrameImages.Size = new System.Drawing.Size( 88, 16 );
			this.chkFrameMakeFrameImages.TabIndex = 4;
			this.chkFrameMakeFrameImages.Text = "Make Frame";
			this.chkFrameMakeFrameImages.CheckedChanged += new System.EventHandler( this.chkFrameMakeFrameImages_CheckedChanged );
			// 
			// chkFrameSliceAndSaveImages
			// 
			this.chkFrameSliceAndSaveImages.Location = new System.Drawing.Point( 156, 20 );
			this.chkFrameSliceAndSaveImages.Name = "chkFrameSliceAndSaveImages";
			this.chkFrameSliceAndSaveImages.Size = new System.Drawing.Size( 104, 16 );
			this.chkFrameSliceAndSaveImages.TabIndex = 5;
			this.chkFrameSliceAndSaveImages.Text = "Slice And Save";
			// 
			// ctrlShadowImages
			// 
			this.ctrlShadowImages.Blur = 0;
			rgbColor2.B = 0;
			rgbColor2.G = 0;
			rgbColor2.R = 0;
			this.ctrlShadowImages.Color = rgbColor2;
			this.ctrlShadowImages.Location = new System.Drawing.Point( 200, 48 );
			this.ctrlShadowImages.Name = "ctrlShadowImages";
			this.ctrlShadowImages.ShadowIsDown = false;
			this.ctrlShadowImages.ShadowIsRight = false;
			this.ctrlShadowImages.ShadowWidth = 0;
			this.ctrlShadowImages.Size = new System.Drawing.Size( 360, 218 );
			this.ctrlShadowImages.Softness = 0;
			this.ctrlShadowImages.TabIndex = 2;
			// 
			// ctrlBorderImages
			// 
			this.ctrlBorderImages.BorderWidth = 1;
			rgbColor1.B = 0;
			rgbColor1.G = 0;
			rgbColor1.R = 0;
			this.ctrlBorderImages.Color = rgbColor1;
			this.ctrlBorderImages.Location = new System.Drawing.Point( 16, 48 );
			this.ctrlBorderImages.Name = "ctrlBorderImages";
			this.ctrlBorderImages.Size = new System.Drawing.Size( 168, 136 );
			this.ctrlBorderImages.TabIndex = 0;
			// 
			// ctrlBackgroundImages
			// 
			this.ctrlBackgroundImages.BackgroundWidth = 10;
			rgbColor3.B = 0;
			rgbColor3.G = 0;
			rgbColor3.R = 0;
			this.ctrlBackgroundImages.Color = rgbColor3;
			this.ctrlBackgroundImages.Location = new System.Drawing.Point( 16, 186 );
			this.ctrlBackgroundImages.Name = "ctrlBackgroundImages";
			this.ctrlBackgroundImages.Size = new System.Drawing.Size( 168, 80 );
			this.ctrlBackgroundImages.TabIndex = 1;
			// 
			// tabThumbs
			// 
			this.tabThumbs.Controls.Add( this.ctrlShadowThumbs );
			this.tabThumbs.Controls.Add( this.chkFrameMakeFrameThumbs );
			this.tabThumbs.Controls.Add( this.chkFrameSliceAndSaveThumbs );
			this.tabThumbs.Controls.Add( this.ctrlBorderThumbs );
			this.tabThumbs.Controls.Add( this.ctrlBackgroundThumbs );
			this.tabThumbs.Location = new System.Drawing.Point( 4, 22 );
			this.tabThumbs.Name = "tabThumbs";
			this.tabThumbs.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabThumbs.Size = new System.Drawing.Size( 576, 280 );
			this.tabThumbs.TabIndex = 1;
			this.tabThumbs.Text = "Thumbs";
			this.tabThumbs.UseVisualStyleBackColor = true;
			this.tabThumbs.Click += new System.EventHandler( this.tabThumbs_Click );
			// 
			// ctrlShadowThumbs
			// 
			this.ctrlShadowThumbs.Blur = 0;
			rgbColor4.B = 0;
			rgbColor4.G = 0;
			rgbColor4.R = 0;
			this.ctrlShadowThumbs.Color = rgbColor4;
			this.ctrlShadowThumbs.Location = new System.Drawing.Point( 200, 48 );
			this.ctrlShadowThumbs.Name = "ctrlShadowThumbs";
			this.ctrlShadowThumbs.ShadowIsDown = false;
			this.ctrlShadowThumbs.ShadowIsRight = false;
			this.ctrlShadowThumbs.ShadowWidth = 0;
			this.ctrlShadowThumbs.Size = new System.Drawing.Size( 360, 218 );
			this.ctrlShadowThumbs.Softness = 0;
			this.ctrlShadowThumbs.TabIndex = 11;
			// 
			// chkFrameMakeFrameThumbs
			// 
			this.chkFrameMakeFrameThumbs.Location = new System.Drawing.Point( 40, 20 );
			this.chkFrameMakeFrameThumbs.Name = "chkFrameMakeFrameThumbs";
			this.chkFrameMakeFrameThumbs.Size = new System.Drawing.Size( 88, 16 );
			this.chkFrameMakeFrameThumbs.TabIndex = 2;
			this.chkFrameMakeFrameThumbs.Text = "Make Frame";
			this.chkFrameMakeFrameThumbs.CheckedChanged += new System.EventHandler( this.chkFrameMakeFrameThumbs_CheckedChanged );
			// 
			// chkFrameSliceAndSaveThumbs
			// 
			this.chkFrameSliceAndSaveThumbs.Location = new System.Drawing.Point( 156, 20 );
			this.chkFrameSliceAndSaveThumbs.Name = "chkFrameSliceAndSaveThumbs";
			this.chkFrameSliceAndSaveThumbs.Size = new System.Drawing.Size( 104, 16 );
			this.chkFrameSliceAndSaveThumbs.TabIndex = 3;
			this.chkFrameSliceAndSaveThumbs.Text = "Slice And Save";
			// 
			// ctrlBorderThumbs
			// 
			this.ctrlBorderThumbs.BorderWidth = 1;
			rgbColor5.B = 0;
			rgbColor5.G = 0;
			rgbColor5.R = 0;
			this.ctrlBorderThumbs.Color = rgbColor5;
			this.ctrlBorderThumbs.Location = new System.Drawing.Point( 16, 48 );
			this.ctrlBorderThumbs.Name = "ctrlBorderThumbs";
			this.ctrlBorderThumbs.Size = new System.Drawing.Size( 168, 136 );
			this.ctrlBorderThumbs.TabIndex = 9;
			// 
			// ctrlBackgroundThumbs
			// 
			this.ctrlBackgroundThumbs.BackgroundWidth = 10;
			rgbColor6.B = 0;
			rgbColor6.G = 0;
			rgbColor6.R = 0;
			this.ctrlBackgroundThumbs.Color = rgbColor6;
			this.ctrlBackgroundThumbs.Location = new System.Drawing.Point( 16, 186 );
			this.ctrlBackgroundThumbs.Name = "ctrlBackgroundThumbs";
			this.ctrlBackgroundThumbs.Size = new System.Drawing.Size( 168, 80 );
			this.ctrlBackgroundThumbs.TabIndex = 10;
			// 
			// FrameCtrl
			// 
			this.Controls.Add( this.grpFrame );
			this.Name = "FrameCtrl";
			this.Size = new System.Drawing.Size( 616, 351 );
			this.grpFrame.ResumeLayout( false );
			this.theTabControl.ResumeLayout( false );
			this.tabImages.ResumeLayout( false );
			this.tabThumbs.ResumeLayout( false );
			this.ResumeLayout( false );

		}
		#endregion


		private void tabImages_Click( object sender, EventArgs e )
		{
		}

		private void tabThumbs_Click( object sender, EventArgs e )
		{
		}

		private void chkFrameMakeFrameImages_CheckedChanged( object sender, System.EventArgs e )
		{
			// Enable/disable everything.
			ctrlBackgroundImages.Enabled			=  chkFrameMakeFrameImages.Checked;
			ctrlBorderImages.Enabled				=  chkFrameMakeFrameImages.Checked;
			ctrlShadowImages.Enabled				=  chkFrameMakeFrameImages.Checked;
			chkFrameSliceAndSaveImages.Enabled	=  chkFrameMakeFrameImages.Checked;

			// Keep images & thumbs in synch for making a frame or not.
			imagesFrameInfo.makeFrame	=  chkFrameMakeFrameImages.Checked;
		}

		private void chkFrameMakeFrameThumbs_CheckedChanged( object sender, System.EventArgs e )
		{
			// Enable/disable everything.
			ctrlBackgroundThumbs.Enabled			=  chkFrameMakeFrameThumbs.Checked;
			ctrlBorderThumbs.Enabled				=  chkFrameMakeFrameThumbs.Checked;
			ctrlShadowThumbs.Enabled				=  chkFrameMakeFrameThumbs.Checked;
			chkFrameSliceAndSaveThumbs.Enabled	=  chkFrameMakeFrameThumbs.Checked;

			// Keep images & thumbs in synch for making a frame or not.
			thumbsFrameInfo.makeFrame	=  chkFrameMakeFrameThumbs.Checked;
		}
	}
}
