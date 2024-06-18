using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MyClasses;
using System.Reflection;

namespace MyControls
{
	public partial class WindowForm : Form
	{
		protected XmlFileControlsSerializer serializer	=  null;
		protected bool autoSerialize;
		protected bool saveState;
		protected bool saveEnabledStatus;
		protected static string defaultsFilename	=  "MyDefaults";

		public WindowForm()
		{
			InitializeComponent();

			// Auto Serializing is true by default.
			autoSerialize		=  true;
			saveState			=  true;
			saveEnabledStatus	=  false;
		}

		private void WindowForm_Paint( object sender, PaintEventArgs e )
		{
		}

		public bool AutoSerialize
		{
			get { return ( autoSerialize ); }
			set { autoSerialize	=  value; }
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
				string assemblyCodePath =  Assembly.GetExecutingAssembly().CodeBase;
				string path =  assemblyCodePath.Substring( 8 );

// 				string assemblyPath	=  Path.GetDirectoryName( Assembly.GetExecutingAssembly().CodeBase );
				string assemblyPath	=  Path.GetDirectoryName( path );

				// Strip of the 'file:\' uri part.
				assemblyPath	=  assemblyPath.Replace( @"file:\", "" );

				// Build the complete path name.
				return ( Path.Combine( assemblyPath, defaultsFilename + "_" + Name ) );
			}
		}

		public virtual void Serialize( bool getEm )
		{
			if ( getEm )
				serializer.SerializeIn();
			else
				serializer.SerializeOut();
		}

		private void WindowForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			if ( autoSerialize )
				Serialize( false );
		}

		private void WindowForm_Load( object sender, EventArgs e )
		{
			if ( serializer == null )
				serializer	=  new XmlFileControlsSerializer( DefaultsPath );

			if ( autoSerialize )
			{
				// Automatic.
				// Add controls.
				//foreach( Control control in this.Controls )
				//{
				//    serializer.Add( control );
				//}
				serializer.Add( this );

				// Serialize in.
				Serialize( true );
			}
		}
	}
}
