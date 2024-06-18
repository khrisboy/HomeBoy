using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using MyControls;

namespace PhotoshopUtilities
{
	/// <summary>
	/// Summary description for UtilitiesForm.
	/// </summary>
	public class UtilitiesForm : MyWindowsForm
	{
		#region Members

		private ArrayList theKidsAr;
		private string baseTitle =  "Photoshop Utilities";

		private MainMenu mainMenu1;
		private MenuItem File;
		private MenuItem FileExit;
		private MenuItem Utilities;
		private MenuItem UtilitiesCalendarPrep;
		private MenuItem UtilitiesImageMap;
		private MenuItem WebImagesProcessorI;
// 		private MenuItem WebImagesProcessorII;
		private MenuItem PrintSizes;
		private MenuItem FamilyPics;
		private MenuItem CreateGuides;
		private MenuItem Mosaic;
		private MenuItem NikonScanProcessor;
		private MenuItem LabelMaker;
		private MenuItem ContactSheetsMaker;
		private MenuItem IconExtractor;
		private MenuItem FrameRemover;
		private MenuItem MotelArtwork;
// 		private MenuItem PanoramaVille;
		private MenuItem NEFFileChecker;
		private MenuItem TouchMe;
		private MenuItem SaveTiffCompressed;
		private MenuItem CompareFiles;

		private IContainer components;

		#endregion Members

		#region Constructors

		public UtilitiesForm()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

			// This is an MDI Container.
			IsMdiContainer	=  true;
			
			// Create (but don't show yet) the children.
			theKidsAr =  new ArrayList();

			theKidsAr.Add( new CalendarPrep() );
			theKidsAr.Add( new CompareFiles() );
			theKidsAr.Add( new ContactSheetMaker() );
			theKidsAr.Add( new CreateGuides() );
			theKidsAr.Add( new CreateImageMap() );
			theKidsAr.Add( new CreateMosaic() );
			theKidsAr.Add( new FamilyPhotos50th() );
			theKidsAr.Add( new FileChecker() );
			theKidsAr.Add( new IconExtractor() );
			theKidsAr.Add( new LabelMaker() );
			theKidsAr.Add( new MotelArtwork() );
			theKidsAr.Add( new NikonScanProcessor() );
// 			theKidsAr.Add( new PanoramaVille() );
			theKidsAr.Add( new PrintSizes() );
			theKidsAr.Add( new RemoveFrame() );
			theKidsAr.Add( new SaveTiffCompressed() );
			theKidsAr.Add( new TouchMe() );
			theKidsAr.Add( new WebImagesProcessorI() );

			for ( int i= 0;  i< theKidsAr.Count;  i++ )
			{
				( (Form) theKidsAr[ i ] ).MdiParent	=  this;
			}

			// Set the title.
			Text =  baseTitle;

			// No saving.
			AutoSerialize =  false;
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
				System.Windows.Forms.Application.Run( new UtilitiesForm() );
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message + "\r\n" + ex.StackTrace );
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UtilitiesForm));
			this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
			this.File = new System.Windows.Forms.MenuItem();
			this.FileExit = new System.Windows.Forms.MenuItem();
			this.Utilities = new System.Windows.Forms.MenuItem();
			this.UtilitiesCalendarPrep = new System.Windows.Forms.MenuItem();
			this.ContactSheetsMaker = new System.Windows.Forms.MenuItem();
			this.CreateGuides = new System.Windows.Forms.MenuItem();
			this.FamilyPics = new System.Windows.Forms.MenuItem();
			this.NEFFileChecker = new System.Windows.Forms.MenuItem();
			this.FrameRemover = new System.Windows.Forms.MenuItem();
			this.IconExtractor = new System.Windows.Forms.MenuItem();
			this.UtilitiesImageMap = new System.Windows.Forms.MenuItem();
			this.LabelMaker = new System.Windows.Forms.MenuItem();
			this.Mosaic = new System.Windows.Forms.MenuItem();
			this.MotelArtwork = new System.Windows.Forms.MenuItem();
			this.NikonScanProcessor = new System.Windows.Forms.MenuItem();
// 			this.PanoramaVille = new System.Windows.Forms.MenuItem();
			this.PrintSizes = new System.Windows.Forms.MenuItem();
			this.SaveTiffCompressed = new System.Windows.Forms.MenuItem();
			this.TouchMe = new System.Windows.Forms.MenuItem();
			this.WebImagesProcessorI = new System.Windows.Forms.MenuItem();
// 			this.WebImagesProcessorII = new System.Windows.Forms.MenuItem();
			this.CompareFiles = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.File,
            this.Utilities});
			// 
			// File
			// 
			this.File.Index = 0;
			this.File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.FileExit});
			this.File.Text = "&File";
			// 
			// FileExit
			// 
			this.FileExit.Index = 0;
			this.FileExit.Text = "Exit";
			this.FileExit.Click += new System.EventHandler(this.FileExit_Click);
			// 
			// Utilities
			// 
			this.Utilities.Index = 1;
			this.Utilities.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.UtilitiesCalendarPrep,
            this.CompareFiles,
            this.ContactSheetsMaker,
            this.CreateGuides,
            this.FamilyPics,
            this.NEFFileChecker,
            this.FrameRemover,
            this.IconExtractor,
            this.UtilitiesImageMap,
            this.LabelMaker,
            this.Mosaic,
            this.MotelArtwork,
            this.NikonScanProcessor,
//			this.PanoramaVille,
            this.PrintSizes,
			this.SaveTiffCompressed,
			this.TouchMe,
			this.WebImagesProcessorI,
//			this.WebImagesProcessorII
			} );
			this.Utilities.Text = "&Utilities";
			// 
			// UtilitiesCalendarPrep
			// 
			this.UtilitiesCalendarPrep.Index = 0;
			this.UtilitiesCalendarPrep.Text = "&Calendar Prep";
			this.UtilitiesCalendarPrep.Click += new System.EventHandler(this.UtilitiesCalendarPrep_Click);
			// 
			// CompareFiles
			// 
			this.CompareFiles.Index = 1;
			this.CompareFiles.Text = "Compare Fi&les";
			this.CompareFiles.Click += new System.EventHandler( this.CompareFiles_Click );
			// 
			// ContactSheetsMaker
			// 
			this.ContactSheetsMaker.Index = 2;
			this.ContactSheetsMaker.Text = "Contact &Sheets Maker";
			this.ContactSheetsMaker.Click += new System.EventHandler(this.ContactSheetsMaker_Click);
			// 
			// CreateGuides
			// 
			this.CreateGuides.Index = 3;
			this.CreateGuides.Text = "Create &Guides";
			this.CreateGuides.Click += new System.EventHandler(this.CreateGuides_Click);
			// 
			// FamilyPics
			// 
			this.FamilyPics.Index = 4;
			this.FamilyPics.Text = "&Family Pics";
			this.FamilyPics.Click += new System.EventHandler(this.FamilyPics_Click);
			// 
			// NEFFileChecker
			// 
			this.NEFFileChecker.Index = 5;
			this.NEFFileChecker.Text = "File Chec&ker";
			this.NEFFileChecker.Click += new System.EventHandler(this.NEFFileChecker_Click);
			// 
			// FrameRemover
			// 
			this.FrameRemover.Index = 6;
			this.FrameRemover.Text = "Frame &Remover";
			this.FrameRemover.Click += new System.EventHandler(this.FrameRemover_Click);
			// 
			// IconExtractor
			// 
			this.IconExtractor.Index = 7;
			this.IconExtractor.Text = "Icon E&xtractor";
			this.IconExtractor.Click += new System.EventHandler(this.IconExtractor_Click);
			// 
			// UtilitiesImageMap
			// 
			this.UtilitiesImageMap.Index = 8;
			this.UtilitiesImageMap.Text = "Image &Map";
			this.UtilitiesImageMap.Click += new System.EventHandler(this.UtilitiesImageMap_Click);
			// 
			// LabelMaker
			// 
			this.LabelMaker.Index = 9;
			this.LabelMaker.Text = "&Label Maker";
			this.LabelMaker.Click += new System.EventHandler(this.LabelMaker_Click);
			// 
			// Mosaic
			// 
			this.Mosaic.Index = 10;
			this.Mosaic.Text = "M&osaic";
			this.Mosaic.Click += new System.EventHandler(this.Mosaic_Click);
			// 
			// MotelArtwork
			// 
			this.MotelArtwork.Index = 11;
			this.MotelArtwork.Text = "Motel &Artwork";
			this.MotelArtwork.Click += new System.EventHandler(this.MotelArtwork_Click);
			// 
			// NikonScanProcessor
			// 
			this.NikonScanProcessor.Index = 12;
			this.NikonScanProcessor.Text = "&Nikon Scan Processor";
			this.NikonScanProcessor.Click += new System.EventHandler(this.NikonScanProcessor_Click);
			// 
			// PrintSizes
			// 
			this.PrintSizes.Index = 13;
			this.PrintSizes.Text = "&Print Sizes";
			this.PrintSizes.Click += new System.EventHandler(this.PrintSizes_Click);
			// 
			// SaveTiffCompressed
			// 
			this.SaveTiffCompressed.Index = 14;
			this.SaveTiffCompressed.Text = "Sa&ve Tiff Compressed";
			this.SaveTiffCompressed.Click += new System.EventHandler( this.SaveTiffCompressed_Click );
			// 
			// TouchMe
			// 
			this.TouchMe.Index = 15;
			this.TouchMe.Text = "&Touch Me";
			this.TouchMe.Click += new System.EventHandler(this.TouchMe_Click);
			// 
			// WebImagesProcessorI
			// 
			this.WebImagesProcessorI.Index = 16;
			this.WebImagesProcessorI.Text = "&Web Image Processor I";
			this.WebImagesProcessorI.Click += new System.EventHandler(this.WebImagesProcessorI_Click);
// 			// 
// 			// WebImagesProcessorII
// 			// 
// 			this.WebImagesProcessorII.Enabled = false;
// 			this.WebImagesProcessorII.Index = 17;
// 			this.WebImagesProcessorII.Text = "Web Image Processor II";
// 			this.WebImagesProcessorII.Click += new System.EventHandler(this.WebImagesProcessorII_Click);
// 			// 
// 			// PanoramaVille
// 			// 
// 			this.PanoramaVille.Index = 13;
// 			this.PanoramaVille.Text = "Panorama&Ville";
// 			this.PanoramaVille.Click += new System.EventHandler(this.PanoramaVille_Click);
			// 
			// UtilitiesForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(512, 595);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.IsMdiContainer = true;
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "UtilitiesForm";
			this.Load += new System.EventHandler(this.UtilitiesForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		#endregion Methods

		#region Events

		private void FileExit_Click( object sender, System.EventArgs e )
		{
			Close();
		}

		private void UtilitiesForm_Load( object sender, System.EventArgs e )
		{
		}

		private void ShowDialog( Type whichOne )
		{
			// Hide the old one.
			for ( int i= 0;  i< MdiChildren.Length;  i++ )
			{
				Form theDlg =  MdiChildren[ i ];

				if ( theDlg.GetType() != whichOne )
				{
					theDlg.Hide();
				}
			}

			// Show the new one.
			for ( int i= 0;  i< MdiChildren.Length;  i++ )
			{
				Form theDlg =  MdiChildren[ i ];

				if ( theDlg.GetType() == whichOne )
				{
					// Set back to normal window state first(?)
					theDlg.WindowState = FormWindowState.Normal;

					theDlg.Show();
	
					// Set form to top corner of client area.
					theDlg.Top	=  0;
					theDlg.Left	=  0;
	
					// Resize client area to match form.
					//*** For some reason we have to add 4 to each dimension????????????????????????
					Size dlgSize =  theDlg.Size;
	
					dlgSize.Width  +=  4;
					dlgSize.Height +=  4;
				
					ClientSize =  dlgSize;
	
					theDlg.WindowState = FormWindowState.Maximized;
	
					// Update the title.
					//Text =  baseTitle + " -  " + theDlg.Text;

					break;
				}
			}
		}

		private void UtilitiesCalendarPrep_Click( object sender, System.EventArgs e )
		{
			ShowDialog( typeof( CalendarPrep ) );
		}

		private void UtilitiesImageMap_Click(object sender, System.EventArgs e)
		{
			ShowDialog( typeof( CreateImageMap ) );
		}

		private void WebImagesProcessorI_Click(object sender, System.EventArgs e)
		{
			ShowDialog( typeof( WebImagesProcessorI ) );
		}

// 		private void WebImagesProcessorII_Click(object sender, System.EventArgs e)
// 		{
// 			//***ShowDialog( typeof( WebImagesProcessorII ) );
// 		}

		private void PrintSizes_Click(object sender, System.EventArgs e)
		{
			ShowDialog( typeof( PrintSizes ) );
		}

		private void FamilyPics_Click(object sender, System.EventArgs e)
		{
			ShowDialog( typeof( FamilyPhotos50th ) );
		}

		private void CreateGuides_Click(object sender, System.EventArgs e)
		{
			ShowDialog( typeof( CreateGuides ) );
		}

		private void Mosaic_Click(object sender, System.EventArgs e)
		{
			ShowDialog( typeof( CreateMosaic ) );
		}

		private void NikonScanProcessor_Click(object sender, System.EventArgs e)
		{
			ShowDialog( typeof( NikonScanProcessor ) );
		}

		private void LabelMaker_Click( object sender, EventArgs e )
		{
			ShowDialog( typeof( LabelMaker ) );
		}

		private void ContactSheetsMaker_Click( object sender, EventArgs e )
		{
			ShowDialog( typeof( ContactSheetMaker ) );
		}

		private void IconExtractor_Click(object sender, EventArgs e)
		{
			ShowDialog( typeof( IconExtractor ) );
		}

		private void FrameRemover_Click(object sender, EventArgs e)
		{
			ShowDialog( typeof( RemoveFrame ) );
		}

		private void MotelArtwork_Click(object sender, EventArgs e)
		{
			ShowDialog( typeof( MotelArtwork ) );
		}

// 		private void PanoramaVille_Click( object sender, EventArgs e )
// 		{
// 			ShowDialog( typeof( PanoramaVille ) );
// 		}

		private void NEFFileChecker_Click( object sender, EventArgs e )
		{
			ShowDialog( typeof( FileChecker ) );
		}

		private void TouchMe_Click( object sender, EventArgs e )
		{
			ShowDialog( typeof( TouchMe ) );
		}

		private void CompareFiles_Click( object sender, EventArgs e )
		{
			ShowDialog( typeof( CompareFiles ) );
		}

		private void SaveTiffCompressed_Click( object sender, System.EventArgs e )
		{
			ShowDialog( typeof( SaveTiffCompressed ) );
		}

		#endregion Events
	}
}
