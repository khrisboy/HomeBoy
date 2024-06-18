using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Xml;

using MyControls;
using MyClasses;
using Photoshop;
using Photoshopper;

namespace PhotoshopUtilities
{
	public class LabelMaker : MyControls.MyWindowsForm
	{
		#region Member Variables
		private Photoshop.Application						psApp	=  null;
		private Photoshopper.Photoshopper				ps		=  null;
		private Thread											processThread;
		private bool											processing;
		private LabelDataArray								labelDataAr;
		private LabelTextArray								labelTextAr;
		private LabelText										currentLT	=  null;

		private System.Windows.Forms.TabControl		theTabs;
		private System.Windows.Forms.TabPage			tabCreate;
		private System.Windows.Forms.Button				btnMakeLabel;
		private MyControls.FilesListView					theFilesListView;
		private MyControls.MyGroupBox						grpTemplates;
		private MyControls.BrowseForFile					browseForLabelTemplate;
		private MyControls.BrowseForLabelTemplates	browseForLabelXml;
		private MyControls.MyGroupBox						grpSaveAs;
		private System.Windows.Forms.Label				stSaveAs;
		private System.Windows.Forms.TextBox			edtSaveAs;
		private System.Windows.Forms.GroupBox			grpLabelText;
		private System.Windows.Forms.TextBox			edtLabelText;
		private System.Windows.Forms.Label				stLabelTextLines;
		private System.Windows.Forms.ComboBox			cbTextLines;
		private System.Windows.Forms.Label				stTextLine;

		private System.Windows.Forms.TabPage			tabPrint;
		private MyControls.BrowseForFile					browseForPageTemplate;
		private MyControls.MyGroupBox						grpLabelFiles;
		private MyControls.BrowseForFile					browseForLabelFile1;
		private MyControls.BrowseForFile					browseForLabelFile2;
		private System.Windows.Forms.PictureBox		pbLabelFile1;
		private System.Windows.Forms.PictureBox		pbLabelFile2;
		private MyControls.MyGroupBox						grpPageFile;
		private System.Windows.Forms.Button				btnPrint;
		private MyControls.MyGroupBox grpCenters;
		private MyControls.XYCoordinates xyCoordinates1;
		private MyControls.XYCoordinates xyCoordinates2;
		private System.Windows.Forms.TextBox edtLabelsPageSaveAs;
		private System.Windows.Forms.Label stLabelsPageSaveAs;
		private System.ComponentModel.IContainer		components	=  null;
		#endregion

		public LabelMaker()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			currentLT	=  null;
		}

		private void GetLabelXml()
		{
			// Load XML file.
			XmlDocument	doc	=  new XmlDocument();

			doc.Load( browseForLabelXml.FileName );

			// Now process the files.
			// Get the root node.
			XmlNode	root	=  doc.FirstChild.NextSibling;

			XmlNodeList	images	=  root.SelectNodes( "child::Pattern/Images/Image" );

			// Loop and process the image locations.
			labelDataAr	=  new LabelDataArray();

			foreach ( XmlNode node in images )
			{
				LabelData	ld	=  new LabelData();

				ld.Load( node );

				labelDataAr.Add( ld );
			}

			//***XmlNodeList	positions	=  root.SelectNodes( "child::Pattern[attribute::Name=\"37 Images\"]/Label/Text" );
			XmlNodeList	positions	=  root.SelectNodes( "child::Pattern/Label/Text" );

			// Loop and process text information.
			labelTextAr	=  new LabelTextArray();

			foreach ( XmlNode node in positions )
			{
				LabelText	lt	=  new LabelText();

				lt.Load( node );

				labelTextAr.Add( lt );
			}
		}

		// Make the label.
		private void MakeLabel()
		{
			PsUnits	originalRulerUnits	=  PsUnits.psPixels;

			try
			{
				// Get the template information.
				GetLabelXml();

				// Make sure we get the latest text changes.
				currentLT.Text	=  edtLabelText.Text;

				// Might take a while.
				Cursor.Current	=  Cursors.WaitCursor;

				// Get the list of files into an array.
				ArrayListUnique	filesAr	=  theFilesListView.Files;

				if ( filesAr.Count == 0 )
				{
					throw new Exception( "There must be at least 1 file to process!" );
				}

				// Must be something to save as.
				string	saveAsFilename	=  this.edtSaveAs.Text;

				if ( saveAsFilename == string.Empty )
				{
					throw new Exception( "Save As cannot be blank!" );
				}

				// Start up Photoshop.
				psApp	=  new Photoshop.Application();
				ps		=  new Photoshopper.Photoshopper( psApp );

				// Save preferences.
				originalRulerUnits	=  psApp.Preferences.RulerUnits;
			
				// Set ruler units to pixels
				psApp.Preferences.RulerUnits	=  PsUnits.psPixels;
			
				// Don't display dialogs
				psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

				// Open the Label template.
				Document	templateDoc	=  ps.OpenDoc( browseForLabelTemplate.FileName );

				// Get the resolution.
				double	resolution	=  templateDoc.Resolution;
				
				// Loop through the files.
				for ( int i= 0;  i< filesAr.Count;  i++ )
				{
					MyFileInfo	fileInfo	=  (MyFileInfo) filesAr[ i ];

					// Select the file that we're processing.
					theFilesListView.Select( fileInfo );

					// Get the label data.
					LabelData	ld	=  labelDataAr[ i ];

					// Open the image.
					Document	imageDoc	=  ps.OpenDoc( fileInfo.FullName );

					// Rotate?
					if ( imageDoc.Height > imageDoc.Width )
						imageDoc.RotateCanvas( -90.0 );

					// Resize.
					imageDoc.ResizeImage( null, null, resolution, PsResampleMethod.psNoResampling );
					imageDoc.ResizeImage( ld.Width, ld.Height, resolution, PsResampleMethod.psBicubic );

					// Select & Copy.
					imageDoc.Selection.SelectAll();
					imageDoc.Selection.Copy( imageDoc.Layers.Count > 1 ?  true : false );

					// Activate the template.
					ps.theApp.ActiveDocument	=  templateDoc;

					// Define where to place the image (must be in pixels).
					double[]	corners	=  new double[4] { ld.X, ld.Y, ld.Right, ld.Bottom };
					Bounds	region	=  new Bounds( corners );

					// Select and paste.
					ps.SelectRectArea( region, PsSelectionType.psReplaceSelection, 0.0 );
					templateDoc.Paste( true );

					// Close the file.
					imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
				}

				// Set the text.
				labelTextAr	=  (LabelTextArray) cbTextLines.DataSource;

				foreach ( LabelText lt in labelTextAr )
				{
					// Only if there's some text to write!
					if ( lt.Text != null && lt.Text != "" )
					{
						ArtLayer textLayer	=  templateDoc.ArtLayers.Add();

						textLayer.Kind	=  Photoshop.PsLayerKind.psTextLayer;
						
						textLayer.TextItem.Contents			=  lt.Text;
						textLayer.TextItem.Font					=  lt.FontName;
						textLayer.TextItem.Size					=  lt.FontSize;
						textLayer.TextItem.Justification		=  lt.Alignment;
						textLayer.TextItem.AntiAliasMethod	=  lt.AntiAlias;

						Array position =  (Array) textLayer.TextItem.Position;

						position.SetValue( lt.X, 0 );
						position.SetValue( lt.Y, 1 );

						textLayer.TextItem.Position	=  position;
					}
				}

				// Merge the layers.
				ArtLayers	layers	=  templateDoc.ArtLayers;

				for ( int i= layers.Count;  i> 0;  i-- )
				{
					layers[ i ].Merge();
				}

				// Get the channels.
				Photoshop.Channels	channels	=  templateDoc.Channels;

				// Loop through the channels and get rid of any masks.
				for ( int j= channels.Count;  j> 0;  j-- )
				{
					Channel	channel	=  channels[ j ];

					string			channelName	=  channel.Name;
					PsChannelType	channelType	=  channel.Kind;

					if ( channelType == PsChannelType.psMaskedAreaAlphaChannel )
					{
						channel.Delete();
					}
				}

				// Save the label file.
				ps.SaveAsTIFF( templateDoc, "", saveAsFilename );

				// Close the file.
				templateDoc.Close( PsSaveOptions.psDoNotSaveChanges );
			}

			catch( ThreadAbortException )
			{
				// We were aborted!
			}

			catch( Exception ex )
			{
				// Uh oh!
				MessageBox.Show( ex.Message, "Making Label" );
			}

			finally
			{
				// Restore preferences.
				if ( psApp != null )
					psApp.Preferences.RulerUnits	=  originalRulerUnits;

				// Restore the cursor.
				Cursor.Current	=  Cursors.Default;

				processing	=  false;

				// Let 'em know we're done.
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnMakeLabel = new System.Windows.Forms.Button();
			this.theFilesListView = new MyControls.FilesListView();
			this.grpTemplates = new MyControls.MyGroupBox();
			this.grpLabelText = new System.Windows.Forms.GroupBox();
			this.cbTextLines = new System.Windows.Forms.ComboBox();
			this.edtLabelText = new System.Windows.Forms.TextBox();
			this.stTextLine = new System.Windows.Forms.Label();
			this.stLabelTextLines = new System.Windows.Forms.Label();
			this.browseForLabelXml = new MyControls.BrowseForLabelTemplates();
			this.browseForLabelTemplate = new MyControls.BrowseForFile();
			this.grpSaveAs = new MyControls.MyGroupBox();
			this.edtSaveAs = new System.Windows.Forms.TextBox();
			this.stSaveAs = new System.Windows.Forms.Label();
			this.theTabs = new System.Windows.Forms.TabControl();
			this.tabCreate = new System.Windows.Forms.TabPage();
			this.tabPrint = new System.Windows.Forms.TabPage();
			this.btnPrint = new System.Windows.Forms.Button();
			this.grpPageFile = new MyControls.MyGroupBox();
			this.edtLabelsPageSaveAs = new System.Windows.Forms.TextBox();
			this.stLabelsPageSaveAs = new System.Windows.Forms.Label();
			this.grpCenters = new MyControls.MyGroupBox();
			this.xyCoordinates2 = new MyControls.XYCoordinates();
			this.xyCoordinates1 = new MyControls.XYCoordinates();
			this.browseForPageTemplate = new MyControls.BrowseForFile();
			this.grpLabelFiles = new MyControls.MyGroupBox();
			this.pbLabelFile2 = new System.Windows.Forms.PictureBox();
			this.pbLabelFile1 = new System.Windows.Forms.PictureBox();
			this.browseForLabelFile2 = new MyControls.BrowseForFile();
			this.browseForLabelFile1 = new MyControls.BrowseForFile();
			this.grpTemplates.SuspendLayout();
			this.grpLabelText.SuspendLayout();
			this.grpSaveAs.SuspendLayout();
			this.theTabs.SuspendLayout();
			this.tabCreate.SuspendLayout();
			this.tabPrint.SuspendLayout();
			this.grpPageFile.SuspendLayout();
			this.grpCenters.SuspendLayout();
			this.grpLabelFiles.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.pbLabelFile2 ) ).BeginInit();
			( (System.ComponentModel.ISupportInitialize) ( this.pbLabelFile1 ) ).BeginInit();
			this.SuspendLayout();
			// 
			// btnMakeLabel
			// 
			this.btnMakeLabel.Location = new System.Drawing.Point( 504, 57 );
			this.btnMakeLabel.Name = "btnMakeLabel";
			this.btnMakeLabel.Size = new System.Drawing.Size( 75, 23 );
			this.btnMakeLabel.TabIndex = 0;
			this.btnMakeLabel.Text = "Make Label";
			this.btnMakeLabel.Click += new System.EventHandler( this.btnMakeLabel_Click );
			// 
			// theFilesListView
			// 
			this.theFilesListView.Location = new System.Drawing.Point( 16, 16 );
			this.theFilesListView.Name = "theFilesListView";
			this.theFilesListView.Size = new System.Drawing.Size( 420, 200 );
			this.theFilesListView.TabIndex = 1;
			// 
			// grpTemplates
			// 
			this.grpTemplates.Controls.Add( this.grpLabelText );
			this.grpTemplates.Controls.Add( this.browseForLabelXml );
			this.grpTemplates.Controls.Add( this.browseForLabelTemplate );
			this.grpTemplates.Location = new System.Drawing.Point( 16, 232 );
			this.grpTemplates.Name = "grpTemplates";
			this.grpTemplates.Size = new System.Drawing.Size( 616, 216 );
			this.grpTemplates.TabIndex = 0;
			this.grpTemplates.TabStop = false;
			this.grpTemplates.Text = "Templates";
			// 
			// grpLabelText
			// 
			this.grpLabelText.Controls.Add( this.cbTextLines );
			this.grpLabelText.Controls.Add( this.edtLabelText );
			this.grpLabelText.Controls.Add( this.stTextLine );
			this.grpLabelText.Controls.Add( this.stLabelTextLines );
			this.grpLabelText.Location = new System.Drawing.Point( 16, 72 );
			this.grpLabelText.Name = "grpLabelText";
			this.grpLabelText.Size = new System.Drawing.Size( 272, 127 );
			this.grpLabelText.TabIndex = 3;
			this.grpLabelText.TabStop = false;
			this.grpLabelText.Text = "Label Text";
			// 
			// cbTextLines
			// 
			this.cbTextLines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTextLines.Location = new System.Drawing.Point( 16, 40 );
			this.cbTextLines.MaxDropDownItems = 4;
			this.cbTextLines.Name = "cbTextLines";
			this.cbTextLines.Size = new System.Drawing.Size( 240, 21 );
			this.cbTextLines.TabIndex = 4;
			this.cbTextLines.SelectedIndexChanged += new System.EventHandler( this.cbTextLines_SelectedIndexChanged );
			// 
			// edtLabelText
			// 
			this.edtLabelText.Location = new System.Drawing.Point( 16, 86 );
			this.edtLabelText.Name = "edtLabelText";
			this.edtLabelText.Size = new System.Drawing.Size( 240, 20 );
			this.edtLabelText.TabIndex = 3;
			// 
			// stTextLine
			// 
			this.stTextLine.Location = new System.Drawing.Point( 16, 70 );
			this.stTextLine.Name = "stTextLine";
			this.stTextLine.Size = new System.Drawing.Size( 240, 14 );
			this.stTextLine.TabIndex = 2;
			this.stTextLine.Text = "Line 1";
			// 
			// stLabelTextLines
			// 
			this.stLabelTextLines.Location = new System.Drawing.Point( 16, 24 );
			this.stLabelTextLines.Name = "stLabelTextLines";
			this.stLabelTextLines.Size = new System.Drawing.Size( 59, 14 );
			this.stLabelTextLines.TabIndex = 0;
			this.stLabelTextLines.Text = "Text Lines";
			// 
			// browseForLabelXml
			// 
			this.browseForLabelXml.BrowseLabel = "...";
			this.browseForLabelXml.DisplayFileNameOnly = false;
			this.browseForLabelXml.CheckFileExists = false;
			this.browseForLabelXml.FileName = "";
			this.browseForLabelXml.FilesFilter = "Label Template files (*.xml)|*.xml";
			this.browseForLabelXml.FilterIndex = 1;
			this.browseForLabelXml.InitialDirectory = "c:\\";
			this.browseForLabelXml.Label = "Label Xml";
			this.browseForLabelXml.Location = new System.Drawing.Point( 16, 24 );
			this.browseForLabelXml.Name = "browseForLabelXml";
			this.browseForLabelXml.Size = new System.Drawing.Size( 272, 38 );
			this.browseForLabelXml.TabIndex = 2;
			this.browseForLabelXml.OnMyTextChanged += new MyControls.BrowseForObject.MyTextChanged( this.browseForLabelXml_OnMyTextChanged );
			// 
			// browseForLabelTemplate
			// 
			this.browseForLabelTemplate.BrowseLabel = "...";
			this.browseForLabelTemplate.DisplayFileNameOnly = false;
			this.browseForLabelTemplate.CheckFileExists = false;
			this.browseForLabelTemplate.FileName = "";
			this.browseForLabelTemplate.FilesFilter = "All files (*.*)|*.*";
			this.browseForLabelTemplate.FilterIndex = 1;
			this.browseForLabelTemplate.InitialDirectory = "c:\\";
			this.browseForLabelTemplate.Label = "Label";
			this.browseForLabelTemplate.Location = new System.Drawing.Point( 328, 24 );
			this.browseForLabelTemplate.Name = "browseForLabelTemplate";
			this.browseForLabelTemplate.Size = new System.Drawing.Size( 272, 38 );
			this.browseForLabelTemplate.TabIndex = 0;
			// 
			// grpSaveAs
			// 
			this.grpSaveAs.Controls.Add( this.edtSaveAs );
			this.grpSaveAs.Controls.Add( this.stSaveAs );
			this.grpSaveAs.Location = new System.Drawing.Point( 16, 464 );
			this.grpSaveAs.Name = "grpSaveAs";
			this.grpSaveAs.Size = new System.Drawing.Size( 616, 79 );
			this.grpSaveAs.TabIndex = 2;
			this.grpSaveAs.TabStop = false;
			this.grpSaveAs.Text = "Result";
			// 
			// edtSaveAs
			// 
			this.edtSaveAs.Location = new System.Drawing.Point( 16, 40 );
			this.edtSaveAs.Name = "edtSaveAs";
			this.edtSaveAs.Size = new System.Drawing.Size( 584, 20 );
			this.edtSaveAs.TabIndex = 1;
			// 
			// stSaveAs
			// 
			this.stSaveAs.Location = new System.Drawing.Point( 16, 24 );
			this.stSaveAs.Name = "stSaveAs";
			this.stSaveAs.Size = new System.Drawing.Size( 100, 16 );
			this.stSaveAs.TabIndex = 0;
			this.stSaveAs.Text = "Save As";
			// 
			// theTabs
			// 
			this.theTabs.Controls.Add( this.tabCreate );
			this.theTabs.Controls.Add( this.tabPrint );
			this.theTabs.Location = new System.Drawing.Point( 0, 0 );
			this.theTabs.Name = "theTabs";
			this.theTabs.SelectedIndex = 0;
			this.theTabs.Size = new System.Drawing.Size( 656, 593 );
			this.theTabs.TabIndex = 3;
			// 
			// tabCreate
			// 
			this.tabCreate.Controls.Add( this.theFilesListView );
			this.tabCreate.Controls.Add( this.grpTemplates );
			this.tabCreate.Controls.Add( this.grpSaveAs );
			this.tabCreate.Controls.Add( this.btnMakeLabel );
			this.tabCreate.Location = new System.Drawing.Point( 4, 22 );
			this.tabCreate.Name = "tabCreate";
			this.tabCreate.Size = new System.Drawing.Size( 648, 567 );
			this.tabCreate.TabIndex = 0;
			this.tabCreate.Text = "Create";
			// 
			// tabPrint
			// 
			this.tabPrint.Controls.Add( this.btnPrint );
			this.tabPrint.Controls.Add( this.grpPageFile );
			this.tabPrint.Controls.Add( this.grpLabelFiles );
			this.tabPrint.Location = new System.Drawing.Point( 4, 22 );
			this.tabPrint.Name = "tabPrint";
			this.tabPrint.Size = new System.Drawing.Size( 648, 567 );
			this.tabPrint.TabIndex = 1;
			this.tabPrint.Text = "Print";
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point( 287, 507 );
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size( 75, 23 );
			this.btnPrint.TabIndex = 5;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler( this.btnPrint_Click );
			// 
			// grpPageFile
			// 
			this.grpPageFile.Controls.Add( this.edtLabelsPageSaveAs );
			this.grpPageFile.Controls.Add( this.stLabelsPageSaveAs );
			this.grpPageFile.Controls.Add( this.grpCenters );
			this.grpPageFile.Controls.Add( this.browseForPageTemplate );
			this.grpPageFile.Location = new System.Drawing.Point( 16, 280 );
			this.grpPageFile.Name = "grpPageFile";
			this.grpPageFile.Size = new System.Drawing.Size( 616, 191 );
			this.grpPageFile.TabIndex = 4;
			this.grpPageFile.TabStop = false;
			this.grpPageFile.Text = "Labels Page";
			// 
			// edtLabelsPageSaveAs
			// 
			this.edtLabelsPageSaveAs.Location = new System.Drawing.Point( 16, 96 );
			this.edtLabelsPageSaveAs.Name = "edtLabelsPageSaveAs";
			this.edtLabelsPageSaveAs.Size = new System.Drawing.Size( 272, 20 );
			this.edtLabelsPageSaveAs.TabIndex = 5;
			// 
			// stLabelsPageSaveAs
			// 
			this.stLabelsPageSaveAs.Location = new System.Drawing.Point( 16, 80 );
			this.stLabelsPageSaveAs.Name = "stLabelsPageSaveAs";
			this.stLabelsPageSaveAs.Size = new System.Drawing.Size( 77, 16 );
			this.stLabelsPageSaveAs.TabIndex = 4;
			this.stLabelsPageSaveAs.Text = "Save As";
			// 
			// grpCenters
			// 
			this.grpCenters.Controls.Add( this.xyCoordinates2 );
			this.grpCenters.Controls.Add( this.xyCoordinates1 );
			this.grpCenters.Location = new System.Drawing.Point( 323, 24 );
			this.grpCenters.Name = "grpCenters";
			this.grpCenters.Size = new System.Drawing.Size( 272, 143 );
			this.grpCenters.TabIndex = 3;
			this.grpCenters.TabStop = false;
			this.grpCenters.Text = "Label Centers";
			// 
			// xyCoordinates2
			// 
			this.xyCoordinates2.Label = "Label 1";
			this.xyCoordinates2.Location = new System.Drawing.Point( 32, 74 );
			this.xyCoordinates2.Name = "xyCoordinates2";
			this.xyCoordinates2.Size = new System.Drawing.Size( 194, 42 );
			this.xyCoordinates2.TabIndex = 1;
			// 
			// xyCoordinates1
			// 
			this.xyCoordinates1.Label = "Label 1";
			this.xyCoordinates1.Location = new System.Drawing.Point( 32, 23 );
			this.xyCoordinates1.Name = "xyCoordinates1";
			this.xyCoordinates1.Size = new System.Drawing.Size( 194, 42 );
			this.xyCoordinates1.TabIndex = 0;
			// 
			// browseForPageTemplate
			// 
			this.browseForPageTemplate.BrowseLabel = "...";
			this.browseForPageTemplate.DisplayFileNameOnly = false;
			this.browseForPageTemplate.CheckFileExists = false;
			this.browseForPageTemplate.FileName = "";
			this.browseForPageTemplate.FilesFilter = "All files (*.*)|*.*";
			this.browseForPageTemplate.FilterIndex = 1;
			this.browseForPageTemplate.InitialDirectory = "c:\\";
			this.browseForPageTemplate.Label = "Page";
			this.browseForPageTemplate.Location = new System.Drawing.Point( 16, 24 );
			this.browseForPageTemplate.Name = "browseForPageTemplate";
			this.browseForPageTemplate.Size = new System.Drawing.Size( 272, 38 );
			this.browseForPageTemplate.TabIndex = 2;
			this.browseForPageTemplate.OnMyTextChanged += new MyControls.BrowseForObject.MyTextChanged( this.browseForPageTemplate_OnMyTextChanged );
			// 
			// grpLabelFiles
			// 
			this.grpLabelFiles.Controls.Add( this.pbLabelFile2 );
			this.grpLabelFiles.Controls.Add( this.pbLabelFile1 );
			this.grpLabelFiles.Controls.Add( this.browseForLabelFile2 );
			this.grpLabelFiles.Controls.Add( this.browseForLabelFile1 );
			this.grpLabelFiles.Location = new System.Drawing.Point( 16, 16 );
			this.grpLabelFiles.Name = "grpLabelFiles";
			this.grpLabelFiles.Size = new System.Drawing.Size( 616, 250 );
			this.grpLabelFiles.TabIndex = 3;
			this.grpLabelFiles.TabStop = false;
			this.grpLabelFiles.Text = "Label Files";
			// 
			// pbLabelFile2
			// 
			this.pbLabelFile2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbLabelFile2.Location = new System.Drawing.Point( 385, 80 );
			this.pbLabelFile2.Name = "pbLabelFile2";
			this.pbLabelFile2.Size = new System.Drawing.Size( 150, 150 );
			this.pbLabelFile2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbLabelFile2.TabIndex = 6;
			this.pbLabelFile2.TabStop = false;
			// 
			// pbLabelFile1
			// 
			this.pbLabelFile1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbLabelFile1.Location = new System.Drawing.Point( 77, 80 );
			this.pbLabelFile1.Name = "pbLabelFile1";
			this.pbLabelFile1.Size = new System.Drawing.Size( 150, 150 );
			this.pbLabelFile1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbLabelFile1.TabIndex = 5;
			this.pbLabelFile1.TabStop = false;
			// 
			// browseForLabelFile2
			// 
			this.browseForLabelFile2.BrowseLabel = "...";
			this.browseForLabelFile2.DisplayFileNameOnly = false;
			this.browseForLabelFile2.CheckFileExists = false;
			this.browseForLabelFile2.FileName = "";
			this.browseForLabelFile2.FilesFilter = "Label files (*.tif;*.psd)|*.tif;*.psd";
			this.browseForLabelFile2.FilterIndex = 1;
			this.browseForLabelFile2.InitialDirectory = "c:\\";
			this.browseForLabelFile2.Label = "Label File 2";
			this.browseForLabelFile2.Location = new System.Drawing.Point( 324, 24 );
			this.browseForLabelFile2.Name = "browseForLabelFile2";
			this.browseForLabelFile2.Size = new System.Drawing.Size( 272, 38 );
			this.browseForLabelFile2.TabIndex = 4;
			this.browseForLabelFile2.OnMyTextChanged += new MyControls.BrowseForObject.MyTextChanged( this.browseForLabelFile2_OnMyTextChanged );
			// 
			// browseForLabelFile1
			// 
			this.browseForLabelFile1.BrowseLabel = "...";
			this.browseForLabelFile1.DisplayFileNameOnly = false;
			this.browseForLabelFile1.CheckFileExists = false;
			this.browseForLabelFile1.FileName = "";
			this.browseForLabelFile1.FilesFilter = "Label files (*.tif;*.psd)|*.tif;*.psd";
			this.browseForLabelFile1.FilterIndex = 1;
			this.browseForLabelFile1.InitialDirectory = "c:\\";
			this.browseForLabelFile1.Label = "Label File 1";
			this.browseForLabelFile1.Location = new System.Drawing.Point( 16, 24 );
			this.browseForLabelFile1.Name = "browseForLabelFile1";
			this.browseForLabelFile1.Size = new System.Drawing.Size( 272, 38 );
			this.browseForLabelFile1.TabIndex = 3;
			this.browseForLabelFile1.OnMyTextChanged += new MyControls.BrowseForObject.MyTextChanged( this.browseForLabelFile1_OnMyTextChanged );
			// 
			// LabelMaker
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 656, 595 );
			this.Controls.Add( this.theTabs );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "LabelMaker";
			this.Text = "CD Label Maker";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.LabelMaker_FormClosing );
			this.Load += new System.EventHandler( this.LabelMaker_Load );
			this.grpTemplates.ResumeLayout( false );
			this.grpLabelText.ResumeLayout( false );
			this.grpLabelText.PerformLayout();
			this.grpSaveAs.ResumeLayout( false );
			this.grpSaveAs.PerformLayout();
			this.theTabs.ResumeLayout( false );
			this.tabCreate.ResumeLayout( false );
			this.tabPrint.ResumeLayout( false );
			this.grpPageFile.ResumeLayout( false );
			this.grpPageFile.PerformLayout();
			this.grpCenters.ResumeLayout( false );
			this.grpLabelFiles.ResumeLayout( false );
			( (System.ComponentModel.ISupportInitialize) ( this.pbLabelFile2 ) ).EndInit();
			( (System.ComponentModel.ISupportInitialize) ( this.pbLabelFile1 ) ).EndInit();
			this.ResumeLayout( false );

		}
		#endregion

		private void EnableDisable()
		{
			grpTemplates.Enabled			=  !processing;
			theFilesListView.Enabled	=  !processing;
		}

		private void ResetForm()
		{
			// Enable/Disable pertinent controls.
			EnableDisable();

			// Flip the button text.
			btnMakeLabel.Text	=  processing ?  "Cancel" : "Make Label";
		}

		private void btnMakeLabel_Click( object sender, System.EventArgs e )
		{
			if ( !processing )
			{
				// Create the thread.
				processThread	=  new Thread( new ThreadStart( MakeLabel ) );

				// We're processing!
				processing	=  true;

				ResetForm();

				// We can start the thread now.
				processThread.Start();
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

		private void LabelMaker_Load( object sender, System.EventArgs e )
		{
			// Adjust the widths.
			edtLabelText.Enabled	=  false;

			ProcessBrowseForLabelXmlChanged();
		}

		private void LabelMaker_FormClosing( object sender, FormClosingEventArgs e )
		{
		}

		private void browseForLabelXml_OnMyTextChanged( object sender, System.EventArgs e )
		{
			ProcessBrowseForLabelXmlChanged();
		}

		private void ProcessBrowseForLabelXmlChanged()
		{
			// See if we have a valid xml file.
			try
			{
				// Save current values.
				// Make sure we get the latest text changes 1st.
				if ( currentLT != null )
					currentLT.Text	=  edtLabelText.Text;

				// Now copy the current values into the saved array.
				LabelTextArray	oldAr	=  new LabelTextArray();
					
				if ( labelTextAr != null )
				{
					for ( int i= 0;  i< labelTextAr.Count;  i++ )
					{
						LabelText	lt	=  new LabelText();

						lt.Name	=  labelTextAr[ i ].Name;
						lt.Text	=  labelTextAr[ i ].Text;

						oldAr.Add( lt );
					}
				}

				// Process the xml.
				GetLabelXml();

				if ( labelTextAr != null )
				{
					// If the names are the same, fill in the current text values.
					for ( int i= 0;  i< Math.Min( labelTextAr.Count, oldAr.Count );  i++ )
					{
						LabelText	lt	=  labelTextAr[ i ];

						if ( lt.Name == oldAr[ i ].Name )
							lt.Text	=  oldAr[ i ].Text;
					}
				}

				// Clear & refill the combox.
				cbTextLines.DataSource		=  null;
				cbTextLines.DataSource		=  labelTextAr;
				cbTextLines.DisplayMember	=  "Name";
				cbTextLines.ValueMember		=  "Text";

				// And select the first one.
				if ( cbTextLines.Items.Count > 0 )
				{
					edtLabelText.Enabled			=  true;
					cbTextLines.SelectedIndex	=  0;
				}
				else
					edtLabelText.Enabled	=  false;
			}

			catch( Exception )
			{
			}
		}

		private void cbTextLines_SelectedIndexChanged( object sender, System.EventArgs e )
		{
			ProcessTextLinesSelectionChanged();
		}

		private void ProcessTextLinesSelectionChanged()
		{

			// Get whatever's there.
			if ( currentLT != null )
			{
				currentLT.Text	=  edtLabelText.Text;
			}

			// Show the current values.
			if ( cbTextLines.SelectedIndex != -1 )
			{
				currentLT	=  (LabelText) cbTextLines.SelectedItem;

				stTextLine.Text	=  currentLT.Name;
				edtLabelText.Text	=  currentLT.Text;
			}
		}

		private void LoadImageFile( BrowseForFile bff, PictureBox pb )
		{
			try
			{
				// Load the file.
				string	filename	=  bff.FileName;

				if ( filename != null && filename != "" )
				{
					Bitmap	image		=  new Bitmap( filename );

					pb.Image	=  image;
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Load Image File" );
			}
		}

		private void browseForLabelFile1_OnMyTextChanged( object sender, System.EventArgs e )
		{
			LoadImageFile( browseForLabelFile1, pbLabelFile1 );
		}

		private void browseForLabelFile2_OnMyTextChanged( object sender, System.EventArgs e )
		{
			LoadImageFile( browseForLabelFile2, pbLabelFile2 );
		}

		private void browseForPageTemplate_OnMyTextChanged( object sender, System.EventArgs e )
		{
		}

		private void btnPrint_Click( object sender, System.EventArgs e )
		{
			// Print baby, print!
			CreateLabelsPrintPage();
		}

		void CreateLabelsPrintPage()
		{
			PsUnits	originalRulerUnits	=  PsUnits.psPixels;

			try
			{
				// Might take a while.
				Cursor.Current	=  Cursors.WaitCursor;

				// Must be something to save as.
				string	saveAsFilename	=  this.edtLabelsPageSaveAs.Text;

				if ( saveAsFilename == string.Empty )
				{
					throw new Exception( "Save As cannot be blank!" );
				}

				// Start up Photoshop.
				psApp	=  new Photoshop.Application();
				ps		=  new Photoshopper.Photoshopper( psApp );

				// Save preferences.
				originalRulerUnits	=  psApp.Preferences.RulerUnits;
			
				// Set ruler units to pixels
				psApp.Preferences.RulerUnits	=  PsUnits.psPixels;
			
				// Don't display dialogs
				psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

				// Open the Label template.
				Document	labelsDoc	=  ps.OpenDoc( browseForPageTemplate.FileName );

				// Get the resolution.
				double	resolution	=  labelsDoc.Resolution;

				// Get the filenames & locations.
				string[]	labels	=  new string[ 2 ];
				DPoint[]	points	=  new DPoint[ 2 ];

				labels[ 0 ]	=  browseForLabelFile1.FileName;
				labels[ 1 ]	=  browseForLabelFile2.FileName;

				points[ 0 ]	=  xyCoordinates1.Point.ConvertToPixels( resolution );
				points[ 1 ]	=  xyCoordinates2.Point.ConvertToPixels( resolution );

				for ( int i= 0;  i< 2;  i++ )
				{
					// Open the image.
					Document	imageDoc	=  ps.OpenDoc( labels[ i ] );

					// Select & Copy.
					imageDoc.Selection.SelectAll();
					imageDoc.Selection.Copy( imageDoc.Layers.Count > 1 ?  true : false );

					// Activate the template.
					ps.theApp.ActiveDocument	=  labelsDoc;

					// Define where to place the image (must be in pixels).
					double	left		=	points[ i ].X - ( imageDoc.Width / 2 );
					double	top		=	points[ i ].Y - ( imageDoc.Height / 2 );
					double	right		=  left + imageDoc.Width;
					double	bottom	=  top + imageDoc.Height;

					double[]	corners	=  new double[ 4 ] { left, top, right, bottom };
					Bounds	region	=  new Bounds( corners );

					// Select and paste.
					ps.SelectRectArea( region, PsSelectionType.psReplaceSelection, 0.0 );
					labelsDoc.Paste( true );

					// Close the file.
					imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
				}

				// Flatten.
				labelsDoc.Flatten();

				// Save the label file.
				ps.SaveAsTIFF( labelsDoc, "", saveAsFilename );

				// Close the file.
				//***labelsDoc.Close( PsSaveOptions.psDoNotSaveChanges );
			}

			catch( ThreadAbortException )
			{
				// We were aborted!
			}

			catch( Exception ex )
			{
				// Uh oh!
				MessageBox.Show( ex.Message, "Making Label" );
			}

			finally
			{
				// Restore preferences.
				if ( psApp != null )
					psApp.Preferences.RulerUnits	=  originalRulerUnits;

				// Restore the cursor.
				Cursor.Current	=  Cursors.Default;

				processing	=  false;

				// Let 'em know we're done.
				ResetForm();
			}
		}
	}

	public class LabelText
	{
		string	name;
		string	text;
		int		line;
		double	fontSize;
		string	fontName;
		string	fontStyle;
		string	antiAlias;
		string	alignment;
		double	x;
		double	y;

		public LabelText()
		{
		}

		public int Line
		{
			get { return ( line ); }
		}

		public double X
		{
			get { return ( x ); }
		}

		public double Y
		{
			get { return ( y ); }
		}

		public double FontSize
		{
			get { return ( fontSize ); }
		}

		public string Name
		{
			get { return ( name ); }
			set { name	=  value; }
		}

		public string Text
		{
			get { return ( text ); }
			set { text	=  value; }
		}

		public string FontName
		{
			get { return ( fontName ); }
		}

		public string FontStyle
		{
			get { return ( fontStyle ); }
		}

		public Photoshop.PsAntiAlias AntiAlias
		{
			get
			{
				Photoshop.PsAntiAlias	aa;

				switch( alignment )
				{
					case "Crisp":
						aa	=  PsAntiAlias.psCrisp;
						break;
					case "None":
						aa	=  PsAntiAlias.psNoAntialias;
						break;
					case "Sharp":
						aa	=  PsAntiAlias.psSharp;
						break;
					case "Smooth":
						aa	=  PsAntiAlias.psSmooth;
						break;
					case "Strong":
						aa	=  PsAntiAlias.psStrong;
						break;
					default:
						aa	=  PsAntiAlias.psCrisp;
						break;
				}

				return ( aa );
			}
		}

		public Photoshop.PsJustification Alignment
		{
			get
			{
				Photoshop.PsJustification	align;

				switch( alignment )
				{
					case "Center":
						align	=  PsJustification.psCenter;
						break;
					case "CenterJustified":
						align	=  PsJustification.psCenterJustified;
						break;
					case "FullyJustified":
						align	=  PsJustification.psFullyJustified;
						break;
					case "Left":
						align	=  PsJustification.psLeft;
						break;
					case "LeftJustified":
						align	=  PsJustification.psLeftJustified;
						break;
					case "Right":
						align	=  PsJustification.psRight;
						break;
					case "RightJustified":
						align	=  PsJustification.psRightJustified;
						break;
					default:
						align	=  PsJustification.psLeft;
						break;
				}

				return ( align );
			}
		}

		public void Load( XmlNode node )
		{
			// Line number.
			line	=  Int32.Parse( node.Attributes[ "Line" ].InnerXml );
			name	=  node.Attributes[ "Name" ].InnerXml;

			// Font.
			XmlNode	font	=  node.SelectSingleNode( "descendant::Font" );

			fontSize		=  Double.Parse( font.Attributes[ "Size" ].InnerXml );
			fontName		=  font.InnerXml;
			fontStyle	=  font.Attributes[ "Style" ].InnerXml;
			antiAlias	=  font.Attributes[ "AntiALias" ].InnerXml;

			// Alignment.
			XmlNode	align	=  node.SelectSingleNode( "descendant::Alignment" );

			alignment	=  align.InnerXml;

			// Position
			XmlNode	position	=  node.SelectSingleNode( "descendant::Position" );

			x	=  Double.Parse( position.Attributes[ "X" ].InnerXml );
			y	=  Double.Parse( position.Attributes[ "Y" ].InnerXml );
		}
	}

	public class LabelTextArray : ArrayList
	{
		public new LabelText this[ int index ]
		{
			get
			{
				return ( (LabelText) base[ index ] );
			}
		}
	}

	public struct LabelData
	{
		int	left;
		int	top;
		int	right;
		int	bottom;

		public int X
		{
			get { return ( left ); }
		}

		public int Y
		{
			get { return ( top ); }
		}

		public int Right
		{
			get { return ( right ); }
		}

		public int Bottom
		{
			get { return ( bottom ); }
		}

		public int Width
		{
			get { return ( right - left ); }
		}

		public int Height
		{
			get { return ( bottom - top ); }
		}

		public void Load( XmlNode node )
		{
			left		=  Int32.Parse( node.Attributes[ "Left" ].InnerXml );
			top		=  Int32.Parse( node.Attributes[ "Top" ].InnerXml );
			right		=  Int32.Parse( node.Attributes[ "Right" ].InnerXml );
			bottom	=  Int32.Parse( node.Attributes[ "Btm" ].InnerXml );
		}
	}

	public class LabelDataArray : ArrayList
	{
		public new LabelData this[ int index ]
		{
			get
			{
				return ( (LabelData) base[ index ] );
			}
		}
	}
}

