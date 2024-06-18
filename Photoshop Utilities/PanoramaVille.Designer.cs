using System.Collections.Generic;

namespace PhotoshopUtilities
{
	partial class PanoramaVille
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( PanoramaVille ) );
			this.btnProcess = new System.Windows.Forms.Button();
			this.theTabControl = new System.Windows.Forms.TabControl();
			this.tabProject = new System.Windows.Forms.TabPage();
			this.grpRun = new MyControls.MyGroupBox();
			this.lsThreads = new MyControls.LabelAndSpinner();
			this.btnCreate = new System.Windows.Forms.Button();
			this.grpCrop = new MyControls.MyGroupBox();
			this.chkCrop = new System.Windows.Forms.CheckBox();
			this.rbCropLeftAndRight = new System.Windows.Forms.RadioButton();
			this.rbCropTopAndBottom = new System.Windows.Forms.RadioButton();
			this.rbCropUnique = new System.Windows.Forms.RadioButton();
			this.udCropRight = new System.Windows.Forms.NumericUpDown();
			this.udCropBottom = new System.Windows.Forms.NumericUpDown();
			this.udCropLeft = new System.Windows.Forms.NumericUpDown();
			this.udCropTop = new System.Windows.Forms.NumericUpDown();
			this.grpSave = new MyControls.MyGroupBox();
			this.pnlAbsRelSave = new System.Windows.Forms.Panel();
			this.rbSaveAbsolute = new System.Windows.Forms.RadioButton();
			this.rbSaveRelative = new System.Windows.Forms.RadioButton();
			this.rbSaveConvertedAbsolute = new System.Windows.Forms.RadioButton();
			this.rbSaveConvertedRelative = new System.Windows.Forms.RadioButton();
			this.bfdSaveConvertedFiles = new MyControls.BrowseForDirectory();
			this.bfdSaveSaveDirectory = new MyControls.BrowseForDirectory();
			this.bfCurrentProject = new MyControls.BrowseForFile();
			this.chkAutoGenerateProjectName = new System.Windows.Forms.CheckBox();
			this.grpOptionsImage = new MyControls.MyGroupBox();
			this.stOptionsImageBlending = new System.Windows.Forms.Label();
			this.udOptionsImageBlending = new System.Windows.Forms.NumericUpDown();
			this.chkOptionsImageAlign = new System.Windows.Forms.CheckBox();
			this.chkOptionsImageAutoCrop = new System.Windows.Forms.CheckBox();
			this.chkOptionsImageAntiAliasing = new System.Windows.Forms.CheckBox();
			this.chkOptionsImageSharpen = new System.Windows.Forms.CheckBox();
			this.chkOptionsImage360Wrapping = new System.Windows.Forms.CheckBox();
			this.chkOptionsImageAdjustColors = new System.Windows.Forms.CheckBox();
			this.chkDisplayPanorama = new System.Windows.Forms.CheckBox();
			this.grpOptionsStitching = new MyControls.MyGroupBox();
			this.btnOptionsStitchingSearchArea = new System.Windows.Forms.Button();
			this.rbOptionsStitchingManual5or8Flags = new System.Windows.Forms.RadioButton();
			this.rbOptionsStitchingManual3or6Flags = new System.Windows.Forms.RadioButton();
			this.rbOptionsStitchingManual1Flag = new System.Windows.Forms.RadioButton();
			this.rbOptionsStitchingAutomatic = new System.Windows.Forms.RadioButton();
			this.thePanoBox = new System.Windows.Forms.PictureBox();
			this.grpLensDefinition = new MyControls.MyGroupBox();
			this.chkLensDefinitionDX = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.ltLensDefinitionFocalLength = new MyControls.LabelAndText();
			this.ltLensDefinitionLensName = new MyControls.LabelAndText();
			this.grpOrientation = new MyControls.MyGroupBox();
			this.rbOrientationVertical = new System.Windows.Forms.RadioButton();
			this.rbOrientationHorizontal = new System.Windows.Forms.RadioButton();
			this.flvProjectFiles = new MyControls.FilesListView();
			this.flvProjects = new MyControls.FilesListView();
			this.tabAutoGenerate = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.edtReplacementValue = new System.Windows.Forms.TextBox();
			this.grpType = new MyControls.MyGroupBox();
			this.rbTypeSourceDirectory = new System.Windows.Forms.RadioButton();
			this.rbTypeSourceDrive = new System.Windows.Forms.RadioButton();
			this.filesListView1 = new MyControls.FilesListView();
			this.btnReTarget = new System.Windows.Forms.Button();
			this.theProgressBar = new System.Windows.Forms.ProgressBar();
			this.stStatus = new System.Windows.Forms.Label();
			this.theTimer = new System.Windows.Forms.Timer( this.components );
			this.mProgressBar = new MyControls.MultiProgressBar();
			this.chkSwitchToNEFs = new System.Windows.Forms.CheckBox();
			this.theTabControl.SuspendLayout();
			this.tabProject.SuspendLayout();
			this.grpRun.SuspendLayout();
			this.grpCrop.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropRight ) ).BeginInit();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropBottom ) ).BeginInit();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropLeft ) ).BeginInit();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropTop ) ).BeginInit();
			this.grpSave.SuspendLayout();
			this.pnlAbsRelSave.SuspendLayout();
			this.grpOptionsImage.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.udOptionsImageBlending ) ).BeginInit();
			this.grpOptionsStitching.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.thePanoBox ) ).BeginInit();
			this.grpLensDefinition.SuspendLayout();
			this.grpOrientation.SuspendLayout();
			this.tabAutoGenerate.SuspendLayout();
			this.grpType.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point( 26, 72 );
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size( 75, 23 );
			this.btnProcess.TabIndex = 1;
			this.btnProcess.Text = "Process";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler( this.btnProcess_Click );
			// 
			// theTabControl
			// 
			this.theTabControl.Controls.Add( this.tabProject );
			this.theTabControl.Controls.Add( this.tabAutoGenerate );
			this.theTabControl.Location = new System.Drawing.Point( 2, 3 );
			this.theTabControl.Name = "theTabControl";
			this.theTabControl.SelectedIndex = 0;
			this.theTabControl.Size = new System.Drawing.Size( 908, 700 );
			this.theTabControl.TabIndex = 9;
			// 
			// tabProject
			// 
			this.tabProject.Controls.Add( this.grpRun );
			this.tabProject.Controls.Add( this.grpCrop );
			this.tabProject.Controls.Add( this.grpSave );
			this.tabProject.Controls.Add( this.grpOptionsImage );
			this.tabProject.Controls.Add( this.chkDisplayPanorama );
			this.tabProject.Controls.Add( this.grpOptionsStitching );
			this.tabProject.Controls.Add( this.thePanoBox );
			this.tabProject.Controls.Add( this.grpLensDefinition );
			this.tabProject.Controls.Add( this.grpOrientation );
			this.tabProject.Controls.Add( this.flvProjectFiles );
			this.tabProject.Controls.Add( this.flvProjects );
			this.tabProject.Location = new System.Drawing.Point( 4, 22 );
			this.tabProject.Name = "tabProject";
			this.tabProject.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabProject.Size = new System.Drawing.Size( 900, 674 );
			this.tabProject.TabIndex = 0;
			this.tabProject.Text = "Project";
			this.tabProject.UseVisualStyleBackColor = true;
			// 
			// grpRun
			// 
			this.grpRun.Controls.Add( this.lsThreads );
			this.grpRun.Controls.Add( this.btnProcess );
			this.grpRun.Controls.Add( this.btnCreate );
			this.grpRun.Location = new System.Drawing.Point( 16, 520 );
			this.grpRun.Name = "grpRun";
			this.grpRun.Size = new System.Drawing.Size( 129, 139 );
			this.grpRun.TabIndex = 7;
			this.grpRun.TabStop = false;
			this.grpRun.Text = "Run";
			// 
			// lsThreads
			// 
			this.lsThreads.Increment = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			this.lsThreads.Label = "# Threads";
			this.lsThreads.Location = new System.Drawing.Point( 14, 107 );
			this.lsThreads.Max = new decimal( new int[] {
            100,
            0,
            0,
            0} );
			this.lsThreads.Min = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			this.lsThreads.Name = "lsThreads";
			this.lsThreads.Size = new System.Drawing.Size( 100, 20 );
			this.lsThreads.TabIndex = 2;
			this.lsThreads.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point( 26, 18 );
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size( 75, 23 );
			this.btnCreate.TabIndex = 0;
			this.btnCreate.Text = "Create";
			this.btnCreate.UseVisualStyleBackColor = true;
			this.btnCreate.Click += new System.EventHandler( this.btnCreate_Click );
			// 
			// grpCrop
			// 
			this.grpCrop.Controls.Add( this.chkCrop );
			this.grpCrop.Controls.Add( this.rbCropLeftAndRight );
			this.grpCrop.Controls.Add( this.rbCropTopAndBottom );
			this.grpCrop.Controls.Add( this.rbCropUnique );
			this.grpCrop.Controls.Add( this.udCropRight );
			this.grpCrop.Controls.Add( this.udCropBottom );
			this.grpCrop.Controls.Add( this.udCropLeft );
			this.grpCrop.Controls.Add( this.udCropTop );
			this.grpCrop.Location = new System.Drawing.Point( 630, 520 );
			this.grpCrop.Name = "grpCrop";
			this.grpCrop.Size = new System.Drawing.Size( 251, 139 );
			this.grpCrop.TabIndex = 8;
			this.grpCrop.TabStop = false;
			this.grpCrop.Text = "Crop";
			// 
			// chkCrop
			// 
			this.chkCrop.AutoSize = true;
			this.chkCrop.Location = new System.Drawing.Point( 30, 106 );
			this.chkCrop.Name = "chkCrop";
			this.chkCrop.Size = new System.Drawing.Size( 132, 17 );
			this.chkCrop.TabIndex = 0;
			this.chkCrop.Text = "Crop Project\'s Pictures";
			this.chkCrop.UseVisualStyleBackColor = true;
			this.chkCrop.CheckedChanged += new System.EventHandler( this.chkCrop_CheckedChanged );
			// 
			// rbCropLeftAndRight
			// 
			this.rbCropLeftAndRight.AutoSize = true;
			this.rbCropLeftAndRight.Location = new System.Drawing.Point( 154, 73 );
			this.rbCropLeftAndRight.Name = "rbCropLeftAndRight";
			this.rbCropLeftAndRight.Size = new System.Drawing.Size( 80, 17 );
			this.rbCropLeftAndRight.TabIndex = 7;
			this.rbCropLeftAndRight.TabStop = true;
			this.rbCropLeftAndRight.Text = "Left && Right";
			this.rbCropLeftAndRight.UseVisualStyleBackColor = true;
			this.rbCropLeftAndRight.CheckedChanged += new System.EventHandler( this.rbCropTypeCheckedChanged );
			// 
			// rbCropTopAndBottom
			// 
			this.rbCropTopAndBottom.AutoSize = true;
			this.rbCropTopAndBottom.Location = new System.Drawing.Point( 154, 48 );
			this.rbCropTopAndBottom.Name = "rbCropTopAndBottom";
			this.rbCropTopAndBottom.Size = new System.Drawing.Size( 89, 17 );
			this.rbCropTopAndBottom.TabIndex = 6;
			this.rbCropTopAndBottom.TabStop = true;
			this.rbCropTopAndBottom.Text = "Top && Bottom";
			this.rbCropTopAndBottom.UseVisualStyleBackColor = true;
			this.rbCropTopAndBottom.CheckedChanged += new System.EventHandler( this.rbCropTypeCheckedChanged );
			// 
			// rbCropUnique
			// 
			this.rbCropUnique.AutoSize = true;
			this.rbCropUnique.Location = new System.Drawing.Point( 154, 23 );
			this.rbCropUnique.Name = "rbCropUnique";
			this.rbCropUnique.Size = new System.Drawing.Size( 59, 17 );
			this.rbCropUnique.TabIndex = 5;
			this.rbCropUnique.TabStop = true;
			this.rbCropUnique.Text = "Unique";
			this.rbCropUnique.UseVisualStyleBackColor = true;
			this.rbCropUnique.CheckedChanged += new System.EventHandler( this.rbCropTypeCheckedChanged );
			// 
			// udCropRight
			// 
			this.udCropRight.DecimalPlaces = 1;
			this.udCropRight.Increment = new decimal( new int[] {
            5,
            0,
            0,
            65536} );
			this.udCropRight.Location = new System.Drawing.Point( 97, 45 );
			this.udCropRight.Maximum = new decimal( new int[] {
            50,
            0,
            0,
            0} );
			this.udCropRight.Name = "udCropRight";
			this.udCropRight.Size = new System.Drawing.Size( 40, 20 );
			this.udCropRight.TabIndex = 3;
			// 
			// udCropBottom
			// 
			this.udCropBottom.DecimalPlaces = 1;
			this.udCropBottom.Increment = new decimal( new int[] {
            5,
            0,
            0,
            65536} );
			this.udCropBottom.Location = new System.Drawing.Point( 57, 68 );
			this.udCropBottom.Maximum = new decimal( new int[] {
            50,
            0,
            0,
            0} );
			this.udCropBottom.Name = "udCropBottom";
			this.udCropBottom.Size = new System.Drawing.Size( 40, 20 );
			this.udCropBottom.TabIndex = 4;
			// 
			// udCropLeft
			// 
			this.udCropLeft.DecimalPlaces = 1;
			this.udCropLeft.Increment = new decimal( new int[] {
            5,
            0,
            0,
            65536} );
			this.udCropLeft.Location = new System.Drawing.Point( 16, 45 );
			this.udCropLeft.Maximum = new decimal( new int[] {
            50,
            0,
            0,
            0} );
			this.udCropLeft.Name = "udCropLeft";
			this.udCropLeft.Size = new System.Drawing.Size( 40, 20 );
			this.udCropLeft.TabIndex = 2;
			// 
			// udCropTop
			// 
			this.udCropTop.DecimalPlaces = 1;
			this.udCropTop.Increment = new decimal( new int[] {
            5,
            0,
            0,
            65536} );
			this.udCropTop.Location = new System.Drawing.Point( 57, 21 );
			this.udCropTop.Maximum = new decimal( new int[] {
            50,
            0,
            0,
            0} );
			this.udCropTop.Name = "udCropTop";
			this.udCropTop.Size = new System.Drawing.Size( 40, 20 );
			this.udCropTop.TabIndex = 1;
			// 
			// grpSave
			// 
			this.grpSave.Controls.Add( this.pnlAbsRelSave );
			this.grpSave.Controls.Add( this.rbSaveConvertedAbsolute );
			this.grpSave.Controls.Add( this.rbSaveConvertedRelative );
			this.grpSave.Controls.Add( this.bfdSaveConvertedFiles );
			this.grpSave.Controls.Add( this.bfdSaveSaveDirectory );
			this.grpSave.Controls.Add( this.bfCurrentProject );
			this.grpSave.Controls.Add( this.chkAutoGenerateProjectName );
			this.grpSave.Location = new System.Drawing.Point( 16, 374 );
			this.grpSave.Name = "grpSave";
			this.grpSave.Size = new System.Drawing.Size( 865, 122 );
			this.grpSave.TabIndex = 6;
			this.grpSave.TabStop = false;
			this.grpSave.Text = "Save";
			// 
			// pnlAbsRelSave
			// 
			this.pnlAbsRelSave.Controls.Add( this.rbSaveAbsolute );
			this.pnlAbsRelSave.Controls.Add( this.rbSaveRelative );
			this.pnlAbsRelSave.Location = new System.Drawing.Point( 352, 72 );
			this.pnlAbsRelSave.Name = "pnlAbsRelSave";
			this.pnlAbsRelSave.Size = new System.Drawing.Size( 72, 45 );
			this.pnlAbsRelSave.TabIndex = 8;
			// 
			// rbSaveAbsolute
			// 
			this.rbSaveAbsolute.AutoSize = true;
			this.rbSaveAbsolute.Location = new System.Drawing.Point( 4, 6 );
			this.rbSaveAbsolute.Name = "rbSaveAbsolute";
			this.rbSaveAbsolute.Size = new System.Drawing.Size( 66, 17 );
			this.rbSaveAbsolute.TabIndex = 2;
			this.rbSaveAbsolute.TabStop = true;
			this.rbSaveAbsolute.Text = "Absolute";
			this.rbSaveAbsolute.UseVisualStyleBackColor = true;
			// 
			// rbSaveRelative
			// 
			this.rbSaveRelative.AutoSize = true;
			this.rbSaveRelative.Location = new System.Drawing.Point( 4, 23 );
			this.rbSaveRelative.Name = "rbSaveRelative";
			this.rbSaveRelative.Size = new System.Drawing.Size( 64, 17 );
			this.rbSaveRelative.TabIndex = 3;
			this.rbSaveRelative.TabStop = true;
			this.rbSaveRelative.Text = "Relative";
			this.rbSaveRelative.UseVisualStyleBackColor = true;
			// 
			// rbSaveConvertedAbsolute
			// 
			this.rbSaveConvertedAbsolute.AutoSize = true;
			this.rbSaveConvertedAbsolute.Location = new System.Drawing.Point( 707, 78 );
			this.rbSaveConvertedAbsolute.Name = "rbSaveConvertedAbsolute";
			this.rbSaveConvertedAbsolute.Size = new System.Drawing.Size( 66, 17 );
			this.rbSaveConvertedAbsolute.TabIndex = 5;
			this.rbSaveConvertedAbsolute.TabStop = true;
			this.rbSaveConvertedAbsolute.Text = "Absolute";
			this.rbSaveConvertedAbsolute.UseVisualStyleBackColor = true;
			// 
			// rbSaveConvertedRelative
			// 
			this.rbSaveConvertedRelative.AutoSize = true;
			this.rbSaveConvertedRelative.Location = new System.Drawing.Point( 707, 95 );
			this.rbSaveConvertedRelative.Name = "rbSaveConvertedRelative";
			this.rbSaveConvertedRelative.Size = new System.Drawing.Size( 64, 17 );
			this.rbSaveConvertedRelative.TabIndex = 6;
			this.rbSaveConvertedRelative.TabStop = true;
			this.rbSaveConvertedRelative.Text = "Relative";
			this.rbSaveConvertedRelative.UseVisualStyleBackColor = true;
			// 
			// bfdSaveConvertedFiles
			// 
			this.bfdSaveConvertedFiles.BrowseLabel = "...";
			this.bfdSaveConvertedFiles.Directory = "";
			this.bfdSaveConvertedFiles.Label = "Save Converted/Cropped Files Directory";
			this.bfdSaveConvertedFiles.Location = new System.Drawing.Point( 435, 69 );
			this.bfdSaveConvertedFiles.Name = "bfdSaveConvertedFiles";
			this.bfdSaveConvertedFiles.Size = new System.Drawing.Size( 262, 38 );
			this.bfdSaveConvertedFiles.TabIndex = 4;
			// 
			// bfdSaveSaveDirectory
			// 
			this.bfdSaveSaveDirectory.BrowseLabel = "...";
			this.bfdSaveSaveDirectory.Directory = "";
			this.bfdSaveSaveDirectory.Label = "Save Directory";
			this.bfdSaveSaveDirectory.Location = new System.Drawing.Point( 16, 69 );
			this.bfdSaveSaveDirectory.Name = "bfdSaveSaveDirectory";
			this.bfdSaveSaveDirectory.Size = new System.Drawing.Size( 330, 38 );
			this.bfdSaveSaveDirectory.TabIndex = 1;
			// 
			// bfCurrentProject
			// 
			this.bfCurrentProject.BrowseLabel = "...";
			this.bfCurrentProject.CheckFileExists = false;
			this.bfCurrentProject.DefaultExtension = null;
			this.bfCurrentProject.DisplayFileNameOnly = true;
			this.bfCurrentProject.FileName = "";
			this.bfCurrentProject.FilesFilter = "Project Files (*.xia;*.pia;*.vst)|*.xia;*.pia;*.vst";
			this.bfCurrentProject.FilterIndex = 1;
			this.bfCurrentProject.InitialDirectory = "c:\\";
			this.bfCurrentProject.Label = "Current Project";
			this.bfCurrentProject.Location = new System.Drawing.Point( 16, 21 );
			this.bfCurrentProject.Name = "bfCurrentProject";
			this.bfCurrentProject.Size = new System.Drawing.Size( 681, 38 );
			this.bfCurrentProject.TabIndex = 0;
			// 
			// chkAutoGenerateProjectName
			// 
			this.chkAutoGenerateProjectName.AutoSize = true;
			this.chkAutoGenerateProjectName.Location = new System.Drawing.Point( 738, 39 );
			this.chkAutoGenerateProjectName.Name = "chkAutoGenerateProjectName";
			this.chkAutoGenerateProjectName.Size = new System.Drawing.Size( 95, 17 );
			this.chkAutoGenerateProjectName.TabIndex = 7;
			this.chkAutoGenerateProjectName.Text = "Auto Generate";
			this.chkAutoGenerateProjectName.UseVisualStyleBackColor = true;
			this.chkAutoGenerateProjectName.CheckedChanged += new System.EventHandler( this.chkAutoGenerateProjectName_CheckedChanged );
			// 
			// grpOptionsImage
			// 
			this.grpOptionsImage.Controls.Add( this.stOptionsImageBlending );
			this.grpOptionsImage.Controls.Add( this.udOptionsImageBlending );
			this.grpOptionsImage.Controls.Add( this.chkOptionsImageAlign );
			this.grpOptionsImage.Controls.Add( this.chkOptionsImageAutoCrop );
			this.grpOptionsImage.Controls.Add( this.chkOptionsImageAntiAliasing );
			this.grpOptionsImage.Controls.Add( this.chkOptionsImageSharpen );
			this.grpOptionsImage.Controls.Add( this.chkOptionsImage360Wrapping );
			this.grpOptionsImage.Controls.Add( this.chkOptionsImageAdjustColors );
			this.grpOptionsImage.Location = new System.Drawing.Point( 412, 232 );
			this.grpOptionsImage.Name = "grpOptionsImage";
			this.grpOptionsImage.Size = new System.Drawing.Size( 286, 124 );
			this.grpOptionsImage.TabIndex = 4;
			this.grpOptionsImage.TabStop = false;
			this.grpOptionsImage.Text = "Image Options";
			// 
			// stOptionsImageBlending
			// 
			this.stOptionsImageBlending.AutoSize = true;
			this.stOptionsImageBlending.Location = new System.Drawing.Point( 22, 26 );
			this.stOptionsImageBlending.Name = "stOptionsImageBlending";
			this.stOptionsImageBlending.Size = new System.Drawing.Size( 97, 13 );
			this.stOptionsImageBlending.TabIndex = 0;
			this.stOptionsImageBlending.Text = "Image Blending (%)";
			// 
			// udOptionsImageBlending
			// 
			this.udOptionsImageBlending.Location = new System.Drawing.Point( 132, 23 );
			this.udOptionsImageBlending.Name = "udOptionsImageBlending";
			this.udOptionsImageBlending.Size = new System.Drawing.Size( 42, 20 );
			this.udOptionsImageBlending.TabIndex = 4;
			// 
			// chkOptionsImageAlign
			// 
			this.chkOptionsImageAlign.AutoSize = true;
			this.chkOptionsImageAlign.Location = new System.Drawing.Point( 140, 100 );
			this.chkOptionsImageAlign.Name = "chkOptionsImageAlign";
			this.chkOptionsImageAlign.Size = new System.Drawing.Size( 131, 17 );
			this.chkOptionsImageAlign.TabIndex = 7;
			this.chkOptionsImageAlign.Text = "Align Row Horizontally";
			this.chkOptionsImageAlign.UseVisualStyleBackColor = true;
			// 
			// chkOptionsImageAutoCrop
			// 
			this.chkOptionsImageAutoCrop.AutoSize = true;
			this.chkOptionsImageAutoCrop.Location = new System.Drawing.Point( 140, 77 );
			this.chkOptionsImageAutoCrop.Name = "chkOptionsImageAutoCrop";
			this.chkOptionsImageAutoCrop.Size = new System.Drawing.Size( 73, 17 );
			this.chkOptionsImageAutoCrop.TabIndex = 6;
			this.chkOptionsImageAutoCrop.Text = "Auto Crop";
			this.chkOptionsImageAutoCrop.UseVisualStyleBackColor = true;
			// 
			// chkOptionsImageAntiAliasing
			// 
			this.chkOptionsImageAntiAliasing.AutoSize = true;
			this.chkOptionsImageAntiAliasing.Location = new System.Drawing.Point( 140, 54 );
			this.chkOptionsImageAntiAliasing.Name = "chkOptionsImageAntiAliasing";
			this.chkOptionsImageAntiAliasing.Size = new System.Drawing.Size( 83, 17 );
			this.chkOptionsImageAntiAliasing.TabIndex = 5;
			this.chkOptionsImageAntiAliasing.Text = "Anti-Aliasing";
			this.chkOptionsImageAntiAliasing.UseVisualStyleBackColor = true;
			this.chkOptionsImageAntiAliasing.CheckedChanged += new System.EventHandler( this.chkOptionsImageAntiAliasing_CheckedChanged );
			// 
			// chkOptionsImageSharpen
			// 
			this.chkOptionsImageSharpen.AutoSize = true;
			this.chkOptionsImageSharpen.Location = new System.Drawing.Point( 24, 100 );
			this.chkOptionsImageSharpen.Name = "chkOptionsImageSharpen";
			this.chkOptionsImageSharpen.Size = new System.Drawing.Size( 66, 17 );
			this.chkOptionsImageSharpen.TabIndex = 3;
			this.chkOptionsImageSharpen.Text = "Sharpen";
			this.chkOptionsImageSharpen.UseVisualStyleBackColor = true;
			// 
			// chkOptionsImage360Wrapping
			// 
			this.chkOptionsImage360Wrapping.AutoSize = true;
			this.chkOptionsImage360Wrapping.Location = new System.Drawing.Point( 24, 77 );
			this.chkOptionsImage360Wrapping.Name = "chkOptionsImage360Wrapping";
			this.chkOptionsImage360Wrapping.Size = new System.Drawing.Size( 93, 17 );
			this.chkOptionsImage360Wrapping.TabIndex = 2;
			this.chkOptionsImage360Wrapping.Text = "360 Wrapping";
			this.chkOptionsImage360Wrapping.UseVisualStyleBackColor = true;
			// 
			// chkOptionsImageAdjustColors
			// 
			this.chkOptionsImageAdjustColors.AutoSize = true;
			this.chkOptionsImageAdjustColors.Location = new System.Drawing.Point( 24, 54 );
			this.chkOptionsImageAdjustColors.Name = "chkOptionsImageAdjustColors";
			this.chkOptionsImageAdjustColors.Size = new System.Drawing.Size( 87, 17 );
			this.chkOptionsImageAdjustColors.TabIndex = 1;
			this.chkOptionsImageAdjustColors.Text = "Adjust Colors";
			this.chkOptionsImageAdjustColors.UseVisualStyleBackColor = true;
			// 
			// chkDisplayPanorama
			// 
			this.chkDisplayPanorama.AutoSize = true;
			this.chkDisplayPanorama.Location = new System.Drawing.Point( 348, 642 );
			this.chkDisplayPanorama.Name = "chkDisplayPanorama";
			this.chkDisplayPanorama.Size = new System.Drawing.Size( 111, 17 );
			this.chkDisplayPanorama.TabIndex = 9;
			this.chkDisplayPanorama.Text = "Display Panorama";
			this.chkDisplayPanorama.UseVisualStyleBackColor = true;
			// 
			// grpOptionsStitching
			// 
			this.grpOptionsStitching.Controls.Add( this.btnOptionsStitchingSearchArea );
			this.grpOptionsStitching.Controls.Add( this.rbOptionsStitchingManual5or8Flags );
			this.grpOptionsStitching.Controls.Add( this.rbOptionsStitchingManual3or6Flags );
			this.grpOptionsStitching.Controls.Add( this.rbOptionsStitchingManual1Flag );
			this.grpOptionsStitching.Controls.Add( this.rbOptionsStitchingAutomatic );
			this.grpOptionsStitching.Location = new System.Drawing.Point( 138, 232 );
			this.grpOptionsStitching.Name = "grpOptionsStitching";
			this.grpOptionsStitching.Size = new System.Drawing.Size( 250, 124 );
			this.grpOptionsStitching.TabIndex = 3;
			this.grpOptionsStitching.TabStop = false;
			this.grpOptionsStitching.Text = "Stitching Options";
			// 
			// btnOptionsStitchingSearchArea
			// 
			this.btnOptionsStitchingSearchArea.Location = new System.Drawing.Point( 146, 21 );
			this.btnOptionsStitchingSearchArea.Name = "btnOptionsStitchingSearchArea";
			this.btnOptionsStitchingSearchArea.Size = new System.Drawing.Size( 75, 23 );
			this.btnOptionsStitchingSearchArea.TabIndex = 4;
			this.btnOptionsStitchingSearchArea.Text = "Search Area";
			this.btnOptionsStitchingSearchArea.UseVisualStyleBackColor = true;
			// 
			// rbOptionsStitchingManual5or8Flags
			// 
			this.rbOptionsStitchingManual5or8Flags.AutoSize = true;
			this.rbOptionsStitchingManual5or8Flags.Location = new System.Drawing.Point( 26, 94 );
			this.rbOptionsStitchingManual5or8Flags.Name = "rbOptionsStitchingManual5or8Flags";
			this.rbOptionsStitchingManual5or8Flags.Size = new System.Drawing.Size( 118, 17 );
			this.rbOptionsStitchingManual5or8Flags.TabIndex = 3;
			this.rbOptionsStitchingManual5or8Flags.TabStop = true;
			this.rbOptionsStitchingManual5or8Flags.Text = "Manual 5 or 8 Flags";
			this.rbOptionsStitchingManual5or8Flags.UseVisualStyleBackColor = true;
			// 
			// rbOptionsStitchingManual3or6Flags
			// 
			this.rbOptionsStitchingManual3or6Flags.AutoSize = true;
			this.rbOptionsStitchingManual3or6Flags.Location = new System.Drawing.Point( 26, 71 );
			this.rbOptionsStitchingManual3or6Flags.Name = "rbOptionsStitchingManual3or6Flags";
			this.rbOptionsStitchingManual3or6Flags.Size = new System.Drawing.Size( 118, 17 );
			this.rbOptionsStitchingManual3or6Flags.TabIndex = 2;
			this.rbOptionsStitchingManual3or6Flags.TabStop = true;
			this.rbOptionsStitchingManual3or6Flags.Text = "Manual 3 or 6 Flags";
			this.rbOptionsStitchingManual3or6Flags.UseVisualStyleBackColor = true;
			// 
			// rbOptionsStitchingManual1Flag
			// 
			this.rbOptionsStitchingManual1Flag.AutoSize = true;
			this.rbOptionsStitchingManual1Flag.Location = new System.Drawing.Point( 26, 48 );
			this.rbOptionsStitchingManual1Flag.Name = "rbOptionsStitchingManual1Flag";
			this.rbOptionsStitchingManual1Flag.Size = new System.Drawing.Size( 92, 17 );
			this.rbOptionsStitchingManual1Flag.TabIndex = 1;
			this.rbOptionsStitchingManual1Flag.TabStop = true;
			this.rbOptionsStitchingManual1Flag.Text = "Manual 1 Flag";
			this.rbOptionsStitchingManual1Flag.UseVisualStyleBackColor = true;
			// 
			// rbOptionsStitchingAutomatic
			// 
			this.rbOptionsStitchingAutomatic.AutoSize = true;
			this.rbOptionsStitchingAutomatic.Location = new System.Drawing.Point( 26, 25 );
			this.rbOptionsStitchingAutomatic.Name = "rbOptionsStitchingAutomatic";
			this.rbOptionsStitchingAutomatic.Size = new System.Drawing.Size( 72, 17 );
			this.rbOptionsStitchingAutomatic.TabIndex = 0;
			this.rbOptionsStitchingAutomatic.TabStop = true;
			this.rbOptionsStitchingAutomatic.Text = "Automatic";
			this.rbOptionsStitchingAutomatic.UseVisualStyleBackColor = true;
			this.rbOptionsStitchingAutomatic.CheckedChanged += new System.EventHandler( this.rbOptionsStitchingAutomatic_CheckedChanged );
			// 
			// thePanoBox
			// 
			this.thePanoBox.Anchor = ( (System.Windows.Forms.AnchorStyles) ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) ) );
			this.thePanoBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.thePanoBox.Location = new System.Drawing.Point( 160, 520 );
			this.thePanoBox.Name = "thePanoBox";
			this.thePanoBox.Size = new System.Drawing.Size( 448, 100 );
			this.thePanoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.thePanoBox.TabIndex = 17;
			this.thePanoBox.TabStop = false;
			this.thePanoBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler( this.thePanoBox_LoadCompleted );
			this.thePanoBox.LoadProgressChanged += new System.ComponentModel.ProgressChangedEventHandler( this.thePanoBox_LoadProgressChanged );
			// 
			// grpLensDefinition
			// 
			this.grpLensDefinition.Controls.Add( this.chkLensDefinitionDX );
			this.grpLensDefinition.Controls.Add( this.label1 );
			this.grpLensDefinition.Controls.Add( this.ltLensDefinitionFocalLength );
			this.grpLensDefinition.Controls.Add( this.ltLensDefinitionLensName );
			this.grpLensDefinition.Location = new System.Drawing.Point( 717, 232 );
			this.grpLensDefinition.Name = "grpLensDefinition";
			this.grpLensDefinition.Size = new System.Drawing.Size( 164, 118 );
			this.grpLensDefinition.TabIndex = 5;
			this.grpLensDefinition.TabStop = false;
			this.grpLensDefinition.Text = "Lens Definition";
			// 
			// chkLensDefinitionDX
			// 
			this.chkLensDefinitionDX.AutoSize = true;
			this.chkLensDefinitionDX.Location = new System.Drawing.Point( 119, 90 );
			this.chkLensDefinitionDX.Name = "chkLensDefinitionDX";
			this.chkLensDefinitionDX.Size = new System.Drawing.Size( 41, 17 );
			this.chkLensDefinitionDX.TabIndex = 3;
			this.chkLensDefinitionDX.Text = "DX";
			this.chkLensDefinitionDX.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 90, 91 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 23, 13 );
			this.label1.TabIndex = 2;
			this.label1.Text = "mm";
			// 
			// ltLensDefinitionFocalLength
			// 
			this.ltLensDefinitionFocalLength.Label = "Focal Length";
			this.ltLensDefinitionFocalLength.Location = new System.Drawing.Point( 12, 70 );
			this.ltLensDefinitionFocalLength.Name = "ltLensDefinitionFocalLength";
			this.ltLensDefinitionFocalLength.Size = new System.Drawing.Size( 75, 37 );
			this.ltLensDefinitionFocalLength.TabIndex = 1;
			// 
			// ltLensDefinitionLensName
			// 
			this.ltLensDefinitionLensName.Label = "Lens Name";
			this.ltLensDefinitionLensName.Location = new System.Drawing.Point( 12, 22 );
			this.ltLensDefinitionLensName.Name = "ltLensDefinitionLensName";
			this.ltLensDefinitionLensName.Size = new System.Drawing.Size( 140, 37 );
			this.ltLensDefinitionLensName.TabIndex = 0;
			// 
			// grpOrientation
			// 
			this.grpOrientation.Controls.Add( this.rbOrientationVertical );
			this.grpOrientation.Controls.Add( this.rbOrientationHorizontal );
			this.grpOrientation.Location = new System.Drawing.Point( 16, 232 );
			this.grpOrientation.Name = "grpOrientation";
			this.grpOrientation.Size = new System.Drawing.Size( 101, 124 );
			this.grpOrientation.TabIndex = 2;
			this.grpOrientation.TabStop = false;
			this.grpOrientation.Text = "Orientation";
			// 
			// rbOrientationVertical
			// 
			this.rbOrientationVertical.AutoSize = true;
			this.rbOrientationVertical.Location = new System.Drawing.Point( 16, 76 );
			this.rbOrientationVertical.Name = "rbOrientationVertical";
			this.rbOrientationVertical.Size = new System.Drawing.Size( 60, 17 );
			this.rbOrientationVertical.TabIndex = 1;
			this.rbOrientationVertical.TabStop = true;
			this.rbOrientationVertical.Text = "Vertical";
			this.rbOrientationVertical.UseVisualStyleBackColor = true;
			// 
			// rbOrientationHorizontal
			// 
			this.rbOrientationHorizontal.AutoSize = true;
			this.rbOrientationHorizontal.Location = new System.Drawing.Point( 16, 35 );
			this.rbOrientationHorizontal.Name = "rbOrientationHorizontal";
			this.rbOrientationHorizontal.Size = new System.Drawing.Size( 72, 17 );
			this.rbOrientationHorizontal.TabIndex = 0;
			this.rbOrientationHorizontal.TabStop = true;
			this.rbOrientationHorizontal.Text = "Horizontal";
			this.rbOrientationHorizontal.UseVisualStyleBackColor = true;
			// 
			// flvProjectFiles
			// 
			this.flvProjectFiles.FilesFilter = resources.GetString( "flvProjectFiles.FilesFilter" );
			this.flvProjectFiles.InitialDirectory = "c:\\";
			this.flvProjectFiles.Location = new System.Drawing.Point( 461, 13 );
			this.flvProjectFiles.MultiSelection = false;
			this.flvProjectFiles.Name = "flvProjectFiles";
			this.flvProjectFiles.Size = new System.Drawing.Size( 420, 200 );
			this.flvProjectFiles.Sorting = MyControls.FilesListView.SortDirection.Asc;
			this.flvProjectFiles.TabIndex = 1;
			this.flvProjectFiles.Title = "Image Files";
			// 
			// flvProjects
			// 
			this.flvProjects.FilesFilter = "Project Files (*.XIA;*.PIA;*.VST)|*.XIA;*.PIA;*.VST";
			this.flvProjects.InitialDirectory = "";
			this.flvProjects.Location = new System.Drawing.Point( 16, 13 );
			this.flvProjects.MultiSelection = false;
			this.flvProjects.Name = "flvProjects";
			this.flvProjects.Size = new System.Drawing.Size( 420, 200 );
			this.flvProjects.Sorting = MyControls.FilesListView.SortDirection.Asc;
			this.flvProjects.TabIndex = 0;
			this.flvProjects.Title = "Panavue Project Files";
			this.flvProjects.ValidFileTypes = new List<string>() {
        ".xia",
        ".pia",
        ".vst"};
			this.flvProjects.OnMySelectionChanged += new MyControls.FilesListView.MySelectionChanged( this.flvProjects_OnMySelectionChanged );
			// 
			// tabAutoGenerate
			// 
			this.tabAutoGenerate.Controls.Add( this.label2 );
			this.tabAutoGenerate.Controls.Add( this.edtReplacementValue );
			this.tabAutoGenerate.Controls.Add( this.grpType );
			this.tabAutoGenerate.Controls.Add( this.filesListView1 );
			this.tabAutoGenerate.Controls.Add( this.btnReTarget );
			this.tabAutoGenerate.Location = new System.Drawing.Point( 4, 22 );
			this.tabAutoGenerate.Name = "tabAutoGenerate";
			this.tabAutoGenerate.Padding = new System.Windows.Forms.Padding( 3 );
			this.tabAutoGenerate.Size = new System.Drawing.Size( 900, 674 );
			this.tabAutoGenerate.TabIndex = 1;
			this.tabAutoGenerate.Text = "Auto Generate";
			this.tabAutoGenerate.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point( 21, 252 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 100, 13 );
			this.label2.TabIndex = 8;
			this.label2.Text = "Replacement Value";
			// 
			// edtReplacementValue
			// 
			this.edtReplacementValue.Location = new System.Drawing.Point( 127, 248 );
			this.edtReplacementValue.Name = "edtReplacementValue";
			this.edtReplacementValue.Size = new System.Drawing.Size( 314, 20 );
			this.edtReplacementValue.TabIndex = 7;
			// 
			// grpType
			// 
			this.grpType.Controls.Add( this.chkSwitchToNEFs );
			this.grpType.Controls.Add( this.rbTypeSourceDirectory );
			this.grpType.Controls.Add( this.rbTypeSourceDrive );
			this.grpType.Location = new System.Drawing.Point( 480, 20 );
			this.grpType.Name = "grpType";
			this.grpType.Size = new System.Drawing.Size( 188, 151 );
			this.grpType.TabIndex = 6;
			this.grpType.TabStop = false;
			this.grpType.Text = "Type";
			// 
			// rbTypeSourceDirectory
			// 
			this.rbTypeSourceDirectory.AutoSize = true;
			this.rbTypeSourceDirectory.Location = new System.Drawing.Point( 36, 60 );
			this.rbTypeSourceDirectory.Name = "rbTypeSourceDirectory";
			this.rbTypeSourceDirectory.Size = new System.Drawing.Size( 104, 17 );
			this.rbTypeSourceDirectory.TabIndex = 1;
			this.rbTypeSourceDirectory.TabStop = true;
			this.rbTypeSourceDirectory.Text = "Source Directory";
			this.rbTypeSourceDirectory.UseVisualStyleBackColor = true;
			// 
			// rbTypeSourceDrive
			// 
			this.rbTypeSourceDrive.AutoSize = true;
			this.rbTypeSourceDrive.Location = new System.Drawing.Point( 36, 25 );
			this.rbTypeSourceDrive.Name = "rbTypeSourceDrive";
			this.rbTypeSourceDrive.Size = new System.Drawing.Size( 87, 17 );
			this.rbTypeSourceDrive.TabIndex = 0;
			this.rbTypeSourceDrive.TabStop = true;
			this.rbTypeSourceDrive.Text = "Source Drive";
			this.rbTypeSourceDrive.UseVisualStyleBackColor = true;
			// 
			// filesListView1
			// 
			this.filesListView1.FilesFilter = "Project Files (*.XIA;*.PIA;*.VST)|*.XIA;*.PIA;*.VST";
			this.filesListView1.InitialDirectory = "";
			this.filesListView1.Location = new System.Drawing.Point( 21, 20 );
			this.filesListView1.MultiSelection = true;
			this.filesListView1.Name = "filesListView1";
			this.filesListView1.Size = new System.Drawing.Size( 420, 200 );
			this.filesListView1.Sorting = MyControls.FilesListView.SortDirection.Asc;
			this.filesListView1.TabIndex = 5;
			this.filesListView1.Title = "Panavue Project Files";
			this.filesListView1.ValidFileTypes = new List<string>() {
        ".xia",
        ".pia",
        ".vst"};
			// 
			// btnReTarget
			// 
			this.btnReTarget.Location = new System.Drawing.Point( 535, 206 );
			this.btnReTarget.Name = "btnReTarget";
			this.btnReTarget.Size = new System.Drawing.Size( 75, 23 );
			this.btnReTarget.TabIndex = 4;
			this.btnReTarget.Text = "ReTarget";
			this.btnReTarget.UseVisualStyleBackColor = true;
			this.btnReTarget.Click += new System.EventHandler( this.btnReTarget_Click );
			// 
			// theProgressBar
			// 
			this.theProgressBar.ForeColor = System.Drawing.Color.MediumSpringGreen;
			this.theProgressBar.Location = new System.Drawing.Point( 18, 717 );
			this.theProgressBar.Name = "theProgressBar";
			this.theProgressBar.Size = new System.Drawing.Size( 270, 14 );
			this.theProgressBar.Step = 5;
			this.theProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.theProgressBar.TabIndex = 0;
			// 
			// stStatus
			// 
			this.stStatus.Location = new System.Drawing.Point( 666, 717 );
			this.stStatus.Name = "stStatus";
			this.stStatus.Size = new System.Drawing.Size( 234, 14 );
			this.stStatus.TabIndex = 1;
			this.stStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mProgressBar
			// 
			this.mProgressBar.Color = System.Drawing.Color.MediumSpringGreen;
			this.mProgressBar.Location = new System.Drawing.Point( 300, 710 );
			this.mProgressBar.Name = "mProgressBar";
			this.mProgressBar.Size = new System.Drawing.Size( 342, 26 );
			this.mProgressBar.Step = 5;
			this.mProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.mProgressBar.TabIndex = 2;
			// 
			// chkSwitchToNEFs
			// 
			this.chkSwitchToNEFs.AutoSize = true;
			this.chkSwitchToNEFs.Location = new System.Drawing.Point( 30, 106 );
			this.chkSwitchToNEFs.Name = "chkSwitchToNEFs";
			this.chkSwitchToNEFs.Size = new System.Drawing.Size( 132, 17 );
			this.chkSwitchToNEFs.TabIndex = 9;
			this.chkSwitchToNEFs.Text = "switch to source NEFs";
			this.chkSwitchToNEFs.UseVisualStyleBackColor = true;
			// 
			// PanoramaVille
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.ClientSize = new System.Drawing.Size( 910, 745 );
			this.Controls.Add( this.mProgressBar );
			this.Controls.Add( this.stStatus );
			this.Controls.Add( this.theProgressBar );
			this.Controls.Add( this.theTabControl );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "PanoramaVille";
			this.Text = "Panorama Maker";
			this.Load += new System.EventHandler( this.PanoramaVille_Load );
			this.theTabControl.ResumeLayout( false );
			this.tabProject.ResumeLayout( false );
			this.tabProject.PerformLayout();
			this.grpRun.ResumeLayout( false );
			this.grpCrop.ResumeLayout( false );
			this.grpCrop.PerformLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropRight ) ).EndInit();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropBottom ) ).EndInit();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropLeft ) ).EndInit();
			( (System.ComponentModel.ISupportInitialize) ( this.udCropTop ) ).EndInit();
			this.grpSave.ResumeLayout( false );
			this.grpSave.PerformLayout();
			this.pnlAbsRelSave.ResumeLayout( false );
			this.pnlAbsRelSave.PerformLayout();
			this.grpOptionsImage.ResumeLayout( false );
			this.grpOptionsImage.PerformLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.udOptionsImageBlending ) ).EndInit();
			this.grpOptionsStitching.ResumeLayout( false );
			this.grpOptionsStitching.PerformLayout();
			( (System.ComponentModel.ISupportInitialize) ( this.thePanoBox ) ).EndInit();
			this.grpLensDefinition.ResumeLayout( false );
			this.grpLensDefinition.PerformLayout();
			this.grpOrientation.ResumeLayout( false );
			this.grpOrientation.PerformLayout();
			this.tabAutoGenerate.ResumeLayout( false );
			this.tabAutoGenerate.PerformLayout();
			this.grpType.ResumeLayout( false );
			this.grpType.PerformLayout();
			this.ResumeLayout( false );

		}

		#endregion

		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.TabControl theTabControl;
		private System.Windows.Forms.TabPage tabProject;
		private MyControls.MyGroupBox grpLensDefinition;
		private MyControls.LabelAndText ltLensDefinitionLensName;
		private MyControls.MyGroupBox grpOptionsImage;
		private System.Windows.Forms.Label stOptionsImageBlending;
		private System.Windows.Forms.NumericUpDown udOptionsImageBlending;
		private System.Windows.Forms.CheckBox chkOptionsImageAlign;
		private System.Windows.Forms.CheckBox chkOptionsImageAutoCrop;
		private System.Windows.Forms.CheckBox chkOptionsImageAntiAliasing;
		private System.Windows.Forms.CheckBox chkOptionsImageSharpen;
		private System.Windows.Forms.CheckBox chkOptionsImage360Wrapping;
		private System.Windows.Forms.CheckBox chkOptionsImageAdjustColors;
		private MyControls.MyGroupBox grpOptionsStitching;
		private System.Windows.Forms.Button btnOptionsStitchingSearchArea;
		private System.Windows.Forms.RadioButton rbOptionsStitchingManual5or8Flags;
		private System.Windows.Forms.RadioButton rbOptionsStitchingManual3or6Flags;
		private System.Windows.Forms.RadioButton rbOptionsStitchingManual1Flag;
		private System.Windows.Forms.RadioButton rbOptionsStitchingAutomatic;
		private MyControls.MyGroupBox grpOrientation;
		private System.Windows.Forms.RadioButton rbOrientationVertical;
		private System.Windows.Forms.RadioButton rbOrientationHorizontal;
		private System.Windows.Forms.Button btnCreate;
		private MyControls.FilesListView flvProjectFiles;
		private MyControls.FilesListView flvProjects;
		private System.Windows.Forms.TabPage tabAutoGenerate;
		private MyControls.LabelAndText ltLensDefinitionFocalLength;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox thePanoBox;
		private System.Windows.Forms.CheckBox chkDisplayPanorama;
		private System.Windows.Forms.ProgressBar theProgressBar;
		private System.Windows.Forms.Label stStatus;
		private System.Windows.Forms.Timer theTimer;
		private MyControls.MyGroupBox grpSave;
		public  System.Windows.Forms.RadioButton rbSaveAbsolute;
		private System.Windows.Forms.RadioButton rbSaveRelative;
		private MyControls.BrowseForDirectory bfdSaveSaveDirectory;
		private MyControls.BrowseForFile bfCurrentProject;
		private System.Windows.Forms.CheckBox chkAutoGenerateProjectName;
		public MyControls.BrowseForDirectory bfdSaveConvertedFiles;
		private MyControls.MyGroupBox grpCrop;
		private System.Windows.Forms.NumericUpDown udCropRight;
		private System.Windows.Forms.NumericUpDown udCropBottom;
		private System.Windows.Forms.NumericUpDown udCropLeft;
		private System.Windows.Forms.NumericUpDown udCropTop;
		private System.Windows.Forms.RadioButton rbCropLeftAndRight;
		private System.Windows.Forms.RadioButton rbCropTopAndBottom;
		private System.Windows.Forms.RadioButton rbCropUnique;
		private System.Windows.Forms.CheckBox chkCrop;
		private MyControls.MyGroupBox grpRun;
		private MyControls.MultiProgressBar mProgressBar;
		private MyControls.LabelAndSpinner lsThreads;
        public  System.Windows.Forms.RadioButton rbSaveConvertedAbsolute;
        private System.Windows.Forms.RadioButton rbSaveConvertedRelative;
        private System.Windows.Forms.Panel pnlAbsRelSave;
		private System.Windows.Forms.CheckBox chkLensDefinitionDX;
		private MyControls.MyGroupBox grpType;
		private System.Windows.Forms.RadioButton rbTypeSourceDirectory;
		private System.Windows.Forms.RadioButton rbTypeSourceDrive;
		private MyControls.FilesListView filesListView1;
		private System.Windows.Forms.Button btnReTarget;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox edtReplacementValue;
		private System.Windows.Forms.CheckBox chkSwitchToNEFs;
	}
}
