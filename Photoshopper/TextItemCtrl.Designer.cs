namespace Photoshopper
{
	partial class TextItemCtrl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			MyClasses.DPoint dPoint1 = new MyClasses.DPoint();
			this.grpTextData = new MyControls.MyGroupBox();
			this.stDpi = new System.Windows.Forms.Label();
			this.ltStyle = new MyControls.LabelAndText();
			this.stAntiAlias = new System.Windows.Forms.Label();
			this.cbAntiAlias = new System.Windows.Forms.ComboBox();
			this.stJustification = new System.Windows.Forms.Label();
			this.cbJustification = new System.Windows.Forms.ComboBox();
			this.rbPixels = new System.Windows.Forms.RadioButton();
			this.rbInches = new System.Windows.Forms.RadioButton();
			this.xyLocation = new MyControls.XYCoordinates();
			this.ltText = new MyControls.LabelAndText();
			this.ltFontSize = new MyControls.LabelAndText();
			this.ltFont = new MyControls.LabelAndText();
			this.ltRgbValue = new MyControls.LabelAndText();
			this.grpTextData.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpTextData
			// 
			this.grpTextData.Controls.Add( this.stDpi );
			this.grpTextData.Controls.Add( this.ltStyle );
			this.grpTextData.Controls.Add( this.stAntiAlias );
			this.grpTextData.Controls.Add( this.cbAntiAlias );
			this.grpTextData.Controls.Add( this.stJustification );
			this.grpTextData.Controls.Add( this.cbJustification );
			this.grpTextData.Controls.Add( this.rbPixels );
			this.grpTextData.Controls.Add( this.rbInches );
			this.grpTextData.Controls.Add( this.xyLocation );
			this.grpTextData.Controls.Add( this.ltText );
			this.grpTextData.Controls.Add( this.ltFontSize );
			this.grpTextData.Controls.Add( this.ltFont );
			this.grpTextData.Controls.Add( this.ltRgbValue );
			this.grpTextData.Location = new System.Drawing.Point( 0, 0 );
			this.grpTextData.Name = "grpTextData";
			this.grpTextData.Size = new System.Drawing.Size( 450, 265 );
			this.grpTextData.TabIndex = 2;
			this.grpTextData.TabStop = false;
			this.grpTextData.Text = "Text Layer Data";
			// 
			// stDpi
			// 
			this.stDpi.AutoSize = true;
			this.stDpi.Location = new System.Drawing.Point( 160, 241 );
			this.stDpi.Name = "stDpi";
			this.stDpi.Size = new System.Drawing.Size( 42, 13 );
			this.stDpi.TabIndex = 12;
			this.stDpi.Text = "360 ppi";
			this.stDpi.Click += new System.EventHandler( this.stDpi_Click );
			// 
			// ltStyle
			// 
			this.ltStyle.Label = "Apply Style";
			this.ltStyle.Location = new System.Drawing.Point( 236, 191 );
			this.ltStyle.Name = "ltStyle";
			this.ltStyle.Size = new System.Drawing.Size( 194, 37 );
			this.ltStyle.TabIndex = 11;
			// 
			// stAntiAlias
			// 
			this.stAntiAlias.AutoSize = true;
			this.stAntiAlias.Location = new System.Drawing.Point( 233, 136 );
			this.stAntiAlias.Name = "stAntiAlias";
			this.stAntiAlias.Size = new System.Drawing.Size( 47, 13 );
			this.stAntiAlias.TabIndex = 10;
			this.stAntiAlias.Text = "AntiAlias";
			// 
			// cbAntiAlias
			// 
			this.cbAntiAlias.FormattingEnabled = true;
			this.cbAntiAlias.Items.AddRange( new object[] {
            "Crisp",
            "None",
            "Sharp",
            "Smooth",
            "Strong"} );
			this.cbAntiAlias.Location = new System.Drawing.Point( 236, 152 );
			this.cbAntiAlias.Name = "cbAntiAlias";
			this.cbAntiAlias.Size = new System.Drawing.Size( 137, 21 );
			this.cbAntiAlias.TabIndex = 9;
			// 
			// stJustification
			// 
			this.stJustification.AutoSize = true;
			this.stJustification.Location = new System.Drawing.Point( 16, 136 );
			this.stJustification.Name = "stJustification";
			this.stJustification.Size = new System.Drawing.Size( 62, 13 );
			this.stJustification.TabIndex = 8;
			this.stJustification.Text = "Justification";
			// 
			// cbJustification
			// 
			this.cbJustification.FormattingEnabled = true;
			this.cbJustification.Items.AddRange( new object[] {
            "Left",
            "Centered",
            "Right",
            "Justified"} );
			this.cbJustification.Location = new System.Drawing.Point( 16, 152 );
			this.cbJustification.Name = "cbJustification";
			this.cbJustification.Size = new System.Drawing.Size( 192, 21 );
			this.cbJustification.TabIndex = 7;
			// 
			// rbPixels
			// 
			this.rbPixels.AutoSize = true;
			this.rbPixels.Location = new System.Drawing.Point( 86, 239 );
			this.rbPixels.Name = "rbPixels";
			this.rbPixels.Size = new System.Drawing.Size( 36, 17 );
			this.rbPixels.TabIndex = 6;
			this.rbPixels.TabStop = true;
			this.rbPixels.Text = "px";
			this.rbPixels.UseVisualStyleBackColor = true;
			this.rbPixels.CheckedChanged += new System.EventHandler( this.rbUnits_CheckedChanged );
			// 
			// rbInches
			// 
			this.rbInches.AutoSize = true;
			this.rbInches.Location = new System.Drawing.Point( 123, 239 );
			this.rbInches.Name = "rbInches";
			this.rbInches.Size = new System.Drawing.Size( 33, 17 );
			this.rbInches.TabIndex = 5;
			this.rbInches.TabStop = true;
			this.rbInches.Text = "in";
			this.rbInches.UseVisualStyleBackColor = true;
			this.rbInches.CheckedChanged += new System.EventHandler( this.rbUnits_CheckedChanged );
			// 
			// xyLocation
			// 
			this.xyLocation.Label = "Position";
			this.xyLocation.Location = new System.Drawing.Point( 16, 191 );
			this.xyLocation.Name = "xyLocation";
			dPoint1.X = 0;
			dPoint1.Y = 0;
			this.xyLocation.Point = dPoint1;
			this.xyLocation.Size = new System.Drawing.Size( 194, 42 );
			this.xyLocation.TabIndex = 4;
			this.xyLocation.X = 0;
			this.xyLocation.XVisible = false;
			this.xyLocation.Y = 0;
			// 
			// ltText
			// 
			this.ltText.Label = "Text";
			this.ltText.Location = new System.Drawing.Point( 16, 24 );
			this.ltText.Name = "ltText";
			this.ltText.Size = new System.Drawing.Size( 414, 37 );
			this.ltText.TabIndex = 3;
			// 
			// ltFontSize
			// 
			this.ltFontSize.Label = "Font Size";
			this.ltFontSize.Location = new System.Drawing.Point( 236, 77 );
			this.ltFontSize.Name = "ltFontSize";
			this.ltFontSize.Size = new System.Drawing.Size( 72, 37 );
			this.ltFontSize.TabIndex = 2;
			// 
			// ltFont
			// 
			this.ltFont.Label = "Font";
			this.ltFont.Location = new System.Drawing.Point( 16, 77 );
			this.ltFont.Name = "ltFont";
			this.ltFont.Size = new System.Drawing.Size( 192, 37 );
			this.ltFont.TabIndex = 1;
			// 
			// ltRgbValue
			// 
			this.ltRgbValue.Label = "Font Color (RGB)";
			this.ltRgbValue.Location = new System.Drawing.Point( 338, 77 );
			this.ltRgbValue.Name = "ltRgbValue";
			this.ltRgbValue.Size = new System.Drawing.Size( 92, 37 );
			this.ltRgbValue.TabIndex = 0;
			// 
			// TextItemCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add( this.grpTextData );
			this.Name = "TextItemCtrl";
			this.Size = new System.Drawing.Size( 450, 270 );
			this.Load += new System.EventHandler( this.TextItemCtrl_Load );
			this.grpTextData.ResumeLayout( false );
			this.grpTextData.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private MyControls.MyGroupBox grpTextData;
		private System.Windows.Forms.RadioButton rbPixels;
		private System.Windows.Forms.RadioButton rbInches;
		private MyControls.XYCoordinates xyLocation;
		private MyControls.LabelAndText ltText;
		private MyControls.LabelAndText ltFontSize;
		private MyControls.LabelAndText ltFont;
		private MyControls.LabelAndText ltRgbValue;
		private System.Windows.Forms.Label stJustification;
		private System.Windows.Forms.ComboBox cbJustification;
		private System.Windows.Forms.Label stAntiAlias;
		private System.Windows.Forms.ComboBox cbAntiAlias;
		private MyControls.LabelAndText ltStyle;
		private System.Windows.Forms.Label stDpi;
	}

}
