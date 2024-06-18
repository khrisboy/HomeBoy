using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	public class SharpenCtrlDual : UserControl
	{
		private SharpenInfo	imagesSharpenInfo;
		private SharpenInfo	thumbsSharpenInfo;

		public SharpenCtrlDual()
		{
			InitializeComponent();

			imagesSharpenInfo	=  new SharpenInfo();
			thumbsSharpenInfo	=  new SharpenInfo();
		}

		public SharpenInfo ImagesSharpenInfo
		{
			get
			{
				imagesSharpenInfo	=  sharpenCtrlSingleImages.LoadSharpenData();

				return ( imagesSharpenInfo  );
			}
		}

		public SharpenInfo ThumbsSharpenInfo
		{
			get
			{
				thumbsSharpenInfo	=  sharpenCtrlSingleThumbs.LoadSharpenData();

				return ( thumbsSharpenInfo );
			}
		}

		private void rbSharpenImagesThumbs_CheckedChanged( object sender, EventArgs e )
		{
			sharpenCtrlSingleImages.Visible	=  rbSharpenImages.Checked;
			sharpenCtrlSingleThumbs.Visible	=  !rbSharpenImages.Checked;
		}

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
			this.sharpenCtrlSingleImages = new WebFramerCS2ControlLibrary.SharpenCtrlSingle();
			this.sharpenCtrlSingleThumbs = new WebFramerCS2ControlLibrary.SharpenCtrlSingle();
			this.rbSharpenImages = new System.Windows.Forms.RadioButton();
			this.rbSharpenThumbs = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// sharpenCtrlSingleImages
			// 
			this.sharpenCtrlSingleImages.Location = new System.Drawing.Point( 0, 0 );
			this.sharpenCtrlSingleImages.Name = "sharpenCtrlSingleImages";
			this.sharpenCtrlSingleImages.Size = new System.Drawing.Size( 328, 112 );
			this.sharpenCtrlSingleImages.TabIndex = 0;
			// 
			// sharpenCtrlSingleThumbs
			// 
			this.sharpenCtrlSingleThumbs.Location = new System.Drawing.Point( 0, 0 );
			this.sharpenCtrlSingleThumbs.Name = "sharpenCtrlSingleThumbs";
			this.sharpenCtrlSingleThumbs.Size = new System.Drawing.Size( 328, 112 );
			this.sharpenCtrlSingleThumbs.TabIndex = 0;
			this.sharpenCtrlSingleThumbs.Visible = false;
			// 
			// rbSharpenImages
			// 
			this.rbSharpenImages.AutoSize = true;
			this.rbSharpenImages.Location = new System.Drawing.Point( 70, 116 );
			this.rbSharpenImages.Name = "rbSharpenImages";
			this.rbSharpenImages.Size = new System.Drawing.Size( 59, 17 );
			this.rbSharpenImages.TabIndex = 1;
			this.rbSharpenImages.TabStop = true;
			this.rbSharpenImages.Text = "&Images";
			this.rbSharpenImages.UseVisualStyleBackColor = true;
			this.rbSharpenImages.CheckedChanged += new System.EventHandler( this.rbSharpenImagesThumbs_CheckedChanged );
			// 
			// rbSharpenThumbs
			// 
			this.rbSharpenThumbs.AutoSize = true;
			this.rbSharpenThumbs.Location = new System.Drawing.Point( 195, 116 );
			this.rbSharpenThumbs.Name = "rbSharpenThumbs";
			this.rbSharpenThumbs.Size = new System.Drawing.Size( 63, 17 );
			this.rbSharpenThumbs.TabIndex = 2;
			this.rbSharpenThumbs.TabStop = true;
			this.rbSharpenThumbs.Text = "&Thumbs";
			this.rbSharpenThumbs.UseVisualStyleBackColor = true;
			this.rbSharpenThumbs.CheckedChanged += new System.EventHandler( this.rbSharpenImagesThumbs_CheckedChanged );
			// 
			// DualSharpenCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.rbSharpenThumbs );
			this.Controls.Add( this.rbSharpenImages );
			this.Controls.Add( this.sharpenCtrlSingleImages );
			this.Controls.Add( this.sharpenCtrlSingleThumbs );
			this.Name = "DualSharpenCtrl";
			this.Size = new System.Drawing.Size( 328, 140 );
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private WebFramerCS2ControlLibrary.SharpenCtrlSingle sharpenCtrlSingleImages;
		private WebFramerCS2ControlLibrary.SharpenCtrlSingle sharpenCtrlSingleThumbs;
		private System.Windows.Forms.RadioButton rbSharpenImages;
		private System.Windows.Forms.RadioButton rbSharpenThumbs;
	}
}
