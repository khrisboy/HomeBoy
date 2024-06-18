//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="BrowseForDirectory.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyControls
{
	/// <summary>
	/// Summary description for BrowseForDirectory.
	/// </summary>
	public class BrowseForDirectory : BrowseForObject
	{
		#region Members
		
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion Members

		#region Properties

		public string Directory
		{
			get
			{
				return ( edtPath.Text != null ?  edtPath.Text : "" );
			}

			set { edtPath.Text =  value; }
		}
		
		#endregion Properties

		#region Constructors

		public BrowseForDirectory()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		#endregion Constructors

		#region Methods
		
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// btnBrowse
			// 
			this.btnBrowse.Click += new System.EventHandler( this.btnBrowse_Click );
			// 
			// BrowseForDirectory
			// 
			this.Name = "BrowseForDirectory";
			this.ResumeLayout( false );
		}
		
		#endregion

		#endregion Methods

		#region Events

		private void btnBrowse_Click( object sender, System.EventArgs e )
		{
			// Bring up dialog to select directory.
			FolderBrowserDialog	folderDlg =  new FolderBrowserDialog();

			folderDlg.Description  =  "Select folder containing files";
			folderDlg.SelectedPath =  Directory;

			if ( folderDlg.ShowDialog() == DialogResult.OK ) 
			{
				Directory =  folderDlg.SelectedPath;
			}

			folderDlg.Dispose();
		}

		#endregion Events
	}
}
