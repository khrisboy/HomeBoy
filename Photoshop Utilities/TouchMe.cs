using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using MyClasses;
using MyControls;

namespace PhotoshopUtilities
{
	public partial class TouchMe : MyWindowsForm
	{
		[DllImport("User32.dll")]
		public static extern int MessageBox(int h, string m, string c, int type);

// 		[DllImport( "NkImgSDK.dll" )]
// 		public static extern ulong Nkfl_Entry( ulong ulCommand, void* pParam );
	
		bool IsGoodFileTimeAndDate
		{
			get
			{
				bool isGoodFileTimeAndDate =  false;

				return ( isGoodFileTimeAndDate );
			}
		}

		#region Constructors
		
		public TouchMe()
		{
			InitializeComponent();
		}
		
		#endregion Constructors

		void TouchFile( string fileName, DateTime touchTime )
		{
			if ( !String.IsNullOrEmpty( fileName ) )
			{
				MyFileInfo info =  new MyFileInfo( fileName );

				if ( info.FileInfo.Exists )
				{
				}
			}
		}

		#region Events
		
		private void btnTouchMe_Click( object sender, EventArgs e )
		{
			if ( IsGoodFileTimeAndDate )
			{
				DateTime touchTime;

				DateTime.TryParse( "", out touchTime );

				TouchFile( this.bffToFile.FileName, touchTime );
			}
		}
		
		#endregion Events

		private void bffFromFile_OnMyTextChanged( object sender, EventArgs e )
		{
			if ( !String.IsNullOrEmpty( bffFromFile.FileName ) )
			{
				MyFileInfo info =  new MyFileInfo( bffFromFile.FileName );

				if ( info.FileInfo.Exists )
				{
					DateTime creationTime   =  info.FileInfo.CreationTime;
					DateTime lastWriteTime  =  info.FileInfo.LastWriteTime;
					DateTime lastAccessTime =  info.FileInfo.LastAccessTime;

					tbDateAndTime.Text =  creationTime.ToString();
				}
			}
		}
	}
}
