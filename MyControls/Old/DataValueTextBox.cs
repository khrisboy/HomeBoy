using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyControls
{
	/// <summary>
	/// Summary description for DataValueTextBox.
	/// </summary>
	public class DataValueTextBox : TextBox
	{
		private string	m_valueText;

		#region Constructors
		
		public DataValueTextBox()
		{
		}

		#endregion Constructors

		#region Properties
		
		public virtual string ValueText
		{
			get
			{
				return ( m_valueText );
			}

			set
			{
				m_valueText	=  value;
			}
		}

		#endregion Properties
	}
}
