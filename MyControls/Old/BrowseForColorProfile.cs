using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

using MyClasses;
// using ColorInformer;

namespace MyControls
{
	public class BrowseForColorProfile : BrowseForFile
	{
		public BrowseForColorProfile()
		{
			InitialDirectory	=  @"C:\WINDOWS\system32\spool\drivers\color";
			FilesFilter			=  "ICC files (*.icc)|*.icc|ICM files (*.icm)|*.icm|All files (*.*)|*.*";
			FilterIndex			=  1;
			RestoreDirectory	=  true;

			// Display only the filename portion.
			DisplayFileNameOnly	=  false;
		}

		public string ProfileName
		{
			get
			{
				return ( base.DisplayText );
			}
		}

		public override string TheText
		{
			set
			{
				// Set the ValueText to be the value (FileName).
				base.ValueText	=  value;

				if ( value != null && value != "" )
				{
					m_theToolTip.SetToolTip( edtPath, value );

					// But the DisplayText is the profile name.
					base.DisplayText =  GetProfileName( value );
				}
			}
		}

		private string GetProfileName( string filename )
		{
			string name	=  "";

			try
			{
				if ( filename != null && filename != "" )
				{ 
// 					name	=  ColorProfileInformer.GetProfileName( filename );
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "GetProfileName Call Error" );
			}

			return ( name );
		}
	}
}
