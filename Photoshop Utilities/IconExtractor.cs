using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MyControls;

namespace PhotoshopUtilities
{
	public partial class IconExtractor : MyControls.MyWindowsForm
	{
		#region Members

		string m_directory =  String.Empty;

		#endregion Members

		#region Constructors

		public IconExtractor()
		{
			InitializeComponent();
		}

		#endregion Constructors

		#region Methods
		
		bool IsIconnable( FileInfo file )
		{
			return ( file.Extension.ToLower() == ".exe" || file.Extension.ToLower() == ".dll" );
		}

		#endregion Methods

		#region Events

		private void IconExtractor_Load( object sender, EventArgs e )
		{
			 // Initialize the ListView
			 lvIcons.SmallImageList	=  theImageList;
			 lvIcons.View			=  View.List;
		}

		private void btnExtract_Click( object sender, EventArgs e )
		{
			// Clear the ListView.
			lvIcons.Items.Clear();
			theImageList.Images.Clear();

			// Get the directory.
			DirectoryInfo	dir	=  new DirectoryInfo( bfd1.Directory );

			lvIcons.BeginUpdate();

			// For each file in the directory, create a ListViewItem
			// and set the icon to the icon extracted from the file.
			foreach ( FileInfo file in dir.GetFiles() )
			{
				if ( IsIconnable( file ) )
				{
					// Set a default icon for the file.
					Icon iconForFile  =  SystemIcons.WinLogo;
					ListViewItem item =  new ListViewItem( file.Name, 1 );
					  
					// Check to see if the image collection contains an image
					// for this extension, using the extension as a key.
					if ( !theImageList.Images.ContainsKey( file.Name ) )
					{
						// If not, add the image to the image list.
						iconForFile =  Icon.ExtractAssociatedIcon( file.FullName );
						
						theImageList.Images.Add( file.Name, iconForFile );
					}

					item.ImageKey =  file.Name;

					lvIcons.Items.Add( item );
				}
			}

			lvIcons.EndUpdate();
		}

		private void btnSave_Click( object sender, EventArgs e )
		{
			DirectoryInfo dir =  new DirectoryInfo( bfdSave.Directory );

			foreach( ListViewItem item in lvIcons.SelectedItems )
			{
				string iconKey =  item.ImageKey;
				Image icon     =  theImageList.Images[ iconKey ];

				icon.Save( dir.FullName + @"\" + iconKey + ".ico" );
			}
		}
		
		#endregion Events

		private void bfd1_OnMyTextChanged( object sender, EventArgs e )
		{
			// Set the directory location for the file browser.
			bff1.InitialDirectory =  bfd1.Directory;
		}
	}
}