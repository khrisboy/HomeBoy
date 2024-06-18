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
	public partial class NikPrintSharpenCtrl : UserControl
	{
		private SharpenInfo	sharpenInfo;

		public NikPrintSharpenCtrl()
		{
			InitializeComponent();

			sharpenInfo	=  new SharpenInfo();

			sharpenInfo.sharpenType	=  SharpenInfo.SharpenType.Nik;
			sharpenInfo.options		=  new NikPrintSharpenInfo();
		}

		public virtual void LoadDefaults( DefaultsAr defaultsAr )
		{
			sharpenInfo.Load( defaultsAr, "Images" );
		}
	
		public virtual void SaveDefaults( DefaultsAr defaultsAr )
		{
			sharpenInfo.Save( defaultsAr, "Images" );
		}

		// Load sharpen data.
		public virtual SharpenInfo LoadSharpenData()
		{
			( (NikPrintSharpenInfo) sharpenInfo.options ).ProfileType		=  SharpenProfile;
			( (NikPrintSharpenInfo) sharpenInfo.options ).PrinterResolution	=  PrinterResolution;
			( (NikPrintSharpenInfo) sharpenInfo.options ).PaperType			=  PaperType;

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

		public NikSharpenInfo.NikProfileType SharpenProfile
		{
			get { return ( chkSharpen.Checked ? (NikSharpenInfo.NikProfileType) cbNikProfileType.SelectedIndex+1 :
												 NikSharpenInfo.NikProfileType.None ); }
			set { cbNikProfileType.SelectedIndex =  ( (int) value )-1; }
		}

		private void chkSharpen_CheckedChanged( object sender, EventArgs e )
		{
			cbNikProfileType.Enabled	=  chkSharpen.Checked;
			cbPaperType.Enabled			=  chkSharpen.Checked;
			cbPrintResolution.Enabled	=  chkSharpen.Checked;
		}

		private void NikSharpenCtrl_Load(object sender, EventArgs e)
		{
			// Defaults.
			PrinterResolution	=  1;
			PaperType			=  4;
			SharpenProfile		=  NikSharpenInfo.NikProfileType.John;

			chkSharpen.Checked	=  true;
		}	
	}
}
