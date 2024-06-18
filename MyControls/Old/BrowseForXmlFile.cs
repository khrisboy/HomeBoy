using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
	public class BrowseForXmlFile : MyControls.BrowseForFile
	{
		private System.ComponentModel.IContainer components = null;

		public BrowseForXmlFile()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			FilesFilter	=  "Label Template files (*.xml)|*.xml";
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null ) 
				{
					components.Dispose();
				}
			}

			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();

			// 
			// BrowseForXmlFile
			// 
			this.Name = "BrowseForXmlFile";
			this.ResumeLayout(false);

		}
		#endregion

		protected override void HandleBrowseClick( object sender, System.EventArgs e )
		{
			base.HandleBrowseClick( sender, e );
		}
	}
}

