using System;

namespace MyClasses
{
	/// <summary>
	/// 
	/// </summary>
	public class DPoint
	{
		public double	xCoord;
		public double	yCoord;

		public DPoint()
		{
			xCoord	=	yCoord	=  0.0;
		}

		public DPoint( double x, double y )
		{
			xCoord	=	x;
			yCoord	=	y;
		}

		public double X
		{
			get { return ( xCoord ); }
			set { xCoord	=  value; }
		}

		public double Y
		{
			get { return ( yCoord ); }
			set { yCoord	=  value; }
		}

		public DPoint ConvertToPixels( double resolution )
		{
			return ( new DPoint( X*resolution, Y*resolution ) ); 
		}
	}
}
