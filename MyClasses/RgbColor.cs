using System;
using System.Globalization;

namespace MyClasses
{
	/// <summary>
	/// RgbColor
	/// </summary>
	public class RgbColor
	{
		private int	red;
		private int	green;
		private int	blue;

		public static RgbColor	Black			=  new RgbColor( 0,0,0 );
		public static RgbColor	White			=  new RgbColor( 255,255,255 );
		public static RgbColor	Red			=  new RgbColor( 255,0,0 );
		public static RgbColor	Green			=  new RgbColor( 0,255,0 );
		public static RgbColor	Blue			=  new RgbColor( 0,0,255 );
		public static RgbColor	DarkGray		=  new RgbColor( 127,127,127 );
		public static RgbColor	LightGray	=  new RgbColor( 192,192,192 );

		public RgbColor()
		{
			red	=  green	=  blue	=  255;
		}

		public RgbColor( int r, int g, int b )
		{
			red	=  r;
			green	=  g;
			blue	=  b;
		}

		public RgbColor( string rgb )
		{
			this.Equals( rgb );
		}

		// RgbColor ststic functions.
		public static string FromColor( int r, int g, int b )
		{
			RgbColor	newColor	=  new RgbColor( r, g, b );

			return ( newColor.ToString() );
		}

		// RgbColor member functions.
		public override string ToString()
		{
			return ( hex() );
		}

		public string hex()
		{
			string	strRed	=  red.ToString( "x" );
			string	strGreen	=  green.ToString( "x" );
			string	strBlue	=  blue.ToString( "x" );
			
			// Fix "0" (need "00").
			if ( strRed == "0" )
				strRed	=  "00";
			
			if ( strGreen == "0" )
				strGreen	=  "00";
			
			if ( strBlue == "0" )
				strBlue	=  "00";
				
			string	hexString	=  strRed + strGreen + strBlue;
			
			return ( hexString );
		}

		public int R
		{
			get { return ( red ); }
			set { red	=  value; }
		}

		public int G
		{
			get { return ( green ); }
			set { green	=  value; }
		}

		public int B
		{
			get { return ( blue ); }
			set { blue	=  value; }
		}

		public bool IsEqual( RgbColor rgbColor )
		{
			return ( red   == rgbColor.red	&&
						green	== rgbColor.green	&&
						blue	== rgbColor.blue );
		}
			
		public RgbColor Equals( RgbColor rgbColor )
		{
			red	=  rgbColor.red;
			green	=  rgbColor.green;
			blue	=  rgbColor.blue;

			return( this );
		}

		public RgbColor Equals( string rgbColor )
		{
			// Fix '000' & '' for black.
			if ( rgbColor == "000" || rgbColor == "" )
				rgbColor	=  "000000";
		
			// Now convert from hex codes to values.
			string	r	=  rgbColor.Substring( 0, 2 );
			string	g	=  rgbColor.Substring( 2, 2 );
			string	b	=  rgbColor.Substring( 4, 2 );
	
			red	=  Int32.Parse( r, NumberStyles.AllowHexSpecifier );
			green	=  Int32.Parse( g, NumberStyles.AllowHexSpecifier );
			blue	=  Int32.Parse( b, NumberStyles.AllowHexSpecifier );
			
			return ( this );
		}
	}
}
