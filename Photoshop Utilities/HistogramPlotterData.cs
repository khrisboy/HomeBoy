using System;
using System.Collections.Generic;

namespace PhotoshopUtilities
{
	internal struct HistogramPlotterData
	{
		public string	theInfo;
		public string	theMean;
		public string	theStdDev;
		public string	theMedian;
		public string	theWeightsDelta;
		public bool		theInfoChanged;
		public bool		theMeanChanged;
		public bool		theStdDevChanged;
		public bool		theMedianChanged;
		public bool		theDeltaChanged;

		public HistogramPlotterData( string info, string mean, string stdDev, string median, string weightsDelta )
		{
			theInfo				=  info;
			theMean				=  mean;
			theStdDev			=  stdDev;
			theMedian			=  median;
			theWeightsDelta		=  weightsDelta;

			theInfoChanged		=  true;
			theMeanChanged		=  true;
			theStdDevChanged	=  true;
			theMedianChanged	=  true;
			theDeltaChanged		=  true;
		}

		public HistogramPlotterData( string mean, string stdDev, string median, string weightsDelta )
		{
			theInfo				=  "";
			theMean				=  mean;
			theStdDev			=  stdDev;
			theMedian			=  median;
			theWeightsDelta		=  weightsDelta;

			theInfoChanged		=  false;
			theMeanChanged		=  true;
			theStdDevChanged	=  true;
			theMedianChanged	=  true;
			theDeltaChanged		=  true;
		}
	}
}
