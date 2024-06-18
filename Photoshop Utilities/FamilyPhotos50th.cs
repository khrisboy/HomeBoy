using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;

namespace PhotoshopUtilities
{
	public class FamilyPhotos50th : MyControls.MyWindowsForm
	{
		#region Data Members
		private Photoshopper.Photoshopper			ps;
		private Photoshop.Application					psApp;
		private System.Windows.Forms.GroupBox		groupBox1;
		private System.Windows.Forms.GroupBox		groupBox2;
		private System.Windows.Forms.Label			label1;
		private System.Windows.Forms.TextBox		edtBaseName;
		private System.Windows.Forms.GroupBox		groupBox3;
		private System.Windows.Forms.RadioButton	rbImage100;
		private System.Windows.Forms.RadioButton rbImage190;
		private System.Windows.Forms.RadioButton rbImage180;
		private System.Windows.Forms.RadioButton rbImage170;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton rbImage270;
		private System.Windows.Forms.RadioButton rbImage280;
		private System.Windows.Forms.RadioButton rbImage290;
		private System.Windows.Forms.RadioButton rbImage200;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton rbImage370;
		private System.Windows.Forms.RadioButton rbImage380;
		private System.Windows.Forms.RadioButton rbImage390;
		private System.Windows.Forms.RadioButton rbImage300;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.RadioButton rbImage470;
		private System.Windows.Forms.RadioButton rbImage480;
		private System.Windows.Forms.RadioButton rbImage490;
		private System.Windows.Forms.RadioButton rbImage400;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCCWAssignsRGB;
		private System.Windows.Forms.Button btnCWAssignsRGB;
		private MyControls.BrowseForDirectory browseForDirectory;
		private System.Windows.Forms.CheckBox chkCloseMain;
		private System.Windows.Forms.RadioButton rbImage1Unknown;
		private System.Windows.Forms.RadioButton rbImage2Unknown;
		private System.Windows.Forms.RadioButton rbImage3Unknown;
		private System.Windows.Forms.RadioButton rbImage4Unknown;
		private System.ComponentModel.IContainer components = null;
		#endregion

		public FamilyPhotos50th()
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
			this.btnCCWAssignsRGB = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.chkCloseMain = new System.Windows.Forms.CheckBox();
			this.btnCWAssignsRGB = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.browseForDirectory = new MyControls.BrowseForDirectory();
			this.btnSave = new System.Windows.Forms.Button();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.rbImage4Unknown = new System.Windows.Forms.RadioButton();
			this.rbImage470 = new System.Windows.Forms.RadioButton();
			this.rbImage480 = new System.Windows.Forms.RadioButton();
			this.rbImage490 = new System.Windows.Forms.RadioButton();
			this.rbImage400 = new System.Windows.Forms.RadioButton();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.rbImage3Unknown = new System.Windows.Forms.RadioButton();
			this.rbImage370 = new System.Windows.Forms.RadioButton();
			this.rbImage380 = new System.Windows.Forms.RadioButton();
			this.rbImage390 = new System.Windows.Forms.RadioButton();
			this.rbImage300 = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.rbImage1Unknown = new System.Windows.Forms.RadioButton();
			this.rbImage170 = new System.Windows.Forms.RadioButton();
			this.rbImage180 = new System.Windows.Forms.RadioButton();
			this.rbImage190 = new System.Windows.Forms.RadioButton();
			this.rbImage100 = new System.Windows.Forms.RadioButton();
			this.edtBaseName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.rbImage2Unknown = new System.Windows.Forms.RadioButton();
			this.rbImage270 = new System.Windows.Forms.RadioButton();
			this.rbImage280 = new System.Windows.Forms.RadioButton();
			this.rbImage290 = new System.Windows.Forms.RadioButton();
			this.rbImage200 = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCCWAssignsRGB
			// 
			this.btnCCWAssignsRGB.Location = new System.Drawing.Point(32, 28);
			this.btnCCWAssignsRGB.Name = "btnCCWAssignsRGB";
			this.btnCCWAssignsRGB.Size = new System.Drawing.Size(115, 23);
			this.btnCCWAssignsRGB.TabIndex = 0;
			this.btnCCWAssignsRGB.Text = "CCW, Assign sRGB";
			this.btnCCWAssignsRGB.Click += new System.EventHandler(this.btnCCWAssignsRGB_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkCloseMain);
			this.groupBox1.Controls.Add(this.btnCWAssignsRGB);
			this.groupBox1.Controls.Add(this.btnCCWAssignsRGB);
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(321, 86);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Main Scan";
			// 
			// chkCloseMain
			// 
			this.chkCloseMain.Location = new System.Drawing.Point(108, 62);
			this.chkCloseMain.Name = "chkCloseMain";
			this.chkCloseMain.Size = new System.Drawing.Size(104, 17);
			this.chkCloseMain.TabIndex = 2;
			this.chkCloseMain.Text = "Close if only file";
			// 
			// btnCWAssignsRGB
			// 
			this.btnCWAssignsRGB.Location = new System.Drawing.Point(174, 28);
			this.btnCWAssignsRGB.Name = "btnCWAssignsRGB";
			this.btnCWAssignsRGB.Size = new System.Drawing.Size(115, 23);
			this.btnCWAssignsRGB.TabIndex = 1;
			this.btnCWAssignsRGB.Text = "CW, Assign sRGB";
			this.btnCWAssignsRGB.Click += new System.EventHandler(this.btnCWAssignsRGB_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.browseForDirectory);
			this.groupBox2.Controls.Add(this.btnSave);
			this.groupBox2.Controls.Add(this.groupBox6);
			this.groupBox2.Controls.Add(this.groupBox5);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.edtBaseName);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.groupBox4);
			this.groupBox2.Location = new System.Drawing.Point(16, 113);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(364, 421);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Individual Images";
			// 
			// browseForDirectory
			// 
			this.browseForDirectory.BrowseLabel = "...";
			this.browseForDirectory.Location = new System.Drawing.Point(20, 358);
			this.browseForDirectory.Name = "browseForDirectory";
			this.browseForDirectory.Size = new System.Drawing.Size(272, 38);
			this.browseForDirectory.TabIndex = 8;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(207, 30);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.rbImage4Unknown);
			this.groupBox6.Controls.Add(this.rbImage470);
			this.groupBox6.Controls.Add(this.rbImage480);
			this.groupBox6.Controls.Add(this.rbImage490);
			this.groupBox6.Controls.Add(this.rbImage400);
			this.groupBox6.Location = new System.Drawing.Point(20, 283);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(322, 60);
			this.groupBox6.TabIndex = 6;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Image 3";
			// 
			// rbImage4Unknown
			// 
			this.rbImage4Unknown.Location = new System.Drawing.Point(266, 24);
			this.rbImage4Unknown.Name = "rbImage4Unknown";
			this.rbImage4Unknown.Size = new System.Drawing.Size(41, 20);
			this.rbImage4Unknown.TabIndex = 5;
			this.rbImage4Unknown.Text = "???";
			// 
			// rbImage470
			// 
			this.rbImage470.Location = new System.Drawing.Point(204, 24);
			this.rbImage470.Name = "rbImage470";
			this.rbImage470.Size = new System.Drawing.Size(54, 20);
			this.rbImage470.TabIndex = 3;
			this.rbImage470.Text = "1970s";
			// 
			// rbImage480
			// 
			this.rbImage480.Location = new System.Drawing.Point(144, 24);
			this.rbImage480.Name = "rbImage480";
			this.rbImage480.Size = new System.Drawing.Size(54, 20);
			this.rbImage480.TabIndex = 2;
			this.rbImage480.Text = "1980s";
			// 
			// rbImage490
			// 
			this.rbImage490.Location = new System.Drawing.Point(84, 24);
			this.rbImage490.Name = "rbImage490";
			this.rbImage490.Size = new System.Drawing.Size(54, 20);
			this.rbImage490.TabIndex = 1;
			this.rbImage490.Text = "1990s";
			// 
			// rbImage400
			// 
			this.rbImage400.Location = new System.Drawing.Point(24, 24);
			this.rbImage400.Name = "rbImage400";
			this.rbImage400.Size = new System.Drawing.Size(54, 20);
			this.rbImage400.TabIndex = 0;
			this.rbImage400.Text = "2000s";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.rbImage3Unknown);
			this.groupBox5.Controls.Add(this.rbImage370);
			this.groupBox5.Controls.Add(this.rbImage380);
			this.groupBox5.Controls.Add(this.rbImage390);
			this.groupBox5.Controls.Add(this.rbImage300);
			this.groupBox5.Location = new System.Drawing.Point(20, 212);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(322, 60);
			this.groupBox5.TabIndex = 5;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Image 3";
			// 
			// rbImage3Unknown
			// 
			this.rbImage3Unknown.Location = new System.Drawing.Point(266, 24);
			this.rbImage3Unknown.Name = "rbImage3Unknown";
			this.rbImage3Unknown.Size = new System.Drawing.Size(42, 20);
			this.rbImage3Unknown.TabIndex = 5;
			this.rbImage3Unknown.Text = "???";
			// 
			// rbImage370
			// 
			this.rbImage370.Location = new System.Drawing.Point(204, 24);
			this.rbImage370.Name = "rbImage370";
			this.rbImage370.Size = new System.Drawing.Size(54, 20);
			this.rbImage370.TabIndex = 3;
			this.rbImage370.Text = "1970s";
			// 
			// rbImage380
			// 
			this.rbImage380.Location = new System.Drawing.Point(144, 24);
			this.rbImage380.Name = "rbImage380";
			this.rbImage380.Size = new System.Drawing.Size(54, 20);
			this.rbImage380.TabIndex = 2;
			this.rbImage380.Text = "1980s";
			// 
			// rbImage390
			// 
			this.rbImage390.Location = new System.Drawing.Point(84, 24);
			this.rbImage390.Name = "rbImage390";
			this.rbImage390.Size = new System.Drawing.Size(54, 20);
			this.rbImage390.TabIndex = 1;
			this.rbImage390.Text = "1990s";
			// 
			// rbImage300
			// 
			this.rbImage300.Location = new System.Drawing.Point(24, 24);
			this.rbImage300.Name = "rbImage300";
			this.rbImage300.Size = new System.Drawing.Size(54, 20);
			this.rbImage300.TabIndex = 0;
			this.rbImage300.Text = "2000s";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.rbImage1Unknown);
			this.groupBox3.Controls.Add(this.rbImage170);
			this.groupBox3.Controls.Add(this.rbImage180);
			this.groupBox3.Controls.Add(this.rbImage190);
			this.groupBox3.Controls.Add(this.rbImage100);
			this.groupBox3.Location = new System.Drawing.Point(20, 70);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(322, 60);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Image 1";
			// 
			// rbImage1Unknown
			// 
			this.rbImage1Unknown.Location = new System.Drawing.Point(266, 24);
			this.rbImage1Unknown.Name = "rbImage1Unknown";
			this.rbImage1Unknown.Size = new System.Drawing.Size(42, 20);
			this.rbImage1Unknown.TabIndex = 4;
			this.rbImage1Unknown.Text = "???";
			// 
			// rbImage170
			// 
			this.rbImage170.Location = new System.Drawing.Point(204, 24);
			this.rbImage170.Name = "rbImage170";
			this.rbImage170.Size = new System.Drawing.Size(53, 20);
			this.rbImage170.TabIndex = 3;
			this.rbImage170.Text = "1970s";
			// 
			// rbImage180
			// 
			this.rbImage180.Location = new System.Drawing.Point(144, 24);
			this.rbImage180.Name = "rbImage180";
			this.rbImage180.Size = new System.Drawing.Size(54, 20);
			this.rbImage180.TabIndex = 2;
			this.rbImage180.Text = "1980s";
			// 
			// rbImage190
			// 
			this.rbImage190.Location = new System.Drawing.Point(84, 24);
			this.rbImage190.Name = "rbImage190";
			this.rbImage190.Size = new System.Drawing.Size(54, 20);
			this.rbImage190.TabIndex = 1;
			this.rbImage190.Text = "1990s";
			// 
			// rbImage100
			// 
			this.rbImage100.Location = new System.Drawing.Point(24, 24);
			this.rbImage100.Name = "rbImage100";
			this.rbImage100.Size = new System.Drawing.Size(54, 20);
			this.rbImage100.TabIndex = 0;
			this.rbImage100.Text = "2000s";
			// 
			// edtBaseName
			// 
			this.edtBaseName.Location = new System.Drawing.Point(20, 40);
			this.edtBaseName.Name = "edtBaseName";
			this.edtBaseName.TabIndex = 1;
			this.edtBaseName.Text = "Scan 036-";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(70, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Base Name";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.rbImage2Unknown);
			this.groupBox4.Controls.Add(this.rbImage270);
			this.groupBox4.Controls.Add(this.rbImage280);
			this.groupBox4.Controls.Add(this.rbImage290);
			this.groupBox4.Controls.Add(this.rbImage200);
			this.groupBox4.Location = new System.Drawing.Point(20, 141);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(322, 60);
			this.groupBox4.TabIndex = 4;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Image 2";
			// 
			// rbImage2Unknown
			// 
			this.rbImage2Unknown.Location = new System.Drawing.Point(266, 24);
			this.rbImage2Unknown.Name = "rbImage2Unknown";
			this.rbImage2Unknown.Size = new System.Drawing.Size(41, 20);
			this.rbImage2Unknown.TabIndex = 5;
			this.rbImage2Unknown.Text = "???";
			// 
			// rbImage270
			// 
			this.rbImage270.Location = new System.Drawing.Point(204, 24);
			this.rbImage270.Name = "rbImage270";
			this.rbImage270.Size = new System.Drawing.Size(54, 20);
			this.rbImage270.TabIndex = 3;
			this.rbImage270.Text = "1970s";
			// 
			// rbImage280
			// 
			this.rbImage280.Location = new System.Drawing.Point(144, 24);
			this.rbImage280.Name = "rbImage280";
			this.rbImage280.Size = new System.Drawing.Size(54, 20);
			this.rbImage280.TabIndex = 2;
			this.rbImage280.Text = "1980s";
			// 
			// rbImage290
			// 
			this.rbImage290.Location = new System.Drawing.Point(84, 24);
			this.rbImage290.Name = "rbImage290";
			this.rbImage290.Size = new System.Drawing.Size(54, 20);
			this.rbImage290.TabIndex = 1;
			this.rbImage290.Text = "1990s";
			// 
			// rbImage200
			// 
			this.rbImage200.Location = new System.Drawing.Point(24, 24);
			this.rbImage200.Name = "rbImage200";
			this.rbImage200.Size = new System.Drawing.Size(54, 20);
			this.rbImage200.TabIndex = 0;
			this.rbImage200.Text = "2000s";
			// 
			// FamilyPhotos50th
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(397, 555);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FamilyPhotos50th";
			this.Text = "Family Photos";
			this.Load += new System.EventHandler(this.FamilyPhotos50th_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FamilyPhotos50th_Load( object sender, System.EventArgs e )
		{
			// Fire up Photoshop.
			psApp =  new Photoshop.Application();
			ps	  =  new Photoshopper.Photoshopper( psApp );

			browseForDirectory.LabelIsVisible =  true;
			browseForDirectory.Label		  =  "Base Directory";
			browseForDirectory.Width		  =  324;
		}

		private void btnCCWAssignsRGB_Click( object sender, System.EventArgs e )
		{
			try
			{
				psApp.DoAction( "Rotate CCW & Assign sRGB", "My Stuff (General)" );
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		private void btnCWAssignsRGB_Click(object sender, System.EventArgs e)
		{
			try
			{
				psApp.DoAction( "Rotate CW & Assign sRGB", "My Stuff (General)" );
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		private void btnSave_Click( object sender, System.EventArgs e )
		{
			// First image
			try
			{
				string	baseName =  this.edtBaseName.Text;
				string	pathBase =  this.browseForDirectory.Directory;
				string	path;

				System.Collections.IEnumerator iter	=  psApp.Documents.GetEnumerator();

				while( iter.MoveNext() )
				{
					Document doc =  iter.Current as Photoshop.Document;
					string name  =  doc.Name;

					if ( name == "Untitled-1" )
					{
						if ( rbImage100.Checked )
						{
							path =  pathBase + @"\2000s";
						}
						else if ( rbImage190.Checked )
						{
							path =  pathBase + @"\1990s";
						}
						else if ( rbImage180.Checked )
						{
							path =  pathBase + @"\1980s";
						}
						else if ( rbImage170.Checked )
						{
							path =  pathBase + @"\1970s";
						}
						else
						{
							path =  pathBase + @"\Unknown";
						}

						// Make active.
						psApp.ActiveDocument =  doc;

						// Save and close.
						ps.SaveAsTIFF( doc, path, baseName + "1" );
						doc.Close( PsSaveOptions.psDoNotSaveChanges );

						iter =  psApp.Documents.GetEnumerator();
					}
					else if ( name == "Untitled-2" )
					{
						if ( rbImage200.Checked )
							path =  pathBase + @"\2000s";
						else if ( rbImage290.Checked )
							path =  pathBase + @"\1990s";
						else if ( rbImage280.Checked )
							path =  pathBase + @"\1980s";
						else if ( rbImage270.Checked )
							path =  pathBase + @"\1970s";
						else
							path =  pathBase + @"\Unknown";

						// Make active.
						psApp.ActiveDocument =  doc;

						// Save and close.
						ps.SaveAsTIFF( doc, path, baseName + "2" );
						doc.Close( PsSaveOptions.psDoNotSaveChanges );

						iter	=  psApp.Documents.GetEnumerator();
					}
					else if ( name == "Untitled-3" )
					{
						if ( rbImage300.Checked )
							path =  pathBase + @"\2000s";
						else if ( rbImage390.Checked )
							path =  pathBase + @"\1990s";
						else if ( rbImage380.Checked )
							path =  pathBase + @"\1980s";
						else if ( rbImage370.Checked )
							path =  pathBase + @"\1970s";
						else
							path =  pathBase + @"\Unknown";

						// Make active.
						psApp.ActiveDocument =  doc;

						// Save and close.
						ps.SaveAsTIFF( doc, path, baseName + "3" );
						doc.Close( PsSaveOptions.psDoNotSaveChanges );

						iter =  psApp.Documents.GetEnumerator();
					}
					else if ( name == "Untitled-4" )
					{
						if ( rbImage400.Checked )
							path =  pathBase + @"\2000s";
						else if ( rbImage490.Checked )
							path =  pathBase + @"\1990s";
						else if ( rbImage480.Checked )
							path =  pathBase + @"\1980s";
						else if ( rbImage470.Checked )
							path =  pathBase + @"\1970s";
						else
							path =  pathBase + @"\Unknown";

						// Make active.
						psApp.ActiveDocument	=  doc;

						// Save and close.
						ps.SaveAsTIFF( doc, path, baseName + "4" );
						doc.Close( PsSaveOptions.psDoNotSaveChanges );

						iter =  psApp.Documents.GetEnumerator();
					}
				}

				// Close main scan?
				if ( psApp.Documents.Count == 1 && chkCloseMain.Checked )
				{
					psApp.ActiveDocument.Close( PsSaveOptions.psDoNotSaveChanges );
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}
	}
}

