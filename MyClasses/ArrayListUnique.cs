//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="ArrayListUnique.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections;

namespace MyClasses
{
	/// <summary>
	/// ArrayListUnique -  ArrayList class for unique entries only.
	/// Works only for classes that have ToString() overridden.
	/// </summary>
	public class ArrayListUnique : ArrayList
	{
		private bool			raiseExceptionOnBadAdd;
		protected static string	badAddExceptionMessage	=  "Object already in unique array";

		public ArrayListUnique()
		{
			// Default to not raise an exception.
			raiseExceptionOnBadAdd =  false;
		}

		protected virtual bool IsDuplicate( object value )
		{
			bool duplicate =  false;

			foreach ( object obj in this )
			{
				if ( obj.ToString() == value.ToString() )
				{
					duplicate	=  true;
					break;
				}
			}

			return ( duplicate );
		}

		// Accessors.
		public bool RaiseExceptionOnBadAdd
		{
			get { return ( raiseExceptionOnBadAdd ); }
			set { raiseExceptionOnBadAdd	=  value; }
		}

		// Overrides.
		public override int Add( object value )
		{
			// Can only add if not already in there.
			if ( IsDuplicate( value ) )
			{
				if ( RaiseExceptionOnBadAdd )
				{
					throw new Exception( badAddExceptionMessage );
				}
				else
				{
					return ( -1 );
				}
			}
			else
				return ( base.Add( value ) );
		}

		public override void Insert( int index, object value )
		{
			// Can only add if not already in there.
			if ( IsDuplicate( value ) )
			{
				if ( RaiseExceptionOnBadAdd )
				{
					throw new Exception( badAddExceptionMessage );
				}
			}
			else
			{
				base.Insert( index, value );
			}
		}

		public override void AddRange(ICollection c)
		{
			base.AddRange (c);
		}

		public override void InsertRange( int index, ICollection c )
		{
			// Can only add if not already in there.
			bool	foundDuplicate	=  false;

			foreach ( object value in c )
			{
				if ( IsDuplicate( value ) )
				{
					if ( RaiseExceptionOnBadAdd )
					{
						throw new Exception( badAddExceptionMessage );
					}
					else
					{
						foundDuplicate	=  true;
						break;
					}
				}
			}

			if ( !foundDuplicate )
			{
				base.InsertRange( index, c );
			}
		}

		// Take care of this later!
		public override void SetRange( int index, ICollection c )
		{
			//***base.SetRange (index, c);
		}

		public override object this[ int index ]
		{
			get
			{
				return base[ index ];
			}
			set
			{
				// Can only add if not already in there.
				if ( IsDuplicate( value ) )
				{
					if ( RaiseExceptionOnBadAdd )
					{
						throw new Exception( badAddExceptionMessage );
					}
				}
				else
				{
					base[ index ]	=  value;
				}
			}
		}
	}
}
