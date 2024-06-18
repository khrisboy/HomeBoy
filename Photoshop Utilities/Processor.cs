using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

using Photoshop;
using Photoshopper;

using MyClasses;

namespace PhotoshopUtilities
{

	internal class Processor
	{
		private string					alreadyProcessed	=  "File is already processed!";
		private string					openingFile			=  "Opening file";
		private string					savingFile			=  "Saving cropped file";
		private ArrayListUnique			filesAr;
		private double					cropWidth;
		private double					cropHeight;
		private double					cropX;
		private double					cropY;
		private bool					isLandscape;
		private bool					assignProfile;
		private bool					saveAsCopy;
		private string					saveAsCopyDir;
		private Rotate					rotation;
		private string					theProfile;
		private double					theWeightsDelta;
		private NikonScanProcessor	myParent;

		private HistogramPlotter	theHistogramPlotter;

		private Photoshop.Application		psApp;
		private Photoshopper.Photoshopper	ps;

		public Processor( NikonScanProcessor parent, ArrayListUnique files, double width, double height, bool assign,
								bool saveCopy, string saveCopyDir, Rotate rotate, string profile, HistogramPlotter histogramPlotter,
								double weightsDelta )
		{
			myParent				=  parent;
			filesAr				=  files;
			cropWidth			=  width;
			cropHeight			=  height;
			assignProfile		=  assign;
			saveAsCopy			=  saveCopy;
			saveAsCopyDir		=  saveCopyDir;
			rotation				=  rotate;
			theProfile			=  profile;
			theWeightsDelta		=  weightsDelta;

			theHistogramPlotter	=  histogramPlotter;

			isLandscape	=  true;
			cropX		=  cropWidth;
			cropY		=  cropHeight;
		}

		public void ThreadProc()
		{
			try
			{
				Process();
			}

			finally
			{
				// Raise an event that notifies the user that the processing has terminated.  
				// You do not have to do this through a marshaled call, but marshaling is
				// recommended for the following reason:  Users of this control do not know that
				// it is multithreaded, so they expect its events to come back on the same thread
				// as the control.
				myParent.BeginInvoke( myParent.onProcessingComplete, new object[] { this, EventArgs.Empty } );
			}
		}

		private void Process()
		{
			PsUnits originalRulerUnits = PsUnits.psPixels;

			try
			{
				// Start up Photoshop.
				psApp	=  new Photoshop.Application();
				ps		=  new Photoshopper.Photoshopper( psApp );

				// Save preferences.
				originalRulerUnits	=  psApp.Preferences.RulerUnits;

				// Set ruler units to pixels
				psApp.Preferences.RulerUnits	=  PsUnits.psPixels;

				// Don't display dialogs
				psApp.DisplayDialogs	=  PsDialogModes.psDisplayNoDialogs;

				// Loop through the files.
				for ( int i= 0; i< filesAr.Count; i++ )
				{
					MyFileInfo	fileInfo	=  (MyFileInfo) filesAr[ i ];

					// Select the file that we're processing.
					IAsyncResult	r	=  myParent.BeginInvoke( myParent.selectFileDelegate, new object[] { fileInfo } );

					// Open the file.
					myParent.BeginInvoke( myParent.updateStatusInfoDelegate, new object[] { openingFile } );

					Document imageDoc	=  ps.OpenDoc( fileInfo.FullName );

					// Landscape or Portrait?
					if ( imageDoc.Width > imageDoc.Height )
						isLandscape	=  true;
					else
						isLandscape	=  false;

					if ( isLandscape )
					{
						cropX	=  cropWidth;
						cropY	=  cropHeight;
					}
					else
					{
						cropX	=  cropHeight;
						cropY	=  cropWidth;
					}

					bool	doCrop	=  false;

					try
					{
						// Quick check to see if we've already processed.
						if ( ( imageDoc.Height == cropHeight && imageDoc.Width == cropWidth ) ||
							  ( imageDoc.Height == cropWidth && imageDoc.Width == cropHeight ) )
						{
							throw new Exception( alreadyProcessed );
						}

						// Find the "edges" of the image.
						double[] theEdges	=  FindEdges( imageDoc );

						// Find the upper left corner of the cropping rectangle to use from the
						// midpoint of the edges of the image.
						DPoint	midPt	=  GetCroppingStartCornerFromTheEdgesMidPoint( theEdges );

						double[]	selCorners	=  new double[ 4 ] { midPt.X, midPt.Y, midPt.X+cropX, midPt.Y+cropY };
						Bounds	selBounds	=  new Bounds( selCorners );

						// Select the cropping rectangle.
						ps.SelectRectArea( selBounds, PsSelectionType.psReplaceSelection, 0.0 );

						// Set the flag to crop.
						doCrop	=  true;
					}

					catch ( Exception ex )
					{
						// Problems with processing. Probably already been processed.
						if ( ex.Message != alreadyProcessed )
						{
							MessageBox.Show( ex.Message, "Find Edges Error" );
						}

						// We're done with this one if we had an "unexpected" exception or we don't still
						// need to rotate and/or assign a profile.
						if ( ex.Message != alreadyProcessed ||
							 ( rotation == Rotate.None && !assignProfile ) )
						{
							// Close the file.
							imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );

							// Refresh everyone.
							myParent.BeginInvoke( myParent.refreshDelegate );

							continue;
						}
					}

					// Crop.
					if ( doCrop )
						ps.Crop();

					// Deselect.
					imageDoc.Selection.Deselect();

					// Rotate?
					if ( rotation == Rotate.CCW )
					{
						imageDoc.RotateCanvas( -90.0 );
					}
					else if ( rotation == Rotate.CW )
					{
						imageDoc.RotateCanvas( 90.0 );
					}

					// Assign the profile.
					if ( assignProfile )
						ps.AssignProfile( theProfile );

					// Update status.
					myParent.BeginInvoke( myParent.updateStatusInfoDelegate, new object[] { savingFile } );

					// Save or SaveAs?
					if ( !saveAsCopy )
					{
						imageDoc.Save();
					}
					else
					{
						// Get the type.
						string	ext	=  Path.GetExtension( imageDoc.FullName );

						if ( ext == ".tif" )
							ps.SaveAsTIFF( imageDoc, saveAsCopyDir, "" );
						else if ( ext == ".psd" )
							ps.SaveAsPSD( imageDoc, saveAsCopyDir, "" );
						else if ( ext == ".jpg" )
							ps.SaveAsJPEG( imageDoc, saveAsCopyDir, "", 12 );
						else
						{
						}
					}

					// Close the file.
					imageDoc.Close( PsSaveOptions.psDoNotSaveChanges );

					// Refresh everyone.
					myParent.BeginInvoke( myParent.refreshDelegate );
				}
			}

			catch ( ThreadAbortException )
			{
				// We were aborted. Bummer!
			}

			catch ( Exception ex )
			{
				// Uh oh!
				MessageBox.Show( ex.Message, "Processing" );
			}

			finally
			{
				// Restore preferences.
				if ( psApp != null )
					psApp.Preferences.RulerUnits	=  originalRulerUnits;
			}
		}

		double[] FindEdges( Document imageDoc )
		{
			double[] edges = new double[] { 0.0, 0.0, 0.0, 0.0 };

			double imgW	=  imageDoc.Width;
			double imgH	=  imageDoc.Height;

			double	start		=  0;
			double	end		=  0;
			int		nTimes	=  0;
			double	k			=  0;

			for ( int whichEdge= 0; whichEdge< 4; whichEdge++ )
			{
				string whichEdgeText	=  "";

				switch ( whichEdge )
				{
					case 0:	// From the Left edge.
						start				=  1.0;
						end				=  imgW - cropX;
						nTimes			=  (int) ( end - start );
						whichEdgeText	=  "Processing the left edge";
						break;
					case 1:	// From the Top edge.
						start				=  1.0;
						end				=  imgH - cropY;
						nTimes			=  (int) ( end - start );
						whichEdgeText	= "Processing the top edge";
						break;
					case 2:	// From the Right edge.
						start				=  imgW;
						end				=  cropX;
						nTimes			=  (int) ( start - end );
						whichEdgeText	=  "Processing the right edge";
						break;
					case 3:	// From the Bottom edge.
						start				=  imgH;
						end				=  cropY;
						nTimes			=  (int) ( start - end );
						whichEdgeText	=  "Processing the bottom edge";
						break;
				}

				if ( nTimes < 1 )
				{
					throw new Exception( alreadyProcessed );
				}

				// Show which edge we're processing.
				myParent.BeginInvoke( myParent.updateStatusInfoDelegate, new object[] { whichEdgeText } );

				// Do the rest.
				k	=  start;

				bool	exceededWeightsDelta	=  false;
				int		theEdge					=  0;
				double	weight, weightPrev		=  0.0;


				for ( int nthTime= 0; nthTime< nTimes; nthTime++ )
				{
					double[]	corners =  null;

					switch ( whichEdge )
					{
						case 0:	// From the Left edge.
							corners	=  new double[ 4 ] { k-1, 0, k, imgH };
							break;
						case 1:	// From the Top edge.
							corners	=  new double[ 4 ] { 0, k-1, imgW, k };
							break;
						case 2:	// From the Right edge.
							corners	=  new double[ 4 ] { k-1, 0, k, imgH };
							break;
						case 3:	// From the Bottom edge.
							corners	=  new double[ 4 ] { 0, k-1, imgW, k };
							break;
					}

					// Define the new bounding rectangle.
					Bounds	bounds	=  new Bounds( corners );

					// Select that portion of the image.
					ps.SelectRectArea( bounds, PsSelectionType.psReplaceSelection, 0.0 );

					// Get the histogram for the selected area.
					Histogram theHistogram	=  new Histogram( imageDoc );

					// Save the measures of the Histogram for this selection.
					weight	=  theHistogram.GetWeighted( true );

					// Plot the histogram of the selected area.
					if ( nthTime > 0 )
					{
						double	deltaWeights	=  Math.Abs( weight-weightPrev );

						PlotHistogram( theHistogram, deltaWeights );

						// Are we at the edge yet?
						if ( IsTheEdge( deltaWeights, ref exceededWeightsDelta ) )
						{
							theEdge	=  nthTime;
							break;
						}
					}
					else
					{
						PlotHistogram( theHistogram, 0 );
					}

					if ( whichEdge == 0 || whichEdge == 1 )
						k++;
					else
						k--;

					// Save the weight.
					weightPrev	=  weight;
				}

				// Set the amount in from each edge into the proper slot of edges[].
				if ( whichEdge == 0 || whichEdge == 1 )			// Left, Top
					edges[ whichEdge ]	=  theEdge;
				else if ( whichEdge == 2 )						// Right
					edges[ whichEdge ] = (int) imgW - theEdge;
				else
					edges[ whichEdge ] = (int) imgH - theEdge;	// Bottom
			}

			// Blank out the info box.
			myParent.BeginInvoke( myParent.updateStatusInfoDelegate, new object[] { "" } );

			return ( edges );
		}

		private bool IsTheEdge( double deltaWeights, ref bool exceededWeightsDelta )
		{
			if ( !exceededWeightsDelta )
			{
				if ( Math.Abs( deltaWeights ) > theWeightsDelta )
				{
					exceededWeightsDelta	=  true;
				}
			}

			return ( exceededWeightsDelta );
		}

		private DPoint GetCroppingStartCornerFromTheEdgesMidPoint( double[] theEdges )
		{
			// Find the middle of the edges.
			double midX = (int) ( theEdges[ 0 ] + ( theEdges[ 2 ] - theEdges[ 0 ] ) / 2.0 );
			double midY = (int) ( theEdges[ 1 ] + ( theEdges[ 3 ] - theEdges[ 1 ] ) / 2.0 );

			// Now calculate the starting corner for the cropping rectangle.
			double x = midX - ( cropX / 2 );
			double y = midY - ( cropY / 2 );

			return ( new DPoint( x, y ) );
		}

		private void PlotHistogram( Histogram histogram, double delta )
		{
			myParent.BeginInvoke( myParent.plotHistogramDelegate, new object[] { histogram, delta } );
		}
	}
}
