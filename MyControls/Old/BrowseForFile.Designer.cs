namespace MyControls
{
	partial class BrowseForFile : BrowseForObject
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
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
			this.components = new System.ComponentModel.Container();
			this.m_theToolTip = new System.Windows.Forms.ToolTip( this.components );
			this.SuspendLayout();
			// 
			// stLabel
			// 
			this.stLabel.Name = "stLabel";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Click += new System.EventHandler( this.btnBrowse_Click );
			// 
			// edtPath
			// 
			this.edtPath.Name = "edtPath";
			// 
			// BrowseForFile
			// 
			this.Name = "BrowseForFile";
			this.Load += new System.EventHandler( this.BrowseForFile_Load );
			this.OnMyTextChanged += new BrowseForObject.MyTextChanged( this.BrowseForFile_OnMyTextChanged );
			this.ResumeLayout( false );

		}
		#endregion

		protected System.Windows.Forms.ToolTip m_theToolTip;
	}
}

