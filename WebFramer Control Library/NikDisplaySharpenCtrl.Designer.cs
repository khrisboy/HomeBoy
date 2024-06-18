namespace WebFramerCS2ControlLibrary
{
	partial class NikDisplaySharpenCtrl
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
			this.grpNikSharpener = new MyControls.MyGroupBox();
			this.stNikProfileType = new System.Windows.Forms.Label();
			this.chkSharpen = new System.Windows.Forms.CheckBox();
			this.cbNikProfileType = new System.Windows.Forms.ComboBox();
			this.lsStrength = new MyControls.LabelAndSpinner();
			this.grpNikSharpener.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpNikSharpener
			// 
			this.grpNikSharpener.Controls.Add( this.lsStrength );
			this.grpNikSharpener.Controls.Add( this.stNikProfileType );
			this.grpNikSharpener.Controls.Add( this.chkSharpen );
			this.grpNikSharpener.Controls.Add( this.cbNikProfileType );
			this.grpNikSharpener.Location = new System.Drawing.Point( 0, 0 );
			this.grpNikSharpener.Name = "grpNikSharpener";
			this.grpNikSharpener.Size = new System.Drawing.Size( 268, 112 );
			this.grpNikSharpener.TabIndex = 1;
			this.grpNikSharpener.TabStop = false;
			this.grpNikSharpener.Text = "Nik Sharpening";
			// 
			// stNikProfileType
			// 
			this.stNikProfileType.AutoSize = true;
			this.stNikProfileType.Location = new System.Drawing.Point( 14, 62 );
			this.stNikProfileType.Name = "stNikProfileType";
			this.stNikProfileType.Size = new System.Drawing.Size( 36, 13 );
			this.stNikProfileType.TabIndex = 15;
			this.stNikProfileType.Text = "Profile";
			// 
			// chkSharpen
			// 
			this.chkSharpen.AutoSize = true;
			this.chkSharpen.Location = new System.Drawing.Point( 26, 27 );
			this.chkSharpen.Name = "chkSharpen";
			this.chkSharpen.Size = new System.Drawing.Size( 66, 17 );
			this.chkSharpen.TabIndex = 14;
			this.chkSharpen.Text = "Sharpen";
			this.chkSharpen.UseVisualStyleBackColor = true;
			this.chkSharpen.CheckedChanged += new System.EventHandler( this.chkSharpen_CheckedChanged );
			// 
			// cbNikProfileType
			// 
			this.cbNikProfileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbNikProfileType.Items.AddRange( new object[] {
            "Anna",
            "John",
            "Zap"} );
			this.cbNikProfileType.Location = new System.Drawing.Point( 16, 78 );
			this.cbNikProfileType.Name = "cbNikProfileType";
			this.cbNikProfileType.Size = new System.Drawing.Size( 100, 21 );
			this.cbNikProfileType.TabIndex = 13;
			// 
			// lsStrength
			// 
			this.lsStrength.Increment = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			this.lsStrength.Label = "Strength";
			this.lsStrength.Location = new System.Drawing.Point( 144, 78 );
			this.lsStrength.Max = new decimal( new int[] {
            100,
            0,
            0,
            0} );
			this.lsStrength.Min = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			this.lsStrength.Name = "lsStrength";
			this.lsStrength.Size = new System.Drawing.Size( 100, 20 );
			this.lsStrength.TabIndex = 16;
			this.lsStrength.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			// 
			// NikDisplaySharpenCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.grpNikSharpener );
			this.Name = "NikDisplaySharpenCtrl";
			this.Size = new System.Drawing.Size( 268, 112 );
			this.Load += new System.EventHandler( this.NikDisplaySharpenCtrl_Load );
			this.grpNikSharpener.ResumeLayout( false );
			this.grpNikSharpener.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private MyControls.MyGroupBox grpNikSharpener;
		private System.Windows.Forms.Label stNikProfileType;
		private System.Windows.Forms.CheckBox chkSharpen;
		protected System.Windows.Forms.ComboBox cbNikProfileType;
		private MyControls.LabelAndSpinner lsStrength;
	}
}
