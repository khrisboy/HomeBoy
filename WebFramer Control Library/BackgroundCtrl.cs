using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using MyClasses;
using MyControls;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	/// <summary>
	/// Summary description for BackgroundCtrl.
	/// </summary>
	public class BackgroundCtrl : System.Windows.Forms.UserControl
	{
		private MyControls.MyGroupBox grpBackground;
		private System.Windows.Forms.TextBox edtBackground;
		private System.Windows.Forms.Label stBackground;
		private System.Windows.Forms.Label stBackgroundWidth;
		private System.Windows.Forms.Label stBackgroundPx;
		private System.Windows.Forms.TextBox edtBackgroundWidth;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BackgroundCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public int BackgroundWidth
		{
			get
			{
				int	backgroundWidth	=  10;

				try
				{
					backgroundWidth	=  Int32.Parse( edtBackgroundWidth.Text );
				}
				catch ( Exception )
				{
				}

				return ( backgroundWidth );
			}

			set
			{
				edtBackgroundWidth.Text	=  value.ToString();
			}
		}

		public RgbColor Color
		{
			get
			{
				RgbColor	color	=  new RgbColor();

				color.Equals( edtBackground.Text );

				return ( color );
			}

			set
			{
				edtBackground.Text	=  value.hex();
			}
		}

		public new bool Enabled
		{
			set
			{
				// By passing this along to the MyGroupBox we can get this to work for everyone..
				grpBackground.Enabled	=  value;
				base.Enabled				=  value;
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
			this.grpBackground = new MyControls.MyGroupBox();
			this.edtBackground = new System.Windows.Forms.TextBox();
			this.stBackground = new System.Windows.Forms.Label();
			this.stBackgroundWidth = new System.Windows.Forms.Label();
			this.stBackgroundPx = new System.Windows.Forms.Label();
			this.edtBackgroundWidth = new System.Windows.Forms.TextBox();
			this.grpBackground.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBackground
			// 
			this.grpBackground.Controls.Add( this.edtBackground );
			this.grpBackground.Controls.Add( this.stBackground );
			this.grpBackground.Controls.Add( this.stBackgroundWidth );
			this.grpBackground.Controls.Add( this.stBackgroundPx );
			this.grpBackground.Controls.Add( this.edtBackgroundWidth );
			this.grpBackground.Location = new System.Drawing.Point( 0, 0 );
			this.grpBackground.Name = "grpBackground";
			this.grpBackground.Size = new System.Drawing.Size( 168, 80 );
			this.grpBackground.TabIndex = 5;
			this.grpBackground.TabStop = false;
			this.grpBackground.Text = "Background";
			// 
			// edtBackground
			// 
			this.edtBackground.Location = new System.Drawing.Point( 56, 16 );
			this.edtBackground.Name = "edtBackground";
			this.edtBackground.Size = new System.Drawing.Size( 64, 20 );
			this.edtBackground.TabIndex = 1;
			// 
			// stBackground
			// 
			this.stBackground.Location = new System.Drawing.Point( 12, 20 );
			this.stBackground.Name = "stBackground";
			this.stBackground.Size = new System.Drawing.Size( 32, 16 );
			this.stBackground.TabIndex = 0;
			this.stBackground.Text = "Color";
			// 
			// stBackgroundWidth
			// 
			this.stBackgroundWidth.Location = new System.Drawing.Point( 12, 50 );
			this.stBackgroundWidth.Name = "stBackgroundWidth";
			this.stBackgroundWidth.Size = new System.Drawing.Size( 36, 16 );
			this.stBackgroundWidth.TabIndex = 2;
			this.stBackgroundWidth.Text = "Width";
			// 
			// stBackgroundPx
			// 
			this.stBackgroundPx.Location = new System.Drawing.Point( 128, 50 );
			this.stBackgroundPx.Name = "stBackgroundPx";
			this.stBackgroundPx.Size = new System.Drawing.Size( 19, 16 );
			this.stBackgroundPx.TabIndex = 4;
			this.stBackgroundPx.Text = "px";
			// 
			// edtBackgroundWidth
			// 
			this.edtBackgroundWidth.Location = new System.Drawing.Point( 56, 48 );
			this.edtBackgroundWidth.Name = "edtBackgroundWidth";
			this.edtBackgroundWidth.Size = new System.Drawing.Size( 64, 20 );
			this.edtBackgroundWidth.TabIndex = 3;
			// 
			// BackgroundCtrl
			// 
			this.Controls.Add( this.grpBackground );
			this.Name = "BackgroundCtrl";
			this.Size = new System.Drawing.Size( 168, 80 );
			this.grpBackground.ResumeLayout( false );
			this.grpBackground.PerformLayout();
			this.ResumeLayout( false );

		}
		#endregion
	}
}
