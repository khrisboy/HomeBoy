using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MyClasses;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	public class SharpenCtrlMasks : WebFramerCS2ControlLibrary.SharpenCtrl
	{
		public class SharpenMaskInfo
		{
			public static string NoMask
			{
				get
				{
					return ( "No Mask" );
				}
			}

			private SharpenInfo	info;
			private string			mask;

			public SharpenMaskInfo()
			{
				mask	=  "No Mask";
				info	=  new SharpenInfo();
			}

			public override string ToString()
			{
				return ( mask );
			}


			public SharpenInfo Info
			{
				get
				{
					return ( info );
				}

				set
				{
					info	=  value;
				}
			}

			public string Mask
			{
				get
				{
					return ( mask );
				}

				set
				{
					mask	=  value;
				}
			}
		}

		private SharpenInfosArray	imagesSharpenInfoAr;
		private SharpenInfosArray	thumbsSharpenInfoAr;
		private ArrayList				masksStrings;
		private int						lastSelIndex;

		private System.Windows.Forms.CheckedListBox chklbMasksNames;
		private System.Windows.Forms.Label stMasksContaining;
		private System.Windows.Forms.GroupBox grpMasks;
		private System.ComponentModel.IContainer components = null;

		public SharpenCtrlMasks()
		{
			imagesSharpenInfoAr	=  new SharpenInfosArray();
			thumbsSharpenInfoAr	=  new SharpenInfosArray();
			lastSelIndex			=  -1;

			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// Get the mask strings.
			masksStrings	=  GetMaskStrings();
		}

		protected ArrayList GetMaskStrings()
		{
			ArrayList	masks	=  new ArrayList( 4 );

			masks.Add( "No Mask" );
			masks.Add( "Sky" );
			masks.Add( "Land" );
			masks.Add( "Water" );

			return ( masks );
		}

		protected void LoadMaskItems( SharpenInfosArray sharpenInfosAr)
		{
			// Create the necessary SharpenMaskInfos.
			SharpenMaskInfo[]	maskInfos	=  new SharpenMaskInfo[ masksStrings.Count ];

			for ( int i= 0;  i< masksStrings.Count;  i++ )
			{
				SharpenMaskInfo	info	=  new SharpenMaskInfo();

				info.Mask	=  (string) masksStrings[ i ];
				info.Info	=  sharpenInfosAr[ i ];

				info.Info.maskString	=  info.Mask;

				maskInfos.SetValue( info, i );
			}

			// Add to the listbox.
			chklbMasksNames.DataSource		=  maskInfos;
			chklbMasksNames.DisplayMember	=  "Mask";
		}

		protected void UnloadMaskItems( SharpenInfosArray sharpenInfosAr )
		{
			for ( int i= 0;  i< this.chklbMasksNames.Items.Count;  i++ )
			{
				SharpenMaskInfo	maskInfo	=  ( ( SharpenMaskInfo[] ) chklbMasksNames.DataSource )[ i ];
				SharpenInfo			info		=  maskInfo.Info;

				sharpenInfosAr[ i ]	=  info;
			}
		}

		public override void LoadDefaults( DefaultsAr defaultsAr )
		{
			for ( int i= 0;  i< masksStrings.Count;  i++ )
			{
				SharpenInfo	imagesInfo	=  new SharpenInfo();
				SharpenInfo	thumbsInfo	=  new SharpenInfo();

				imagesInfo.Load( defaultsAr, "Images"+masksStrings[ i ] );
				thumbsInfo.Load( defaultsAr, "Thumbnails"+masksStrings[ i ] );

				imagesSharpenInfoAr.Add( imagesInfo );
				thumbsSharpenInfoAr.Add( thumbsInfo );
			}

			// Load the list box.
			LoadMaskItems( imagesSharpenInfoAr );
		}

		public override void SaveDefaults( DefaultsAr defaultsAr )
		{
			base.SaveDefaults (defaultsAr);
		}

		public override SharpenInfo LoadSharpenDataForImages()
		{
			//if ( transferToDlg && rbSharpenImages.Checked )
			//{
			//   // Transfer to Dialog
			//   // Load the list box.
			//   LoadMaskItems( imagesSharpenInfoAr );

			//   // Use the first one in the listbox
			//   imagesSharpenInfo	=  imagesSharpenInfoAr[ 0 ];

			//   LoadSharpenData( imagesSharpenInfo, transferToDlg );

			//   sharpenImagesNeedTransferringFrom	=  true;
			//}
			//else if ( !transferToDlg && sharpenThumbsNeedTransferringFrom )
			//{
			//   // Transfer from Dialog.
			//   UnloadMaskItems( imagesSharpenInfoAr );

			//   base.LoadSharpenDataForImages( transferToDlg );
			//}

			return ( imagesSharpenInfo );
		}

		public override SharpenInfo LoadSharpenDataForThumbs()
		{
			//if ( transferToDlg && rbSharpenThumbs.Checked )
			//{
			//   // Transfer to Dialog
			//   // Load the list box.
			//   LoadMaskItems( thumbsSharpenInfoAr );

			//   // Use the first one in the listbox
			//   thumbsSharpenInfo	=  thumbsSharpenInfoAr[ 0 ];

			//   LoadSharpenData( thumbsSharpenInfo, transferToDlg );

			//   sharpenThumbsNeedTransferringFrom	=  true;
			//}
			//else if ( !transferToDlg && sharpenThumbsNeedTransferringFrom )
			//{
			//   // Transfer from Dialog.
			//   UnloadMaskItems( thumbsSharpenInfoAr );

			//   base.LoadSharpenDataForThumbs( transferToDlg );
			//}

			return ( thumbsSharpenInfo );
		}

		/***public override void LoadSharpenData( SharpenInfo sharpenInfo, bool transferToDlg )
		{
			base.LoadSharpenData (sharpenInfo, transferToDlg);
		}***/


		private void chklbMasksNames_SelectedIndexChanged( object sender, System.EventArgs e )
		{
			// Load up values for current selection (if any).
			int	whichOne		=  chklbMasksNames.SelectedIndex;
			bool	isChecked	=  chklbMasksNames.GetItemCheckState( whichOne ) == CheckState.Checked;

			if ( !isChecked )
			{
			}

			// Save current values which "must" be for thumbnails.
			// Load up sharpen info for images.
			if ( lastSelIndex != whichOne )
			{
				// Save the values for the current mask, then transfer into the dialog the
				// values for the newly selected one.
				SharpenMaskInfo	maskInfo;
				SharpenInfo			info;

				if ( lastSelIndex > -1 )
				{
					// Transfer from the dialog into the infos for the last item selected.
					maskInfo	=  ( ( SharpenMaskInfo[] ) chklbMasksNames.DataSource )[ lastSelIndex ];
					info		=  maskInfo.Info;

					info	=  LoadSharpenData();	// From dialog into data structure for "old" selection.
				}

				// Transfer from the infos into the dialog for the new item selected.
				maskInfo	=  ( ( SharpenMaskInfo[] ) chklbMasksNames.DataSource )[ whichOne ];
				info		=  maskInfo.Info;

				//***LoadSharpenData( info, true );	// From data structure into the dialog for "new" selection.
			}

			// Save which one is last selected.
			lastSelIndex	=  whichOne;
		}

		private void chklbMasksNames_ItemCheck( object sender, System.Windows.Forms.ItemCheckEventArgs e )
		{
			SharpenMaskInfo	maskInfo;
			SharpenInfo			info;

			// Get the infos for this item in the check listbox.
			maskInfo	=  ( ( SharpenMaskInfo[] ) chklbMasksNames.DataSource )[ e.Index ];
			info		=  maskInfo.Info;

			// Don't allow check if there is None sharpen type!
			if ( info.sharpenType == SharpenInfo.SharpenType.None && e.NewValue == CheckState.Checked )
			{
				chklbMasksNames.SetItemCheckState( e.Index, CheckState.Unchecked );

				info.useMask	=  false;
			}
			else
				info.useMask	=  e.NewValue == CheckState.Checked;
		}

		private void rbSharpenImages_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( rbSharpenImages.Checked )
			{

				// Save current values which "must" be for thumbnails.
				// Load up sharpen info for images.
				//LoadSharpenDataForThumbs( false );	// From dialog into data structure.
				//LoadSharpenDataForImages( true );	// From data structure into the dialog
			}
		}

		private void rbSharpenThumbs_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( rbSharpenThumbs.Checked )
			{

				// Save current values which "must" be for thumbnails.
				// Load up sharpen info for images.
				//LoadSharpenDataForThumbs( true );	// From dialog into data structure.
				//LoadSharpenDataForImages( false );	// From data structure into the dialog
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.chklbMasksNames = new System.Windows.Forms.CheckedListBox();
			this.stMasksContaining = new System.Windows.Forms.Label();
			this.grpMasks = new System.Windows.Forms.GroupBox();
			this.grpSharpen.SuspendLayout();
			this.grpSharpenChoices.SuspendLayout();
			this.grpSharpenOptions.SuspendLayout();
			this.grpMasks.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpSharpen
			// 
			this.grpSharpen.Controls.Add(this.grpMasks);
			this.grpSharpen.Name = "grpSharpen";
			this.grpSharpen.Size = new System.Drawing.Size(328, 158);
			this.grpSharpen.Controls.SetChildIndex(this.grpMasks, 0);
			this.grpSharpen.Controls.SetChildIndex(this.grpSharpenOptions, 0);
			this.grpSharpen.Controls.SetChildIndex(this.grpSharpenChoices, 0);
			this.grpSharpen.Controls.SetChildIndex(this.rbSharpenImages, 0);
			this.grpSharpen.Controls.SetChildIndex(this.rbSharpenThumbs, 0);
			// 
			// rbSharpenImages
			// 
			this.rbSharpenImages.Location = new System.Drawing.Point(67, 132);
			this.rbSharpenImages.Name = "rbSharpenImages";
			this.rbSharpenImages.CheckedChanged += new System.EventHandler(this.rbSharpenImages_CheckedChanged);
			// 
			// rbSharpenThumbs
			// 
			this.rbSharpenThumbs.Location = new System.Drawing.Point(180, 132);
			this.rbSharpenThumbs.Name = "rbSharpenThumbs";
			this.rbSharpenThumbs.CheckedChanged += new System.EventHandler(this.rbSharpenThumbs_CheckedChanged);
			// 
			// grpSharpenChoices
			// 
			this.grpSharpenChoices.Location = new System.Drawing.Point(8, 19);
			this.grpSharpenChoices.Name = "grpSharpenChoices";
			// 
			// rbSharpenNone
			// 
			this.rbSharpenNone.Name = "rbSharpenNone";
			// 
			// rbSharpenUnsharpMask
			// 
			this.rbSharpenUnsharpMask.Name = "rbSharpenUnsharpMask";
			// 
			// rbSharpenNik
			// 
			this.rbSharpenNik.Name = "rbSharpenNik";
			// 
			// grpSharpenOptions
			// 
			this.grpSharpenOptions.Location = new System.Drawing.Point(8, 58);
			this.grpSharpenOptions.Name = "grpSharpenOptions";
			// 
			// cbSharpenOptionsNikProfileType
			// 
			this.cbSharpenOptionsNikProfileType.Name = "cbSharpenOptionsNikProfileType";
			this.cbSharpenOptionsNikProfileType.Size = new System.Drawing.Size(100, 21);
			// 
			// edtSharpenOptionsThreshhold
			// 
			this.edtSharpenOptionsThreshhold.Name = "edtSharpenOptionsThreshhold";
			// 
			// edtSharpenOptionsRadius
			// 
			this.edtSharpenOptionsRadius.Name = "edtSharpenOptionsRadius";
			// 
			// stSharpenOptionsThreshhold
			// 
			this.stSharpenOptionsThreshhold.Name = "stSharpenOptionsThreshhold";
			// 
			// stSharpenOptionsRadius
			// 
			this.stSharpenOptionsRadius.Name = "stSharpenOptionsRadius";
			// 
			// edtSharpenOptionsAmount
			// 
			this.edtSharpenOptionsAmount.Name = "edtSharpenOptionsAmount";
			// 
			// stSharpenOptionsAmount
			// 
			this.stSharpenOptionsAmount.Name = "stSharpenOptionsAmount";
			// 
			// stSharpenOptionsNikProfileType
			// 
			this.stSharpenOptionsNikProfileType.Name = "stSharpenOptionsNikProfileType";
			// 
			// chklbMasksNames
			// 
			this.chklbMasksNames.AllowDrop = true;
			this.chklbMasksNames.Location = new System.Drawing.Point(10, 34);
			this.chklbMasksNames.Name = "chklbMasksNames";
			this.chklbMasksNames.Size = new System.Drawing.Size(71, 64);
			this.chklbMasksNames.TabIndex = 13;
			this.chklbMasksNames.ThreeDCheckBoxes = true;
			this.chklbMasksNames.SelectedIndexChanged += new System.EventHandler(this.chklbMasksNames_SelectedIndexChanged);
			this.chklbMasksNames.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chklbMasksNames_ItemCheck);
			// 
			// stMasksContaining
			// 
			this.stMasksContaining.Location = new System.Drawing.Point(10, 19);
			this.stMasksContaining.Name = "stMasksContaining";
			this.stMasksContaining.Size = new System.Drawing.Size(61, 13);
			this.stMasksContaining.TabIndex = 17;
			this.stMasksContaining.Text = "Containing";
			// 
			// grpMasks
			// 
			this.grpMasks.Controls.Add(this.stMasksContaining);
			this.grpMasks.Controls.Add(this.chklbMasksNames);
			this.grpMasks.Location = new System.Drawing.Point(225, 12);
			this.grpMasks.Name = "grpMasks";
			this.grpMasks.Size = new System.Drawing.Size(93, 110);
			this.grpMasks.TabIndex = 18;
			this.grpMasks.TabStop = false;
			this.grpMasks.Text = "Masks";
			// 
			// SharpenCtrlMasks
			// 
			this.Name = "SharpenCtrlMasks";
			this.Size = new System.Drawing.Size(328, 158);
			this.grpSharpen.ResumeLayout(false);
			this.grpSharpenChoices.ResumeLayout(false);
			this.grpSharpenOptions.ResumeLayout(false);
			this.grpMasks.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}

