using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ps = Photoshop;

using MyClasses;
using MyControls;

namespace PhotoshopUtilities
{
	public partial class SaveTiffCompressed : Form
	{
		enum ProcessingModeType
		{
			Process,
			End,
			Cancel,
			Pause,
			Resume,
		};

		//================================================================================ Constants

		#region Constants

		const string COMPRESS_FILES =  "Compress Files";
		const string CANCEL         =  "Cancel";
		const string PAUSE          =  "Pause";
		const string RESUME         =  "Resume";

		const int WAIT_DELAY =  333;
		
		#endregion Constants

		//================================================================================ Properties
		
		#region Properties
		
		MyFileInfosArray m_filesAr;

		bool m_isProcessing        =  false;
		int m_progress             =  0;
		int m_distanceFromRight    =  0;
		bool m_isPause             =  false;
		bool m_isResume            =  false;
		int m_startAt              =  0;
		int m_processingIndex      =  0;
		string m_processingFileName;

		string[] m_dots = { ".", "..", "...", "....", ".....", "......", ".......", "........", ".........", "..........", };

		#endregion Properties

		//================================================================================ Constructors

		#region Constructors

		public SaveTiffCompressed()
		{
			InitializeComponent();

			// Set up the BackgroundWorker.
			theBackgroundWorker.WorkerSupportsCancellation =  true;
			theBackgroundWorker.WorkerReportsProgress      =  true;

			theBackgroundWorker.DoWork             +=  backgroundWorker1_DoWork;
			theBackgroundWorker.RunWorkerCompleted +=  backgroundWorker1_RunWorkerCompleted;

			// Get distance of FilesGatherer from the right side.
			m_distanceFromRight =  this.Width - (theFilesListView.Left+theFilesListView.Width);
		}

		#endregion Constructors

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
			}
			else if ( type == ProcessingModeType.End || type == ProcessingModeType.Cancel )
			{
				m_isProcessing =  false;
				m_isPause      =  false;
				m_isResume     =  false;
				m_startAt      =  0;

				btnRunCancel.Text =  COMPRESS_FILES;

				btnPauseResume.Visible =  false;
			}
			else if ( type == ProcessingModeType.Pause )
			{
				m_isResume =  false;

				btnPauseResume.Text =  RESUME;
			}
			else if ( type == ProcessingModeType.Resume )
			{
				m_isResume =  false;
				m_isPause      =  false;
				m_isProcessing =  true;

				btnRunCancel.Text   =  CANCEL;
				btnPauseResume.Text =  PAUSE;
			}
		}

		/// <summary>
		/// Handles the button clicks.
		/// Sets up processing (from initial or resume), or handles Pause, or handles Cancel.
		/// </summary>
		private void DoCompressFiles()
		{
			UpdateProcessingFile();

			if ( !m_isProcessing || m_isResume )
			{
				// Processing (Check Files) or resuming processing.
				if ( !m_isResume )
				{
					// Transfer files info.
					m_filesAr =  this.theFilesListView.Files;

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
				theBackgroundWorker.RunWorkerAsync();

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
				theBackgroundWorker.CancelAsync();
			}
		}

		private void UpdateProcessingFile( string processingFileName= null )
		{
			if ( String.IsNullOrEmpty( processingFileName ) )
			{
				labelProcessingFile.Text = String.Empty;
			}
			else
			{
				labelProcessingFile.Text = String.Format( "Processing file: {0}", processingFileName );
			}
		}

		/// <summary>
		/// Async method to do the status updating on the main (gui) thread.
		/// (Requires .NET 4.5)
		/// </summary>
		private async void DoStatusUpdateAsync()
		{
			int whichDots =  0;

			while ( theBackgroundWorker.IsBusy )
			{
				if ( theProgressBar.Value != m_progress )
				{
					theProgressBar.Value     =  m_progress;
					theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed", m_progress, m_filesAr.Count );
					labelProcessingFile.Text = String.Empty;

					whichDots =  0;
				}
				else
				{
					// Time to update the status message.
					theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed{2}", m_progress, m_filesAr.Count, m_dots[ whichDots ] );

					UpdateProcessingFile( m_processingFileName );

					// Add a dot. Reset to none if at 10.
					whichDots++;

					if ( whichDots >= 10 )
					{
						whichDots =  0;
					}
				}

				if ( m_processingIndex != theFilesListView.SelectedIndex )
				{
					// Select the current file.
					theFilesListView.Select( m_processingIndex );
				}

				await Task.Delay( WAIT_DELAY );
			}

			// Last status/progress update.
			theProgressBar.Value =  m_progress;

			theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed", m_progress, m_filesAr.Count );

			UpdateProcessingFile();
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

				// Fire up Photoshop.
				ps.Application app =  new ps.Application();

				// No dialogs
				app.DisplayDialogs =  Photoshop.PsDialogModes.psDisplayNoDialogs;

				// Set up some parameters.
				int numberToProcess =  m_filesAr.Count;
				int numberProcessed =  m_startAt;

				// Save options.
				var options = new ps.TiffSaveOptions();

				options.ByteOrder = ps.PsByteOrderType.psIBMByteOrder;
				options.ImageCompression = ps.PsTiffEncodingType.psTiffZIP;

				// Loop through all of the (remaining) files.
				for ( int i= m_startAt; i< m_filesAr.Count; i++ )
				{
					MyFileInfo file =  m_filesAr[ i ];

					// Try to open each one.
					try
					{
						ps.Document document =  app.Open( file.FullName );

						if ( document != null )
						{
							m_processingFileName = file.Name;
							m_processingIndex = i;

							// Save it. Compressed.
							document.SaveAs( document.FullName, options, false );

							// Close it.
							document.Close( Photoshop.PsSaveOptions.psDoNotSaveChanges );
						}
					}

					catch ( Exception ex )
					{
						MessageBox.Show( ex.Message );
					}

					numberProcessed++;

					m_progress =  numberProcessed;
					m_startAt  =  i+1;

					// Canceled?
					if ( theBackgroundWorker.CancellationPending )
					{
						if ( numberProcessed < m_filesAr.Count )
						{
							e.Cancel =  true;
						}

						break;
					}
				}

				m_processingFileName = String.Empty;

				if ( !e.Cancel )
				{
					e.Result =  true;
				}
			}

			catch ( Exception ex )
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
			theFilesListView.Width =  Width-theFilesListView.Left-m_distanceFromRight;
		}

		//================================================================================ Events

		#region Events

		#region Button clicks

		/// <summary>
		/// Handles the Compress Files button Click event.
		/// Calls the function that runs the process.
		/// </summary>
		private void btnCompressFiles_Click( object sender, EventArgs e )
		{
			DoCompressFiles();
		}

		/// <summary>
		/// Handles the Click event of the Pause/Resume button.
		/// </summary>
		private void btnPauseResume_Click( object sender, EventArgs e )
		{
			if ( ((Button) sender).Text == PAUSE )
			{
				// Specify that it's a pause.
				m_isPause =  true;

				// Cancel the background worker.
				theBackgroundWorker.CancelAsync();
			}
			else if ( ((Button) sender).Text == RESUME )
			{
				// It's a Resume...
				m_isResume =  true;

				DoCompressFiles();
			}
		}

		#endregion Button clicks

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

			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		#endregion BackgroundWorker events

		/// <summary>
		/// Handles the Status Resize event.
		/// </summary>
		private void theStatusStrip_Resize( object sender, EventArgs e )
		{
			// Resize the progress bar.
			ResizeProgressBar();
		}

		#endregion Events
	}
}
