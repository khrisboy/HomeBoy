using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ps = Photoshop;

using MyClasses;
using MyControls;

namespace PhotoshopUtilities
{
	public partial class FileChecker : MyWindowsForm
	{
		enum ProcessingModeType
		{
			Process,
			End,
			Cancel,
			Pause,
			Resume,
		};

		#region Constants

		const string CHECK_FILES =  "Check Files";
		const string CANCEL      =  "Cancel";
		const string PAUSE       =  "Pause";
		const string RESUME      =  "Resume";

		const int WAIT_DELAY =  333;

		#endregion Constants

		#region Members

		MyFileInfosArray m_badFiles;
		MyFileInfosArray m_filesAr;

		bool m_isProcessing        =  false;
		int m_progress             =  0;
		int m_distanceFromRight    =  0;
		bool m_isPause             =  false;
		bool m_isResume            =  false;
		int m_startAt              =  0;
		MyFileInfo m_latestBadFile =  null;

		string[] m_dots = { ".", "..", "...", "....", ".....", "......", ".......", "........", ".........", "..........", };

		#endregion Members

		#region Constructors

		public FileChecker()
		{
			InitializeComponent();

			// Set up the BackgroundWorker.
			backgroundWorker1.WorkerSupportsCancellation =  true;
			backgroundWorker1.WorkerReportsProgress      =  true;

			backgroundWorker1.DoWork             +=  backgroundWorker1_DoWork;
			backgroundWorker1.RunWorkerCompleted +=  backgroundWorker1_RunWorkerCompleted;

			ResizeProgressBar();

			// Specify the file types.
			theFilesGatherer.FilesFilter =  "Raw Files (*.NEF;*.CRW)|*.NEF;*.CRW|" +
											"Nikon Raw Files (*.NEF)|*.NEF|" +
											"Canon Raw Files (*.CRW)|*.CRW|" +
											"JPEG Files (*.JPG)|*.JPG|" +
											"Raw & JPEG Files (*.NEF;*.CRW;*.JPG)|*.NEF;*.CRW;*.JPG|" +
											"All Image Files (*.TIF;*.PSD;*.PSB;*.JPG;*.NEF;*.CRW;*.BMP;*.PNG)|*.TIF;*.PSD;*.PSB;*.JPG;*.NEF;*.CRW;*.BMP;*.PNG";

			// Get distance of FilesGatherer from the right side.
			m_distanceFromRight =  this.Width - ( theFilesGatherer.Left+theFilesGatherer.Width);
		}

		#endregion Constructors

		#region Methods

		/// <summary>
		/// For each 'mode' update various flags & button texts, and
		/// enable/disable and set visibility of controls as necessary.
		/// </summary>
		/// <param name="type">The processing mode type.</param>
		private void DoEnableDisableEtc( ProcessingModeType type )
		{
			if ( type == ProcessingModeType.Process )
			{
				m_progress     =  0;
				m_isProcessing =  true;
				m_isPause      =  false;
				m_isResume     =  false;

				btnRunCancel.Text   =  CANCEL;
				btnPauseResume.Text =  PAUSE;

				btnPauseResume.Visible =  true;
				btnCopy.Enabled        =  false;
			}
			else if ( type == ProcessingModeType.End || type == ProcessingModeType.Cancel )
			{
				m_isProcessing =  false;
				m_isPause      =  false;
				m_isResume     =  false;
				m_startAt      =  0;

				btnRunCancel.Text =  CHECK_FILES;

				btnPauseResume.Visible =  false;
				btnCopy.Enabled        =  lbBadFiles.Items.Count > 0 ?  true : false;
			}
			else if ( type == ProcessingModeType.Pause )
			{
				m_isResume =  false;

				btnPauseResume.Text =  RESUME;
				btnCopy.Enabled     =  lbBadFiles.Items.Count > 0 ?  true : false;
			}
			else if ( type == ProcessingModeType.Resume )
			{
				m_isResume =  false;
				m_isPause      =  false;
				m_isProcessing =  true;

				btnRunCancel.Text   =  CANCEL;
				btnPauseResume.Text =  PAUSE;

				btnCopy.Enabled =  false;
			}
		}

		/// <summary>
		/// Handles the button clicks.
		/// Sets up processing (from initial or resume), or handles Pause, or handles Cancel.
		/// </summary>
		private void DoCheckFiles()
		{
			// Set focus to Bad Files?
			// (This seems to fix issue with clicking on a different button actually clicking on the one clicked before - first time...)
			lblBadFiles.Focus();

			if ( !m_isProcessing || m_isResume )
			{
				// Processing (Check Files) or resuming processing.
				if ( !m_isResume )
				{
					// Clear the bad files list.
					lbBadFiles.Items.Clear();
		
					// Transfer files info.
					m_filesAr =  theFilesGatherer.Files;
	
					// Set up the progress bar.
					theProgressBar.Minimum =  0;
					theProgressBar.Maximum =  m_filesAr.Count;
					theProgressBar.Step =  1;
					theProgressBar.Value   =  0;
	
					DoEnableDisableEtc( ProcessingModeType.Process );
				}
				else
				{
					DoEnableDisableEtc( ProcessingModeType.Resume );
				}
	
				// Run the actual file checking operation asynchronously.
				backgroundWorker1.RunWorkerAsync();

				// Kick off an async method to do the status updating.
				DoStatusUpdateAsync();
			} 
			else if ( m_isPause )
			{
				// Canceled while paused.
				DoEnableDisableEtc( ProcessingModeType.Cancel );
			}
			else
			{
				// Cancelled while running.
				backgroundWorker1.CancelAsync();
			}
		}

		/// <summary>
		/// Async method to do the status updating on the main (gui) thread.
		/// (Requires .NET 4.5)
		/// </summary>
		private async void DoStatusUpdateAsync()
		{
			int whichDots =  0;

			while ( backgroundWorker1.IsBusy )
			{
				if ( theProgressBar.Value != m_progress )
				{
					theProgressBar.Value =  m_progress;
					theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed", m_progress, m_filesAr.Count );

					whichDots =  0;
				}
				else
				{
					// Time to update the status message.
					theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed{2}", m_progress, m_filesAr.Count, m_dots[ whichDots ] );

					// Add a dot. Reset to none if at 10.
					whichDots++;

					if ( whichDots >= 10 )
					{
						whichDots =  0;
					}

					// 'New' bad file?
					if ( m_latestBadFile != null )
					{
						// Yes. Add it to the display of bad files.
						MyFileInfo info =  m_latestBadFile;
						m_latestBadFile =  null;

						DisplayBadFile( info );
					}
				}

				await Task.Delay( WAIT_DELAY );
			}

			// Last status/progress update.
			theProgressBar.Value =  m_progress;

			theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed, {2} bad files", m_progress, m_filesAr.Count, m_badFiles.Count );
		}

		/// <summary>
		/// Displays the list of bad files.
		/// </summary>
		private void DisplayBadFiles()
		{
			// Display the bad files.
			if ( m_badFiles != null && m_badFiles.Count > 0 )
			{
				lbBadFiles.Items.AddRange( m_badFiles.ToArray() );
			}
		}

		/// <summary>
		/// Add a bad file to the display of bad files.
		/// </summary>
		private void DisplayBadFile( MyFileInfo file )
		{
			// Display the bad files.
			lbBadFiles.Items.Add( file );
		}

		/// <summary>
		/// Does the actual file checking process.
		/// Uses Photoshop...
		/// </summary>
		/// <param name="e"><c>DoWorkEventArgs</c> for kicking off the <c>BackgroundWorker</c> async thread. So we can update if necessary for possible checking later.</param>
		private void ProcessFiles( DoWorkEventArgs e )
		{
			try
			{
				e.Result =  false;

				if ( m_badFiles == null )
				{
					m_badFiles =  new MyFileInfosArray();
				} 
				else if ( !m_isResume )
				{
					// Not resuming so clear the bad files.
					m_badFiles.Clear();
				}
	
				// Fire up Photoshop (and some setup for raw).
				ps.Application app =  new ps.Application();
	
				app.DisplayDialogs =  Photoshop.PsDialogModes.psDisplayNoDialogs;
	
				var rawOpenOptions =  new ps.CameraRAWOpenOptions();
	
				rawOpenOptions.Contrast =  1;
				rawOpenOptions.Exposure =  0.50;
	
				// Set up some parameters.
				int numberToProcess =  m_filesAr.Count;
				int numberProcessed =  m_startAt;
	
				// Loop through all of the files.
				for ( int i= m_startAt;  i< m_filesAr.Count;  i++ )
				{
					MyFileInfo file =  m_filesAr[ i ];
	
					// Try to open each one.
					try
					{
						ps.Document document =  app.Open( file.FullName, rawOpenOptions, false );
	
						if ( document != null )
						{
							// It's cool. Close it.
							document.Close( Photoshop.PsSaveOptions.psDoNotSaveChanges );
						}
					}
	
					catch ( Exception /*ex*/ )
					{
						// This is a bad one.
						// Save the filename.
						m_badFiles.Add( file );
						m_latestBadFile =  file;
					}
	
					numberProcessed++;
	
					m_progress =  numberProcessed;
					m_startAt  =  i+1;
	
					// Canceled?
					if ( backgroundWorker1.CancellationPending )
					{
						if ( numberProcessed < m_filesAr.Count )
						{
							e.Cancel =  true;
						}

						break;
					}
				}

				if ( !e.Cancel )
				{
					e.Result =  true;
				}
			}
			
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		/// <summary>
		/// Resizes the Progress Bar so that it adjusts size as the width of the form changes.
		/// (Left side stays fixed.)
		/// </summary>
		private void ResizeProgressBar()
		{
			// Resize the progress bar.
			theProgressBar.Width =  theStatusStrip.Width - 280;
		}

		/// <summary>
		///  Resizes the FilesGatherer (when the whole form resizes).
		/// </summary>
		private void ResizeFilesGatherer()
		{
			// Resize the files gatherer.
			theFilesGatherer.Width =  Width-theFilesGatherer.Left-m_distanceFromRight;
		}
		
		#endregion Methods

		#region Events

		#region Button Click events
		
		/// <summary>
		/// Handles the Check Files button Click event.
		/// Calls the function that runs the process.
		/// </summary>
		private void btnCheckFiles_Click( object sender, EventArgs e )
		{
			// Hand it off...
			DoCheckFiles();
		}

		/// <summary>
		/// Handles the Click event of the Pause/Resume button.
		/// </summary>
		private void btnPauseResume_Click( object sender, EventArgs e )
		{
			// Set focus to Bad Files?
			lblBadFiles.Focus();

			if ( ( (Button) sender ).Text == PAUSE )
			{
				// Specify that it's a pause.
				m_isPause =  true;
	
				// Cancel the background worker.
				backgroundWorker1.CancelAsync();
			}
			else if ( ( (Button) sender ).Text == RESUME )
			{
				// It's a Resume...
				m_isResume =  true;

				DoCheckFiles();
			}
		}

		/// <summary>
		/// Handler for Copy button Click event.
		/// Copies list of bad file paths to the Clipboard.
		/// </summary>
		private void btnCopy_Click( object sender, EventArgs e )
		{
			try
			{
				Clipboard.SetText( m_badFiles.ToText );
			}
			
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		#endregion Button Click events

		#region BackgroundWorker events

		/// <summary>
		/// Handler for the BackgroundWorker task Completed/Canceled/Ended event.
		/// </summary>
		private void backgroundWorker1_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
		{
			if ( !m_isPause || !e.Cancelled )
			{
				// Canceled or run to completion (even after Paused was pressed possibly).
				DoEnableDisableEtc( e.Cancelled ? ProcessingModeType.Cancel : ProcessingModeType.End );
			} 
			else
			{
				// It's a pause.
				DoEnableDisableEtc( ProcessingModeType.Pause );
			}
		}

		/// <summary>
		/// Handler for kicking of the BackgroundWorker task event.
		/// </summary>
		private void backgroundWorker1_DoWork( object sender, DoWorkEventArgs e )
		{
			try
			{
				ProcessFiles( e );
			}
			
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		#endregion BackgroundWorker events

		#region Resize events

		/// <summary>
		/// Handles the Status Resize event.
		/// </summary>
		private void theStatusStrip_Resize( object sender, EventArgs e )
		{
			// Resize the progress bar.
			ResizeProgressBar();
		}

		/// <summary>
		///  Handles the (whole) File Checker Resize event.
		/// </summary>
		private void FileChecker_Resize( object sender, EventArgs e )
		{
			// Resize the files gatherer.
			ResizeFilesGatherer();
		}
		
		#endregion Resize events

		#region Other events
		
		/// <summary>
		/// Handle Select/Copy keyboard shortcuts for Bad Files list.
		/// (On KeyUp.)
		/// </summary>
		void BadFilesListBox_KeyUp( object sender, KeyEventArgs e )
		{
			if ( e.Control && e.KeyCode == Keys.A )
			{
				// Select all.
				for ( int i= 0;  i< lbBadFiles.Items.Count;  i++ )
				{
					lbBadFiles.SetSelected( i, true );
				}
			}
			if ( e.Control && e.KeyCode == Keys.C )
			{
				if ( lbBadFiles.SelectedIndices.Count > 0 )
				{
					// Copy to Clipboard. (Just the ones selected.)
					StringBuilder selected =  new StringBuilder();
	
					foreach ( int index in lbBadFiles.SelectedIndices  )
					{
						selected.AppendLine( lbBadFiles.Items[ index ].ToString() );
					}
	
					Clipboard.SetText( selected.ToString() );
				}
			}
		}

		#endregion Other events

		#endregion Events
	}
}
