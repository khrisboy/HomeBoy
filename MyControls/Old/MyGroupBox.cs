using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyControls 
{
	/// <summary>
	/// Enables/Disables child controls based on enabling/disabling of itself.
	/// </summary>
	public class MyGroupBox : GroupBox
	{
		public MyGroupBox()
		{
		}

		public new bool Enabled
		{
			set
			{
				// We assume that enabling/disabling of the GroupBox means the same should be done
				// to all kids.
				Enable( this, value );

				base.Enabled	=  value;
			}
		}

		public void Enable( Control control, bool enable )
		{
			// Now process the child itself.
			control.Enabled	=  enable;

			// Loop through any contained controls.
			foreach ( Control child in control.Controls )
			{
				Enable( child, enable );
			}
		}
	}
}
