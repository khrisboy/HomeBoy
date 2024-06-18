using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using MyClasses;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	/// <summary>
	/// Summary description for SharpenCtrl.
	/// </summary>
	public class SharpenCtrl : WebFramerCS2ControlLibrary.SharpenCtrlSingle
	{
		protected System.Windows.Forms.RadioButton rbSharpenImages;
		protected System.Windows.Forms.RadioButton rbSharpenThumbs;

		// Non-control members.
		protected SharpenInfo	thumbsSharpenInfo;
		protected bool			sharpenImagesNeedTransferringFrom;
		protected bool			sharpenThumbsNeedTransferringFrom;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SharpenCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			imagesSharpenInfo					=  new SharpenInfo();
			thumbsSharpenInfo					=  new SharpenInfo();
			sharpenImagesNeedTransferringFrom	=  false;
			sharpenThumbsNeedTransferringFrom	=  false;
			isNikOnly							=  false;

			// Shove everything right.
			//foreach ( Control control in this.Controls )
			//{
			//   control.Left	+=  64;
			//}

			//rbSharpenImages.Left	=  16;
			//rbSharpenThumbs.Left	=  16;
		}

		public void SetImagesCheck( bool check )
		{
			//rbSharpenImages.Checked	=  check;
		}

		public override void LoadDefaults( DefaultsAr defaultsAr )
		{
			base.LoadDefaults( defaultsAr );
			imagesSharpenInfo.Load( defaultsAr, "Images" );
			thumbsSharpenInfo.Load( defaultsAr, "Thumbnails" );
		}
	
		public override void SaveDefaults( DefaultsAr defaultsAr )
		{
			base.SaveDefaults( defaultsAr );
			imagesSharpenInfo.Save( defaultsAr, "Images" );
			thumbsSharpenInfo.Save( defaultsAr, "Thumbnails" );
		}

		// Load/Unload sharpen data for images
		public virtual SharpenInfo LoadSharpenDataForImages()
		{
			imagesSharpenInfo	=  LoadSharpenData();

			return ( imagesSharpenInfo );
		}

		// Load/Unload sharpen data for thumbnails
		public virtual SharpenInfo LoadSharpenDataForThumbs()
		{
			thumbsSharpenInfo	=  LoadSharpenData();

			return ( thumbsSharpenInfo );
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null )
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
			this.grpSharpen.SuspendLayout();
			this.SuspendLayout();
			// 
			// rbSharpenThumbs
			// 
			//this.rbSharpenThumbs.Location = new System.Drawing.Point(16, 56);
			//this.rbSharpenThumbs.Name = "rbSharpenThumbs";
			//this.rbSharpenThumbs.Size = new System.Drawing.Size(82, 16);
			//this.rbSharpenThumbs.TabIndex = 5;
			//this.rbSharpenThumbs.Text = "Thumbnails";
			//this.rbSharpenThumbs.CheckedChanged += new System.EventHandler(this.rbSharpenThumbs_CheckedChanged);
			//// 
			//// rbSharpenImages
			//// 
			//this.rbSharpenImages.Location = new System.Drawing.Point(16, 28);
			//this.rbSharpenImages.Name = "rbSharpenImages";
			//this.rbSharpenImages.Size = new System.Drawing.Size(72, 16);
			//this.rbSharpenImages.TabIndex = 4;
			//this.rbSharpenImages.Text = "Images";
			//this.rbSharpenImages.CheckedChanged += new System.EventHandler(this.rbSharpenImages_CheckedChanged);
			// 
			// grpSharpen
			// 
			//this.grpSharpen.Controls.Add( this.rbSharpenImages );
			//this.grpSharpen.Controls.Add( this.rbSharpenThumbs );
			//this.grpSharpen.Size = new System.Drawing.Size( 360, 112 );
			// 
			// SharpenCtrl
			// 
			//***this.Size = new System.Drawing.Size( 392, 112 );
			this.grpSharpen.ResumeLayout( false );
			this.grpSharpenChoices.ResumeLayout(false);
			this.grpSharpenOptions.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void rbSharpenImages_CheckedChanged(object sender, System.EventArgs e)
		{
			// Save current values which "must" be for thumbnails.
			// Load up sharpen info for images.
			if ( rbSharpenImages.Checked )
			{
				//LoadSharpenDataForThumbs( false );	// From dialog into data structure.
				//LoadSharpenDataForImages( true );	// From data structure into the dialog
			}
		}

		private void rbSharpenThumbs_CheckedChanged(object sender, System.EventArgs e)
		{
			// Save current values which "must" be for images.
			// Load up sharpen info for thumbnails.
			if ( rbSharpenThumbs.Checked )
			{
				//LoadSharpenDataForImages( false );	// From dialog into data structure.
				//LoadSharpenDataForThumbs( true );	// From data structure into the dialog
			}
		}

		private void rbSharpenNone_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( rbSharpenNone.Checked )
			{
				// Hide all the options controls
				stSharpenOptionsAmount.Visible			=  false;
				edtSharpenOptionsAmount.Visible			=  false;
				stSharpenOptionsRadius.Visible			=  false;
				edtSharpenOptionsRadius.Visible			=  false;
				stSharpenOptionsThreshhold.Visible		=  false;
				edtSharpenOptionsThreshhold.Visible		=  false;
				stSharpenOptionsNikProfileType.Visible	=  false;
				cbSharpenOptionsNikProfileType.Visible	=  false;
			}
		}

		private void rbSharpenUnsharpMask_CheckedChanged( object sender, System.EventArgs e )
		{
			if ( rbSharpenUnsharpMask.Checked )
			{
				// Hide the nik options controls
				stSharpenOptionsNikProfileType.Visible	=  false;
				cbSharpenOptionsNikProfileType.Visible	=  false;

				// Show the USM options controls
				stSharpenOptionsAmount.Visible			=  true;
				edtSharpenOptionsAmount.Visible			=  true;
				stSharpenOptionsRadius.Visible			=  true;
				edtSharpenOptionsRadius.Visible			=  true;
				stSharpenOptionsThreshhold.Visible		=  true;
				edtSharpenOptionsThreshhold.Visible		=  true;
			}
		}

		private void rbSharpenNik_CheckedChanged( object sender, System.EventArgs e )
		{
			if ( rbSharpenNik.Checked )
			{
				// Hide the USM options controls
				stSharpenOptionsAmount.Visible				=  false;
				edtSharpenOptionsAmount.Visible				=  false;
				stSharpenOptionsRadius.Visible				=  false;
				edtSharpenOptionsRadius.Visible				=  false;
				stSharpenOptionsThreshhold.Visible			=  false;
				edtSharpenOptionsThreshhold.Visible			=  false;

				// Show the nik options controls
				stSharpenOptionsNikProfileType.Visible		=  true;
				cbSharpenOptionsNikProfileType.Visible	=  true;
			}
		}
	}
}
