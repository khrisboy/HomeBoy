//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="ArrayListSerial.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections;

namespace MyClasses
{
	/// <summary>
	/// 
	/// </summary>
	public class ArrayListSerial : System.Collections.ArrayList
	{
		private string	separator;

		public ArrayListSerial()
		{
			separator	=  "|";
		}

		public string Separator
		{
			get { return ( separator ); }

			set
			{
				if ( value.Length == 1 )
				{
					separator	=  value;
				}
			}
		}

		public override string ToString()
		{
			string	toString	=  "";

			// Write out as one string.
			for ( int i= 0;  i< Count;  i++ )
			{
				toString	+=  this[ i ].ToString();
				toString	+=  Separator;
			}

			return ( toString );
		}

		public virtual void FromString( string fromString )
		{
			if ( fromString != null )
			{
				// Look for the separator character.
				string	from =  fromString;

				while( from.Length > 0 )
				{
					int	index =  from.IndexOf( separator );

					if ( index > 0 )
					{
						string	item	=  from.Substring( 0, index );

						Add( item );

						if ( index < from.Length )
						{
							from =  from.Substring( index+1 );
						}
					}
					else if ( from.Length > 0 )
					{
						Add( from );

						from =  string.Empty;
					}
				}
			}
		}

	}
}
