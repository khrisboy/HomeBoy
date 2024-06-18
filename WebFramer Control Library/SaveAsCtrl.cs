using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace WebFramerCS2ControlLibrary
{
	/// <summary>
	/// Summary description for SaveAsCtrl.
	/// </summary>
	public class SaveAsCtrl : System.Windows.Forms.UserControl
	{
		#region Member variables
		private System.Windows.Forms.GroupBox		grpSaveAs;
		private System.Windows.Forms.RadioButton	rbSaveAsTIFF;
		private System.Windows.Forms.RadioButton	rbSaveAsJPEG;
		private System.Windows.Forms.Label			stImagesJPEGQuality;
		private System.Windows.Forms.TextBox		edtImagesJPEGQuality;
		private System.Windows.Forms.TrackBar		slImagesJPEGQuality;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public SaveAsCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public int JPEGQuality
		{
			get
			{
				// Default is 12.
				int jpegQuality	=  12;

				try
				{
					jpegQuality	=  Int32.Parse( edtImagesJPEGQuality.Text );
				}
				catch( Exception )
				{
				}

				return ( jpegQuality );
			}

			set
			{
				edtImagesJPEGQuality.Text	=  value.ToString();
			}
		}

		public bool SaveAsJPEG
		{
			get
			{
				return ( rbSaveAsJPEG.Checked );
			}

			set
			{
				if ( value )
					rbSaveAsJPEG.Checked	=  true;
				else
					rbSaveAsTIFF.Checked	=  true;

				edtImagesJPEGQuality.Enabled	=  value;
				slImagesJPEGQuality.Enabled	=  value;
			}
		}

		public bool SaveAsTif
		{
			get
			{
				return ( rbSaveAsTIFF.Checked );
			}

			set
			{
				if ( value )
					rbSaveAsTIFF.Checked	=  true;
				else
					rbSaveAsJPEG.Checked =  true;

				edtImagesJPEGQuality.Enabled	=  value;
				slImagesJPEGQuality.Enabled	=  value;
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
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
			this.grpSaveAs = new System.Windows.Forms.GroupBox();
			this.rbSaveAsTIFF = new System.Windows.Forms.RadioButton();
			this.rbSaveAsJPEG = new System.Windows.Forms.RadioButton();
			this.stImagesJPEGQuality = new System.Windows.Forms.Label();
			this.edtImagesJPEGQuality = new System.Windows.Forms.TextBox();
			this.slImagesJPEGQuality = new System.Windows.Forms.TrackBar();
			this.grpSaveAs.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.slImagesJPEGQuality ) ).BeginInit();
			this.SuspendLayout();
			// 
			// grpSaveAs
			// 
			this.grpSaveAs.Controls.Add( this.rbSaveAsTIFF );
			this.grpSaveAs.Controls.Add( this.rbSaveAsJPEG );
			this.grpSaveAs.Controls.Add( this.stImagesJPEGQuality );
			this.grpSaveAs.Controls.Add( this.edtImagesJPEGQuality );
			this.grpSaveAs.Controls.Add( this.slImagesJPEGQuality );
			this.grpSaveAs.Location = new System.Drawing.Point( 0, 0 );
			this.grpSaveAs.Name = "grpSaveAs";
			this.grpSaveAs.Size = new System.Drawing.Size( 200, 112 );
			this.grpSaveAs.TabIndex = 4;
			this.grpSaveAs.TabStop = false;
			this.grpSaveAs.Text = "Save Images As";
			// 
			// rbSaveAsTIFF
			// 
			this.rbSaveAsTIFF.Location = new System.Drawing.Point( 16, 28 );
			this.rbSaveAsTIFF.Name = "rbSaveAsTIFF";
			this.rbSaveAsTIFF.Size = new System.Drawing.Size( 50, 16 );
			this.rbSaveAsTIFF.TabIndex = 0;
			this.rbSaveAsTIFF.Text = "TIFF";
			this.rbSaveAsTIFF.CheckedChanged += new System.EventHandler( this.rbSaveAsTIFF_CheckedChanged );
			// 
			// rbSaveAsJPEG
			// 
			this.rbSaveAsJPEG.Location = new System.Drawing.Point( 16, 56 );
			this.rbSaveAsJPEG.Name = "rbSaveAsJPEG";
			this.rbSaveAsJPEG.Size = new System.Drawing.Size( 60, 16 );
			this.rbSaveAsJPEG.TabIndex = 1;
			this.rbSaveAsJPEG.Text = "JPEG";
			this.rbSaveAsJPEG.CheckedChanged += new System.EventHandler( this.rbSaveAsJPEG_CheckedChanged );
			// 
			// stImagesJPEGQuality
			// 
			this.stImagesJPEGQuality.Location = new System.Drawing.Point( 76, 32 );
			this.stImagesJPEGQuality.Name = "stImagesJPEGQuality";
			this.stImagesJPEGQuality.Size = new System.Drawing.Size( 40, 16 );
			this.stImagesJPEGQuality.TabIndex = 2;
			this.stImagesJPEGQuality.Text = "Quality";
			// 
			// edtImagesJPEGQuality
			// 
			this.edtImagesJPEGQuality.Location = new System.Drawing.Point( 122, 32 );
			this.edtImagesJPEGQuality.Name = "edtImagesJPEGQuality";
			this.edtImagesJPEGQuality.Size = new System.Drawing.Size( 64, 20 );
			this.edtImagesJPEGQuality.TabIndex = 2;
			this.edtImagesJPEGQuality.TextChanged += new System.EventHandler( this.edtImagesJPEGQuality_TextChanged );
			// 
			// slImagesJPEGQuality
			// 
			this.slImagesJPEGQuality.Location = new System.Drawing.Point( 76, 56 );
			this.slImagesJPEGQuality.Maximum = 12;
			this.slImagesJPEGQuality.Minimum = 1;
			this.slImagesJPEGQuality.Name = "slImagesJPEGQuality";
			this.slImagesJPEGQuality.Size = new System.Drawing.Size( 112, 45 );
			this.slImagesJPEGQuality.TabIndex = 4;
			this.slImagesJPEGQuality.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.slImagesJPEGQuality.Value = 1;
			this.slImagesJPEGQuality.Scroll += new System.EventHandler( this.slImagesJPEGQuality_Scroll );
			// 
			// SaveAsCtrl
			// 
			this.Controls.Add( this.grpSaveAs );
			this.Name = "SaveAsCtrl";
			this.Size = new System.Drawing.Size( 200, 112 );
			this.grpSaveAs.ResumeLayout( false );
			this.grpSaveAs.PerformLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.slImagesJPEGQuality ) ).EndInit();
			this.ResumeLayout( false );

		}
		#endregion

		private void rbSaveAsTIFF_CheckedChanged( object sender, System.EventArgs e )
		{
			slImagesJPEGQuality.Enabled	=  !rbSaveAsTIFF.Checked;
			edtImagesJPEGQuality.Enabled	=  !rbSaveAsTIFF.Checked;
		}

		private void rbSaveAsJPEG_CheckedChanged( object sender, System.EventArgs e )
		{
			slImagesJPEGQuality.Enabled	=  rbSaveAsJPEG.Checked;
			edtImagesJPEGQuality.Enabled	=  rbSaveAsJPEG.Checked;
		}

		public void edtImagesJPEGQuality_TextChanged( object sender, System.EventArgs e )
		{
			// Make sure quality is valid and update slider.
			int	quality	=  12;
			
			try
			{
				quality	=  Int32.Parse( edtImagesJPEGQuality.Text );
			}

			catch( Exception )
			{
			}
		
			if ( quality < 1 || quality > 12 )
			{
			}
			else
			{
				if ( quality != slImagesJPEGQuality.Value )
					slImagesJPEGQuality.Value	=  quality;
			}
		}

		private void slImagesJPEGQuality_Scroll( object sender, System.EventArgs e )
		{
			// Update edit field.
			edtImagesJPEGQuality.Text	=  slImagesJPEGQuality.Value.ToString();
		}
	}
}
