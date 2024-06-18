using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using MyClasses;
using MyControls;
using Photoshop;
using Photoshopper;

namespace PhotoshopUtilities
{
	public class CreateGuides : MyControls.MyWindowsForm
	{
		#region Member variables.
		private System.Windows.Forms.GroupBox grpMethod;
		private System.Windows.Forms.RadioButton rbEvery;
		private System.Windows.Forms.TextBox edtMethodEvery;
		private System.Windows.Forms.RadioButton rbAt;
		private System.Windows.Forms.TextBox edtMethodAt;
		private System.Windows.Forms.GroupBox grpUnits;
		private System.Windows.Forms.RadioButton rbUnitsPixels;
		private System.Windows.Forms.RadioButton rbUnitsInches;
		private System.Windows.Forms.GroupBox grpDirection;
		private System.Windows.Forms.CheckBox chkDirectionHorizontal;
		private System.Windows.Forms.CheckBox chkDirectionVertical;
		private System.Windows.Forms.Button btnCreateGrid;
		private System.Windows.Forms.RadioButton rbUnitsMM;
		private System.Windows.Forms.TextBox edtFrom;
		private System.Windows.Forms.Label stFrom;
		private System.Windows.Forms.Label stTo;
		private System.Windows.Forms.TextBox edtTo;
		private System.ComponentModel.IContainer components = null;
		#endregion

		public CreateGuides()
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
			this.rbEvery = new System.Windows.Forms.RadioButton();
			this.grpMethod = new System.Windows.Forms.GroupBox();
			this.rbAt = new System.Windows.Forms.RadioButton();
			this.edtMethodAt = new System.Windows.Forms.TextBox();
			this.edtMethodEvery = new System.Windows.Forms.TextBox();
			this.grpDirection = new System.Windows.Forms.GroupBox();
			this.chkDirectionVertical = new System.Windows.Forms.CheckBox();
			this.chkDirectionHorizontal = new System.Windows.Forms.CheckBox();
			this.grpUnits = new System.Windows.Forms.GroupBox();
			this.rbUnitsInches = new System.Windows.Forms.RadioButton();
			this.rbUnitsPixels = new System.Windows.Forms.RadioButton();
			this.btnCreateGrid = new System.Windows.Forms.Button();
			this.rbUnitsMM = new System.Windows.Forms.RadioButton();
			this.edtFrom = new System.Windows.Forms.TextBox();
			this.edtTo = new System.Windows.Forms.TextBox();
			this.stFrom = new System.Windows.Forms.Label();
			this.stTo = new System.Windows.Forms.Label();
			this.grpMethod.SuspendLayout();
			this.grpDirection.SuspendLayout();
			this.grpUnits.SuspendLayout();
			this.SuspendLayout();
			// 
			// rbEvery
			// 
			this.rbEvery.Location = new System.Drawing.Point(24, 34);
			this.rbEvery.Name = "rbEvery";
			this.rbEvery.Size = new System.Drawing.Size(53, 24);
			this.rbEvery.TabIndex = 0;
			this.rbEvery.Text = "&Every";
			this.rbEvery.CheckedChanged += new System.EventHandler(this.rbEvery_CheckedChanged);
			// 
			// grpMethod
			// 
			this.grpMethod.Controls.Add(this.stTo);
			this.grpMethod.Controls.Add(this.stFrom);
			this.grpMethod.Controls.Add(this.edtTo);
			this.grpMethod.Controls.Add(this.edtFrom);
			this.grpMethod.Controls.Add(this.rbAt);
			this.grpMethod.Controls.Add(this.edtMethodAt);
			this.grpMethod.Controls.Add(this.edtMethodEvery);
			this.grpMethod.Controls.Add(this.rbEvery);
			this.grpMethod.Location = new System.Drawing.Point(24, 24);
			this.grpMethod.Name = "grpMethod";
			this.grpMethod.Size = new System.Drawing.Size(300, 120);
			this.grpMethod.TabIndex = 1;
			this.grpMethod.TabStop = false;
			this.grpMethod.Text = "Method";
			// 
			// rbAt
			// 
			this.rbAt.Location = new System.Drawing.Point(24, 71);
			this.rbAt.Name = "rbAt";
			this.rbAt.Size = new System.Drawing.Size(41, 24);
			this.rbAt.TabIndex = 4;
			this.rbAt.Text = "&At";
			// 
			// edtMethodAt
			// 
			this.edtMethodAt.Location = new System.Drawing.Point(80, 73);
			this.edtMethodAt.Name = "edtMethodAt";
			this.edtMethodAt.Size = new System.Drawing.Size(180, 20);
			this.edtMethodAt.TabIndex = 3;
			this.edtMethodAt.Text = "";
			// 
			// edtMethodEvery
			// 
			this.edtMethodEvery.Location = new System.Drawing.Point(80, 36);
			this.edtMethodEvery.Name = "edtMethodEvery";
			this.edtMethodEvery.Size = new System.Drawing.Size(44, 20);
			this.edtMethodEvery.TabIndex = 2;
			this.edtMethodEvery.Text = "";
			// 
			// grpDirection
			// 
			this.grpDirection.Controls.Add(this.chkDirectionVertical);
			this.grpDirection.Controls.Add(this.chkDirectionHorizontal);
			this.grpDirection.Location = new System.Drawing.Point(24, 272);
			this.grpDirection.Name = "grpDirection";
			this.grpDirection.Size = new System.Drawing.Size(300, 80);
			this.grpDirection.TabIndex = 2;
			this.grpDirection.TabStop = false;
			this.grpDirection.Text = "Direction";
			// 
			// chkDirectionVertical
			// 
			this.chkDirectionVertical.Location = new System.Drawing.Point(140, 32);
			this.chkDirectionVertical.Name = "chkDirectionVertical";
			this.chkDirectionVertical.Size = new System.Drawing.Size(83, 24);
			this.chkDirectionVertical.TabIndex = 1;
			this.chkDirectionVertical.Text = "&Vertical";
			// 
			// chkDirectionHorizontal
			// 
			this.chkDirectionHorizontal.Location = new System.Drawing.Point(24, 32);
			this.chkDirectionHorizontal.Name = "chkDirectionHorizontal";
			this.chkDirectionHorizontal.Size = new System.Drawing.Size(83, 24);
			this.chkDirectionHorizontal.TabIndex = 0;
			this.chkDirectionHorizontal.Text = "&Horizontal";
			// 
			// grpUnits
			// 
			this.grpUnits.Controls.Add(this.rbUnitsMM);
			this.grpUnits.Controls.Add(this.rbUnitsInches);
			this.grpUnits.Controls.Add(this.rbUnitsPixels);
			this.grpUnits.Location = new System.Drawing.Point(24, 168);
			this.grpUnits.Name = "grpUnits";
			this.grpUnits.Size = new System.Drawing.Size(300, 80);
			this.grpUnits.TabIndex = 3;
			this.grpUnits.TabStop = false;
			this.grpUnits.Text = "Units";
			// 
			// rbUnitsInches
			// 
			this.rbUnitsInches.Location = new System.Drawing.Point(114, 32);
			this.rbUnitsInches.Name = "rbUnitsInches";
			this.rbUnitsInches.Size = new System.Drawing.Size(68, 24);
			this.rbUnitsInches.TabIndex = 1;
			this.rbUnitsInches.Text = "&in";
			// 
			// rbUnitsPixels
			// 
			this.rbUnitsPixels.Location = new System.Drawing.Point(24, 32);
			this.rbUnitsPixels.Name = "rbUnitsPixels";
			this.rbUnitsPixels.Size = new System.Drawing.Size(68, 24);
			this.rbUnitsPixels.TabIndex = 0;
			this.rbUnitsPixels.Text = "&px";
			// 
			// btnCreateGrid
			// 
			this.btnCreateGrid.Location = new System.Drawing.Point(137, 385);
			this.btnCreateGrid.Name = "btnCreateGrid";
			this.btnCreateGrid.TabIndex = 4;
			this.btnCreateGrid.Text = "&Create Grid";
			this.btnCreateGrid.Click += new System.EventHandler(this.btnCreateGrid_Click);
			// 
			// rbUnitsMM
			// 
			this.rbUnitsMM.Location = new System.Drawing.Point(194, 32);
			this.rbUnitsMM.Name = "rbUnitsMM";
			this.rbUnitsMM.Size = new System.Drawing.Size(68, 24);
			this.rbUnitsMM.TabIndex = 2;
			this.rbUnitsMM.Text = "&mm";
			// 
			// edtFrom
			// 
			this.edtFrom.Location = new System.Drawing.Point(160, 36);
			this.edtFrom.Name = "edtFrom";
			this.edtFrom.Size = new System.Drawing.Size(42, 20);
			this.edtFrom.TabIndex = 5;
			this.edtFrom.Text = "";
			// 
			// edtTo
			// 
			this.edtTo.Location = new System.Drawing.Point(220, 36);
			this.edtTo.Name = "edtTo";
			this.edtTo.Size = new System.Drawing.Size(42, 20);
			this.edtTo.TabIndex = 6;
			this.edtTo.Text = "";
			// 
			// stFrom
			// 
			this.stFrom.Location = new System.Drawing.Point(160, 20);
			this.stFrom.Name = "stFrom";
			this.stFrom.Size = new System.Drawing.Size(32, 16);
			this.stFrom.TabIndex = 7;
			this.stFrom.Text = "&From";
			// 
			// stTo
			// 
			this.stTo.Location = new System.Drawing.Point(220, 20);
			this.stTo.Name = "stTo";
			this.stTo.Size = new System.Drawing.Size(32, 16);
			this.stTo.TabIndex = 8;
			this.stTo.Text = "&To";
			// 
			// CreateGuides
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(348, 445);
			this.Controls.Add(this.btnCreateGrid);
			this.Controls.Add(this.grpDirection);
			this.Controls.Add(this.grpMethod);
			this.Controls.Add(this.grpUnits);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CreateGuides";
			this.Text = "Create Guides";
			this.Load += new System.EventHandler(this.CreateGuides_Load);
			this.grpMethod.ResumeLayout(false);
			this.grpDirection.ResumeLayout(false);
			this.grpUnits.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCreateGrid_Click( object sender, System.EventArgs e )
		{
			try
			{
				// Make some checks.
				if ( !( chkDirectionHorizontal.Checked || chkDirectionVertical.Checked ) )
					throw new Exception( "At least one direction must be checked!" );

				if ( rbEvery.Checked && edtMethodEvery.Text == String.Empty )
					throw new Exception( "Must specify interval value for Every!" );

				if ( rbAt.Checked && edtMethodAt.Text == String.Empty )
					throw new Exception( "Must specify interval value for At!" );

				// Okey dokey.
				// Start up Photoshop.
				Photoshop.Application		psApp	=  new Photoshop.Application();
				Photoshopper.Photoshopper	ps		=  new Photoshopper.Photoshopper( psApp );

				// Save preferences.
				PsUnits	originalRulerUnits	=  psApp.Preferences.RulerUnits;

				// Work in pixels.
				psApp.Preferences.RulerUnits	=  PsUnits.psPixels;
			
				// Don't display dialogs
				psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

				// Get the document sizes.
				Document	doc	=  psApp.ActiveDocument;

				double	width			=  doc.Width;			// In pixels!
				double	height		=  doc.Height;
				double	resolution	=  doc.Resolution;

				// Create the guides.
				if ( rbEvery.Checked )
				{
					// Every x units.
					double	interval	=  double.Parse( edtMethodEvery.Text );

					// Starting & ending at...
					double	start		=  interval;
					double	endW		=  width;
					double	endH		=  height;

					if ( edtFrom.Text != "" )
						start	=  double.Parse( edtFrom.Text );

					if ( edtTo.Text != "" )
					{
						endW	=  double.Parse( edtTo.Text );
						endH	=  double.Parse( edtTo.Text );
					}

					double	position	=  start;

					// Convert to pixels if necessary.
					if ( rbUnitsInches.Checked )
					{
						interval	*=  resolution;
						position	*=  resolution;

						if ( edtTo.Text != "" )
						{
							endW	*=  resolution;
							endH	*=  resolution;
						}
					}
					else if ( rbUnitsMM.Checked )
					{
						interval	*=  resolution*0.03937007874015748031496062992126;
						position	*=  resolution*0.03937007874015748031496062992126;

						if ( edtTo.Text != "" )
						{
							endW	*=  resolution*0.03937007874015748031496062992126;
							endH	*=  resolution*0.03937007874015748031496062992126;
						}
					}

					// Add a little for roundoff.
					endW	+=  0.2*interval;
					endH	+=  0.2*interval;

					while( position < endW || position < endH )
					{
						if ( chkDirectionHorizontal.Checked && position < endW )
							ps.MakeGuide( (int) position, true );

						if ( chkDirectionVertical.Checked && position < endH )
							ps.MakeGuide( (int) position, false );

						position	+=  interval;
					}
				}
				else
				{
					// Use a Regular Expression to parse out the string.
					string				values	=  edtMethodAt.Text;
					Regex					rg			=  new Regex( @"\d*(\.\d*)?" );
					MatchCollection	m			=  rg.Matches( values );

					for ( int i= 0;  i< m.Count;  i++ )
					{
						Match		match	=  m[ i ];
						string	sVal	=	match.Value;

						if ( sVal != String.Empty && match.Success )
						{
							double	dVal	=  double.Parse( sVal );
							
							// Convert to pixels if necessary.
							if ( rbUnitsInches.Checked )
								dVal	*=  resolution;
							else if ( rbUnitsMM.Checked )
								dVal	*=  resolution*0.03937007874015748031496062992126;

							if ( chkDirectionHorizontal.Checked )
								ps.MakeGuide( (int) dVal, true );

							if ( chkDirectionVertical.Checked )
								ps.MakeGuide( (int) dVal, false );
						}
					}
				}

				// Restore preferences.
				psApp.Preferences.RulerUnits	=  originalRulerUnits;
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		private void rbEvery_CheckedChanged( object sender, System.EventArgs e )
		{
			UpdateFromTo();
		}

		private void UpdateFromTo()
		{
			// Enable/Disable From & To.
			edtFrom.Enabled			=  rbEvery.Checked;
			edtTo.Enabled				=  rbEvery.Checked;
			edtMethodEvery.Enabled	=  rbEvery.Checked;
			edtMethodAt.Enabled		=  !rbEvery.Checked;
		}

		private void CreateGuides_Load(object sender, System.EventArgs e)
		{
			UpdateFromTo();
		}
	}
}

