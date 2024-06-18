using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;

namespace PhotoshopUtilities
{
	public class ContactSheetMaker : MyWindowsForm
	{
		#region Data members.

		public delegate void ResetFormDelegate();
		public ResetFormDelegate resetFormDelegate;

		public delegate void RefreshDelegate();
		public RefreshDelegate refreshDelegate;

		public delegate void MyEventHandlerDelegate( Object sender, EventArgs e );
		public MyEventHandlerDelegate onProcessingComplete;

		private Thread		processThread;
		private bool		processing;

		private MyControls.MyGroupBox grpSourceImages;
		private MyControls.BrowseForDirectory bfdSourceImages;
		private MyControls.FilesListView flvImageFiles;
		private System.Windows.Forms.RadioButton rbFiles;
		private System.Windows.Forms.RadioButton rbPattern;
		private MyControls.LabelAndText ltStartRoll;
		private MyControls.LabelAndText ltEndRoll;
		private MyControls.MyGroupBox grpDocument;
		private System.Windows.Forms.Label stX;
		private MyControls.LabelAndText ltHeight;
		private MyControls.LabelAndText ltWidth;
		private System.Windows.Forms.Label stDPI;
		private System.Windows.Forms.CheckBox chkAutoResolution;
		private MyControls.LabelAndText ltResolution;
		private System.Windows.Forms.CheckBox chkFlattenAllLayers;
		private MyControls.MyGroupBox grpThumbnails;
		private System.Windows.Forms.ComboBox cbPlace;
		private System.Windows.Forms.Label stPlace;
		private MyControls.LabelAndText ltColumns;
		private System.Windows.Forms.Label stXthumbs;
		private MyControls.LabelAndText ltRows;
		private System.Windows.Forms.CheckBox chkUseAutoSpacing;
		private System.Windows.Forms.CheckBox chkRotateForBestFit;
		private System.Windows.Forms.Button btnProcess;
		private LabelAndText ltHorizontalSpacing;
		private LabelAndText ltVerticalSpacing;
		private System.Windows.Forms.Label stInches;
		private System.Windows.Forms.Label stInchesHorizontal;
		private System.Windows.Forms.Label stInchesVertical;
		private LabelAndText ltSaveAs;
		private CheckBox chkAutoSaveAs;
		private BrowseForDirectory bfdSaveAs;
		private CheckBox chkAutoSaveAsDir;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#endregion

		public ContactSheetMaker()
		{
			InitializeComponent();

			processing	=  false;

			resetFormDelegate		=  new ResetFormDelegate( ResetForm );
			refreshDelegate		=  new RefreshDelegate( RefreshMe );
			onProcessingComplete	=  new MyEventHandlerDelegate( Form_ProcessingComplete );
		}

		private void ResetForm()
		{
			// Enable/Disable pertinent controls.
			EnableDisable();

			// Flip the button text.
			btnProcess.Text = processing ? "Cancel" : "Process";
		}

		void RefreshMe()
		{
			Refresh();
		}

		private void EnableDisable()
		{
			grpDocument.Enabled		=  !processing;
			grpSourceImages.Enabled	=  !processing;
			grpThumbnails.Enabled	=  !processing;
		}

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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grpSourceImages = new MyControls.MyGroupBox();
			this.ltEndRoll = new MyControls.LabelAndText();
			this.ltStartRoll = new MyControls.LabelAndText();
			this.rbFiles = new System.Windows.Forms.RadioButton();
			this.rbPattern = new System.Windows.Forms.RadioButton();
			this.bfdSourceImages = new MyControls.BrowseForDirectory();
			this.flvImageFiles = new MyControls.FilesListView();
			this.grpDocument = new MyControls.MyGroupBox();
			this.chkAutoSaveAsDir = new System.Windows.Forms.CheckBox();
			this.bfdSaveAs = new MyControls.BrowseForDirectory();
			this.chkAutoSaveAs = new System.Windows.Forms.CheckBox();
			this.ltSaveAs = new MyControls.LabelAndText();
			this.stInches = new System.Windows.Forms.Label();
			this.chkFlattenAllLayers = new System.Windows.Forms.CheckBox();
			this.stDPI = new System.Windows.Forms.Label();
			this.chkAutoResolution = new System.Windows.Forms.CheckBox();
			this.ltResolution = new MyControls.LabelAndText();
			this.stX = new System.Windows.Forms.Label();
			this.ltHeight = new MyControls.LabelAndText();
			this.ltWidth = new MyControls.LabelAndText();
			this.grpThumbnails = new MyControls.MyGroupBox();
			this.stInchesHorizontal = new System.Windows.Forms.Label();
			this.stInchesVertical = new System.Windows.Forms.Label();
			this.ltHorizontalSpacing = new MyControls.LabelAndText();
			this.ltVerticalSpacing = new MyControls.LabelAndText();
			this.chkUseAutoSpacing = new System.Windows.Forms.CheckBox();
			this.chkRotateForBestFit = new System.Windows.Forms.CheckBox();
			this.stXthumbs = new System.Windows.Forms.Label();
			this.ltRows = new MyControls.LabelAndText();
			this.ltColumns = new MyControls.LabelAndText();
			this.cbPlace = new System.Windows.Forms.ComboBox();
			this.stPlace = new System.Windows.Forms.Label();
			this.btnProcess = new System.Windows.Forms.Button();
			this.grpSourceImages.SuspendLayout();
			this.grpDocument.SuspendLayout();
			this.grpThumbnails.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpSourceImages
			// 
			this.grpSourceImages.Controls.Add( this.ltEndRoll );
			this.grpSourceImages.Controls.Add( this.ltStartRoll );
			this.grpSourceImages.Controls.Add( this.rbFiles );
			this.grpSourceImages.Controls.Add( this.rbPattern );
			this.grpSourceImages.Controls.Add( this.bfdSourceImages );
			this.grpSourceImages.Controls.Add( this.flvImageFiles );
			this.grpSourceImages.Location = new System.Drawing.Point( 16, 16 );
			this.grpSourceImages.Name = "grpSourceImages";
			this.grpSourceImages.Size = new System.Drawing.Size( 536, 350 );
			this.grpSourceImages.TabIndex = 0;
			this.grpSourceImages.TabStop = false;
			this.grpSourceImages.Text = "Source Images";
			// 
			// ltEndRoll
			// 
			this.ltEndRoll.Label = "&End Roll #";
			this.ltEndRoll.Location = new System.Drawing.Point( 222, 294 );
			this.ltEndRoll.Name = "ltEndRoll";
			this.ltEndRoll.Size = new System.Drawing.Size( 100, 37 );
			this.ltEndRoll.TabIndex = 5;
			// 
			// ltStartRoll
			// 
			this.ltStartRoll.Label = "&Start Roll #";
			this.ltStartRoll.Location = new System.Drawing.Point( 95, 293 );
			this.ltStartRoll.Name = "ltStartRoll";
			this.ltStartRoll.Size = new System.Drawing.Size( 100, 37 );
			this.ltStartRoll.TabIndex = 4;
			// 
			// rbFiles
			// 
			this.rbFiles.AutoSize = true;
			this.rbFiles.Location = new System.Drawing.Point( 20, 112 );
			this.rbFiles.Name = "rbFiles";
			this.rbFiles.Size = new System.Drawing.Size( 46, 17 );
			this.rbFiles.TabIndex = 3;
			this.rbFiles.TabStop = true;
			this.rbFiles.Text = "F&iles";
			this.rbFiles.UseVisualStyleBackColor = true;
			this.rbFiles.CheckedChanged += new System.EventHandler( this.rbFilesPattern_CheckedChanged );
			// 
			// rbPattern
			// 
			this.rbPattern.AutoSize = true;
			this.rbPattern.Location = new System.Drawing.Point( 20, 258 );
			this.rbPattern.Name = "rbPattern";
			this.rbPattern.Size = new System.Drawing.Size( 59, 17 );
			this.rbPattern.TabIndex = 2;
			this.rbPattern.TabStop = true;
			this.rbPattern.Text = "&Pattern";
			this.rbPattern.UseVisualStyleBackColor = true;
			this.rbPattern.CheckedChanged += new System.EventHandler( this.rbFilesPattern_CheckedChanged );
			// 
			// bfdSourceImages
			// 
			this.bfdSourceImages.BrowseLabel = "...";
			this.bfdSourceImages.Directory = "";
			this.bfdSourceImages.Label = "Source Director&y";
			this.bfdSourceImages.Location = new System.Drawing.Point( 95, 240 );
			this.bfdSourceImages.Name = "bfdSourceImages";
			this.bfdSourceImages.Size = new System.Drawing.Size( 272, 38 );
			this.bfdSourceImages.TabIndex = 1;
			// 
			// flvImageFiles
			// 
			this.flvImageFiles.Location = new System.Drawing.Point( 95, 20 );
			this.flvImageFiles.Name = "flvImageFiles";
			this.flvImageFiles.Size = new System.Drawing.Size( 420, 200 );
			this.flvImageFiles.TabIndex = 0;
			// 
			// grpDocument
			// 
			this.grpDocument.Controls.Add( this.chkAutoSaveAsDir );
			this.grpDocument.Controls.Add( this.bfdSaveAs );
			this.grpDocument.Controls.Add( this.chkAutoSaveAs );
			this.grpDocument.Controls.Add( this.ltSaveAs );
			this.grpDocument.Controls.Add( this.stInches );
			this.grpDocument.Controls.Add( this.chkFlattenAllLayers );
			this.grpDocument.Controls.Add( this.stDPI );
			this.grpDocument.Controls.Add( this.chkAutoResolution );
			this.grpDocument.Controls.Add( this.ltResolution );
			this.grpDocument.Controls.Add( this.stX );
			this.grpDocument.Controls.Add( this.ltHeight );
			this.grpDocument.Controls.Add( this.ltWidth );
			this.grpDocument.Location = new System.Drawing.Point( 16, 382 );
			this.grpDocument.Name = "grpDocument";
			this.grpDocument.Size = new System.Drawing.Size( 188, 248 );
			this.grpDocument.TabIndex = 1;
			this.grpDocument.TabStop = false;
			this.grpDocument.Text = "Document";
			// 
			// chkAutoSaveAsDir
			// 
			this.chkAutoSaveAsDir.AutoSize = true;
			this.chkAutoSaveAsDir.Location = new System.Drawing.Point( 124, 195 );
			this.chkAutoSaveAsDir.Name = "chkAutoSaveAsDir";
			this.chkAutoSaveAsDir.Size = new System.Drawing.Size( 48, 17 );
			this.chkAutoSaveAsDir.TabIndex = 13;
			this.chkAutoSaveAsDir.Text = "Auto";
			this.chkAutoSaveAsDir.UseVisualStyleBackColor = true;
			this.chkAutoSaveAsDir.CheckedChanged += new System.EventHandler( this.chkAutoSaveAsDir_CheckedChanged );
			// 
			// bfdSaveAs
			// 
			this.bfdSaveAs.BrowseLabel = "...";
			this.bfdSaveAs.Directory = "";
			this.bfdSaveAs.Label = "Save As &Directory";
			this.bfdSaveAs.Location = new System.Drawing.Point( 16, 198 );
			this.bfdSaveAs.Name = "bfdSaveAs";
			this.bfdSaveAs.Size = new System.Drawing.Size( 156, 38 );
			this.bfdSaveAs.TabIndex = 4;
			// 
			// chkAutoSaveAs
			// 
			this.chkAutoSaveAs.AutoSize = true;
			this.chkAutoSaveAs.Location = new System.Drawing.Point( 124, 150 );
			this.chkAutoSaveAs.Name = "chkAutoSaveAs";
			this.chkAutoSaveAs.Size = new System.Drawing.Size( 48, 17 );
			this.chkAutoSaveAs.TabIndex = 12;
			this.chkAutoSaveAs.Text = "Auto";
			this.chkAutoSaveAs.UseVisualStyleBackColor = true;
			this.chkAutoSaveAs.CheckedChanged += new System.EventHandler( this.chkSaveAsAuto_CheckedChanged );
			// 
			// ltSaveAs
			// 
			this.ltSaveAs.Label = "Sa&ve As";
			this.ltSaveAs.Location = new System.Drawing.Point( 16, 153 );
			this.ltSaveAs.Name = "ltSaveAs";
			this.ltSaveAs.Size = new System.Drawing.Size( 156, 37 );
			this.ltSaveAs.TabIndex = 11;
			// 
			// stInches
			// 
			this.stInches.AutoSize = true;
			this.stInches.Location = new System.Drawing.Point( 158, 41 );
			this.stInches.Name = "stInches";
			this.stInches.Size = new System.Drawing.Size( 15, 13 );
			this.stInches.TabIndex = 10;
			this.stInches.Text = "in";
			// 
			// chkFlattenAllLayers
			// 
			this.chkFlattenAllLayers.AutoSize = true;
			this.chkFlattenAllLayers.Location = new System.Drawing.Point( 37, 120 );
			this.chkFlattenAllLayers.Name = "chkFlattenAllLayers";
			this.chkFlattenAllLayers.Size = new System.Drawing.Size( 106, 17 );
			this.chkFlattenAllLayers.TabIndex = 6;
			this.chkFlattenAllLayers.Text = "&Flatten All Layers";
			this.chkFlattenAllLayers.UseVisualStyleBackColor = true;
			// 
			// stDPI
			// 
			this.stDPI.AutoSize = true;
			this.stDPI.Location = new System.Drawing.Point( 81, 90 );
			this.stDPI.Name = "stDPI";
			this.stDPI.Size = new System.Drawing.Size( 21, 13 );
			this.stDPI.TabIndex = 5;
			this.stDPI.Text = "dpi";
			// 
			// chkAutoResolution
			// 
			this.chkAutoResolution.AutoSize = true;
			this.chkAutoResolution.Location = new System.Drawing.Point( 118, 89 );
			this.chkAutoResolution.Name = "chkAutoResolution";
			this.chkAutoResolution.Size = new System.Drawing.Size( 48, 17 );
			this.chkAutoResolution.TabIndex = 4;
			this.chkAutoResolution.Text = "&Auto";
			this.chkAutoResolution.UseVisualStyleBackColor = true;
			// 
			// ltResolution
			// 
			this.ltResolution.Label = "&Resolution";
			this.ltResolution.Location = new System.Drawing.Point( 16, 70 );
			this.ltResolution.Name = "ltResolution";
			this.ltResolution.Size = new System.Drawing.Size( 60, 37 );
			this.ltResolution.TabIndex = 3;
			// 
			// stX
			// 
			this.stX.AutoSize = true;
			this.stX.Location = new System.Drawing.Point( 80, 43 );
			this.stX.Name = "stX";
			this.stX.Size = new System.Drawing.Size( 12, 13 );
			this.stX.TabIndex = 2;
			this.stX.Text = "x";
			// 
			// ltHeight
			// 
			this.ltHeight.Label = "&Height";
			this.ltHeight.Location = new System.Drawing.Point( 92, 20 );
			this.ltHeight.Name = "ltHeight";
			this.ltHeight.Size = new System.Drawing.Size( 60, 37 );
			this.ltHeight.TabIndex = 1;
			// 
			// ltWidth
			// 
			this.ltWidth.Label = "&Width";
			this.ltWidth.Location = new System.Drawing.Point( 16, 20 );
			this.ltWidth.Name = "ltWidth";
			this.ltWidth.Size = new System.Drawing.Size( 60, 37 );
			this.ltWidth.TabIndex = 0;
			// 
			// grpThumbnails
			// 
			this.grpThumbnails.Controls.Add( this.stInchesHorizontal );
			this.grpThumbnails.Controls.Add( this.stInchesVertical );
			this.grpThumbnails.Controls.Add( this.ltHorizontalSpacing );
			this.grpThumbnails.Controls.Add( this.ltVerticalSpacing );
			this.grpThumbnails.Controls.Add( this.chkUseAutoSpacing );
			this.grpThumbnails.Controls.Add( this.chkRotateForBestFit );
			this.grpThumbnails.Controls.Add( this.stXthumbs );
			this.grpThumbnails.Controls.Add( this.ltRows );
			this.grpThumbnails.Controls.Add( this.ltColumns );
			this.grpThumbnails.Controls.Add( this.cbPlace );
			this.grpThumbnails.Controls.Add( this.stPlace );
			this.grpThumbnails.Location = new System.Drawing.Point( 220, 382 );
			this.grpThumbnails.Name = "grpThumbnails";
			this.grpThumbnails.Size = new System.Drawing.Size( 332, 158 );
			this.grpThumbnails.TabIndex = 2;
			this.grpThumbnails.TabStop = false;
			this.grpThumbnails.Text = "Thumbnails";
			// 
			// stInchesHorizontal
			// 
			this.stInchesHorizontal.AutoSize = true;
			this.stInchesHorizontal.Location = new System.Drawing.Point( 264, 126 );
			this.stInchesHorizontal.Name = "stInchesHorizontal";
			this.stInchesHorizontal.Size = new System.Drawing.Size( 15, 13 );
			this.stInchesHorizontal.TabIndex = 10;
			this.stInchesHorizontal.Text = "in";
			// 
			// stInchesVertical
			// 
			this.stInchesVertical.AutoSize = true;
			this.stInchesVertical.Location = new System.Drawing.Point( 264, 81 );
			this.stInchesVertical.Name = "stInchesVertical";
			this.stInchesVertical.Size = new System.Drawing.Size( 15, 13 );
			this.stInchesVertical.TabIndex = 9;
			this.stInchesVertical.Text = "in";
			// 
			// ltHorizontalSpacing
			// 
			this.ltHorizontalSpacing.Label = "Hori&zontal";
			this.ltHorizontalSpacing.Location = new System.Drawing.Point( 195, 106 );
			this.ltHorizontalSpacing.Name = "ltHorizontalSpacing";
			this.ltHorizontalSpacing.Size = new System.Drawing.Size( 60, 37 );
			this.ltHorizontalSpacing.TabIndex = 8;
			// 
			// ltVerticalSpacing
			// 
			this.ltVerticalSpacing.Label = "Ver&tical";
			this.ltVerticalSpacing.Location = new System.Drawing.Point( 195, 60 );
			this.ltVerticalSpacing.Name = "ltVerticalSpacing";
			this.ltVerticalSpacing.Size = new System.Drawing.Size( 60, 37 );
			this.ltVerticalSpacing.TabIndex = 7;
			// 
			// chkUseAutoSpacing
			// 
			this.chkUseAutoSpacing.AutoSize = true;
			this.chkUseAutoSpacing.Location = new System.Drawing.Point( 182, 31 );
			this.chkUseAutoSpacing.Name = "chkUseAutoSpacing";
			this.chkUseAutoSpacing.Size = new System.Drawing.Size( 112, 17 );
			this.chkUseAutoSpacing.TabIndex = 6;
			this.chkUseAutoSpacing.Text = "&Use Auto Spacing";
			this.chkUseAutoSpacing.UseVisualStyleBackColor = true;
			this.chkUseAutoSpacing.CheckedChanged += new System.EventHandler( this.chkUseAutoSpacing_CheckedChanged );
			// 
			// chkRotateForBestFit
			// 
			this.chkRotateForBestFit.AutoSize = true;
			this.chkRotateForBestFit.Location = new System.Drawing.Point( 24, 120 );
			this.chkRotateForBestFit.Name = "chkRotateForBestFit";
			this.chkRotateForBestFit.Size = new System.Drawing.Size( 114, 17 );
			this.chkRotateForBestFit.TabIndex = 5;
			this.chkRotateForBestFit.Text = "Rotate For &Best Fit";
			this.chkRotateForBestFit.UseVisualStyleBackColor = true;
			// 
			// stXthumbs
			// 
			this.stXthumbs.AutoSize = true;
			this.stXthumbs.Location = new System.Drawing.Point( 71, 92 );
			this.stXthumbs.Name = "stXthumbs";
			this.stXthumbs.Size = new System.Drawing.Size( 12, 13 );
			this.stXthumbs.TabIndex = 4;
			this.stXthumbs.Text = "x";
			// 
			// ltRows
			// 
			this.ltRows.Label = "Ro&ws";
			this.ltRows.Location = new System.Drawing.Point( 87, 70 );
			this.ltRows.Name = "ltRows";
			this.ltRows.Size = new System.Drawing.Size( 50, 37 );
			this.ltRows.TabIndex = 3;
			// 
			// ltColumns
			// 
			this.ltColumns.Label = "Colu&mns";
			this.ltColumns.Location = new System.Drawing.Point( 16, 70 );
			this.ltColumns.Name = "ltColumns";
			this.ltColumns.Size = new System.Drawing.Size( 50, 37 );
			this.ltColumns.TabIndex = 2;
			// 
			// cbPlace
			// 
			this.cbPlace.FormattingEnabled = true;
			this.cbPlace.Items.AddRange( new object[] {
            "Across first",
            "Down first"} );
			this.cbPlace.Location = new System.Drawing.Point( 16, 36 );
			this.cbPlace.Name = "cbPlace";
			this.cbPlace.Size = new System.Drawing.Size( 121, 21 );
			this.cbPlace.TabIndex = 1;
			// 
			// stPlace
			// 
			this.stPlace.AutoSize = true;
			this.stPlace.Location = new System.Drawing.Point( 16, 20 );
			this.stPlace.Name = "stPlace";
			this.stPlace.Size = new System.Drawing.Size( 34, 13 );
			this.stPlace.TabIndex = 0;
			this.stPlace.Text = "&Place";
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point( 339, 579 );
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size( 75, 23 );
			this.btnProcess.TabIndex = 3;
			this.btnProcess.Text = "&Process";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler( this.btnProcess_Click );
			// 
			// ContactSheetMaker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 566, 645 );
			this.Controls.Add( this.btnProcess );
			this.Controls.Add( this.grpThumbnails );
			this.Controls.Add( this.grpDocument );
			this.Controls.Add( this.grpSourceImages );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ContactSheetMaker";
			this.Text = "Contact Sheet Maker";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.ContactSheetMaker_FormClosing );
			this.Load += new System.EventHandler( this.ContactSheetMaker_Load );
			this.grpSourceImages.ResumeLayout( false );
			this.grpSourceImages.PerformLayout();
			this.grpDocument.ResumeLayout( false );
			this.grpDocument.PerformLayout();
			this.grpThumbnails.ResumeLayout( false );
			this.grpThumbnails.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private void ContactSheetMaker_Load( object sender, EventArgs e )
		{
			cbPlace.SelectedIndex	=  0;	// Across by default.
			bfdSourceImages.Width	=  420;

			chkSaveAsAuto_CheckedChanged( this, e );
			chkAutoSaveAsDir_CheckedChanged( this, e );
			chkUseAutoSpacing_CheckedChanged( this, e );
			rbFilesPattern_CheckedChanged( this, e );
		}

		private void chkUseAutoSpacing_CheckedChanged( object sender, EventArgs e )
		{
			ltVerticalSpacing.Enabled		=  !chkUseAutoSpacing.Checked;
			ltHorizontalSpacing.Enabled	=  !chkUseAutoSpacing.Checked;
		}

		private void rbFilesPattern_CheckedChanged( object sender, EventArgs e )
		{
			flvImageFiles.Enabled	=  rbFiles.Checked;

			bfdSourceImages.Enabled =  rbPattern.Checked;
			ltStartRoll.Enabled		=  rbPattern.Checked;
			ltEndRoll.Enabled			=  rbPattern.Checked;
		}

		private void ContactSheetMaker_FormClosing( object sender, System.Windows.Forms.FormClosingEventArgs e )
		{
			// Make sure the thread is killed if we're processing.
			if ( processing )
			{
				processThread.Abort();
				processThread.Join();

				processing	=  false;
			}
		}

		private void chkSaveAsAuto_CheckedChanged( object sender, EventArgs e )
		{
			ltSaveAs.Enabled	=  !chkAutoSaveAs.Checked;
		}

		private void chkAutoSaveAsDir_CheckedChanged( object sender, EventArgs e )
		{
			bfdSaveAs.Enabled	=  !chkAutoSaveAsDir.Checked;
		}

		/// <summary>
		/// This method is called by the background thread when it has finished processing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_ProcessingComplete( Object sender, System.EventArgs e )
		{
			processing	=  false;

			ResetForm();
		}

		private double GetResolution()
		{
			return ( 360.0 );
		}

		private data FillData()
		{
			data theData =  new data();

			theData.sourceIsFiles =  rbFiles.Checked;

			if ( theData.sourceIsFiles )
			{
				theData.files         =  flvImageFiles.Files.FileStrings;
				theData.autoSaveAs    =  chkAutoSaveAs.Checked;
				theData.saveAs	      =  ltSaveAs.Text;
				theData.autoSaveAsDir =  chkAutoSaveAsDir.Checked;
				theData.saveAsDir     =  bfdSaveAs.Directory;
			}
			else
			{
				theData.sourceDir =  bfdSourceImages.Directory;
				theData.startRoll =  (int) ltStartRoll;
				theData.endRoll   =  (int) ltEndRoll;
			}

			return ( theData );
		}

		private ContactSheetOptions FillContactSheetOptions()
		{
			ContactSheetOptions	cso	=  new ContactSheetOptions();

			try
			{
				cso.Resolution			=  GetResolution();
				cso.AcrossFirst		=  cbPlace.SelectedIndex != 1;
				cso.BestFit				=  chkRotateForBestFit.Checked;
				cso.Flatten				=  chkFlattenAllLayers.Checked;
				cso.UseAutoSpacing	=  chkUseAutoSpacing.Checked;
				cso.ColumnCount		=  Int32.Parse( this.ltColumns.Text );
				cso.RowCount			=  Int32.Parse( this.ltRows.Text );
				cso.Caption				=  true;
				cso.Font					=  PsGalleryFontType.psHelvetica;
				cso.FontSize			=  5;
				cso.Height				=  Int32.Parse( ltHeight.Text )*(int) cso.Resolution;
				cso.Width				=  Int32.Parse( ltWidth.Text )*(int) cso.Resolution;
				cso.Mode					=  PsNewDocumentMode.psNewRGB;
				cso.Vertical			=  (int)( Double.Parse( ltVerticalSpacing.Text )*cso.Resolution );
				cso.Horizontal			=  (int)( Double.Parse( ltHorizontalSpacing.Text )*cso.Resolution );
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Load Contact Sheet Options" );
				throw new System.ArgumentException( "Bad data for Contact Sheet Options!" );
			}

			return ( cso );
		}

		private void btnProcess_Click( object sender, EventArgs e )
		{
			if ( !processing )
			{
				try
				{
					// Load the Contact Sheet Options.
					ContactSheetOptions	cso		=  FillContactSheetOptions();
					data						theData	=  FillData();

					// Create the thread.
					Processor processor = new Processor( this, theData, cso );

					processThread =  new Thread( new ThreadStart( processor.ThreadProc ) );

					// We're processing!
					processing	=  true;

					ResetForm();

					// We can start the thread now.
					processThread.Start();
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message, "Processing Loop" );
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

		public struct data
		{
			public bool		 sourceIsFiles;
			public ArrayList files;
			public string	 sourceDir;
			public int		 startRoll;
			public int		 endRoll;
			public string	 saveAs;
			public string	 saveAsDir;
			public bool		 autoSaveAs;
			public bool		 autoSaveAsDir;
		}

		internal class Processor
		{
			private ContactSheetMaker				myParent;
			private ContactSheetOptions		myCSOptions;
			private data								myDataOptions;
			private Photoshop.Application			psApp;
			private Photoshopper.Photoshopper	ps;

			public Processor( ContactSheetMaker parent, data dataOptions, ContactSheetOptions options )
			{
				myParent			=  parent;
				myDataOptions	=  dataOptions;
				myCSOptions		=  options;
			}

			public void ThreadProc()
			{
				try
				{
					Process();
				}

				finally
				{
					myParent.BeginInvoke( myParent.onProcessingComplete, new object[] { this, EventArgs.Empty } );
				}
			}


			private ArrayList GetFiles( int whichRoll )
			{
				if ( myDataOptions.sourceIsFiles )
				{
					return ( myDataOptions.files );
				}
				else
				{
					string			rollID	 =  whichRoll.ToString( @"'R'00" );
					DirectoryInfo	dir		 =  new DirectoryInfo( myDataOptions.sourceDir );
					FileInfo[]		dirFiles =  dir.GetFiles();
					Regex			re		 =  new Regex( @"(.+_" + rollID + @")_\d\d(.*\.tif)" );
					ArrayList		files	 =  new ArrayList();

					foreach ( FileInfo info in dirFiles )
					{
						string	name	=  info.Name;
						string	ext	=  info.Extension;

						if ( re.IsMatch( name ) )
						{
							files.Add( info.FullName );
						}
					}

					ArrayList filesAr =  new ArrayList( files.Count );

					foreach( string file in files )
					{
						filesAr.Add( file );
					}

					return ( filesAr );
				}
			}

			private string GetSaveAs( int whichRoll, string oneOfTheFiles )
			{
				if ( myDataOptions.sourceIsFiles )
				{
					if ( !myDataOptions.autoSaveAs )
						return ( myDataOptions.saveAs );
					else
					{
						return ( myDataOptions.saveAs );
					}
				}
				else
				{
					string	rollID	=  whichRoll.ToString( @"'R'00" );
					Regex		re			=  new Regex( @"(.+_" + rollID + @")_\d\d.*(.tif)" );
					string	saveAs	=  re.Match( oneOfTheFiles ).Result( "${1}${2}" );

					return ( saveAs );
				}
			}

			private string GetSaveAsDir()
			{
				if ( myDataOptions.sourceIsFiles )
				{
					if ( !myDataOptions.autoSaveAsDir )
						return ( myDataOptions.saveAsDir );
					else
					{
						return ( myDataOptions.saveAsDir );
					}
				}
				else
				{
					return ( myDataOptions.sourceDir );
				}
			}

			private void Process()
			{
				try
				{
					// Start up Photoshop.
					psApp =  new Photoshop.Application();
					ps		=  new Photoshopper.Photoshopper( psApp );

					// Don't display dialogs
					psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

					int	start	=  1;
					int	end	=  1;

					if ( !myDataOptions.sourceIsFiles )
					{
						start =  (int) myDataOptions.startRoll;
						end	  =  (int) myDataOptions.endRoll;
					}

					for ( int i= start;  i<= end;  i++ )
					{
						// Get the files.
						ArrayList theFiles =  GetFiles( i );

						if ( theFiles.Count == 0 )
						{
							continue;
						}

						string saveAsFilename =  GetSaveAs( i, (string) theFiles[ 0 ] );
						string saveAsDir      =  GetSaveAsDir();


						object[] sFiles	=  new object[ theFiles.Count ];
						int		 j      =  0;

						foreach ( string file in theFiles )
						{
							sFiles[ j++ ] =  file;
						}

						// Make the contact sheet.
						string result =  psApp.MakeContactSheet( sFiles, myCSOptions );

						// Get the document.
						Document imageDoc =  psApp.ActiveDocument;

						// Flatten the background layer too.
						if ( myCSOptions.Flatten )
						{
							imageDoc.Flatten();
						}

						// Save it.
						ps.SaveAsTIFF( imageDoc, saveAsDir, saveAsFilename );

						// Close it.
						imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
					}
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
				}
			}
		}
	}
}