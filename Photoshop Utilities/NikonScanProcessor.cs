using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;
using NPlot;
using NPlot.Windows;

namespace PhotoshopUtilities
{
	public class NikonScanProcessor : MyWindowsForm
	{
		#region Member Variables
		public delegate void PlotHistogramDelegate( Histogram histogram, double delta );
		public PlotHistogramDelegate plotHistogramDelegate;

		public delegate void SelectFileDelegate( MyFileInfo info );
		public SelectFileDelegate selectFileDelegate;

		public delegate void RefreshDelegate();
		public RefreshDelegate refreshDelegate;

		public delegate void UpdateStatusInfoDelegate( string status );
		public UpdateStatusInfoDelegate updateStatusInfoDelegate;
		
		public delegate void MyEventHandlerDelegate( Object sender, EventArgs e );
		public MyEventHandlerDelegate onProcessingComplete;

		private Thread								processThread;
		private bool								processing;

		private FilesListView						theFilesListView;
		private MyControls.MyGroupBox				grpCrop;
		private System.Windows.Forms.Label			stCropWidth;
		private System.Windows.Forms.TextBox		edtCropWidth;
		private System.Windows.Forms.Label			stCropX;
		private System.Windows.Forms.Label			stCropHeight;
		private System.Windows.Forms.TextBox		edtCropHeight;
		private System.Windows.Forms.Label			stCropPx;
		private System.Windows.Forms.Button			btnProcess;
		private MyControls.MyGroupBox				grpEdgeMeasures;
		private HistogramPlotter					theHistogramPlotter;
		private MyControls.MyGroupBox				grpProfile;
		private MyControls.BrowseForColorProfile	browseForProfile;
		private System.Windows.Forms.Label			stMeasuresWeightsDelta;
		private System.Windows.Forms.TextBox		edtMeasuresDelta;
		private MyControls.MyGroupBox				grpSaveAsCopy;
		private MyControls.BrowseForDirectory		browseForSaveAsDirectory;
		private MyControls.MyGroupBox				grpRotateImages;
		private System.Windows.Forms.RadioButton	rbRotateNone;
		private System.Windows.Forms.RadioButton	rbRotateCCW;
		private System.Windows.Forms.RadioButton	rbRotateCW;
		private CheckBox							chkAssignProfile;
		private CheckBox							chkSaveAsCopy;
		private ToolTip								theToolTip;
		private PictureBox							pbImage;
		private System.ComponentModel.IContainer	components	=  null;
		#endregion

		public NikonScanProcessor()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.theFilesListView.ValidFileTypes = new List<string>() {
																		".tif",
																		".psd",
																		".psb",
																		".jpg",
																		".bmp"};

			processing	=  false;

			plotHistogramDelegate		=  new PlotHistogramDelegate( PlotHistogram );
			selectFileDelegate			=  new SelectFileDelegate( SelectFile );
			refreshDelegate				=  new RefreshDelegate( RefreshMe );
			updateStatusInfoDelegate	=  new UpdateStatusInfoDelegate( UpdateStatusInfo );
			onProcessingComplete		=  new MyEventHandlerDelegate( Form_ProcessingComplete );
		}

		private void EnableDisable()
		{
			grpCrop.Enabled				=  !processing;
			grpEdgeMeasures.Enabled		=  !processing;
			grpRotateImages.Enabled		=  !processing;
			grpSaveAsCopy.Enabled		=  !processing;
			grpProfile.Enabled			=  !processing;
			theFilesListView.Enabled	=  !processing;

			// If we're not processing make sure the assign profile and saveas browseForFiles are
			// enabled/disabled properly.
			if ( !processing )
			{
				browseForProfile.Enabled			=  chkAssignProfile.Checked;
				browseForSaveAsDirectory.Enabled	=  chkSaveAsCopy.Checked;
			}
		}

		private void UpdateStatusInfo( string status )
		{
			theHistogramPlotter.stInfo.Text		=  status;
			theHistogramPlotter.stInfo.Visible	=  processing;
		}

		void SelectFile( MyFileInfo info )
		{
			theFilesListView.Select( info );
		}

		void PlotHistogram( Histogram histogram, double delta )
		{
			theHistogramPlotter.PlotHistogram( histogram, delta );
		}

		void RefreshMe()
		{
			Refresh();

			// Clear the histogram plot surface.
			theHistogramPlotter.thePlotSurface2D.Clear();
		}

		private void ResetForm()
		{
			// Enable/Disable pertinent controls.
			EnableDisable();

			// Flip the button text.
			btnProcess.Text	=  processing ?  "Cancel" : "Process";

			// Always blank the info control.
			theHistogramPlotter.stInfo.Text		=  "";

			// Show/Hide the info control.
			theHistogramPlotter.stInfo.Visible	=  processing;

			// Clear the histogram plot surface.
			theHistogramPlotter.thePlotSurface2D.Clear();
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
			this.components = new System.ComponentModel.Container();
			this.theFilesListView = new MyControls.FilesListView();
			this.grpCrop = new MyControls.MyGroupBox();
			this.stCropWidth = new System.Windows.Forms.Label();
			this.edtCropWidth = new System.Windows.Forms.TextBox();
			this.stCropX = new System.Windows.Forms.Label();
			this.stCropHeight = new System.Windows.Forms.Label();
			this.edtCropHeight = new System.Windows.Forms.TextBox();
			this.stCropPx = new System.Windows.Forms.Label();
			this.btnProcess = new System.Windows.Forms.Button();
			this.grpEdgeMeasures = new MyControls.MyGroupBox();
			this.stMeasuresWeightsDelta = new System.Windows.Forms.Label();
			this.edtMeasuresDelta = new System.Windows.Forms.TextBox();
			this.theHistogramPlotter = new PhotoshopUtilities.HistogramPlotter();
			this.grpProfile = new MyControls.MyGroupBox();
			this.chkAssignProfile = new System.Windows.Forms.CheckBox();
			this.browseForProfile = new MyControls.BrowseForColorProfile();
			this.grpSaveAsCopy = new MyControls.MyGroupBox();
			this.chkSaveAsCopy = new System.Windows.Forms.CheckBox();
			this.browseForSaveAsDirectory = new MyControls.BrowseForDirectory();
			this.grpRotateImages = new MyControls.MyGroupBox();
			this.rbRotateCW = new System.Windows.Forms.RadioButton();
			this.rbRotateCCW = new System.Windows.Forms.RadioButton();
			this.rbRotateNone = new System.Windows.Forms.RadioButton();
			this.theToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.pbImage = new System.Windows.Forms.PictureBox();
			this.grpCrop.SuspendLayout();
			this.grpEdgeMeasures.SuspendLayout();
			this.grpProfile.SuspendLayout();
			this.grpSaveAsCopy.SuspendLayout();
			this.grpRotateImages.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
			this.SuspendLayout();
			// 
			// theFilesListView
			// 
			this.theFilesListView.FilesFilter = "Image Files(*.TIF;*.PSD;*.PSB;*.JPG;*.BMP;*.PNG)|*.TIF;*.PSD;*.PSB;*.JPG;*.BMP|Ph" +
				 "otoshop Files (*PSD;*.PSB)|*.PSD;*.PSB|TIFF Files (*.TIF)|*.TIF|JPEG Files (*.JP" +
				 "G)|*.JPG|All files (*.*)|*.*";
			this.theFilesListView.InitialDirectory = "c:\\";
			this.theFilesListView.Location = new System.Drawing.Point(16, 16);
			this.theFilesListView.MultiSelection = true;
			this.theFilesListView.Name = "theFilesListView";
			this.theFilesListView.Size = new System.Drawing.Size(420, 200);
			this.theFilesListView.TabIndex = 0;
			this.theFilesListView.Title = "Image Files";
			// 
			// grpCrop
			// 
			this.grpCrop.Controls.Add(this.stCropWidth);
			this.grpCrop.Controls.Add(this.edtCropWidth);
			this.grpCrop.Controls.Add(this.stCropX);
			this.grpCrop.Controls.Add(this.stCropHeight);
			this.grpCrop.Controls.Add(this.edtCropHeight);
			this.grpCrop.Controls.Add(this.stCropPx);
			this.grpCrop.Location = new System.Drawing.Point(19, 240);
			this.grpCrop.Name = "grpCrop";
			this.grpCrop.Size = new System.Drawing.Size(200, 80);
			this.grpCrop.TabIndex = 1;
			this.grpCrop.TabStop = false;
			this.grpCrop.Text = "Crop";
			// 
			// stCropWidth
			// 
			this.stCropWidth.Location = new System.Drawing.Point(16, 22);
			this.stCropWidth.Name = "stCropWidth";
			this.stCropWidth.Size = new System.Drawing.Size(40, 14);
			this.stCropWidth.TabIndex = 0;
			this.stCropWidth.Text = "Width";
			// 
			// edtCropWidth
			// 
			this.edtCropWidth.Location = new System.Drawing.Point(16, 38);
			this.edtCropWidth.Name = "edtCropWidth";
			this.edtCropWidth.Size = new System.Drawing.Size(60, 20);
			this.edtCropWidth.TabIndex = 1;
			// 
			// stCropX
			// 
			this.stCropX.Location = new System.Drawing.Point(76, 41);
			this.stCropX.Name = "stCropX";
			this.stCropX.Size = new System.Drawing.Size(14, 14);
			this.stCropX.TabIndex = 2;
			this.stCropX.Text = "x";
			this.stCropX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// stCropHeight
			// 
			this.stCropHeight.Location = new System.Drawing.Point(91, 22);
			this.stCropHeight.Name = "stCropHeight";
			this.stCropHeight.Size = new System.Drawing.Size(40, 14);
			this.stCropHeight.TabIndex = 3;
			this.stCropHeight.Text = "Height";
			// 
			// edtCropHeight
			// 
			this.edtCropHeight.Location = new System.Drawing.Point(91, 38);
			this.edtCropHeight.Name = "edtCropHeight";
			this.edtCropHeight.Size = new System.Drawing.Size(60, 20);
			this.edtCropHeight.TabIndex = 4;
			// 
			// stCropPx
			// 
			this.stCropPx.Location = new System.Drawing.Point(155, 41);
			this.stCropPx.Name = "stCropPx";
			this.stCropPx.Size = new System.Drawing.Size(18, 14);
			this.stCropPx.TabIndex = 5;
			this.stCropPx.Text = "px";
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point(719, 464);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(75, 23);
			this.btnProcess.TabIndex = 8;
			this.btnProcess.Text = "&Process";
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// grpEdgeMeasures
			// 
			this.grpEdgeMeasures.Controls.Add(this.stMeasuresWeightsDelta);
			this.grpEdgeMeasures.Controls.Add(this.edtMeasuresDelta);
			this.grpEdgeMeasures.Location = new System.Drawing.Point(240, 240);
			this.grpEdgeMeasures.Name = "grpEdgeMeasures";
			this.grpEdgeMeasures.Size = new System.Drawing.Size(200, 80);
			this.grpEdgeMeasures.TabIndex = 2;
			this.grpEdgeMeasures.TabStop = false;
			this.grpEdgeMeasures.Text = "Edge Measures";
			// 
			// stMeasuresWeightsDelta
			// 
			this.stMeasuresWeightsDelta.Location = new System.Drawing.Point(16, 22);
			this.stMeasuresWeightsDelta.Name = "stMeasuresWeightsDelta";
			this.stMeasuresWeightsDelta.Size = new System.Drawing.Size(87, 14);
			this.stMeasuresWeightsDelta.TabIndex = 0;
			this.stMeasuresWeightsDelta.Text = "Weights Delta";
			// 
			// edtMeasuresDelta
			// 
			this.edtMeasuresDelta.Location = new System.Drawing.Point(16, 38);
			this.edtMeasuresDelta.Name = "edtMeasuresDelta";
			this.edtMeasuresDelta.Size = new System.Drawing.Size(151, 20);
			this.edtMeasuresDelta.TabIndex = 1;
			this.theToolTip.SetToolTip(this.edtMeasuresDelta, "Amount the Weighted Average should be different to determine you\'re at an edge");
			// 
			// theHistogramPlotter
			// 
			this.theHistogramPlotter.Location = new System.Drawing.Point(452, 16);
			this.theHistogramPlotter.Name = "theHistogramPlotter";
			this.theHistogramPlotter.Size = new System.Drawing.Size(420, 304);
			this.theHistogramPlotter.TabIndex = 5;
			this.theHistogramPlotter.TabStop = false;
			// 
			// grpProfile
			// 
			this.grpProfile.Controls.Add(this.chkAssignProfile);
			this.grpProfile.Controls.Add(this.browseForProfile);
			this.grpProfile.Location = new System.Drawing.Point(19, 336);
			this.grpProfile.Name = "grpProfile";
			this.grpProfile.Size = new System.Drawing.Size(421, 80);
			this.grpProfile.TabIndex = 3;
			this.grpProfile.TabStop = false;
			this.grpProfile.Text = "Assign Profile";
			// 
			// chkAssignProfile
			// 
			this.chkAssignProfile.AutoSize = true;
			this.chkAssignProfile.Location = new System.Drawing.Point(16, 43);
			this.chkAssignProfile.Name = "chkAssignProfile";
			this.chkAssignProfile.Size = new System.Drawing.Size(15, 14);
			this.chkAssignProfile.TabIndex = 0;
			this.chkAssignProfile.UseVisualStyleBackColor = true;
			this.chkAssignProfile.CheckedChanged += new System.EventHandler(this.chkAssignProfile_CheckedChanged);
			// 
			// browseForProfile
			// 
			this.browseForProfile.BrowseLabel = "...";
			this.browseForProfile.DisplayFileNameOnly = false;
			this.browseForProfile.CheckFileExists = false;
			this.browseForProfile.FileName = null;
			this.browseForProfile.FilesFilter = "ICC files (*.icc)|*.icc|ICM files (*.icm)|*.icm|All files (*.*)|*.*";
			this.browseForProfile.FilterIndex = 1;
			this.browseForProfile.InitialDirectory = "C:\\WINDOWS\\system32\\spool\\drivers\\color";
			this.browseForProfile.Label = "Profile";
			this.browseForProfile.Location = new System.Drawing.Point(44, 24);
			this.browseForProfile.Name = "browseForProfile";
			this.browseForProfile.Size = new System.Drawing.Size(272, 38);
			this.browseForProfile.TabIndex = 1;
			// 
			// grpSaveAsCopy
			// 
			this.grpSaveAsCopy.Controls.Add(this.chkSaveAsCopy);
			this.grpSaveAsCopy.Controls.Add(this.browseForSaveAsDirectory);
			this.grpSaveAsCopy.Location = new System.Drawing.Point(452, 336);
			this.grpSaveAsCopy.Name = "grpSaveAsCopy";
			this.grpSaveAsCopy.Size = new System.Drawing.Size(421, 80);
			this.grpSaveAsCopy.TabIndex = 6;
			this.grpSaveAsCopy.TabStop = false;
			this.grpSaveAsCopy.Text = "Save As Copy";
			// 
			// chkSaveAsCopy
			// 
			this.chkSaveAsCopy.AutoSize = true;
			this.chkSaveAsCopy.Location = new System.Drawing.Point(16, 43);
			this.chkSaveAsCopy.Name = "chkSaveAsCopy";
			this.chkSaveAsCopy.Size = new System.Drawing.Size(15, 14);
			this.chkSaveAsCopy.TabIndex = 1;
			this.chkSaveAsCopy.UseVisualStyleBackColor = true;
			this.chkSaveAsCopy.CheckedChanged += new System.EventHandler(this.chkSaveAs_CheckedChanged);
			// 
			// browseForSaveAsDirectory
			// 
			this.browseForSaveAsDirectory.BrowseLabel = "...";
			this.browseForSaveAsDirectory.Directory = "";
			this.browseForSaveAsDirectory.Label = "Directory";
			this.browseForSaveAsDirectory.Location = new System.Drawing.Point(44, 24);
			this.browseForSaveAsDirectory.Name = "browseForSaveAsDirectory";
			this.browseForSaveAsDirectory.Size = new System.Drawing.Size(272, 38);
			this.browseForSaveAsDirectory.TabIndex = 0;
			// 
			// grpRotateImages
			// 
			this.grpRotateImages.Controls.Add(this.rbRotateCW);
			this.grpRotateImages.Controls.Add(this.rbRotateCCW);
			this.grpRotateImages.Controls.Add(this.rbRotateNone);
			this.grpRotateImages.Location = new System.Drawing.Point(19, 432);
			this.grpRotateImages.Name = "grpRotateImages";
			this.grpRotateImages.Size = new System.Drawing.Size(421, 80);
			this.grpRotateImages.TabIndex = 4;
			this.grpRotateImages.TabStop = false;
			this.grpRotateImages.Text = "Rotate Images";
			// 
			// rbRotateCW
			// 
			this.rbRotateCW.Location = new System.Drawing.Point(300, 31);
			this.rbRotateCW.Name = "rbRotateCW";
			this.rbRotateCW.Size = new System.Drawing.Size(62, 24);
			this.rbRotateCW.TabIndex = 2;
			this.rbRotateCW.Text = "CW";
			// 
			// rbRotateCCW
			// 
			this.rbRotateCCW.Location = new System.Drawing.Point(173, 31);
			this.rbRotateCCW.Name = "rbRotateCCW";
			this.rbRotateCCW.Size = new System.Drawing.Size(62, 24);
			this.rbRotateCCW.TabIndex = 1;
			this.rbRotateCCW.Text = "CCW";
			// 
			// rbRotateNone
			// 
			this.rbRotateNone.Checked = true;
			this.rbRotateNone.Location = new System.Drawing.Point(46, 31);
			this.rbRotateNone.Name = "rbRotateNone";
			this.rbRotateNone.Size = new System.Drawing.Size(62, 24);
			this.rbRotateNone.TabIndex = 0;
			this.rbRotateNone.TabStop = true;
			this.rbRotateNone.Text = "None";
			// 
			// theToolTip
			// 
			this.theToolTip.BackColor = System.Drawing.Color.LemonChiffon;
			this.theToolTip.ForeColor = System.Drawing.Color.Red;
			this.theToolTip.IsBalloon = true;
			// 
			// pbImage
			// 
			this.pbImage.Location = new System.Drawing.Point(496, 432);
			this.pbImage.Name = "pbImage";
			this.pbImage.Size = new System.Drawing.Size(140, 90);
			this.pbImage.TabIndex = 9;
			this.pbImage.TabStop = false;
			// 
			// NikonScanProcessor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(888, 540);
			this.Controls.Add(this.pbImage);
			this.Controls.Add(this.grpRotateImages);
			this.Controls.Add(this.grpSaveAsCopy);
			this.Controls.Add(this.grpProfile);
			this.Controls.Add(this.theHistogramPlotter);
			this.Controls.Add(this.btnProcess);
			this.Controls.Add(this.grpCrop);
			this.Controls.Add(this.theFilesListView);
			this.Controls.Add(this.grpEdgeMeasures);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "NikonScanProcessor";
			this.Text = "Nikon Scan Processor";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.NikonScanProcessor_Closing);
			this.Load += new System.EventHandler(this.NikonScanProcessor_Load);
			this.grpCrop.ResumeLayout(false);
			this.grpCrop.PerformLayout();
			this.grpEdgeMeasures.ResumeLayout(false);
			this.grpEdgeMeasures.PerformLayout();
			this.grpProfile.ResumeLayout(false);
			this.grpProfile.PerformLayout();
			this.grpSaveAsCopy.ResumeLayout(false);
			this.grpSaveAsCopy.PerformLayout();
			this.grpRotateImages.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnProcess_Click( object sender, System.EventArgs e )
		{
			try
			{
				if ( !processing )
				{
					// Get the list of files into an array.
					ArrayListUnique	filesAr	=  theFilesListView.Files;

					if ( filesAr.Count == 0 )
					{
						throw new Exception( "There must be at least 1 file to process!" );
					}

					// Get the cropping rectangle size.
					double	cropWidth	=  double.Parse( edtCropWidth.Text );
					double	cropHeight	=  double.Parse( edtCropHeight.Text );

					// Assign the profile?
					bool		assignProfile	=  chkAssignProfile.Checked;
					string	profile			=  browseForProfile.ProfileName;

					// If there is no color profile ask why.
					if ( !assignProfile || profile.Length == 0 )
					{
						if ( DialogResult.No == MessageBox.Show( "Are you sure that you don't want to assign a profile?", "Assign Profile", MessageBoxButtons.YesNo ) )
							return;
					}

					// Get the SaveAsCopy directory.
					bool	saveAsCopy		=  chkSaveAsCopy.Checked;
					string	saveAsCopyDir	=  browseForSaveAsDirectory.Directory;

					// Rotate?
					Rotate	rotate	=  Rotate.None;

					if ( rbRotateCCW.Checked )
						rotate	=  Rotate.CCW;
					else if ( rbRotateCW.Checked )
						rotate	=  Rotate.CW;

					double	weightsDelta	=  double.Parse( edtMeasuresDelta.Text );

					// Create the thread.
					Processor	processor	=  new Processor( this, filesAr, cropWidth, cropHeight, assignProfile, saveAsCopy, saveAsCopyDir,
															  rotate, profile, theHistogramPlotter, weightsDelta );

					processThread	=  new Thread( new ThreadStart( processor.ThreadProc ) );

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

					processing = false;

					ResetForm();
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Processing" );
			}
		}

		private void NikonScanProcessor_Load( object sender, System.EventArgs e )
		{
			browseForProfile.Width				=  360;
			browseForSaveAsDirectory.Width	=  360;

			// In case we closed while still processing.
			ResetForm();

			chkAssignProfile_CheckedChanged( sender , e );
			chkSaveAs_CheckedChanged( sender , e );
		}

		private void chkAssignProfile_CheckedChanged( object sender, EventArgs e )
		{
			browseForProfile.Enabled	=  chkAssignProfile.Checked;
		}

		private void chkSaveAs_CheckedChanged( object sender, EventArgs e )
		{
			browseForSaveAsDirectory.Enabled	=  chkSaveAsCopy.Checked;
		}

		private void NikonScanProcessor_Closing( object sender, System.ComponentModel.CancelEventArgs e )
		{
			// Make sure the thread is killed if we're processing.
			if ( processing )
			{
				processThread.Abort();
				processThread.Join();

				processing	=  false;
			}
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
	}

	internal enum Rotate { None, CW, CCW };
}
