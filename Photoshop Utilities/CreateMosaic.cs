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
	public class CreateMosaic : MyControls.MyWindowsForm
	{
		Photoshop.Application		psApp;
		Photoshopper.Photoshopper	ps;
		Document					doc;
		Photoshop.Channels			channels;

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cbSelections;
		private System.Windows.Forms.Button btnSelectionsLoad;
		private System.Windows.Forms.Button btnSelectionsRefresh;
		private System.ComponentModel.IContainer components = null;

		public CreateMosaic()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnSelectionsLoad = new System.Windows.Forms.Button();
			this.cbSelections = new System.Windows.Forms.ComboBox();
			this.btnSelectionsRefresh = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnSelectionsRefresh);
			this.groupBox1.Controls.Add(this.btnSelectionsLoad);
			this.groupBox1.Controls.Add(this.cbSelections);
			this.groupBox1.Location = new System.Drawing.Point(16, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 138);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Selection Masks";
			// 
			// btnSelectionsLoad
			// 
			this.btnSelectionsLoad.Location = new System.Drawing.Point(172, 90);
			this.btnSelectionsLoad.Name = "btnSelectionsLoad";
			this.btnSelectionsLoad.TabIndex = 1;
			this.btnSelectionsLoad.Text = "Load";
			this.btnSelectionsLoad.Click += new System.EventHandler(this.btnSelectionsLoad_Click);
			// 
			// cbSelections
			// 
			this.cbSelections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSelections.Location = new System.Drawing.Point(24, 32);
			this.cbSelections.Name = "cbSelections";
			this.cbSelections.Size = new System.Drawing.Size(252, 21);
			this.cbSelections.Sorted = true;
			this.cbSelections.TabIndex = 0;
			// 
			// btnSelectionsRefresh
			// 
			this.btnSelectionsRefresh.Location = new System.Drawing.Point(52, 90);
			this.btnSelectionsRefresh.Name = "btnSelectionsRefresh";
			this.btnSelectionsRefresh.TabIndex = 2;
			this.btnSelectionsRefresh.Text = "Refresh";
			this.btnSelectionsRefresh.Click += new System.EventHandler(this.btnSelectionsRefresh_Click);
			// 
			// CreateMosaic
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(439, 452);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CreateMosaic";
			this.Text = "Create Mosaic";
			this.Load += new System.EventHandler(this.CreateMosaic_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void CreateMosaic_Load( object sender, System.EventArgs e )
		{
			RefreshSelections();
		}

		private void RefreshSelections()
		{
			try
			{
				// Start up Photoshop.
				psApp =  new Photoshop.Application();
				ps	  =  new Photoshopper.Photoshopper( psApp );

				// Save preferences.
				PsUnits	originalRulerUnits =  psApp.Preferences.RulerUnits;

				// Get the active document.
				doc	=  psApp.ActiveDocument;

				// Get the list of available selections (channels).
				channels =  doc.Channels;

				// Loop through channels and add any masking channels.
				for ( int i= 1;  i<= channels.Count;  i++ )
				{
					Channel	channel	=  channels[ i ];

					string			channelName	=  channel.Name;
					PsChannelType	channelType	=  channel.Kind;

					if ( channelType == PsChannelType.psMaskedAreaAlphaChannel )
					{
						cbSelections.Items.Add( channelName );
					}
				}

				// Select the first one.
				if ( cbSelections.Items.Count > 0 )
				{
					cbSelections.SelectedIndex	=  0;
				}

				// Restore preferences.
				psApp.Preferences.RulerUnits =  originalRulerUnits;
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}

		private void btnSelectionsRefresh_Click(object sender, System.EventArgs e)
		{
			RefreshSelections();
		}

		private void btnSelectionsLoad_Click(object sender, System.EventArgs e)
		{
			try
			{
				// If there are no selections...
				if ( cbSelections.Items.Count == 0 )
					return;

				// Loop through channels and load the selected selection mask.
				string	selName	=  cbSelections.SelectedItem.ToString();

				for ( int i= 1;  i<= channels.Count;  i++ )
				{
					Channel	channel	=  channels[ i ];

					string			channelName	=  channel.Name;
					PsChannelType	channelType	=  channel.Kind;

					if ( channelType == PsChannelType.psMaskedAreaAlphaChannel && channelName == selName )
					{
						doc.Selection.Load( channel, PsSelectionType.psReplaceSelection, false );
					}
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message );
			}
		}
	}
}

