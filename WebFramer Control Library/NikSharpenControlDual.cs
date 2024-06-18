using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	public partial class NikSharpenControlDual : UserControl
	{
		private SharpenInfo	imagesSharpenInfo;
		private SharpenInfo	thumbsSharpenInfo;

		public NikSharpenControlDual()
		{
			InitializeComponent();

			imagesSharpenInfo	=  new SharpenInfo();
			thumbsSharpenInfo	=  new SharpenInfo();
		}

		public SharpenInfo ImagesSharpenInfo
		{
			get
			{
				imagesSharpenInfo	=  nikSharpenCtrlImages.LoadSharpenData();

				return ( imagesSharpenInfo  );
			}
		}

		public SharpenInfo ThumbsSharpenInfo
		{
			get
			{
				thumbsSharpenInfo	=  nikSharpenCtrlThumbs.LoadSharpenData();

				return ( thumbsSharpenInfo );
			}
		}

		private void rbSharpenImagesThumbs_CheckedChanged( object sender, EventArgs e )
		{
			nikSharpenCtrlImages.Visible	=  rbSharpenImages.Checked;
			nikSharpenCtrlThumbs.Visible	=  !rbSharpenImages.Checked;
		}

		private void NikSharpenControlDual_Load( object sender, EventArgs e )
		{
			// Default to Display for both.
			nikSharpenCtrlImages.DisplaySharpening	=  true;
			nikSharpenCtrlThumbs.DisplaySharpening	=  true;
		}
	}
}
