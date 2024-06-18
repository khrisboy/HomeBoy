namespace MyControls
{
	partial class WindowForm
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
			this.SuspendLayout();
			// 
			// WindowForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 284, 264 );
			this.Name = "WindowForm";
			this.Load += new System.EventHandler( this.WindowForm_Load );
			this.Paint += new System.Windows.Forms.PaintEventHandler( this.WindowForm_Paint );
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.WindowForm_FormClosing );
			this.ResumeLayout( false );

		}

		#endregion
	}
}
