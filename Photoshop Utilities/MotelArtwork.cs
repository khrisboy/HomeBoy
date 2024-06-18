using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Threading;

using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;

namespace PhotoshopUtilities
{
	public partial class MotelArtwork : MyWindowsForm
	{
		private Thread			 processThread;
		private HybridDictionary filesAndTitles	=  new HybridDictionary( true );
		private bool			 processing		=  false;
		private Object			 lastFileSelected;

		public delegate void SelectFileDelegate( MyFileInfo info );
		public SelectFileDelegate selectFileDelegate;

		public delegate void GetTextItemDataDelegate( TextItem item, int whichOne, string replacementText );
		public GetTextItemDataDelegate getTextItemDelegate;

		public delegate void RefreshDelegate();
		public RefreshDelegate refreshDelegate;

		public delegate void ResetFormDelegate();
		public ResetFormDelegate resetFormDelegate;

		public delegate void MyEventHandlerDelegate( Object sender, EventArgs e );
		public MyEventHandlerDelegate onProcessingComplete;

		public MotelArtwork()
		{
			InitializeComponent();

			this.theFilesListView.ValidFileTypes = new List<string>() {
																		".tif",
																		".psd",
																		".psb",
																		".jpg",
																		".bmp",
																		".png"};

			selectFileDelegate	 =  new SelectFileDelegate( SelectFile );
			getTextItemDelegate	 =  new GetTextItemDataDelegate( GetTextItemData );
			refreshDelegate		 =  new RefreshDelegate( RefreshMe );
			resetFormDelegate    =  new ResetFormDelegate( ResetForm );
			onProcessingComplete =  new MyEventHandlerDelegate( Form_ProcessingComplete );
		}

		private void SerializeIn()
		{
			// Read in the files and Titles.
			DefaultsAr defaults =  MyDefaults;

			int ith =  1;

			while ( defaults.Contains( Name + "_FilesAndTitles_" + ith ) )
			{
				string fileAndTitle	=  defaults[ Name + "_FilesAndTitles_" + ith ];

				// Split it apart.
				string[]	fatAr	=  fileAndTitle.Split( new char[] { ';' } );

				if ( fatAr.Length == 2 )
					filesAndTitles.Add( fatAr[ 0 ], fatAr[ 1 ] );

				ith++;
			}
		}

		private void SerializeOut()
		{
			DefaultsAr defaults =  MyDefaults;

			int ith =  1;

			foreach ( DictionaryEntry entry in filesAndTitles )
			{
				string	fileAndTitle	=  entry.Key + ";" + entry.Value;

				defaults[ Name + "_FilesAndTitles_" + ith ]	=  (MyString) fileAndTitle;

				ith++;
			}
		}

		private void EnableDisable()
		{
			theTabControl.Enabled		=  !processing;
			theFilesListView.Enabled	=  !processing;
			tiTitle.Enabled				=  !processing;
			tiPromoBlurb.Enabled			=  !processing;
			tiWebsite.Enabled				=  !processing;

			cbTitlesFiles.Enabled		=  chkTitlesPerFile.Checked;
		}

		void SelectFile( MyFileInfo info )
		{
			theFilesListView.Select( info );
		}

		void GetTextItemData( TextItem item, int whichOne, string replacementText )
		{
			switch( whichOne )
			{
				case 0:
					item =  tiTitle.Equals( item );
					break;
				case 1:
					item =  tiPromoBlurb.Equals( item );
					break;
				case 2:
					item =  tiWebsite.Equals( item );
					break;
			}

			if ( replacementText != "" )
				item.Contents	=  replacementText;
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

		private void Form_ProcessingComplete( Object sender, System.EventArgs e )
		{
			processing =  false;

			ResetForm();
		}

		public void TextProperties( TextItem text )
		{
			SolidColor			color =  text.Color;
			RGBColor				rgb	=  color.RGB;
			string				hex	=  rgb.HexValue;

			String				theText			=  text.Contents;
			String				font				=  text.Font;
			PsJustification	justification	=  text.Justification;
			PsAntiAlias			antiAlias		=  text.AntiAliasMethod;
			double				fontSize			=  text.Size;

			Array	unitValues =  (Array) text.Position;

			double	x	=  (double) unitValues.GetValue( 0 );
			double	y	=  (double) unitValues.GetValue( 1 );

			string info	=  "";

			info += "Color = ";
			info += hex;
			info += "\r\n";
			info += "Contents= ";
			info += theText;
			info += "\r\n";
			info += "Font= ";
			info += font;
			info += "\r\n";
			info += "Font Size= ";
			info += fontSize;
			info += "\r\n";
			info += "Justification= ";
			info += justification;
			info += "\r\n";
			info += "Anti-Alias= ";
			info += antiAlias;
			info += "\r\n";
			info += "X= ";
			info += x;
			info += "\r\n";
			info += "Y= ";
			info += y;
			info += "\r\n";

			MessageBox.Show( info, "Text Layer Info" );
		}

		private void Test_Click( object sender, EventArgs e )
		{
			Photoshop.Application		psApp	=  null;
			Photoshopper.Photoshopper	ps		=  null;

			PsUnits originalRulerUnits = PsUnits.psPixels;

			try
			{
				// Start up Photoshop.
				psApp =  new Photoshop.Application();
				ps		=  new Photoshopper.Photoshopper(psApp);

				// Save preferences.
				originalRulerUnits =  psApp.Preferences.RulerUnits;

				// Set ruler units to pixels
				psApp.Preferences.RulerUnits =  PsUnits.psPixels;

				// Don't display dialogs
				psApp.DisplayDialogs = PsDialogModes.psDisplayNoDialogs;

				// Get the layers for the current document.
				Document		imageDoc		=  psApp.ActiveDocument;
				ArtLayers	artLayers	=  imageDoc.ArtLayers;

				foreach ( ArtLayer artLayer in artLayers )
				{
					if ( artLayer.Kind == PsLayerKind.psTextLayer )
					{
						TextItem	text	=  artLayer.TextItem;

						TextProperties( text );
					}
				}
			}

			catch( Exception ex )
			{
				// Uh oh!
				MessageBox.Show( ex.Message, "Processing" );
			}

			finally
			{
				// Restore preferences.
				if ( psApp != null )
					psApp.Preferences.RulerUnits =  originalRulerUnits;
			}
		}

		private void btnAdd_Click( object sender, EventArgs e )
		{
			// Add text to current document.
			Photoshop.Application		psApp	=  null;
			Photoshopper.Photoshopper	ps		=  null;

			try
			{
				// Start up Photoshop.
				psApp =  new Photoshop.Application();
				ps		=  new Photoshopper.Photoshopper(psApp);

				Document imageDoc =  psApp.ActiveDocument;

				ArtLayer	newLayer	=  imageDoc.ArtLayers.Add();

				newLayer.Kind	=  PsLayerKind.psTextLayer;

				TextItem textItem = newLayer.TextItem;

				textItem =  tiTitle.Equals( textItem );
			}

			catch( Exception ex )
			{
				// Uh oh!
				MessageBox.Show( ex.Message, "Processing" );
			}
		}

		private void btnProcess_Click(object sender, EventArgs e)
		{
			try
			{
				if ( !processing )
				{
					// Get the list of files into an array.
					ArrayListUnique filesAr =  theFilesListView.Files;

					if ( filesAr.Count == 0 )
					{
						throw new Exception( "There must be at least 1 file to process!" );
					}

					// Create the thread.
					MotelArtWorkProcessor processor =  new MotelArtWorkProcessor( this, filesAr, filesAndTitles );

					processThread =  new Thread( new ThreadStart( processor.ThreadProc ) );

					// We're processing!
					processing =  true;

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

		private void MotelArtwork_Load(object sender, EventArgs e)
		{
			EnableDisable();
		}

		private void chkTitlesPerFile_CheckedChanged( object sender, EventArgs e )
		{
			cbTitlesFiles.Enabled	=  chkTitlesPerFile.Checked;
		}

		private void chkTitlesPerFile_SelectedIndexChanged( object sender, EventArgs e )
		{
			// Update the current file's Title.
			if ( lastFileSelected != null )
				filesAndTitles[ lastFileSelected ] =  tiTitle.Contents;

			lastFileSelected	=  cbTitlesFiles.SelectedItem;
			tiTitle.Contents	=  (string) filesAndTitles[ cbTitlesFiles.SelectedItem ];
		}

		private void tabTitles_Enter( object sender, EventArgs e )
		{
			// Update Dictionary
			MyFileInfosArray	files	=  theFilesListView.Files;

			foreach ( MyFileInfo info in files )
			{
				if ( !filesAndTitles.Contains( info.Name ) )
					filesAndTitles[ info.Name ]	=  "";

				// Update the combobox.
				if ( !cbTitlesFiles.Items.Contains( info.Name ) )
					cbTitlesFiles.Items.Add( info.Name );
			}
		}

		private void tabTitles_Leave(object sender, EventArgs e)
		{
			// Save the current text.
			if ( lastFileSelected != null )
				filesAndTitles[ lastFileSelected ] =  tiTitle.Contents;
		}
	}

	internal class MotelArtWorkProcessor
	{
		private ArrayListUnique		filesAr;
		private HybridDictionary	filesAndTitles;
		private MotelArtwork			myParent;

		private Photoshop.Application			psApp;
		private Photoshopper.Photoshopper	ps;

		public MotelArtWorkProcessor( MotelArtwork parent, ArrayListUnique files, HybridDictionary fat )
		{
			myParent			=  parent;
			filesAr			=  files;
			filesAndTitles	=  fat;
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
			PsUnits originalRulerUnits =  PsUnits.psPixels;

			try
			{
				// Start up Photoshop.
				psApp	=  new Photoshop.Application();
				ps		=  new Photoshopper.Photoshopper( psApp );

				// Save preferences.
				originalRulerUnits =  psApp.Preferences.RulerUnits;

				// Set ruler units to pixels
				psApp.Preferences.RulerUnits =  PsUnits.psPixels;

				// Don't display dialogs
				psApp.DisplayDialogs =  PsDialogModes.psDisplayNoDialogs;

				// Loop through the files.
				foreach ( MyFileInfo fileInfo in filesAr )
				{
					// Select the file that we're processing.
					IAsyncResult r =  myParent.BeginInvoke( myParent.selectFileDelegate, new object[] { fileInfo } );

					// Open the file.
					Document imageDoc =  ps.OpenDoc( fileInfo.FullName );

					// Create the new text layers.
					ArtLayer newLayer1  =  imageDoc.ArtLayers.Add();
					ArtLayer newLayer2  =  imageDoc.ArtLayers.Add();
					ArtLayer newLayer3  =  imageDoc.ArtLayers.Add();

					newLayer1.Kind	=  PsLayerKind.psTextLayer;
					newLayer2.Kind	=  PsLayerKind.psTextLayer;
					newLayer3.Kind	=  PsLayerKind.psTextLayer;

					TextItem textItem1 =  newLayer1.TextItem;	// Title.
					TextItem textItem2 =  newLayer2.TextItem;	// Promo blurb.
					TextItem textItem3 =  newLayer3.TextItem;	// Website.

					myParent.Invoke( myParent.getTextItemDelegate, new Object[] { textItem1, 0, filesAndTitles[ fileInfo.Name ] } );
					myParent.Invoke( myParent.getTextItemDelegate, new Object[] { textItem2, 1, "" } );
					myParent.Invoke( myParent.getTextItemDelegate, new Object[] { textItem3, 2, "" } );

					// Save.
					imageDoc.Save();

					// Close the file.
					imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
				}
			}

			catch( ThreadAbortException )
			{
				// We were aborted. Bummer!
			}

			catch( Exception ex )
			{
				// Uh oh!
				MessageBox.Show( ex.Message, "Processing" );
			}

			finally
			{
				// Restore preferences.
				if ( psApp != null )
					psApp.Preferences.RulerUnits =  originalRulerUnits;
			}
		}
	}
}