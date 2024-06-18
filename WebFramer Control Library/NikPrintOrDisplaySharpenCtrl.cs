using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MyClasses;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	public partial class NikPrintOrDisplaySharpenCtrl : UserControl
	{
		private SharpenInfo	sharpenInfo;

		public NikPrintOrDisplaySharpenCtrl()
		{
			InitializeComponent();

			sharpenInfo	=  new SharpenInfo();
		}

		public virtual void LoadDefaults( DefaultsAr defaultsAr )
		{
			sharpenInfo.Load( defaultsAr, "Images" );
		}
	
		public virtual void SaveDefaults( DefaultsAr defaultsAr )
		{
			sharpenInfo.Save( defaultsAr, "Images" );
		}

		// Transfer sharpen data.
		public virtual SharpenInfo LoadSharpenData()
		{
			if ( rbPrint.Checked )
			{
				sharpenInfo.sharpenType	=  SharpenInfo.SharpenType.Nik;
				sharpenInfo.options		=  new NikPrintSharpenInfo();

				( (NikPrintSharpenInfo) sharpenInfo.options ).ProfileType		=  SharpenProfile;
				( (NikPrintSharpenInfo) sharpenInfo.options ).PrinterResolution	=  PrinterResolution;
				( (NikPrintSharpenInfo) sharpenInfo.options ).PaperType			=  PaperType;
			}
			else
			{
				sharpenInfo.sharpenType	=  SharpenInfo.SharpenType.NikDisplay;
				sharpenInfo.options		=  new NikDisplaySharpenInfo();

				( (NikDisplaySharpenInfo) sharpenInfo.options ).ProfileType	=  SharpenProfile;
				( (NikDisplaySharpenInfo) sharpenInfo.options ).Strength	=  Strength;
			}

			return ( sharpenInfo );
		}

		public int PrinterResolution
		{
			get { return ( cbPrintResolution.SelectedIndex ); }
			set { cbPrintResolution.SelectedIndex	=  value; }
		}

		public int PaperType
		{
			get { return ( cbPaperType.SelectedIndex ); }
			set { cbPaperType.SelectedIndex =  value; }
		}

		public int Strength
		{
			get { return ( (int) lsStrength.Value ); }
			set { lsStrength.Value =  value; }
		}

		public bool PrintSharpening
		{
			get { return ( rbPrint.Checked ); }
			set { rbPrint.Checked =  value; }
		}

		public bool DisplaySharpening
		{
			get { return ( rbDisplay.Checked ); }
			set
			{
				rbDisplay.Checked =  value;
				rbPrintDisplay_CheckedChanged( null, null );
			}
		}

		public NikSharpenInfo.NikProfileType SharpenProfile
		{
			get { return ( chkSharpen.Checked ? (NikSharpenInfo.NikProfileType) cbNikProfileType.SelectedIndex+1 :
												 NikSharpenInfo.NikProfileType.None ); }
			set { cbNikProfileType.SelectedIndex =  ( (int) value )-1; }
		}

		private void chkSharpen_CheckedChanged( object sender, EventArgs e )
		{
			if ( rbPrint.Checked )
			{
				cbNikProfileType.Enabled	=  chkSharpen.Checked;
				cbPaperType.Enabled			=  chkSharpen.Checked;
				cbPrintResolution.Enabled	=  chkSharpen.Checked;
			}
			else
			{
				cbNikProfileType.Enabled	=  chkSharpen.Checked;
				lsStrength.Enabled			=  chkSharpen.Checked;
			}
		}

		private void NikSharpenCtrl_Load(object sender, EventArgs e)
		{
			// Defaults.
			PrinterResolution	=  1;
			PaperType			=  4;
			SharpenProfile		=  NikSharpenInfo.NikProfileType.John;
			Strength			=  75;

			chkSharpen.Checked	=  true;
		}

		private void rbPrintDisplay_CheckedChanged( object sender, EventArgs e )
		{
			stPaperType.Visible			=  rbPrint.Checked;
			cbPaperType.Visible			=  rbPrint.Checked;
			stPrintResolution.Visible	=  rbPrint.Checked;
			cbPrintResolution.Visible	=  rbPrint.Checked;
			lsStrength.Visible			=  !rbPrint.Checked;

			chkSharpen_CheckedChanged( sender, e );
		}	
	}
}
