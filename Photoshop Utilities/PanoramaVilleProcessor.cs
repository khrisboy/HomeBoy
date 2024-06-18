using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;

using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;

using ImageAssembler;

namespace PhotoshopUtilities
{
	class PanoramaVilleProcessor
	{
		PanoramaVille					myParent;
		ArrayList						filesAr;
		public int						threadNumber;
		ImageAssembler.Project		project;
		Photoshop.Application		psApp;
		Photoshopper.Photoshopper	ps;

		public PanoramaVilleProcessor( PanoramaVille parent, ArrayList files, int whichThread )
		{
			myParent		=  parent;
			filesAr			=  files;
			threadNumber	=  whichThread;
			psApp			=  null;
			ps				=  null;
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

		private string UpdateDirectory( MyFileInfo info )
		{
			string	directory	=  myParent.GetDirectory( info.FileInfo.Directory, myParent.bfdSaveConvertedFiles.Directory,
                                                          myParent.rbSaveConvertedAbsolute.Checked );

			return ( directory );
		}

		bool NeedsRawProcessing( Dictionary<int,MyFileInfo>	files )
		{
			bool	needsRawProcessing	=  false;

			foreach( KeyValuePair<int,MyFileInfo> keyVal in files )
			{
				MyFileInfo	info	=  keyVal.Value;

				if ( info.FileInfo.Extension.ToLower() == ".nef" || info.FileInfo.Extension.ToLower() == ".crw" )
				{
					needsRawProcessing	=  true;
					break;
				}
			}

			return ( needsRawProcessing );
		}

		private void ConvertFromRaw( string filename )
		{
			PanaVueProject	project	=  new PanaVueProject( filename );

			Dictionary<int,MyFileInfo>	files	=  project.Files;

			if ( NeedsRawProcessing( files ) )
			{
				// Start up Photoshop.
				if ( psApp == null )
					psApp	=  new Photoshop.Application();

				if ( ps == null )
					ps	=  new Photoshopper.Photoshopper( psApp );

				// Don't display dialogs
				psApp.DisplayDialogs =  PsDialogModes.psDisplayNoDialogs;

				Logger.Step( "Convert from RAW" );

				try
				{
					int							nthFile		=  0;
					Dictionary<int,MyFileInfo>	newFiles	=  new Dictionary<int,MyFileInfo>();

					project_Progress( "Converting raw files", 0 );

					foreach( KeyValuePair<int,MyFileInfo> keyVal in files )
					{
						int			index	=  keyVal.Key;
						MyFileInfo	info	=  keyVal.Value;

						project_Progress( "Opening & converting raw file #" + ( ++nthFile ).ToString(), ( 100*( 2*nthFile-1 ) / files.Count / 2 ) );
						
						Document imageDoc	=  ps.OpenDoc( info.FullName );

						project_Progress( "Saving converted raw file #" + ( nthFile ).ToString(), ( 100*( 2*nthFile ) / files.Count / 2 ) );
						
						string	newFilename	=  ps.SaveAsTIFF( imageDoc, UpdateDirectory( info ), "" );

						// Close the file.
						imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );

						newFiles[ index ] =  new MyFileInfo( newFilename );
					}

					project.Files	=  newFiles;

					project.Save( filename );
				}

				catch ( Exception ex )
				{
					// Uh oh!
					Messager.Show( "Converting exception: " + ex.Message );
				}
			}
		}

		private void Process()
		{
			// Start up PanaVue Image Assembler.
			ImageAssembler.Application theImageAssembler =  new ImageAssembler.Application();

			Logger.LogFile	=  "Panoramaville.log";
			Logger.StartSection( "Generating " + filesAr.Count + " panoramas" );

			try
			{
				// Loop through the files.
				foreach ( MyFileInfo fileInfo in filesAr )
				{
					try
					{
						Logger.Start( fileInfo.FullName );

						// Select the file that we're processing.
						IAsyncResult r =  myParent.BeginInvoke( myParent.selectFileDelegate, new object[] { fileInfo, threadNumber } );

						// Unload the current panorama.
						r =  myParent.BeginInvoke( myParent.unloadPanoramaDelegate );

						// Convert from raw if necessary.
						ConvertFromRaw( fileInfo.FullName );

						// Open the file.
						project	=  theImageAssembler.OpenProject( fileInfo.FullName );

						project.Progress		+=  new Progress_ProgressEventHandler( project_Progress );
						project.EndOfExecution	+=  new Progress_EndOfExecutionEventHandler( project_EndOfExecution );

						// Get it's data.
						PanaVueProject	panaVueProject	=  new PanaVueProject( fileInfo );

						// Execute.
						Logger.Step( "Execute" );
						project.StartExecution();

						// Check results code.
						int results	=  project.GetExecutionResult();

						if ( results == -10000 )
						{
						}

						Logger.Step();

						if ( myParent.Processing )
						{
							// Make sure we have a good resulting image path.
							if ( panaVueProject.PathOfFinalImage == "" )
							{
								MyFileInfosArray			files			=  new MyFileInfosArray();
								Dictionary<int,MyFileInfo>	projectFiles	=  panaVueProject.Files;

								string[] strings =  new string[ projectFiles.Count ];

								for ( int i= 0;  i< projectFiles.Count;  i++ )
								{
									files.Add( new MyFileInfo( projectFiles[ i + 1 ].FullName ) );
								}

								panaVueProject.PathOfFinalImage	=  myParent.AutoGenerateResultName( files );
							}

							// Get rid of the existing result image file.
							FileInfo	info	=  new FileInfo( panaVueProject.PathOfFinalImage );

							if ( info.Exists )
							{
								info.Delete();
							}

							// Save the resulting image.
							Logger.Step( "Save panorama" );
							project.SaveResultingImage( panaVueProject.PathOfFinalImage, "" );
							Logger.Step();

							// Save the project.
							project.SaveAs( fileInfo.FullName );

							// Load generated panorama.
							r =  myParent.BeginInvoke( myParent.loadPanoramaDelegate, new object[] { panaVueProject.PathOfFinalImage } );
							
							project	=  null;

							Logger.End();
						}
						else
						{
							// We're supposed to be done. (Canceled?)
							project	=  null;

							break;
						}
					}

					catch( Exception ex )
					{
						// We'll keep looping, but display a message.
						myParent.BeginInvoke( myParent.messagerDelegate,
							                  new Object[] { "While processing " + fileInfo.Name + ": " + ex.Message,
											                 threadNumber } );
					}
				}
			}

			catch( ThreadAbortException )
			{
				// We were aborted. Bummer!
				myParent.BeginInvoke( myParent.messagerDelegate, new Object[] { "Processing thread aborted!", threadNumber} );
			}

			catch( Exception ex )
			{
				// Uh oh!
				myParent.BeginInvoke( myParent.messagerDelegate, new Object[] { "While processing: " + ex.Message, threadNumber } );
			}

			finally
			{
				theImageAssembler =  null;

				Logger.EndSection();

				if ( psApp != null )
					psApp.Quit();
			}
		}

		void project_EndOfExecution( string errorText )
		{
			// Call the form's delegate;
			if ( errorText == "" )
				errorText =  "We're done!";

			myParent.BeginInvoke( myParent.endOfExecutionDelegate, new object[] { errorText, threadNumber } );
		}

		void project_Progress( string text, int percent )
		{
			// Call the form's delegate;
			myParent.BeginInvoke( myParent.progressDelegate, new object[] { text, percent, threadNumber } );
		}

		public void StopExecution()
		{
			myParent.BeginInvoke( myParent.progressDelegate, new object[] { "Canceling execution!", 0, threadNumber } );
			project.StopExecution();
		}
	}
}
