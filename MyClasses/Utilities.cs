//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="Utilities.cs">
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
	/// Summary description for Utilities.
	/// </summary>
	public class Utilities
	{
		// Class to get a list of subdirectories (and optionally, theirs too).
		public static ArrayList GetSubdirectories( string parentDirectory, ArrayList directoriesAr, bool recurse ) 
		{
			// Recurse into subdirectories of this directory.
			string[] subDirectoryEntries	=  Directory.GetDirectories( parentDirectory );

			foreach( string subDirectory in subDirectoryEntries )
			{
				directoriesAr.Add( subDirectory );

				// Recurse?
				if ( recurse )
				{
					GetSubdirectories( subDirectory, directoriesAr, recurse );
				}
			}

			return ( directoriesAr );
		}

		public static ArrayList GetFiles( string parentDirectory, ArrayList filesAr, bool recurse )
		{
			// Get files in this directory.
			filesAr.Add( Directory.GetFiles( parentDirectory ) );

			if ( recurse )
			{
				// Get all subdirectories.
				ArrayList	subsAr	=  new ArrayList();

				GetSubdirectories( parentDirectory, subsAr, recurse );

				// Now loop through each sundirectory and get the files in each.
				for( int i= 0;  i< subsAr.Count;  i++ )
				{
					filesAr.AddRange( Directory.GetFiles( subsAr[ i ].ToString() ) );
				}
			}

			return ( filesAr );
		}
	}
}
