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
	}
}
