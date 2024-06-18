using System.Windows.Forms;
using MyControls;
using System.Collections.Generic;

namespace PhotoshopUtilities
{
	partial class PrintSizes
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( PrintSizes ) );
			this.btnPrintSizes = new System.Windows.Forms.Button();
			this.grpGanging = new System.Windows.Forms.GroupBox();
			this.grpGangingNumberRows = new MyControls.MyGroupBox();
			this.ltGanging6x8 = new MyControls.LabelAndText();
			this.ltGanging24x32 = new MyControls.LabelAndText();
			this.ltGanging9x12 = new MyControls.LabelAndText();
			this.ltGanging12x16 = new MyControls.LabelAndText();
			this.ltGanging18x24 = new MyControls.LabelAndText();
			this.ltGanging6x9 = new MyControls.LabelAndText();
			this.ltGanging20x30 = new MyControls.LabelAndText();
			this.ltGanging8x12 = new MyControls.LabelAndText();
			this.ltGanging12x18 = new MyControls.LabelAndText();
			this.ltGanging16x24 = new MyControls.LabelAndText();
			this.grpCombine = new MyControls.MyGroupBox();
			this.chkGangingCombineRotate = new System.Windows.Forms.CheckBox();
			this.rbCombineOnePictureOneSize = new System.Windows.Forms.RadioButton();
			this.rbCombineOnePictureAllSizes = new System.Windows.Forms.RadioButton();
			this.rbCombineMultPicturesAllSizes = new System.Windows.Forms.RadioButton();
			this.rbCombineMultPicturesOneSize = new System.Windows.Forms.RadioButton();
			this.grpPrintSizes = new System.Windows.Forms.GroupBox();
			this.chkPrintSizesNewOnly = new System.Windows.Forms.CheckBox();
			this.grpPrintSizesSizes = new MyControls.MyGroupBox();
			this.chk6x9 = new System.Windows.Forms.CheckBox();
			this.chk8x12 = new System.Windows.Forms.CheckBox();
			this.chk12x16 = new System.Windows.Forms.CheckBox();
			this.chk16x24 = new System.Windows.Forms.CheckBox();
			this.chk18x24 = new System.Windows.Forms.CheckBox();
			this.chk12x18 = new System.Windows.Forms.CheckBox();
			this.chk9x12 = new System.Windows.Forms.CheckBox();
			this.chk6x8 = new System.Windows.Forms.CheckBox();
			this.grpPrintSizesRatio = new MyControls.MyGroupBox();
			this.rbPrintSizesAuto = new System.Windows.Forms.RadioButton();
			this.rbPrintSizes4x3 = new System.Windows.Forms.RadioButton();
			this.rbPrintSizes3x2 = new System.Windows.Forms.RadioButton();
			this.grpPrintSizesDirectories = new System.Windows.Forms.GroupBox();
			this.chkDirectoriesGangedDirectoryRelative = new System.Windows.Forms.CheckBox();
			this.chkDirectoriesPrintsDirectoryRelative = new System.Windows.Forms.CheckBox();
			this.bfdDirectoriesGangedFiles = new MyControls.BrowseForDirectory();
			this.bfdDirectoriesPrintFiles = new MyControls.BrowseForDirectory();
			this.flvImages = new MyControls.FilesListView();
			this.nikSharpenCtrl = new WebFramerCS2ControlLibrary.NikPrintSharpenCtrl();
			this.grpGanging.SuspendLayout();
			this.grpGangingNumberRows.SuspendLayout();
			this.grpCombine.SuspendLayout();
			this.grpPrintSizes.SuspendLayout();
			this.grpPrintSizesSizes.SuspendLayout();
			this.grpPrintSizesRatio.SuspendLayout();
			this.grpPrintSizesDirectories.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnPrintSizes
			// 
			this.btnPrintSizes.AutoSize = true;
			this.btnPrintSizes.Location = new System.Drawing.Point( 360, 391 );
			this.btnPrintSizes.Name = "btnPrintSizes";
			this.btnPrintSizes.Size = new System.Drawing.Size( 71, 23 );
			this.btnPrintSizes.TabIndex = 23;
			this.btnPrintSizes.Text = "Run";
			this.btnPrintSizes.Click += new System.EventHandler( this.btnPrintSizes_Click );
			// 
			// grpGanging
			// 
			this.grpGanging.Controls.Add( this.grpGangingNumberRows );
			this.grpGanging.Controls.Add( this.grpCombine );
			this.grpGanging.Location = new System.Drawing.Point( 446, 222 );
			this.grpGanging.Name = "grpGanging";
			this.grpGanging.Size = new System.Drawing.Size( 382, 249 );
			this.grpGanging.TabIndex = 21;
			this.grpGanging.TabStop = false;
			this.grpGanging.Text = "Ganging";
			// 
			// grpGangingNumberRows
			// 
			this.grpGangingNumberRows.Controls.Add( this.ltGanging6x8 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging24x32 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging9x12 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging12x16 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging18x24 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging6x9 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging20x30 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging8x12 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging12x18 );
			this.grpGangingNumberRows.Controls.Add( this.ltGanging16x24 );
			this.grpGangingNumberRows.Location = new System.Drawing.Point( 19, 18 );
			this.grpGangingNumberRows.Name = "grpGangingNumberRows";
			this.grpGangingNumberRows.Size = new System.Drawing.Size( 346, 115 );
			this.grpGangingNumberRows.TabIndex = 32;
			this.grpGangingNumberRows.TabStop = false;
			this.grpGangingNumberRows.Text = "Number of Rows";
			// 
			// ltGanging6x8
			// 
			this.ltGanging6x8.Label = "6x8";
			this.ltGanging6x8.Location = new System.Drawing.Point( 24, 67 );
			this.ltGanging6x8.Name = "ltGanging6x8";
			this.ltGanging6x8.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging6x8.TabIndex = 32;
			// 
			// ltGanging24x32
			// 
			this.ltGanging24x32.Label = "23x32";
			this.ltGanging24x32.Location = new System.Drawing.Point( 280, 66 );
			this.ltGanging24x32.Name = "ltGanging24x32";
			this.ltGanging24x32.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging24x32.TabIndex = 36;
			// 
			// ltGanging9x12
			// 
			this.ltGanging9x12.Label = "9x12";
			this.ltGanging9x12.Location = new System.Drawing.Point( 88, 66 );
			this.ltGanging9x12.Name = "ltGanging9x12";
			this.ltGanging9x12.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging9x12.TabIndex = 33;
			// 
			// ltGanging12x16
			// 
			this.ltGanging12x16.Label = "12x16";
			this.ltGanging12x16.Location = new System.Drawing.Point( 152, 66 );
			this.ltGanging12x16.Name = "ltGanging12x16";
			this.ltGanging12x16.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging12x16.TabIndex = 35;
			// 
			// ltGanging18x24
			// 
			this.ltGanging18x24.Label = "18x24";
			this.ltGanging18x24.Location = new System.Drawing.Point( 216, 66 );
			this.ltGanging18x24.Name = "ltGanging18x24";
			this.ltGanging18x24.Size = new System.Drawing.Size( 44, 40 );
			this.ltGanging18x24.TabIndex = 34;
			// 
			// ltGanging6x9
			// 
			this.ltGanging6x9.Label = "6x9";
			this.ltGanging6x9.Location = new System.Drawing.Point( 24, 19 );
			this.ltGanging6x9.Name = "ltGanging6x9";
			this.ltGanging6x9.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging6x9.TabIndex = 9;
			// 
			// ltGanging20x30
			// 
			this.ltGanging20x30.Label = "20x30";
			this.ltGanging20x30.Location = new System.Drawing.Point( 280, 18 );
			this.ltGanging20x30.Name = "ltGanging20x30";
			this.ltGanging20x30.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging20x30.TabIndex = 31;
			// 
			// ltGanging8x12
			// 
			this.ltGanging8x12.Label = "8x12";
			this.ltGanging8x12.Location = new System.Drawing.Point( 88, 18 );
			this.ltGanging8x12.Name = "ltGanging8x12";
			this.ltGanging8x12.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging8x12.TabIndex = 28;
			// 
			// ltGanging12x18
			// 
			this.ltGanging12x18.Label = "12x18";
			this.ltGanging12x18.Location = new System.Drawing.Point( 152, 18 );
			this.ltGanging12x18.Name = "ltGanging12x18";
			this.ltGanging12x18.Size = new System.Drawing.Size( 44, 37 );
			this.ltGanging12x18.TabIndex = 30;
			// 
			// ltGanging16x24
			// 
			this.ltGanging16x24.Label = "16x24";
			this.ltGanging16x24.Location = new System.Drawing.Point( 216, 18 );
			this.ltGanging16x24.Name = "ltGanging16x24";
			this.ltGanging16x24.Size = new System.Drawing.Size( 44, 40 );
			this.ltGanging16x24.TabIndex = 29;
			// 
			// grpCombine
			// 
			this.grpCombine.Controls.Add( this.chkGangingCombineRotate );
			this.grpCombine.Controls.Add( this.rbCombineOnePictureOneSize );
			this.grpCombine.Controls.Add( this.rbCombineOnePictureAllSizes );
			this.grpCombine.Controls.Add( this.rbCombineMultPicturesAllSizes );
			this.grpCombine.Controls.Add( this.rbCombineMultPicturesOneSize );
			this.grpCombine.Location = new System.Drawing.Point( 19, 141 );
			this.grpCombine.Name = "grpCombine";
			this.grpCombine.Size = new System.Drawing.Size( 346, 95 );
			this.grpCombine.TabIndex = 27;
			this.grpCombine.TabStop = false;
			this.grpCombine.Text = "Combine";
			// 
			// chkGangingCombineRotate
			// 
			this.chkGangingCombineRotate.AutoSize = true;
			this.chkGangingCombineRotate.Location = new System.Drawing.Point( 132, 70 );
			this.chkGangingCombineRotate.Name = "chkGangingCombineRotate";
			this.chkGangingCombineRotate.Size = new System.Drawing.Size( 58, 17 );
			this.chkGangingCombineRotate.TabIndex = 16;
			this.chkGangingCombineRotate.Text = "Rotate";
			this.chkGangingCombineRotate.UseVisualStyleBackColor = true;
			// 
			// rbCombineOnePictureOneSize
			// 
			this.rbCombineOnePictureOneSize.AutoSize = true;
			this.rbCombineOnePictureOneSize.Location = new System.Drawing.Point( 159, 44 );
			this.rbCombineOnePictureOneSize.Name = "rbCombineOnePictureOneSize";
			this.rbCombineOnePictureOneSize.Size = new System.Drawing.Size( 165, 17 );
			this.rbCombineOnePictureOneSize.TabIndex = 15;
			this.rbCombineOnePictureOneSize.TabStop = true;
			this.rbCombineOnePictureOneSize.Text = "One Picture, One Size (None)";
			this.rbCombineOnePictureOneSize.UseVisualStyleBackColor = true;
			// 
			// rbCombineOnePictureAllSizes
			// 
			this.rbCombineOnePictureAllSizes.AutoSize = true;
			this.rbCombineOnePictureAllSizes.Location = new System.Drawing.Point( 159, 24 );
			this.rbCombineOnePictureAllSizes.Name = "rbCombineOnePictureAllSizes";
			this.rbCombineOnePictureAllSizes.Size = new System.Drawing.Size( 126, 17 );
			this.rbCombineOnePictureAllSizes.TabIndex = 14;
			this.rbCombineOnePictureAllSizes.TabStop = true;
			this.rbCombineOnePictureAllSizes.Text = "One Picture, All Sizes";
			this.rbCombineOnePictureAllSizes.UseVisualStyleBackColor = true;
			// 
			// rbCombineMultPicturesAllSizes
			// 
			this.rbCombineMultPicturesAllSizes.AutoSize = true;
			this.rbCombineMultPicturesAllSizes.Location = new System.Drawing.Point( 16, 24 );
			this.rbCombineMultPicturesAllSizes.Name = "rbCombineMultPicturesAllSizes";
			this.rbCombineMultPicturesAllSizes.Size = new System.Drawing.Size( 122, 17 );
			this.rbCombineMultPicturesAllSizes.TabIndex = 13;
			this.rbCombineMultPicturesAllSizes.TabStop = true;
			this.rbCombineMultPicturesAllSizes.Text = "All Pictures, All Sizes";
			this.rbCombineMultPicturesAllSizes.UseVisualStyleBackColor = true;
			// 
			// rbCombineMultPicturesOneSize
			// 
			this.rbCombineMultPicturesOneSize.AutoSize = true;
			this.rbCombineMultPicturesOneSize.Location = new System.Drawing.Point( 16, 44 );
			this.rbCombineMultPicturesOneSize.Name = "rbCombineMultPicturesOneSize";
			this.rbCombineMultPicturesOneSize.Size = new System.Drawing.Size( 126, 17 );
			this.rbCombineMultPicturesOneSize.TabIndex = 12;
			this.rbCombineMultPicturesOneSize.TabStop = true;
			this.rbCombineMultPicturesOneSize.Text = "All Pictures, One Size";
			this.rbCombineMultPicturesOneSize.UseVisualStyleBackColor = true;
			// 
			// grpPrintSizes
			// 
			this.grpPrintSizes.Controls.Add( this.chkPrintSizesNewOnly );
			this.grpPrintSizes.Controls.Add( this.grpPrintSizesSizes );
			this.grpPrintSizes.Controls.Add( this.grpPrintSizesRatio );
			this.grpPrintSizes.Location = new System.Drawing.Point( 446, 12 );
			this.grpPrintSizes.Name = "grpPrintSizes";
			this.grpPrintSizes.Size = new System.Drawing.Size( 382, 200 );
			this.grpPrintSizes.TabIndex = 20;
			this.grpPrintSizes.TabStop = false;
			this.grpPrintSizes.Text = "Print Sizes";
			// 
			// chkPrintSizesNewOnly
			// 
			this.chkPrintSizesNewOnly.AutoSize = true;
			this.chkPrintSizesNewOnly.Location = new System.Drawing.Point( 40, 154 );
			this.chkPrintSizesNewOnly.Name = "chkPrintSizesNewOnly";
			this.chkPrintSizesNewOnly.Size = new System.Drawing.Size( 115, 17 );
			this.chkPrintSizesNewOnly.TabIndex = 28;
			this.chkPrintSizesNewOnly.Text = "Create If New Only";
			this.chkPrintSizesNewOnly.UseVisualStyleBackColor = true;
			// 
			// grpPrintSizesSizes
			// 
			this.grpPrintSizesSizes.Controls.Add( this.chk6x9 );
			this.grpPrintSizesSizes.Controls.Add( this.chk8x12 );
			this.grpPrintSizesSizes.Controls.Add( this.chk12x16 );
			this.grpPrintSizesSizes.Controls.Add( this.chk16x24 );
			this.grpPrintSizesSizes.Controls.Add( this.chk18x24 );
			this.grpPrintSizesSizes.Controls.Add( this.chk12x18 );
			this.grpPrintSizesSizes.Controls.Add( this.chk9x12 );
			this.grpPrintSizesSizes.Controls.Add( this.chk6x8 );
			this.grpPrintSizesSizes.Location = new System.Drawing.Point( 118, 24 );
			this.grpPrintSizesSizes.Name = "grpPrintSizesSizes";
			this.grpPrintSizesSizes.Size = new System.Drawing.Size( 244, 100 );
			this.grpPrintSizesSizes.TabIndex = 15;
			this.grpPrintSizesSizes.TabStop = false;
			this.grpPrintSizesSizes.Text = "Available Sizes";
			// 
			// chk6x9
			// 
			this.chk6x9.AutoSize = true;
			this.chk6x9.Location = new System.Drawing.Point( 15, 30 );
			this.chk6x9.Name = "chk6x9";
			this.chk6x9.Size = new System.Drawing.Size( 43, 17 );
			this.chk6x9.TabIndex = 0;
			this.chk6x9.Text = "6x9";
			// 
			// chk8x12
			// 
			this.chk8x12.AutoSize = true;
			this.chk8x12.Location = new System.Drawing.Point( 65, 30 );
			this.chk8x12.Name = "chk8x12";
			this.chk8x12.Size = new System.Drawing.Size( 49, 17 );
			this.chk8x12.TabIndex = 1;
			this.chk8x12.Text = "8x12";
			// 
			// chk12x16
			// 
			this.chk12x16.AutoSize = true;
			this.chk12x16.Location = new System.Drawing.Point( 121, 64 );
			this.chk12x16.Name = "chk12x16";
			this.chk12x16.Size = new System.Drawing.Size( 55, 17 );
			this.chk12x16.TabIndex = 13;
			this.chk12x16.Text = "12x16";
			// 
			// chk16x24
			// 
			this.chk16x24.AutoSize = true;
			this.chk16x24.Location = new System.Drawing.Point( 183, 30 );
			this.chk16x24.Name = "chk16x24";
			this.chk16x24.Size = new System.Drawing.Size( 55, 17 );
			this.chk16x24.TabIndex = 2;
			this.chk16x24.Text = "16x24";
			// 
			// chk18x24
			// 
			this.chk18x24.AutoSize = true;
			this.chk18x24.Location = new System.Drawing.Point( 183, 64 );
			this.chk18x24.Name = "chk18x24";
			this.chk18x24.Size = new System.Drawing.Size( 55, 17 );
			this.chk18x24.TabIndex = 12;
			this.chk18x24.Text = "18x24";
			// 
			// chk12x18
			// 
			this.chk12x18.AutoSize = true;
			this.chk12x18.Location = new System.Drawing.Point( 121, 30 );
			this.chk12x18.Name = "chk12x18";
			this.chk12x18.Size = new System.Drawing.Size( 55, 17 );
			this.chk12x18.TabIndex = 3;
			this.chk12x18.Text = "12x18";
			// 
			// chk9x12
			// 
			this.chk9x12.AutoSize = true;
			this.chk9x12.Location = new System.Drawing.Point( 65, 64 );
			this.chk9x12.Name = "chk9x12";
			this.chk9x12.Size = new System.Drawing.Size( 49, 17 );
			this.chk9x12.TabIndex = 11;
			this.chk9x12.Text = "9x12";
			// 
			// chk6x8
			// 
			this.chk6x8.AutoSize = true;
			this.chk6x8.Location = new System.Drawing.Point( 15, 64 );
			this.chk6x8.Name = "chk6x8";
			this.chk6x8.Size = new System.Drawing.Size( 43, 17 );
			this.chk6x8.TabIndex = 10;
			this.chk6x8.Text = "6x8";
			// 
			// grpPrintSizesRatio
			// 
			this.grpPrintSizesRatio.Controls.Add( this.rbPrintSizesAuto );
			this.grpPrintSizesRatio.Controls.Add( this.rbPrintSizes4x3 );
			this.grpPrintSizesRatio.Controls.Add( this.rbPrintSizes3x2 );
			this.grpPrintSizesRatio.Location = new System.Drawing.Point( 18, 24 );
			this.grpPrintSizesRatio.Name = "grpPrintSizesRatio";
			this.grpPrintSizesRatio.Size = new System.Drawing.Size( 86, 100 );
			this.grpPrintSizesRatio.TabIndex = 9;
			this.grpPrintSizesRatio.TabStop = false;
			this.grpPrintSizesRatio.Text = "Picture Ratio";
			// 
			// rbPrintSizesAuto
			// 
			this.rbPrintSizesAuto.AutoSize = true;
			this.rbPrintSizesAuto.Location = new System.Drawing.Point( 18, 76 );
			this.rbPrintSizesAuto.Name = "rbPrintSizesAuto";
			this.rbPrintSizesAuto.Size = new System.Drawing.Size( 47, 17 );
			this.rbPrintSizesAuto.TabIndex = 10;
			this.rbPrintSizesAuto.TabStop = true;
			this.rbPrintSizesAuto.Text = "Auto";
			this.rbPrintSizesAuto.UseVisualStyleBackColor = true;
			this.rbPrintSizesAuto.CheckedChanged += new System.EventHandler( this.rbPrintSizesRatio_CheckedChanged );
			// 
			// rbPrintSizes4x3
			// 
			this.rbPrintSizes4x3.AutoSize = true;
			this.rbPrintSizes4x3.Location = new System.Drawing.Point( 18, 50 );
			this.rbPrintSizes4x3.Name = "rbPrintSizes4x3";
			this.rbPrintSizes4x3.Size = new System.Drawing.Size( 42, 17 );
			this.rbPrintSizes4x3.TabIndex = 9;
			this.rbPrintSizes4x3.TabStop = true;
			this.rbPrintSizes4x3.Text = "4x3";
			this.rbPrintSizes4x3.UseVisualStyleBackColor = true;
			this.rbPrintSizes4x3.CheckedChanged += new System.EventHandler( this.rbPrintSizesRatio_CheckedChanged );
			// 
			// rbPrintSizes3x2
			// 
			this.rbPrintSizes3x2.AutoSize = true;
			this.rbPrintSizes3x2.Location = new System.Drawing.Point( 18, 24 );
			this.rbPrintSizes3x2.Name = "rbPrintSizes3x2";
			this.rbPrintSizes3x2.Size = new System.Drawing.Size( 42, 17 );
			this.rbPrintSizes3x2.TabIndex = 8;
			this.rbPrintSizes3x2.TabStop = true;
			this.rbPrintSizes3x2.Text = "3x2";
			this.rbPrintSizes3x2.UseVisualStyleBackColor = true;
			this.rbPrintSizes3x2.CheckedChanged += new System.EventHandler( this.rbPrintSizesRatio_CheckedChanged );
			// 
			// grpPrintSizesDirectories
			// 
			this.grpPrintSizesDirectories.Controls.Add( this.chkDirectoriesGangedDirectoryRelative );
			this.grpPrintSizesDirectories.Controls.Add( this.chkDirectoriesPrintsDirectoryRelative );
			this.grpPrintSizesDirectories.Controls.Add( this.bfdDirectoriesGangedFiles );
			this.grpPrintSizesDirectories.Controls.Add( this.bfdDirectoriesPrintFiles );
			this.grpPrintSizesDirectories.Location = new System.Drawing.Point( 12, 222 );
			this.grpPrintSizesDirectories.Name = "grpPrintSizesDirectories";
			this.grpPrintSizesDirectories.Size = new System.Drawing.Size( 420, 122 );
			this.grpPrintSizesDirectories.TabIndex = 22;
			this.grpPrintSizesDirectories.TabStop = false;
			this.grpPrintSizesDirectories.Text = "Directories";
			// 
			// chkDirectoriesGangedDirectoryRelative
			// 
			this.chkDirectoriesGangedDirectoryRelative.AutoSize = true;
			this.chkDirectoriesGangedDirectoryRelative.Location = new System.Drawing.Point( 350, 92 );
			this.chkDirectoriesGangedDirectoryRelative.Name = "chkDirectoriesGangedDirectoryRelative";
			this.chkDirectoriesGangedDirectoryRelative.Size = new System.Drawing.Size( 65, 17 );
			this.chkDirectoriesGangedDirectoryRelative.TabIndex = 18;
			this.chkDirectoriesGangedDirectoryRelative.Text = "Relative";
			this.chkDirectoriesGangedDirectoryRelative.UseVisualStyleBackColor = true;
			// 
			// chkDirectoriesPrintsDirectoryRelative
			// 
			this.chkDirectoriesPrintsDirectoryRelative.AutoSize = true;
			this.chkDirectoriesPrintsDirectoryRelative.Location = new System.Drawing.Point( 350, 43 );
			this.chkDirectoriesPrintsDirectoryRelative.Name = "chkDirectoriesPrintsDirectoryRelative";
			this.chkDirectoriesPrintsDirectoryRelative.Size = new System.Drawing.Size( 65, 17 );
			this.chkDirectoriesPrintsDirectoryRelative.TabIndex = 17;
			this.chkDirectoriesPrintsDirectoryRelative.Text = "Relative";
			this.chkDirectoriesPrintsDirectoryRelative.UseVisualStyleBackColor = true;
			// 
			// bfdDirectoriesGangedFiles
			// 
			this.bfdDirectoriesGangedFiles.BrowseLabel = "...";
			this.bfdDirectoriesGangedFiles.Directory = "";
			this.bfdDirectoriesGangedFiles.Label = "Directory for Ganged Files";
			this.bfdDirectoriesGangedFiles.Location = new System.Drawing.Point( 19, 73 );
			this.bfdDirectoriesGangedFiles.Name = "bfdDirectoriesGangedFiles";
			this.bfdDirectoriesGangedFiles.Size = new System.Drawing.Size( 320, 38 );
			this.bfdDirectoriesGangedFiles.TabIndex = 16;
			// 
			// bfdDirectoriesPrintFiles
			// 
			this.bfdDirectoriesPrintFiles.BrowseLabel = "...";
			this.bfdDirectoriesPrintFiles.Directory = "";
			this.bfdDirectoriesPrintFiles.Label = "Directory for Print Files";
			this.bfdDirectoriesPrintFiles.Location = new System.Drawing.Point( 19, 24 );
			this.bfdDirectoriesPrintFiles.Name = "bfdDirectoriesPrintFiles";
			this.bfdDirectoriesPrintFiles.Size = new System.Drawing.Size( 320, 38 );
			this.bfdDirectoriesPrintFiles.TabIndex = 15;
			// 
			// flvImages
			// 
			this.flvImages.FilesFilter = resources.GetString( "flvImages.FilesFilter" );
			this.flvImages.InitialDirectory = "c:\\";
			this.flvImages.Location = new System.Drawing.Point( 12, 12 );
			this.flvImages.MultiSelection = true;
			this.flvImages.Name = "flvImages";
			this.flvImages.Size = new System.Drawing.Size( 420, 200 );
			this.flvImages.Sorting = MyControls.FilesListView.SortDirection.Asc;
			this.flvImages.TabIndex = 26;
			this.flvImages.Title = "Image Files";
			// 
			// nikSharpenCtrl
			// 
			this.nikSharpenCtrl.Location = new System.Drawing.Point( 12, 359 );
			this.nikSharpenCtrl.Name = "nikSharpenCtrl";
			this.nikSharpenCtrl.PaperType = 4;
			this.nikSharpenCtrl.PrinterResolution = 1;
			this.nikSharpenCtrl.SharpenProfile = PhotoshopSupport.NikSharpenInfo.NikProfileType.John;
			this.nikSharpenCtrl.Size = new System.Drawing.Size( 328, 112 );
			this.nikSharpenCtrl.TabIndex = 27;
			// 
			// PrintSizes
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size( 5, 13 );
			this.ClientSize = new System.Drawing.Size( 843, 483 );
			this.Controls.Add( this.nikSharpenCtrl );
			this.Controls.Add( this.flvImages );
			this.Controls.Add( this.btnPrintSizes );
			this.Controls.Add( this.grpGanging );
			this.Controls.Add( this.grpPrintSizes );
			this.Controls.Add( this.grpPrintSizesDirectories );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "PrintSizes";
			this.Text = "Print Sizes";
			this.Load += new System.EventHandler( this.PrintSizes_Load );
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.PrintSizes_FormClosing );
			this.grpGanging.ResumeLayout( false );
			this.grpGangingNumberRows.ResumeLayout( false );
			this.grpCombine.ResumeLayout( false );
			this.grpCombine.PerformLayout();
			this.grpPrintSizes.ResumeLayout( false );
			this.grpPrintSizes.PerformLayout();
			this.grpPrintSizesSizes.ResumeLayout( false );
			this.grpPrintSizesSizes.PerformLayout();
			this.grpPrintSizesRatio.ResumeLayout( false );
			this.grpPrintSizesRatio.PerformLayout();
			this.grpPrintSizesDirectories.ResumeLayout( false );
			this.grpPrintSizesDirectories.PerformLayout();
			this.ResumeLayout( false );
			this.PerformLayout();

		}
		#endregion

		private Button btnPrintSizes;
		private GroupBox grpGanging;
		private GroupBox grpPrintSizes;
		private CheckBox chk12x18;
		private CheckBox chk16x24;
		private CheckBox chk8x12;
		private CheckBox chk6x9;
		private GroupBox grpPrintSizesDirectories;
		private FilesListView flvImages;
		private BrowseForDirectory bfdDirectoriesPrintFiles;
		private BrowseForDirectory bfdDirectoriesGangedFiles;
		private MyGroupBox grpCombine;
		private CheckBox chkPrintSizesNewOnly;
		private LabelAndText ltGanging6x9;
		private MyGroupBox grpPrintSizesRatio;
		private RadioButton rbPrintSizesAuto;
		private RadioButton rbPrintSizes4x3;
		private RadioButton rbPrintSizes3x2;
		private CheckBox chk12x16;
		private CheckBox chk18x24;
		private CheckBox chk9x12;
		private CheckBox chk6x8;
		private MyGroupBox grpPrintSizesSizes;
		private RadioButton rbCombineOnePictureOneSize;
		private RadioButton rbCombineOnePictureAllSizes;
		private RadioButton rbCombineMultPicturesAllSizes;
		private RadioButton rbCombineMultPicturesOneSize;
		private LabelAndText ltGanging20x30;
		private LabelAndText ltGanging12x18;
		private LabelAndText ltGanging16x24;
		private LabelAndText ltGanging8x12;
		private MyGroupBox grpGangingNumberRows;
		private LabelAndText ltGanging6x8;
		private LabelAndText ltGanging24x32;
		private LabelAndText ltGanging9x12;
		private LabelAndText ltGanging12x16;
		private LabelAndText ltGanging18x24;
		private CheckBox chkDirectoriesGangedDirectoryRelative;
		private CheckBox chkDirectoriesPrintsDirectoryRelative;
		private CheckBox chkGangingCombineRotate;
		private WebFramerCS2ControlLibrary.NikPrintSharpenCtrl nikSharpenCtrl;
	}
}