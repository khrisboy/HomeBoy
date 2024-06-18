using System;

using Photoshop;

namespace Photoshopper
{
	/// <summary>
	/// Wraps the Document class's Histogram object.
	/// </summary>
	public class Histogram
	{
		private Array		theHistogram;
		private double		theMean;
		private double		theMedian;
		private double		theStdDev;
		private int			numPixels;

		public Histogram( Document doc )
		{
			theHistogram	=  doc.Histogram as Array;

			CalcMeasures();
		}

		private void CalcMeasures()
		{
			theMean		=  0.0;
			numPixels	=  0;
		  
			for ( int rgbValue= 0;  rgbValue< theHistogram.Length;  rgbValue++ )
			{
				int	numPixelsAt	=  (int) theHistogram.GetValue( rgbValue );

				if ( numPixelsAt > 0 )
				{
					theMean		+=  numPixelsAt*rgbValue;
					numPixels	+=  numPixelsAt;
				}
			}

			// Divide by number of pixels.
			theMean	/=	 numPixels;
		  
			// Calculate the standard deviation.:  s =  Sqrt( Sum[ ( x - mean )**2 ] / n-1 )
			theStdDev	=  0.0;

			for ( int rgbValue= 0;  rgbValue< theHistogram.Length;  rgbValue++ )
			{
				int	nPixelsAt	=  (int) theHistogram.GetValue( rgbValue );

				if ( nPixelsAt > 0 )
					theStdDev	+=  nPixelsAt*Math.Pow( rgbValue-theMean, 2 );
			}

			theStdDev	/=  ( numPixels - 1 );
			theStdDev	 =  Math.Sqrt( theStdDev );

			// Calc the median.
			int	nPixels		=  0;
			int	nHalfPixels	=  numPixels/2;

			for ( int rgbValue= 0;  rgbValue< theHistogram.Length;  rgbValue++ )
			{
				nPixels	+=  (int) theHistogram.GetValue( rgbValue );

				if ( nPixels >= nHalfPixels )
				{
					theMedian	=  rgbValue;
					break;
				}
			}
		}

		public double Mean
		{
			get
			{
				return ( theMean );
			}
		}

		public double Median
		{
			get
			{
				return ( theMedian );
			}
		}

		public double StdDev
		{
			get
			{
				return ( theStdDev );
			}
		}

		public Array GetHistogram()
		{
			return ( theHistogram );
		}

		public double GetWeighted( bool blackIsBest )
		{
			double	weighted		=  0.0;
			double	rgbWeight	=  blackIsBest ?  256.0 : 1.0;
		  
			for ( int j= 0;  j< theHistogram.Length;  j++ )
			{
				int	nPixelsAt	=  (int) theHistogram.GetValue( j );

				if ( nPixelsAt > 0 )
					weighted	+=  nPixelsAt * (rgbWeight*rgbWeight);

				if ( blackIsBest )
					rgbWeight--;
				else
					rgbWeight++;
			}

			// Divide by number of pixels.
			weighted	/=  (double) numPixels;

			return ( weighted );
		}
	}
}
