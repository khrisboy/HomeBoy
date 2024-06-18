using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

using MyClasses;
using MyControls;

namespace PhotoshopUtilities
{
	public partial class CompareFiles : MyControls.WindowForm
	{
		#region Enums
		
		enum ProcessingModeType
		{
			Process,
			End,
			Cancel,
			Pause,
			Resume,
		};
		
		#endregion Enums

		#region Constants

		const string COMPARE =  "Compare";
		const string CANCEL  =  "Cancel";
		const string PAUSE   =  "Pause";
		const string RESUME  =  "Resume";

		const int WAIT_DELAY =  333;

		#endregion Constants
		
		#region Fields

		MyFileInfosArray m_filesArSource   =  new MyFileInfosArray();
		MyFileInfosArray m_filesArCopy     =  new MyFileInfosArray();
		List<List<MyFileInfo>> m_filesBoth =  new List<List<MyFileInfo>>();
		
		List<MyFileInfo> m_notInSource;
		List<MyFileInfo> m_notInCopy;
		List<MyFileInfo> m_differentFiles;

		SHA256 m_mySHA256 =  SHA256Managed.Create();

		bool m_isProcessing         =  false;
		int m_progress              =  0;
		bool m_isPause              =  false;
		bool m_isResume             =  false;
		bool m_finishedNotInSource  =  false;
		bool m_finishedNotInCopy    =  false;
		int m_startAt               =  0;
		MyFileInfo m_latestDiffFile =  null;

		string[] m_dots = { ".", "..", "...", "....", ".....", "......", ".......", "........", ".........", "..........", };

		List<string> m_validFileTypes = new List<string>() { ".tif", ".tiff", ".psd", ".psb", ".jpg", ".nef", ".crw", ".bmp", ".png" };

		#endregion Fields

		#region Constructors

		public CompareFiles()
		{
			InitializeComponent();

			ResizeProgressBar();
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

				btnPauseResume.Visible       =  true;
// 				btnDeleteNotInSource.Enabled =  false;
			}
			else if ( type == ProcessingModeType.End || type == ProcessingModeType.Cancel )
			{
				m_isProcessing =  false;
				m_isPause      =  false;
				m_isResume     =  false;
				m_startAt      =  0;

				btnRunCancel.Text =  COMPARE;

				btnPauseResume.Visible =  false;
// 				btnDeleteNotInSource.Enabled =  lbFilesNotInSource.Items.Count > 0 ?  true : false;
			}
			else if ( type == ProcessingModeType.Pause )
			{
				m_isResume =  false;

				btnPauseResume.Text =  RESUME;
// 				btnDeleteNotInSource.Enabled =  lbFilesNotInSource.Items.Count > 0 ?  true : false;
			}
			else if ( type == ProcessingModeType.Resume )
			{
				m_isResume =  false;
				m_isPause      =  false;
				m_isProcessing =  true;

				btnRunCancel.Text   =  CANCEL;
				btnPauseResume.Text =  PAUSE;

// 				btnDeleteNotInSource.Enabled =  false;
			}
		}

		/// <summary>
		/// Loads the file names that are not in the source.
		/// </summary>
		private void LoadNotInSource()
		{
			if ( m_notInSource != null && m_finishedNotInSource )
			{
				lbFilesNotInSource.Items.Clear();

				foreach ( MyFileInfo info in m_notInSource )
				{
					lbFilesNotInSource.Items.Add( info.FullName );
				}

				m_finishedNotInSource =  false;
			}
		}

		/// <summary>
		/// Loads the file names that are not in the copy.
		/// </summary>
		private void LoadNotInCopy()
		{
			if ( m_notInCopy != null && m_finishedNotInCopy )
			{
				lbFilesNotInCopy.Items.Clear();

				foreach ( MyFileInfo info in m_notInCopy )
				{
					lbFilesNotInCopy.Items.Add( info.FullName );
				}

				m_finishedNotInCopy =  false;
			}
		}

		private MyFileInfosArray GetFilesFromDirectories( ref MyFileInfosArray filesAr, string directory )
		{
			try
			{
				if ( !String.IsNullOrEmpty( directory ) )
				{
					// Get the files from this directory.
					string[] fileEntries =  Directory.GetFiles( directory );

					foreach ( string file in fileEntries )
					{
						MyFileInfo info =  new MyFileInfo( file );

						if ( IsValidFileType( info ) )
						{
							filesAr.Add( info );
						}
					}

					// Recurse into subdirectories of this directory?
					if ( chkIncludeSubdirectories.Checked )
					{
						string[] subdirectoryEntries =  Directory.GetDirectories( directory );

						foreach ( string subdirectory in subdirectoryEntries )
						{
							GetFilesFromDirectories( ref filesAr, subdirectory );
						}
					}
				}
			}

			catch ( Exception ex )
			{
				MessageBox.Show( ex.Message + "\r\n" + ex.StackTrace );
			}

			return ( filesAr );
		}

		void DoFileCompares()
		{
			if ( !m_isProcessing || m_isResume )
			{
				// Processing or resuming processing.
				if ( !m_isResume )
				{
					lbFilesNotInSource.Items.Clear();
					lbFilesNotInCopy.Items.Clear();
					lbFilesDifferent.Items.Clear();

					m_filesArSource =  new MyFileInfosArray();
					m_filesArCopy   =  new MyFileInfosArray();
					m_filesBoth     =  new List<List<MyFileInfo>>();

					DoEnableDisableEtc( ProcessingModeType.Process );

					m_finishedNotInSource =  false;
					m_finishedNotInCopy =  false;
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
				if ( m_filesBoth.Count > 0 )
				{
					// Set up the progress bar.
					theProgressBar.Minimum =  0;
					theProgressBar.Maximum =  m_filesBoth.Count;
					theProgressBar.Step    =  1;
					theProgressBar.Value   =  0;
				}

				if ( theProgressBar.Value != m_progress )
				{
					theProgressBar.Value =  m_progress;

					whichDots =  0;
				}
				else
				{
					// Add a dot. Reset to none if at 10.
					whichDots++;

					if ( whichDots >= 10 )
					{
						whichDots =  0;
					}
				}

				// Time to update the status message.
				if ( m_filesBoth.Count == 0 )
				{
					theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed{2}", 
						                                       m_progress, m_filesBoth.Count, whichDots > 0 ? m_dots[ whichDots ] : "" );
				} 
				else
				{
					theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed, {2} different files", m_progress, m_filesBoth.Count, m_differentFiles.Count );
				}


				// 'New' different file?
				if ( m_latestDiffFile != null )
				{
					// Yes. Add it to the display of bad files.
					MyFileInfo info =  m_latestDiffFile;
					m_latestDiffFile =  null;

					DisplayDifferentFile( info );
				}

				LoadNotInSource();
				LoadNotInCopy();

				await Task.Delay( WAIT_DELAY );
			}

			// Last status/progress update.
			theProgressBar.Value =  m_progress;

			theStatusStripLabel.Text =  String.Format( "{0} out of {1} files processed, {2} different files", m_progress, m_filesBoth.Count, m_differentFiles.Count );
		}

		/// <summary>
		/// Add a different file to the display of different files.
		/// </summary>
		private void DisplayDifferentFile( MyFileInfo file )
		{
			// Display the different files.
			lbFilesDifferent.Items.Add( file );
		}

		/// <summary>
		/// Does the work.
		/// </summary>
		void CompareTheFiles( DoWorkEventArgs e )
		{
			try
			{
				e.Result =  false;

				if ( !m_isResume )
				{
					if ( m_differentFiles == null )
					{
						m_differentFiles =  new List<MyFileInfo>();
					}

					// Always clear
					m_differentFiles.Clear();

					if ( m_filesArSource.Count == 0 )
					{
						GetFilesFromDirectories( ref m_filesArSource, browseForDirectory1.Directory );
					}
	
					if ( m_filesArCopy.Count == 0 )
					{
						GetFilesFromDirectories( ref m_filesArCopy, browseForDirectory2.Directory );
					}
	
					// Get files not in each directory.
					ArrayList fileNamesWithParentSource =  m_filesArSource.FileNamesWithParent;
					ArrayList fileNamesWithParentCopy =  m_filesArCopy.FileNamesWithParent;

					var filesNotInSource =  from fileInfo in m_filesArCopy.Cast<MyFileInfo>()
											where !fileNamesWithParentSource.Contains( fileInfo.FileNameWithParent )
											select fileInfo;
		
					var filesNotInCopy =  from fileInfo in m_filesArSource.Cast<MyFileInfo>()
										  where !fileNamesWithParentCopy.Contains( fileInfo.FileNameWithParent )
										  select fileInfo;
	
					var filesInBothSource =  from fileInfo in m_filesArSource.Cast<MyFileInfo>()
											 where fileNamesWithParentCopy.Contains( fileInfo.FileNameWithParent )
											 select fileInfo;
	
					var filesInBothCopy =  from fileInfo in m_filesArCopy.Cast<MyFileInfo>()
										   where fileNamesWithParentSource.Contains( fileInfo.FileNameWithParent )
										   select fileInfo;
	
					m_notInSource =  filesNotInSource.ToList();
					m_notInCopy   =  filesNotInCopy.ToList();

					m_finishedNotInSource =  true;
					m_finishedNotInCopy =  true;
	
					List<MyFileInfo> inBothSource =  filesInBothSource.ToList();
					List<MyFileInfo> inBothCopy   =  filesInBothCopy.ToList();
	
					m_filesBoth =  new List<List<MyFileInfo>>();
	
					for ( int i= 0; i< inBothSource.Count;  i++ )
					{
						List<MyFileInfo> infoList =  new List<MyFileInfo>();
	
						infoList.Add( inBothSource[ i ] );
						infoList.Add( inBothCopy[ i ] );
	
						m_filesBoth.Add( infoList );
					}
				}

				// And get the files in both
				CompareSameFiles( e );

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

		void CompareSameFiles( DoWorkEventArgs e )
		{
			try
			{
				// Set up some parameters.
				int numberToProcess =  m_filesBoth.Count;
				int numberProcessed =  m_startAt;
	
				// Loop through all of the 'same' files.
				for ( int i= m_startAt;  i< m_filesBoth.Count;  i++ )
				{
					try
					{
						MyFileInfo sourceInfo =  m_filesBoth[ i ][ 0 ];
						MyFileInfo copyInfo   =  m_filesBoth[ i ][ 1 ];
	
						byte[] sourceHash =  ComputeFileHash( sourceInfo );
						byte[] copyHash   =  ComputeFileHash( copyInfo );
	
						if ( !CompareFileHashes( sourceHash, copyHash ) )
						{
							// Files are different.
							m_differentFiles.Add( copyInfo );
							m_latestDiffFile =  copyInfo;
						}
					}
					
					catch( Exception ex )
					{
						MessageBox.Show( ex.Message );
					}

					numberProcessed++;

					m_progress =  numberProcessed;
					m_startAt  =  i+1;

					// Canceled?
					if ( backgroundWorker1.CancellationPending )
					{
						if ( numberProcessed < m_filesBoth.Count )
						{
							e.Cancel =  true;
						}

						break;
					}
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

		void DrawListBoxItem( ListBox listBox, DrawItemEventArgs e, Brush brush )
		{
			// Draw the background of the ListBox control for each item.
			e.DrawBackground();

			string entry =  listBox.Items[ e.Index ].ToString();

			// Draw the current item text based on the current Font  
			// and the custom brush settings.
			e.Graphics.DrawString( entry,
								   e.Font,
								   brush,
								   e.Bounds,
								   StringFormat.GenericDefault );

			// If the ListBox has focus, draw a focus rectangle around the selected item.
			e.DrawFocusRectangle();
		}

		private byte[] ComputeFileHash( MyFileInfo info )
		{
			byte[] hashValue;

			// Create a fileStream for the file.
			FileStream fileStream = info.FileInfo.Open( FileMode.Open );

			// Be sure it's positioned to the beginning of the stream.
			fileStream.Position = 0;

			// Compute the hash of the fileStream.
			hashValue = m_mySHA256.ComputeHash( fileStream );

			return ( hashValue );
		}

		private bool CompareFileHashes( byte[] sourceHash, byte[] copyHash )
		{
			bool areSame =  true;

			if ( sourceHash.Length == copyHash.Length )
			{
				for ( int i= 0;  i< sourceHash.Length;  i++ )
				{
					if ( sourceHash[ i ] != copyHash[ i ] )
					{
						areSame =  false;
						break;
					}
				}
			}
			else
			{
				areSame =  false;
			}

			return ( areSame );
		}

		protected virtual bool IsValidFileType( MyFileInfo fileInfo )
		{
			// Base default is for graphics files.
			bool isCool	=  false;

			foreach ( string type in m_validFileTypes )
			{
				if ( fileInfo.FileInfo.Extension.ToLower() == type.ToLower() )
				{
					isCool =  true;
					break;
				}
			}

			return ( isCool );
		}
		
		#endregion Methods

		#region Events
		
		#region Button Clicks
		
		private void btnCompare_Click( object sender, EventArgs e )
		{
			// Hand it off...
			DoFileCompares();
		}

		private void btnPauseResume_Click( object sender, EventArgs e )
		{
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

				DoFileCompares();
			}
		}

		private void btnDeleteNotInSource_Click( object sender, EventArgs e )
		{
			try
			{
				List<string> selectedItemsText =  new List<string>();

				foreach( String item in lbFilesNotInSource.SelectedItems )
				{
					MyFileInfo info = new MyFileInfo( item );

					selectedItemsText.Add( item );

					info.FileInfo.Delete();
				}

				foreach( String item in selectedItemsText )
				{
					lbFilesNotInSource.Items.Remove( item );
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}
		
		#endregion Button Clicks

		#region Custom Draw ListBox
		
		private void lbDiffFiles_DrawItem( object sender, DrawItemEventArgs e )
		{
			// Define the color of the brush to use.
			DrawListBoxItem( lbFilesNotInSource, e, Brushes.Red );
		}

		private void lbFilesNotInCopy_DrawItem( object sender, DrawItemEventArgs e )
		{
			// Define the color of the brush to use.
			DrawListBoxItem( lbFilesNotInCopy, e, Brushes.Purple );
		}

		private void lbFilesDifferent_DrawItem( object sender, DrawItemEventArgs e )
		{
			// Define the color of the brush to use.
			DrawListBoxItem( lbFilesDifferent, e, Brushes.DarkGreen );
		}

		#endregion Custom Draw ListBox

		#region Background Worker
		
		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				CompareTheFiles( e );
			}
			
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if ( !m_isPause || !e.Cancelled )
			{
				DoEnableDisableEtc( e.Cancelled ? ProcessingModeType.Cancel : ProcessingModeType.End );
			}
			else
			{
				// It's a pause.
				DoEnableDisableEtc( ProcessingModeType.Pause );
			}
		}

		#endregion Background Worker

		private void theStatusStrip_Resize( object sender, EventArgs e )
		{
			// Resize the progress bar.
			ResizeProgressBar();
		}
		
		#endregion Events
	}
}
