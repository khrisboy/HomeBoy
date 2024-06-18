//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="MyWindowsForm.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using MyClasses;
using System.Reflection;

namespace MyControls
{
	/// <summary>
	/// Summary description for MyWindowsForm.
	/// Note:  Cannot be abstract if you want Designer to still work with derived Forms.
	/// </summary>
	public partial class MyWindowsForm : Form
	{
		protected XmlFileControlsSerializer	serializer =  null;
		protected bool						autoSerialize;
		protected bool						saveState;
		protected bool						saveEnabledStatus;
		protected bool                      saveSizeAndPosition =  false;
		protected static string				defaultsFilename =  "MyDefaults";

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components	=  null;

		public MyWindowsForm()
		{
			InitializeComponent();

			// Auto Serializing is true by default.
			autoSerialize	  =  true;
			saveState		  =  true;
			saveEnabledStatus =  false;
		}

		public MyWindowsForm( string defaultsPath )
		{
			InitializeComponent();

			serializer		  =  new XmlFileControlsSerializer( defaultsPath );
			autoSerialize	  =  true;
			saveState		  =  true;
			saveEnabledStatus =  false;
		}

		public MyWindowsForm( string defaultsPath, string defaultsFilename )
		{
			InitializeComponent();

			serializer		  =  new XmlFileControlsSerializer( defaultsPath, defaultsFilename );
			autoSerialize	  =  true;
			saveState		  =  true;
			saveEnabledStatus =  false;
		}

		public bool SaveSizeAndPosition
		{
			get { return (saveSizeAndPosition); }
			set { saveSizeAndPosition =  value; }
		}

		public bool AutoSerialize
		{
			get { return (autoSerialize); }
			set { autoSerialize =  value; }
		}

		public bool SaveEnabledStatus
		{
			get { return ( saveEnabledStatus ); }
			set
			{
				saveEnabledStatus	=  value;

				if ( autoSerialize && serializer != null )
				{
					serializer.SaveEnabledStatus	=  value;
				}
			}
		}

		public bool SaveState
		{
			get { return ( saveState ); }
			set { saveState	=  value; }
		}

		public string DefaultsFileName
		{
			get { return ( defaultsFilename ); }
			set { defaultsFilename	=  value; }
		}

		public DefaultsAr MyDefaults
		{
			get { return ( serializer.MyDefaults ); }
		}

		public string DefaultsPath
		{
			get
			{
				// Note we must wait until the Load event to uniquely define the defaults file name.
				string assemblyPath =  Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );

				// Build the complete path name.
				return ( Path.Combine( assemblyPath, String.Format( "{0}_{1}", defaultsFilename, Name ) ) );
			}
		}

		public virtual void Serialize( bool getEm )
		{
			if ( getEm )
			{
				serializer.SerializeIn();
			}
			else
			{
				serializer.SerializeOut();
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if ( disposing )
			{
				if ( components != null )
				{
					components.Dispose();
				}
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
			this.SuspendLayout();
			// 
			// MyWindowsForm
			// 
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Name = "MyWindowsForm";
			this.Load += new System.EventHandler(this.MyWindowsForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyWindowsForm_Paint);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MyWindowsForm_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void MyWindowsForm_Load( object sender, System.EventArgs e )
		{
			if ( serializer == null )
			{
				serializer =  new XmlFileControlsSerializer( DefaultsPath );
			}

			if ( autoSerialize )
			{
				// Automatic.
				serializer.Add( this );

				// Serialize in.
				Serialize( true );
			}
		}

		private void MyWindowsForm_Closing( object sender, System.ComponentModel.CancelEventArgs e )
		{
			if ( autoSerialize )
			{
				Serialize( false );
			}
		}

		private void MyWindowsForm_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
