using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyControls
{
	/// <summary>
	/// Summary description for LabelAndText.
	/// </summary>
	public class LabelAndText : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label		theLabel;
		private System.Windows.Forms.TextBox	theText;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LabelAndText()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		public override string Text
		{
			get
			{
				return ( theText.Text );
			}
			set
			{
				theText.Text = value;
			}
		}

		public string Label
		{
			get
			{
				return ( theLabel.Text );
			}
			set
			{
				theLabel.Text = value;
			}
		}

		public new int Width
		{
			get
			{
				return ( theText.Width );
			}
			
			set
			{
				// Set the widths of the controls.
				theText.Width	=  value;
				theLabel.Width	=  value;

				base.Width	=  value;
			}
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null )
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
			this.theLabel = new System.Windows.Forms.Label();
			this.theText = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// theLabel
			// 
			this.theLabel.Location = new System.Drawing.Point(0, 0);
			this.theLabel.Name = "theLabel";
			this.theLabel.Size = new System.Drawing.Size(100, 16);
			this.theLabel.TabIndex = 0;
			this.theLabel.Text = "label1";
			// 
			// theText
			// 
			this.theText.Location = new System.Drawing.Point(0, 17);
			this.theText.Name = "theText";
			this.theText.TabIndex = 1;
			this.theText.Text = "";
			// 
			// LabelAndText
			// 
			this.Controls.Add(this.theText);
			this.Controls.Add(this.theLabel);
			this.Name = "LabelAndText";
			this.Size = new System.Drawing.Size(100, 37);
			this.Load += new System.EventHandler(this.LabelAndText_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void LabelAndText_Load( object sender, System.EventArgs e )
		{
			// Resize to fit.
			Width	=  base.Width;
		}

		public new bool Enabled
		{
			get
			{
				return ( base.Enabled );
			}

			set
			{
				theText.Enabled		=  value;
				theLabel.Enabled	=  value;

				base.Enabled	=  value;
			}
		}

		// Implicit conversions to an int.
		public static explicit operator int( LabelAndText lt )
		{
			return ( Int32.Parse( lt.theText.Text ) );
		}

		// Implicit conversions to an int.
		public static explicit operator double( LabelAndText lt )
		{
			return ( double.Parse( lt.theText.Text ) );
		}
	}
}
