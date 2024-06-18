using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using MyClasses;

namespace MyControls
{
	/// <summary>
	/// Summary description for XYCoordinates.
	/// </summary>
	public class XYCoordinates : System.Windows.Forms.UserControl
	{
		#region Data Members
		private System.Windows.Forms.Label		stCoordY;
		private System.Windows.Forms.Label		stCoordX;
		private System.Windows.Forms.TextBox	edtCoordY;
		private System.Windows.Forms.TextBox	edtCoordX;
		private System.Windows.Forms.Label		stLabel;
		private System.Windows.Forms.Label		stX;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		public XYCoordinates()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public double X
		{
			get
			{
				double	value	=  0;

				try
				{
					value =  double.Parse( edtCoordX.Text );
				}

				catch( Exception )
				{
				}

				return ( value );
			}

			set { edtCoordX.Text	=  value.ToString(); }
		}

		public double Y
		{
			get
			{
				double	value	=  0;

				try
				{
					value =  double.Parse( edtCoordY.Text );
				}

				catch( Exception )
				{
				}

				return ( value );
			}

			set { edtCoordY.Text	=  value.ToString(); }
		}

		public DPoint Point
		{
			get { return new DPoint( X, Y ); }
			
			set
			{
				X	=  value.X;
				Y	=  value.Y;
			}
		}

		public Array Values
		{
			get
			{
				Array location =  Array.CreateInstance( typeof( Double ), 2 );

				location.SetValue( X, 0 );
				location.SetValue( Y, 1 );

				return ( location );
			}
		}

		public string Label
		{
			get { return ( stLabel.Text ); }
			set { stLabel.Text	=  value; }
		}

		public bool XVisible
		{
			get { return ( stX.Visible ); }
			set { stX.Visible	=  value; }
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
			this.stCoordY = new System.Windows.Forms.Label();
			this.stCoordX = new System.Windows.Forms.Label();
			this.edtCoordY = new System.Windows.Forms.TextBox();
			this.edtCoordX = new System.Windows.Forms.TextBox();
			this.stLabel = new System.Windows.Forms.Label();
			this.stX = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// stCoordY
			// 
			this.stCoordY.Location = new System.Drawing.Point(140, 2);
			this.stCoordY.Name = "stCoordY";
			this.stCoordY.Size = new System.Drawing.Size(14, 16);
			this.stCoordY.TabIndex = 9;
			this.stCoordY.Text = "Y";
			// 
			// stCoordX
			// 
			this.stCoordX.Location = new System.Drawing.Point(60, 2);
			this.stCoordX.Name = "stCoordX";
			this.stCoordX.Size = new System.Drawing.Size(14, 16);
			this.stCoordX.TabIndex = 8;
			this.stCoordX.Text = "X";
			// 
			// edtCoordY
			// 
			this.edtCoordY.Location = new System.Drawing.Point(140, 18);
			this.edtCoordY.Name = "edtCoordY";
			this.edtCoordY.Size = new System.Drawing.Size(50, 20);
			this.edtCoordY.TabIndex = 7;
			this.edtCoordY.Text = "";
			// 
			// edtCoordX
			// 
			this.edtCoordX.Location = new System.Drawing.Point(60, 18);
			this.edtCoordX.Name = "edtCoordX";
			this.edtCoordX.Size = new System.Drawing.Size(50, 20);
			this.edtCoordX.TabIndex = 6;
			this.edtCoordX.Text = "";
			// 
			// stLabel
			// 
			this.stLabel.Location = new System.Drawing.Point(0, 21);
			this.stLabel.Name = "stLabel";
			this.stLabel.Size = new System.Drawing.Size(60, 16);
			this.stLabel.TabIndex = 5;
			this.stLabel.Text = "Label 1";
			// 
			// stX
			// 
			this.stX.Location = new System.Drawing.Point(115, 21);
			this.stX.Name = "stX";
			this.stX.Size = new System.Drawing.Size(18, 14);
			this.stX.TabIndex = 10;
			this.stX.Text = "x";
			this.stX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// XYCoordinates
			// 
			this.Controls.Add(this.stX);
			this.Controls.Add(this.stCoordY);
			this.Controls.Add(this.stCoordX);
			this.Controls.Add(this.edtCoordY);
			this.Controls.Add(this.edtCoordX);
			this.Controls.Add(this.stLabel);
			this.Name = "XYCoordinates";
			this.Size = new System.Drawing.Size(194, 42);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
