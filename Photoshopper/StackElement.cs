using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Photoshop;

namespace Photoshopper
{
	public class StackElement
	{
		public static bool PMDebug;

		Application app;
		List<TPoint> fCorners;
		string fName;
		string fString;
		double fScale;
		string fLayerID;

		StackElement fConnectedTo;

		public static void  dumpTrap( string name, List<TPoint> corners )
		{
			if (! PMDebug) 
				return;

			Console.WriteLine( name + "= [" );

			for ( int i= 0;  i< corners.Count; i++ )
			{
				Console.WriteLine( ((i > 0) ? "; " : "" ) + corners[i].fX + " " + corners[i].fY );
			}

			Console.WriteLine( "];");
		}

		public void dumpMLCorners()
		{
			// Weed out file suffix (chokes matlab)
// 			dumpTrap( this.fName.Match( @"([^.]+)" )[1], this.fCorners );
		}

		// Set the fCorners of the layer to the bounds of the Photoshop layer.
		public void setCornersToLayerBounds( Photoshop.Document stackDoc )
		{
			if ( stackDoc == null )
			{
				stackDoc = app.ActiveDocument;
			}
		
			ArtLayer layer =  (ArtLayer) (stackDoc.Layers[this.fName]);
			double[] bounds =  (double[]) layer.Bounds;

			this.fCorners = new List<TPoint>();

			// Assumes 'px'.
			this.fCorners[0] = new TPoint( bounds[0], bounds[1] );
			this.fCorners[2] = new TPoint( bounds[2], bounds[3] );
	
			this.fCorners[1] = new TPoint( this.fCorners[2].fX, this.fCorners[0].fY );
			this.fCorners[3] = new TPoint( this.fCorners[0].fX, this.fCorners[2].fY );
		}
	
		// Add the corner data to the string of per-stackElement information
		// that gets passed to the filter plugin
		void addPieceData()
		{
			if ( this.fCorners != null )
			{
				// Add corners in place of trailing '\n'
				this.fString = this.fString.Substring( 0, fString.Length-1 ) + "fCorners=";

				for (int j = 0; j < 4; j++)
				{
					this.fString += " " + this.fCorners[j].fX.ToString() + " " + this.fCorners[j].fY.ToString();
				}
				
				this.fString += "\t";
		
				if ( this.fScale != null )
				{
					this.fString += ("fScale=" + this.fScale.ToString() + "\t");
				}
			
				if ( this.fConnectedTo != null )
				{
					this.fString += "fConnectedTo=" + this.fConnectedTo.fLayerID + "\t";
				}
			
				if ( this.fLayerID != null )
				{
					this.fString += "fLayerID=" + this.fLayerID.ToString() + "\t";
				}
			
				this.fString += "\n";
			}
			else
			{
				throw new Exception( "!" );	// Corner data missing!
			}
		}

		Object overlapArea( StackElement other )
		{
			if (other == this)
			{
				return TPoint.polygonArea( this.fCorners );
			}

// 			var overlapBounds = TRect.intersection( this.fBoundsCache, other.fBoundsCache );
// 
// 			if (overlapBounds.isEmpty())
// 				return 0.0;

			List<TPoint> clipPoly = TPoint.intersectConvexPolygons( this.fCorners, other.fCorners );

			if ( clipPoly == null )
			{
				return 0.0;
			}
			else
			{
				return TPoint.polygonArea( clipPoly );
			}
		}
	}

	public class TPoint
	{
		public double fX;
		public double fY;

		public TPoint()
		{
		}

		public TPoint( double fX, double fY )
		{
			this.fX =  fX;
			this.fY =  fY;
		}

		public static Object polygonArea( List<TPoint> fCorners )
		{
			return ( null );
		}

		public static List<TPoint> intersectConvexPolygons( List<TPoint> fCornersA, List<TPoint> fCornersB )
		{
			return ( null );
		}
	}
}
