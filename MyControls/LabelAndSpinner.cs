//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="LabelAndSpinner.cs">
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

namespace MyControls
{
	public partial class LabelAndSpinner : UserControl
	{
		public LabelAndSpinner()
		{
			InitializeComponent();

			stLabel.AutoSize = true;

			stLabel.SizeChanged += StLabel_SizeChanged;

			udSpinner.Anchor = AnchorStyles.Right;
		}

		private void StLabel_SizeChanged( object sender, EventArgs e )
		{
			Width =  stLabel.Width + udSpinner.Width + 10;
			udSpinner.Left = stLabel.Width + 2;
		}

		public decimal Min
		{
			get { return ( udSpinner.Minimum ); }
			set { udSpinner.Minimum	=  value; }
		}

		public decimal Max
		{
			get { return ( udSpinner.Maximum ); }
			set { udSpinner.Maximum =  value; }
		}

		public decimal Increment
		{
			get { return ( udSpinner.Increment ); }
			set { udSpinner.Increment =  value; }
		}

		public decimal Value
		{
			get { return ( udSpinner.Value ); }
			set { udSpinner.Value =  value; }
		}

		public string Label
		{
			get { return ( stLabel.Text ); }
			set { stLabel.Text =  value; }
		}

		public int SpinnerWidth
		{
			get { return (udSpinner.Width); }
			set
			{
				Width = value+stLabel.Width+10;
				udSpinner.Width =  value;
				udSpinner.Left = stLabel.Width + 2;
			}
		}

// 		public int LabelWidth
// 		{
// 			get { return (stLabel.Width); }
// 			set
// 			{
// 				Width = value+udSpinner.Width+20;
// 				stLabel.Width =  value;
// 			}
// 		}

// 		public new int Width
// 		{
// 			get 
// 			{
// 				return ( stLabel.Width + udSpinner.Width );
// 			}
// 
// 			set
// 			{
// 				Width = value;
// 			}
// 		}
	}
}
