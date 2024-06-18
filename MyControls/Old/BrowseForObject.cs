using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyControls
{
	/// <summary>
	/// Summary description for BrowseForObject.
	/// </summary>
	public partial class BrowseForObject : System.Windows.Forms.UserControl
	{
		#region Members

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		protected System.Windows.Forms.Label stLabel;
		protected System.Windows.Forms.Button btnBrowse;
		protected DataValueTextBox edtPath;

		// Create an event that can handled for when the text changes.
		public delegate void MyTextChanged( object sender, EventArgs e );
		public event MyTextChanged OnMyTextChanged;

		// Create an event that can be handled when dropped on.
		public delegate void MyDragDrop( object sender, EventArgs e );
		public event MyDragDrop OnMyDragDrop;

		// Create an event that can be handled when browse button is clicked.
		public delegate void MyBrowsed( object sender, EventArgs e );
		public event MyBrowsed OnMyBrowsed;

		protected bool		m_displayData;
		protected bool		m_allowDragDrop;
		private static int	MinWidth =  100;

		#endregion

		#region Constructors
		
		public BrowseForObject()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

// 			OnMyDragDrop	+=  new MyDragDrop( BrowseForObject_OnMyDragDrop );
// 			OnMyTextChanged	+=  new MyTextChanged( BrowseForObject_OnMyTextChanged );

			m_displayData	=  false;
		}

		#endregion Constructors

		#region Properties

		public virtual string TheText
		{
			// We don't make any distinction here. Set both to be the
			// same. (DisplayText is what gets displayed in the control)
			set
			{
				ValueText	=  value;
				DisplayText	=  ValueText;
			}
		}

		protected string DisplayText
		{
			// DisplayText is what's displayed
			get { return ( edtPath.Text ); }
			set { edtPath.Text	=  value; }
		}

		protected string ValueText
		{
			get { return ( edtPath.ValueText ); }
			set { edtPath.ValueText	=  value; }
		}
		
		public string Label
		{
			get { return ( stLabel.Text ); }
			set { stLabel.Text	=  value; }
		}

		public string BrowseLabel
		{
			get { return ( btnBrowse.Text ); }
			set { btnBrowse.Text	=  value; }
		}

		public bool LabelIsVisible
		{
			set { stLabel.Visible	=  value; }
		}
		
		// Overrides.
		new public int Width
		{
			set
			{ 
				// Must be greater than MinWidth
				if ( value >= MinWidth )
				{
					// Make the container big enough.
					base.Width	=  value;

					// Now adjust everyone else.
					edtPath.Width	=  value - ( 10 + btnBrowse.Width );
					btnBrowse.Left	=  edtPath.Width + 10;
					stLabel.Width	=  edtPath.Width;
				}
			}

			get { return ( base.Width ); }
		}

		public new bool Enabled
		{
			get
			{
				return ( base.Enabled );
			}

			set
			{
				base.Enabled		=  value;
				stLabel.Enabled	=  value;
				btnBrowse.Enabled	=  value;
				edtPath.Enabled	=  value;
			}
		}

		#endregion Properties

		#region Methods

		protected virtual bool IsValidType( Object obj )
		{
			return ( true );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.stLabel = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.edtPath = new MyControls.DataValueTextBox();
			this.SuspendLayout();
			// 
			// stLabel
			// 
			this.stLabel.Location = new System.Drawing.Point(0, 0);
			this.stLabel.Name = "stLabel";
			this.stLabel.Size = new System.Drawing.Size(152, 14);
			this.stLabel.TabIndex = 5;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(242, 15);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(30, 22);
			this.btnBrowse.TabIndex = 4;
			this.btnBrowse.Text = "...";
			// 
			// edtPath
			// 
			this.edtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.edtPath.Location = new System.Drawing.Point(3, 15);
			this.edtPath.Name = "edtPath";
			this.edtPath.Size = new System.Drawing.Size(233, 20);
			this.edtPath.TabIndex = 3;
			this.edtPath.ValueText = null;
			this.edtPath.TextChanged += new System.EventHandler(this.edtPath_TextChanged);
			this.edtPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.edtPath_DragDrop);
			this.edtPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.edtPath_DragEnter);
			// 
			// BrowseForObject
			// 
			this.Controls.Add(this.edtPath);
			this.Controls.Add(this.stLabel);
			this.Controls.Add(this.btnBrowse);
			this.Name = "BrowseForObject";
			this.Size = new System.Drawing.Size(272, 38);
			this.Load += new System.EventHandler(this.BrowseForObject_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && components != null )
			{
				components.Dispose();
			}

			base.Dispose( disposing );
		}

		#endregion Methods

		#region Events

		protected void BrowseForObject_Load( object sender, System.EventArgs e )
		{
			// Set text box to allow drops.
			edtPath.AllowDrop =  true;

			Width =  base.Width;
		}

		protected void edtPath_DragEnter( object sender, System.Windows.Forms.DragEventArgs e )
		{
			// If the data is a single file display the copy cursor.
			e.Effect =  DragDropEffects.None;

			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) ) 
			{
				// Don't want more than 1 file!
				// The data is a filename.
				string[] files =  (string[]) e.Data.GetData( DataFormats.FileDrop );

				if ( files.GetLength( 0 ) == 1 )
				{
					// One more check.
					e.Effect =  IsValidType( e.Data.GetData( DataFormats.FileDrop ) ) ?  DragDropEffects.Copy : DragDropEffects.None;
				}
			}
		}

		protected void edtPath_DragDrop( object sender, System.Windows.Forms.DragEventArgs e )
		{
			// Handle FileDrop data.
			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				try
				{
					// The data is a filename.
					string[] files =  (string[]) e.Data.GetData( DataFormats.FileDrop );

					TheText =  files[ 0 ];

					// Fire off a custom event.
					OnMyDragDrop( sender, e );
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message );
				}
			}
		}

		// Custom events.
		public void edtPath_TextChanged( object sender, System.EventArgs e )
		{
			// Raise the custom event
// 			OnMyTextChanged( this, e );
			BrowseForObject_OnMyTextChanged( sender, e );
		}

		private void BrowseForObject_OnMyDragDrop( object sender, EventArgs e )
		{
			if ( OnMyDragDrop != null )
			{
				OnMyDragDrop( sender, e );
			}
		}

		private void BrowseForObject_OnMyTextChanged( object sender, EventArgs e )
		{
			if ( OnMyTextChanged != null )
			{
				OnMyTextChanged( sender, e );
			}
		}

		protected void BrowseForObject_OnMyBrowsed( object sender, EventArgs e )
		{
			if ( OnMyBrowsed != null )
			{
				OnMyBrowsed( sender, e );
			}
		}

		#endregion Events
	}
}
