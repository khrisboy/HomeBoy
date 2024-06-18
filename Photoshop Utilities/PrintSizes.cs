using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;
using PhotoshopSupport;

namespace PhotoshopUtilities
{
	public partial class PrintSizes : MyWindowsForm
	{
		#region Member variables.
		private Thread				processThread;
		private bool				processing;

		public delegate void SelectFileDelegate( MyFileInfo info );
		public SelectFileDelegate selectFileDelegate;

		public delegate void RefreshDelegate();
		public RefreshDelegate refreshDelegate;

		public delegate void MyEventHandlerDelegate( Object sender, EventArgs e );
		public MyEventHandlerDelegate onProcessingComplete;
		#endregion

		public PrintSizes()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.flvImages.ValidFileTypes = new List<string>() {
																".tif",
																".psd",
																".psb",
																".jpg",
																".nef",
																".crw",
																".bmp",
																".png"};

			selectFileDelegate		=  new SelectFileDelegate( SelectFile );
			refreshDelegate			=  new RefreshDelegate( RefreshMe );
			onProcessingComplete		=	new MyEventHandlerDelegate( Form_ProcessingComplete );
		}

		private void ResetForm()
		{
			// Enable/Disable pertinent controls.
			EnableDisable();

			// Flip the button text.
			btnPrintSizes.Text =  processing ? "Cancel" : "Process";
		}

		private void EnableDisable()
		{
			grpGanging.Enabled					=  !processing;
			grpPrintSizes.Enabled				=  !processing;
			grpPrintSizesDirectories.Enabled	=  !processing;
			flvImages.Enabled						=  !processing;
			nikSharpenCtrl.Enabled				=  !processing;
		}

		void SelectFile( MyFileInfo info )
		{
			flvImages.Select( info );
		}

		void RefreshMe()
		{
			Refresh();
		}

		private void Form_ProcessingComplete( Object sender, System.EventArgs e )
		{
			processing =  false;

			ResetForm();
		}

		private void PrintSizes_Load( object sender, System.EventArgs e )
		{
		}

		private void PrintSizes_FormClosing( object sender, FormClosingEventArgs e )
		{
			// Make sure the thread is killed if we're processing.
			if ( processing )
			{
				processThread.Abort();
				processThread.Join();

				processing =  false;
			}
		}

		private void rbPrintSizesRatio_CheckedChanged( object sender, EventArgs e )
		{
			bool	is3x2	=  rbPrintSizes3x2.Checked || rbPrintSizesAuto.Checked;
			bool	is4x3	=  rbPrintSizes4x3.Checked || rbPrintSizesAuto.Checked;

			chk6x9.Enabled		=  is3x2;
			chk8x12.Enabled		=  is3x2;
			chk12x16.Enabled	=  is3x2;
			chk16x24.Enabled	=  is3x2;
			//***chk20x30.Enabled	=  is3x2;

			chk6x8.Enabled		=  is4x3;
			chk9x12.Enabled		=  is4x3;
			chk12x16.Enabled	=  is4x3;
			chk18x24.Enabled	=  is4x3;
			//***chk24x32.Enabled	=  is4x3;
		}

		private void btnPrintSizes_Click( object sender, System.EventArgs e )
		{
			try
			{
				if ( !processing )
				{
					// Validate the input data (throws an exception if no good).
					ValidatePrintSizesData();

					// Get the sharpening level, paper type, printer resolution.
					NikSharpenInfo.NikProfileType	sharpenLevel		=  nikSharpenCtrl.SharpenProfile;
					int								printerResolution	=  nikSharpenCtrl.PrinterResolution;
					int								paperType			=  nikSharpenCtrl.PaperType;

					// What kind of print blocks type?
					PrintSizesProcessor.PrintBlocksType	printBlocksType;

					if ( rbCombineMultPicturesAllSizes.Checked )
						printBlocksType	=  PrintSizesProcessor.PrintBlocksType.MultImagesAllSize;
					else if ( rbCombineMultPicturesOneSize.Checked )
						printBlocksType	=  PrintSizesProcessor.PrintBlocksType.MultImagesOneSize;
					else if ( rbCombineOnePictureAllSizes.Checked )
						printBlocksType	=  PrintSizesProcessor.PrintBlocksType.OneImageAllSizes;
					else
						printBlocksType	=  PrintSizesProcessor.PrintBlocksType.OneImageOneSize;

					// Create the thread.
					PrintSizesProcessor	processor	=  new PrintSizesProcessor( this, flvImages.Files, sharpenLevel, printerResolution,
						                                                          paperType, true, printBlocksType );

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

					processing =  false;

					ResetForm();
				}
			}

			catch( ApplicationBadDataException ex )
			{
				Messager.Show( ex.Message );
				ex.BadDataControl.Focus();
			}

			catch( Exception ex )
			{
				Messager.Show( ex.Message );
			}
		}
	
		private void ValidatePrintSizesData()
		{
		}

		internal class PrintSizesProcessor
		{
			public enum PrintBlocksType { MultImagesOneSize, MultImagesAllSize, OneImageAllSizes, OneImageOneSize };

			#region Members
			const string	ProPhoto				=  "ProPhoto RGB";
			const int		autoEyeDistance	=  0;

			MyFileInfosArray								filesAr;
			NikSharpenInfo.NikProfileType					sharpenLevel;
			int												paperType;
			int												printerResolution;
			bool												askIfNoLandChannel;
			PrintBlocksType								printBlocksType;
			DirectoryInfo									imagesDirInfo;

			PrintSizes						printSizes;
			private double					resolution		=  360.0;
			Photoshop.Application		photoshopApp	=  null;
			Photoshopper.Photoshopper	ps					=  null;
			#endregion

			public PrintSizesProcessor( PrintSizes parent, MyFileInfosArray files,
				                         NikSharpenInfo.NikProfileType nikProfileType,
										 int nikPrinterResolution, int nikPaperType,
				                         bool askToSharpen, PrintBlocksType pbType )
			{
				filesAr					=  files;
				sharpenLevel			=  nikProfileType;
				printerResolution		=  nikPrinterResolution;
				paperType				=  nikPaperType;
				askIfNoLandChannel	=  askToSharpen;
				printBlocksType		=  pbType;
				printSizes				=  parent;
			}

			public void ThreadProc()
			{
				try
				{
					Process();
				}

				finally
				{
					printSizes.BeginInvoke( printSizes.onProcessingComplete, new object[] { this, EventArgs.Empty } );
				}
			}

			public void Process()
			{
				// Create the Photoshop object.
				photoshopApp	=  new Photoshop.Application();
				ps				=  new Photoshopper.Photoshopper( photoshopApp );

				// Save preferences.
				PsUnits	originalRulerUnits	=  photoshopApp.Preferences.RulerUnits;
			
				// Set ruler units to pixels
				photoshopApp.Preferences.RulerUnits	=  PsUnits.psPixels;
			
				// Don't display dialogs
				photoshopApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

				// Are all the images from one directory.
				imagesDirInfo	=  filesAr[ 0 ].FileInfo.Directory;

				foreach( MyFileInfo fileInfo in filesAr )
				{
					if ( fileInfo.FileInfo.Directory != imagesDirInfo )
					{
						break;
					}
				}

				// Create the print block(s)
				if ( printBlocksType == PrintBlocksType.MultImagesOneSize )
				{
					// Create print files first.
					MyFileInfosArray printFiles =  CreatePrintFiles();

					// Now create the printing blocks.
					CreateMultImagesOneSizePrintBlocks( printFiles );
				}
				else if ( printBlocksType == PrintBlocksType.OneImageAllSizes )
				{
					CreateOneImageAllSizesBlocks();
				}
				else if ( printBlocksType == PrintBlocksType.OneImageOneSize )
				{
					// Create print files first.
					MyFileInfosArray printFiles =  CreatePrintFiles();

					// Now create the printing blocks.
					CreateOneImageOneSizeBlocks( printFiles );
				}

				// Restore units
				photoshopApp.Preferences.RulerUnits	=  originalRulerUnits;
			}

			MyFileInfo GetPrintFileInfo( int whichSize, MyFileInfo imageInfo )
			{
				string	baseName		=  imageInfo.Name;
				int		indexOfExt	=  baseName.IndexOf( imageInfo.FileInfo.Extension );

				baseName	=  baseName.Substring( 0, indexOfExt );

				string	printFilename	=  "";

				switch ( whichSize )
				{
					case 0:
						printFilename	=  baseName + "_(6x9)";
						break;

					case 1:
						printFilename	=  baseName + "_(8x12)";
						break;

					case 2:
						printFilename	=  baseName + "_(12x18)";
						break;

					case 3:
						printFilename	=  baseName + "_(16x24)";
						break;

					case 4:
						printFilename	=  baseName + "_(18x24)";
						break;
				}

				// Get path and create output path name.
				string	pathName	=  GetPrintBlocksDirectory( imageInfo.FileInfo.Directory,  printSizes.bfdDirectoriesPrintFiles.Directory, printSizes.chkDirectoriesPrintsDirectoryRelative.Checked );

// 				DirectoryInfo	dirInfo			=  imageInfo.FileInfo.Directory;
// 				string			basePathName	=  dirInfo.FullName;
// 				string			pathName			=  basePathName + "\\" + printSizes.bfdDirectoriesPrintFiles.Directory;

				MyFileInfo	printFileInfo	=  new MyFileInfo( pathName + printFilename + ".tif" );

				return ( printFileInfo );
			}

			private MyFileInfosArray CreatePrintFiles()
			{
				MyFileInfosArray	printFilesAr	=  new MyFileInfosArray();

				try
				{
					for ( int i= 0;  i< filesAr.Count;  i++ )
					{
						MyFileInfo	imageInfo	=  filesAr[ i ];

						// Select the file that we're processing.
						IAsyncResult r =  printSizes.BeginInvoke( printSizes.selectFileDelegate, new object[] { imageInfo } );

						// Open document.
						Document	imageDoc	=  ps.OpenDoc( imageInfo.FullName );
								
						// Save this history state.
						HistoryState	historyStateOpened	=  imageDoc.ActiveHistoryState;

						DirectoryInfo	dirInfo			=  imageInfo.FileInfo.Directory;
						string			basePathName	=  dirInfo.FullName;

						// Loop through all the sizes.
						for ( int j= 0;  j< 5;  j++ )
						{
							double	newHeight		=  0;

							// Create document for this print size.
							switch ( j )
							{
								case 0:
								{
									if ( printSizes.chk6x9.Checked )
									{
										newHeight		=  6.0;
									}

									break;
								}

								case 1:
								{
									if ( printSizes.chk8x12.Checked )
									{
										newHeight		=  8.0;
									}

									break;
								}

								case 2:
								{
									if ( printSizes.chk12x18.Checked )
									{
										newHeight		=  12.0;
									}

									break;
								}

								case 3:
								{
									if ( printSizes.chk16x24.Checked )
									{
										newHeight		=  16.0;
									}

									break;
								}

								case 4:
								{
									if ( printSizes.chk18x24.Checked )
									{
										newHeight		=  18.0;
									}

									break;
								}

								default:
								{
									MessageBox.Show( "Did you forget to add for another size?" );
									break;
								}
							}

							if ( newHeight > 0 )
							{
								MyFileInfo printFileInfo =  GetPrintFileInfo( j, imageInfo );

								if ( !( printFileInfo.FileInfo.Exists && printSizes.chkPrintSizesNewOnly.Checked ) )
									PrepareSpecificSize( imageDoc, newHeight, j, imageInfo );

								printFilesAr.Add( printFileInfo );
							}

							// Reset history state.
							imageDoc.ActiveHistoryState	=  historyStateOpened;
						}

						// Close document
						imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );

						// Refresh everyone?
						printSizes.BeginInvoke( printSizes.refreshDelegate );
					}
				}

				catch ( Exception ex )
				{
					MessageBox.Show( ex.Message );
				}

				return ( printFilesAr );
			}

			private MyFileInfo PrepareSpecificSize( Document imageDoc, double newHeight, int whichSize, MyFileInfo imageInfo )
			{
				MyFileInfo printFileInfo =  GetPrintFileInfo( whichSize, imageInfo );

				// Resize to new size.
				MyClasses.Size	size	=  new MyClasses.Size();

				if ( imageDoc.Width > imageDoc.Height )
				{
					size.Height	=  newHeight*resolution;
					size.Width	=  (size.Height/imageDoc.Height)*imageDoc.Width;
				}
				else
				{
					size.Width	=  newHeight*resolution;
					size.Height	=  (size.Width/imageDoc.Width)*imageDoc.Height;
				}

				// Make sure there's only 1 layer.
				ps.theApp.ActiveDocument	=  imageDoc;
				imageDoc.Flatten();

				// Now resize to 360dpi
				imageDoc.ResizeImage( null, null, resolution, PsResampleMethod.psNoResampling );
				imageDoc.ResizeImage( null, size.Height, resolution, PsResampleMethod.psBicubic );

				// Make sure it's 8-bit (for old Nik Sharpener).
				imageDoc.BitsPerChannel	=  PsBitsPerChannelType.psDocument8Bits;

				// And sharpen.
				Sharpen( imageDoc, askIfNoLandChannel );

				// Get path and create output path name.
				DirectoryInfo	dirInfo	=  printFileInfo.FileInfo.Directory;
				string			pathName	=  dirInfo.FullName + @"\";

				// Save as tiff.
				return ( new MyFileInfo( ps.SaveAsTIFF( imageDoc, pathName, printFileInfo.Name ) ) );
			}

			bool DoThisPrintSize( int whichSize )
			{
				bool doThisSize	=  false;

				if ( whichSize == 0 && printSizes.chk6x9.Checked )
					doThisSize =  true;
				else if ( whichSize == 1 && printSizes.chk8x12.Checked )
					doThisSize =  true;
				else if ( whichSize == 2 && printSizes.chk12x18.Checked )
					doThisSize =  true;
				else if ( whichSize == 3 && printSizes.chk16x24.Checked )
					doThisSize =  true;
				else if ( whichSize == 4 && printSizes.chk18x24.Checked )
					doThisSize =  true;

				return ( doThisSize );
			}

			bool DoThisPrintBlockSize( int whichSize )
			{
				bool doThisPrintBlockSize	=  false;

				if ( whichSize == 0 && printSizes.ltGanging6x9.Text != "" )
					doThisPrintBlockSize =  true;
				else if ( whichSize == 1 && printSizes.ltGanging8x12.Text != "" )
					doThisPrintBlockSize =  true;
				else if ( whichSize == 2 && printSizes.ltGanging12x18.Text != "" )
					doThisPrintBlockSize =  true;
				else if ( whichSize == 3 && printSizes.ltGanging16x24.Text != "" )
					doThisPrintBlockSize =  true;
				else if ( whichSize == 4 && printSizes.ltGanging18x24.Text != "" )
					doThisPrintBlockSize =  true;

				return ( doThisPrintBlockSize );
			}

			bool DoThisFile( int whichSize, string filename )
			{
				bool doThisFile =  false;

				if ( whichSize == 0 && filename.IndexOf( "_(6x9)" ) != -1 )
					doThisFile =  true;
				else if ( whichSize == 1 && filename.IndexOf( "_(8x12)" ) != -1 )
					doThisFile =  true;
				else if ( whichSize == 2 && filename.IndexOf( "_(12x18)" ) != -1 )
					doThisFile =  true;
				else if ( whichSize == 3 && filename.IndexOf( "_(16x24)" ) != -1 )
					doThisFile =  true;
				else if ( whichSize == 4 && filename.IndexOf( "_(18x24)" ) != -1 )
					doThisFile =  true;

				return ( doThisFile );
			}

			int GetWhichSizeFromPrintFile( MyFileInfo printFile )
			{
				int whichSize	=  -1;

				if ( printFile.Name.IndexOf( "_(6x9)" ) != -1 )
					whichSize =  0;
				else if ( printFile.Name.IndexOf( "_(8x12)" ) != -1 )
					whichSize =  1;
				else if ( printFile.Name.IndexOf( "_(12x18)" ) != -1 )
					whichSize =  2;
				else if ( printFile.Name.IndexOf( "_(16x24)" ) != -1 )
					whichSize =  3;
				else if ( printFile.Name.IndexOf( "_(18x24)" ) != -1 )
					whichSize	=  4;

				return ( whichSize );
			}

			string GetBaseFilenameFromPrintFile( MyFileInfo printFile )
			{
				string lookForString	=  "";

				if ( printFile.Name.IndexOf( "_(6x9)" ) != -1 )
					lookForString	=  "_(6x9)";
				else if ( printFile.Name.IndexOf( "_(8x12)" ) != -1 )
					lookForString =  "_(8x12)";
				else if ( printFile.Name.IndexOf( "_(12x18)" ) != -1 )
					lookForString =  "_(12x18)";
				else if ( printFile.Name.IndexOf( "_(16x24)" ) != -1 )
					lookForString =  "_(16x24)";
				else if ( printFile.Name.IndexOf( "_(18x24)" ) != -1 )
					lookForString	=  "_(18x24)";

				int index	=  printFile.Name.IndexOf( lookForString );

				string	baseName	=  printFile.Name.Substring( 0, index );

				return ( baseName );

			}

			double GetBlockLength( int whichSize )
			{
				double	length	=  0.0;

				try
				{
					switch( whichSize )
					{
						case 0:
							length	=  9*Int32.Parse( printSizes.ltGanging6x9.Text );
							break;
						case 1:
							length	=  12*Int32.Parse( printSizes.ltGanging8x12.Text );
							break;
						case 2:
							length	=  18*Int32.Parse( printSizes.ltGanging12x18.Text );
							break;
						case 3:
							length	=  16*Int32.Parse( printSizes.ltGanging16x24.Text );
							break;
						case 4:
							length	=  18*Int32.Parse( printSizes.ltGanging18x24.Text );
							break;
					}
				}

				catch ( Exception )
				{
					// Ok to ignore.
				}

				return ( length );
			}

			string GetPrintBlocksDirectory( DirectoryInfo currentDir, string saveDir, bool isRelative )
			{
				// Figure out the directory.
				string	directory	=  "";

				if ( saveDir != "" )
				{
					DirectoryInfo	curDirectory;

					if ( !isRelative )
					{
						// Absolute. Make sure it exist and if not create it.
						curDirectory	=  new DirectoryInfo( saveDir );

						if ( !curDirectory.Exists )
							curDirectory.Create();
					}
					else
					{
						string relativeDir =  saveDir;

						curDirectory =  currentDir;

						while( relativeDir.IndexOf( @"..\" ) != -1 )
						{
							curDirectory =  curDirectory.Parent;
							relativeDir  =  relativeDir.Substring( 3 );
						}

						curDirectory	=  curDirectory.CreateSubdirectory( relativeDir );
					}

					directory	=  curDirectory.FullName + @"\";
				}
				else
				{
				}

				return ( directory );
			}

			string GetPrintBlockSaveAsName( int whichSize )
			{
				return ( "Ganged" + GetPrintBlockSaveAsSuffix( whichSize ) + "_x" + filesAr.Count.ToString() );
			}

			string GetPrintBlockSaveAsName( MyFileInfo fileInfo, bool isPrintFile )
			{
				string saveAsPrintBlockName	=  "";

				if ( isPrintFile )
				{
					// Print file.
					int		whichSize	=  GetWhichSizeFromPrintFile( fileInfo );
					string	suffix		=  GetPrintBlockSaveAsSuffix( whichSize );
					string	basename	=  GetBaseFilenameFromPrintFile( fileInfo );

					saveAsPrintBlockName	=  basename + suffix;
				}
				else
				{
					// Image file
					string	suffix		=  GetPrintBlockSaveAsSuffixForAll();
					string	basename	=  fileInfo.Name.Substring( 0, fileInfo.Name.LastIndexOf( "." ) );

					saveAsPrintBlockName	=  basename + suffix;
				}

				return ( saveAsPrintBlockName );
			}

			string GetPrintBlockSaveAsSuffixForAll()
			{
				string printBlockSaveAsSuffix	=  "";
				
				if ( DoThisPrintBlockSize( 0 ) )
					printBlockSaveAsSuffix +=  "_(6x9)_(4x" + printSizes.ltGanging6x9.Text + ")";

				if ( DoThisPrintBlockSize( 1 ) )
					printBlockSaveAsSuffix +=  "_(8x12)_(3x" + printSizes.ltGanging8x12.Text + ")";

				if ( DoThisPrintBlockSize( 2 ) )
					printBlockSaveAsSuffix +=  "_(12x18)_(2x" + printSizes.ltGanging12x18.Text + ")";

				if ( DoThisPrintBlockSize( 3 ) )
					printBlockSaveAsSuffix +=  "_(16x24)_(1x" + printSizes.ltGanging16x24.Text + ")";

				if ( DoThisPrintBlockSize( 4 ) )
					printBlockSaveAsSuffix	+=  "_(18x24)_(1x" + printSizes.ltGanging18x24.Text + ")";

				return ( printBlockSaveAsSuffix );
			}

			string GetPrintBlockSaveAsSuffix( int whichSize )
			{
				string saveAsSuffix	=  "";

				switch( whichSize )
				{
					case 0:
						saveAsSuffix =  "_(6x9)_(4x" + printSizes.ltGanging6x9.Text + ")";
						break;
					case 1:
						saveAsSuffix =  "_(8x12)_(3x" + printSizes.ltGanging8x12.Text + ")";
						break;
					case 2:
						saveAsSuffix =  "_(12x18)_(2x" + printSizes.ltGanging12x18.Text + ")";
						break;
					case 3:
						saveAsSuffix =  "_(16x24)_(1x" + printSizes.ltGanging16x24.Text + ")";
						break;
					case 4:
						saveAsSuffix =  "_(18x24)_(1x" + printSizes.ltGanging18x24.Text + ")";
						break;
					default:
						break;
				}

				return ( saveAsSuffix );
			}

			double AddToPrintBlock( Document printingDoc, Document imageDoc, int whichSize, double startX )
			{
				// Set ruler units to pixels
				ps.theApp.Preferences.RulerUnits = PsUnits.psPixels;

				double w =  imageDoc.Width;
				double h =  imageDoc.Height;

				// Flip vertical pics or horizontal 16x24 & 18x24s.
				if ( whichSize < 3 )
				{
					if ( h > w )
					{
						imageDoc.RotateCanvas( 90.0 );

						w =  imageDoc.Width;
						h =  imageDoc.Height;
					}
				}
				else
				{
					if ( w > h )
					{
						imageDoc.RotateCanvas( 90.0 );

						w =  imageDoc.Width;
						h =  imageDoc.Height;
					}
				}

				// Get the array of bounds for pasting.
				ArrayList	boundsAr	=  GetPrintingBlockBounds( whichSize, w, h, startX );

				// Select all of the base document.
				ps.theApp.ActiveDocument	=  imageDoc;
				imageDoc.Selection.SelectAll();

				// Copy.
				( (ArtLayer) ( imageDoc.ActiveLayer ) ).Copy( false );

				// Paste
				for ( int i= 0;  i< boundsAr.Count;  i++ )
				{
					Photoshopper.Bounds	selBounds	=  (Photoshopper.Bounds) ( boundsAr[ i ] );

					ps.theApp.ActiveDocument	=  printingDoc;
					ps.SelectRectArea( selBounds, PsSelectionType.psReplaceSelection, 0.0 );

					printingDoc.Paste( true );
					printingDoc.Selection.Deselect();
				}

				Photoshopper.Bounds	lastBounds	=  ( (Photoshopper.Bounds) boundsAr[ boundsAr.Count-1 ] );

				double	nextStartX	=  lastBounds[ 2 ];

				return ( nextStartX );
			}

			private void CreateOneImageAllSizesBlocks()
			{
				try
				{
					for ( int i= 0;  i< filesAr.Count;  i++ )
					{
						MyFileInfo	imageInfo	=  filesAr[ i ];

						// Select the file that we're processing.
						IAsyncResult r =  printSizes.BeginInvoke( printSizes.selectFileDelegate, new object[] { imageInfo } );

						// Open document.
						Document	imageDoc	=  ps.OpenDoc( imageInfo.FullName );
								
						// Save this history state.
						HistoryState	historyStateOpened	=  imageDoc.ActiveHistoryState;

						// Save base name.
						int		indexOfExt	=  imageDoc.Name.LastIndexOf( "." );
						string	baseName		=  imageDoc.Name.Substring( 0, indexOfExt );

						DirectoryInfo	dirInfo			=  imageInfo.FileInfo.Directory;
						string			basePathName	=  dirInfo.FullName;

						// What size document do we need?
						double paperWidth = 24 * resolution;
						double	length		=  0.0;

						for ( int whichSize= 0;  whichSize< 5;  whichSize++ )
						{
							length	+=  GetBlockLength( whichSize )*resolution;
						}

						Document printingDoc	=  null;

						if ( length > 0.0 )
						{
							// Create a blank document.
							printingDoc	=  ps.theApp.Documents.Add( length, paperWidth, resolution, "Print Block", 
																	          Photoshop.PsNewDocumentMode.psNewRGB,
																	          Photoshop.PsDocumentFill.psWhite, 1.0, 16, null );
							// Work in ProPhoto.
							ps.ConvertToProfile( ProPhoto );
						}


						double nextStartX	=  0.0;

						// Loop through all the sizes.
						for ( int whichSize= 0;  whichSize< 5;  whichSize++ )
						{
							double	newHeight		=  0;

							// Create document for this print size.
							switch ( whichSize )
							{
								case 0:
								{
									if ( printSizes.chk6x9.Checked )
									{
										newHeight		=  6.0;
									}

									break;
								}

								case 1:
								{
									if ( printSizes.chk8x12.Checked )
									{
										newHeight		=  8.0;
									}

									break;
								}

								case 2:
								{
									if ( printSizes.chk12x18.Checked )
									{
										newHeight		=  12.0;
									}

									break;
								}

								case 3:
								{
									if ( printSizes.chk16x24.Checked )
									{
										newHeight		=  16.0;
									}

									break;
								}

								case 4:
								{
									if ( printSizes.chk18x24.Checked )
									{
										newHeight		=  18.0;
									}

									break;
								}

								default:
								{
									MessageBox.Show( "Did you forget to add for another size?" );
									break;
								}
							}

							if ( newHeight > 0 )
							{
								MyFileInfo printFileInfo =  GetPrintFileInfo( whichSize, imageInfo );

								if ( !( printFileInfo.FileInfo.Exists && printSizes.chkPrintSizesNewOnly.Checked ) )
									PrepareSpecificSize( imageDoc, newHeight, whichSize, imageInfo );
							}
					
							// Create a print block document.
							if ( DoThisPrintBlockSize( whichSize ) )
							{
								// Add to the print block document.
								nextStartX	=  AddToPrintBlock( printingDoc, imageDoc, whichSize, nextStartX );
							}

							// Reset history state.
							ps.theApp.ActiveDocument		=  imageDoc;
							imageDoc.ActiveHistoryState	=  historyStateOpened;
						}

						// Close image document
						imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );

						// Make sure there's only 1 layer.
						printingDoc.Flatten();

						// Rotate?
						if ( printSizes.chkGangingCombineRotate.Checked )
							printingDoc.RotateCanvas( 90.0 );

						// Save it.
						string printBlockPathName		=  GetPrintBlocksDirectory( imagesDirInfo,
							                                                       printSizes.bfdDirectoriesGangedFiles.Directory,
						                                                          printSizes.chkDirectoriesGangedDirectoryRelative.Checked );
						string printBlockSaveAsString	=  GetPrintBlockSaveAsName( imageInfo, false );

						// Save as tiff.
						ps.SaveAsTIFF( printingDoc, printBlockPathName, printBlockSaveAsString );

						// Close the print block document.
						printingDoc.Close( PsSaveOptions.psDoNotSaveChanges );
					}
				}

				catch ( Exception ex )
				{
					MessageBox.Show( ex.Message );
				}
			}

			private void CreateOneImageOneSizeBlocks( MyFileInfosArray printFiles )
			{
				try
				{
					for ( int i= 0;  i< printFiles.Count;  i++ )
					{
						MyFileInfo	printInfo	=  printFiles[ i ];
						int			whichSize	=  GetWhichSizeFromPrintFile( printInfo );

						// What size document do we need?
						double paperWidth	=  24.0*resolution;
						double length		=  GetBlockLength( whichSize )*resolution;

						if ( length > 0 )
						{
							// Create a blank document.
							Document printingDoc =  ps.theApp.Documents.Add( length, paperWidth, resolution, "Print Block",
																			             Photoshop.PsNewDocumentMode.psNewRGB,
																			             Photoshop.PsDocumentFill.psWhite, 1.0, 16, null );
							// Work in ProPhoto.
							ps.ConvertToProfile( ProPhoto );

							// Open document.
							Document imageDoc =  ps.OpenDoc( printFiles[ i ].FullName );

							// Convert to ProPhoto.
							ps.ConvertToProfile( ProPhoto );

							// Add to the print block document.
							AddToPrintBlock( printingDoc, imageDoc, whichSize, 0.0 );

							// Close image document.
							imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );

							// Make sure there's only 1 layer.
							printingDoc.Flatten();

							// Rotate?
							if ( printSizes.chkGangingCombineRotate.Checked )
								printingDoc.RotateCanvas( 90.0 );

							// Save it.
							string printBlockPathName = GetPrintBlocksDirectory( imagesDirInfo,
																								  printSizes.bfdDirectoriesGangedFiles.Directory,
																								  printSizes.chkDirectoriesGangedDirectoryRelative.Checked );
							string printBlockSaveAsString	=  GetPrintBlockSaveAsName( printInfo, true );

							// Save as tiff.
							ps.SaveAsTIFF( printingDoc, printBlockPathName, printBlockSaveAsString );

							// Close the print block document.
							printingDoc.Close( PsSaveOptions.psDoNotSaveChanges );
						}
					}
				}

				catch ( Exception ex )
				{
					MessageBox.Show( ex.Message );
				}
			}

			private void CreateMultImagesOneSizePrintBlocks( MyFileInfosArray printFiles )
			{
				try
				{
					// Loop through all the print files and do a print block for each (if needed).
					for ( int whichSize= 0;  whichSize< 5;  whichSize++ )
					{
						if ( DoThisPrintSize( whichSize ) )
						{
							// What size document do we need?
							double	paperWidth	=  24*resolution;
							double	length		=  GetBlockLength( whichSize )*filesAr.Count*resolution;

							if ( length == 0 )
								continue;

							// Create a blank document.
							Document printingDoc	=  ps.theApp.Documents.Add( length, paperWidth, resolution, "Print Block", 
								                                              Photoshop.PsNewDocumentMode.psNewRGB,
																							 Photoshop.PsDocumentFill.psWhite, 1.0, 16, null );
							// Work in ProPhoto.
							ps.ConvertToProfile( ProPhoto );

							double nextStartX	=  0;

							for ( int i= 0;  i< printFiles.Count;  i++ )
							{
								if ( DoThisFile( whichSize, printFiles[ i ].FullName ) )
								{
									// Open document.
									Document	imageDoc	=  ps.OpenDoc( printFiles[ i ].FullName );

									// Convert to ProPhoto.
									ps.ConvertToProfile( ProPhoto );

									// Add to the print block document.
									nextStartX	=  AddToPrintBlock( printingDoc, imageDoc, whichSize, nextStartX );

									// Close image document.
									imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );
								}
							}

							// Make sure there's only 1 layer.
							printingDoc.Flatten();

							// Rotate?
							if ( printSizes.chkGangingCombineRotate.Checked )
								printingDoc.RotateCanvas( 90.0 );

							// Save it.
							string printBlockPathName		=  GetPrintBlocksDirectory( imagesDirInfo,
																								       printSizes.bfdDirectoriesGangedFiles.Directory,
																									    printSizes.chkDirectoriesGangedDirectoryRelative.Checked );
							string printBlockSaveAsString	=  GetPrintBlockSaveAsName( whichSize );

							// Save as tiff.
							ps.SaveAsTIFF( printingDoc, printBlockPathName, printBlockSaveAsString );

							// Close the print block document.
							printingDoc.Close( PsSaveOptions.psDoNotSaveChanges );
						}
					}
				}

				catch ( Exception ex )
				{
					MessageBox.Show( ex.Message );
				}
			}

			public ArrayList GetPrintingBlockBounds( int whichSize, double w, double h, double startX )
			{
				ArrayList	boundsAr	=  new ArrayList();

				switch( whichSize )
				{
					case 0:	// 6x9, 4 x nRows.
					{
						for ( int i= 0;  i< Int32.Parse( printSizes.ltGanging6x9.Text );  i++ )
						{
							double[]	selCorners1	=  new double[4] { startX+i*w, 0*h, startX+(i+1)*w, 1*h };
							double[]	selCorners2	=  new double[4] { startX+i*w, 1*h, startX+(i+1)*w, 2*h };
							double[]	selCorners3	=  new double[4] { startX+i*w, 2*h, startX+(i+1)*w, 3*h };
							double[]	selCorners4	=  new double[4] { startX+i*w, 3*h, startX+(i+1)*w, 4*h };

							boundsAr.Add( new Photoshopper.Bounds( selCorners1 ) );
							boundsAr.Add( new Photoshopper.Bounds( selCorners2 ) );
							boundsAr.Add( new Photoshopper.Bounds( selCorners3 ) );
							boundsAr.Add( new Photoshopper.Bounds( selCorners4 ) );
						}

						break;
					}

					case 1:	// 8x12, 3 x nRows.
					{
						for ( int i= 0;  i< Int32.Parse( printSizes.ltGanging8x12.Text );  i++ )
						{
							double[]	selCorners1	=  new double[4] { startX+i*w, 0*h, startX+(i+1)*w, 1*h };
							double[]	selCorners2	=  new double[4] { startX+i*w, 1*h, startX+(i+1)*w, 2*h };
							double[]	selCorners3	=  new double[4] { startX+i*w, 2*h, startX+(i+1)*w, 3*h };

							boundsAr.Add( new Photoshopper.Bounds( selCorners1 ) );
							boundsAr.Add( new Photoshopper.Bounds( selCorners2 ) );
							boundsAr.Add( new Photoshopper.Bounds( selCorners3 ) );
						}

						break;
					}

					case 2:	// 12x18, 2 x nRows.
					{
						for ( int i= 0;  i< Int32.Parse( printSizes.ltGanging12x18.Text );  i++ )
						{
							double[]	selCorners1	=  new double[4] { startX+i*w, 0*h, startX+(i+1)*w, 1*h };
							double[]	selCorners2	=  new double[4] { startX+i*w, 1*h, startX+(i+1)*w, 2*h };

							boundsAr.Add( new Photoshopper.Bounds( selCorners1 ) );
							boundsAr.Add( new Photoshopper.Bounds( selCorners2 ) );
						}

						break;
					}

					case 3:	// 16x24, 1 x nRows.
					{
						for ( int i= 0;  i< Int32.Parse( printSizes.ltGanging16x24.Text );  i++ )
						{
							double[]	selCorners1	=  new double[4] { startX+i*w, 0*h, startX+(i+1)*w, 1*h };

							boundsAr.Add( new Photoshopper.Bounds( selCorners1 ) );
						}

						break;
					}

					case 4:	// 18x24, 1 x nRows.
					{
						for ( int i= 0;  i< Int32.Parse( printSizes.ltGanging18x24.Text );  i++ )
						{
							double[]	selCorners1	=  new double[4] { startX+i*w, 0*h, startX+(i+1)*w, 1*h };

							boundsAr.Add( new Photoshopper.Bounds( selCorners1 ) );
						}

						break;
					}
				}

				return ( boundsAr );
			}

			private void Sharpen( Document imageDoc, bool askIfNoLandChannel )
			{
				try
				{
					// Get the channels.
					Photoshop.Channels	channels	=  imageDoc.Channels;

					// Loop through channels and sharpen any land ones.
					bool	foundLandChannel	=  false;

					if ( sharpenLevel != NikSharpenInfo.NikProfileType.None )
					{
						for ( int j= 1;  j<= channels.Count;  j++ )
						{
							Channel	channel	=  channels[ j ];

							string			channelName	=  channel.Name;
							PsChannelType	channelType	=  channel.Kind;

							if ( channelName.ToLower().IndexOf( "land" ) > -1 && channelType == PsChannelType.psMaskedAreaAlphaChannel )
							{
								// Select channel.
								imageDoc.Selection.Load( channel, PsSelectionType.psReplaceSelection, false );

								// Sharpen.
								ps.NikSharpen2( sharpenLevel, autoEyeDistance, paperType, printerResolution );

								foundLandChannel	=  true;
							}
						}
					}

					if ( !foundLandChannel && askIfNoLandChannel )
					{
						// Put up a message so I can do it manually.
						MessageBox.Show( "Sharpen image manually!", "No land channel found" );
					}
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message );
				}
			}
		}

	}
}

