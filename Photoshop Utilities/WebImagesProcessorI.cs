using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading;

using Photoshop;
using Photoshopper;
using PhotoshopSupport;
using WebFramerCS2ControlLibrary;
using MyClasses;
using MyControls;

namespace PhotoshopUtilities
{
	/// <summary>
	/// Summary description for DlgWebImagesProcessor.
	/// </summary>
	public class WebImagesProcessorI : MyControls.MyWindowsForm
	{
		#region Member variables.

		public delegate void SelectFileDelegate( MyFileInfo info );
		public SelectFileDelegate selectFileDelegate;

		public delegate void RefreshDelegate();
		public RefreshDelegate refreshDelegate;

		public delegate void ResetFormDelegate();
		public ResetFormDelegate resetFormDelegate;
		
		public delegate void MyEventHandlerDelegate( Object sender, EventArgs e );
		public MyEventHandlerDelegate onProcessingComplete;

		private Thread											processThread;
		private bool											processing;

		private System.Windows.Forms.GroupBox			grpImages;
		private System.Windows.Forms.CheckBox			chkImagesResize;
		private System.Windows.Forms.TextBox			edtImagesSize;
		private System.Windows.Forms.Label				stImagesPx;
		private System.Windows.Forms.Label				stImagesThumbPx;
		private System.Windows.Forms.TextBox			edtImagesThumbSize;
		private System.Windows.Forms.CheckBox			chkImagesThumb;
		private System.Windows.Forms.Button				btnProcess;
		private FrameCtrl ctrlFrame;
		private SaveAsCtrl								ctrlSaveAs;
		private MyControls.BrowseForDirectory			bfdSaveIn;
		private CheckBox								chkReproduceStructure;
		private System.Windows.Forms.GroupBox			grpColorSpace;
		private System.Windows.Forms.CheckBox			chkColorSpace;
		private RadioButton								rbColorSpaceProPhoto;
		private System.Windows.Forms.RadioButton		rbColorSpaceAdobe98;
		private System.Windows.Forms.RadioButton		rbColorSpaceSRGB;
		private RadioButton								rbColorSpaceOther;
		private TextBox									edtColorSpaceOther;
		private FilesGatherer							theFilesGatherer;
		private TabControl								theTabCtrl;
		private TabPage									tabFiles;
		private TabPage									tabImages;
		private TabPage									tabFrame;
		private NikSharpenControlDual ctrlSharpen;
		private LabelAndText ltPanoramaRatio;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container	components	=  null;

		#endregion

		public WebImagesProcessorI()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

		this.theFilesGatherer.ValidFileTypes = new List<string> {	".tif",
																	".psd",
																	".psb",
																	".jpg",
																	".bmp",
																	".png"};

			processing	=  false;

			selectFileDelegate			=  new SelectFileDelegate( SelectFile );
			refreshDelegate				=  new RefreshDelegate( RefreshMe );
			resetFormDelegate			=  new ResetFormDelegate( ResetForm );
			onProcessingComplete		=	new MyEventHandlerDelegate( Form_ProcessingComplete );
		}

		private void EnableDisable()
		{
			theTabCtrl.TabPages[ 1 ].Enabled	=  !processing;	
			theTabCtrl.TabPages[ 2 ].Enabled	=  !processing;
	
			theFilesGatherer.Enabled			=  !processing;
			bfdSaveIn.Enabled					=  !processing;
			chkReproduceStructure.Enabled		=  !processing;

		}

		void SelectFile( MyFileInfo info )
		{
			theFilesGatherer.Select( info );
		}

		void RefreshMe()
		{
			Refresh();
		}

		private void ResetForm()
		{
			// Enable/Disable pertinent controls.
			EnableDisable();

			// Flip the button text.
			btnProcess.Text	=  processing ?  "Cancel" : "Process";
		}

		private ImagesInfo LoadImagesData()
		{
			// Transfer images info.
			ImagesInfo	imagesInfo	=  new ImagesInfo();

			imagesInfo.resizeImages					  =  chkImagesResize.Checked;
			imagesInfo.resizeInfoImages.resizeRegular =  TextValueParser.GetInt( edtImagesSize, 0 );

			imagesInfo.thumbnails					  =  chkImagesThumb.Checked;
			imagesInfo.resizeInfoThumbs.resizeRegular =  TextValueParser.GetInt( edtImagesThumbSize, 0 );

			imagesInfo.saveImagesAsJPEG	=  ctrlSaveAs.SaveAsJPEG;
			imagesInfo.jpegQuality		=  ctrlSaveAs.JPEGQuality;

			return ( imagesInfo );
		}

		private ProcessorData Transfer()
		{
			ProcessorData	data	=  new ProcessorData();

			// Transfer files info.
			data.filesAr			=  theFilesGatherer.Files;
			data.saveInDirectory	=  bfdSaveIn.Directory;
			data.rootDirectory		=  theFilesGatherer.RootDirectory;
			data.reproduceStructure =  chkReproduceStructure.Checked ?  ( theFilesGatherer.IsFromListView ?  false : true ) :
																							false;

			// Transfer images info.
			data.imagesInfo	=  LoadImagesData();

			// Panorama ratio;
			if ( ltPanoramaRatio.Text != null && ltPanoramaRatio.Text != String.Empty )
				data.panoramaRatio =  double.Parse( ltPanoramaRatio.Text );
			else
				data.panoramaRatio =  10000000.0;	// Some big old #

			// Convert to a color profile?
			if ( chkColorSpace.Checked )
			{
				data.colorProfile =  rbColorSpaceProPhoto.Checked ?  "ProPhoto RGB" :
											rbColorSpaceAdobe98.Checked ? "Adobe RGB (1998)" :
											rbColorSpaceSRGB.Checked ? "sRGB IEC61966-2.1" : edtColorSpaceOther.Text;
			}
			else
				data.colorProfile	=  "";

			// Transfer sharpen info.
			data.imagesSharpenInfo	=  ctrlSharpen.ImagesSharpenInfo;
			data.thumbsSharpenInfo	=  ctrlSharpen.ThumbsSharpenInfo;

			// Transfer frame info.
			data.imagesFrameInfo	=  ctrlFrame.ImagesFrameInfo;
			data.thumbsFrameInfo	=  ctrlFrame.ThumbsFrameInfo;

			return ( data );
		}

		private void ValidateData()
		{
		}

		private void WebImagesProcessorI_Load( object sender, System.EventArgs e )
		{
			chkImagesResize_CheckedChanged( this, e );
			chkImagesThumb_CheckedChanged( this, e );

			ctrlFrame.Init();

			bfdSaveIn.LabelIsVisible	=  true;
			bfdSaveIn.Width				=  470;

			// Make sure enabled status is correct.
			chkColorSpace_CheckedChanged( this, e );
		}

		private void WebImagesProcessorI_FormClosing( object sender, FormClosingEventArgs e )
		{
			// Make sure the thread is killed if we're processing.
			if ( processing )
			{
				processThread.Abort();
				processThread.Join();

				processing	=  false;
			}
		}

		// This method is called by the background thread when it has finished processing
		private void Form_ProcessingComplete( Object sender, System.EventArgs e )
		{
			processing	=  false;

			ResetForm();
		}

		private void chkImagesResize_CheckedChanged( object sender, System.EventArgs e )
		{
			// Enable/disable size field.
			edtImagesSize.Enabled	=  chkImagesResize.Checked;
		}

		private void chkImagesThumb_CheckedChanged( object sender, System.EventArgs e )
		{
			// Enable/disable size field and Thumbnails radio button.
			edtImagesThumbSize.Enabled	=  chkImagesThumb.Checked;
		}

		private void chkColorSpace_CheckedChanged( object sender, System.EventArgs e )
		{
			// Enable/disable.
			rbColorSpaceProPhoto.Enabled	=  chkColorSpace.Checked;
			rbColorSpaceAdobe98.Enabled		=  chkColorSpace.Checked;
			rbColorSpaceSRGB.Enabled		=  chkColorSpace.Checked;
			rbColorSpaceOther.Enabled		=  chkColorSpace.Checked;
			edtColorSpaceOther.Enabled		=  chkColorSpace.Checked;
		}

		private void btnProcess_Click( object sender, System.EventArgs e )
		{
			if ( !processing )
			{
				try
				{
					// Transfer data.
					ProcessorData	data	=  Transfer();

					// Validate the input data (throws an exception if no good).
					ValidateData();

					// Create the thread.
					Processor	processor	=  new Processor( this, data );

					processThread	=  new Thread( new ThreadStart( processor.ThreadProc ) );

					// We're processing!
					processing	=  true;

					ResetForm();

					// We can start the thread now.
					processThread.Start();
				}

				catch ( ApplicationBadDataException ex )
				{
					MessageBox.Show( ex.Message, "Data Exception", 0 );
					ex.BadDataControl.Focus();
				}
			}
			else
			{
				// Abort the processing thread (and join until it dies).
				processThread.Abort();
				processThread.Join();

				processing	=  false;

				ResetForm();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.bfdSaveIn = new MyControls.BrowseForDirectory();
			this.grpImages = new System.Windows.Forms.GroupBox();
			this.ltPanoramaRatio = new MyControls.LabelAndText();
			this.ctrlSharpen = new WebFramerCS2ControlLibrary.NikSharpenControlDual();
			this.grpColorSpace = new System.Windows.Forms.GroupBox();
			this.edtColorSpaceOther = new System.Windows.Forms.TextBox();
			this.rbColorSpaceOther = new System.Windows.Forms.RadioButton();
			this.rbColorSpaceProPhoto = new System.Windows.Forms.RadioButton();
			this.rbColorSpaceSRGB = new System.Windows.Forms.RadioButton();
			this.rbColorSpaceAdobe98 = new System.Windows.Forms.RadioButton();
			this.chkColorSpace = new System.Windows.Forms.CheckBox();
			this.ctrlSaveAs = new WebFramerCS2ControlLibrary.SaveAsCtrl();
			this.stImagesThumbPx = new System.Windows.Forms.Label();
			this.edtImagesThumbSize = new System.Windows.Forms.TextBox();
			this.chkImagesThumb = new System.Windows.Forms.CheckBox();
			this.stImagesPx = new System.Windows.Forms.Label();
			this.edtImagesSize = new System.Windows.Forms.TextBox();
			this.chkImagesResize = new System.Windows.Forms.CheckBox();
			this.btnProcess = new System.Windows.Forms.Button();
			this.ctrlFrame = new WebFramerCS2ControlLibrary.FrameCtrl();
			this.theFilesGatherer = new MyControls.FilesGatherer();
			this.theTabCtrl = new System.Windows.Forms.TabControl();
			this.tabFiles = new System.Windows.Forms.TabPage();
			this.chkReproduceStructure = new System.Windows.Forms.CheckBox();
			this.tabImages = new System.Windows.Forms.TabPage();
			this.tabFrame = new System.Windows.Forms.TabPage();
			this.grpImages.SuspendLayout();
			this.grpColorSpace.SuspendLayout();
			this.theTabCtrl.SuspendLayout();
			this.tabFiles.SuspendLayout();
			this.tabImages.SuspendLayout();
			this.tabFrame.SuspendLayout();
			this.SuspendLayout();
			// 
			// bfdSaveIn
			// 
			this.bfdSaveIn.BrowseLabel = "...";
			this.bfdSaveIn.Directory = "";
			this.bfdSaveIn.Label = "Save &Directory";
			this.bfdSaveIn.Location = new System.Drawing.Point( 16, 329 );
			this.bfdSaveIn.Name = "bfdSaveIn";
			this.bfdSaveIn.Size = new System.Drawing.Size( 470, 38 );
			this.bfdSaveIn.TabIndex = 6;
			// 
			// grpImages
			// 
			this.grpImages.Controls.Add( this.ltPanoramaRatio );
			this.grpImages.Controls.Add( this.ctrlSharpen );
			this.grpImages.Controls.Add( this.grpColorSpace );
			this.grpImages.Controls.Add( this.ctrlSaveAs );
			this.grpImages.Controls.Add( this.stImagesThumbPx );
			this.grpImages.Controls.Add( this.edtImagesThumbSize );
			this.grpImages.Controls.Add( this.chkImagesThumb );
			this.grpImages.Controls.Add( this.stImagesPx );
			this.grpImages.Controls.Add( this.edtImagesSize );
			this.grpImages.Controls.Add( this.chkImagesResize );
			this.grpImages.Location = new System.Drawing.Point( 32, 19 );
			this.grpImages.Name = "grpImages";
			this.grpImages.Size = new System.Drawing.Size( 576, 330 );
			this.grpImages.TabIndex = 1;
			this.grpImages.TabStop = false;
			this.grpImages.Text = "Images";
			// 
			// ltPanoramaRatio
			// 
			this.ltPanoramaRatio.Label = "Panorama Ratio";
			this.ltPanoramaRatio.Location = new System.Drawing.Point( 355, 18 );
			this.ltPanoramaRatio.Name = "ltPanoramaRatio";
			this.ltPanoramaRatio.Size = new System.Drawing.Size( 86, 37 );
			this.ltPanoramaRatio.TabIndex = 13;
			// 
			// ctrlSharpen
			// 
			this.ctrlSharpen.Location = new System.Drawing.Point( 290, 90 );
			this.ctrlSharpen.Name = "ctrlSharpen";
			this.ctrlSharpen.Size = new System.Drawing.Size( 270, 140 );
			this.ctrlSharpen.TabIndex = 12;
			// 
			// grpColorSpace
			// 
			this.grpColorSpace.Controls.Add( this.edtColorSpaceOther );
			this.grpColorSpace.Controls.Add( this.rbColorSpaceOther );
			this.grpColorSpace.Controls.Add( this.rbColorSpaceProPhoto );
			this.grpColorSpace.Controls.Add( this.rbColorSpaceSRGB );
			this.grpColorSpace.Controls.Add( this.rbColorSpaceAdobe98 );
			this.grpColorSpace.Controls.Add( this.chkColorSpace );
			this.grpColorSpace.Location = new System.Drawing.Point( 16, 236 );
			this.grpColorSpace.Name = "grpColorSpace";
			this.grpColorSpace.Size = new System.Drawing.Size( 544, 78 );
			this.grpColorSpace.TabIndex = 11;
			this.grpColorSpace.TabStop = false;
			this.grpColorSpace.Text = "Color Space";
			// 
			// edtColorSpaceOther
			// 
			this.edtColorSpaceOther.Location = new System.Drawing.Point( 300, 42 );
			this.edtColorSpaceOther.Name = "edtColorSpaceOther";
			this.edtColorSpaceOther.Size = new System.Drawing.Size( 229, 20 );
			this.edtColorSpaceOther.TabIndex = 15;
			// 
			// rbColorSpaceOther
			// 
			this.rbColorSpaceOther.AutoSize = true;
			this.rbColorSpaceOther.Location = new System.Drawing.Point( 243, 44 );
			this.rbColorSpaceOther.Name = "rbColorSpaceOther";
			this.rbColorSpaceOther.Size = new System.Drawing.Size( 51, 17 );
			this.rbColorSpaceOther.TabIndex = 14;
			this.rbColorSpaceOther.TabStop = true;
			this.rbColorSpaceOther.Text = "Other";
			this.rbColorSpaceOther.UseVisualStyleBackColor = true;
			// 
			// rbColorSpaceProPhoto
			// 
			this.rbColorSpaceProPhoto.Location = new System.Drawing.Point( 105, 22 );
			this.rbColorSpaceProPhoto.Name = "rbColorSpaceProPhoto";
			this.rbColorSpaceProPhoto.Size = new System.Drawing.Size( 121, 16 );
			this.rbColorSpaceProPhoto.TabIndex = 13;
			this.rbColorSpaceProPhoto.Text = "ProPhoto RGB";
			// 
			// rbColorSpaceSRGB
			// 
			this.rbColorSpaceSRGB.Checked = true;
			this.rbColorSpaceSRGB.Location = new System.Drawing.Point( 243, 21 );
			this.rbColorSpaceSRGB.Name = "rbColorSpaceSRGB";
			this.rbColorSpaceSRGB.Size = new System.Drawing.Size( 62, 16 );
			this.rbColorSpaceSRGB.TabIndex = 12;
			this.rbColorSpaceSRGB.TabStop = true;
			this.rbColorSpaceSRGB.Text = "sRGB";
			// 
			// rbColorSpaceAdobe98
			// 
			this.rbColorSpaceAdobe98.Location = new System.Drawing.Point( 105, 44 );
			this.rbColorSpaceAdobe98.Name = "rbColorSpaceAdobe98";
			this.rbColorSpaceAdobe98.Size = new System.Drawing.Size( 121, 16 );
			this.rbColorSpaceAdobe98.TabIndex = 11;
			this.rbColorSpaceAdobe98.Text = "Adobe RGB (1998)";
			// 
			// chkColorSpace
			// 
			this.chkColorSpace.Location = new System.Drawing.Point( 17, 22 );
			this.chkColorSpace.Name = "chkColorSpace";
			this.chkColorSpace.Size = new System.Drawing.Size( 82, 16 );
			this.chkColorSpace.TabIndex = 10;
			this.chkColorSpace.Text = "Con&vert to:";
			this.chkColorSpace.CheckedChanged += new System.EventHandler( this.chkColorSpace_CheckedChanged );
			// 
			// ctrlSaveAs
			// 
			this.ctrlSaveAs.JPEGQuality = 0;
			this.ctrlSaveAs.Location = new System.Drawing.Point( 16, 90 );
			this.ctrlSaveAs.Name = "ctrlSaveAs";
			this.ctrlSaveAs.SaveAsJPEG = false;
			this.ctrlSaveAs.SaveAsTif = true;
			this.ctrlSaveAs.Size = new System.Drawing.Size( 200, 112 );
			this.ctrlSaveAs.TabIndex = 8;
			// 
			// stImagesThumbPx
			// 
			this.stImagesThumbPx.Location = new System.Drawing.Point( 294, 49 );
			this.stImagesThumbPx.Name = "stImagesThumbPx";
			this.stImagesThumbPx.Size = new System.Drawing.Size( 19, 16 );
			this.stImagesThumbPx.TabIndex = 6;
			this.stImagesThumbPx.Text = "px";
			// 
			// edtImagesThumbSize
			// 
			this.edtImagesThumbSize.Location = new System.Drawing.Point( 224, 47 );
			this.edtImagesThumbSize.Name = "edtImagesThumbSize";
			this.edtImagesThumbSize.Size = new System.Drawing.Size( 64, 20 );
			this.edtImagesThumbSize.TabIndex = 5;
			// 
			// chkImagesThumb
			// 
			this.chkImagesThumb.Location = new System.Drawing.Point( 104, 49 );
			this.chkImagesThumb.Name = "chkImagesThumb";
			this.chkImagesThumb.Size = new System.Drawing.Size( 120, 16 );
			this.chkImagesThumb.TabIndex = 4;
			this.chkImagesThumb.Text = "Create Thumbnails";
			this.chkImagesThumb.CheckedChanged += new System.EventHandler( this.chkImagesThumb_CheckedChanged );
			// 
			// stImagesPx
			// 
			this.stImagesPx.Location = new System.Drawing.Point( 294, 21 );
			this.stImagesPx.Name = "stImagesPx";
			this.stImagesPx.Size = new System.Drawing.Size( 26, 16 );
			this.stImagesPx.TabIndex = 2;
			this.stImagesPx.Text = "px";
			// 
			// edtImagesSize
			// 
			this.edtImagesSize.Location = new System.Drawing.Point( 224, 19 );
			this.edtImagesSize.Name = "edtImagesSize";
			this.edtImagesSize.Size = new System.Drawing.Size( 64, 20 );
			this.edtImagesSize.TabIndex = 1;
			// 
			// chkImagesResize
			// 
			this.chkImagesResize.Location = new System.Drawing.Point( 104, 22 );
			this.chkImagesResize.Name = "chkImagesResize";
			this.chkImagesResize.Size = new System.Drawing.Size( 96, 16 );
			this.chkImagesResize.TabIndex = 0;
			this.chkImagesResize.Text = "Resize Images";
			this.chkImagesResize.CheckedChanged += new System.EventHandler( this.chkImagesResize_CheckedChanged );
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point( 510, 51 );
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size( 75, 23 );
			this.btnProcess.TabIndex = 4;
			this.btnProcess.Text = "Run";
			this.btnProcess.Click += new System.EventHandler( this.btnProcess_Click );
			// 
			// ctrlFrame
			// 
			this.ctrlFrame.Location = new System.Drawing.Point( 12, 12 );
			this.ctrlFrame.Name = "ctrlFrame";
			this.ctrlFrame.Size = new System.Drawing.Size( 616, 372 );
			this.ctrlFrame.TabIndex = 5;
			// 
			// theFilesGatherer
			// 
			this.theFilesGatherer.FilesFilter = "Image Files(*.TIF;*.PSD;*.PSB;*.JPG;*.BMP;*.PNG)|*.TIF;*.PSD;*.PSB;*.JPG;*.BMP;*.PNG|Ph" +
				"otoshop Files (*PSD;*.PSB)|*.PSD;*.PSB|TIFF Files (*.TIF)|*.TIF|JPEG Files (*.JP" +
				"G)|*.JPG|All files (*.*)|*.*";
			this.theFilesGatherer.InitialDirectory = "c:\\";
			this.theFilesGatherer.Location = new System.Drawing.Point( 18, 12 );
			this.theFilesGatherer.MultiSelection = true;
			this.theFilesGatherer.Name = "theFilesGatherer";
			this.theFilesGatherer.RootDirectory = "";
			this.theFilesGatherer.Size = new System.Drawing.Size( 420, 300 );
			this.theFilesGatherer.Sorting = MyControls.FilesListView.SortDirection.Asc;
			this.theFilesGatherer.TabIndex = 6;
			this.theFilesGatherer.Title = "Image Files";
			// 
			// theTabCtrl
			// 
			this.theTabCtrl.Controls.Add( this.tabFiles );
			this.theTabCtrl.Controls.Add( this.tabImages );
			this.theTabCtrl.Controls.Add( this.tabFrame );
			this.theTabCtrl.Location = new System.Drawing.Point( 0, 0 );
			this.theTabCtrl.Name = "theTabCtrl";
			this.theTabCtrl.SelectedIndex = 0;
			this.theTabCtrl.Size = new System.Drawing.Size( 652, 403 );
			this.theTabCtrl.TabIndex = 7;
			// 
			// tabFiles
			// 
			this.tabFiles.Controls.Add( this.chkReproduceStructure );
			this.tabFiles.Controls.Add( this.theFilesGatherer );
			this.tabFiles.Controls.Add( this.bfdSaveIn );
			this.tabFiles.Controls.Add( this.btnProcess );
			this.tabFiles.Location = new System.Drawing.Point( 4, 22 );
			this.tabFiles.Name = "tabFiles";
			this.tabFiles.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabFiles.Size = new System.Drawing.Size( 644, 377 );
			this.tabFiles.TabIndex = 0;
			this.tabFiles.Text = "Files";
			this.tabFiles.UseVisualStyleBackColor = true;
			// 
			// chkReproduceStructure
			// 
			this.chkReproduceStructure.AutoSize = true;
			this.chkReproduceStructure.Location = new System.Drawing.Point( 501, 348 );
			this.chkReproduceStructure.Name = "chkReproduceStructure";
			this.chkReproduceStructure.Size = new System.Drawing.Size( 125, 17 );
			this.chkReproduceStructure.TabIndex = 7;
			this.chkReproduceStructure.Text = "Reproduce Structure";
			this.chkReproduceStructure.UseVisualStyleBackColor = true;
			// 
			// tabImages
			// 
			this.tabImages.Controls.Add( this.grpImages );
			this.tabImages.Location = new System.Drawing.Point( 4, 22 );
			this.tabImages.Name = "tabImages";
			this.tabImages.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabImages.Size = new System.Drawing.Size( 644, 377 );
			this.tabImages.TabIndex = 1;
			this.tabImages.Text = "Images";
			this.tabImages.UseVisualStyleBackColor = true;
			// 
			// tabFrame
			// 
			this.tabFrame.Controls.Add( this.ctrlFrame );
			this.tabFrame.Location = new System.Drawing.Point( 4, 22 );
			this.tabFrame.Name = "tabFrame";
			this.tabFrame.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabFrame.Size = new System.Drawing.Size( 192, 74 );
			this.tabFrame.TabIndex = 2;
			this.tabFrame.Text = "Frame";
			this.tabFrame.UseVisualStyleBackColor = true;
			// 
			// WebImagesProcessorI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 652, 404 );
			this.ControlBox = false;
			this.Controls.Add( this.theTabCtrl );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WebImagesProcessorI";
			this.Text = "Web Images Processor";
			this.Load += new System.EventHandler( this.WebImagesProcessorI_Load );
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.WebImagesProcessorI_FormClosing );
			this.grpImages.ResumeLayout( false );
			this.grpImages.PerformLayout();
			this.grpColorSpace.ResumeLayout( false );
			this.grpColorSpace.PerformLayout();
			this.theTabCtrl.ResumeLayout( false );
			this.tabFiles.ResumeLayout( false );
			this.tabFiles.PerformLayout();
			this.tabImages.ResumeLayout( false );
			this.tabFrame.ResumeLayout( false );
			this.ResumeLayout( false );

		}
		#endregion

		#region Processor
		internal class ProcessorData
		{
			public PhotoshopSupport.ImagesInfo	imagesInfo;
			public PhotoshopSupport.SharpenInfo	imagesSharpenInfo;
			public PhotoshopSupport.SharpenInfo	thumbsSharpenInfo;
			public PhotoshopSupport.FrameInfo	imagesFrameInfo;
			public PhotoshopSupport.FrameInfo	thumbsFrameInfo;
			public MyFileInfosArray				filesAr;
			public string						rootDirectory;
			public bool							reproduceStructure;
			public string						saveInDirectory;
			public string						colorProfile;
			public double						panoramaRatio;

			public ProcessorData()
			{
			}
		}

		internal class Processor
		{
			private PhotoshopSupport.ImagesInfo		imagesInfo;
			private PhotoshopSupport.SharpenInfo	imagesSharpenInfo;
			private PhotoshopSupport.SharpenInfo	thumbsSharpenInfo;
			private PhotoshopSupport.FrameInfo		imagesFrameInfo;
			private PhotoshopSupport.FrameInfo		thumbsFrameInfo;
			private MyFileInfosArray				filesAr;
			private string							rootDirectory;
			private bool							reproduceStructure;
			private string							saveInDirectory;
			private string							colorProfile;
			private double							panoramaRatio;
			private WebImagesProcessorI				myParent;
			private Photoshop.Application		psApp;
			private Photoshopper.Photoshopper		ps;

			public Processor( WebImagesProcessorI parent, ProcessorData data )
			{
				myParent			=  parent;

				filesAr				=  data.filesAr;
				rootDirectory		=  data.rootDirectory;
				reproduceStructure	=  data.reproduceStructure;
				imagesInfo			=  data.imagesInfo;
				imagesSharpenInfo	=  data.imagesSharpenInfo;
				thumbsSharpenInfo	=  data.thumbsSharpenInfo;
				imagesFrameInfo		=  data.imagesFrameInfo;
				thumbsFrameInfo		=  data.thumbsFrameInfo;
				saveInDirectory		=  data.saveInDirectory;
				colorProfile		=  data.colorProfile;
				panoramaRatio		=  data.panoramaRatio;
			}

			public void ThreadProc()
			{
				try
				{
					Process();
				}

				finally
				{
					// Raise an event that notifies the user that the processing has terminated.  
					myParent.BeginInvoke( myParent.onProcessingComplete, new object[] { this, EventArgs.Empty } );
				}
			}

			private void Process()
			{
				PsUnits originalRulerUnits	=  PsUnits.psPixels;

				try
				{
					if ( filesAr.Count == 0 )
						return;

					// Create the Photoshop object.
					psApp	=  new Photoshop.Application();
					ps		=  new Photoshopper.Photoshopper( psApp );

					// Save preferences.
					originalRulerUnits	=  psApp.Preferences.RulerUnits;

					// Set ruler units to pixels
					psApp.Preferences.RulerUnits	=  PsUnits.psPixels;

					// Don't display dialogs
					psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

					// Some variables.
					ArrayList imageResizesAr	=  new ArrayList();
					ArrayList thumbResizesAr	=  new ArrayList();

					for ( int i= 0;  i< filesAr.Count;  i++ )
					{
						// Open document.
						Document	imageDoc	=  ps.OpenDoc( filesAr[ i ].FullName );

						if ( imageDoc != null )
						{
							// Make sure it's 8-bit
							imageDoc.BitsPerChannel	=  PsBitsPerChannelType.psDocument8Bits;

							// Make sure there's only 1 layer.
							imageDoc.Flatten();

							// Save this history state.
							HistoryState resetHistoryState1	=  imageDoc.ActiveHistoryState;

							// Resize.
							if ( imagesInfo.resizeImages )
							{
								double	ratio	=  0.0;
								double	width	=  imageDoc.Width;
								double	height	=  imageDoc.Height;

								ratio	=  ( width > height ) ?  width/height : height/width;

								if ( ratio < panoramaRatio )
									ps.ResizeDoc( imageDoc, imagesInfo.resizeInfoImages.resizeRegular, imagesInfo.resizeInfoImages.typeRegular );	// ResizeType.MaxBoth
								else
									ps.ResizeDoc( imageDoc, imagesInfo.resizeInfoImages.resizeRegular, imagesInfo.resizeInfoImages.typePanorama );	// ResizeType.Height
							}

							// Sharpen?
							if ( imagesSharpenInfo.sharpenType != SharpenInfo.SharpenType.None )
							{
								SharpenImage( imageDoc, false, imagesSharpenInfo.sharpenType, imagesSharpenInfo.options );
							}

							// Make frame (and slice it)?
							if ( imagesFrameInfo.makeFrame )
							{
								if ( !imagesFrameInfo.sliceAndSave )
									ps.MakeFrame( imageDoc, imagesFrameInfo );	// No slicing so always make frame.
								else
								{
									// Slice and Save only if we don't haven't done it for this resize.
									bool		doneIt		=  false;
									string	resizeName	=  imageDoc.Width + "x" + imageDoc.Height;

									foreach ( string sizeName in imageResizesAr )
									{
										if ( sizeName == resizeName )
										{
											doneIt	=  true;
											break;
										}
									}

									if ( !doneIt )
									{
										// Haven't done it yet.
										// Save the current history state.
										HistoryState resetHistoryState2	=  imageDoc.ActiveHistoryState;

										// Make frame and slice it!
										ps.MakeFrame( imageDoc, imagesFrameInfo );
										ps.SliceUpWebImage( imageDoc, saveInDirectory + @"/Images", imagesFrameInfo.borderWidth, resizeName );

										// Reset to just after resizing after framing and slicing.
										imageDoc.ActiveHistoryState	=  resetHistoryState2;

										// Add to the array.
										imageResizesAr.Add( resizeName );
									}
								}
							}

							// Convert to a different color space?
							if ( colorProfile != "" )
								ps.ConvertToProfile( colorProfile );

							// Save it, but where?
							if ( reproduceStructure )
							{
								string saveDirectory	=  GetSaveDirectory( rootDirectory, imageDoc.Path, saveInDirectory );

								// Save image in Images directory as jpeg or tiff.
								string filename	=  "";

								if ( imagesInfo.saveImagesAsJPEG )
									ps.SaveAsJPEG( imageDoc, saveDirectory, filename, imagesInfo.jpegQuality );
								else
									ps.SaveAsTIFF( imageDoc, saveDirectory, filename );
							}
							else
							{
								// Save image in Images directory as jpeg or tiff.
								string filename	=  "";

								if ( imagesInfo.saveImagesAsJPEG )
									ps.SaveAsJPEG( imageDoc, saveInDirectory + @"/Images/", filename, imagesInfo.jpegQuality );
								else
									ps.SaveAsTIFF( imageDoc, saveInDirectory + @"/Images/", filename );
							}

							#region Thumbnails
							// Create thumbnail?
							if ( imagesInfo.thumbnails )
							{
								// Restore to the "beginning".
								imageDoc.ActiveHistoryState	=  resetHistoryState1;

								// Create the thumbnail.
								// Resize.
								ps.ResizeDoc( imageDoc, imagesInfo.resizeInfoThumbs.resizeRegular, imagesInfo.resizeInfoThumbs.typeRegular );

								// Sharpen?
								if ( thumbsSharpenInfo.sharpenType != SharpenInfo.SharpenType.None )
								{
									SharpenImage( imageDoc, false, thumbsSharpenInfo.sharpenType, thumbsSharpenInfo.options );
								}

								// Make frame (and slice it)?
								if ( thumbsFrameInfo.makeFrame )
								{
									if ( !thumbsFrameInfo.sliceAndSave )
										ps.MakeFrame( imageDoc, thumbsFrameInfo );	// No slicing so always make frame.
									else
									{
										// Slice and Save only if we don't haven't done it for this resize.
										bool doneIt = false;
										string resizeName = imageDoc.Width + "x" + imageDoc.Height;

										foreach ( string sizeName in imageResizesAr )
										{
											if ( sizeName == resizeName )
											{
												doneIt = true;
												break;
											}
										}

										if ( !doneIt )
										{
											// Haven't done it yet.
											// Save the current history state.
											HistoryState resetHistoryState2 = imageDoc.ActiveHistoryState;

											// Make frame and slice it!
											ps.MakeFrame( imageDoc, thumbsFrameInfo );
											ps.SliceUpWebImage( imageDoc, saveInDirectory + @"\Thumbnails", thumbsFrameInfo.borderWidth, resizeName );

											// Reset to just after resizing after framing and slicing.
											imageDoc.ActiveHistoryState = resetHistoryState2;

											// Add to the array.
											imageResizesAr.Add( resizeName );
										}
									}
								}

								string filename	=  "";

								if ( this.imagesInfo.saveThumbsAsGIF )
									ps.SaveAsGIF( imageDoc, saveInDirectory + @"/Thumbnails", filename );
								else
									ps.SaveAsJPEG( imageDoc, saveInDirectory + @"/Thumbnails", filename, imagesInfo.jpegQuality );
							}
							#endregion

							// Close this document.
							imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
						}
					}

					// Restore preferences.
					psApp.Preferences.RulerUnits	=  originalRulerUnits;
				}

				catch ( ThreadAbortException )
				{
					// We were aborted!
				}

				catch ( Exception ex )
				{
					// Uh oh!
					MessageBox.Show( ex.Message, "Processing" );
				}

				finally
				{
					// Restore preferences.
					if ( psApp != null )
						psApp.Preferences.RulerUnits	=  originalRulerUnits;
				}
			}

			private string GetSaveDirectory( string rootDirectory, string path, string saveDirectory )
			{
				string	saveDir	=  "";

				int	index	=  path.IndexOf( rootDirectory );

				saveDir	=  saveDirectory + "\\" + path.Substring( index + rootDirectory.Length + 1 );

				return ( saveDir );
			}

			private void SharpenImage( Document imageDoc, bool manualIfNoLandChannel,
									   SharpenInfo.SharpenType sharpenType, SharpenOptionsInfo options )
			{
				// Get the channels.
				Photoshop.Channels	channels	=  imageDoc.Channels;

				// Loop through channels and sharpen any land ones.
				bool foundLandChannel	=  false;

				for ( int j= 1;  j<= channels.Count;  j++ )
				{
					Channel channel	=  channels[ j ];

					string			channelName	=  channel.Name;
					PsChannelType	channelType	=  channel.Kind;

					if ( channelName.ToLower().IndexOf( "land" ) > -1 && channelType == PsChannelType.psMaskedAreaAlphaChannel )
					{
						// Select channel.
						imageDoc.Selection.Load( channel, PsSelectionType.psReplaceSelection, false );

						// Sharpen.
						if ( sharpenType == SharpenInfo.SharpenType.NikDisplay )
							ps.NikSharpen2Display( options as NikDisplaySharpenInfo );
						else if ( sharpenType == SharpenInfo.SharpenType.Nik )
						{
							NikPrintSharpenInfo nikOptions =  options as NikPrintSharpenInfo;

							ps.NikSharpen2( nikOptions.ProfileType, 0, nikOptions.PaperType, nikOptions.PrinterResolution );
						}

						foundLandChannel	=  true;
					}
				}

				if ( !foundLandChannel && manualIfNoLandChannel )
				{
					// Put up a message so I can do it manually.
					DialogResult	doIt	=  MessageBox.Show( "Sharpen image manually!", "No land channel found" );

					if ( doIt == DialogResult.OK )
					{
						if ( sharpenType == SharpenInfo.SharpenType.NikDisplay )
							ps.NikSharpen2Display( options as NikDisplaySharpenInfo );
						else if ( sharpenType == SharpenInfo.SharpenType.Nik )
						{
							NikPrintSharpenInfo nikOptions =  options as NikPrintSharpenInfo;

							ps.NikSharpen2( nikOptions.ProfileType, 0, nikOptions.PaperType, nikOptions.PrinterResolution );
						}
					}
				}
			}
		}
		#endregion
	}


	/////////////////////////////////////////////////////////////////
	/// Exception class for bad data
	/// 
	internal class ApplicationBadDataException : ApplicationException 
	{
		private Control	badDataControl;

		public ApplicationBadDataException( string message, Control control ) : base( message )
		{
			BadDataControl	=  control;
		}

		public Control BadDataControl
		{
			get { return ( badDataControl ); }
			set { badDataControl	=  value; }
		}
	}

// 	internal class SpecialApps
// 	{
// 		public void PrepareSpecificSize( Photoshopper.Photoshopper ps, Photoshop.Document imageDoc, double newHeight,
// 													string saveAsString, NikSharpenInfo.NikProfileType sharpenLevel, int eyeDistance )
// 		{
// 			// Set ruler units to inches
// 			ps.theApp.Preferences.RulerUnits = PsUnits.psInches;
// 
// 			// Resize to new size.
// 			MyClasses.Size size = new MyClasses.Size();
// 
// 			if ( imageDoc.Width > imageDoc.Height )
// 			{
// 				size.Height = newHeight;
// 				size.Width = ( size.Height / imageDoc.Height ) * imageDoc.Width;
// 			}
// 			else
// 			{
// 				size.Width = newHeight;
// 				size.Height = ( size.Width / imageDoc.Width ) * imageDoc.Height;
// 			}
// 
// 			imageDoc.ResizeImage( null, size.Height, null, PsResampleMethod.psNoResampling );
// 
// 			// Make sure there's only 1 layer.
// 			imageDoc.Flatten();
// 
// 			// Now resize to 360dpi
// 			imageDoc.ResizeImage( imageDoc.Width, imageDoc.Height, 360.0, PsResampleMethod.psBicubic );
// 
// 			// Make sure it's 8-bit
// 			imageDoc.BitsPerChannel = PsBitsPerChannelType.psDocument8Bits;
// 
// 			// Get the channels.
// 			Photoshop.Channels channels = imageDoc.Channels;
// 
// 			// Loop through channels and sharpen any land ones.
// 			bool foundLandChannel = false;
// 
// 			for ( int j = 1; j <= channels.Count; j++ )
// 			{
// 				Channel channel = channels[ j ];
// 
// 				string channelName = channel.Name;
// 				PsChannelType channelType = channel.Kind;
// 
// 				if ( channelName.ToLower().IndexOf( "land" ) > -1 && channelType == PsChannelType.psMaskedAreaAlphaChannel )
// 				{
// 					// Select channel.
// 					imageDoc.Selection.Load( channel, PsSelectionType.psReplaceSelection, false );
// 
// 					//***int profile = (int) sharpenLevel;
// 					// Sharpen.
// 					NikDisplaySharpenInfo	options	=  new NikDisplaySharpenInfo();
// 
// 					options.ProfileType	=  sharpenLevel;
// 
// 					ps.NikSharpen2Display( options );	// John, Book, Good, 1440x1440.
// 					//***ps.NikSharpen2Display( profile, eyeDistance, 4, 7 );	// John, Book, Good, 1440x1440.
// 
// 					foundLandChannel = true;
// 				}
// 			}
// 
// 			if ( !foundLandChannel )
// 			{
// 				// Put up a message so I can do it manually.
// 				MessageBox.Show( "Sharpen image manually!", "No land channel found" );
// 			}
// 
// 			// Get path and create output path name.
// 			string pathName = imageDoc.FullName;
// 
// 			pathName = pathName.Substring( 0, pathName.LastIndexOf( "\\" ) );
// 
// 			// Save as photoshop.
// 			ps.SaveAsPSD( imageDoc, pathName, saveAsString );
// 		}
// 
// 		/// <summary>
// 		/// /////////////////////////////////////////////////////////////
// 		/// // Special case helpers.
// 		/// </summary>
// 		/// <returns></returns>
// 		public void MakeBannerMap( Photoshopper.Photoshopper ps, ArrayList filesAr )
// 		{
// 			double	lastX	=  0.0;
// 			double	lastY	=  0.0;
// 			bool		merge	=  false;
// 
// 			// Save preferences.
// 			PsUnits	originalRulerUnits	=  ps.theApp.Preferences.RulerUnits;
// 		
// 			// Set ruler units to inches
// 			ps.theApp.Preferences.RulerUnits	=  PsUnits.psPixels;
// 		
// 			// Save current foreground color.
// 			SolidColor	orgBackground	=  ps.theApp.BackgroundColor;
// 			SolidColor	newBackground	=  new SolidColor();
// 
// 			// Set background to white.	
// 			newBackground.RGB.Red	=  255;
// 			newBackground.RGB.Green	=  255;
// 			newBackground.RGB.Blue	=  255;
// 
// 			ps.theApp.BackgroundColor.RGB	=  newBackground.RGB;
// 
// 			// Create new document?
// 			Document	bannerDoc	=  ps.theApp.Documents[ "Banner" ];
// 
// 			if ( bannerDoc == null )
// 				bannerDoc	=  ps.theApp.Documents[ "Banner.psb" ];
// 
// 			if ( bannerDoc == null )
// 				bannerDoc	=  ps.theApp.Documents.Add( 14400.0, 4320.0, 360.0, "Banner", Photoshop.PsNewDocumentMode.psNewRGB, Photoshop.PsDocumentFill.psBackgroundColor, 1.0, 16, null );
// 
// 			double	w		=  0.0;
// 			double	h		=  0.0;
// 			double	mapW	=  bannerDoc.Width;
// 			double	mapH	=  bannerDoc.Height;
// 
// 			for ( int i= 0;  i< filesAr.Count;  i++ )
// 			{
// 				// Open document.
// 				Document	imageDoc	=  ps.OpenDoc( (string) filesAr[ i ] );
// 
// 				bool	isPanorama	=  imageDoc.Width / imageDoc.Height >= 2.0;
// 
// 				// Resize.
// 				if ( isPanorama )
// 				{
// 					w	=  ( 540.0 / imageDoc.Height )*imageDoc.Width;
// 
// 					imageDoc.ResizeImage( w, 540.0, 360.0, PsResampleMethod.psBicubic );
// 				}
// 				else
// 				{
// 					// Flip vertical pics.
// 					if ( imageDoc.Height > imageDoc.Width )
// 						imageDoc.RotateCanvas( -90.0 );
// 
// 					// Resize image.
// 					imageDoc.ResizeImage( 900, 540, 360.0, Photoshop.PsResampleMethod.psBicubic );
// 				}
// 
// 				w	=  imageDoc.Width;
// 				h	=  imageDoc.Height;
// 
// 				// Are we past
// 				if ( lastX+w > mapW )
// 				{
// 					lastX	 =  0;
// 					lastY	+=  h;
// 				}
// 
// 				if ( lastY+h > mapH )
// 				{
// 					// Resize height.
// 					bannerDoc.ResizeCanvas( mapW, mapH+h, Photoshop.PsAnchorPosition.psTopCenter );
// 				}
// 
// 				// Select and copy.
// 				imageDoc.Selection.SelectAll();
// 
// 				if ( imageDoc.Layers.Count > 1 )
// 					merge	=  true;
// 				else
// 					merge	=  false;
// 
// 				( (ArtLayer) ( imageDoc.ActiveLayer ) ).Copy( merge );
// 
// 				// Now paste into the proper location.
// 				double[]	selCorners	=  new double[4] { lastX, lastY, lastX+w, lastY+h };
// 				Bounds	selBounds	=  new Bounds( selCorners );
// 
// 				ps.theApp.ActiveDocument	=  bannerDoc;
// 				ps.SelectRectArea( selBounds, PsSelectionType.psReplaceSelection, 0.0 );
// 
// 				bannerDoc.Paste( true );
// 				bannerDoc.Selection.Deselect();
// 
// 				lastX	+=  w;
// 
// 				// Close file.
// 				imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
// 			}
// 
// 			// Resize the canvas.
// 			//***bannerDoc.ResizeCanvas( bannerDoc.Width, lastY+h, Photoshop.PsAnchorPosition.psTopCenter );
// 
// 			// Restore foreground color.
// 			ps.theApp.BackgroundColor	=  orgBackground;
// 		
// 			// Restore units
// 			ps.theApp.Preferences.RulerUnits	=  originalRulerUnits;
// 		}
// 	}
}

