using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyControls
{
	public class TextValueParser
	{
		public static int GetInt( Control control, int defaultValue )
		{
			int value =  defaultValue;

			try
			{
				if ( control is TextBox || control is LabelAndText )
					value =  Int32.Parse( control.Text );
			}

			catch( Exception )
			{
			}

			return ( value );
		}

		public static double GetDouble( Control control, double defaultValue )
		{
			double	value	=  defaultValue;

			try
			{
				if ( control is TextBox || control is LabelAndText )
					value	=  Double.Parse( control.Text );
			}

			catch( Exception )
			{
			}

			return ( value );
		}
	}
}
