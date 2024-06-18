using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using MyClasses;
using MyControls;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	/// <summary>
	/// Summary description for ResizeConstraintsCtrl.
	/// </summary>
	public class ResizeConstraintsCtrl : UserControl
	{
		#region
		/// <summary>
		/// Non-Control members
		/// </summary>
		public ResizeInfo	imagesResizeInfo;
		public ResizeInfo	thumbsResizeInfo;
		private bool		resizeImagesNeedTransferringFrom;
		private bool		resizeThumbsNeedTransferringFrom;

		/// <summary>
		/// Control members
		/// </summary>
		private MyInvisibleGroupBox grpConstrain;
		private RadioButton rbConstrainThumbs;
		private RadioButton rbConstrainImages;
		private MyInvisibleGroupBox grpConstrainPanorama;
		private Label stPanoramasPx;
		private MyInvisibleGroupBox grpConstrainRegular;
		private Label stImagesPx;
		private RadioButton rbConstrainRegularWidth;
		private RadioButton rbConstrainRegularHeight;
		private RadioButton rbConstrainRegularMin;
		private RadioButton rbConstrainRegularMax;
		private RadioButton rbConstrainPanoramaHeight;
		private RadioButton rbConstrainPanoramaWidth;
		private RadioButton rbConstrainPanoramaMin;
		private RadioButton rbConstrainPanoramaMax;
		private LabelAndText ltImagesPanorama;
		private LabelAndText ltImagesRegular;
		private LabelAndText ltPanoramaAspectRatio;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public ResizeConstraintsCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			imagesResizeInfo						=  new ResizeInfo();
			thumbsResizeInfo						=  new ResizeInfo();
			resizeImagesNeedTransferringFrom	=  false;
			resizeThumbsNeedTransferringFrom	=  false;
		}

		public string Label
		{
			get { return ( grpConstrain.Text ); }
			set { grpConstrain.Text	=  value; }
		}

		public string RegularLabel
		{
			get { return ( grpConstrainRegular.Text ); }
			set { grpConstrainRegular.Text	=  value; }
		}

		public string PanoramaLabel
		{
			get { return ( grpConstrainPanorama.Text ); }
			set { grpConstrainPanorama.Text	=  value; }
		}

		public bool IsVisible
		{
			get { return ( grpConstrain.PaintMe ); }
			set { grpConstrain.PaintMe =  value; }
		}

		public bool ShowSubGroups
		{
			get { return ( grpConstrainRegular.PaintMe ); }

			set
			{
				grpConstrainRegular.PaintMe  =  value;
				grpConstrainPanorama.PaintMe =  value;
			}
		}

		public void SetImagesCheck( bool check )
		{
			rbConstrainImages.Checked	=  check;
		}

		public void EnableImages( bool enable )
		{
			this.rbConstrainImages.Enabled	=  enable;
		}

		public void EnableThumbs( bool enable )
		{
			this.rbConstrainThumbs.Enabled	=  enable;
		}

		public void LoadDefaults( DefaultsAr defaultsAr )
		{
			imagesResizeInfo.Load( defaultsAr, "Images" );
			thumbsResizeInfo.Load( defaultsAr, "Thumbnails" );
		}
	
		public void SaveDefaults( DefaultsAr defaultsAr )
		{
			imagesResizeInfo.Save( defaultsAr, "Images" );
			thumbsResizeInfo.Save( defaultsAr, "Thumbnails" );
		}

		// Load/Unload resize data for images.
		public ResizeInfo LoadResizeDataForImages( bool transferTo )
		{
			if ( transferTo && rbConstrainImages.Checked )
			{
				LoadResizeData( imagesResizeInfo, transferTo );

				resizeImagesNeedTransferringFrom	=  true;
			}
			else if ( !transferTo && resizeImagesNeedTransferringFrom )
			{
				LoadResizeData( imagesResizeInfo, transferTo );

				resizeImagesNeedTransferringFrom	=  false;
			}

			return ( imagesResizeInfo );
		}

		// Load/Unload resize data for thumbnails
		public ResizeInfo LoadResizeDataForThumbs( bool transferTo )
		{
			if ( transferTo && this.rbConstrainThumbs.Checked )
			{
				LoadResizeData( thumbsResizeInfo, transferTo );

				resizeThumbsNeedTransferringFrom	=  true;
			}
			else if ( !transferTo && resizeThumbsNeedTransferringFrom )
			{
				LoadResizeData( thumbsResizeInfo, transferTo );

				resizeThumbsNeedTransferringFrom	=  false;
			}

			return ( thumbsResizeInfo );
		}

		// Load/Unload resize data.
		public void LoadResizeData( ResizeInfo resizeInfo, bool transferTo )
		{
			if ( transferTo )
			{
				// True:  from data structure into dialog.
				ltImagesRegular.Text		=  resizeInfo.resizeRegular.ToString();
				ltImagesPanorama.Text		=  resizeInfo.resizePanorama.ToString();
				ltPanoramaAspectRatio.Text	=  resizeInfo.panoramaAspect.ToString();

				if ( resizeInfo.typeRegular == ResizeType.Height )
					rbConstrainRegularHeight.Checked	=  true;
				else if ( resizeInfo.typeRegular == ResizeType.Width )
					rbConstrainRegularWidth.Checked	=  true;
				else if ( resizeInfo.typeRegular == ResizeType.MinBoth )
					rbConstrainRegularMin.Checked		=  true;
				else
					rbConstrainRegularMax.Checked		=  true;

				if ( resizeInfo.typePanorama == ResizeType.Height )
					rbConstrainPanoramaHeight.Checked	=  true;
				else if ( resizeInfo.typePanorama == ResizeType.Width )
					rbConstrainPanoramaWidth.Checked		=  true;
				else if ( resizeInfo.typePanorama == ResizeType.MinBoth )
					rbConstrainPanoramaMin.Checked		=  true;
				else
					rbConstrainPanoramaMax.Checked		=  true;
			}
			else
			{
				// False:  from dialog into data structure.
				resizeInfo.resizeRegular	=  TextValueParser.GetInt( ltImagesRegular, resizeInfo.resizeRegular );
				resizeInfo.resizePanorama	=  TextValueParser.GetInt( ltImagesPanorama, resizeInfo.resizePanorama );
				resizeInfo.panoramaAspect	=  TextValueParser.GetDouble( ltPanoramaAspectRatio, resizeInfo.panoramaAspect );

				if ( rbConstrainRegularHeight.Checked )
					resizeInfo.typeRegular	=  ResizeType.Height;
				else if ( rbConstrainRegularWidth.Checked )
					resizeInfo.typeRegular	=  ResizeType.Width;
				else if ( rbConstrainRegularMin.Checked )
					resizeInfo.typeRegular	=  ResizeType.MinBoth;
				else
					resizeInfo.typeRegular	=  ResizeType.MaxBoth;

				if ( rbConstrainPanoramaHeight.Checked )
					resizeInfo.typePanorama	=  ResizeType.Height;
				else if ( rbConstrainPanoramaWidth.Checked )
					resizeInfo.typePanorama	=  ResizeType.Width;
				else if ( rbConstrainPanoramaMin.Checked )
					resizeInfo.typePanorama	=  ResizeType.MinBoth;
				else
					resizeInfo.typePanorama	=  ResizeType.MaxBoth;
			}
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
			this.grpConstrain = new MyControls.MyInvisibleGroupBox();
			this.grpConstrainPanorama = new MyControls.MyInvisibleGroupBox();
			this.ltPanoramaAspectRatio = new MyControls.LabelAndText();
			this.ltImagesPanorama = new MyControls.LabelAndText();
			this.rbConstrainPanoramaMin = new System.Windows.Forms.RadioButton();
			this.rbConstrainPanoramaMax = new System.Windows.Forms.RadioButton();
			this.rbConstrainPanoramaHeight = new System.Windows.Forms.RadioButton();
			this.rbConstrainPanoramaWidth = new System.Windows.Forms.RadioButton();
			this.stPanoramasPx = new System.Windows.Forms.Label();
			this.grpConstrainRegular = new MyControls.MyInvisibleGroupBox();
			this.ltImagesRegular = new MyControls.LabelAndText();
			this.rbConstrainRegularMin = new System.Windows.Forms.RadioButton();
			this.rbConstrainRegularMax = new System.Windows.Forms.RadioButton();
			this.rbConstrainRegularWidth = new System.Windows.Forms.RadioButton();
			this.rbConstrainRegularHeight = new System.Windows.Forms.RadioButton();
			this.stImagesPx = new System.Windows.Forms.Label();
			this.rbConstrainThumbs = new System.Windows.Forms.RadioButton();
			this.rbConstrainImages = new System.Windows.Forms.RadioButton();
			this.grpConstrain.SuspendLayout();
			this.grpConstrainPanorama.SuspendLayout();
			this.grpConstrainRegular.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpConstrain
			// 
			this.grpConstrain.Controls.Add( this.grpConstrainPanorama );
			this.grpConstrain.Controls.Add( this.grpConstrainRegular );
			this.grpConstrain.Controls.Add( this.rbConstrainThumbs );
			this.grpConstrain.Controls.Add( this.rbConstrainImages );
			this.grpConstrain.Location = new System.Drawing.Point( 0, 0 );
			this.grpConstrain.Name = "grpConstrain";
			this.grpConstrain.PaintMe = false;
			this.grpConstrain.Size = new System.Drawing.Size( 430, 134 );
			this.grpConstrain.TabIndex = 0;
			this.grpConstrain.TabStop = false;
			this.grpConstrain.Text = "Constrain";
			// 
			// grpConstrainPanorama
			// 
			this.grpConstrainPanorama.Controls.Add( this.ltPanoramaAspectRatio );
			this.grpConstrainPanorama.Controls.Add( this.ltImagesPanorama );
			this.grpConstrainPanorama.Controls.Add( this.rbConstrainPanoramaMin );
			this.grpConstrainPanorama.Controls.Add( this.rbConstrainPanoramaMax );
			this.grpConstrainPanorama.Controls.Add( this.rbConstrainPanoramaHeight );
			this.grpConstrainPanorama.Controls.Add( this.rbConstrainPanoramaWidth );
			this.grpConstrainPanorama.Controls.Add( this.stPanoramasPx );
			this.grpConstrainPanorama.Location = new System.Drawing.Point( 260, 12 );
			this.grpConstrainPanorama.Name = "grpConstrainPanorama";
			this.grpConstrainPanorama.PaintMe = false;
			this.grpConstrainPanorama.Size = new System.Drawing.Size( 160, 110 );
			this.grpConstrainPanorama.TabIndex = 3;
			this.grpConstrainPanorama.TabStop = false;
			this.grpConstrainPanorama.Text = "Panorama";
			// 
			// ltPanoramaAspectRatio
			// 
			this.ltPanoramaAspectRatio.Label = "Aspect";
			this.ltPanoramaAspectRatio.Location = new System.Drawing.Point( 105, 64 );
			this.ltPanoramaAspectRatio.Name = "ltPanoramaAspectRatio";
			this.ltPanoramaAspectRatio.Size = new System.Drawing.Size( 45, 37 );
			this.ltPanoramaAspectRatio.TabIndex = 12;
			// 
			// ltImagesPanorama
			// 
			this.ltImagesPanorama.Label = "Panoramas";
			this.ltImagesPanorama.Location = new System.Drawing.Point( 10, 64 );
			this.ltImagesPanorama.Name = "ltImagesPanorama";
			this.ltImagesPanorama.Size = new System.Drawing.Size( 65, 37 );
			this.ltImagesPanorama.TabIndex = 11;
			// 
			// rbConstrainPanoramaMin
			// 
			this.rbConstrainPanoramaMin.Location = new System.Drawing.Point( 70, 18 );
			this.rbConstrainPanoramaMin.Name = "rbConstrainPanoramaMin";
			this.rbConstrainPanoramaMin.Size = new System.Drawing.Size( 44, 16 );
			this.rbConstrainPanoramaMin.TabIndex = 9;
			this.rbConstrainPanoramaMin.Text = "Min";
			// 
			// rbConstrainPanoramaMax
			// 
			this.rbConstrainPanoramaMax.AutoSize = true;
			this.rbConstrainPanoramaMax.Location = new System.Drawing.Point( 70, 38 );
			this.rbConstrainPanoramaMax.Name = "rbConstrainPanoramaMax";
			this.rbConstrainPanoramaMax.Size = new System.Drawing.Size( 45, 17 );
			this.rbConstrainPanoramaMax.TabIndex = 10;
			this.rbConstrainPanoramaMax.Text = "Max";
			// 
			// rbConstrainPanoramaHeight
			// 
			this.rbConstrainPanoramaHeight.Location = new System.Drawing.Point( 10, 38 );
			this.rbConstrainPanoramaHeight.Name = "rbConstrainPanoramaHeight";
			this.rbConstrainPanoramaHeight.Size = new System.Drawing.Size( 56, 16 );
			this.rbConstrainPanoramaHeight.TabIndex = 8;
			this.rbConstrainPanoramaHeight.Text = "Height";
			// 
			// rbConstrainPanoramaWidth
			// 
			this.rbConstrainPanoramaWidth.Location = new System.Drawing.Point( 10, 18 );
			this.rbConstrainPanoramaWidth.Name = "rbConstrainPanoramaWidth";
			this.rbConstrainPanoramaWidth.Size = new System.Drawing.Size( 56, 16 );
			this.rbConstrainPanoramaWidth.TabIndex = 7;
			this.rbConstrainPanoramaWidth.Text = "Width";
			// 
			// stPanoramasPx
			// 
			this.stPanoramasPx.AutoSize = true;
			this.stPanoramasPx.Location = new System.Drawing.Point( 77, 85 );
			this.stPanoramasPx.Name = "stPanoramasPx";
			this.stPanoramasPx.Size = new System.Drawing.Size( 18, 13 );
			this.stPanoramasPx.TabIndex = 4;
			this.stPanoramasPx.Text = "px";
			// 
			// grpConstrainRegular
			// 
			this.grpConstrainRegular.Controls.Add( this.ltImagesRegular );
			this.grpConstrainRegular.Controls.Add( this.rbConstrainRegularMin );
			this.grpConstrainRegular.Controls.Add( this.rbConstrainRegularMax );
			this.grpConstrainRegular.Controls.Add( this.rbConstrainRegularWidth );
			this.grpConstrainRegular.Controls.Add( this.rbConstrainRegularHeight );
			this.grpConstrainRegular.Controls.Add( this.stImagesPx );
			this.grpConstrainRegular.Location = new System.Drawing.Point( 120, 12 );
			this.grpConstrainRegular.Name = "grpConstrainRegular";
			this.grpConstrainRegular.PaintMe = false;
			this.grpConstrainRegular.Size = new System.Drawing.Size( 124, 110 );
			this.grpConstrainRegular.TabIndex = 2;
			this.grpConstrainRegular.TabStop = false;
			this.grpConstrainRegular.Text = "Regular";
			// 
			// ltImagesRegular
			// 
			this.ltImagesRegular.Label = "Images";
			this.ltImagesRegular.Location = new System.Drawing.Point( 10, 64 );
			this.ltImagesRegular.Name = "ltImagesRegular";
			this.ltImagesRegular.Size = new System.Drawing.Size( 71, 37 );
			this.ltImagesRegular.TabIndex = 9;
			// 
			// rbConstrainRegularMin
			// 
			this.rbConstrainRegularMin.Location = new System.Drawing.Point( 70, 18 );
			this.rbConstrainRegularMin.Name = "rbConstrainRegularMin";
			this.rbConstrainRegularMin.Size = new System.Drawing.Size( 44, 16 );
			this.rbConstrainRegularMin.TabIndex = 7;
			this.rbConstrainRegularMin.Text = "Min";
			// 
			// rbConstrainRegularMax
			// 
			this.rbConstrainRegularMax.AutoSize = true;
			this.rbConstrainRegularMax.Location = new System.Drawing.Point( 70, 38 );
			this.rbConstrainRegularMax.Name = "rbConstrainRegularMax";
			this.rbConstrainRegularMax.Size = new System.Drawing.Size( 45, 17 );
			this.rbConstrainRegularMax.TabIndex = 8;
			this.rbConstrainRegularMax.Text = "Max";
			// 
			// rbConstrainRegularWidth
			// 
			this.rbConstrainRegularWidth.Location = new System.Drawing.Point( 10, 18 );
			this.rbConstrainRegularWidth.Name = "rbConstrainRegularWidth";
			this.rbConstrainRegularWidth.Size = new System.Drawing.Size( 56, 16 );
			this.rbConstrainRegularWidth.TabIndex = 5;
			this.rbConstrainRegularWidth.Text = "Width";
			// 
			// rbConstrainRegularHeight
			// 
			this.rbConstrainRegularHeight.Location = new System.Drawing.Point( 10, 38 );
			this.rbConstrainRegularHeight.Name = "rbConstrainRegularHeight";
			this.rbConstrainRegularHeight.Size = new System.Drawing.Size( 56, 16 );
			this.rbConstrainRegularHeight.TabIndex = 6;
			this.rbConstrainRegularHeight.Text = "Height";
			// 
			// stImagesPx
			// 
			this.stImagesPx.AutoSize = true;
			this.stImagesPx.Location = new System.Drawing.Point( 84, 85 );
			this.stImagesPx.Name = "stImagesPx";
			this.stImagesPx.Size = new System.Drawing.Size( 18, 13 );
			this.stImagesPx.TabIndex = 4;
			this.stImagesPx.Text = "px";
			// 
			// rbConstrainThumbs
			// 
			this.rbConstrainThumbs.Location = new System.Drawing.Point( 24, 77 );
			this.rbConstrainThumbs.Name = "rbConstrainThumbs";
			this.rbConstrainThumbs.Size = new System.Drawing.Size( 88, 16 );
			this.rbConstrainThumbs.TabIndex = 1;
			this.rbConstrainThumbs.Text = "Thumbnails";
			this.rbConstrainThumbs.CheckedChanged += new System.EventHandler( this.rbConstrainThumbs_CheckedChanged );
			// 
			// rbConstrainImages
			// 
			this.rbConstrainImages.Location = new System.Drawing.Point( 24, 42 );
			this.rbConstrainImages.Name = "rbConstrainImages";
			this.rbConstrainImages.Size = new System.Drawing.Size( 66, 16 );
			this.rbConstrainImages.TabIndex = 0;
			this.rbConstrainImages.Text = "Images";
			this.rbConstrainImages.CheckedChanged += new System.EventHandler( this.rbConstrainImages_CheckedChanged );
			// 
			// ResizeConstraintsCtrl
			// 
			this.Controls.Add( this.grpConstrain );
			this.Name = "ResizeConstraintsCtrl";
			this.Size = new System.Drawing.Size( 430, 134 );
			this.grpConstrain.ResumeLayout( false );
			this.grpConstrainPanorama.ResumeLayout( false );
			this.grpConstrainPanorama.PerformLayout();
			this.grpConstrainRegular.ResumeLayout( false );
			this.grpConstrainRegular.PerformLayout();
			this.ResumeLayout( false );

		}
		#endregion

		private void rbConstrainImages_CheckedChanged(object sender, System.EventArgs e)
		{
			// Save current values which "must" be for thumbnails.
			// Load up resize info for images.
			LoadResizeDataForThumbs( false );	// From dialog into data structure.
			LoadResizeDataForImages( true );		// From data structure into the dialog
		}

		private void rbConstrainThumbs_CheckedChanged(object sender, System.EventArgs e)
		{
			// Save current values which "must" be for images.
			// Load up resize info for thumbnails.
			LoadResizeDataForImages( false );	// From dialog into data structure.
			LoadResizeDataForThumbs( true );		// From data structure into the dialog
		}
	}
}
