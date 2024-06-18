using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
	public class BrowseForLabelTemplates : MyControls.BrowseForXmlFile
	{
		private System.ComponentModel.IContainer components = null;

		public BrowseForLabelTemplates()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
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
			components = new System.ComponentModel.Container();
		}
		#endregion

		protected override void HandleBrowseClick( object sender, EventArgs e )
		{
			// Pop up the dialog.
			BrowseForLabelTemplatesDlg	dlg	=  new BrowseForLabelTemplatesDlg();
			
			if ( dlg.ShowDialog() == DialogResult.OK ) 
			{
				FileName	=  dlg.Selected.FullName;
			}

			dlg.Dispose();
		}

	}
}

