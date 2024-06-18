using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;


namespace PhotoshopUtilities
{
	public class RemoveFrame : MyWindowsForm
	{
		#region Data Members
		public delegate void ResetFormDelegate();
		public ResetFormDelegate resetFormDelegate;

		public delegate void RefreshDelegate();
		public RefreshDelegate refreshDelegate;

		public delegate void MyEventHandlerDelegate( Object sender, EventArgs e );
		public MyEventHandlerDelegate onProcessingComplete;

		private Thread processThread;
		private bool processing;

		private MyControls.FilesListView flvImages;
		private MyControls.MyGroupBox grpSaveAs;
		private System.Windows.Forms.CheckBox chkSaveOriginal;
		private MyControls.BrowseForDirectory bfdSave;
		private MyControls.MyGroupBox grpSaveAsType;
		private System.Windows.Forms.RadioButton rbSaveAsTif;
		private System.Windows.Forms.RadioButton rbSaveAsGif;
		private System.Windows.Forms.RadioButton rbSaveAsJpg;
		private MyControls.MyGroupBox grpCrop;
		private MyControls.LabelAndText ltBottom;
		private MyControls.LabelAndText ltRight;
		private MyControls.LabelAndText ltTop;
		private MyControls.LabelAndText ltLeft;
		private System.Windows.Forms.CheckBox chkAllSame;
		private MyControls.MyGroupBox grpUnits;
		private System.Windows.Forms.RadioButton rbUnitsInches;
		private System.Windows.Forms.RadioButton rbUnitsPixels;
		private System.Windows.Forms.Button btnProcess;
		private TextBox edtJPEGQuality;
		private Label stJPEGQuality;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		#endregion

		public RemoveFrame()
		{
			InitializeComponent();

			processing	=  false;

			resetFormDelegate		=  new ResetFormDelegate( ResetForm );
			refreshDelegate		=  new RefreshDelegate( RefreshMe );
			onProcessingComplete	=  new MyEventHandlerDelegate( Form_ProcessingComplete );
		}

		private void chkAllSame_CheckedChanged( object sender, EventArgs e )
		{
			ltTop.Enabled		=  !chkAllSame.Checked;
			ltRight.Enabled	=  !chkAllSame.Checked;
			ltBottom.Enabled	=  !chkAllSame.Checked;
		}

		private void SaveAsType_CheckedChanged( object sender, EventArgs e )
		{
			// Enable/Disable JPEG Quality.
			edtJPEGQuality.Enabled	=  rbSaveAsJpg.Checked;
		}

		private void RemoveFrame_Load( object sender, EventArgs e )
		{
			SaveAsType_CheckedChanged( sender, e );
			chkAllSame_CheckedChanged( sender, e );
		}

		private void ResetForm()
		{
			// Enable/Disable pertinent controls.
			EnableDisable();

			// Flip the button text.
			btnProcess.Text	=  processing ?  "Cancel" : "Process";
		}

		void RefreshMe()
		{
			Refresh();
		}

		private void EnableDisable()
		{
			grpCrop.Enabled			=  !processing;
			grpSaveAsType.Enabled	=  !processing;
			flvImages.Enabled			=  !processing;
		}

		private data FillData()
		{
			data	theData	=  new data();

			theData.filesAr		=  flvImages.Files;
			theData.left			=  (double) ltLeft;

			if ( chkAllSame.Checked )
			{
				theData.top		=  theData.left;
				theData.right	=  theData.left;
				theData.bottom	=  theData.left;
			}
			else
			{
				theData.top		=  (double) ltTop;
				theData.right	=  (double) ltRight;
				theData.bottom	=  (double) ltBottom;
			}

			if ( chkSaveOriginal.Checked )
				theData.saveAsDir	=  "";
			else
				theData.saveAsDir	=  bfdSave.Directory;

			if ( rbSaveAsJpg.Checked )
			{
				theData.fileType		=  data.FileType.jpeg;
				theData.jpegQuality	=  Int32.Parse( edtJPEGQuality.Text );
			}
			else if ( rbSaveAsGif.Checked )
				theData.fileType	=  data.FileType.gif;
			else
				theData.fileType	=  data.FileType.tif;

			if ( rbUnitsInches.Checked )
				theData.unitsArePixels	=  false;
			else
				theData.unitsArePixels	=  true;

			return ( theData );
		}

		private void btnProcess_Click( object sender, EventArgs e )
		{
			if ( !processing )
			{
				try
				{
					data	theData	=  FillData();

					// Create the thread.
					Processor processor	=  new Processor( this, theData );

					processThread	=  new Thread( new ThreadStart( processor.ThreadProc ) );

					// We're processing!
					processing	=  true;

					ResetForm();

					// We can start the thread now.
					processThread.Start();
				}

				catch ( Exception ex )
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

		private void Form_ProcessingComplete( Object sender, System.EventArgs e )
		{
			processing = false;

			ResetForm();
		}

		private void RemoveFrame_FormClosing( object sender, FormClosingEventArgs e )
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
			this.flvImages = new MyControls.FilesListView();
			this.grpSaveAs = new MyControls.MyGroupBox();
			this.grpSaveAsType = new MyControls.MyGroupBox();
			this.edtJPEGQuality = new System.Windows.Forms.TextBox();
			this.stJPEGQuality = new System.Windows.Forms.Label();
			this.rbSaveAsTif = new System.Windows.Forms.RadioButton();
			this.rbSaveAsGif = new System.Windows.Forms.RadioButton();
			this.rbSaveAsJpg = new System.Windows.Forms.RadioButton();
			this.chkSaveOriginal = new System.Windows.Forms.CheckBox();
			this.bfdSave = new MyControls.BrowseForDirectory();
			this.grpCrop = new MyControls.MyGroupBox();
			this.chkAllSame = new System.Windows.Forms.CheckBox();
			this.grpUnits = new MyControls.MyGroupBox();
			this.rbUnitsInches = new System.Windows.Forms.RadioButton();
			this.rbUnitsPixels = new System.Windows.Forms.RadioButton();
			this.ltBottom = new MyControls.LabelAndText();
			this.ltRight = new MyControls.LabelAndText();
			this.ltTop = new MyControls.LabelAndText();
			this.ltLeft = new MyControls.LabelAndText();
			this.btnProcess = new System.Windows.Forms.Button();
			this.grpSaveAs.SuspendLayout();
			this.grpSaveAsType.SuspendLayout();
			this.grpCrop.SuspendLayout();
			this.grpUnits.SuspendLayout();
			this.SuspendLayout();
			// 
			// flvImages
			// 
			this.flvImages.Location = new System.Drawing.Point( 16, 16 );
			this.flvImages.Name = "flvImages";
			this.flvImages.Size = new System.Drawing.Size( 420, 200 );
			this.flvImages.TabIndex = 0;
			// 
			// grpSaveAs
			// 
			this.grpSaveAs.Controls.Add( this.grpSaveAsType );
			this.grpSaveAs.Controls.Add( this.chkSaveOriginal );
			this.grpSaveAs.Controls.Add( this.bfdSave );
			this.grpSaveAs.Location = new System.Drawing.Point( 16, 241 );
			this.grpSaveAs.Name = "grpSaveAs";
			this.grpSaveAs.Size = new System.Drawing.Size( 420, 177 );
			this.grpSaveAs.TabIndex = 1;
			this.grpSaveAs.TabStop = false;
			this.grpSaveAs.Text = "Save";
			// 
			// grpSaveAsType
			// 
			this.grpSaveAsType.Controls.Add( this.edtJPEGQuality );
			this.grpSaveAsType.Controls.Add( this.stJPEGQuality );
			this.grpSaveAsType.Controls.Add( this.rbSaveAsTif );
			this.grpSaveAsType.Controls.Add( this.rbSaveAsGif );
			this.grpSaveAsType.Controls.Add( this.rbSaveAsJpg );
			this.grpSaveAsType.Location = new System.Drawing.Point( 16, 99 );
			this.grpSaveAsType.Name = "grpSaveAsType";
			this.grpSaveAsType.Size = new System.Drawing.Size( 319, 58 );
			this.grpSaveAsType.TabIndex = 5;
			this.grpSaveAsType.TabStop = false;
			this.grpSaveAsType.Text = "Type";
			// 
			// edtJPEGQuality
			// 
			this.edtJPEGQuality.Location = new System.Drawing.Point( 240, 25 );
			this.edtJPEGQuality.Name = "edtJPEGQuality";
			this.edtJPEGQuality.Size = new System.Drawing.Size( 48, 20 );
			this.edtJPEGQuality.TabIndex = 6;
			// 
			// stJPEGQuality
			// 
			this.stJPEGQuality.AutoSize = true;
			this.stJPEGQuality.Location = new System.Drawing.Point( 198, 28 );
			this.stJPEGQuality.Name = "stJPEGQuality";
			this.stJPEGQuality.Size = new System.Drawing.Size( 39, 13 );
			this.stJPEGQuality.TabIndex = 5;
			this.stJPEGQuality.Text = "&Quality";
			// 
			// rbSaveAsTif
			// 
			this.rbSaveAsTif.AutoSize = true;
			this.rbSaveAsTif.Location = new System.Drawing.Point( 16, 24 );
			this.rbSaveAsTif.Name = "rbSaveAsTif";
			this.rbSaveAsTif.Size = new System.Drawing.Size( 33, 17 );
			this.rbSaveAsTif.TabIndex = 2;
			this.rbSaveAsTif.TabStop = true;
			this.rbSaveAsTif.Text = "&tif";
			this.rbSaveAsTif.UseVisualStyleBackColor = true;
			this.rbSaveAsTif.CheckedChanged += new System.EventHandler( this.SaveAsType_CheckedChanged );
			// 
			// rbSaveAsGif
			// 
			this.rbSaveAsGif.AutoSize = true;
			this.rbSaveAsGif.Location = new System.Drawing.Point( 77, 24 );
			this.rbSaveAsGif.Name = "rbSaveAsGif";
			this.rbSaveAsGif.Size = new System.Drawing.Size( 36, 17 );
			this.rbSaveAsGif.TabIndex = 4;
			this.rbSaveAsGif.TabStop = true;
			this.rbSaveAsGif.Text = "&gif";
			this.rbSaveAsGif.UseVisualStyleBackColor = true;
			this.rbSaveAsGif.CheckedChanged += new System.EventHandler( this.SaveAsType_CheckedChanged );
			// 
			// rbSaveAsJpg
			// 
			this.rbSaveAsJpg.AutoSize = true;
			this.rbSaveAsJpg.Location = new System.Drawing.Point( 141, 24 );
			this.rbSaveAsJpg.Name = "rbSaveAsJpg";
			this.rbSaveAsJpg.Size = new System.Drawing.Size( 39, 17 );
			this.rbSaveAsJpg.TabIndex = 3;
			this.rbSaveAsJpg.TabStop = true;
			this.rbSaveAsJpg.Text = "&jpg";
			this.rbSaveAsJpg.UseVisualStyleBackColor = true;
			this.rbSaveAsJpg.CheckedChanged += new System.EventHandler( this.SaveAsType_CheckedChanged );
			// 
			// chkSaveOriginal
			// 
			this.chkSaveOriginal.AutoSize = true;
			this.chkSaveOriginal.Location = new System.Drawing.Point( 35, 66 );
			this.chkSaveOriginal.Name = "chkSaveOriginal";
			this.chkSaveOriginal.Size = new System.Drawing.Size( 61, 17 );
			this.chkSaveOriginal.TabIndex = 1;
			this.chkSaveOriginal.Text = "&Original";
			this.chkSaveOriginal.UseVisualStyleBackColor = true;
			// 
			// bfdSave
			// 
			this.bfdSave.BrowseLabel = "...";
			this.bfdSave.Directory = "";
			this.bfdSave.Label = "&Save In";
			this.bfdSave.Location = new System.Drawing.Point( 16, 20 );
			this.bfdSave.Name = "bfdSave";
			this.bfdSave.Size = new System.Drawing.Size( 382, 38 );
			this.bfdSave.TabIndex = 0;
			// 
			// grpCrop
			// 
			this.grpCrop.Controls.Add( this.chkAllSame );
			this.grpCrop.Controls.Add( this.grpUnits );
			this.grpCrop.Controls.Add( this.ltBottom );
			this.grpCrop.Controls.Add( this.ltRight );
			this.grpCrop.Controls.Add( this.ltTop );
			this.grpCrop.Controls.Add( this.ltLeft );
			this.grpCrop.Location = new System.Drawing.Point( 456, 16 );
			this.grpCrop.Name = "grpCrop";
			this.grpCrop.Size = new System.Drawing.Size( 227, 283 );
			this.grpCrop.TabIndex = 2;
			this.grpCrop.TabStop = false;
			this.grpCrop.Text = "Crop";
			// 
			// chkAllSame
			// 
			this.chkAllSame.AutoSize = true;
			this.chkAllSame.Location = new System.Drawing.Point( 61, 165 );
			this.chkAllSame.Name = "chkAllSame";
			this.chkAllSame.Size = new System.Drawing.Size( 104, 17 );
			this.chkAllSame.TabIndex = 5;
			this.chkAllSame.Text = "Same &All Around";
			this.chkAllSame.UseVisualStyleBackColor = true;
			this.chkAllSame.CheckedChanged += new System.EventHandler( this.chkAllSame_CheckedChanged );
			// 
			// grpUnits
			// 
			this.grpUnits.Controls.Add( this.rbUnitsInches );
			this.grpUnits.Controls.Add( this.rbUnitsPixels );
			this.grpUnits.Location = new System.Drawing.Point( 30, 205 );
			this.grpUnits.Name = "grpUnits";
			this.grpUnits.Size = new System.Drawing.Size( 167, 54 );
			this.grpUnits.TabIndex = 4;
			this.grpUnits.TabStop = false;
			this.grpUnits.Text = "Units";
			// 
			// rbUnitsInches
			// 
			this.rbUnitsInches.AutoSize = true;
			this.rbUnitsInches.Location = new System.Drawing.Point( 98, 22 );
			this.rbUnitsInches.Name = "rbUnitsInches";
			this.rbUnitsInches.Size = new System.Drawing.Size( 56, 17 );
			this.rbUnitsInches.TabIndex = 1;
			this.rbUnitsInches.TabStop = true;
			this.rbUnitsInches.Text = "&inches";
			this.rbUnitsInches.UseVisualStyleBackColor = true;
			// 
			// rbUnitsPixels
			// 
			this.rbUnitsPixels.AutoSize = true;
			this.rbUnitsPixels.Location = new System.Drawing.Point( 22, 22 );
			this.rbUnitsPixels.Name = "rbUnitsPixels";
			this.rbUnitsPixels.Size = new System.Drawing.Size( 51, 17 );
			this.rbUnitsPixels.TabIndex = 0;
			this.rbUnitsPixels.TabStop = true;
			this.rbUnitsPixels.Text = "pi&xels";
			this.rbUnitsPixels.UseVisualStyleBackColor = true;
			// 
			// ltBottom
			// 
			this.ltBottom.Label = "&Bottom";
			this.ltBottom.Location = new System.Drawing.Point( 77, 108 );
			this.ltBottom.Name = "ltBottom";
			this.ltBottom.Size = new System.Drawing.Size( 72, 37 );
			this.ltBottom.TabIndex = 3;
			// 
			// ltRight
			// 
			this.ltRight.Label = "&Right Side";
			this.ltRight.Location = new System.Drawing.Point( 137, 60 );
			this.ltRight.Name = "ltRight";
			this.ltRight.Size = new System.Drawing.Size( 72, 37 );
			this.ltRight.TabIndex = 2;
			// 
			// ltTop
			// 
			this.ltTop.Label = "&Top";
			this.ltTop.Location = new System.Drawing.Point( 77, 17 );
			this.ltTop.Name = "ltTop";
			this.ltTop.Size = new System.Drawing.Size( 72, 37 );
			this.ltTop.TabIndex = 1;
			// 
			// ltLeft
			// 
			this.ltLeft.Label = "&Left Side";
			this.ltLeft.Location = new System.Drawing.Point( 18, 60 );
			this.ltLeft.Name = "ltLeft";
			this.ltLeft.Size = new System.Drawing.Size( 72, 37 );
			this.ltLeft.TabIndex = 0;
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point( 533, 375 );
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size( 75, 23 );
			this.btnProcess.TabIndex = 3;
			this.btnProcess.Text = "&Process";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler( this.btnProcess_Click );
			// 
			// RemoveFrame
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 702, 436 );
			this.Controls.Add( this.btnProcess );
			this.Controls.Add( this.grpCrop );
			this.Controls.Add( this.grpSaveAs );
			this.Controls.Add( this.flvImages );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "RemoveFrame";
			this.Text = "RemoveFrame";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.RemoveFrame_FormClosing );
			this.Load += new System.EventHandler( this.RemoveFrame_Load );
			this.grpSaveAs.ResumeLayout( false );
			this.grpSaveAs.PerformLayout();
			this.grpSaveAsType.ResumeLayout( false );
			this.grpSaveAsType.PerformLayout();
			this.grpCrop.ResumeLayout( false );
			this.grpCrop.PerformLayout();
			this.grpUnits.ResumeLayout( false );
			this.grpUnits.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		public struct data
		{
			public ArrayListUnique	filesAr;
			public string				saveAsDir;
			public double				left;
			public double				top;
			public double				right;
			public double				bottom;
			public bool					unitsArePixels;

			public enum	FileType { tif, jpeg, gif };

			public FileType			fileType;
			public int					jpegQuality;
		}

		internal class Processor
		{
			private RemoveFrame						myParent;
			private data								myDataOptions;
			private Photoshop.Application			psApp;
			private Photoshopper.Photoshopper	ps;

			public Processor( RemoveFrame parent, data dataOptions )
			{
				myParent			=  parent;
				myDataOptions	=  dataOptions;
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

			private void Process()
			{
				PsUnits originalRulerUnits	=  PsUnits.psPixels;

				try
				{
					// Start up Photoshop.
					psApp	=  new Photoshop.Application();
					ps		=  new Photoshopper.Photoshopper( psApp );

					// Don't display dialogs.
					psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

					// Save preferences.
					originalRulerUnits	=  psApp.Preferences.RulerUnits;

					// Set ruler units to pixels
					psApp.Preferences.RulerUnits = myDataOptions.unitsArePixels ?  PsUnits.psPixels : PsUnits.psInches;

					for ( int i= 0;  i< myDataOptions.filesAr.Count;  i++ )
					{
						// Get the document.
						MyFileInfo	fileInfo	=  (MyFileInfo) myDataOptions.filesAr[ i ];
						Document		imageDoc	=  ps.OpenDoc( fileInfo.FullName );

						// Select the image minus the frame.
						double[]	selCorners	=  new double[ 4 ] { myDataOptions.left, myDataOptions.top,  imageDoc.Width-myDataOptions.right, imageDoc.Height-myDataOptions.bottom };
						Bounds	selBounds	=  new Bounds( selCorners );

						// Crop it.
						ps.SelectRectArea( selBounds, PsSelectionType.psReplaceSelection, 0.0 );
						ps.Crop();

						// Deselect.
						imageDoc.Selection.Deselect();

						// Save it.
						if ( myDataOptions.fileType == data.FileType.jpeg )
							ps.SaveAsJPEG( imageDoc, myDataOptions.saveAsDir, "", myDataOptions.jpegQuality );
						else if ( myDataOptions.fileType == data.FileType.gif )
							ps.SaveAsGIF( imageDoc, myDataOptions.saveAsDir, "" );
						else
							ps.SaveAsTIFF( imageDoc, myDataOptions.saveAsDir, "" );

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
					// Restore preferences.
					if ( psApp != null )
						psApp.Preferences.RulerUnits	=  originalRulerUnits;
				}
			}
		}
	}
}