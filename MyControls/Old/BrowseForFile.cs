using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;

using MyClasses;

namespace MyControls
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public partial class BrowseForFile : BrowseForObject
	{
		#region Members

		private string		m_filesFilter;
		private int			m_filterIndex;
		private bool		m_restoreDirectory;
		private string		m_initialDirectory;
		private bool		m_checkFileExists;
		private string		m_defaultExtension;
		
		#endregion

		#region Constructors
		
		public BrowseForFile()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Initialize.
			FilesFilter        =  "All files (*.*)|*.*";
			m_filterIndex      =  1;
			m_restoreDirectory =  true;
			m_initialDirectory =  "c:\\";
			m_checkFileExists  =  true;

			// Display only the filename portion.
			DisplayFileNameOnly =  false;
		}

		#endregion Constructors

		#region Properties

		public string FilesFilter
		{
			set { m_filesFilter	=  value; }
			get { return ( m_filesFilter );}
		}

		public int FilterIndex
		{
			set { m_filterIndex	=  value; }
			get { return ( m_filterIndex ); }
		}

		public bool RestoreDirectory
		{
			set { m_restoreDirectory =  value; }
		}

		public string InitialDirectory
		{
			get { return ( m_initialDirectory );}
			
			set
			{
				m_initialDirectory =  value;

				// If this is set, then clear out the FileName.
				FileName =  String.Empty;
			}
		}

		public bool DisplayFileNameOnly
		{
			get
			{
				return ( base.m_displayData );
			}

			set
			{
				base.m_displayData =  value;
			}
		}

		public override string TheText
		{
			set
			{
				// Set the value (always).
				base.ValueText =  value;

				m_theToolTip.SetToolTip( edtPath, FileName );
				
				// If we're displaying the filename only tell the control to do that.
				if ( DisplayFileNameOnly )
				{
					try
					{
						MyFileInfo info =  new MyFileInfo( value );

						base.DisplayText =  info.Name;
					}

					catch( Exception )
					{
						base.DisplayText =  value;
					}
				}
				else
				{
					base.DisplayText =  value;
				}
			}
		}

		public string FileName
		{
			get { return ( base.ValueText ); }

			set
			{
				TheText	=  value;
			}
		}

		public string File
		{
			get { return ( Path.GetFileName( FileName ) ); }
		}

		public string FileNoExtension
		{
			get { return ( Path.GetFileNameWithoutExtension( FileName ) ); }
		}

		public string Directory
		{
			get { return ( Path.GetDirectoryName( FileName ) ); }
		}

		public bool CheckFileExists
		{
			get { return ( m_checkFileExists ); }
			set { m_checkFileExists =  value; }
		}

        public string DefaultExtension
        {
            get { return ( m_defaultExtension ); }
            
			set 
            {
                if ( value != null && value != "" )
                {
                    if ( value.IndexOf( "." ) == 0 )
                    {
	                    m_defaultExtension =  value;
                    }
                    else
                    {
	                    m_defaultExtension =  "." + value;
                    }
                }
            }
        }
		
		#endregion Properties

		#region Methods
		
		public void Browse()
		{
			HandleBrowseClick( btnBrowse, new EventArgs() );
		}

		protected virtual void HandleBrowseClick( object sender, System.EventArgs e )
		{
			// Bring up dialog to select file.
			OpenFileDialog fileDlg =  new OpenFileDialog();

			// Update initial directory in case it was typed in.
			if ( FileName != "" )
			{
				string path =  Path.GetDirectoryName( FileName );

				if ( path != String.Empty )
				{
					m_initialDirectory =  path;
				}
			}

			fileDlg.InitialDirectory =  m_initialDirectory;
			fileDlg.Filter			 =  FilesFilter;
			fileDlg.FilterIndex		 =  m_filterIndex;
			fileDlg.RestoreDirectory =  true;
			fileDlg.FileName		 =  FileName;
			fileDlg.CheckFileExists	 =  m_checkFileExists;

			if ( fileDlg.ShowDialog() == DialogResult.OK ) 
			{
				// Update initial directory.
				m_initialDirectory	=  Path.GetDirectoryName( fileDlg.FileName );

				// Tack on an extension?
				if ( m_defaultExtension != null && m_defaultExtension.Length == 0 )
				{
					if ( Path.GetExtension( fileDlg.FileName ).Length == 0 )
					{
						fileDlg.FileName =  fileDlg.FileName + m_defaultExtension;
					}
				}
	
				// Update the filename (and path).
				FileName =  fileDlg.FileName;

				// Fire off an event.
				base.BrowseForObject_OnMyBrowsed( sender, e );
			}

			fileDlg.Dispose();
		}

		#endregion Methods

		#region Events

		protected void btnBrowse_Click( object sender, System.EventArgs e )
		{
			HandleBrowseClick( sender, e );
		}

		private void BrowseForFile_OnMyTextChanged( object sender, System.EventArgs e )
		{
			// Update the file (and directory).
			string display =  DisplayText;
			string value   =  ValueText;

			string both =  display + value;

			if ( DisplayFileNameOnly )
			{
				// We're changing the filename portion only.
				String directory =  Directory;

				if ( directory != null )
				{
					TheText =  directory + "\\" + display;
				}
			}
		}

		private void BrowseForFile_Load( object sender, System.EventArgs e )
		{
		}

		#endregion Events
	}
}
