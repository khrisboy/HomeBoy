namespace MyControls
{
	partial class LabelAndSpinner
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
			this.stLabel = new System.Windows.Forms.Label();
			this.udSpinner = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.udSpinner)).BeginInit();
			this.SuspendLayout();
			// 
			// stLabel
			// 
			this.stLabel.Location = new System.Drawing.Point(0, 3);
			this.stLabel.Name = "stLabel";
			this.stLabel.Size = new System.Drawing.Size(56, 13);
			this.stLabel.TabIndex = 0;
			this.stLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// udSpinner
			// 
			this.udSpinner.Location = new System.Drawing.Point(60, 0);
			this.udSpinner.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.udSpinner.Name = "udSpinner";
			this.udSpinner.Size = new System.Drawing.Size(40, 20);
			this.udSpinner.TabIndex = 1;
			this.udSpinner.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// LabelAndSpinner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.stLabel);
			this.Controls.Add(this.udSpinner);
			this.Name = "LabelAndSpinner";
			this.Size = new System.Drawing.Size(100, 20);
			((System.ComponentModel.ISupportInitialize)(this.udSpinner)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label stLabel;
		private System.Windows.Forms.NumericUpDown udSpinner;
	}
}
