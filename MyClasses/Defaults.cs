//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="Defaults.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.IO;
using System.Collections;

namespace MyClasses
{
	/// <summary>
	/// Summary description for Defaults.
	/// </summary>
	public class Defaults
	{
		static private string	presetsPath			=  "";
		static private string	presetsFilename	=  "";
		static private string	presetsRootName	=  "";

		/// Static functions
		public static string PresetsPath
		{
			get
			{
				return ( presetsPath );
			}

			set
			{
				presetsPath	=  value;
			}
		}

		public static string PresetsFileName
		{
			get
			{
				return ( presetsFilename );
			}

			set
			{
				presetsFilename	=  value;
				
				// Make sure it's .xml.
				if ( presetsFilename.Substring( presetsFilename.Length-5 ) != ".xml" )
				{
					presetsFilename	+=  ".xml";
				}
			}
		}

		public static string PresetsRootName
		{
			get
			{
				return ( presetsRootName );
			}

			set
			{
				presetsRootName	=  value;
				PresetsFileName	=  value;
			}
		}

		// A very crude xml parser, this reads the "Tag" of the <Tag>Data</Tag>
		private static string ReadHeader( StreamReader inFile )
		{
			string	returnValue	=  "";
			int		c;
	
			while( ( c	=  inFile.Read() ) != -1 && c != '<' );

			while( ( c	=  inFile.Read() ) != -1 && c != '>' )
			{
				returnValue	+=  (char) c;
			}

			return ( returnValue );
		}

		// Very crude xml parser, this reads the "Data" of the <Tag>Data</Tag>
		private static MyString ReadData( StreamReader inFile )
		{
			string	returnValue	=  "";
			int		c;
	
			while ( ( c	=  inFile.Peek() ) != -1 && c != '<' )
			{
				c	=  inFile.Read();

				returnValue	+=  (char) c;
			}
	
			return ( new MyString( returnValue ) );
		}

		public static bool LoadDefaultsFromDisk( StreamReader loadFile, DefaultsAr defaults )
		{
			bool isCool	=  true;

			// Read in the root tag.
			string	projectSpace =  ReadHeader( loadFile );
	
			// Needs to match what we have here.
			if ( projectSpace == GetScriptNameForXML() )
			{
				while ( loadFile.Peek() != -1 )
				{
					string	starter =  ReadHeader( loadFile );	// Reads opening tag.
					MyString data   =  ReadData( loadFile );	// Reads the data.
					string ender    =  ReadHeader( loadFile );	// Reads the closing tag.
			
					// Open and close tags need to match before we pull off the data.
					if ( ( "/" + starter ) == ender )
					{
						defaults[ starter ]	=  data;
					}

					// Force boolean values to boolean types
					if ( data == "true"  ||  data == "false" )
					{
						defaults[ starter ]	=  (MyString) ( data == "true" );
					}
				}
			}
	
			// Close the file.
			loadFile.Close();
	
			// For now, just set the version.

			return ( isCool );
		}

		public static bool LoadDefaultsFromDisk( DefaultsAr defaults )
		{
			return ( LoadDefaultsFromDisk( GetDefaultsFileToRead(), defaults ) );
		}

		public static bool SaveDefaultsToDisk( TextWriter saveFile, DefaultsAr defaults )
		{
			bool isCool	=  true;

			// Write out the root node.
			string scriptNameForXML =  GetScriptNameForXML();
	
			saveFile.WriteLine( "<" + scriptNameForXML + ">" );
	
			// Now write out the parameters we're going to save.
			IDictionaryEnumerator iter =  defaults.GetEnumerator();

			while( iter.MoveNext() )
			{
				DictionaryEntry	entry =  (DictionaryEntry) iter.Current;

				string	k =  (string) entry.Key;
				string	v =  (MyString) entry.Value;

				saveFile.WriteLine( "\t<" + k + ">" + v + "</" + k + ">" );
			}

			// Close the root node.
			saveFile.WriteLine( "</" + scriptNameForXML + ">" );
	
			// Close the file.
			saveFile.Close();

			return ( isCool );
		}

		public static bool SaveDefaultsToDisk( DefaultsAr defaults )
		{
			return ( SaveDefaultsToDisk( GetDefaultsFileToWrite(), defaults ) );
		}

		public static TextWriter GetDefaultsFileToWrite()
		{
			TextWriter	writer;
			string		filename;

			try
			{
				// Make sure the folder exists.
				if ( !Directory.Exists( PresetsPath ) )
				{
					Directory.CreateDirectory( PresetsPath );
				}
				
				if ( !Directory.Exists( PresetsPath ) )
				{
					throw( new Exception( "Couldn't create defaults directory!" ) );
				}

				// Now make sure the file exists.
				filename =  PresetsPath+"/"+presetsFilename;

				if ( !File.Exists( filename ) )
				{
					writer =  File.CreateText( filename ); 

					if ( !File.Exists( filename ) )
					{
						throw( new Exception( "Couldn't create defaults file!" ) );
					}
				}
				else
				{
					writer =  new StreamWriter( filename );
				}
			}

			catch( Exception e )
			{
				throw( e );
			}
	
			return ( writer );
		}

		public static StreamReader GetDefaultsFileToRead()
		{
			StreamReader reader;

			try
			{
				// Make sure the folder exists.
				if ( PresetsPath != string.Empty && !Directory.Exists( PresetsPath ) )
				{
					Directory.CreateDirectory( PresetsPath );
				}
				
				if ( PresetsPath != string.Empty && !Directory.Exists( PresetsPath ) )
				{
					throw( new Exception( "Couldn't create defaults directory!" ) );
				}

				// Now make sure the file exists.
				string	filename	=  PresetsPath+"/"+presetsFilename;

				if ( !File.Exists( filename ) )
				{
					StreamWriter w =  File.CreateText( filename );
					w.Close();

					if ( !File.Exists( filename ) )
					{
						throw( new Exception( "Couldn't create defaults file!" ) );
					}
				}

				reader =  new StreamReader( filename );
			}

			catch( Exception e )
			{
				throw( e );
			}
	
			return ( reader );
		}

		// You can't save certain characters in xml, strip them here
		// this list is not complete
		private static string GetScriptNameForXML()
		{
			string	scriptNameForXML =  PresetsRootName;
			char[]	charsToStrip     =  new char[3] { ' ', '\'', '.' };
	
			for ( int a= 0;  a< charsToStrip.Length;  a++ )
			{
				string[] nameArray =  scriptNameForXML.Split( charsToStrip[ a ] );
		
				scriptNameForXML =  "";
		
				for ( int b= 0;  b< nameArray.Length;  b++ )
				{
					scriptNameForXML +=  nameArray[ b ];
				}
			}
	
			return ( scriptNameForXML );
		}
	}

	/// <summary>
	/// DefaultsAr class:  a dictionary class for application defaults.
	/// </summary>
	public class DefaultsAr : DictionaryBase
	{
		public MyString this[ string key ]  
		{
			get  
			{
				Object d =  Dictionary[ key ];

				return ( (MyString) d );
			}

			set  
			{
				Dictionary[ key ] = value;
			}
		}

		// Do we have an entry for this key?
		public bool Contains( string key )
		{
			return ( Dictionary.Contains( key ) );
		}

		// Add a string value.
		public void Add( string key, string val )
		{
			Dictionary.Add( key, val );
		}

		// Add a boolean value.
		public void Add( string key, bool bVal )
		{
			string val;

			if ( bVal )
			{
				val	=  "true";
			}
			else
			{
				val	=  "false";
			}

			Dictionary.Add( key, val );
		}
	}
}
