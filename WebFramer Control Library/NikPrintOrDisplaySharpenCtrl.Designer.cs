namespace WebFramerCS2ControlLibrary
{
	partial class NikPrintOrDisplaySharpenCtrl
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
			this.lsStrength = new MyControls.LabelAndSpinner();
			this.rbDisplay = new System.Windows.Forms.RadioButton();
			this.rbPrint = new System.Windows.Forms.RadioButton();
			this.cbPrintResolution = new System.Windows.Forms.ComboBox();
			this.stPrintResolution = new System.Windows.Forms.Label();
			this.cbPaperType = new System.Windows.Forms.ComboBox();
			this.stPaperType = new System.Windows.Forms.Label();
			this.stNikProfileType = new System.Windows.Forms.Label();
			this.chkSharpen = new System.Windows.Forms.CheckBox();
			this.cbNikProfileType = new System.Windows.Forms.ComboBox();
			this.grpNikSharpener.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpNikSharpener
			// 
			this.grpNikSharpener.Controls.Add( this.lsStrength );
			this.grpNikSharpener.Controls.Add( this.rbDisplay );
			this.grpNikSharpener.Controls.Add( this.rbPrint );
			this.grpNikSharpener.Controls.Add( this.cbPrintResolution );
			this.grpNikSharpener.Controls.Add( this.stPrintResolution );
			this.grpNikSharpener.Controls.Add( this.cbPaperType );
			this.grpNikSharpener.Controls.Add( this.stPaperType );
			this.grpNikSharpener.Controls.Add( this.stNikProfileType );
			this.grpNikSharpener.Controls.Add( this.chkSharpen );
			this.grpNikSharpener.Controls.Add( this.cbNikProfileType );
			this.grpNikSharpener.Location = new System.Drawing.Point( 0, 0 );
			this.grpNikSharpener.Name = "grpNikSharpener";
			this.grpNikSharpener.Size = new System.Drawing.Size( 268, 112 );
			this.grpNikSharpener.TabIndex = 0;
			this.grpNikSharpener.TabStop = false;
			this.grpNikSharpener.Text = "Nik Sharpening";
			// 
			// lsStrength
			// 
			this.lsStrength.Increment = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			this.lsStrength.Label = "Strength";
			this.lsStrength.Location = new System.Drawing.Point( 138, 82 );
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
			this.lsStrength.TabIndex = 22;
			this.lsStrength.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			// 
			// rbDisplay
			// 
			this.rbDisplay.AutoSize = true;
			this.rbDisplay.Location = new System.Drawing.Point( 89, 19 );
			this.rbDisplay.Name = "rbDisplay";
			this.rbDisplay.Size = new System.Drawing.Size( 59, 17 );
			this.rbDisplay.TabIndex = 21;
			this.rbDisplay.TabStop = true;
			this.rbDisplay.Text = "Display";
			this.rbDisplay.UseVisualStyleBackColor = true;
			// 
			// rbPrint
			// 
			this.rbPrint.AutoSize = true;
			this.rbPrint.Location = new System.Drawing.Point( 89, 42 );
			this.rbPrint.Name = "rbPrint";
			this.rbPrint.Size = new System.Drawing.Size( 46, 17 );
			this.rbPrint.TabIndex = 20;
			this.rbPrint.TabStop = true;
			this.rbPrint.Text = "Print";
			this.rbPrint.UseVisualStyleBackColor = true;
			this.rbPrint.CheckedChanged += new System.EventHandler( this.rbPrintDisplay_CheckedChanged );
			// 
			// cbPrintResolution
			// 
			this.cbPrintResolution.FormattingEnabled = true;
			this.cbPrintResolution.Items.AddRange( new object[] {
            "1440x720",
            "1440x1440",
            "2880x1440",
            "2880x720",
            "5760x1440",
            "1440x1440",
            "720x720"} );
			this.cbPrintResolution.Location = new System.Drawing.Point( 157, 36 );
			this.cbPrintResolution.Name = "cbPrintResolution";
			this.cbPrintResolution.Size = new System.Drawing.Size( 99, 21 );
			this.cbPrintResolution.TabIndex = 19;
			// 
			// stPrintResolution
			// 
			this.stPrintResolution.AutoSize = true;
			this.stPrintResolution.Location = new System.Drawing.Point( 154, 19 );
			this.stPrintResolution.Name = "stPrintResolution";
			this.stPrintResolution.Size = new System.Drawing.Size( 81, 13 );
			this.stPrintResolution.TabIndex = 18;
			this.stPrintResolution.Text = "Print Resolution";
			// 
			// cbPaperType
			// 
			this.cbPaperType.FormattingEnabled = true;
			this.cbPaperType.Items.AddRange( new object[] {
            "Texture & Fine Art",
            "Canvas",
            "Plain",
            "Matte",
            "Lustre",
            "Glossy"} );
			this.cbPaperType.Location = new System.Drawing.Point( 134, 82 );
			this.cbPaperType.Name = "cbPaperType";
			this.cbPaperType.Size = new System.Drawing.Size( 121, 21 );
			this.cbPaperType.TabIndex = 17;
			// 
			// stPaperType
			// 
			this.stPaperType.AutoSize = true;
			this.stPaperType.Location = new System.Drawing.Point( 131, 65 );
			this.stPaperType.Name = "stPaperType";
			this.stPaperType.Size = new System.Drawing.Size( 62, 13 );
			this.stPaperType.TabIndex = 16;
			this.stPaperType.Text = "Paper Type";
			// 
			// stNikProfileType
			// 
			this.stNikProfileType.AutoSize = true;
			this.stNikProfileType.Location = new System.Drawing.Point( 10, 65 );
			this.stNikProfileType.Name = "stNikProfileType";
			this.stNikProfileType.Size = new System.Drawing.Size( 36, 13 );
			this.stNikProfileType.TabIndex = 15;
			this.stNikProfileType.Text = "Profile";
			// 
			// chkSharpen
			// 
			this.chkSharpen.AutoSize = true;
			this.chkSharpen.Location = new System.Drawing.Point( 13, 30 );
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
			this.cbNikProfileType.Location = new System.Drawing.Point( 12, 82 );
			this.cbNikProfileType.Name = "cbNikProfileType";
			this.cbNikProfileType.Size = new System.Drawing.Size( 100, 21 );
			this.cbNikProfileType.TabIndex = 13;
			// 
			// NikPrintOrDisplaySharpenCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.grpNikSharpener );
			this.Name = "NikPrintOrDisplaySharpenCtrl";
			this.Size = new System.Drawing.Size( 270, 112 );
			this.Load += new System.EventHandler( this.NikSharpenCtrl_Load );
			this.grpNikSharpener.ResumeLayout( false );
			this.grpNikSharpener.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private MyControls.MyGroupBox grpNikSharpener;
		private System.Windows.Forms.ComboBox cbPaperType;
		private System.Windows.Forms.Label stPaperType;
		private System.Windows.Forms.Label stNikProfileType;
		private System.Windows.Forms.CheckBox chkSharpen;
		protected System.Windows.Forms.ComboBox cbNikProfileType;
		private System.Windows.Forms.Label stPrintResolution;
		private System.Windows.Forms.ComboBox cbPrintResolution;
		private System.Windows.Forms.RadioButton rbDisplay;
		private System.Windows.Forms.RadioButton rbPrint;
		private MyControls.LabelAndSpinner lsStrength;

	}
}
