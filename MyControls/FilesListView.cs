//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="FilesListView.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using MyClasses;

namespace MyControls
{
	/// <summary>
	/// Summary description for FilesListView.
	/// </summary>
	public class FilesListView : System.Windows.Forms.UserControl
	{
		#region Members

		public enum SortDirection { Asc, Desc, None };

		protected string		m_filesFilter;
		protected int			m_filterIndex;
		protected string		m_initialDirectory;
		protected List<string>  m_validFileTypes;
		private SortDirection	m_sorting;
		private bool			m_settingSortDirection;

		protected MyControls.MyGroupBox	grpFiles;
		protected Button				btnClear;
		protected ListBox				lbImages;
		protected Button				btnRemove;
		protected Button				btnBrowse;
		protected Label					stImages;
		protected CheckBox				chkFilesSort;
		protected RadioButton			rbFilesSortDescending;
		protected RadioButton			rbFilesSortAscending;
		protected CheckBox				chkClearAutoDrop;

		// Create an event that can handled for when the selection changes.
		public delegate void MySelectionChanged( object sender, EventArgs e );
		public event MySelectionChanged OnMySelectionChanged;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		#endregion

		#region Properties

		public SortDirection Sorting
		{
			get { return ( m_sorting ); }
			
			set
			{
				m_settingSortDirection	=  true;

				m_sorting =  value;

				chkFilesSort.Checked	=  m_sorting != SortDirection.None;

				m_settingSortDirection	=  false;
			}
		}

		public string Title
		{
			get { return ( stImages.Text ); }
			set { stImages.Text	=  value; }
		}

		public string Label
		{
			get { return ( grpFiles.Text ); }
			set { grpFiles.Text	=  value; }
		}

		public virtual string FilesFilter
		{
			get { return ( m_filesFilter ); }
			set { m_filesFilter	=  value; }
		}

		public int FilterIndex
		{
			set { m_filterIndex	=  value; }
		}

		public override AnchorStyles Anchor
		{
			get
			{
				return grpFiles.Anchor;
			}

			set
			{
				grpFiles.Anchor = value;
			}
		}

		public virtual List<string> ValidFileTypes
		{
			get { return ( m_validFileTypes ); }
			set { m_validFileTypes	=  value;}
		}

		public string InitialDirectory
		{
			get { return ( m_initialDirectory ); }
			set { m_initialDirectory = value; }
		}

		public bool MultiSelection
		{
			get
			{
				return ( !(lbImages.SelectionMode == SelectionMode.One) );
			}

			set
			{
				lbImages.SelectionMode =  value ?  SelectionMode.MultiExtended : SelectionMode.One;
			}
		}

		public virtual MyFileInfosArray Files
		{
			get
			{
				// Get the list of files into an array.
				MyFileInfosArray	filesAr	=  (MyFileInfosArray) lbImages.DataSource;

				return ( filesAr );
			}
		}

		public MyFileInfosArray Selected
		{
			get
			{
				MyFileInfosArray infosAr =  new MyFileInfosArray();

				foreach ( Object obj in lbImages.SelectedItems )
				{
					infosAr.Add( obj );
				}

				return ( infosAr );
			}
		}

		public int SelectedIndex
		{
			get
			{
				return (lbImages.SelectedIndex);
			}
		}

		#endregion Properties

		#region Constructors

		public FilesListView()
		{
			m_settingSortDirection =  false;

			InitializeComponent();

			m_filesFilter      =  "Image Files(*.TIF;*.PSD;*.PSB;*.JPG;*.NEF;*.CRW;*.BMP;*.PNG)|*.TIF;*.PSD;*.PSB;*.JPG;*.NEF;*.CRW;*.BMP;*.PNG|Photoshop Files (*PSD;*.PSB)|*.PSD;*.PSB|TIFF Files (*.TIF)|*.TIF|JPEG Files (*.JPG)|*.JPG|Raw Files (*.NEF;*.CRW)|*.NEF;*.CRW|All files (*.*)|*.*";
			m_validFileTypes   =  new List<string>() { ".tif", ".psd", ".psb", ".jpg", ".nef", ".crw", ".bmp", ".png" };
			m_filterIndex      =  1;
			m_initialDirectory =  "c:\\";
			MultiSelection     =  true;

			OnMySelectionChanged +=  new MySelectionChanged( FilesListView_OnMySelectionChanged );
		}

		#endregion Constructors

		#region Methods

		public void Select( MyFileInfo file )
		{
			try
			{
				// Clear any selections first.
				lbImages.ClearSelected();

				int	index =  lbImages.Items.IndexOf( file );

				lbImages.SelectedIndex	=  index;
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Select" );
			}
		}

		public void Select( int index )
		{
			try
			{
				// Clear any selections first.
				lbImages.ClearSelected();

				lbImages.SelectedIndex =  index;
			}

			catch ( Exception ex )
			{
				Messager.Show( ex.Message, "Select" );
			}
		}

		public void Clear()
		{
			// Clear out the listbox.
			if ( lbImages.Items.Count > 0 )
			{
				// Might take a while
				Cursor.Current	=  Cursors.WaitCursor;
	
				// Create an int array with all the indices.
				int[] indexes =  new int[ lbImages.Items.Count ];
	
				for ( int i= 0;  i< lbImages.Items.Count;  i++ )
				{
					indexes[ i ]	=  i;
				}
	
				// Clear 'em out.
				ClearChildren( indexes );
	
				// Restore the cursor.
				Cursor.Current =  Cursors.Default;
			}
		}

		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose( disposing );
		}

		public new bool Enabled
		{
			set
			{
				// We assume that enabling/disabling of the control means the same should be done
				// to all kids.
				Enable( this, value );
			}
		}

		public void Enable( Control control, bool enable )
		{
			// Now process the child itself.
			control.Enabled =  enable;

			// Loop through any contained controls.
			foreach ( Control child in control.Controls )
			{
				Enable( child, enable );
			}
		}

		void Sort()
		{
			MyFileInfosArray	files	=  (MyFileInfosArray) lbImages.DataSource;

			if ( Sorting == SortDirection.Asc )
			{
				files.Sort();
			}
			else if ( Sorting == SortDirection.Desc )
			{
				files.Sort( new DescendingSorter() );
			}

			lbImages.DataSource    =  null;
			lbImages.DataSource    =  files;
			lbImages.DisplayMember =  "Name";
			lbImages.ValueMember   =  "FullName";
		}

		protected virtual void HandleDragEnter( object sender, System.Windows.Forms.DragEventArgs e )
		{
			// If the data is a file(s) of the proper type display the copy cursor.
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) ) 
			{
				// The data is a filename(s).
				string[]	files	=  (string[]) e.Data.GetData( DataFormats.FileDrop );
				bool	isCool	=  false;

				// Check each one for the proper type.
				// We only need 1 to be "valid".
				for ( int i= 0;  i< files.GetLength( 0 );  i++ )
				{
					MyFileInfo	fileInfo	=  new MyFileInfo( files[ i ] );

					if ( IsValidFileType( fileInfo ) )
					{
						isCool	=  true;
						break;
					}
				}

				if ( isCool )
					e.Effect	=  DragDropEffects.Copy;
				else
					e.Effect	=  DragDropEffects.None;
			}
			else
			{
				e.Effect	=  DragDropEffects.None;
			}
		}

		protected virtual void HandleDragDrop( object sender, System.Windows.Forms.DragEventArgs e )
		{
			// Handle FileDrop data.
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				try
				{
					// Auto Clear?
					if ( chkClearAutoDrop.Checked )
						Clear();

					// The data is a filename(s).
					string[]				files	=  ( string[] ) e.Data.GetData( DataFormats.FileDrop );
					MyFileInfosArray	list	=  (MyFileInfosArray) lbImages.DataSource;

					// Load the listbox, but only with allowed file types.
					for ( int i= 0;  i< files.GetLength( 0 );  i++ )
					{
						MyFileInfo	fileInfo	=  new MyFileInfo( files[ i ] );

						if ( IsValidFileType( fileInfo ) )
							list.Add( fileInfo );
					}

					Sort();
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message );
					return;
				}
			}
		}

		protected virtual void HandleBrowseClick( object sender, System.EventArgs e )
		{
			// Bring up dialog to select directory.
			OpenFileDialog	fileDlg	=  new OpenFileDialog();

			fileDlg.InitialDirectory	=  m_initialDirectory;
			fileDlg.Filter					=  m_filesFilter;
			fileDlg.FilterIndex			=  m_filterIndex;
			fileDlg.RestoreDirectory	=  true;
			fileDlg.Multiselect			=  true;

			if ( fileDlg.ShowDialog() == DialogResult.OK ) 
			{
				// Update initial directory.
				m_initialDirectory	=  Path.GetDirectoryName( fileDlg.FileName );
	
				// Update the filename (and path).
				string[]	files	=  fileDlg.FileNames;

				AddFiles( files );
			}

			fileDlg.Dispose();
		}

		protected virtual bool IsValidFileType( MyFileInfo fileInfo )
		{
			// Base default is for graphics files.
			bool isCool	=  false;

			foreach ( string type in ValidFileTypes )
			{
				if ( fileInfo.FileInfo.Extension.ToLower() == type.ToLower() )
				{
					isCool =  true;
					break;
				}
			}

			return ( isCool );
		}

		public void AddFile( string filename )
		{
			string[] files	=  new string[ 1 ];

			files[ 0 ] =  filename;

			AddFiles( files );
		}

		public void AddFiles( string[] files )
		{
			try
			{
				// The data is a filename(s).
				MyFileInfosArray list =   (MyFileInfosArray) lbImages.DataSource;

				// Load the listbox.
				for ( int i= 0;  i< files.GetLength( 0 );  i++ )
				{
					MyFileInfo	fileInfo	=  new MyFileInfo( files[ i ] );

					if ( IsValidFileType( fileInfo ) )
					{
						list.Add( fileInfo );
					}
				}

				Sort();
			}

			catch( Exception ex )
			{
				Messager.Show( ex.Message );
			}
		}

		private void ClearChildren( int[] indexes )
		{
			try
			{
				// Remove selected items from the listbox.
				// Assume that the indices are in ascending order.
				MyFileInfosArray list =  (MyFileInfosArray) lbImages.DataSource;

				for ( int i= indexes.Length-1;  i>= 0;  i-- )
				{
					int	selIndex =  indexes[ i ];

					list.RemoveAt( selIndex );
				}

				// Reset.
				Sort();
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}
		
		#endregion Methods

		#region Events

		private void FilesListView_Load( object sender, System.EventArgs e )
		{
			// Allow drop on the listbox.
			lbImages.AllowDrop =  true;

			// Set the data source for the listbox.
			MyFileInfosArray list =  new MyFileInfosArray();

			lbImages.DataSource	=  list;
		}

		private void lbImages_DragEnter( object sender, System.Windows.Forms.DragEventArgs e )
		{
			HandleDragEnter( sender, e );
		}
		
		private void lbImages_DragDrop( object sender, System.Windows.Forms.DragEventArgs e )
		{
			HandleDragDrop( sender, e );
		}
		
		private void btnBrowse_Click( object sender, System.EventArgs e )
		{
			HandleBrowseClick( sender, e );
		}

		private void lbImages_DoubleClick( object sender, EventArgs e )
		{
			//***HandleImagesDoubleClick( sender, e );
		}

		private void btnRemove_Click( object sender, System.EventArgs e )
		{
			// Remove selected items from the listbox.
			ListBox.SelectedIndexCollection	sel	=  lbImages.SelectedIndices;

			if ( sel.Count == 0 )
			{
				return;
			}

			// Might take a while
			Cursor.Current	=  Cursors.WaitCursor;

			// Transfer to an int array.
			int[] indexes =  new int[ sel.Count ];

			for ( int i= 0;  i< sel.Count;  i++ )
				indexes[ i ] =  sel[ i ];

			// Clear 'em out.
			ClearChildren( indexes );

			// Restore the cursor.
			Cursor.Current =  Cursors.Default;
		}

		private void btnClear_Click( object sender, System.EventArgs e )
		{
			Clear();
		}

		private void FilesListView_OnMySelectionChanged( object sender, EventArgs e )
		{
			// Need at least one in case no one else does.
		}

		private void lbImages_SelectedIndexChanged( object sender, EventArgs e )
		{
			// Raise the custom event.
			OnMySelectionChanged( this, e );
		}

		private void chkFilesSort_CheckedChanged( object sender, EventArgs e )
		{
			if ( !m_settingSortDirection )
			{
				if ( chkFilesSort.Checked )
				{
					m_sorting =  rbFilesSortDescending.Checked ?  SortDirection.Desc : SortDirection.Asc;

					MyFileInfosArray list =  (MyFileInfosArray) lbImages.DataSource;

					if ( list != null )
					{
						Sort();
					}
					
				}
				else
				{
					m_sorting =  SortDirection.None;
				}
			}

			rbFilesSortAscending.Enabled	=  chkFilesSort.Checked;
			rbFilesSortDescending.Enabled	=  chkFilesSort.Checked;
		}

		private void rbFilesSorting_CheckedChanged( object sender, EventArgs e )
		{
			chkFilesSort_CheckedChanged( sender, e );
		}
		
		#endregion

		internal class DescendingSorter : IComparer
		{
			public int Compare( Object x, Object y )
			{
				return ( ( new CaseInsensitiveComparer() ).Compare( y, x ) );
			}
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grpFiles = new MyControls.MyGroupBox();
			this.chkClearAutoDrop = new System.Windows.Forms.CheckBox();
			this.rbFilesSortDescending = new System.Windows.Forms.RadioButton();
			this.rbFilesSortAscending = new System.Windows.Forms.RadioButton();
			this.chkFilesSort = new System.Windows.Forms.CheckBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.lbImages = new System.Windows.Forms.ListBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.stImages = new System.Windows.Forms.Label();
			this.grpFiles.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpFiles
			// 
			this.grpFiles.Controls.Add(this.chkClearAutoDrop);
			this.grpFiles.Controls.Add(this.rbFilesSortDescending);
			this.grpFiles.Controls.Add(this.rbFilesSortAscending);
			this.grpFiles.Controls.Add(this.chkFilesSort);
			this.grpFiles.Controls.Add(this.btnClear);
			this.grpFiles.Controls.Add(this.lbImages);
			this.grpFiles.Controls.Add(this.btnRemove);
			this.grpFiles.Controls.Add(this.btnBrowse);
			this.grpFiles.Controls.Add(this.stImages);
			this.grpFiles.Location = new System.Drawing.Point(0, 0);
			this.grpFiles.Name = "grpFiles";
			this.grpFiles.Size = new System.Drawing.Size(420, 200);
			this.grpFiles.TabIndex = 0;
			this.grpFiles.TabStop = false;
			this.grpFiles.Text = "Files";
			// 
			// chkClearAutoDrop
			// 
			this.chkClearAutoDrop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkClearAutoDrop.AutoSize = true;
			this.chkClearAutoDrop.Location = new System.Drawing.Point(319, 169);
			this.chkClearAutoDrop.Name = "chkClearAutoDrop";
			this.chkClearAutoDrop.Size = new System.Drawing.Size(75, 17);
			this.chkClearAutoDrop.TabIndex = 9;
			this.chkClearAutoDrop.Text = "Auto Clear";
			this.chkClearAutoDrop.UseVisualStyleBackColor = true;
			// 
			// rbFilesSortDescending
			// 
			this.rbFilesSortDescending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbFilesSortDescending.AutoSize = true;
			this.rbFilesSortDescending.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.rbFilesSortDescending.Location = new System.Drawing.Point(381, 28);
			this.rbFilesSortDescending.Name = "rbFilesSortDescending";
			this.rbFilesSortDescending.Size = new System.Drawing.Size(18, 30);
			this.rbFilesSortDescending.TabIndex = 8;
			this.rbFilesSortDescending.Text = "V";
			this.rbFilesSortDescending.UseVisualStyleBackColor = true;
			this.rbFilesSortDescending.CheckedChanged += new System.EventHandler(this.rbFilesSorting_CheckedChanged);
			// 
			// rbFilesSortAscending
			// 
			this.rbFilesSortAscending.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.rbFilesSortAscending.AutoSize = true;
			this.rbFilesSortAscending.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.rbFilesSortAscending.Checked = true;
			this.rbFilesSortAscending.Location = new System.Drawing.Point(360, 28);
			this.rbFilesSortAscending.Name = "rbFilesSortAscending";
			this.rbFilesSortAscending.Size = new System.Drawing.Size(18, 30);
			this.rbFilesSortAscending.TabIndex = 7;
			this.rbFilesSortAscending.TabStop = true;
			this.rbFilesSortAscending.Text = "Λ";
			this.rbFilesSortAscending.UseVisualStyleBackColor = true;
			this.rbFilesSortAscending.CheckedChanged += new System.EventHandler(this.rbFilesSorting_CheckedChanged);
			// 
			// chkFilesSort
			// 
			this.chkFilesSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkFilesSort.Location = new System.Drawing.Point(309, 37);
			this.chkFilesSort.Name = "chkFilesSort";
			this.chkFilesSort.Size = new System.Drawing.Size(45, 16);
			this.chkFilesSort.TabIndex = 3;
			this.chkFilesSort.Text = "Sort";
			this.chkFilesSort.CheckedChanged += new System.EventHandler(this.chkFilesSort_CheckedChanged);
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClear.Location = new System.Drawing.Point(310, 138);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// lbImages
			// 
			this.lbImages.AllowDrop = true;
			this.lbImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbImages.Location = new System.Drawing.Point(16, 37);
			this.lbImages.Name = "lbImages";
			this.lbImages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbImages.Size = new System.Drawing.Size(260, 147);
			this.lbImages.TabIndex = 2;
			this.lbImages.SelectedIndexChanged += new System.EventHandler(this.lbImages_SelectedIndexChanged);
			this.lbImages.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbImages_DragDrop);
			this.lbImages.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbImages_DragEnter);
			this.lbImages.DoubleClick += new System.EventHandler(this.lbImages_DoubleClick);
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemove.Location = new System.Drawing.Point(310, 105);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 5;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(310, 72);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 4;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// stImages
			// 
			this.stImages.Location = new System.Drawing.Point(16, 20);
			this.stImages.Name = "stImages";
			this.stImages.Size = new System.Drawing.Size(260, 14);
			this.stImages.TabIndex = 1;
			this.stImages.Text = "Image Files";
			// 
			// FilesListView
			// 
			this.Controls.Add(this.grpFiles);
			this.Name = "FilesListView";
			this.Size = new System.Drawing.Size(420, 200);
			this.Load += new System.EventHandler(this.FilesListView_Load);
			this.grpFiles.ResumeLayout(false);
			this.grpFiles.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
