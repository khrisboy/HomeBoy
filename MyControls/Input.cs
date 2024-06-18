//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="Input.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyControls
{
	/// <summary>
	/// Summary description for Input.
	/// </summary>
	public class Input : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label stLabel;
		private System.Windows.Forms.TextBox edtText;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Input()
		{
			InitializeComponent();

			Label				=  String.Empty;
			LabelVisible	=  false;
			Caption			=  "Input";
		}

		public string Caption
		{
			get { return ( base.Text ); }
			set { base.Text	=  value; }
		}

		public string Label
		{
			get { return ( stLabel.Text ); }
			set { stLabel.Text	=  value; }
		}

		public new string Text
		{
			get { return ( edtText.Text ); }
			set { edtText.Text	=  value; }
		}

		public bool LabelVisible
		{
			set { stLabel.Visible	=  value; }
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}

			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.stLabel = new System.Windows.Forms.Label();
			this.edtText = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// stLabel
			// 
			this.stLabel.Location = new System.Drawing.Point(12, 12);
			this.stLabel.Name = "stLabel";
			this.stLabel.Size = new System.Drawing.Size(100, 12);
			this.stLabel.TabIndex = 0;
			this.stLabel.Text = "Description";
			// 
			// edtText
			// 
			this.edtText.Location = new System.Drawing.Point(12, 28);
			this.edtText.Name = "edtText";
			this.edtText.Size = new System.Drawing.Size(466, 20);
			this.edtText.TabIndex = 1;
			this.edtText.Text = "";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(137, 69);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "Ok";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(278, 69);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			// 
			// Input
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(490, 111);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.edtText);
			this.Controls.Add(this.stLabel);
			this.Name = "Input";
			this.Load += new System.EventHandler(this.Input_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Input_Load(object sender, System.EventArgs e)
		{
		}
	}
}
