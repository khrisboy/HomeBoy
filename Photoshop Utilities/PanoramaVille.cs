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
	public partial class PanoramaVille : WindowForm
	{
		#region Members

		PanaVueProject					theProject;
		bool							processing			=  false;
		Bitmap							thePanoramaImage	=  null;
		System.Drawing.Size				orgPanoBoxSize;
		Point							orgPanoCenter;
		bool							dontUpdateProject	=  false;
		List<Thread>					processThreads		=  null;
		List<PanoramaVilleProcessor>	theProcessors		=  null;

		public delegate void SelectFileDelegate( MyFileInfo info, int threadNumber );
		public SelectFileDelegate selectFileDelegate;

		public delegate void LoadPanoramaDelegate( string filename );
		public LoadPanoramaDelegate loadPanoramaDelegate;

		public delegate void UnloadPanoramaDelegate();
		public UnloadPanoramaDelegate unloadPanoramaDelegate;

		public delegate void RefreshDelegate();
		public RefreshDelegate refreshDelegate;

		public delegate void ResetFormDelegate();
		public ResetFormDelegate resetFormDelegate;

		public delegate void ProgressDelegate( string text, int percent, int threadNumber );
		public ProgressDelegate progressDelegate;

		public delegate void EndOfExecutionDelegate( string text, int threadNumber );
		public EndOfExecutionDelegate endOfExecutionDelegate;

		public delegate void MessagerDelegate( string text, int threadNumber  );
		public MessagerDelegate messagerDelegate;

		public delegate void MyEventHandlerDelegate( Object sender, EventArgs e );
		public MyEventHandlerDelegate onProcessingComplete;
		
		#endregion

		public PanoramaVille()
		{
			InitializeComponent();

			this.flvProjectFiles.ValidFileTypes = new List<string> {
																	".tif",
																	".psd",
																	".psb",
																	".jpg",
																	".nef",
																	".crw",
																	".bmp",
																	".png"};

			theProject	=  new PanaVueProject();

			selectFileDelegate		=  new SelectFileDelegate( SelectFile );
			loadPanoramaDelegate	=  new LoadPanoramaDelegate( LoadPanorama );
			unloadPanoramaDelegate	=  new UnloadPanoramaDelegate( UnloadPanorama );
			refreshDelegate			=  new RefreshDelegate( RefreshMe );
			resetFormDelegate		=  new ResetFormDelegate( ResetForm );
			onProcessingComplete	=  new MyEventHandlerDelegate( Form_ProcessingComplete );
			progressDelegate		=  new ProgressDelegate( Progress );
			endOfExecutionDelegate	=  new EndOfExecutionDelegate( EndOfExecution );
			messagerDelegate		=  new MessagerDelegate( DisplayStatusText );

			// Set up the timer for Messager.
			theTimer.Interval	 =  10000;
			theTimer.Tick		+=  TimerEventProcessor;

			// Display exceptions here.
			Messager.DisplayObject	=  stStatus;
			Messager.Timer			=  theTimer;
		}

		private int NumThreads
		{
			get
			{
				int numThreads	=  1;
				
				try
				{
					numThreads	=  Math.Max( 1, (int) lsThreads.Value );
				}

				catch( Exception )
				{
				}

				return ( numThreads );
			}
		}

		private void EnableDisable()
		{
			flvProjects.Enabled			=  !processing;
			flvProjectFiles.Enabled		=  !processing;
			btnCreate.Enabled				=  !processing;
			lsThreads.Enabled				=  !processing;

			grpLensDefinition.Enabled		=  !processing;
			grpSave.Enabled					=  !processing;
			grpOptionsImage.Enabled			=  !processing;
			grpOptionsStitching.Enabled	=  !processing;
			grpOrientation.Enabled			=  !processing;
			grpCrop.Enabled					=  !processing;

			chkDisplayPanorama.Enabled		=  true;	// Always

			if ( !processing )
				btnProcess.Enabled	=  true;
		}

		public bool Processing
		{
			get { return ( processing ); }
		}

		void SelectFile( MyFileInfo info, int threadNumber )
		{
			dontUpdateProject	=  true;

			flvProjects.Select( info );

			dontUpdateProject	=  false;
		}

		void UnloadPanorama()
		{
			thePanoramaImage	=  null;
			thePanoBox.Image	=  null;
		}

		void LoadPanorama( string filename )
		{
			try
			{
				if ( !chkDisplayPanorama.Checked )
					return;

				if ( thePanoBox.ImageLocation == filename )
					return;

				thePanoBox.WaitOnLoad =  false;

				thePanoBox.LoadAsync( filename );
			}

			catch( Exception ex )
			{
				Messager.Show( ex.Message );
			}
		}

		void thePanoBox_LoadProgressChanged( object sender, ProgressChangedEventArgs e )
		{
			Progress( "Loading panorama", e.ProgressPercentage, 0 );
		}

		private void thePanoBox_LoadCompleted( object sender, AsyncCompletedEventArgs e )
		{
			try
			{
				// We want to resize the PictureBox...
				thePanoBox.SuspendLayout();

				thePanoramaImage =  (Bitmap) thePanoBox.Image;

				System.Drawing.Size	theBoxSize	 =  thePanoBox.Size;
				System.Drawing.Size theImageSize =  thePanoramaImage.Size;

				// If a vertical panorama rotate to fit.
				if ( theImageSize.Height > theImageSize.Width )
				{
					thePanoramaImage.RotateFlip( RotateFlipType.Rotate270FlipNone );

					theImageSize =  thePanoramaImage.Size;
				}

				double ratioImage =  (double) theImageSize.Width / (double) theImageSize.Height; ;
				double ratioBox   =  (double) orgPanoBoxSize.Width / (double) orgPanoBoxSize.Height;

				if ( ratioBox < ratioImage )
				{
					// Original Width, reduced height
					int newBoxHeight = (int) ( (double) orgPanoBoxSize.Width / ratioImage );

					thePanoBox.Width	=  orgPanoBoxSize.Width;
					thePanoBox.Height	=  newBoxHeight;
				}
				else
				{
					// Original height, reduced width.
					int newBoxWidth =  (int) ( (double) orgPanoBoxSize.Height * ratioImage );

					thePanoBox.Width  =  newBoxWidth;
					thePanoBox.Height =  orgPanoBoxSize.Height;
				}

				// Resize.
				thePanoBox.Left	=  orgPanoCenter.X - ( thePanoBox.Width/2 );
				thePanoBox.Top	=  orgPanoCenter.Y - ( thePanoBox.Height/2 );

				thePanoBox.ResumeLayout();
			}

			catch ( Exception ex )
			{
				Messager.Show( ex.Message );
			}

			RefreshMe();

			Progress( "", 0, 0 );
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

		void Progress( string msg, int percent, int threadNumber )
		{
			if ( percent > -1 )
			{
				theProgressBar.Value	=  percent;
				mProgressBar.SetValue( percent, threadNumber );

				if ( stStatus.Text != msg )
				{
					stStatus.Text =  msg;
					stStatus.Refresh();
				}
			}
			else
			{
				theProgressBar.Value	=  0;
				mProgressBar.SetValue( 0, threadNumber );
				stStatus.Text			=  msg;
			}
		}

		private void TimerEventProcessor( Object myObject, EventArgs myEventArgs )
		{
			stStatus.Text =  "";

			theTimer.Stop();
		}

		private void EndOfExecution( string errMessage, int threadNumber )
		{
			theProcessors[ threadNumber ] =  null;

			bool	allDone	=  true;

			for ( int i= 0;  i< processThreads.Count;  i++ )
			{
				if ( theProcessors[ i ] != null )
				{
					allDone =  false;
					break;
				}
			}

			if ( allDone )
			{
				theProgressBar.Value	=  0;
				mProgressBar.SetValue( 0, threadNumber );
				stStatus.Text			=  errMessage;

				RefreshMe();
			}
		}

		private void DisplayStatusText( string message, int threadNumber )
		{
			Messager.Show( message );
			RefreshMe();
		}

		private void Form_ProcessingComplete( Object sender, System.EventArgs e )
		{
			PanoramaVilleProcessor	theProcessor	=  (PanoramaVilleProcessor) sender;

			bool	allDone	=  true;

			for ( int i= 0;  i< NumThreads;  i++ )
			{
				PanoramaVilleProcessor	processor	=  theProcessors[ i ];

				if ( theProcessor == processor )
				{
					theProcessors[ i ]	=  null;
				}
			}

			Progress( "", 0, theProcessor.threadNumber );

			if ( allDone )
			{
				AllThreadsDone();
			}
		}

		void AllThreadsDone()
		{
			processing =  false;

			stStatus.Text	=  "";

			mProgressBar.Reset();

			ResetForm();
		}

		private void btnCreate_Click( object sender, EventArgs e )
		{
			UpdateAndCreate();
		}

		public string GetDirectory( DirectoryInfo imagesDirectory, string targetDirectory, bool isAbsolute )
		{
			// Figure out the directory.
			string	directory	=  "";

			if ( targetDirectory != null && targetDirectory != "" )
			{
				DirectoryInfo	curDirectory;

				if ( isAbsolute )
				{
					// Absolute. Make sure it exist and if not create it.
					curDirectory	=  new DirectoryInfo( targetDirectory );

					if ( !curDirectory.Exists )
						curDirectory.Create();
				}
				else
				{
                    // Relative.
					string relativeDir =  targetDirectory;

					curDirectory =  imagesDirectory;

					while( relativeDir.IndexOf( @"..\" ) != -1 )
					{
						curDirectory =  curDirectory.Parent;
						relativeDir  =  relativeDir.Substring( 3 );
					}

					curDirectory	=  curDirectory.CreateSubdirectory( relativeDir );
				}

				directory	=  curDirectory.FullName + @"\";
			}

			return ( directory );
		}

		string GetAutoGenerateBaseName( MyFileInfosArray projectFiles )
		{
			string	autoBaseName	=  "";

			string	saveDir	=  bfdSaveSaveDirectory.Directory;

 			// Figure out the directory. (Because the specification could be relative or absolute,
            // pass in the path for the 1st panorama image if it is relative).
			string directory =  GetDirectory( projectFiles[ 0 ].FileInfo.Directory, bfdSaveSaveDirectory.Directory,
                                              rbSaveAbsolute.Checked );

			// Get the pattern of the files.
			string pattern =  @"(^.+_[R]*)([0-9]+)((_.+)*\.[a-zA-Z]+$)";

			Regex	rex	=  new Regex( pattern );

			// Strip off the directory of the first and last files.
			string filenameFirst =  projectFiles[ 0 ].Name;
			string filenameLast  =  projectFiles[ projectFiles.Count-1 ].Name;

			Match	firstMatch	=  rex.Match( filenameFirst );
			Match	lastMatch	=  rex.Match( filenameLast );

			string	baseName	=  directory + firstMatch.Groups[ 1 ].Captures[ 0 ].Value;
			string	firstNum	=  firstMatch.Groups[ 2 ].Captures[ 0 ].Value;
			string	lastNum	=  lastMatch.Groups[ 2 ].Captures[ 0 ].Value;
			string	suffix	=  firstMatch.Groups[ 4 ].Captures.Count == 1 ?  firstMatch.Groups[ 4 ].Captures[ 0 ].Value : "";

			int first	=  Int32.Parse( firstNum );
			int last	=  Int32.Parse( lastNum );

			if ( first < last )
				autoBaseName	=  baseName + firstNum + "-" + lastNum + suffix;
			else
				autoBaseName	=  baseName + lastNum + "-" + firstNum + suffix;

			return ( autoBaseName );
		}

		private string AutoGenerateProjectName()
		{
			string	projectName	=  GetAutoGenerateBaseName( flvProjectFiles.Files ) + ".xia";

			return ( projectName );
		}
		
		public string AutoGenerateResultName( MyFileInfosArray projectFiles )
		{
			string projectName =  GetAutoGenerateBaseName( projectFiles ) + ".tif";

			return ( projectName );
		}

		private void UpdateAndCreate()
		{
			try
			{
				// Load the project file.
				bool	existsAlready	=  theProject.Open( bfCurrentProject.FileName );

				if ( !existsAlready )
				{
					// Initialize a new one.
					theProject.Initialize();
				}

				if ( chkAutoGenerateProjectName.Checked )
				{
					theProject.PathOfFinalImage	=  AutoGenerateResultName( flvProjectFiles.Files );
				}
				else
				{
					MyFileInfo	info = new MyFileInfo( bfCurrentProject.FileName );

					string	baseName	=  bfCurrentProject.FileName.Substring( 0, bfCurrentProject.FileName.LastIndexOf( info.FileInfo.Extension )+1 );
					
					theProject.PathOfFinalImage =  baseName + "tif";
				}

				// Project type.
				theProject.Type	=  rbOrientationHorizontal.Checked ?  PanaVueProject.ProjectType.PanoramaRow : PanaVueProject.ProjectType.PanoramaCol;

				// Files.
				Dictionary<int, MyFileInfo>	files	=  new Dictionary<int,MyFileInfo>();
				int									index	=  0;

				foreach( MyFileInfo info in flvProjectFiles.Files )
				{
					files.Add( ++index, info );
				}

				theProject.Files	=  files;

				// Stitch type.
				int	numFlags	=  0;

				if ( rbOptionsStitchingAutomatic.Checked )
					numFlags	=  0;
				else if ( rbOptionsStitchingManual1Flag.Checked )
					numFlags	=  1;
				else if ( rbOptionsStitchingManual3or6Flags.Checked )
					numFlags	=  3;
				else if ( rbOptionsStitchingManual5or8Flags.Checked )
					numFlags	=  5;

				theProject.NumberOfFlags	=  numFlags;

				// Image options.
				theProject.ImageBlending =  (Double) udOptionsImageBlending.Value / 100.0;

				theProject.AdjustColors			=  chkOptionsImageAdjustColors.Checked;
				theProject.Wrap360				=  chkOptionsImage360Wrapping.Checked;
				theProject.AntiAliasing			=  chkOptionsImageAntiAliasing.Checked;
				theProject.Sharpen				=  chkOptionsImageSharpen.Enabled && chkOptionsImageSharpen.Checked;
				theProject.AutoCrop				=  chkOptionsImageAutoCrop.Checked;
				theProject.AlignHorizontally	=  chkOptionsImageAlign.Checked;

				// Lens.
				if ( ltLensDefinitionFocalLength.Text.Length > 0 )
				{
					double	focalLength =  Double.Parse( ltLensDefinitionFocalLength.Text );

					theProject.LensFocalLength =  chkLensDefinitionDX.Checked ?  1.5*focalLength : focalLength;
					theProject.LensName			=  theProject.LensFocalLength.ToString( "##.#" ) + "mm";
				}
				else if ( ltLensDefinitionLensName.Text.Length > 0 )
				{
					theProject.LensName =  ltLensDefinitionLensName.Text;
				}
				else
				{
				}

				// Project filename.
				string	projectFilename	=  "";

				//***if (!existsAlready && chkAutoGenerateProjectName.Checked)
				if ( chkAutoGenerateProjectName.Checked || !existsAlready )
					projectFilename	=  AutoGenerateProjectName();
				else
					projectFilename	=  bfCurrentProject.FileName;

				// Save project.
				theProject.Save( projectFilename );

				// Add to list.
				flvProjects.AddFile( projectFilename );

				// And select.
				flvProjects.Select( new MyFileInfo( projectFilename ) );
			}

			catch( Exception e )
			{
				Messager.Show( "Creating new project:  " + e.Message );
			}
		}

		private void LoadProject( PanaVueProject project )
		{
			// Now fill in the fields.
			Dictionary<int, MyFileInfo> files =  project.Files;

			// Project files.
			flvProjectFiles.Clear();

			flvProjectFiles.Sorting	=  FilesListView.SortDirection.None;

			string[] strings	=  new string[ files.Count ];

			for ( int i= 0;  i< files.Count;  i++ )
			{
				strings[ i ] =  files[ i+1 ].FullName;
			}

			flvProjectFiles.AddFiles( strings );

			// Project type.
			if ( project.Type == PanaVueProject.ProjectType.PanoramaRow )
				rbOrientationHorizontal.Checked	=  true;
			else if ( project.Type == PanaVueProject.ProjectType.PanoramaCol )
				rbOrientationVertical.Checked =  true;

			// Stitch type.
			switch( project.NumberOfFlags )
			{
				case 0:
					rbOptionsStitchingAutomatic.Checked			=  true;
					break;
				case 1:
					rbOptionsStitchingManual1Flag.Checked		=  true;
					break;
				case 3:
				case 6:
					rbOptionsStitchingManual3or6Flags.Checked =  true;
					break;
				case 5:
				case 8:
					rbOptionsStitchingManual5or8Flags.Checked =  true;
					break;
				default:
					break;
			}

			// Image options.
			udOptionsImageBlending.Value	=  (Decimal) ( 100.0*project.ImageBlending );
			 
			chkOptionsImageAdjustColors.Checked		=  project.AdjustColors;
			chkOptionsImage360Wrapping.Checked		=  project.Wrap360;
			chkOptionsImageSharpen.Checked			=  project.Sharpen;
			chkOptionsImageAntiAliasing.Checked		=  project.AntiAliasing;
			chkOptionsImageAutoCrop.Checked			=  project.AutoCrop;
			chkOptionsImageAlign.Checked				=  project.AlignHorizontally;

			// Lens.
			ltLensDefinitionLensName.Text		=  project.LensName;
			ltLensDefinitionFocalLength.Text	=  project.LensFocalLength.ToString( "#0.00" );

			// Cropping.
			chkCrop.Checked	=  project.CropMargins;
		}

		private void chkOptionsImageAntiAliasing_CheckedChanged( object sender, EventArgs e )
		{
			chkOptionsImageSharpen.Enabled	=  chkOptionsImageAntiAliasing.Checked;
		}

		private void rbOptionsStitchingAutomatic_CheckedChanged( object sender, EventArgs e )
		{
			btnOptionsStitchingSearchArea.Enabled	=  rbOptionsStitchingAutomatic.Checked;
		}

		private void PanoramaVille_Load( object sender, EventArgs e )
		{
			chkOptionsImageAntiAliasing_CheckedChanged( sender, e );
			rbOptionsStitchingAutomatic_CheckedChanged( sender, e );
			chkCrop_CheckedChanged( sender, e );

			orgPanoBoxSize	=  thePanoBox.Size;

			orgPanoCenter	=  new Point( thePanoBox.Left + ( thePanoBox.Width / 2 ),
				                          thePanoBox.Top + ( thePanoBox.Height / 2 ) );

			mProgressBar.NumProgressBars	=  0;
		}

		MyFileInfosArray Files
		{
			get
			{
				return ( flvProjects.Files );
			}
		}

		void btnProcess_Click( object sender, EventArgs e )
		{
			Photoshop.Application app = new Photoshop.Application();

			MergeToPanorama mtp =  new MergeToPanorama( app );

			mtp.Run();
		}

// 		private void btnProcess_Click( object sender, EventArgs e )
// 		{
// 			try
// 			{
// 				if ( !processing )
// 				{
// 					// Get the list of files into an array.
// 					ArrayList filesAr =  Files;
// 
// 					if ( filesAr.Count == 0 )
// 					{
// 						throw new Exception( "There must be at least 1 file to process!" );
// 					}
// 
// 					// Multiple threads?
// 					int numThreads =  Math.Min( NumThreads, filesAr.Count );
// 
// 					List<ArrayList>	files	=  new List<ArrayList>( numThreads );
// 
// 					processThreads	=  new List<Thread>( numThreads );
// 					theProcessors	=  new List<PanoramaVilleProcessor>( numThreads );
// 
// 					for ( int i= 0;  i< numThreads;  i++ )
// 					{
// 						files.Add( new ArrayList() );
// 					}
// 
// 					for ( int i= 0;  i< filesAr.Count;  i++ )
// 					{
// 						MyFileInfo fileInfo	=  (MyFileInfo) filesAr[ i ];
// 
// 						files[ (i+1) % numThreads ].Add( fileInfo );
// 					}
// 
// 					mProgressBar.NumProgressBars	=  NumThreads;
// 
// 					for ( int i= 0;  i< numThreads;  i++ )
// 					{
// 						// Create the thread.
// 						PanoramaVilleProcessor	theProcessor	=  new PanoramaVilleProcessor( this, files[ i ], i );
// 						Thread					processThread	=  new Thread( new ThreadStart( theProcessor.ThreadProc ) );
// 						
// 						processThreads.Add( processThread );
// 						theProcessors.Add( theProcessor );
// 					}
// 
// 					// We're processing!
// 					processing =  true;
// 
// 					ResetForm();
// 
// 					// We can start the threads now.
// 					for ( int i= 0;  i< numThreads;  i++ )
// 					{
// 						processThreads[ i ].Start();
// 					}
// 				}
// 				else
// 				{
// 					// Set processing flag to false.
// 					processing =  false;
// 
// 					// Disable the button.
// 					btnProcess.Enabled=  false;
// 
// 					if ( theProcessors != null )
// 					{
// 						int numThreads =  Math.Min( NumThreads, Files.Count );
// 						
// 						for ( int i= 0;  i< numThreads;  i++ )
// 						{
// 							// Stop execution.
// 							theProcessors[ i ].StopExecution();
// 
// 							// Abort the processing threads.
// 							processThreads[ i ].Abort();
// 							processThreads[ i ].Join();
// 						}
// 					}
// 
// 					ResetForm();
// 				}
// 			}
// 
// 			catch( Exception ex )
// 			{
// 				Messager.Show( ex.Message, "Processing" );
// 			}
// 		}

		private void flvProjects_OnMySelectionChanged( object sender, EventArgs e )
		{
			try
			{
				// Load up the info for the currently selected project.
				MyFileInfosArray filesAr =  flvProjects.Selected;

				if ( filesAr.Count > 0 )
				{
					bfCurrentProject.FileName =  filesAr[ 0 ].FullName;

					PanaVueProject project =  new PanaVueProject();

					project.Open( filesAr[ 0 ].FullName );

					LoadProject( project );

					// Display the panorama?
					if ( !dontUpdateProject && chkDisplayPanorama.Checked )
					{
						MyFileInfo	info	=  new MyFileInfo( project.PathOfFinalImage );

						if ( info.FileInfo.Exists )
							LoadPanorama( project.PathOfFinalImage );
						else
							UnloadPanorama();
					}
				}
			}

			catch( Exception ex )
			{
				Messager.Show( ex.Message );
			}
		}

		private void chkAutoGenerateProjectName_CheckedChanged( object sender, EventArgs e )
		{
			// Enable/disable the project name field.
			bfCurrentProject.Enabled	=  chkAutoGenerateProjectName.Checked;
		}

		private void rbCropTypeCheckedChanged( object sender, EventArgs e )
		{
			// Enable/disable
			udCropTop.Enabled		=  !rbCropLeftAndRight.Checked;
			udCropBottom.Enabled	=  !rbCropLeftAndRight.Checked;
			udCropLeft.Enabled		=  !rbCropTopAndBottom.Checked;
			udCropRight.Enabled		=  !rbCropTopAndBottom.Checked;
		}

		private void chkCrop_CheckedChanged( object sender, EventArgs e )
		{
			udCropTop.Enabled		=  chkCrop.Checked;
			udCropBottom.Enabled	=  chkCrop.Checked;
			udCropLeft.Enabled		=  chkCrop.Checked;
			udCropRight.Enabled		=  chkCrop.Checked;

			rbCropLeftAndRight.Enabled	=  chkCrop.Checked;
			rbCropTopAndBottom.Enabled	=  chkCrop.Checked;
			rbCropUnique.Enabled		=  chkCrop.Checked;

			// If we're now enabled then defer to what type of cropping is active.
			if ( chkCrop.Checked )
				rbCropTypeCheckedChanged( sender, e );
		}

		private void btnReTarget_Click( object sender, EventArgs e )
		{
			if ( rbTypeSourceDrive.Checked )
			{
				ReTargetSourceDrive();
			}
			else
			{
				ReTargetSourcePath();
			}
		}

		void ReTargetSourcePath()
		{
			try
			{
				if ( !processing )
				{
					// Get the list of files into an array.
					if ( filesListView1.Files.Count == 0 )
					{
						throw new Exception( "There must be at least 1 project file to process!" );
					}
					else if ( edtReplacementValue.Text.Length == 0 )
					{
						throw new Exception( "Specify a replacement path" );
					}

					foreach ( MyFileInfo info in filesListView1.Files )
					{
						// Select.
						flvProjects.Select( info );

						// Load the project file.
						PanaVueProject project =  new PanaVueProject( info );

						// Files.
						Dictionary<int, MyFileInfo> projectsFiles =  project.Files;

						foreach ( KeyValuePair<int, MyFileInfo> pair in projectsFiles )
						{
							MyFileInfo fileInfo =  pair.Value;

							fileInfo.ReplacePath( edtReplacementValue.Text );

							if ( chkSwitchToNEFs.Checked )
							{
								fileInfo.ReplaceExtension( "nef" );
							}
						}

						// Update the project.
						theProject.Files =  projectsFiles;

						// Save project.
						theProject.Save( info.FullName );
					}
				}
			}

			catch ( Exception ex )
			{
				Messager.Show( ex.Message, "Processing" );
			}
		}

		void ReTargetSourceDrive()
		{
			try
			{
				if ( !processing )
				{
					// Get the list of files into an array.
					if ( filesListView1.Files.Count == 0 )
					{
						throw new Exception( "There must be at least 1 project file to process!" );
					}
					else if ( edtReplacementValue.Text.Length != 1 )
					{
						throw new Exception( "Specify a drive letter to replace with (like 'D', 'F', etc.)" );
					}

					foreach ( MyFileInfo info in filesListView1.Files )
					{
						// Select.
						flvProjects.Select( info );

						// Load the project file.
						PanaVueProject project =  new PanaVueProject( info );

						// Files.
						Dictionary<int,MyFileInfo> projectsFiles =  project.Files;

						foreach ( KeyValuePair<int,MyFileInfo> pair in projectsFiles )
						{
							MyFileInfo fileInfo =  pair.Value;
							string   directory	=  fileInfo.FileInfo.DirectoryName;
							string[] split		=  directory.Split( ':' );

							if ( split.Length == 2 )
							{
								directory =  edtReplacementValue.Text + ":" + split[ 1 ];

								fileInfo.ReplacePath( directory );
							}
						}

						// Update the project.
						theProject.Files =  projectsFiles;

						// Save project.
						theProject.Save( info.FullName );
					}
				}
			}

			catch ( Exception ex )
			{
				Messager.Show( ex.Message, "Processing" );
			}
		}
	}
}
