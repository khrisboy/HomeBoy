using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using MyClasses;
using MyControls;
using PhotoshopSupport;

namespace WebFramerCS2ControlLibrary
{
	/// <summary>
	/// Summary description for SharpenCtrl.
	/// </summary>
	public class SharpenCtrlSingle : System.Windows.Forms.UserControl
	{
		#region Member vartiables
		protected System.Windows.Forms.GroupBox grpSharpen;
		protected MyInvisibleGroupBox grpSharpenChoices;
		protected System.Windows.Forms.RadioButton rbSharpenNone;
		protected System.Windows.Forms.RadioButton rbSharpenUnsharpMask;
		protected System.Windows.Forms.RadioButton rbSharpenNik;
		protected System.Windows.Forms.GroupBox grpSharpenOptions;
		protected System.Windows.Forms.ComboBox cbSharpenOptionsNikProfileType;
		protected System.Windows.Forms.TextBox edtSharpenOptionsThreshhold;
		protected System.Windows.Forms.TextBox edtSharpenOptionsRadius;
		protected System.Windows.Forms.Label stSharpenOptionsThreshhold;
		protected System.Windows.Forms.Label stSharpenOptionsRadius;
		protected System.Windows.Forms.TextBox edtSharpenOptionsAmount;
		protected System.Windows.Forms.Label stSharpenOptionsAmount;
		protected System.Windows.Forms.Label stSharpenOptionsNikProfileType;

		// Non-control members.
		protected SharpenInfo	imagesSharpenInfo;
		protected bool			isNikOnly;
		#endregion
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SharpenCtrlSingle()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			imagesSharpenInfo	=  new SharpenInfo();
			isNikOnly			=  false;
		}

		public bool NikOnly
		{
			get { return ( isNikOnly ); }

			set
			{
				if ( isNikOnly != value )
				{
					isNikOnly	=  value;

					rbSharpenUnsharpMask.Enabled	=  !isNikOnly;

					if ( isNikOnly && rbSharpenUnsharpMask.Checked )
					{
						rbSharpenNone.Checked	=  true;
					}
				}
			}
		}

		public virtual void LoadDefaults( DefaultsAr defaultsAr )
		{
			imagesSharpenInfo.Load( defaultsAr, "Images" );
		}
	
		public virtual void SaveDefaults( DefaultsAr defaultsAr )
		{
			imagesSharpenInfo.Save( defaultsAr, "Images" );
		}

		// Load/Unload sharpen data.
		public virtual SharpenInfo LoadSharpenData()
		{
			SharpenInfo	sharpenInfo	=  new SharpenInfo();

			try
			{
				if ( rbSharpenUnsharpMask.Checked )
				{
					sharpenInfo.sharpenType	=  SharpenInfo.SharpenType.Usm;
					sharpenInfo.options		=  new UsmSharpenInfo();
					
					( (UsmSharpenInfo) sharpenInfo.options ).Amount			=  Int32.Parse( edtSharpenOptionsAmount.Text );
					( (UsmSharpenInfo) sharpenInfo.options ).Radius			=  Double.Parse( edtSharpenOptionsRadius.Text );
					( (UsmSharpenInfo) sharpenInfo.options ).Threshhold	=  Int32.Parse( edtSharpenOptionsThreshhold.Text );
				}
				else if ( rbSharpenNik.Checked )
				{
					sharpenInfo.sharpenType	=  SharpenInfo.SharpenType.NikDisplay;
					sharpenInfo.options		=  new NikDisplaySharpenInfo();
					
					( (NikDisplaySharpenInfo) sharpenInfo.options ).ProfileType	=  (NikSharpenInfo.NikProfileType) Enum.Parse( typeof( NikDisplaySharpenInfo.NikProfileType ), cbSharpenOptionsNikProfileType.SelectedItem.ToString() );
					//***( (NikDisplaySharpenInfo) sharpenInfo.options ).Strength	=  Int32.Parse( edtSharpenOptionsStrength.Text );
				}
				else
					sharpenInfo.sharpenType	=  SharpenInfo.SharpenType.None;
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Load Sharpening Data" );
			}

			return ( sharpenInfo );
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null )
					components.Dispose();
			}

			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grpSharpen = new System.Windows.Forms.GroupBox();
			this.grpSharpenChoices = new MyInvisibleGroupBox();
			this.rbSharpenNone = new System.Windows.Forms.RadioButton();
			this.rbSharpenNik = new System.Windows.Forms.RadioButton();
			this.rbSharpenUnsharpMask = new System.Windows.Forms.RadioButton();
			this.grpSharpenOptions = new System.Windows.Forms.GroupBox();
			this.cbSharpenOptionsNikProfileType = new System.Windows.Forms.ComboBox();
			this.edtSharpenOptionsThreshhold = new System.Windows.Forms.TextBox();
			this.edtSharpenOptionsRadius = new System.Windows.Forms.TextBox();
			this.stSharpenOptionsThreshhold = new System.Windows.Forms.Label();
			this.stSharpenOptionsRadius = new System.Windows.Forms.Label();
			this.edtSharpenOptionsAmount = new System.Windows.Forms.TextBox();
			this.stSharpenOptionsAmount = new System.Windows.Forms.Label();
			this.stSharpenOptionsNikProfileType = new System.Windows.Forms.Label();
			this.grpSharpen.SuspendLayout();
			this.grpSharpenChoices.SuspendLayout();
			this.grpSharpenOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpSharpen
			// 
			this.grpSharpen.Controls.Add(this.grpSharpenChoices);
			this.grpSharpen.Controls.Add(this.grpSharpenOptions);
			this.grpSharpen.Location = new System.Drawing.Point(0, 0);
			this.grpSharpen.Name = "grpSharpen";
			this.grpSharpen.Size = new System.Drawing.Size(328, 112);
			this.grpSharpen.TabIndex = 12;
			this.grpSharpen.TabStop = false;
			this.grpSharpen.Text = "Sharpen";
			// 
			// grpSharpenChoices
			// 
			this.grpSharpenChoices.Controls.Add(this.rbSharpenNone);
			this.grpSharpenChoices.Controls.Add(this.rbSharpenNik);
			this.grpSharpenChoices.Controls.Add(this.rbSharpenUnsharpMask);
			this.grpSharpenChoices.Location = new System.Drawing.Point(12, 22);
			this.grpSharpenChoices.Name = "grpSharpenChoices";
			this.grpSharpenChoices.PaintMe = false;
			this.grpSharpenChoices.Size = new System.Drawing.Size(77, 79);
			this.grpSharpenChoices.TabIndex = 12;
			this.grpSharpenChoices.TabStop = false;
			// 
			// rbSharpenNone
			// 
			this.rbSharpenNone.Location = new System.Drawing.Point(11, 3);
			this.rbSharpenNone.Name = "rbSharpenNone";
			this.rbSharpenNone.Size = new System.Drawing.Size(52, 16);
			this.rbSharpenNone.TabIndex = 16;
			this.rbSharpenNone.Text = "None";
			this.rbSharpenNone.CheckedChanged += new System.EventHandler(this.rbSharpenType_CheckedChanged);
			// 
			// rbSharpenNik
			// 
			this.rbSharpenNik.Location = new System.Drawing.Point(11, 30);
			this.rbSharpenNik.Name = "rbSharpenNik";
			this.rbSharpenNik.Size = new System.Drawing.Size(48, 16);
			this.rbSharpenNik.TabIndex = 14;
			this.rbSharpenNik.Text = "nik";
			this.rbSharpenNik.CheckedChanged += new System.EventHandler( this.rbSharpenType_CheckedChanged );
			// 
			// rbSharpenUnsharpMask
			// 
			this.rbSharpenUnsharpMask.Location = new System.Drawing.Point(11, 57);
			this.rbSharpenUnsharpMask.Name = "rbSharpenUnsharpMask";
			this.rbSharpenUnsharpMask.Size = new System.Drawing.Size(48, 16);
			this.rbSharpenUnsharpMask.TabIndex = 15;
			this.rbSharpenUnsharpMask.Text = "USM";
			this.rbSharpenUnsharpMask.CheckedChanged += new System.EventHandler( this.rbSharpenType_CheckedChanged );
			// 
			// grpSharpenOptions
			// 
			this.grpSharpenOptions.Controls.Add(this.cbSharpenOptionsNikProfileType);
			this.grpSharpenOptions.Controls.Add(this.edtSharpenOptionsThreshhold);
			this.grpSharpenOptions.Controls.Add(this.edtSharpenOptionsRadius);
			this.grpSharpenOptions.Controls.Add(this.stSharpenOptionsThreshhold);
			this.grpSharpenOptions.Controls.Add(this.stSharpenOptionsRadius);
			this.grpSharpenOptions.Controls.Add(this.edtSharpenOptionsAmount);
			this.grpSharpenOptions.Controls.Add(this.stSharpenOptionsAmount);
			this.grpSharpenOptions.Controls.Add(this.stSharpenOptionsNikProfileType);
			this.grpSharpenOptions.Location = new System.Drawing.Point(103, 22);
			this.grpSharpenOptions.Name = "grpSharpenOptions";
			this.grpSharpenOptions.Size = new System.Drawing.Size(215, 71);
			this.grpSharpenOptions.TabIndex = 13;
			this.grpSharpenOptions.TabStop = false;
			this.grpSharpenOptions.Text = "Options";
			// 
			// cbSharpenOptionsNikProfileType
			// 
			this.cbSharpenOptionsNikProfileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSharpenOptionsNikProfileType.Items.AddRange(new object[] {
																									 "Anna",
																									 "John",
																									 "Zap"});
			this.cbSharpenOptionsNikProfileType.Location = new System.Drawing.Point(8, 36);
			this.cbSharpenOptionsNikProfileType.Name = "cbSharpenOptionsNikProfileType";
			this.cbSharpenOptionsNikProfileType.Size = new System.Drawing.Size(100, 21);
			this.cbSharpenOptionsNikProfileType.TabIndex = 12;
			// 
			// edtSharpenOptionsThreshhold
			// 
			this.edtSharpenOptionsThreshhold.Location = new System.Drawing.Point(144, 36);
			this.edtSharpenOptionsThreshhold.Name = "edtSharpenOptionsThreshhold";
			this.edtSharpenOptionsThreshhold.Size = new System.Drawing.Size(57, 20);
			this.edtSharpenOptionsThreshhold.TabIndex = 11;
			this.edtSharpenOptionsThreshhold.Text = "";
			// 
			// edtSharpenOptionsRadius
			// 
			this.edtSharpenOptionsRadius.Location = new System.Drawing.Point(76, 36);
			this.edtSharpenOptionsRadius.Name = "edtSharpenOptionsRadius";
			this.edtSharpenOptionsRadius.Size = new System.Drawing.Size(50, 20);
			this.edtSharpenOptionsRadius.TabIndex = 10;
			this.edtSharpenOptionsRadius.Text = "";
			// 
			// stSharpenOptionsThreshhold
			// 
			this.stSharpenOptionsThreshhold.Location = new System.Drawing.Point(142, 20);
			this.stSharpenOptionsThreshhold.Name = "stSharpenOptionsThreshhold";
			this.stSharpenOptionsThreshhold.Size = new System.Drawing.Size(62, 12);
			this.stSharpenOptionsThreshhold.TabIndex = 9;
			this.stSharpenOptionsThreshhold.Text = "Threshhold";
			// 
			// stSharpenOptionsRadius
			// 
			this.stSharpenOptionsRadius.Location = new System.Drawing.Point(76, 20);
			this.stSharpenOptionsRadius.Name = "stSharpenOptionsRadius";
			this.stSharpenOptionsRadius.Size = new System.Drawing.Size(40, 12);
			this.stSharpenOptionsRadius.TabIndex = 8;
			this.stSharpenOptionsRadius.Text = "Radius";
			// 
			// edtSharpenOptionsAmount
			// 
			this.edtSharpenOptionsAmount.Location = new System.Drawing.Point(8, 36);
			this.edtSharpenOptionsAmount.Name = "edtSharpenOptionsAmount";
			this.edtSharpenOptionsAmount.Size = new System.Drawing.Size(50, 20);
			this.edtSharpenOptionsAmount.TabIndex = 7;
			this.edtSharpenOptionsAmount.Text = "";
			// 
			// stSharpenOptionsAmount
			// 
			this.stSharpenOptionsAmount.Location = new System.Drawing.Point(8, 20);
			this.stSharpenOptionsAmount.Name = "stSharpenOptionsAmount";
			this.stSharpenOptionsAmount.Size = new System.Drawing.Size(48, 12);
			this.stSharpenOptionsAmount.TabIndex = 6;
			this.stSharpenOptionsAmount.Text = "Amount";
			// 
			// stSharpenOptionsNikProfileType
			// 
			this.stSharpenOptionsNikProfileType.Location = new System.Drawing.Point(8, 20);
			this.stSharpenOptionsNikProfileType.Name = "stSharpenOptionsNikProfileType";
			this.stSharpenOptionsNikProfileType.Size = new System.Drawing.Size(100, 12);
			this.stSharpenOptionsNikProfileType.TabIndex = 6;
			this.stSharpenOptionsNikProfileType.Text = "Profile Type";
			// 
			// SharpenCtrlSingle
			// 
			this.Controls.Add(this.grpSharpen);
			this.Name = "SharpenCtrl";
			this.Size = new System.Drawing.Size(328, 112);
			this.Load += new System.EventHandler(this.SharpenCtrl_Load);
			this.grpSharpen.ResumeLayout(false);
			this.grpSharpenChoices.ResumeLayout(false);
			this.grpSharpenOptions.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void rbSharpenType_CheckedChanged( object sender, System.EventArgs e )
		{
			if ( rbSharpenUnsharpMask.Checked )
			{
				// Hide the nik options controls
				stSharpenOptionsNikProfileType.Visible	=  false;
				cbSharpenOptionsNikProfileType.Visible	=  false;

				// Show the USM options controls
				stSharpenOptionsAmount.Visible			=  true;
				edtSharpenOptionsAmount.Visible			=  true;
				stSharpenOptionsRadius.Visible			=  true;
				edtSharpenOptionsRadius.Visible			=  true;
				stSharpenOptionsThreshhold.Visible		=  true;
				edtSharpenOptionsThreshhold.Visible		=  true;
			}
			else if ( rbSharpenNik.Checked )
			{
				// Hide the USM options controls
				stSharpenOptionsAmount.Visible				=  false;
				edtSharpenOptionsAmount.Visible				=  false;
				stSharpenOptionsRadius.Visible				=  false;
				edtSharpenOptionsRadius.Visible				=  false;
				stSharpenOptionsThreshhold.Visible			=  false;
				edtSharpenOptionsThreshhold.Visible			=  false;

				// Show the nik options controls
				stSharpenOptionsNikProfileType.Visible		=  true;
				cbSharpenOptionsNikProfileType.Visible		=  true;
			}
			else if ( rbSharpenNone.Checked )
			{
				// Hide all the options controls
				stSharpenOptionsAmount.Visible			=  false;
				edtSharpenOptionsAmount.Visible			=  false;
				stSharpenOptionsRadius.Visible			=  false;
				edtSharpenOptionsRadius.Visible			=  false;
				stSharpenOptionsThreshhold.Visible		=  false;
				edtSharpenOptionsThreshhold.Visible		=  false;
				stSharpenOptionsNikProfileType.Visible	=  false;
				cbSharpenOptionsNikProfileType.Visible	=  false;
			}
			else
			{
				// Force a default to None.
				rbSharpenNone.Checked	=  true;
			}
		}

		private void SharpenCtrl_Load( object sender, System.EventArgs e )
		{
			rbSharpenType_CheckedChanged( sender, e );
		}
	}
}
