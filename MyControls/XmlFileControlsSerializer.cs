//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="XmlFileControlsSerializer.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using MyClasses;

namespace MyControls
{
	public class XmlFileControlsSerializer : ControlsSerializer
	{
		#region Fields

		DefaultsAr m_defaultsAr;
		private bool m_saveEnabledStatus;

		#endregion Fields

		#region Constructors

		public XmlFileControlsSerializer( string defaultsPath )
		{
			// Create the defaultsAr.
			m_defaultsAr =  new DefaultsAr();

			// Set the default root node name.
			Defaults.PresetsRootName =  "Presets";

			// Set the path and filename.
			Defaults.PresetsPath     =  Path.GetDirectoryName( defaultsPath );
			Defaults.PresetsRootName =  Path.GetFileName( defaultsPath );

			// Don't save enabled status.
			m_saveEnabledStatus	=  false;
		}

		public XmlFileControlsSerializer( string defaultsPath, string defaultsFilename )
		{
			// Create the defaultsAr.
			m_defaultsAr =  new DefaultsAr();

			// Set the default root node name.
			Defaults.PresetsRootName =  "Presets";

			// Set the path and filename.
			Defaults.PresetsPath	 =  defaultsPath;
			Defaults.PresetsRootName =  defaultsFilename;

			// Don't save enabled status.
			m_saveEnabledStatus =  false;
		}

		private XmlFileControlsSerializer()
		{
		    // No default construction.
		}

		#endregion Constructors

		#region Properties

		public DefaultsAr MyDefaults
		{
			get { return ( m_defaultsAr ); }
		}

		public bool SaveEnabledStatus
		{
			get { return ( m_saveEnabledStatus ); }
			set { m_saveEnabledStatus =  value; }
		}

		#endregion Properties

		#region Methods

		public override void SerializeIn()
		{
			try
			{
				// Read in the defaultsAr.
				Defaults.LoadDefaultsFromDisk( m_defaultsAr );

				// Now go through the array of controls and set their values (if it's there).
				for ( int i= 0;  i< m_controlsAr.Count;  i++ )
				{
					SerializeIn( m_controlsAr[ i ] as Control, "" );
				}
			}

			catch( Exception ex )
			{
				if ( ShowMessages )
				{
					MessageBox.Show( ex.Message, "SerializeIn" );
				}
			}
		}

		public override void SerializeIn( Control control, string parenting )
		{
			string	name;

			try
			{
				string	parentName =  parenting;
				
				if ( parentName == "" && control.Parent != null)
				{
					parentName =  control.Parent.Name;

					 if ( parentName == "" && control.Parent.Parent != null )
					 {
						 // Try the next parent (for MDI children).
						 parentName =  control.Parent.Parent.Name;
					 }
				}

				name =  parentName + "." + control.Name;

				// Loop through any contained controls.
				foreach ( Control child in control.Controls )
				{
					SerializeIn( child, name );
				}
			}

			catch( Exception )
			{
				name =  control.Name;
			}

			try
			{
				// Now process the child itself.
				if ( control is DataValueTextBox )
				{
					if ( m_defaultsAr.Contains( name+"_ValueText" ) )
					{
						(control as DataValueTextBox).ValueText =  m_defaultsAr[ name+"_ValueText" ];
					}
					
					if ( m_defaultsAr.Contains( name ) )
					{
						(control as DataValueTextBox).Text =  m_defaultsAr[ name ];
					}
				}
				else if ( control is TextBox && !(control as TextBox).Multiline )
				{
					if ( m_defaultsAr.Contains( name ) )
					{
						control.Text =  m_defaultsAr[ name ];
					}
				}
				else if ( control is DateTimePicker )
				{
				}
				else if ( control is MonthCalendar )
				{
				}
				else if ( control is CheckBox )
				{
					if ( m_defaultsAr.Contains( name ) )
					{
						(control as CheckBox).Checked =  m_defaultsAr[ name ];
					}
				}
				else if ( control is RadioButton )
				{
					if ( m_defaultsAr.Contains( name ) )
					{
						(control as RadioButton).Checked =  m_defaultsAr[ name ];
					}
				}
				else if ( control is BrowseForFile )
				{
					if ( m_defaultsAr.Contains( name+"_DisplayFileNameOnly" ) )
					{
						(control as BrowseForFile).DisplayFileNameOnly	=  m_defaultsAr[ name+"_DisplayFileNameOnly" ];
					}
					
					if ( m_defaultsAr.Contains( name+"_FileName" ) )
					{
						(control as BrowseForFile).FileName =  m_defaultsAr[ name+"_FileName" ];
					}

					if ( m_defaultsAr.Contains( name+"_FilterIndex" ) )
					{
						(control as BrowseForFile).FilterIndex	=  Int32.Parse( m_defaultsAr[ name+"_FilterIndex" ] );
					}
					
					if ( m_defaultsAr.Contains( name+"_InitialDirectory" ) )
					{
						(control as BrowseForFile).InitialDirectory =  m_defaultsAr[ name+"_InitialDirectory" ];
					}
				}
				else if ( control is FilesListView )
				{
					if ( m_defaultsAr.Contains( name + "_Sorting" ) )
					{
						(control as FilesListView).Sorting =  (FilesListView.SortDirection) Int32.Parse( m_defaultsAr[ name + "_Sorting" ] );
					}

					int	ith	=  0;

					while ( m_defaultsAr.Contains( name+"_Files_"+ith ) )
					{
						ith++;
					}

					string[] filesAr =  new string[ ith ];
					ith              =  0;

					while ( m_defaultsAr.Contains( name+"_Files_"+ith ) )
					{
						string file =  m_defaultsAr[ name+"_Files_"+ith ];
						filesAr.SetValue( file, ith++ );
					}

					if ( ith > 0 )
					{
						(control as FilesListView).AddFiles( filesAr );
					}
				}
				else if ( control is ComboBox )
				{
					if ( m_defaultsAr.Contains( name + "_SelectedIndex" ) )
					{
						if ( (control as ComboBox).Items.Count > 0 )
						{
							(control as ComboBox).SelectedIndex =  Int32.Parse( m_defaultsAr[ name + "_SelectedIndex" ] );
						}
					}
				}
				else if ( control is MyWindowsForm )
				{
					MyWindowsForm theForm =  control as MyWindowsForm;

					 if ( theForm.SaveSizeAndPosition )
					 {
						 // Read in the size, location, and state (maximized?).
						if ( m_defaultsAr.Contains( name + "_Width" ) )
						{
							theForm.Width =  m_defaultsAr[ name + "_Width" ];
						}
	
						if ( m_defaultsAr.Contains( name + "_Height" ) )
						{
							theForm.Height =  m_defaultsAr[ name + "_Height" ];
						}
	
						if ( m_defaultsAr.Contains( name + "_X" ) )
						{
							theForm.Left =  m_defaultsAr[ name + "_X" ];
						}
	
						if ( m_defaultsAr.Contains( name + "_Y" ) )
						{
							theForm.Top =  m_defaultsAr[ name + "_Y" ];
						}
	
						if ( m_defaultsAr.Contains( name + "_State" ) )
						{
							string state =  m_defaultsAr[ name + "_State" ];
						
							if ( state == "Maximized" )
							{
								theForm.WindowState	=  FormWindowState.Maximized;
							}
							else
							{
								theForm.WindowState	=  FormWindowState.Normal;
							}
						}
					 }
				}

				// Do the Enabled status for everybody (except static text).
				if ( m_saveEnabledStatus && !( control is Label || control is Form ) )
				{
					if ( control.Parent != null && control.Parent is UserControl )
					{
						//***MessageBox.Show( control.Name, "My Parent is a UserControl!" );
					}
					else if ( m_defaultsAr.Contains( name + "_Enabled" ) )
					{
						control.Enabled	=  m_defaultsAr[ name+"_Enabled" ];
					}
				}

				// If the control has a SerializeOut method call it.
				// (Use reflection to find out.)
				Type t	        =  control.GetType();
				MethodInfo m	=  t.GetMethod( "SerializeIn", BindingFlags.Instance | BindingFlags.NonPublic );

				// And invoke.
				if ( m != null )
				{
					m.Invoke( control, new object[0] );
				}
			}

			catch ( Exception ex )
			{
				if ( ShowMessages )
				{
					MessageBox.Show( "For " + control.Name + ": " + ex.Message, "Serialize In" );
				}
			}
		}

		public override void SerializeOut()
		{
			// Clear out the defaultsAr array.
			m_defaultsAr.Clear();

			try
			{
				// Now go through the array of controls and get their current values.
				for ( int i= 0; i< m_controlsAr.Count; i++ )
				{
					try
					{
						SerializeOut( (Control) m_controlsAr[ i ], "" );
					}

					catch ( Exception ex )
					{
						if ( ShowMessages )
						{
							MessageBox.Show( ex.Message );
						}
					}
				}

				// Write 'em out.
				Defaults.SaveDefaultsToDisk( m_defaultsAr );
			}

			catch( Exception ex )
			{
				if ( ShowMessages )
				{
					MessageBox.Show( ex.Message, "SerializeOut" );
				}
			}
		}

		public override void SerializeOut( Control control, string parenting )
		{
			string name	=  "";

			try
			{
				string parentName =  parenting;
				
				if ( parentName == "" && control.Parent != null )
				{
					parentName =  control.Parent.Name;

					 if ( parentName == "" && control.Parent.Parent != null)
					 {
						 // Try the next parent (for MDI children).
						 parentName	=  control.Parent.Parent.Name;
					 }
				}

				name =  parentName + "." + control.Name;

				// Loop through any contained controls.
				foreach ( Control child in control.Controls )
				{
					SerializeOut( child, name );
				}
			}

			catch ( Exception )
			{
				name =  control.Name;
			}

			try
			{
				// Now process the child itself.
				if ( control is DataValueTextBox )
				{
					m_defaultsAr[ name ]              =  (MyString) ( (DataValueTextBox) control ).Text;
					m_defaultsAr[ name+"_ValueText" ] =  (MyString) ( (DataValueTextBox) control ).ValueText;
				}
				else if ( control is TextBox && !(control as TextBox).Multiline )
				{
					m_defaultsAr[ name ] =  (MyString) control.Text;
				}
				else if ( control is DateTimePicker )
				{
				}
				else if ( control is MonthCalendar )
				{
				}
				else if ( control is CheckBox )
				{
					m_defaultsAr[ name ] =  (MyString) (control as CheckBox).Checked;
				}
				else if ( control is RadioButton )
				{
					m_defaultsAr[ name ] =  (MyString) (control as RadioButton).Checked;
				}
				else if ( control is BrowseForFile )
				{
					m_defaultsAr[ name+"_FileName" ]            =  (MyString) (control as BrowseForFile).FileName;
					m_defaultsAr[ name+"_FilterIndex" ]         =  (MyString) (control as BrowseForFile).FilterIndex;
					m_defaultsAr[ name+"_InitialDirectory" ]    =  (MyString) (control as BrowseForFile).InitialDirectory;
					m_defaultsAr[ name+"_DisplayFileNameOnly" ] =  (MyString) (control as BrowseForFile).DisplayFileNameOnly;
				}
				else if ( control is FilesGatherer && !(control as FilesGatherer).IsFromListView )
				{
				}
				else if ( control is FilesListView )
				{
					MyFileInfosArray filesAr =  (control as FilesListView).Files;

					if ( filesAr != null )
					{
						for ( int i = 0; i < filesAr.Count; i++ )
						{
							m_defaultsAr[ name + "_Files_" + i ] = (MyString) filesAr[ i ].FullName;
						}
					}

					m_defaultsAr[ name + "_Sorting" ] =  (MyString) ( (int) (control as FilesListView).Sorting );
				}
				else if ( control is ComboBox )
				{
					m_defaultsAr[ name+"_SelectedIndex" ] =  (MyString) (control as ComboBox).SelectedIndex;
				}
				else if ( control is MyWindowsForm )
				{
					MyWindowsForm theForm =  control as MyWindowsForm;

					 if ( theForm.SaveSizeAndPosition )
					 {
						 // Never save the minimized state.
						 if ( theForm.WindowState != FormWindowState.Minimized )
						 {
							  // Save the size, location, and state (maximized or normal).
							  m_defaultsAr[ name + "_Width" ]  =  (MyString) theForm.Size.Width;
							  m_defaultsAr[ name + "_Height" ] =  (MyString) theForm.Size.Height;
							  m_defaultsAr[ name + "_X" ]      =  (MyString) theForm.Location.X;
							  m_defaultsAr[ name + "_Y" ]      =  (MyString) theForm.Location.Y;
	
							  m_defaultsAr[ name + "_State" ] =  (MyString) ( theForm.WindowState == FormWindowState.Maximized ?  "Maximized" : "Normal" );
						 }
					 }
				}

				// Do the Enabled status for everyone (except static text).
				if ( m_saveEnabledStatus && !( control is Label || control is Form ) )
				{
					m_defaultsAr[ name+"_Enabled" ]	=  (MyString) control.Enabled;
				}

				// If the control has a SerializeOut method call it.
				// (Use reflection to find out.)
				Type t	     =  control.GetType();
				MethodInfo m =  t.GetMethod( "SerializeOut", BindingFlags.Instance | BindingFlags.NonPublic );

				// And invoke.
				if ( m != null )
				{
					m.Invoke( control, new object[ 0 ] );
				}
			}

			catch( Exception ex )
			{
				if ( ShowMessages )
				{
					MessageBox.Show( "For " + control.Name + ": " + ex.Message, "Serialize Out" );
				}
			}
		}

		#endregion Methods
	}
}
