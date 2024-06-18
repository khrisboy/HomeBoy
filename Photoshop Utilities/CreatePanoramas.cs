using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Photoshop_Utilities
{
	public class CreatePanoramas : MyControls.MyWindowsForm
	{
		private System.Windows.Forms.PictureBox pbPreview;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.Label stPreview;
		private MyControls.BrowseForDirectory bfdOutput;
		private System.ComponentModel.IContainer components = null;

		public CreatePanoramas()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.pbPreview = new System.Windows.Forms.PictureBox();
			this.btnRun = new System.Windows.Forms.Button();
			this.stPreview = new System.Windows.Forms.Label();
			this.bfdOutput = new MyControls.BrowseForDirectory();
			this.SuspendLayout();
			// 
			// pbPreview
			// 
			this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pbPreview.Location = new System.Drawing.Point(336, 32);
			this.pbPreview.Name = "pbPreview";
			this.pbPreview.Size = new System.Drawing.Size(256, 168);
			this.pbPreview.TabIndex = 0;
			this.pbPreview.TabStop = false;
			// 
			// btnRun
			// 
			this.btnRun.Location = new System.Drawing.Point(248, 456);
			this.btnRun.Name = "btnRun";
			this.btnRun.TabIndex = 1;
			this.btnRun.Text = "&Run";
			// 
			// stPreview
			// 
			this.stPreview.Location = new System.Drawing.Point(336, 16);
			this.stPreview.Name = "stPreview";
			this.stPreview.Size = new System.Drawing.Size(100, 12);
			this.stPreview.TabIndex = 2;
			this.stPreview.Text = "Preview";
			// 
			// bfdOutput
			// 
			this.bfdOutput.BrowseLabel = "...";
			this.bfdOutput.Directory = "";
			this.bfdOutput.Label = "&Output Directory";
			this.bfdOutput.Location = new System.Drawing.Point(16, 256);
			this.bfdOutput.Name = "bfdOutput";
			this.bfdOutput.Size = new System.Drawing.Size(272, 38);
			this.bfdOutput.TabIndex = 3;
			// 
			// CreatePanoramas
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 526);
			this.Controls.Add(this.bfdOutput);
			this.Controls.Add(this.stPreview);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.pbPreview);
			this.Name = "CreatePanoramas";
			this.Text = "Create Panoramas";
			this.ResumeLayout(false);

		}
		#endregion
	}
}

