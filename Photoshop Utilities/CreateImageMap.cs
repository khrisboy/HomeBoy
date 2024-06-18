using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using MyControls;
using MyClasses;
using Photoshop;
using PhotoshopSupport;
using Photoshopper;

namespace PhotoshopUtilities
{
	/// <summary>
	/// Summary description for CreateImageMap.
	/// </summary>
	public class CreateImageMap : MyWindowsForm
	{
		#region Member variables.
		private System.Windows.Forms.Label stMapWidth;
		private System.Windows.Forms.TextBox edtMapWidth;
		private System.Windows.Forms.Label stImageHeight;
		private System.Windows.Forms.ListBox lbImages;
		private System.Windows.Forms.Label stImages;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnCreate;
		private MyControls.BrowseForDirectory browseForDirectory;
		private System.Windows.Forms.Label stMapFileName;
		private System.Windows.Forms.TextBox edtMapFileName;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.GroupBox grpMap;
		private System.Windows.Forms.CheckBox chkMapTrim;
		private System.Windows.Forms.CheckBox chkMapEqualCells;
		private System.Windows.Forms.Label stFrameWidth;
		private System.Windows.Forms.Label stFrameWidthPixels;
		private System.Windows.Forms.TextBox edtCellWidth;
		private System.Windows.Forms.CheckBox chkTrimFrame;
		private System.Windows.Forms.CheckBox chkResize;
		private System.Windows.Forms.TextBox edtFrameWidth;
		private System.Windows.Forms.TextBox edtResizeSize;
		private System.Windows.Forms.Label stResizeSize;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Label stMapBacgroundColor;
		private System.Windows.Forms.TextBox edtMapBacgroundColor;
		private System.Windows.Forms.Button btnMapBacgroundColor;
		private System.Windows.Forms.CheckBox chkResizeSharpen;
		private System.Windows.Forms.CheckBox chkFilesSort;
		private System.Windows.Forms.GroupBox grpFiles;
		private System.Windows.Forms.GroupBox grpResize;
		private System.Windows.Forms.Label stResizePx;
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components	=  null;

		public CreateImageMap()
		{
			InitializeComponent();

			// Configure the browser control.
			browseForDirectory.LabelIsVisible	=  true;
			browseForDirectory.Label				=  "Output Directory";
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.stMapWidth = new System.Windows.Forms.Label();
			this.edtMapWidth = new System.Windows.Forms.TextBox();
			this.stImageHeight = new System.Windows.Forms.Label();
			this.edtCellWidth = new System.Windows.Forms.TextBox();
			this.grpFiles = new System.Windows.Forms.GroupBox();
			this.chkFilesSort = new System.Windows.Forms.CheckBox();
			this.grpResize = new System.Windows.Forms.GroupBox();
			this.chkResizeSharpen = new System.Windows.Forms.CheckBox();
			this.chkResize = new System.Windows.Forms.CheckBox();
			this.edtResizeSize = new System.Windows.Forms.TextBox();
			this.stResizeSize = new System.Windows.Forms.Label();
			this.stResizePx = new System.Windows.Forms.Label();
			this.stFrameWidthPixels = new System.Windows.Forms.Label();
			this.edtFrameWidth = new System.Windows.Forms.TextBox();
			this.stFrameWidth = new System.Windows.Forms.Label();
			this.chkTrimFrame = new System.Windows.Forms.CheckBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.lbImages = new System.Windows.Forms.ListBox();
			this.edtMapFileName = new System.Windows.Forms.TextBox();
			this.stMapFileName = new System.Windows.Forms.Label();
			this.browseForDirectory = new MyControls.BrowseForDirectory();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.stImages = new System.Windows.Forms.Label();
			this.btnCreate = new System.Windows.Forms.Button();
			this.grpMap = new System.Windows.Forms.GroupBox();
			this.btnMapBacgroundColor = new System.Windows.Forms.Button();
			this.edtMapBacgroundColor = new System.Windows.Forms.TextBox();
			this.stMapBacgroundColor = new System.Windows.Forms.Label();
			this.chkMapEqualCells = new System.Windows.Forms.CheckBox();
			this.chkMapTrim = new System.Windows.Forms.CheckBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.grpFiles.SuspendLayout();
			this.grpResize.SuspendLayout();
			this.grpMap.SuspendLayout();
			this.SuspendLayout();
			// 
			// stMapWidth
			// 
			this.stMapWidth.Location = new System.Drawing.Point( 16, 24 );
			this.stMapWidth.Name = "stMapWidth";
			this.stMapWidth.Size = new System.Drawing.Size( 60, 12 );
			this.stMapWidth.TabIndex = 0;
			this.stMapWidth.Text = "Width";
			// 
			// edtMapWidth
			// 
			this.edtMapWidth.Location = new System.Drawing.Point( 16, 40 );
			this.edtMapWidth.Name = "edtMapWidth";
			this.edtMapWidth.Size = new System.Drawing.Size( 84, 20 );
			this.edtMapWidth.TabIndex = 1;
			// 
			// stImageHeight
			// 
			this.stImageHeight.Location = new System.Drawing.Point( 125, 24 );
			this.stImageHeight.Name = "stImageHeight";
			this.stImageHeight.Size = new System.Drawing.Size( 100, 12 );
			this.stImageHeight.TabIndex = 2;
			this.stImageHeight.Text = "Cell Width";
			// 
			// edtCellWidth
			// 
			this.edtCellWidth.Location = new System.Drawing.Point( 125, 40 );
			this.edtCellWidth.Name = "edtCellWidth";
			this.edtCellWidth.Size = new System.Drawing.Size( 84, 20 );
			this.edtCellWidth.TabIndex = 3;
			// 
			// grpFiles
			// 
			this.grpFiles.Controls.Add( this.chkFilesSort );
			this.grpFiles.Controls.Add( this.grpResize );
			this.grpFiles.Controls.Add( this.stFrameWidthPixels );
			this.grpFiles.Controls.Add( this.edtFrameWidth );
			this.grpFiles.Controls.Add( this.stFrameWidth );
			this.grpFiles.Controls.Add( this.chkTrimFrame );
			this.grpFiles.Controls.Add( this.btnClear );
			this.grpFiles.Controls.Add( this.lbImages );
			this.grpFiles.Controls.Add( this.edtMapFileName );
			this.grpFiles.Controls.Add( this.stMapFileName );
			this.grpFiles.Controls.Add( this.browseForDirectory );
			this.grpFiles.Controls.Add( this.btnRemove );
			this.grpFiles.Controls.Add( this.btnBrowse );
			this.grpFiles.Controls.Add( this.stImages );
			this.grpFiles.Location = new System.Drawing.Point( 16, 130 );
			this.grpFiles.Name = "grpFiles";
			this.grpFiles.Size = new System.Drawing.Size( 469, 399 );
			this.grpFiles.TabIndex = 5;
			this.grpFiles.TabStop = false;
			this.grpFiles.Text = "Files";
			// 
			// chkFilesSort
			// 
			this.chkFilesSort.Location = new System.Drawing.Point( 371, 198 );
			this.chkFilesSort.Name = "chkFilesSort";
			this.chkFilesSort.Size = new System.Drawing.Size( 49, 16 );
			this.chkFilesSort.TabIndex = 13;
			this.chkFilesSort.Text = "Sort";
			// 
			// grpResize
			// 
			this.grpResize.Controls.Add( this.chkResizeSharpen );
			this.grpResize.Controls.Add( this.chkResize );
			this.grpResize.Controls.Add( this.edtResizeSize );
			this.grpResize.Controls.Add( this.stResizeSize );
			this.grpResize.Controls.Add( this.stResizePx );
			this.grpResize.Location = new System.Drawing.Point( 316, 241 );
			this.grpResize.Name = "grpResize";
			this.grpResize.Size = new System.Drawing.Size( 139, 144 );
			this.grpResize.TabIndex = 12;
			this.grpResize.TabStop = false;
			this.grpResize.Text = "Resize";
			// 
			// chkResizeSharpen
			// 
			this.chkResizeSharpen.Location = new System.Drawing.Point( 32, 112 );
			this.chkResizeSharpen.Name = "chkResizeSharpen";
			this.chkResizeSharpen.Size = new System.Drawing.Size( 69, 16 );
			this.chkResizeSharpen.TabIndex = 14;
			this.chkResizeSharpen.Text = "Sharpen";
			// 
			// chkResize
			// 
			this.chkResize.Location = new System.Drawing.Point( 32, 27 );
			this.chkResize.Name = "chkResize";
			this.chkResize.Size = new System.Drawing.Size( 69, 18 );
			this.chkResize.TabIndex = 0;
			this.chkResize.Text = "Resize";
			this.chkResize.CheckedChanged += new System.EventHandler( this.chkResize_CheckedChanged );
			// 
			// edtResizeSize
			// 
			this.edtResizeSize.Location = new System.Drawing.Point( 18, 75 );
			this.edtResizeSize.Name = "edtResizeSize";
			this.edtResizeSize.Size = new System.Drawing.Size( 68, 20 );
			this.edtResizeSize.TabIndex = 6;
			// 
			// stResizeSize
			// 
			this.stResizeSize.Location = new System.Drawing.Point( 18, 59 );
			this.stResizeSize.Name = "stResizeSize";
			this.stResizeSize.Size = new System.Drawing.Size( 60, 12 );
			this.stResizeSize.TabIndex = 5;
			this.stResizeSize.Text = "Size";
			// 
			// stResizePx
			// 
			this.stResizePx.Location = new System.Drawing.Point( 92, 78 );
			this.stResizePx.Name = "stResizePx";
			this.stResizePx.Size = new System.Drawing.Size( 17, 14 );
			this.stResizePx.TabIndex = 13;
			this.stResizePx.Text = "px";
			// 
			// stFrameWidthPixels
			// 
			this.stFrameWidthPixels.Location = new System.Drawing.Point( 194, 358 );
			this.stFrameWidthPixels.Name = "stFrameWidthPixels";
			this.stFrameWidthPixels.Size = new System.Drawing.Size( 31, 14 );
			this.stFrameWidthPixels.TabIndex = 11;
			this.stFrameWidthPixels.Text = "px";
			// 
			// edtFrameWidth
			// 
			this.edtFrameWidth.Location = new System.Drawing.Point( 120, 356 );
			this.edtFrameWidth.Name = "edtFrameWidth";
			this.edtFrameWidth.Size = new System.Drawing.Size( 67, 20 );
			this.edtFrameWidth.TabIndex = 10;
			// 
			// stFrameWidth
			// 
			this.stFrameWidth.Location = new System.Drawing.Point( 120, 340 );
			this.stFrameWidth.Name = "stFrameWidth";
			this.stFrameWidth.Size = new System.Drawing.Size( 70, 12 );
			this.stFrameWidth.TabIndex = 9;
			this.stFrameWidth.Text = "Frame Width";
			// 
			// chkTrimFrame
			// 
			this.chkTrimFrame.Location = new System.Drawing.Point( 26, 358 );
			this.chkTrimFrame.Name = "chkTrimFrame";
			this.chkTrimFrame.Size = new System.Drawing.Size( 82, 16 );
			this.chkTrimFrame.TabIndex = 8;
			this.chkTrimFrame.Text = "Trim Frame";
			this.chkTrimFrame.CheckedChanged += new System.EventHandler( this.chjTrimFrame_CheckedChanged );
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point( 354, 153 );
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size( 75, 23 );
			this.btnClear.TabIndex = 7;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler( this.btnClear_Click );
			// 
			// lbImages
			// 
			this.lbImages.AllowDrop = true;
			this.lbImages.Location = new System.Drawing.Point( 16, 46 );
			this.lbImages.Name = "lbImages";
			this.lbImages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbImages.Size = new System.Drawing.Size( 296, 173 );
			this.lbImages.TabIndex = 0;
			this.lbImages.DragEnter += new System.Windows.Forms.DragEventHandler( this.lbImages_DragEnter );
			this.lbImages.DragDrop += new System.Windows.Forms.DragEventHandler( this.lbImages_DragDrop );
			// 
			// edtMapFileName
			// 
			this.edtMapFileName.AllowDrop = true;
			this.edtMapFileName.Location = new System.Drawing.Point( 16, 300 );
			this.edtMapFileName.Name = "edtMapFileName";
			this.edtMapFileName.Size = new System.Drawing.Size( 232, 20 );
			this.edtMapFileName.TabIndex = 6;
			// 
			// stMapFileName
			// 
			this.stMapFileName.Location = new System.Drawing.Point( 16, 283 );
			this.stMapFileName.Name = "stMapFileName";
			this.stMapFileName.Size = new System.Drawing.Size( 233, 14 );
			this.stMapFileName.TabIndex = 5;
			this.stMapFileName.Text = "Map File Name";
			// 
			// browseForDirectory
			// 
			this.browseForDirectory.BrowseLabel = "...";
			this.browseForDirectory.Directory = "";
			this.browseForDirectory.Label = "";
			this.browseForDirectory.Location = new System.Drawing.Point( 16, 230 );
			this.browseForDirectory.Name = "browseForDirectory";
			this.browseForDirectory.Size = new System.Drawing.Size( 272, 38 );
			this.browseForDirectory.TabIndex = 4;
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point( 354, 108 );
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size( 75, 23 );
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler( this.btnRemove_Click );
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point( 354, 63 );
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size( 75, 23 );
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.Click += new System.EventHandler( this.btnBrowse_Click );
			// 
			// stImages
			// 
			this.stImages.Location = new System.Drawing.Point( 16, 30 );
			this.stImages.Name = "stImages";
			this.stImages.Size = new System.Drawing.Size( 100, 12 );
			this.stImages.TabIndex = 1;
			this.stImages.Text = "Image Files";
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point( 390, 57 );
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size( 75, 23 );
			this.btnCreate.TabIndex = 6;
			this.btnCreate.Text = "Create";
			this.btnCreate.Click += new System.EventHandler( this.btnCreate_Click );
			// 
			// grpMap
			// 
			this.grpMap.Controls.Add( this.btnMapBacgroundColor );
			this.grpMap.Controls.Add( this.edtMapBacgroundColor );
			this.grpMap.Controls.Add( this.stMapBacgroundColor );
			this.grpMap.Controls.Add( this.chkMapEqualCells );
			this.grpMap.Controls.Add( this.chkMapTrim );
			this.grpMap.Controls.Add( this.stMapWidth );
			this.grpMap.Controls.Add( this.edtMapWidth );
			this.grpMap.Controls.Add( this.stImageHeight );
			this.grpMap.Controls.Add( this.edtCellWidth );
			this.grpMap.Location = new System.Drawing.Point( 16, 12 );
			this.grpMap.Name = "grpMap";
			this.grpMap.Size = new System.Drawing.Size( 342, 107 );
			this.grpMap.TabIndex = 7;
			this.grpMap.TabStop = false;
			this.grpMap.Text = "Map";
			// 
			// btnMapBacgroundColor
			// 
			this.btnMapBacgroundColor.Location = new System.Drawing.Point( 260, 72 );
			this.btnMapBacgroundColor.Name = "btnMapBacgroundColor";
			this.btnMapBacgroundColor.Size = new System.Drawing.Size( 26, 23 );
			this.btnMapBacgroundColor.TabIndex = 7;
			this.btnMapBacgroundColor.Text = "...";
			this.btnMapBacgroundColor.Click += new System.EventHandler( this.btnMapBacgroundColor_Click );
			// 
			// edtMapBacgroundColor
			// 
			this.edtMapBacgroundColor.Location = new System.Drawing.Point( 230, 40 );
			this.edtMapBacgroundColor.Name = "edtMapBacgroundColor";
			this.edtMapBacgroundColor.Size = new System.Drawing.Size( 84, 20 );
			this.edtMapBacgroundColor.TabIndex = 6;
			// 
			// stMapBacgroundColor
			// 
			this.stMapBacgroundColor.Location = new System.Drawing.Point( 230, 24 );
			this.stMapBacgroundColor.Name = "stMapBacgroundColor";
			this.stMapBacgroundColor.Size = new System.Drawing.Size( 95, 12 );
			this.stMapBacgroundColor.TabIndex = 5;
			this.stMapBacgroundColor.Text = "Background Color";
			// 
			// chkMapEqualCells
			// 
			this.chkMapEqualCells.Location = new System.Drawing.Point( 125, 76 );
			this.chkMapEqualCells.Name = "chkMapEqualCells";
			this.chkMapEqualCells.Size = new System.Drawing.Size( 84, 16 );
			this.chkMapEqualCells.TabIndex = 4;
			this.chkMapEqualCells.Text = "Make Equal";
			this.chkMapEqualCells.CheckedChanged += new System.EventHandler( this.chkMapEqualCells_CheckedChanged );
			// 
			// chkMapTrim
			// 
			this.chkMapTrim.Location = new System.Drawing.Point( 33, 76 );
			this.chkMapTrim.Name = "chkMapTrim";
			this.chkMapTrim.Size = new System.Drawing.Size( 46, 16 );
			this.chkMapTrim.TabIndex = 2;
			this.chkMapTrim.Text = "Trim";
			// 
			// CreateImageMap
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 502, 546 );
			this.Controls.Add( this.grpMap );
			this.Controls.Add( this.btnCreate );
			this.Controls.Add( this.grpFiles );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CreateImageMap";
			this.Text = "Create Image Map";
			this.Load += new System.EventHandler( this.CreateImageMap_Load );
			this.grpFiles.ResumeLayout( false );
			this.grpFiles.PerformLayout();
			this.grpResize.ResumeLayout( false );
			this.grpResize.PerformLayout();
			this.grpMap.ResumeLayout( false );
			this.grpMap.PerformLayout();
			this.ResumeLayout( false );

		}
		#endregion

		private void CreateImageMap_Load( object sender, System.EventArgs e )
		{
			// Allow drop on the listbox.
			lbImages.AllowDrop	=  true;

			// Set the data source for the listbox.
			ArrayListUnique	list	=  new ArrayListUnique();

			lbImages.DataSource		=  list;

			// Synch.
			chkMapEqualCells_CheckedChanged( sender, e );
			chjTrimFrame_CheckedChanged( sender, e );
			chkResize_CheckedChanged( sender, e );
		}


		#region File buttons processing.
		private void btnBrowse_Click( object sender, System.EventArgs e )
		{
			// Browse for files to include.
		}

		private void btnRemove_Click( object sender, System.EventArgs e )
		{
			// Remove selected items from the listbox.
			ListBox.SelectedIndexCollection	sel	=  lbImages.SelectedIndices;

			// Transfer to an int array.
			int[]	indexes	=  new int[ sel.Count ];

			for ( int i= 0;  i< sel.Count;  i++ )
				indexes[ i ]	=  sel[ i ];

			// Clear 'em out.
			ClearChildren( indexes );
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			// Clear out the listbox.
			// Create an int array with all the indices.
			int[]	indexes	=  new int[ lbImages.Items.Count ];

			for ( int i= 0;  i< lbImages.Items.Count;  i++ )
				indexes[ i ]	=  i;

			// Clear 'em out.
			ClearChildren( indexes );
		}

		private void ClearChildren( int[] indexes )
		{
			try
			{
				// Remove selected items from the listbox.
				// Assume that the indices are in ascending order.
				ArrayListUnique	list	=  (ArrayListUnique) lbImages.DataSource;

				for ( int i= indexes.Length-1;  i>= 0;  i-- )
				{
					int	selIndex	=  indexes[ i ];

					list.RemoveAt( selIndex );
				}

				// Reset.
				lbImages.DataSource		=  null;
				lbImages.DataSource		=  list;
				lbImages.DisplayMember	=  "Name";
				lbImages.ValueMember		=  "FullName";
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		#endregion

		private void btnCreate_Click( object sender, System.EventArgs e )
		{
			// Get the parameters.
			int		mapWidth			=  edtMapWidth.Text != "" ?  int.Parse( edtMapWidth.Text ) : 500;
			int		resizeSize		=  chkResize.Checked ?  ( edtResizeSize.Text != "" ?  int.Parse( edtResizeSize.Text ) : 0 ) : 0;
			bool		resizeSharpen	=  chkResize.Checked ?  chkResizeSharpen.Checked : false;
			int		frameWidth		=  chkTrimFrame.Checked ?  ( edtFrameWidth.Text != "" ?  int.Parse( edtFrameWidth.Text ) : 0 ) : 0;
			string	outputDir		=  browseForDirectory.Directory;
			string	mapFile			=  edtMapFileName.Text == "" ?  "ImageMap" : edtMapFileName.Text;

			// Get the list of files into an array.
			ArrayListUnique	filesAr	=  (ArrayListUnique) lbImages.DataSource;

			// Make sure they're sorted by name?
			if ( chkFilesSort.Checked )
				filesAr.Sort();

			// Create a Photoshopper object.
			Photoshopper.Photoshopper	ps	=  new Photoshopper.Photoshopper( new Photoshop.Application() );

			// Save preferences.
			PsUnits	originalRulerUnits	=  ps.theApp.Preferences.RulerUnits;
			
			// Set ruler units to pixels
			ps.theApp.Preferences.RulerUnits	=  PsUnits.psPixels;

			// No dialogs.
			ps.theApp.DisplayDialogs	=  Photoshop.PsDialogModes.psDisplayNoDialogs;
			
			// Prepare the images for the map.
			MakeRankingImages( ps, frameWidth, resizeSize, resizeSharpen, outputDir, filesAr, true );

			// Set background color of the map.
			RgbColor		backgroundClr	=  new RgbColor( edtMapBacgroundColor.Text );
			SolidColor	newBackground	=  new SolidColor();

			newBackground.RGB.Red	=  backgroundClr.R;
			newBackground.RGB.Green	=  backgroundClr.G;
			newBackground.RGB.Blue	=  backgroundClr.B;

			// Again, make sure they're sorted by name?
			if ( chkFilesSort.Checked )
				filesAr.Sort();

			// Create the map.
			bool	equalCells	=  chkMapEqualCells.Checked;
			int	cellWidth	=  int.Parse( edtCellWidth.Text );

			CreateMap( ps, mapWidth, newBackground, equalCells, cellWidth, outputDir, mapFile, filesAr );

			// Restore preferences.
			ps.theApp.Preferences.RulerUnits	=  originalRulerUnits;
		}
		
		public void MakeRankingImages( Photoshopper.Photoshopper ps, int frameWidth, int resizeSize, bool resizeSharpen, string outputDir, ArrayList filesAr, bool removeFrame )
		{
			try
			{
				// Nothing to do if no frame and no resizing.
				if ( frameWidth > 0 || resizeSize > 0 )
				{
					for ( int i= 0;  i< filesAr.Count;  i++ )
					{
						// Create new document.
						Document	imageDoc	=  ps.OpenDoc( ( (MyFileInfo) filesAr[ i ] ).FullName );

						// Select just the picture
						double	w				=  imageDoc.Width - 2*frameWidth;
						double	h				=  imageDoc.Height - 2*frameWidth;
						double[]	selCorners	=  new double[4] { frameWidth, frameWidth, imageDoc.Width - frameWidth, imageDoc.Height - frameWidth };
						Bounds	selBounds	=  new Bounds( selCorners );

						// Crop it.
						ps.SelectRectArea( selBounds, PsSelectionType.psReplaceSelection, 0.0 );
						ps.Crop();

						// Deselect.
						imageDoc.Selection.Deselect();

						bool	isPanorama	=  imageDoc.Width / imageDoc.Height >= 2.0;

						// Resize?
						if ( resizeSize > 0 )
						{
							// Only resize if we really need to.
							if ( imageDoc.Width >= imageDoc.Height && (int) resizeSize != (int)imageDoc.Height ||
								imageDoc.Width < imageDoc.Height && (int) resizeSize != (int)imageDoc.Width )
							{
								if ( isPanorama )
								{
									imageDoc.ResizeImage( null, resizeSize, 96.0, PsResampleMethod.psBicubic );
								}
								else
								{
									if ( imageDoc.Width > imageDoc.Height )
										imageDoc.ResizeImage( null, resizeSize, 96.0, PsResampleMethod.psBicubic );
									else if ( (int) resizeSize != (int)imageDoc.Width )
										imageDoc.ResizeImage( resizeSize, null, 96.0, PsResampleMethod.psBicubic );
								}

								if ( resizeSharpen )
								{
									// Sharpen.
									NikDisplaySharpenInfo	options	=  new NikDisplaySharpenInfo();

									options.ProfileType	=  NikSharpenInfo.NikProfileType.John;

									ps.NikSharpen2Display( options );
								}
							}
						}

						// Save to images directory.
						ps.SaveAsJPEG( imageDoc, outputDir, "", 12 );

						// Reset the MyFileInfo.
						MyFileInfo	infoNew	=  new MyFileInfo( ( (MyFileInfo) filesAr[ i ] ).FullName );

						infoNew.ReplacePath( outputDir );

						filesAr[ i ]	=  infoNew;

						// Close file.
						imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
					}
				}
				else
				{
					// Copy the file over and reset the MyFileInfo.
					for ( int i= 0;  i< filesAr.Count;  i++ )
					{
						MyFileInfo	infoOrg	=  (MyFileInfo) filesAr[ i ];
						MyFileInfo	infoNew	=  new MyFileInfo( infoOrg.FullName );

						infoNew.ReplacePath( outputDir );

						infoOrg.FileInfo.CopyTo( infoNew.FullName, true );

						filesAr[ i ]	=  infoNew;
					}
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		public void CreateMap( Photoshopper.Photoshopper ps, int mapWidth, SolidColor newBackground, bool equalCells, int cellWidth, string outputPath, string mapFile, ArrayList filesAr )
		{
			try
			{
				double	lastX	=  0.0;
				double	lastY	=  0.0;
				double	maxX	=  0.0;

				// Save current foreground color.
				SolidColor	orgBackground	=  ps.theApp.BackgroundColor;
		
				// Set to new background color
				ps.theApp.BackgroundColor.RGB	=  newBackground.RGB;

				// Create new document.
				Document	imageMapDoc	=  ps.theApp.Documents.Add( mapWidth, 600.0, 96.0, mapFile, Photoshop.PsNewDocumentMode.psNewRGB, Photoshop.PsDocumentFill.psBackgroundColor, 1.0, 16, null );

				// Create file.
				string			path		=  outputPath + @"\" + mapFile + ".xml";
				FileStream		fs			=  new FileStream( path, FileMode.Create );
				StreamWriter	stream	=  new StreamWriter( fs );

				// Write the image map stuff.
				stream.WriteLine( "<map name='Image Map'>" );

				double	w		=  0.0;
				double	h		=  0.0;
				double	mapW	=  imageMapDoc.Width;
				double	mapH	=  imageMapDoc.Height;

				for ( int i= 0;  i< filesAr.Count;  i++ )
				{
					// Open document.
					Document	imageDoc	=  ps.OpenDoc( ( (MyFileInfo) filesAr[ i ] ).FullName );

					if ( equalCells )
					{
						// Cells are equal width
						if ( cellWidth > 0 )
							w	=  h	=  cellWidth;
						else
							w	=  h	=  Math.Max( imageDoc.Width, imageDoc.Height );
					}
					else
					{
						// Cells are based on longest length;
						w	=  imageDoc.Width;
						h	=  imageDoc.Height;

						// Flip vertical pics.
						if ( h > w )
						{
							imageDoc.RotateCanvas( 90.0 );

							w	=  imageDoc.Width;
							h	=  imageDoc.Height;
						}
					}

					// Are we past
					if ( lastX+w > mapW )
					{
						lastX	 =  0;
						lastY	+=  h;
					}

					// Select and copy.
					imageDoc.Selection.SelectAll();
					( (ArtLayer) ( imageDoc.ActiveLayer ) ).Copy( imageDoc.Layers.Count > 1 );

					// Make the map doc active.
					ps.theApp.ActiveDocument	=  imageMapDoc;

					// Resize height if necessary.
					if ( lastY+h > mapH )
					{
						imageMapDoc.ResizeCanvas( mapW, lastY+h, Photoshop.PsAnchorPosition.psTopCenter );

						mapH	=  lastY+h;
					}

					// Now paste into the proper location.
					double[]	selCorners	=  new double[4] { lastX, lastY, lastX+w, lastY+h };
					Bounds	selBounds	=  new Bounds( selCorners );
					ps.SelectRectArea( selBounds, PsSelectionType.psReplaceSelection, 0.0 );

					imageMapDoc.Paste( true );
					imageMapDoc.Selection.Deselect();

					// Write the image map stuff.
					stream.WriteLine( "<area shape=\"rect\" coords=\"" + lastX + ", " + lastY + ", " + ( lastX+w ) + ", " + ( lastY+h ) + "\" href=\"javascript:rateMe( '" + imageDoc.Name + "' );\" alt=\"" + imageDoc.Name + "\">" );

					lastX	+=  w;
					maxX	 =  Math.Max( maxX, lastX );

					// Close file.
					imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
				}

				// Write the image map stuff.
				stream.WriteLine( "</map>" );

				// Close the stream.
				stream.Flush();
				stream.Close();

				// Resize the canvas.
				imageMapDoc.ResizeCanvas( maxX, lastY+h, Photoshop.PsAnchorPosition.psTopLeft );

				// Save the map.
				ps.SaveAsJPEG( imageMapDoc, outputPath, mapFile, 10 );
			
				// Restore foreground color.
				ps.theApp.BackgroundColor	=  orgBackground;
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		private void lbImages_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// If the data is a file(s) display the copy cursor.
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) ) 
			{
				e.Effect	=  DragDropEffects.Copy;
			}
			else
			{
				e.Effect	=  DragDropEffects.None;
			}
		}

		private void lbImages_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// Handle FileDrop data.
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				try
				{
					// The data is a filename(s).
					string[]				files	=  (string[]) e.Data.GetData( DataFormats.FileDrop );
					ArrayListUnique	list	=  (ArrayListUnique) lbImages.DataSource;

					// Load the listbox, but only with graphics files.
					for ( int i= 0;  i< files.GetLength( 0 );  i++ )
					{
						MyFileInfo	fileInfo	=  new MyFileInfo( files[ i ] );

						if ( fileInfo.FileInfo.Extension == ".tif" || fileInfo.FileInfo.Extension == ".jpg" ||
							  fileInfo.FileInfo.Extension == ".psd" || fileInfo.FileInfo.Extension == ".psb" )
						list.Add( fileInfo );
					}

					lbImages.DataSource		=  null;
					lbImages.DataSource		=  list;
					lbImages.DisplayMember	=  "Name";
					lbImages.ValueMember		=  "FullName";
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message );
					return;
				}
			}
		}

		private void chkMapEqualCells_CheckedChanged(object sender, System.EventArgs e)
		{
			// Enable/disable cell width field
			edtCellWidth.Enabled	=  chkMapEqualCells.Checked;
		}

		private void chjTrimFrame_CheckedChanged(object sender, System.EventArgs e)
		{
			// Enable/disable frame width field
			edtFrameWidth.Enabled	=  chkTrimFrame.Checked;
		}

		private void chkResize_CheckedChanged(object sender, System.EventArgs e)
		{
			// Enable/disable resize width and sharpen fields
			edtResizeSize.Enabled		=  chkResize.Checked;
			chkResizeSharpen.Enabled	=  chkResize.Checked;
		}

		private void btnMapBacgroundColor_Click(object sender, System.EventArgs e)
		{
			colorDialog1	=  new ColorDialog();

			RgbColor	background	=  new RgbColor( edtMapBacgroundColor.Text );

			colorDialog1.Color			=  Color.FromArgb( background.R, background.G, background.B );
			colorDialog1.AnyColor		=  true;
			colorDialog1.AllowFullOpen	=  true;
			colorDialog1.CustomColors	=  new int[] { colorDialog1.Color.ToArgb() };

			if ( colorDialog1.ShowDialog() == DialogResult.OK )
			{
				Color	backgroundColor	=  colorDialog1.Color;

				edtMapBacgroundColor.Text	=  RgbColor.FromColor( backgroundColor.R, backgroundColor.G, backgroundColor. B );
			}

			colorDialog1.Dispose();
		}
	}
}
