namespace MyControls
{
	partial class BrowseForObjectNew
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
			this.labelAndSpinner1 = new MyControls.LabelAndSpinner();
			this.labelAndText1 = new MyControls.LabelAndText();
			this.dataValueTextBox1 = new MyControls.DataValueTextBox();
			this.SuspendLayout();
			// 
			// labelAndSpinner1
			// 
			this.labelAndSpinner1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.labelAndSpinner1.Label = "";
			this.labelAndSpinner1.Location = new System.Drawing.Point(4, 4);
			this.labelAndSpinner1.Max = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.labelAndSpinner1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.labelAndSpinner1.Name = "labelAndSpinner1";
			this.labelAndSpinner1.Size = new System.Drawing.Size(100, 20);
			this.labelAndSpinner1.TabIndex = 0;
			this.labelAndSpinner1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// labelAndText1
			// 
			this.labelAndText1.Label = "label1";
			this.labelAndText1.Location = new System.Drawing.Point(4, 31);
			this.labelAndText1.Name = "labelAndText1";
			this.labelAndText1.Size = new System.Drawing.Size(100, 37);
			this.labelAndText1.TabIndex = 1;
			// 
			// dataValueTextBox1
			// 
			this.dataValueTextBox1.Location = new System.Drawing.Point(4, 130);
			this.dataValueTextBox1.Name = "dataValueTextBox1";
			this.dataValueTextBox1.Size = new System.Drawing.Size(100, 20);
			this.dataValueTextBox1.TabIndex = 2;
			this.dataValueTextBox1.ValueText = null;
			// 
			// BrowseForObjectNew
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataValueTextBox1);
			this.Controls.Add(this.labelAndText1);
			this.Controls.Add(this.labelAndSpinner1);
			this.Name = "BrowseForObjectNew";
			this.Size = new System.Drawing.Size(369, 204);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private LabelAndSpinner labelAndSpinner1;
		private LabelAndText labelAndText1;
		private DataValueTextBox dataValueTextBox1;
	}
}
