using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;
using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;

namespace PhotoshopUtilities
{
	/// <summary>
	/// Summary description for CalendarPrep.
	/// </summary>
	public class CalendarPrep : MyWindowsForm
	{
		#region Member variables.
		private System.Windows.Forms.GroupBox grpCalendarImages;
		private System.Windows.Forms.GroupBox grpCalendarImagesSize;
		private System.Windows.Forms.Label stCalendarImagesWidth;
		private System.Windows.Forms.TextBox edtCalendarImagesHeight;
		private System.Windows.Forms.Label stCalendarImagesHeight;
		private System.Windows.Forms.TextBox edtCalendarImagesWidth;
		private System.Windows.Forms.GroupBox grpCalendarImagesTopCorner;
		private System.Windows.Forms.Label stCalendarImagesTopX;
		private System.Windows.Forms.TextBox edtCalendarImagesTopY;
		private System.Windows.Forms.Label stCalendarImagesTopY;
		private System.Windows.Forms.TextBox edtCalendarImagesTopX;
		private System.Windows.Forms.GroupBox grpSecondaryImages;
		private System.Windows.Forms.GroupBox grpSecondaryImagesSize;
		private System.Windows.Forms.Label stSecondaryImagesWidth;
		private System.Windows.Forms.TextBox edtSecondaryImagesHeight;
		private System.Windows.Forms.Label stSecondaryImagesHeight;
		private System.Windows.Forms.TextBox edtSecondaryImagesWidth;
		private System.Windows.Forms.GroupBox grpSecondaryImagesTopCorner;
		private System.Windows.Forms.Label stSecondaryImagesTopX;
		private System.Windows.Forms.TextBox edtSecondaryImagesTopY;
		private System.Windows.Forms.Label stSecondaryImagesTopY;
		private System.Windows.Forms.TextBox edtSecondaryImagesTopX;
		private System.Windows.Forms.GroupBox grpPage;
		private System.Windows.Forms.TextBox edtPageResolution;
		private System.Windows.Forms.Label stPageResolution;
		private System.Windows.Forms.GroupBox grpPageSize;
		private System.Windows.Forms.Label stPageWidth;
		private System.Windows.Forms.TextBox edtPageHeight;
		private System.Windows.Forms.Label stPageHeight;
		private System.Windows.Forms.TextBox edtPageWidth;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.GroupBox Files;
		private MyControls.BrowseForFile browseForXMLFile;
		private MyControls.BrowseForDirectory browseForOutputDirectory;
		private System.Windows.Forms.GroupBox grpText;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label stFontSize;
		private System.Windows.Forms.Label stTextFont;
		private System.Windows.Forms.TextBox edtFontSize;
		private System.Windows.Forms.Label stTextX;
		private System.Windows.Forms.TextBox edtTextY;
		private System.Windows.Forms.Label stTextY;
		private System.Windows.Forms.TextBox edtTextX;
		private System.Windows.Forms.Button btnFont;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.TextBox edtTextFont;
		private System.Windows.Forms.Button btnTextColor;
		private System.Windows.Forms.TextBox edtTextColor;
		private System.Windows.Forms.Label stTextColor;
		private System.Windows.Forms.ColorDialog colorDialog1;
		#endregion


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public CalendarPrep()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
		}

		private void btnRun_Click(object sender, System.EventArgs e)
		{
			try
			{
				// Load XML file.
				XmlDocument	doc	=  new XmlDocument();

				doc.Load( browseForXMLFile.FileName );

				// Make sure output directory is there.
				DirectoryInfo	outputDir	=  new DirectoryInfo( browseForOutputDirectory.Directory );

				if ( !outputDir.Exists )
				{
					outputDir.Create();

					if ( !outputDir.Exists )
						throw new DirectoryNotFoundException( "Output directory not found and cannot be created!" );
				}

				// Start up Photoshop.
				Photoshop.Application		psApp	=  new Photoshop.Application();
				Photoshopper.Photoshopper	ps		=  new Photoshopper.Photoshopper( psApp );

				// Save preferences.
				PsUnits	originalRulerUnits	=  psApp.Preferences.RulerUnits;
			
				// Set ruler units to pixels
				psApp.Preferences.RulerUnits	=  PsUnits.psPixels;
			
				// Don't display dialogs
				psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

				// Create template doc.
				string	templateFilename	=  "Calendar Template";
				int		resolution			=  int.Parse( edtPageResolution.Text );
				Document	templateDoc			=  ps.CreateNewDocument( double.Parse( edtPageWidth.Text ), double.Parse( edtPageHeight.Text ), resolution, templateFilename );

				// Convert all dimensions to pixels.
				int	calendarWidth		=  (int) ( double.Parse( edtCalendarImagesWidth.Text )*resolution );
				int	calendarHeight		=  (int) ( double.Parse( edtCalendarImagesHeight.Text )*resolution );
				int	secondaryWidth		=  (int) ( double.Parse( edtSecondaryImagesWidth.Text )*resolution );
				int	secondaryHeight	=  (int) ( double.Parse( edtSecondaryImagesHeight.Text )*resolution );

				int	calendarTopX		=  (int) ( double.Parse( edtCalendarImagesTopX.Text )*resolution );
				int	calendarTopY		=  (int) ( double.Parse( edtCalendarImagesTopY.Text )*resolution );
				int	secondaryTopX		=  (int) ( double.Parse( edtSecondaryImagesTopX.Text )*resolution );
				int	secondaryTopY		=  (int) ( double.Parse( edtSecondaryImagesTopY.Text )*resolution );

				// Add some guides.
				ps.MakeGuide( calendarTopY, true );
				ps.MakeGuide( calendarTopY+calendarHeight, true );
				ps.MakeGuide( secondaryTopY, true );
				ps.MakeGuide( secondaryTopY+secondaryHeight, true );

				ps.MakeGuide( calendarTopX, false );
				ps.MakeGuide( calendarTopX+calendarWidth, false );
				ps.MakeGuide( secondaryTopX, false );

				// Save the template.
				ps.SaveAsPSD( templateDoc, outputDir.FullName, templateFilename );

				// Save this history state.
				HistoryState	savedState1	=  templateDoc.ActiveHistoryState;

				// Now process the files.
				// Get the root node.
				XmlNode	root	=  doc.FirstChild.NextSibling;

				// Get all the parent nodes.
				XmlNodeList	secondaryImages	=  root.ChildNodes;

				// Loop and process.
				for ( int i= 0;  i< secondaryImages.Count;  i++ )
				{
					// Get the secondary image node.
					XmlNode	parentNode	=  secondaryImages[ i ];

					// Get the File & Path attributes.
					string	filepath	=  parentNode.Attributes[ "Path" ].Value + @"\" + parentNode.Attributes[ "File" ].Value;

					// Open the image.
					Document secondaryImage	=  ps.OpenDoc( filepath );

					// Resize it.
					double	imgWidth		=  secondaryImage.Width;
					double	imgHeight	=  secondaryImage.Height;
					double	imgRes		=  secondaryImage.Resolution;

					double	width		=  secondaryWidth;
					double	height	=  secondaryHeight;

					if ( width == 0.0 )
						width	=  imgWidth*(height / imgHeight );
					else if ( height == 0.0 )
						height	=  imgHeight*( width / imgWidth );
					else if ( width <= 0.0 && height <= 0.0 )
						throw new Exception( "Invalid Width/Height value(s)!" );

					// Resize.
					secondaryImage.ResizeImage( null, null, resolution, PsResampleMethod.psNoResampling );
					secondaryImage.ResizeImage( width, height, resolution, PsResampleMethod.psBicubic );

					// Copy the entire secondary image.
					secondaryImage.Selection.SelectAll();
					secondaryImage.Selection.Copy( secondaryImage.Layers.Count > 1 ?  true : false );

					// Activate the template.
					ps.theApp.ActiveDocument	=  templateDoc;

					// Define where to place secondary image (must be in pixels).
					double[]	corners	=  new double[4] { secondaryTopX, secondaryTopY, secondaryTopX+width, secondaryTopY+height };
					Bounds	region	=  new Bounds( corners );

					// Select and paste.
					ps.SelectRectArea( region, PsSelectionType.psReplaceSelection, 0.0 );
					templateDoc.Paste( true );

					// Save this history state.
					HistoryState	savedState2	=  templateDoc.ActiveHistoryState;

					// Close secondary image.
					secondaryImage.Close( PsSaveOptions.psDoNotSaveChanges );

					// Get the child nodes.
					XmlNodeList	childNodes	=  parentNode.ChildNodes;

					// Loop through the child nodes.
					for ( int j= 0;  j< childNodes.Count;  j++ )
					{
						XmlNode	childNode	=  childNodes[ j ];

						// Get the File & Path attributes.
						filepath	=  childNode.Attributes[ "Path" ].Value + @"\" + childNode.Attributes[ "File" ].Value;

						// Open calendar image.
						Document	calendarImage	=  ps.OpenDoc( filepath );

						// Set ruler units to pixels for resizing.
						psApp.Preferences.RulerUnits	=  PsUnits.psPixels;
			
						// Resize it.
						imgWidth		=  calendarImage.Width;
						imgHeight	=  calendarImage.Height;
						imgRes		=  calendarImage.Resolution;

						calendarImage.ResizeImage( null, null, resolution, PsResampleMethod.psNoResampling );
						
						if ( imgWidth > imgHeight )
							calendarImage.ResizeImage( calendarWidth, null, resolution, PsResampleMethod.psBicubic );
						else
							calendarImage.ResizeImage( null, calendarHeight, resolution, PsResampleMethod.psBicubic );

						// Copy the entire image.
						calendarImage.Selection.SelectAll();
						calendarImage.Selection.Copy( calendarImage.Layers.Count > 1 ?  true : false );
			
						// Make the template active.
						ps.theApp.ActiveDocument	=  templateDoc;

						templateDoc.ActiveLayer	=  templateDoc.Layers[ "Background" ];

						// Define where to place calendar image (must be in pixels).
						corners	=  new double[4] { calendarTopX, calendarTopY, calendarTopX+calendarWidth, calendarTopY+calendarHeight };
						region	=  new Bounds( corners );

						// Select and paste.
						ps.SelectRectArea( region, PsSelectionType.psReplaceSelection, 0.0 );
						templateDoc.Paste( true );

						// Close calendar document.
						calendarImage.Close( PsSaveOptions.psDoNotSaveChanges );

						// Stitch together name for saving.
						string	saveAs	=  Path.GetFileNameWithoutExtension( parentNode.Attributes[ "File" ].Value ) + "_" + Path.GetFileNameWithoutExtension( childNode.Attributes[ "File" ].Value );

						ps.SaveAsPSD( templateDoc, outputDir.FullName, saveAs );

						// Reset history state on template doc.
						templateDoc.ActiveHistoryState	=  savedState2;
					}

					// Reset history state on template doc.
					templateDoc.ActiveHistoryState	=  savedState1;
				}

				// Close template doc.
				templateDoc.Close( PsSaveOptions.psDoNotSaveChanges );

				// Restore preferences.
				psApp.Preferences.RulerUnits	=  originalRulerUnits;
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.Files = new System.Windows.Forms.GroupBox();
			this.browseForOutputDirectory = new MyControls.BrowseForDirectory();
			this.browseForXMLFile = new MyControls.BrowseForFile();
			this.grpCalendarImages = new System.Windows.Forms.GroupBox();
			this.grpCalendarImagesSize = new System.Windows.Forms.GroupBox();
			this.stCalendarImagesWidth = new System.Windows.Forms.Label();
			this.edtCalendarImagesHeight = new System.Windows.Forms.TextBox();
			this.stCalendarImagesHeight = new System.Windows.Forms.Label();
			this.edtCalendarImagesWidth = new System.Windows.Forms.TextBox();
			this.grpCalendarImagesTopCorner = new System.Windows.Forms.GroupBox();
			this.stCalendarImagesTopX = new System.Windows.Forms.Label();
			this.edtCalendarImagesTopY = new System.Windows.Forms.TextBox();
			this.stCalendarImagesTopY = new System.Windows.Forms.Label();
			this.edtCalendarImagesTopX = new System.Windows.Forms.TextBox();
			this.grpSecondaryImages = new System.Windows.Forms.GroupBox();
			this.grpSecondaryImagesSize = new System.Windows.Forms.GroupBox();
			this.stSecondaryImagesWidth = new System.Windows.Forms.Label();
			this.edtSecondaryImagesHeight = new System.Windows.Forms.TextBox();
			this.stSecondaryImagesHeight = new System.Windows.Forms.Label();
			this.edtSecondaryImagesWidth = new System.Windows.Forms.TextBox();
			this.grpSecondaryImagesTopCorner = new System.Windows.Forms.GroupBox();
			this.stSecondaryImagesTopX = new System.Windows.Forms.Label();
			this.edtSecondaryImagesTopY = new System.Windows.Forms.TextBox();
			this.stSecondaryImagesTopY = new System.Windows.Forms.Label();
			this.edtSecondaryImagesTopX = new System.Windows.Forms.TextBox();
			this.grpPage = new System.Windows.Forms.GroupBox();
			this.edtPageResolution = new System.Windows.Forms.TextBox();
			this.stPageResolution = new System.Windows.Forms.Label();
			this.grpPageSize = new System.Windows.Forms.GroupBox();
			this.stPageWidth = new System.Windows.Forms.Label();
			this.edtPageHeight = new System.Windows.Forms.TextBox();
			this.stPageHeight = new System.Windows.Forms.Label();
			this.edtPageWidth = new System.Windows.Forms.TextBox();
			this.btnRun = new System.Windows.Forms.Button();
			this.grpText = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.stFontSize = new System.Windows.Forms.Label();
			this.edtTextFont = new System.Windows.Forms.TextBox();
			this.stTextFont = new System.Windows.Forms.Label();
			this.edtFontSize = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.stTextX = new System.Windows.Forms.Label();
			this.edtTextY = new System.Windows.Forms.TextBox();
			this.stTextY = new System.Windows.Forms.Label();
			this.edtTextX = new System.Windows.Forms.TextBox();
			this.btnFont = new System.Windows.Forms.Button();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.btnTextColor = new System.Windows.Forms.Button();
			this.edtTextColor = new System.Windows.Forms.TextBox();
			this.stTextColor = new System.Windows.Forms.Label();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.Files.SuspendLayout();
			this.grpCalendarImages.SuspendLayout();
			this.grpCalendarImagesSize.SuspendLayout();
			this.grpCalendarImagesTopCorner.SuspendLayout();
			this.grpSecondaryImages.SuspendLayout();
			this.grpSecondaryImagesSize.SuspendLayout();
			this.grpSecondaryImagesTopCorner.SuspendLayout();
			this.grpPage.SuspendLayout();
			this.grpPageSize.SuspendLayout();
			this.grpText.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// Files
			// 
			this.Files.Controls.Add(this.browseForOutputDirectory);
			this.Files.Controls.Add(this.browseForXMLFile);
			this.Files.Location = new System.Drawing.Point(16, 416);
			this.Files.Name = "Files";
			this.Files.Size = new System.Drawing.Size(584, 116);
			this.Files.TabIndex = 6;
			this.Files.TabStop = false;
			this.Files.Text = "Files";
			// 
			// browseForOutputDirectory
			// 
			this.browseForOutputDirectory.BrowseLabel = "...";
			this.browseForOutputDirectory.Label = "";
			this.browseForOutputDirectory.Location = new System.Drawing.Point(24, 64);
			this.browseForOutputDirectory.Name = "browseForOutputDirectory";
			this.browseForOutputDirectory.Size = new System.Drawing.Size(272, 40);
			this.browseForOutputDirectory.TabIndex = 2;
			// 
			// browseForXMLFile
			// 
			this.browseForXMLFile.BrowseLabel = "...";
			this.browseForXMLFile.Label = "";
			this.browseForXMLFile.Location = new System.Drawing.Point(24, 20);
			this.browseForXMLFile.Name = "browseForXMLFile";
			this.browseForXMLFile.Size = new System.Drawing.Size(273, 38);
			this.browseForXMLFile.TabIndex = 1;
			// 
			// grpCalendarImages
			// 
			this.grpCalendarImages.Controls.Add(this.grpCalendarImagesSize);
			this.grpCalendarImages.Controls.Add(this.grpCalendarImagesTopCorner);
			this.grpCalendarImages.Location = new System.Drawing.Point(16, 16);
			this.grpCalendarImages.Name = "grpCalendarImages";
			this.grpCalendarImages.Size = new System.Drawing.Size(224, 200);
			this.grpCalendarImages.TabIndex = 0;
			this.grpCalendarImages.TabStop = false;
			this.grpCalendarImages.Text = "Calendar Images";
			// 
			// grpCalendarImagesSize
			// 
			this.grpCalendarImagesSize.Controls.Add(this.stCalendarImagesWidth);
			this.grpCalendarImagesSize.Controls.Add(this.edtCalendarImagesHeight);
			this.grpCalendarImagesSize.Controls.Add(this.stCalendarImagesHeight);
			this.grpCalendarImagesSize.Controls.Add(this.edtCalendarImagesWidth);
			this.grpCalendarImagesSize.Location = new System.Drawing.Point(16, 24);
			this.grpCalendarImagesSize.Name = "grpCalendarImagesSize";
			this.grpCalendarImagesSize.Size = new System.Drawing.Size(191, 72);
			this.grpCalendarImagesSize.TabIndex = 0;
			this.grpCalendarImagesSize.TabStop = false;
			this.grpCalendarImagesSize.Text = "Size";
			// 
			// stCalendarImagesWidth
			// 
			this.stCalendarImagesWidth.Location = new System.Drawing.Point(16, 22);
			this.stCalendarImagesWidth.Name = "stCalendarImagesWidth";
			this.stCalendarImagesWidth.Size = new System.Drawing.Size(59, 12);
			this.stCalendarImagesWidth.TabIndex = 0;
			this.stCalendarImagesWidth.Text = "Width";
			// 
			// edtCalendarImagesHeight
			// 
			this.edtCalendarImagesHeight.Location = new System.Drawing.Point(110, 38);
			this.edtCalendarImagesHeight.Name = "edtCalendarImagesHeight";
			this.edtCalendarImagesHeight.Size = new System.Drawing.Size(64, 20);
			this.edtCalendarImagesHeight.TabIndex = 3;
			this.edtCalendarImagesHeight.Text = "";
			// 
			// stCalendarImagesHeight
			// 
			this.stCalendarImagesHeight.Location = new System.Drawing.Point(110, 22);
			this.stCalendarImagesHeight.Name = "stCalendarImagesHeight";
			this.stCalendarImagesHeight.Size = new System.Drawing.Size(59, 12);
			this.stCalendarImagesHeight.TabIndex = 2;
			this.stCalendarImagesHeight.Text = "Height";
			// 
			// edtCalendarImagesWidth
			// 
			this.edtCalendarImagesWidth.Location = new System.Drawing.Point(16, 38);
			this.edtCalendarImagesWidth.Name = "edtCalendarImagesWidth";
			this.edtCalendarImagesWidth.Size = new System.Drawing.Size(64, 20);
			this.edtCalendarImagesWidth.TabIndex = 1;
			this.edtCalendarImagesWidth.Text = "";
			// 
			// grpCalendarImagesTopCorner
			// 
			this.grpCalendarImagesTopCorner.Controls.Add(this.stCalendarImagesTopX);
			this.grpCalendarImagesTopCorner.Controls.Add(this.edtCalendarImagesTopY);
			this.grpCalendarImagesTopCorner.Controls.Add(this.stCalendarImagesTopY);
			this.grpCalendarImagesTopCorner.Controls.Add(this.edtCalendarImagesTopX);
			this.grpCalendarImagesTopCorner.Location = new System.Drawing.Point(16, 110);
			this.grpCalendarImagesTopCorner.Name = "grpCalendarImagesTopCorner";
			this.grpCalendarImagesTopCorner.Size = new System.Drawing.Size(191, 72);
			this.grpCalendarImagesTopCorner.TabIndex = 4;
			this.grpCalendarImagesTopCorner.TabStop = false;
			this.grpCalendarImagesTopCorner.Text = "Top Corner";
			// 
			// stCalendarImagesTopX
			// 
			this.stCalendarImagesTopX.Location = new System.Drawing.Point(16, 22);
			this.stCalendarImagesTopX.Name = "stCalendarImagesTopX";
			this.stCalendarImagesTopX.Size = new System.Drawing.Size(59, 12);
			this.stCalendarImagesTopX.TabIndex = 0;
			this.stCalendarImagesTopX.Text = "X";
			// 
			// edtCalendarImagesTopY
			// 
			this.edtCalendarImagesTopY.Location = new System.Drawing.Point(110, 38);
			this.edtCalendarImagesTopY.Name = "edtCalendarImagesTopY";
			this.edtCalendarImagesTopY.Size = new System.Drawing.Size(64, 20);
			this.edtCalendarImagesTopY.TabIndex = 3;
			this.edtCalendarImagesTopY.Text = "";
			// 
			// stCalendarImagesTopY
			// 
			this.stCalendarImagesTopY.Location = new System.Drawing.Point(110, 22);
			this.stCalendarImagesTopY.Name = "stCalendarImagesTopY";
			this.stCalendarImagesTopY.Size = new System.Drawing.Size(59, 12);
			this.stCalendarImagesTopY.TabIndex = 2;
			this.stCalendarImagesTopY.Text = "Y";
			// 
			// edtCalendarImagesTopX
			// 
			this.edtCalendarImagesTopX.Location = new System.Drawing.Point(16, 38);
			this.edtCalendarImagesTopX.Name = "edtCalendarImagesTopX";
			this.edtCalendarImagesTopX.Size = new System.Drawing.Size(64, 20);
			this.edtCalendarImagesTopX.TabIndex = 1;
			this.edtCalendarImagesTopX.Text = "";
			// 
			// grpSecondaryImages
			// 
			this.grpSecondaryImages.Controls.Add(this.grpSecondaryImagesSize);
			this.grpSecondaryImages.Controls.Add(this.grpSecondaryImagesTopCorner);
			this.grpSecondaryImages.Location = new System.Drawing.Point(260, 16);
			this.grpSecondaryImages.Name = "grpSecondaryImages";
			this.grpSecondaryImages.Size = new System.Drawing.Size(224, 200);
			this.grpSecondaryImages.TabIndex = 5;
			this.grpSecondaryImages.TabStop = false;
			this.grpSecondaryImages.Text = "Secondary Images";
			// 
			// grpSecondaryImagesSize
			// 
			this.grpSecondaryImagesSize.Controls.Add(this.stSecondaryImagesWidth);
			this.grpSecondaryImagesSize.Controls.Add(this.edtSecondaryImagesHeight);
			this.grpSecondaryImagesSize.Controls.Add(this.stSecondaryImagesHeight);
			this.grpSecondaryImagesSize.Controls.Add(this.edtSecondaryImagesWidth);
			this.grpSecondaryImagesSize.Location = new System.Drawing.Point(16, 24);
			this.grpSecondaryImagesSize.Name = "grpSecondaryImagesSize";
			this.grpSecondaryImagesSize.Size = new System.Drawing.Size(191, 72);
			this.grpSecondaryImagesSize.TabIndex = 0;
			this.grpSecondaryImagesSize.TabStop = false;
			this.grpSecondaryImagesSize.Text = "Size";
			// 
			// stSecondaryImagesWidth
			// 
			this.stSecondaryImagesWidth.Location = new System.Drawing.Point(16, 22);
			this.stSecondaryImagesWidth.Name = "stSecondaryImagesWidth";
			this.stSecondaryImagesWidth.Size = new System.Drawing.Size(59, 12);
			this.stSecondaryImagesWidth.TabIndex = 0;
			this.stSecondaryImagesWidth.Text = "Width";
			// 
			// edtSecondaryImagesHeight
			// 
			this.edtSecondaryImagesHeight.Location = new System.Drawing.Point(110, 38);
			this.edtSecondaryImagesHeight.Name = "edtSecondaryImagesHeight";
			this.edtSecondaryImagesHeight.Size = new System.Drawing.Size(64, 20);
			this.edtSecondaryImagesHeight.TabIndex = 3;
			this.edtSecondaryImagesHeight.Text = "";
			// 
			// stSecondaryImagesHeight
			// 
			this.stSecondaryImagesHeight.Location = new System.Drawing.Point(110, 22);
			this.stSecondaryImagesHeight.Name = "stSecondaryImagesHeight";
			this.stSecondaryImagesHeight.Size = new System.Drawing.Size(59, 12);
			this.stSecondaryImagesHeight.TabIndex = 2;
			this.stSecondaryImagesHeight.Text = "Height";
			// 
			// edtSecondaryImagesWidth
			// 
			this.edtSecondaryImagesWidth.Location = new System.Drawing.Point(16, 38);
			this.edtSecondaryImagesWidth.Name = "edtSecondaryImagesWidth";
			this.edtSecondaryImagesWidth.Size = new System.Drawing.Size(64, 20);
			this.edtSecondaryImagesWidth.TabIndex = 1;
			this.edtSecondaryImagesWidth.Text = "";
			// 
			// grpSecondaryImagesTopCorner
			// 
			this.grpSecondaryImagesTopCorner.Controls.Add(this.stSecondaryImagesTopX);
			this.grpSecondaryImagesTopCorner.Controls.Add(this.edtSecondaryImagesTopY);
			this.grpSecondaryImagesTopCorner.Controls.Add(this.stSecondaryImagesTopY);
			this.grpSecondaryImagesTopCorner.Controls.Add(this.edtSecondaryImagesTopX);
			this.grpSecondaryImagesTopCorner.Location = new System.Drawing.Point(16, 110);
			this.grpSecondaryImagesTopCorner.Name = "grpSecondaryImagesTopCorner";
			this.grpSecondaryImagesTopCorner.Size = new System.Drawing.Size(191, 72);
			this.grpSecondaryImagesTopCorner.TabIndex = 4;
			this.grpSecondaryImagesTopCorner.TabStop = false;
			this.grpSecondaryImagesTopCorner.Text = "Top Corner";
			// 
			// stSecondaryImagesTopX
			// 
			this.stSecondaryImagesTopX.Location = new System.Drawing.Point(16, 22);
			this.stSecondaryImagesTopX.Name = "stSecondaryImagesTopX";
			this.stSecondaryImagesTopX.Size = new System.Drawing.Size(59, 12);
			this.stSecondaryImagesTopX.TabIndex = 0;
			this.stSecondaryImagesTopX.Text = "X";
			// 
			// edtSecondaryImagesTopY
			// 
			this.edtSecondaryImagesTopY.Location = new System.Drawing.Point(110, 38);
			this.edtSecondaryImagesTopY.Name = "edtSecondaryImagesTopY";
			this.edtSecondaryImagesTopY.Size = new System.Drawing.Size(64, 20);
			this.edtSecondaryImagesTopY.TabIndex = 3;
			this.edtSecondaryImagesTopY.Text = "";
			// 
			// stSecondaryImagesTopY
			// 
			this.stSecondaryImagesTopY.Location = new System.Drawing.Point(110, 22);
			this.stSecondaryImagesTopY.Name = "stSecondaryImagesTopY";
			this.stSecondaryImagesTopY.Size = new System.Drawing.Size(59, 12);
			this.stSecondaryImagesTopY.TabIndex = 2;
			this.stSecondaryImagesTopY.Text = "Y";
			// 
			// edtSecondaryImagesTopX
			// 
			this.edtSecondaryImagesTopX.Location = new System.Drawing.Point(16, 38);
			this.edtSecondaryImagesTopX.Name = "edtSecondaryImagesTopX";
			this.edtSecondaryImagesTopX.Size = new System.Drawing.Size(64, 20);
			this.edtSecondaryImagesTopX.TabIndex = 1;
			this.edtSecondaryImagesTopX.Text = "";
			// 
			// grpPage
			// 
			this.grpPage.Controls.Add(this.edtPageResolution);
			this.grpPage.Controls.Add(this.stPageResolution);
			this.grpPage.Controls.Add(this.grpPageSize);
			this.grpPage.Location = new System.Drawing.Point(16, 226);
			this.grpPage.Name = "grpPage";
			this.grpPage.Size = new System.Drawing.Size(224, 180);
			this.grpPage.TabIndex = 5;
			this.grpPage.TabStop = false;
			this.grpPage.Text = "Page";
			// 
			// edtPageResolution
			// 
			this.edtPageResolution.Location = new System.Drawing.Point(32, 136);
			this.edtPageResolution.Name = "edtPageResolution";
			this.edtPageResolution.TabIndex = 2;
			this.edtPageResolution.Text = "";
			// 
			// stPageResolution
			// 
			this.stPageResolution.Location = new System.Drawing.Point(32, 120);
			this.stPageResolution.Name = "stPageResolution";
			this.stPageResolution.Size = new System.Drawing.Size(72, 12);
			this.stPageResolution.TabIndex = 1;
			this.stPageResolution.Text = "Resolution";
			// 
			// grpPageSize
			// 
			this.grpPageSize.Controls.Add(this.stPageWidth);
			this.grpPageSize.Controls.Add(this.edtPageHeight);
			this.grpPageSize.Controls.Add(this.stPageHeight);
			this.grpPageSize.Controls.Add(this.edtPageWidth);
			this.grpPageSize.Location = new System.Drawing.Point(16, 24);
			this.grpPageSize.Name = "grpPageSize";
			this.grpPageSize.Size = new System.Drawing.Size(191, 72);
			this.grpPageSize.TabIndex = 0;
			this.grpPageSize.TabStop = false;
			this.grpPageSize.Text = "Size";
			// 
			// stPageWidth
			// 
			this.stPageWidth.Location = new System.Drawing.Point(16, 22);
			this.stPageWidth.Name = "stPageWidth";
			this.stPageWidth.Size = new System.Drawing.Size(59, 12);
			this.stPageWidth.TabIndex = 0;
			this.stPageWidth.Text = "Width";
			// 
			// edtPageHeight
			// 
			this.edtPageHeight.Location = new System.Drawing.Point(110, 38);
			this.edtPageHeight.Name = "edtPageHeight";
			this.edtPageHeight.Size = new System.Drawing.Size(64, 20);
			this.edtPageHeight.TabIndex = 3;
			this.edtPageHeight.Text = "";
			// 
			// stPageHeight
			// 
			this.stPageHeight.Location = new System.Drawing.Point(110, 22);
			this.stPageHeight.Name = "stPageHeight";
			this.stPageHeight.Size = new System.Drawing.Size(59, 12);
			this.stPageHeight.TabIndex = 2;
			this.stPageHeight.Text = "Height";
			// 
			// edtPageWidth
			// 
			this.edtPageWidth.Location = new System.Drawing.Point(16, 38);
			this.edtPageWidth.Name = "edtPageWidth";
			this.edtPageWidth.Size = new System.Drawing.Size(64, 20);
			this.edtPageWidth.TabIndex = 1;
			this.edtPageWidth.Text = "";
			// 
			// btnRun
			// 
			this.btnRun.Location = new System.Drawing.Point(514, 53);
			this.btnRun.Name = "btnRun";
			this.btnRun.TabIndex = 2;
			this.btnRun.Text = "Run";
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// grpText
			// 
			this.grpText.Controls.Add(this.groupBox2);
			this.grpText.Controls.Add(this.groupBox3);
			this.grpText.Location = new System.Drawing.Point(260, 226);
			this.grpText.Name = "grpText";
			this.grpText.Size = new System.Drawing.Size(340, 180);
			this.grpText.TabIndex = 6;
			this.grpText.TabStop = false;
			this.grpText.Text = "Calendar Text";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnTextColor);
			this.groupBox2.Controls.Add(this.edtTextColor);
			this.groupBox2.Controls.Add(this.stTextColor);
			this.groupBox2.Controls.Add(this.btnFont);
			this.groupBox2.Controls.Add(this.stFontSize);
			this.groupBox2.Controls.Add(this.edtTextFont);
			this.groupBox2.Controls.Add(this.stTextFont);
			this.groupBox2.Controls.Add(this.edtFontSize);
			this.groupBox2.Location = new System.Drawing.Point(16, 24);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 140);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Font";
			// 
			// stFontSize
			// 
			this.stFontSize.Location = new System.Drawing.Point(16, 22);
			this.stFontSize.Name = "stFontSize";
			this.stFontSize.Size = new System.Drawing.Size(36, 12);
			this.stFontSize.TabIndex = 0;
			this.stFontSize.Text = "Size";
			// 
			// edtTextFont
			// 
			this.edtTextFont.Location = new System.Drawing.Point(16, 94);
			this.edtTextFont.Name = "edtTextFont";
			this.edtTextFont.Size = new System.Drawing.Size(134, 20);
			this.edtTextFont.TabIndex = 3;
			this.edtTextFont.Text = "";
			// 
			// stTextFont
			// 
			this.stTextFont.Location = new System.Drawing.Point(16, 78);
			this.stTextFont.Name = "stTextFont";
			this.stTextFont.Size = new System.Drawing.Size(59, 12);
			this.stTextFont.TabIndex = 2;
			this.stTextFont.Text = "Font";
			// 
			// edtFontSize
			// 
			this.edtFontSize.Location = new System.Drawing.Point(16, 38);
			this.edtFontSize.Name = "edtFontSize";
			this.edtFontSize.Size = new System.Drawing.Size(35, 20);
			this.edtFontSize.TabIndex = 1;
			this.edtFontSize.Text = "24";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.stTextX);
			this.groupBox3.Controls.Add(this.edtTextY);
			this.groupBox3.Controls.Add(this.stTextY);
			this.groupBox3.Controls.Add(this.edtTextX);
			this.groupBox3.Location = new System.Drawing.Point(240, 24);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(80, 140);
			this.groupBox3.TabIndex = 4;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Top Corner";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.TabIndex = 0;
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(0, 0);
			this.textBox4.Name = "textBox4";
			this.textBox4.TabIndex = 0;
			this.textBox4.Text = "";
			// 
			// stTextX
			// 
			this.stTextX.Location = new System.Drawing.Point(16, 22);
			this.stTextX.Name = "stTextX";
			this.stTextX.Size = new System.Drawing.Size(25, 12);
			this.stTextX.TabIndex = 4;
			this.stTextX.Text = "X";
			// 
			// edtTextY
			// 
			this.edtTextY.Location = new System.Drawing.Point(16, 94);
			this.edtTextY.Name = "edtTextY";
			this.edtTextY.Size = new System.Drawing.Size(46, 20);
			this.edtTextY.TabIndex = 7;
			this.edtTextY.Text = "";
			// 
			// stTextY
			// 
			this.stTextY.Location = new System.Drawing.Point(16, 78);
			this.stTextY.Name = "stTextY";
			this.stTextY.Size = new System.Drawing.Size(24, 12);
			this.stTextY.TabIndex = 6;
			this.stTextY.Text = "Y";
			// 
			// edtTextX
			// 
			this.edtTextX.Location = new System.Drawing.Point(16, 38);
			this.edtTextX.Name = "edtTextX";
			this.edtTextX.Size = new System.Drawing.Size(46, 20);
			this.edtTextX.TabIndex = 5;
			this.edtTextX.Text = "";
			// 
			// btnFont
			// 
			this.btnFont.Location = new System.Drawing.Point(160, 93);
			this.btnFont.Name = "btnFont";
			this.btnFont.Size = new System.Drawing.Size(25, 23);
			this.btnFont.TabIndex = 4;
			this.btnFont.Text = "...";
			this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
			// 
			// btnTextColor
			// 
			this.btnTextColor.Location = new System.Drawing.Point(160, 37);
			this.btnTextColor.Name = "btnTextColor";
			this.btnTextColor.Size = new System.Drawing.Size(25, 23);
			this.btnTextColor.TabIndex = 7;
			this.btnTextColor.Text = "...";
			this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
			// 
			// edtTextColor
			// 
			this.edtTextColor.Location = new System.Drawing.Point(68, 38);
			this.edtTextColor.Name = "edtTextColor";
			this.edtTextColor.Size = new System.Drawing.Size(82, 20);
			this.edtTextColor.TabIndex = 6;
			this.edtTextColor.Text = "";
			// 
			// stTextColor
			// 
			this.stTextColor.Location = new System.Drawing.Point(68, 22);
			this.stTextColor.Name = "stTextColor";
			this.stTextColor.Size = new System.Drawing.Size(59, 12);
			this.stTextColor.TabIndex = 5;
			this.stTextColor.Text = "Color";
			// 
			// CalendarPrep
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(617, 548);
			this.ControlBox = false;
			this.Controls.Add(this.grpCalendarImages);
			this.Controls.Add(this.grpSecondaryImages);
			this.Controls.Add(this.grpPage);
			this.Controls.Add(this.Files);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.grpText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CalendarPrep";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "CalendarPrep";
			this.Load += new System.EventHandler(this.CalendarPrep_Load);
			this.Files.ResumeLayout(false);
			this.grpCalendarImages.ResumeLayout(false);
			this.grpCalendarImagesSize.ResumeLayout(false);
			this.grpCalendarImagesTopCorner.ResumeLayout(false);
			this.grpSecondaryImages.ResumeLayout(false);
			this.grpSecondaryImagesSize.ResumeLayout(false);
			this.grpSecondaryImagesTopCorner.ResumeLayout(false);
			this.grpPage.ResumeLayout(false);
			this.grpPageSize.ResumeLayout(false);
			this.grpText.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CalendarPrep_Load( object sender, System.EventArgs e )
		{
			// Update the text.
			browseForXMLFile.Label				=  "XML Input File";
			browseForOutputDirectory.Label	=  "Output Directory";

			browseForXMLFile.LabelIsVisible				=  true;
			browseForOutputDirectory.LabelIsVisible	=  true;

			browseForXMLFile.Width				=  536;
			browseForOutputDirectory.Width	=  536;
		}

		private void btnFont_Click(object sender, System.EventArgs e)
		{
			// Bring up the font dialog.
			fontDialog1	=  new FontDialog();

			Font	font	=  new Font( edtTextFont.Text, float.Parse( edtFontSize.Text ) );

			fontDialog1.Font	=  font;

			if ( fontDialog1.ShowDialog() == DialogResult.OK )
			{
				font	=  fontDialog1.Font;

				edtFontSize.Text	=  ( font.Size ).ToString();
				edtTextFont.Text	=  font.Name;
			}

			fontDialog1.Dispose();
		}

		private void btnTextColor_Click(object sender, System.EventArgs e)
		{
			colorDialog1	=  new ColorDialog();

			RgbColor	fontColor	=  new RgbColor( edtTextColor.Text );

			colorDialog1.Color			=  Color.FromArgb( fontColor.R, fontColor.G, fontColor.B );
			colorDialog1.AnyColor		=  true;
			colorDialog1.AllowFullOpen	=  true;
			colorDialog1.CustomColors	=  new int[] { colorDialog1.Color.ToArgb() };

			if ( colorDialog1.ShowDialog() == DialogResult.OK )
			{
				Color	color	=  colorDialog1.Color;

				edtTextColor.Text	=  RgbColor.FromColor( color.R, color.G, color. B );
			}

			colorDialog1.Dispose();
		}
	}
}
