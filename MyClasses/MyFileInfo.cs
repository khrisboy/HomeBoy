//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="MyFileInfo.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

namespace MyClasses
{
	/// <summary>
	/// Adds equality operations to FileInfo class (and a few other utilities).
	/// </summary>
	public class MyFileInfo : IComparable
	{
		#region Fields
		
		private FileInfo m_fileInfo;
		private string m_description;

		#endregion Fields

		#region Constructors

		public MyFileInfo( string fileName )
		{
			if ( !String.IsNullOrEmpty( fileName ) )
			{
				m_fileInfo =  new FileInfo( fileName );
			}

			m_description =  "";
		}

		#endregion Constructors

		#region Properties

		public string Parent
		{
			get
			{
				string parent =  "";

				if ( m_fileInfo != null )
				{
					parent =  FileInfo.Directory.Name;
				}

				return ( parent );
			}
		}

		public string FileNameWithParent
		{
			get
			{
				string fileNameWithParent =  "";

				if ( m_fileInfo != null )
				{
					fileNameWithParent =  FileInfo.Directory.Name + "\\" + Name;
				}

				return ( fileNameWithParent );
			}
		}
		
		public string Name
		{
			get { return ( m_fileInfo != null ?  m_fileInfo.Name : "" ); }
		}

		public string Extension
		{
			get
			{
				string extension =  Path.GetExtension( m_fileInfo != null ?  m_fileInfo.FullName : "" );

				return ( extension );
			}
		}

		public string ExtensionNoDot
		{
			get
			{
				string extension =  Path.GetExtension( m_fileInfo != null ?  m_fileInfo.FullName : "" );

				if ( extension[ 0 ] == '.' )
				{
					extension =  extension.Replace( ".", "" );
				}

				return ( extension );
			}
		}

		public string FileNameOnly
		{
			get
			{
				string fileNameOnly =  m_fileInfo.Name;
	
				int dotIndex =  Name.LastIndexOf( '.' );
	
				if ( dotIndex != -1 )
				{
					fileNameOnly =  Name.Substring( 0, dotIndex );
				}
	
				return ( fileNameOnly );
			}
		}

		public string FullName
		{
			get { return ( m_fileInfo != null ?  m_fileInfo.FullName : "" ); }
		}

		public string Description
		{
			get { return ( m_description ); }
			set { m_description	=  value; }
		}

		public FileInfo FileInfo
		{
			get { return ( m_fileInfo ); }
			set { m_fileInfo =  value; }
		}

		#endregion Properties

		#region Methods

		// Override the Object.Equals(object o) method:
		public override bool Equals( object o ) 
		{
			try 
			{
				if ( o == null )
				{
					return ( false );
				}
				else if ( (o as MyFileInfo) == null )
				{
					return ( false );
				}

				return ( FullName == (o as MyFileInfo).FullName && Description == (o as MyFileInfo).Description );
			}

			catch 
			{
				return ( false );
			}
		}

		// Cast to FileInfo
		public static implicit operator FileInfo ( MyFileInfo me )
		{
			return ( me.FileInfo );
		}
		
		/// <summary>
		/// Replace the path of the MyFileInfo.
		/// </summary>
		/// <param name="newPath">The new path (loaction).</param>
		public void ReplacePath( string newPath )
		{
			if ( !String.IsNullOrEmpty( newPath ) )
			{
				// Need to replace with a brand new FileInfo.
				FileInfo =  new FileInfo( newPath + @"\" + Name );
			}
		}

		/// <summary>
		/// Replace the extension.
		/// </summary>
		/// <param name="newExtension"></param>
		public void ReplaceExtension( string newExtension )
		{
			if ( !String.IsNullOrEmpty( newExtension ) )
			{
				int dotIndex =  FullName.LastIndexOf( '.' );
	
				if ( dotIndex != -1 )
				{
					string ext =  newExtension;
	
					if ( ext[ 0 ] != '.' )
					{
						ext =  "." + ext;
					}
	
					FileInfo =  new FileInfo( FullName.Substring( 0, dotIndex ) + ext );
				}
			}
		}

		// Override the Object.GetHashCode() method:
		public override int GetHashCode() 
		{
			return ( base.GetHashCode() );
		}

		// Override ToString().
		public override string ToString()
		{
			return ( FileInfo.ToString() );
		}

		// Define equality operators.
		public static bool operator == ( MyFileInfo info1, MyFileInfo info2 )
		{
			if ( info1 == (Object) null && info2 == (Object) null )
			{
				return ( true );
			}
			else if ( info1 == (Object) null || info2 == (Object) null )
			{
				return ( false );
			}
			else
			{
				return ( info1.Equals( info2 ) );
			}
		}

		public static bool operator != ( MyFileInfo info1, MyFileInfo info2 )
		{
			if ( info1 == (Object) null && info2 == (Object) null )
			{
				return ( false );
			}
			else if ( info1 == (Object) null || info2 == (Object) null )
			{
				return ( true );
			}
			else
			{
				return ( !(info1.Equals( info2 )) );
			}
		}

		#region IComparable Members

		public int CompareTo( object obj )
		{
			if ( obj is MyFileInfo ) 
			{
				return ( Name.CompareTo( (obj as MyFileInfo).Name ) );
			}

			// Throw exception if we get here.
			throw new ArgumentException( "Object is not a MyFileInfo!" );
		}

		#endregion

		#endregion Methods
	}

	/// <summary>
	/// Adds some useful functionality with MyFileInfos in an array.
	/// </summary>
	public class MyFileInfosArray : ArrayListUnique
	{
		/// <summary>
		/// Indexer.
		/// </summary>
		/// <param name="index">The location in the array to get/set.</param>
		/// <returns>Get returns the MyFileInfo at the specified index.</returns>
		public new MyFileInfo this[ int index ]  
		{
			get  
			{
				return ( (MyFileInfo) base[ index ] );
			}

			set
			{
				base[ index ] =  value;
			}
		}

		/// <summary>
		/// Returns an array of the complete path for each MyFileInfo.
		/// </summary>
		public ArrayList FileStrings
		{
			get
			{
				ArrayList fileStrings =  new ArrayList( this.Count );

				for ( int index= 0; index< this.Count; index++ )
				{
					fileStrings.Add( this[ index ].FullName );
				}

				return ( fileStrings );
			}
		}

		/// <summary>
		/// Returns an array of the file names for each MyFileInfo.
		/// </summary>
		public ArrayList FileNames
		{
			get
			{
				ArrayList fileNames =  new ArrayList( this.Count );

				for ( int index= 0;  index< this.Count;  index++ )
				{
					fileNames.Add( this[ index ].Name );
				}

				return ( fileNames );
			}
		}
		
		/// <summary>
		/// Returns an array of parent dir\filename.
		/// </summary>
		public ArrayList FileNamesWithParent
		{
			get
			{
				ArrayList fileNamesWithParent =  new ArrayList( this.Count );

				for ( int index= 0; index< this.Count; index++ )
				{
					fileNamesWithParent.Add( this[ index ].FileNameWithParent );
				}

				return ( fileNamesWithParent );
			}
		}

		/// <summary>
		/// Returns in 1 string all of the complete paths for the MyFileInfos in the array.
		/// Delimited by \r\n.
		/// </summary>
		public string ToText
		{
			get
			{
				StringBuilder builder =  new StringBuilder();

				foreach( MyFileInfo info in this )
				{
					builder.AppendLine( info.FullName );
				}

				return (builder.ToString());
			}
		}
	}
}
