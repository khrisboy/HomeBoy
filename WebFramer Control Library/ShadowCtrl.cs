using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using MyClasses;
using MyControls;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	/// <summary>
	/// Summary description for ShadowCtrl.
	/// </summary>
	public class ShadowCtrl : System.Windows.Forms.UserControl
	{
		private MyControls.MyGroupBox grpShadow;
		private MyControls.MyGroupBox grpShadowColor;
		private System.Windows.Forms.RadioButton rbShadowLGray;
		private System.Windows.Forms.RadioButton rbShadowDGray;
		private System.Windows.Forms.TextBox edtShadowOther;
		private System.Windows.Forms.RadioButton rbShadowOther;
		private System.Windows.Forms.RadioButton rbShadowBlack;
		private System.Windows.Forms.Label stShadowSoftness;
		private System.Windows.Forms.Label stShadowSoftnessPct;
		private System.Windows.Forms.TextBox edtShadowSoftness;
		private System.Windows.Forms.Label stShadowBlur;
		private System.Windows.Forms.Label stShadowBlurPx;
		private System.Windows.Forms.TextBox edtShadowBlur;
		private System.Windows.Forms.Label stShadowWidth;
		private System.Windows.Forms.Label stShadowPx;
		private System.Windows.Forms.TextBox edtShadowWidth;
		private MyControls.MyGroupBox grpShadowDirection;
		private MyInvisibleGroupBox grpShadowLR;
		private System.Windows.Forms.RadioButton rbShadowLeft;
		private System.Windows.Forms.RadioButton rbShadowRight;
		private MyInvisibleGroupBox grpShadowUD;
		private System.Windows.Forms.RadioButton rbShadowUp;
		private System.Windows.Forms.RadioButton rbShadowDown;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ShadowCtrl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public new bool Enabled
		{
			set
			{
				// By passing this along to the MyGroupBox we can get this to work for everyone.
				grpShadow.Enabled	=  value;
				base.Enabled		=  value;
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if (components != null)
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
			this.grpShadow = new MyControls.MyGroupBox();
			this.grpShadowColor = new MyControls.MyGroupBox();
			this.rbShadowLGray = new System.Windows.Forms.RadioButton();
			this.rbShadowDGray = new System.Windows.Forms.RadioButton();
			this.edtShadowOther = new System.Windows.Forms.TextBox();
			this.rbShadowOther = new System.Windows.Forms.RadioButton();
			this.rbShadowBlack = new System.Windows.Forms.RadioButton();
			this.stShadowSoftness = new System.Windows.Forms.Label();
			this.stShadowSoftnessPct = new System.Windows.Forms.Label();
			this.edtShadowSoftness = new System.Windows.Forms.TextBox();
			this.stShadowBlur = new System.Windows.Forms.Label();
			this.stShadowBlurPx = new System.Windows.Forms.Label();
			this.edtShadowBlur = new System.Windows.Forms.TextBox();
			this.stShadowWidth = new System.Windows.Forms.Label();
			this.stShadowPx = new System.Windows.Forms.Label();
			this.edtShadowWidth = new System.Windows.Forms.TextBox();
			this.grpShadowDirection = new MyControls.MyGroupBox();
			this.grpShadowLR = new MyInvisibleGroupBox();
			this.rbShadowLeft = new System.Windows.Forms.RadioButton();
			this.rbShadowRight = new System.Windows.Forms.RadioButton();
			this.grpShadowUD = new MyInvisibleGroupBox();
			this.rbShadowUp = new System.Windows.Forms.RadioButton();
			this.rbShadowDown = new System.Windows.Forms.RadioButton();
			this.grpShadow.SuspendLayout();
			this.grpShadowColor.SuspendLayout();
			this.grpShadowDirection.SuspendLayout();
			this.grpShadowLR.SuspendLayout();
			this.grpShadowUD.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpShadow
			// 
			this.grpShadow.Controls.Add( this.grpShadowColor );
			this.grpShadow.Controls.Add( this.stShadowSoftness );
			this.grpShadow.Controls.Add( this.stShadowSoftnessPct );
			this.grpShadow.Controls.Add( this.edtShadowSoftness );
			this.grpShadow.Controls.Add( this.stShadowBlur );
			this.grpShadow.Controls.Add( this.stShadowBlurPx );
			this.grpShadow.Controls.Add( this.edtShadowBlur );
			this.grpShadow.Controls.Add( this.stShadowWidth );
			this.grpShadow.Controls.Add( this.stShadowPx );
			this.grpShadow.Controls.Add( this.edtShadowWidth );
			this.grpShadow.Controls.Add( this.grpShadowDirection );
			this.grpShadow.Location = new System.Drawing.Point( 0, 0 );
			this.grpShadow.Name = "grpShadow";
			this.grpShadow.Size = new System.Drawing.Size( 360, 216 );
			this.grpShadow.TabIndex = 0;
			this.grpShadow.TabStop = false;
			this.grpShadow.Text = "Shadow";
			// 
			// grpShadowColor
			// 
			this.grpShadowColor.Controls.Add( this.rbShadowLGray );
			this.grpShadowColor.Controls.Add( this.rbShadowDGray );
			this.grpShadowColor.Controls.Add( this.edtShadowOther );
			this.grpShadowColor.Controls.Add( this.rbShadowOther );
			this.grpShadowColor.Controls.Add( this.rbShadowBlack );
			this.grpShadowColor.Location = new System.Drawing.Point( 192, 24 );
			this.grpShadowColor.Name = "grpShadowColor";
			this.grpShadowColor.Size = new System.Drawing.Size( 144, 168 );
			this.grpShadowColor.TabIndex = 10;
			this.grpShadowColor.TabStop = false;
			this.grpShadowColor.Text = "Color";
			// 
			// rbShadowLGray
			// 
			this.rbShadowLGray.Location = new System.Drawing.Point( 12, 96 );
			this.rbShadowLGray.Name = "rbShadowLGray";
			this.rbShadowLGray.Size = new System.Drawing.Size( 79, 20 );
			this.rbShadowLGray.TabIndex = 2;
			this.rbShadowLGray.Text = "Light Gray";
			this.rbShadowLGray.CheckedChanged += new System.EventHandler( this.rbShadowLGray_CheckedChanged );
			// 
			// rbShadowDGray
			// 
			this.rbShadowDGray.Location = new System.Drawing.Point( 12, 60 );
			this.rbShadowDGray.Name = "rbShadowDGray";
			this.rbShadowDGray.Size = new System.Drawing.Size( 79, 20 );
			this.rbShadowDGray.TabIndex = 1;
			this.rbShadowDGray.Text = "Dark Gray";
			this.rbShadowDGray.CheckedChanged += new System.EventHandler( this.rbShadowDGray_CheckedChanged );
			// 
			// edtShadowOther
			// 
			this.edtShadowOther.Location = new System.Drawing.Point( 67, 133 );
			this.edtShadowOther.Name = "edtShadowOther";
			this.edtShadowOther.Size = new System.Drawing.Size( 64, 20 );
			this.edtShadowOther.TabIndex = 4;
			// 
			// rbShadowOther
			// 
			this.rbShadowOther.Location = new System.Drawing.Point( 12, 132 );
			this.rbShadowOther.Name = "rbShadowOther";
			this.rbShadowOther.Size = new System.Drawing.Size( 56, 20 );
			this.rbShadowOther.TabIndex = 3;
			this.rbShadowOther.Text = "Other";
			this.rbShadowOther.CheckedChanged += new System.EventHandler( this.rbShadowOther_CheckedChanged );
			// 
			// rbShadowBlack
			// 
			this.rbShadowBlack.Location = new System.Drawing.Point( 12, 24 );
			this.rbShadowBlack.Name = "rbShadowBlack";
			this.rbShadowBlack.Size = new System.Drawing.Size( 66, 20 );
			this.rbShadowBlack.TabIndex = 0;
			this.rbShadowBlack.Text = "Black";
			this.rbShadowBlack.CheckedChanged += new System.EventHandler( this.rbShadowBlack_CheckedChanged );
			// 
			// stShadowSoftness
			// 
			this.stShadowSoftness.Location = new System.Drawing.Point( 16, 86 );
			this.stShadowSoftness.Name = "stShadowSoftness";
			this.stShadowSoftness.Size = new System.Drawing.Size( 48, 16 );
			this.stShadowSoftness.TabIndex = 6;
			this.stShadowSoftness.Text = "Softness";
			// 
			// stShadowSoftnessPct
			// 
			this.stShadowSoftnessPct.Location = new System.Drawing.Point( 144, 86 );
			this.stShadowSoftnessPct.Name = "stShadowSoftnessPct";
			this.stShadowSoftnessPct.Size = new System.Drawing.Size( 24, 16 );
			this.stShadowSoftnessPct.TabIndex = 8;
			this.stShadowSoftnessPct.Text = "px";
			// 
			// edtShadowSoftness
			// 
			this.edtShadowSoftness.Location = new System.Drawing.Point( 72, 84 );
			this.edtShadowSoftness.Name = "edtShadowSoftness";
			this.edtShadowSoftness.Size = new System.Drawing.Size( 64, 20 );
			this.edtShadowSoftness.TabIndex = 7;
			// 
			// stShadowBlur
			// 
			this.stShadowBlur.Location = new System.Drawing.Point( 16, 54 );
			this.stShadowBlur.Name = "stShadowBlur";
			this.stShadowBlur.Size = new System.Drawing.Size( 36, 16 );
			this.stShadowBlur.TabIndex = 3;
			this.stShadowBlur.Text = "Blur";
			// 
			// stShadowBlurPx
			// 
			this.stShadowBlurPx.Location = new System.Drawing.Point( 144, 54 );
			this.stShadowBlurPx.Name = "stShadowBlurPx";
			this.stShadowBlurPx.Size = new System.Drawing.Size( 24, 16 );
			this.stShadowBlurPx.TabIndex = 5;
			this.stShadowBlurPx.Text = "px";
			// 
			// edtShadowBlur
			// 
			this.edtShadowBlur.Location = new System.Drawing.Point( 72, 52 );
			this.edtShadowBlur.Name = "edtShadowBlur";
			this.edtShadowBlur.Size = new System.Drawing.Size( 64, 20 );
			this.edtShadowBlur.TabIndex = 4;
			// 
			// stShadowWidth
			// 
			this.stShadowWidth.Location = new System.Drawing.Point( 16, 22 );
			this.stShadowWidth.Name = "stShadowWidth";
			this.stShadowWidth.Size = new System.Drawing.Size( 36, 16 );
			this.stShadowWidth.TabIndex = 0;
			this.stShadowWidth.Text = "Width";
			// 
			// stShadowPx
			// 
			this.stShadowPx.Location = new System.Drawing.Point( 144, 22 );
			this.stShadowPx.Name = "stShadowPx";
			this.stShadowPx.Size = new System.Drawing.Size( 24, 16 );
			this.stShadowPx.TabIndex = 2;
			this.stShadowPx.Text = "px";
			// 
			// edtShadowWidth
			// 
			this.edtShadowWidth.Location = new System.Drawing.Point( 72, 20 );
			this.edtShadowWidth.Name = "edtShadowWidth";
			this.edtShadowWidth.Size = new System.Drawing.Size( 64, 20 );
			this.edtShadowWidth.TabIndex = 1;
			// 
			// grpShadowDirection
			// 
			this.grpShadowDirection.Controls.Add( this.grpShadowLR );
			this.grpShadowDirection.Controls.Add( this.grpShadowUD );
			this.grpShadowDirection.Location = new System.Drawing.Point( 12, 116 );
			this.grpShadowDirection.Name = "grpShadowDirection";
			this.grpShadowDirection.Size = new System.Drawing.Size( 160, 88 );
			this.grpShadowDirection.TabIndex = 9;
			this.grpShadowDirection.TabStop = false;
			this.grpShadowDirection.Text = "Shadow";
			// 
			// grpShadowLR
			// 
			this.grpShadowLR.Controls.Add( this.rbShadowLeft );
			this.grpShadowLR.Controls.Add( this.rbShadowRight );
			this.grpShadowLR.Location = new System.Drawing.Point( 10, 20 );
			this.grpShadowLR.Name = "grpShadowLR";
			this.grpShadowLR.PaintMe = false;
			this.grpShadowLR.Size = new System.Drawing.Size( 70, 66 );
			this.grpShadowLR.TabIndex = 0;
			this.grpShadowLR.TabStop = false;
			this.grpShadowLR.Text = "Shadow";
			// 
			// rbShadowLeft
			// 
			this.rbShadowLeft.Location = new System.Drawing.Point( 12, 6 );
			this.rbShadowLeft.Name = "rbShadowLeft";
			this.rbShadowLeft.Size = new System.Drawing.Size( 48, 20 );
			this.rbShadowLeft.TabIndex = 0;
			this.rbShadowLeft.Text = "Left";
			// 
			// rbShadowRight
			// 
			this.rbShadowRight.Location = new System.Drawing.Point( 12, 36 );
			this.rbShadowRight.Name = "rbShadowRight";
			this.rbShadowRight.Size = new System.Drawing.Size( 51, 20 );
			this.rbShadowRight.TabIndex = 1;
			this.rbShadowRight.Text = "Right";
			// 
			// grpShadowUD
			// 
			this.grpShadowUD.Controls.Add( this.rbShadowUp );
			this.grpShadowUD.Controls.Add( this.rbShadowDown );
			this.grpShadowUD.Location = new System.Drawing.Point( 86, 20 );
			this.grpShadowUD.Name = "grpShadowUD";
			this.grpShadowUD.PaintMe = false;
			this.grpShadowUD.Size = new System.Drawing.Size( 70, 66 );
			this.grpShadowUD.TabIndex = 1;
			this.grpShadowUD.TabStop = false;
			this.grpShadowUD.Text = "Shadow";
			// 
			// rbShadowUp
			// 
			this.rbShadowUp.Location = new System.Drawing.Point( 12, 6 );
			this.rbShadowUp.Name = "rbShadowUp";
			this.rbShadowUp.Size = new System.Drawing.Size( 48, 20 );
			this.rbShadowUp.TabIndex = 0;
			this.rbShadowUp.Text = "Up";
			// 
			// rbShadowDown
			// 
			this.rbShadowDown.Location = new System.Drawing.Point( 12, 36 );
			this.rbShadowDown.Name = "rbShadowDown";
			this.rbShadowDown.Size = new System.Drawing.Size( 52, 20 );
			this.rbShadowDown.TabIndex = 1;
			this.rbShadowDown.Text = "Down";
			// 
			// ShadowCtrl
			// 
			this.Controls.Add( this.grpShadow );
			this.Name = "ShadowCtrl";
			this.Size = new System.Drawing.Size( 360, 216 );
			this.grpShadow.ResumeLayout( false );
			this.grpShadow.PerformLayout();
			this.grpShadowColor.ResumeLayout( false );
			this.grpShadowColor.PerformLayout();
			this.grpShadowDirection.ResumeLayout( false );
			this.grpShadowLR.ResumeLayout( false );
			this.grpShadowUD.ResumeLayout( false );
			this.ResumeLayout( false );

		}
		#endregion

		private void rbShadowBlack_CheckedChanged(object sender, System.EventArgs e)
		{
			edtShadowOther.Text		=  RgbColor.Black.hex();
			edtShadowOther.Enabled	=  false;
		}

		private void rbShadowDGray_CheckedChanged(object sender, System.EventArgs e)
		{
			edtShadowOther.Text		=  RgbColor.DarkGray.hex();
			edtShadowOther.Enabled	=  false;
		}

		private void rbShadowLGray_CheckedChanged(object sender, System.EventArgs e)
		{
			edtShadowOther.Text		=  RgbColor.LightGray.hex();
			edtShadowOther.Enabled	=  false;
		}

		private void rbShadowOther_CheckedChanged(object sender, System.EventArgs e)
		{
			edtShadowOther.Enabled	=  true;
		}

		public int ShadowWidth
		{
			get
			{
				int	shadowWidth	=  0;

				try
				{
					shadowWidth	=  Int32.Parse( edtShadowWidth.Text );
				}
				catch ( Exception )
				{
				}

				return ( shadowWidth );
			}

			set
			{
				edtShadowWidth.Text	=  value.ToString();
			}
		}

		public int Blur
		{
			get
			{
				int	shadowBlur	=  0;

				try
				{
					shadowBlur	=  Int32.Parse( edtShadowBlur.Text );
				}
				catch ( Exception )
				{
				}

				return ( shadowBlur );
			}

			set
			{
				edtShadowBlur.Text	=  value.ToString();
			}
		}

		public int Softness
		{
			get
			{
				int shadowSoftness	=  0;

				try
				{
					shadowSoftness	=  Int32.Parse( edtShadowSoftness.Text );
				}
				catch ( Exception )
				{
				}

				return ( shadowSoftness );
			}

			set
			{
				edtShadowSoftness.Text	=  value.ToString();
			}
		}

		public bool ShadowIsRight
		{
			get
			{
				return ( rbShadowRight.Checked );
			}

			set
			{
				if ( value )
					rbShadowRight.Checked	=  true;
				else
					rbShadowLeft.Checked		=  true;
			}
		}

		public bool ShadowIsDown
		{
			get
			{
				return ( rbShadowDown.Checked );
			}

			set
			{
				if ( value )
					rbShadowDown.Checked	=  true;
				else
					rbShadowUp.Checked	=  true;
			}
		}

		public RgbColor Color
		{
			get
			{
				RgbColor	color	=  new RgbColor();

				color.Equals( edtShadowOther.Text );

				return ( color );
			}

			set
			{
				if ( value.IsEqual( RgbColor.Black ) )
				{
					rbShadowBlack.Checked	=  true;
					edtShadowOther.Enabled	=  false;
				}
				else if ( value.IsEqual( RgbColor.DarkGray ) )
				{
					rbShadowDGray.Checked	=  true;
					edtShadowOther.Enabled	=  false;
				}
				else if ( value.IsEqual( RgbColor.LightGray ) )
				{
					rbShadowLGray.Checked	=  true;
					edtShadowOther.Enabled	=  false;
				}
				else
				{
					rbShadowOther.Checked	=  true;
					edtShadowOther.Enabled	=  true;
				}

				edtShadowOther.Text	=  value.hex();
			}
		}
	}
}
