//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="MyString.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace MyClasses
{
	/// <summary>
	/// MyString class.
	/// </summary>
	public class MyString
	{
		public string	myString;

		// Constructors.
		// From a string.
		public MyString( string b )
		{
			myString	=  b;
		}

		// From a bool.
		public MyString( bool b )
		{
			myString	=  b ?  "true" : "false";
		}

		// From a long.
		public MyString( long b )
		{
			myString	=  b.ToString();
		}

		// From a double.
		public MyString( double b )
		{
			myString	=  b.ToString();
		}

		// Implicit/explicit conversions (both ways) for string.
		public static implicit operator string ( MyString b )
		{
			return ( b.myString );
		}

		public static explicit operator MyString ( string b )
		{
			MyString s	=  new MyString( b );

			return ( s );
		}

		// Implicit/explicit conversions (both ways) for bool.
		public static implicit operator bool ( MyString b ) 
		{
			bool	isCool;

			if ( b == "true" || b == "True" || b == "T" || b == "t" )    
			{
				isCool	=  true;
			}
			else if ( b == "false" || b == "False" || b == "F" || b == "f" )    
			{
				isCool	=  false;
			}
			else
			{
				throw( new Exception( "Illegal value for True/False value!" ) );
			}

			return ( isCool );
		}

		public static explicit operator MyString ( bool b )
		{
			MyString s	=  new MyString( b );

			return ( s );
		}

		// Implicit/explicit conversions (both ways) for int.
		public static implicit operator int ( MyString b )
		{
			int	result;

			Int32.TryParse( b.myString, out result );

			return ( result );
		}

		public static explicit operator MyString ( int b )
		{
			MyString s	=  new MyString( b );

			return ( s );
		}

		// Implicit/explicit conversions (both ways) for double.
		public static implicit operator double ( MyString b )
		{
			double	result;

			Double.TryParse( b.myString, out result );

			return ( result );
		}

		public static explicit operator MyString ( double b )
		{
			MyString s	=  new MyString( b );

			return ( s );
		}

		public override string ToString()
		{
			string	s	=  myString.ToString();

			return ( s );
		}
	}
}
