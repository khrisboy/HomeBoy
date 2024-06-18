using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Photoshop;
using Photoshopper;
using NPlot;
using NPlot.Windows;

namespace PhotoshopUtilities
{
	internal partial class HistogramPlotter : System.Windows.Forms.UserControl
	{
		public HistogramPlotter()
		{
			InitializeComponent();
		}

		public void PlotHistogram( Histogram histogram, double weightsDelta )
		{
			// Clear the plot surface.
			thePlotSurface2D.Clear();

			// Create a Histogram plot.
			HistogramPlot sp	=  new HistogramPlot();

			sp.DataSource		=  histogram.GetHistogram();
			sp.Pen				=  Pens.DarkBlue;
			sp.Filled			=  true;
			sp.RectangleBrush	=  new RectangleBrushes.HorizontalCenterFade( Color.Lavender, Color.Gold );
			sp.BaseWidth		=  0.5f;

			thePlotSurface2D.Add( sp );

			thePlotSurface2D.YAxis1.WorldMin			=  0.0f;
			thePlotSurface2D.YAxis1.HideTickText	=  true;
			thePlotSurface2D.YAxis1.DrawTheTicks	=  false;
			thePlotSurface2D.XAxis1.HideTickText	=  true;
			thePlotSurface2D.XAxis1.DrawTheTicks	=  false;

			// Update the measures.
			HistogramPlotterData newHP	=  new HistogramPlotterData( "Mean:  " + histogram.Mean.ToString( "####.##" ),
																					  "Std Dev:  " + histogram.StdDev.ToString( "####.##" ),
																					  "Median:  " + histogram.Median.ToString(),
																					  "Delta:  " + weightsDelta.ToString( "####.##" ) );
			Refresh( newHP );

			thePlotSurface2D.Refresh();
		}

		public void Refresh( HistogramPlotterData newData )
		{
			if ( newData.theInfoChanged )
				stInfo.Text	=  newData.theInfo;

			if ( newData.theMeanChanged )
				stMean.Text	=  newData.theMean;

			if ( newData.theMedianChanged )
				stMedian.Text	=  newData.theMedian;

			if ( newData.theStdDevChanged )
				stStdDev.Text	=  newData.theStdDev;

			if ( newData.theDeltaChanged )
				stDelta.Text =  newData.theWeightsDelta;
		}
	}
}
