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
	/// Summary description for BorderCtrl.
	/// </summary>
	public class BorderCtrl : System.Windows.Forms.UserControl
	{
		private MyControls.MyGroupBox grpBorder;
		private MyControls.MyGroupBox grpBorderColor;
		private System.Windows.Forms.TextBox edtBorderOther;
		private System.Windows.Forms.RadioButton rbBorderOther;
		private System.Windows.Forms.RadioButton rbBorderBlack;
		private System.Windows.Forms.Label stBorderWidth;
		private System.Windows.Forms.Label stBorderWidthPx;
		private System.Windows.Forms.TextBox edtBorderWidth;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BorderCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public new bool Enabled
		{
			set
			{
				// By passing this along to the MyGroupBox we can get this to work for everyone.
				grpBorder.Enabled			=  value;
				grpBorderColor.Enabled	=  value;
				base.Enabled				=  value;
			}
		}

		public int BorderWidth
		{
			get
			{
				int	border	=  1;

				try
				{
					border	=  Int32.Parse( edtBorderWidth.Text );
				}
				catch ( Exception )
				{
				}

				return ( border );
			}

			set
			{
				edtBorderWidth.Text	=  value.ToString();
			}
		}

		public RgbColor Color
		{
			get
			{
				RgbColor	color	=  new RgbColor();

				color.Equals( edtBorderOther.Text );

				return ( color );
			}

			set
			{
				if ( value.IsEqual( RgbColor.Black ) )
				{
					rbBorderBlack.Checked	=  true;
					edtBorderOther.Enabled	=  false;
				}
				else
				{
					rbBorderOther.Checked	=  true;
					edtBorderOther.Enabled	=  true;
				}

				edtBorderOther.Text	=  value.hex();
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
			this.grpBorder = new MyControls.MyGroupBox();
			this.grpBorderColor = new MyControls.MyGroupBox();
			this.edtBorderOther = new System.Windows.Forms.TextBox();
			this.rbBorderOther = new System.Windows.Forms.RadioButton();
			this.rbBorderBlack = new System.Windows.Forms.RadioButton();
			this.stBorderWidth = new System.Windows.Forms.Label();
			this.stBorderWidthPx = new System.Windows.Forms.Label();
			this.edtBorderWidth = new System.Windows.Forms.TextBox();
			this.grpBorder.SuspendLayout();
			this.grpBorderColor.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBorder
			// 
			this.grpBorder.Controls.Add( this.grpBorderColor );
			this.grpBorder.Controls.Add( this.stBorderWidth );
			this.grpBorder.Controls.Add( this.stBorderWidthPx );
			this.grpBorder.Controls.Add( this.edtBorderWidth );
			this.grpBorder.Location = new System.Drawing.Point( 0, 0 );
			this.grpBorder.Name = "grpBorder";
			this.grpBorder.Size = new System.Drawing.Size( 168, 136 );
			this.grpBorder.TabIndex = 4;
			this.grpBorder.TabStop = false;
			this.grpBorder.Text = "Border";
			// 
			// grpBorderColor
			// 
			this.grpBorderColor.Controls.Add( this.edtBorderOther );
			this.grpBorderColor.Controls.Add( this.rbBorderOther );
			this.grpBorderColor.Controls.Add( this.rbBorderBlack );
			this.grpBorderColor.Location = new System.Drawing.Point( 16, 46 );
			this.grpBorderColor.Name = "grpBorderColor";
			this.grpBorderColor.Size = new System.Drawing.Size( 136, 76 );
			this.grpBorderColor.TabIndex = 3;
			this.grpBorderColor.TabStop = false;
			this.grpBorderColor.Text = "Color";
			// 
			// edtBorderOther
			// 
			this.edtBorderOther.Location = new System.Drawing.Point( 60, 44 );
			this.edtBorderOther.Name = "edtBorderOther";
			this.edtBorderOther.Size = new System.Drawing.Size( 64, 20 );
			this.edtBorderOther.TabIndex = 2;
			// 
			// rbBorderOther
			// 
			this.rbBorderOther.Location = new System.Drawing.Point( 8, 46 );
			this.rbBorderOther.Name = "rbBorderOther";
			this.rbBorderOther.Size = new System.Drawing.Size( 56, 16 );
			this.rbBorderOther.TabIndex = 1;
			this.rbBorderOther.Text = "Other";
			this.rbBorderOther.CheckedChanged += new System.EventHandler( this.rbBorderOther_CheckedChanged );
			// 
			// rbBorderBlack
			// 
			this.rbBorderBlack.Location = new System.Drawing.Point( 8, 24 );
			this.rbBorderBlack.Name = "rbBorderBlack";
			this.rbBorderBlack.Size = new System.Drawing.Size( 56, 16 );
			this.rbBorderBlack.TabIndex = 0;
			this.rbBorderBlack.Text = "Black";
			this.rbBorderBlack.CheckedChanged += new System.EventHandler( this.rbBorderBlack_CheckedChanged );
			// 
			// stBorderWidth
			// 
			this.stBorderWidth.Location = new System.Drawing.Point( 20, 22 );
			this.stBorderWidth.Name = "stBorderWidth";
			this.stBorderWidth.Size = new System.Drawing.Size( 36, 16 );
			this.stBorderWidth.TabIndex = 0;
			this.stBorderWidth.Text = "Width";
			// 
			// stBorderWidthPx
			// 
			this.stBorderWidthPx.Location = new System.Drawing.Point( 134, 22 );
			this.stBorderWidthPx.Name = "stBorderWidthPx";
			this.stBorderWidthPx.Size = new System.Drawing.Size( 18, 16 );
			this.stBorderWidthPx.TabIndex = 2;
			this.stBorderWidthPx.Text = "px";
			// 
			// edtBorderWidth
			// 
			this.edtBorderWidth.Location = new System.Drawing.Point( 64, 20 );
			this.edtBorderWidth.Name = "edtBorderWidth";
			this.edtBorderWidth.Size = new System.Drawing.Size( 64, 20 );
			this.edtBorderWidth.TabIndex = 1;
			// 
			// BorderCtrl
			// 
			this.Controls.Add( this.grpBorder );
			this.Name = "BorderCtrl";
			this.Size = new System.Drawing.Size( 168, 136 );
			this.grpBorder.ResumeLayout( false );
			this.grpBorder.PerformLayout();
			this.grpBorderColor.ResumeLayout( false );
			this.grpBorderColor.PerformLayout();
			this.ResumeLayout( false );

		}
		#endregion

		private void rbBorderBlack_CheckedChanged(object sender, System.EventArgs e)
		{
			edtBorderOther.Text		=  RgbColor.Black.hex();
			edtBorderOther.Enabled	=  false;
		}

		private void rbBorderOther_CheckedChanged(object sender, System.EventArgs e)
		{
			// Disable the Other edit field.
			if ( rbBorderOther.Checked )
				edtBorderOther.Enabled	=  true;
		}
	}
}
