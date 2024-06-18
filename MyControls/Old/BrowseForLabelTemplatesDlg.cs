using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using MyClasses;

namespace MyControls
{
	public class BrowseForLabelTemplatesDlg : MyControls.MyWindowsForm
	{
		#region Data Members
		private MyControls.FilesListView				theFilesListView;
		private System.Windows.Forms.Button			btnOk;
		private System.Windows.Forms.Button			btnCancel;
		private System.ComponentModel.IContainer	components	=  null;
		#endregion

		public BrowseForLabelTemplatesDlg()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
		}

		public MyFileInfo	Selected
		{
			get { return ( theFilesListView.Selected[ 0 ] ); }
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
			this.theFilesListView = new MyControls.FilesListView();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// theFilesListView
			// 
			this.theFilesListView.Location = new System.Drawing.Point(16, 16);
			this.theFilesListView.Name = "theFilesListView";
			this.theFilesListView.Size = new System.Drawing.Size(420, 200);
			this.theFilesListView.TabIndex = 0;
			this.theFilesListView.Title = "Label Templates";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(98, 252);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(279, 252);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			// 
			// BrowseForLabelTemplatesDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(452, 332);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.theFilesListView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "BrowseForLabelTemplatesDlg";
			this.Load += new System.EventHandler(this.BrowseForLabelTemplatesDlg_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void BrowseForLabelTemplatesDlg_Load( object sender, System.EventArgs e )
		{
			theFilesListView.FilesFilter    =  "Label Template files (*.xml)|*.xml";
			theFilesListView.ValidFileTypes =  new List<string>() { ".xml" };
		}

		private void btnOk_Click( object sender, System.EventArgs e )
		{
			// Not ok if nothing selected?
			if ( theFilesListView.Selected.Count != 1 )
			{
				DialogResult	=  DialogResult.None;
			}
		}
	}
}

