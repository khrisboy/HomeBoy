//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="FilesGatherer.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

using MyClasses;
using MyControls;

namespace MyControls
{
	public class FilesGatherer : FilesListView
	{
		#region Members

		protected BrowseForDirectory	bfdImages;
		protected CheckBox				chkImagesDirectory;
		protected CheckBox				chkIncludeSubdirectories;
		private Label lblFilter;
		private ComboBox cbxFilter;
		private ToolTip theToolTip;

		// Required designer variable.
		private System.ComponentModel.IContainer components =  null;

		#endregion Members

		#region Properties

		public override List<string> ValidFileTypes
		{
			get 
			{
				if ( !IsFromListView )
				{
					// Gleam the valid file types from the selected directory filter.
					m_validFileTypes.Clear();

					string filters =  cbxFilter.SelectedItem.GetType().GetProperty( "Value" ).GetValue( cbxFilter.SelectedItem ).ToString();

// 					if ( filters.Length == 2 )
// 					{
						List<string> extensions =  filters.Split( new char[] { ';' } ).ToList();
	
						var newExtensions =  ( from ext in extensions
												where !m_validFileTypes.Contains( ext.Substring( 1 ), StringComparer.InvariantCultureIgnoreCase ) &&
													    ext != "*.*"
												select ext.Substring( 1 ).ToLower() ).ToList();
	
						m_validFileTypes.AddRange( newExtensions );
// 					}
				}

				return ( base.ValidFileTypes );
			}
			set 
			{ 
			} // No setting allowed...
		}

		public override string FilesFilter
		{
			set
			{
				base.FilesFilter =  value;

				try
				{
// 					// Gleam the valid file types from the filter.
					string[] filters =  m_filesFilter.Split( new char[] { '|' } );
	
					cbxFilter.Items.Clear();
// 					m_validFileTypes.Clear();
	
					cbxFilter.ValueMember =  "Value";
					cbxFilter.DisplayMember = "Text";

					for ( int i= 0;  i< filters.Length;  i+= 2 )
					{
						cbxFilter.Items.Add( new { Text= filters[ i ], Value= filters[ i+1 ] } );
					}
	
					// Default to 1st one.
					// (We'll let it blow if there are none. Not cool!)
					cbxFilter.SelectedIndex =  0;
				}
				
				catch( Exception ex )
				{
					MessageBox.Show( ex.Message + "\r\n" + ex.StackTrace );
				}
			}
		}

		public override MyFileInfosArray Files
		{
			get
			{
				if ( IsFromListView )
				{
					return ( base.Files );
				}
				else
				{
					// Process the list of files found in the directory.
					MyFileInfosArray filesAr =  new MyFileInfosArray();
					
					GetFilesFromDirectories( ref filesAr, bfdImages.Directory );

					return ( filesAr );
				}
			}
		}

		public string RootDirectory
		{
			get
			{
				return ( bfdImages.Directory );
			}

			set
			{
				bfdImages.Directory	=  value;
			}
		}

		public bool IsFromListView
		{
			get
			{
				return ( !chkImagesDirectory.Checked );
			}
		}

		#endregion Properties

		#region Constructors
		
		public FilesGatherer()
		{
			InitializeComponent();

			FilesFilter	=  "Image Files(*.TIF;*.PSD;*.PSB;*.JPG;*.NEF;*.CRW;*.BMP;*.PNG)|*.TIF;*.PSD;*.PSB;*.JPG;*.NEF;*.CRW;*.BMP;*.PNG";
// 			ValidFileTypes	 =  new string[] { ".tif", ".psd", ".psb", ".jpg", ".nef", ".crw", ".bmp", ".png" };

			theToolTip.SetToolTip( cbxFilter, "" );
		}

		#endregion Constructors

		#region Methods
		
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
			
			catch( Exception ex )
			{
				MessageBox.Show( ex.Message + "\r\n" + ex.StackTrace );
			}

			return ( filesAr );
		}

		// Clean up any resources being used.
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}

			base.Dispose( disposing );
		}

		#endregion Methods

		#region Events

		private void Loaded( object sender, System.EventArgs e )
		{
			bfdImages.Width =  388;

			// Set up some control enabling.
			chkImagesDirectory_CheckedChanged( sender, e );
		}

		private void chkImagesDirectory_CheckedChanged( object sender, EventArgs e )
		{
			bfdImages.Enabled				 =  chkImagesDirectory.Checked;
			chkIncludeSubdirectories.Enabled =  chkImagesDirectory.Checked;
			cbxFilter.Enabled                =  chkImagesDirectory.Checked;
		}

		private void cbxFilter_SelectedIndexChanged( object sender, EventArgs e )
		{
			theToolTip.SetToolTip( cbxFilter, cbxFilter.SelectedItem.ToString() );
		}
		
		#endregion Events

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.bfdImages = new MyControls.BrowseForDirectory();
			this.chkImagesDirectory = new System.Windows.Forms.CheckBox();
			this.chkIncludeSubdirectories = new System.Windows.Forms.CheckBox();
			this.cbxFilter = new System.Windows.Forms.ComboBox();
			this.lblFilter = new System.Windows.Forms.Label();
			this.theToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.grpFiles.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpFiles
			// 
			this.grpFiles.Controls.Add(this.lblFilter);
			this.grpFiles.Controls.Add(this.cbxFilter);
			this.grpFiles.Controls.Add(this.bfdImages);
			this.grpFiles.Controls.Add(this.chkImagesDirectory);
			this.grpFiles.Controls.Add(this.chkIncludeSubdirectories);
			this.grpFiles.Size = new System.Drawing.Size(420, 300);
			this.grpFiles.Controls.SetChildIndex(this.rbFilesSortAscending, 0);
			this.grpFiles.Controls.SetChildIndex(this.rbFilesSortDescending, 0);
			this.grpFiles.Controls.SetChildIndex(this.chkClearAutoDrop, 0);
			this.grpFiles.Controls.SetChildIndex(this.chkIncludeSubdirectories, 0);
			this.grpFiles.Controls.SetChildIndex(this.chkImagesDirectory, 0);
			this.grpFiles.Controls.SetChildIndex(this.stImages, 0);
			this.grpFiles.Controls.SetChildIndex(this.bfdImages, 0);
			this.grpFiles.Controls.SetChildIndex(this.btnBrowse, 0);
			this.grpFiles.Controls.SetChildIndex(this.btnRemove, 0);
			this.grpFiles.Controls.SetChildIndex(this.lbImages, 0);
			this.grpFiles.Controls.SetChildIndex(this.btnClear, 0);
			this.grpFiles.Controls.SetChildIndex(this.chkFilesSort, 0);
			this.grpFiles.Controls.SetChildIndex(this.cbxFilter, 0);
			this.grpFiles.Controls.SetChildIndex(this.lblFilter, 0);
			// 
			// stImages
			// 
			this.stImages.Location = new System.Drawing.Point(13, 20);
			this.stImages.Size = new System.Drawing.Size(198, 14);
			// 
			// bfdImages
			// 
			this.bfdImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bfdImages.BrowseLabel = "...";
			this.bfdImages.Directory = "";
			this.bfdImages.Label = "Images Directory";
			this.bfdImages.Location = new System.Drawing.Point(16, 245);
			this.bfdImages.Name = "bfdImages";
			this.bfdImages.Size = new System.Drawing.Size(388, 38);
			this.bfdImages.TabIndex = 30;
			// 
			// chkImagesDirectory
			// 
			this.chkImagesDirectory.AutoSize = true;
			this.chkImagesDirectory.Location = new System.Drawing.Point(32, 218);
			this.chkImagesDirectory.Name = "chkImagesDirectory";
			this.chkImagesDirectory.Size = new System.Drawing.Size(90, 17);
			this.chkImagesDirectory.TabIndex = 10;
			this.chkImagesDirectory.Text = "&Use Directory";
			this.chkImagesDirectory.UseVisualStyleBackColor = true;
			this.chkImagesDirectory.CheckedChanged += new System.EventHandler(this.chkImagesDirectory_CheckedChanged);
			// 
			// chkIncludeSubdirectories
			// 
			this.chkIncludeSubdirectories.AutoSize = true;
			this.chkIncludeSubdirectories.Enabled = false;
			this.chkIncludeSubdirectories.Location = new System.Drawing.Point(133, 218);
			this.chkIncludeSubdirectories.Name = "chkIncludeSubdirectories";
			this.chkIncludeSubdirectories.Size = new System.Drawing.Size(131, 17);
			this.chkIncludeSubdirectories.TabIndex = 20;
			this.chkIncludeSubdirectories.Text = "&Include Subdirectories";
			this.chkIncludeSubdirectories.UseVisualStyleBackColor = true;
			// 
			// cbxFilter
			// 
			this.cbxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxFilter.Enabled = false;
			this.cbxFilter.FormattingEnabled = true;
			this.cbxFilter.Location = new System.Drawing.Point(284, 220);
			this.cbxFilter.Name = "cbxFilter";
			this.cbxFilter.Size = new System.Drawing.Size(121, 21);
			this.cbxFilter.TabIndex = 31;
			this.cbxFilter.SelectedIndexChanged += new System.EventHandler(this.cbxFilter_SelectedIndexChanged);
			// 
			// lblFilter
			// 
			this.lblFilter.AutoSize = true;
			this.lblFilter.Location = new System.Drawing.Point(281, 202);
			this.lblFilter.Name = "lblFilter";
			this.lblFilter.Size = new System.Drawing.Size(29, 13);
			this.lblFilter.TabIndex = 32;
			this.lblFilter.Text = "Filter";
			// 
			// FilesGatherer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Name = "FilesGatherer";
			this.Size = new System.Drawing.Size(420, 300);
			this.Load += new System.EventHandler(this.Loaded);
			this.grpFiles.ResumeLayout(false);
			this.grpFiles.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
