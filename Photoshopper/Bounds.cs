using System;
using System.Collections;

namespace Photoshopper
{
	/// <summary>
	/// Summary description for Bounds.
	/// </summary>
	public class Bounds : ArrayList
	{
		// Constructor (only one) from an array.
		public Bounds( Array array )
		{
			foreach( Object obj in array )
				this.Add( (double) obj );
		}

		// Act like an array for arguments expecting one.
		public static implicit operator Array ( Bounds bounds )
		{
			Object[]	array	=  new Object[ bounds.Count ];
			int		i		=  0;

			foreach( Object obj in bounds )
				array.SetValue( obj, i++ );

			return ( array );
		}

		public new double this[ int key ]  
		{
			get  
			{
				IEnumerator	enumerator	=  GetEnumerator( key, 1 );

				enumerator.MoveNext();

				return ( (double) enumerator.Current );
			}

			set
			{
				base[ key ]	=  value;
			}
		}
	}
}
