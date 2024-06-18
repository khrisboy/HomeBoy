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
	public partial class NikDisplaySharpenCtrl : UserControl
	{
		private SharpenInfo	sharpenInfo;

		public NikDisplaySharpenCtrl()
		{
			InitializeComponent();

			sharpenInfo	=  new SharpenInfo();

			sharpenInfo.options		=  new NikDisplaySharpenInfo();
			sharpenInfo.sharpenType	=  SharpenInfo.SharpenType.NikDisplay;
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
			( (NikDisplaySharpenInfo) sharpenInfo.options ).ProfileType	=  SharpenProfile;
			( (NikDisplaySharpenInfo) sharpenInfo.options ).Strength	=  Strength;

			return ( sharpenInfo );
		}

		public int Strength
		{
			get { return ( (int) lsStrength.Value ); }
			set { lsStrength.Value =  value; }
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
			lsStrength.Enabled			=  chkSharpen.Checked;
		}

		private void NikDisplaySharpenCtrl_Load(object sender, EventArgs e)
		{
			// Defaults.
			Strength			=  75;
			SharpenProfile		=  NikSharpenInfo.NikProfileType.John;

			chkSharpen.Checked	=  true;
		}	
	}
}
