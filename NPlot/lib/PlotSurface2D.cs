/*
NPlot - A charting library for .NET

PlotSurface2D.cs
Copyright (C) 2003
Matt Howlett, Paolo Pierini

Redistribution and use of NPlot or parts there-of in source and
binary forms, with or without modification, are permitted provided
that the following conditions are met:

1. Re-distributions in source form must retain at the head of each
   source file the above copyright notice, this list of conditions
   and the following disclaimer.

2. Any product ("the product") that makes use NPlot or parts 
   there-of must either:
  
    (a) allow any user of the product to obtain a complete machine-
        readable copy of the corresponding source code for the 
        product and the version of NPlot used for a charge no more
        than your cost of physically performing source distribution,
	on a medium customarily used for software interchange, or:

    (b) reproduce the following text in the documentation, about 
        box or other materials intended to be read by human users
        of the product that is provided to every human user of the
        product: 
   
              "This product includes software developed as 
              part of the NPlot library project available 
              from: http://www.nplot.com/" 

        The words "This product" may optionally be replace with 
        the actual name of the product.

------------------------------------------------------------------------

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

// #define DEBUG_BOUNDING_BOXES

using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;

namespace NPlot
{

	/// <summary>
	/// Implements the surface on which IDrawables are drawn. Is extended
	/// by Bitmap.PlotSurface2D, Windows.PlotSurface2D etc. TODO: better explanation.
	/// </summary>
	public class PlotSurface2D : IPlotSurface2D
	{

		/// <summary>
		/// Possible positions of the X axis.
		/// </summary>
		public enum XAxisPosition
		{
			/// <summary>
			/// X axis is on the top.
			/// </summary>
			Top = 1,
			//Center = 2,
			/// <summary>
			/// X axis is on the bottom.
			/// </summary>
			Bottom = 3,
		}


		/// <summary>
		/// Possible positions of the Y axis.
		/// </summary>
		public enum YAxisPosition
		{
			/// <summary>
			/// Y axis on the left.
			/// </summary>
			Left = 1,
			// Center
			/// <summary>
			/// Y axis on the right.
			/// </summary>
			Right = 3,
		}


		private System.Drawing.StringFormat titleDrawFormat_;

		private Font titleFont_;
		private string title_;
		private Brush titleBrush_;
		private int padding_;
		private Axis xAxis1_;
		private Axis yAxis1_;
		private Axis xAxis2_;
		private Axis yAxis2_;
		private PhysicalAxis pXAxis1Cache_;
		private PhysicalAxis pYAxis1Cache_;
		private PhysicalAxis pXAxis2Cache_;
		private PhysicalAxis pYAxis2Cache_;
		private bool autoScaleAutoGeneratedAxes_ = false;
		private bool autoScaleTitle_ = false;

		private object plotAreaBoundingBoxCache_;
		private object bbXAxis1Cache_;
		private object bbXAxis2Cache_;
		private object bbYAxis1Cache_;
		private object bbYAxis2Cache_;
		private object bbTitleCache_;

		private object plotBackColor_ = null;
		private System.Drawing.Bitmap plotBackImage_ = null;
		private IRectangleBrush plotBackBrush_ = null;

		private System.Collections.ArrayList drawables_;
		private System.Collections.ArrayList xAxisPositions_;
		private System.Collections.ArrayList yAxisPositions_;
        private System.Collections.ArrayList zPositions_;
        private System.Collections.SortedList ordering_;

        private System.Drawing.Drawing2D.SmoothingMode smoothingMode_;

		private ArrayList axesConstraints_ = null;

		private Legend legend_;


		/// <summary>
		/// The physical bounding box of the last drawn plot surface area is available here.
		/// </summary>
		public Rectangle PlotAreaBoundingBoxCache
		{
			get
			{
				if (plotAreaBoundingBoxCache_ == null) 
				{
					return Rectangle.Empty;
				}
				else
				{
					return (Rectangle)plotAreaBoundingBoxCache_;
				}
			}
		}

		/// <summary>
		/// Performs a hit test with the given point and returns information 
		/// about the object being hit.
		/// </summary>
		/// <param name="p">The point to test.</param>
		/// <returns></returns>
		public System.Collections.ArrayList HitTest(Point p)
		{

			System.Collections.ArrayList a = new System.Collections.ArrayList();

			// this is the case if PlotSurface has been cleared.
			if (bbXAxis1Cache_ == null)
			{
				return a;
			}
			else if (bbXAxis1Cache_ != null && ((Rectangle) bbXAxis1Cache_ ).Contains(p))
			{
				a.Add( this.xAxis1_ );
				return a;
			}
			else if (bbYAxis1Cache_ != null && ((Rectangle) bbYAxis1Cache_ ).Contains(p))
			{
				a.Add( this.yAxis1_ );
				return a;
			}
			else if (bbXAxis2Cache_ != null && ((Rectangle) bbXAxis2Cache_ ).Contains(p))
			{
				a.Add( this.xAxis2_ );
				return a;
			}
			else if (bbXAxis2Cache_ != null && ((Rectangle) bbYAxis2Cache_ ).Contains(p))
			{
				a.Add( this.yAxis2_ );
				return a;
			}
			else if (bbTitleCache_ != null && ((Rectangle) bbTitleCache_ ).Contains(p))
			{
				a.Add( this );
				return a;
			}
			else if (plotAreaBoundingBoxCache_ != null && ((Rectangle) plotAreaBoundingBoxCache_ ).Contains(p))
			{
				a.Add( this );
				return a;
			}

			return a;
		}


		/// <summary>
		/// The bottom abscissa axis.
		/// </summary>
		public Axis XAxis1
		{
			get
			{
				return xAxis1_;
			}
			set
			{
				xAxis1_ = value;
			}
		}


		/// <summary>
		/// The left ordinate axis.
		/// </summary>
		public Axis YAxis1
		{
			get
			{
				return yAxis1_;
			}
			set
			{
				yAxis1_ = value;
			}
		}


		/// <summary>
		/// The top abscissa axis.
		/// </summary>
		public Axis XAxis2
		{
			get
			{
				return xAxis2_;
			}
			set
			{
				xAxis2_ = value;
			}
		}


		/// <summary>
		/// The right ordinate axis.
		/// </summary>
		public Axis YAxis2
		{
			get
			{
				return yAxis2_;
			}
			set
			{
				yAxis2_ = value;
			}
		}


		/// <summary>
		/// The physical XAxis1 that was last drawn.
		/// </summary>
		public PhysicalAxis PhysicalXAxis1Cache
		{
			get
			{
				return pXAxis1Cache_;
			}
		}


		/// <summary>
		/// The physical YAxis1 that was last drawn.
		/// </summary>
		public PhysicalAxis PhysicalYAxis1Cache
		{
			get
			{
				return pYAxis1Cache_;
			}
		}


		/// <summary>
		/// The physical XAxis2 that was last drawn.
		/// </summary>
		public PhysicalAxis PhysicalXAxis2Cache
		{
			get
			{
				return pXAxis2Cache_;
			}
		}


		/// <summary>
		/// The physical YAxis2 that was last drawn.
		/// </summary>
		public PhysicalAxis PhysicalYAxis2Cache
		{
			get
			{
				return pYAxis2Cache_;
			}
		}



		/// <summary>
		/// The chart title.
		/// </summary>
		public string Title
		{
			get
			{
				return title_;
			}
			set
			{
				title_ = value;
			}
		}


		/// <summary>
		/// The plot title font.
		/// </summary>
		public Font TitleFont
		{
			get
			{
				return titleFont_;
			}
			set
			{
				titleFont_ = value;
			}
		}


		/// <summary>
		/// The distance in pixels to leave between of the edge of the bounding rectangle
		/// supplied to the Draw method, and the markings that make up the plot.
		/// </summary>
		public int Padding
		{
			get
			{
				return padding_;
			}
			set
			{
				padding_ = value;
			}
		}


		/// <summary>
		/// Sets the title to be drawn using a solid brush of this color.
		/// </summary>
		public Color TitleColor
		{
			set
			{
				titleBrush_ = new SolidBrush( value );
			}
		}


		/// <summary>
		/// The brush used for drawing the title.
		/// </summary>
		public Brush TitleBrush
		{
			get
			{
				return titleBrush_;
			}
			set
			{
				titleBrush_ = value;
			}
		}


		/// <summary>
		/// A color used to paint the plot background. Mutually exclusive with PlotBackImage and PlotBackBrush
		/// </summary>
		public System.Drawing.Color PlotBackColor
		{
			set
			{
				plotBackColor_ = value;
				plotBackBrush_ = null;
				plotBackImage_ = null;
			}
		}

		
		/// <summary>
		/// An imaged used to paint the plot background. Mutually exclusive with PlotBackColor and PlotBackBrush
		/// </summary>
		public System.Drawing.Bitmap PlotBackImage
		{
			set
			{
				plotBackImage_ = value;
				plotBackColor_ = null;
				plotBackBrush_ = null;
			}
		}


		/// <summary>
		/// A Rectangle brush used to paint the plot background. Mutually exclusive with PlotBackColor and PlotBackBrush
		/// </summary>
		public IRectangleBrush PlotBackBrush
		{
			set
			{
				plotBackBrush_ = value;
				plotBackColor_ = null;
				plotBackImage_ = null;
			}
		}


		/// <summary>
		/// Smoothing mode to use when drawing plots.
		/// </summary>
		public System.Drawing.Drawing2D.SmoothingMode SmoothingMode 
		{ 
			get
			{
				return smoothingMode_;
			}
			set
			{
				this.smoothingMode_ = value;
			}
		}


		private void Init()
		{
			drawables_ = new ArrayList();
			xAxisPositions_ = new ArrayList();
			yAxisPositions_ = new ArrayList();
            zPositions_ = new ArrayList();
            ordering_ = new SortedList();
            FontFamily fontFamily = new FontFamily("Arial");
			TitleFont = new Font(fontFamily, 14, FontStyle.Regular, GraphicsUnit.Pixel);
			padding_ = 10;
			title_ = "";
			autoScaleTitle_ = false;
			autoScaleAutoGeneratedAxes_ = false;
			xAxis1_ = null;
			xAxis2_ = null;
			yAxis1_ = null;
			yAxis2_ = null;
			pXAxis1Cache_ = null;
			pYAxis1Cache_ = null;
			pXAxis2Cache_ = null;
			pYAxis2Cache_ = null;
			titleBrush_ = new SolidBrush( Color.Black );
			plotBackColor_ = Color.White;
			
			this.legend_ = null;

			smoothingMode_ = System.Drawing.Drawing2D.SmoothingMode.None;

			axesConstraints_ = new ArrayList();
		}


		/// <summary>
		/// Default constructor.
		/// </summary>
		public PlotSurface2D()
		{
			// only create this once.
			titleDrawFormat_ = new StringFormat();
			titleDrawFormat_.Alignment = StringAlignment.Center;

			Init();
		}


		private float DetermineScaleFactor( int w, int h )
		{

			float diag = (float)Math.Sqrt( w*w +  h*h );
			float scaleFactor = (diag / 1400.0f)*2.4f;
			
			if ( scaleFactor > 1.0f )
			{
				return scaleFactor;
			}
			else
			{
				return 1.0f;
			}
		}


        /// <summary>
        /// Adds a drawable object to the plot surface with z-order 0. If the object is an IPlot,
        /// the PlotSurface2D axes will also be updated. 
        /// </summary>
        /// <param name="p">The IDrawable object to add to the plot surface.</param>
        public void Add(IDrawable p)
        {
            Add(p, 0);
        }


        /// <summary>
		/// Adds a drawable object to the plot surface. If the object is an IPlot, 
		/// the PlotSurface2D axes will also be updated.
		/// </summary>
		/// <param name="p">The IDrawable object to add to the plot surface.</param>
		/// <param name="zOrder">The z-ordering when drawing (objects with lower numbers are drawn first)</param>
		public void Add( IDrawable p, int zOrder )
		{
			Add( p, XAxisPosition.Bottom, YAxisPosition.Left, zOrder );
		}


        /// <summary>
        /// Adds a drawable object to the plot surface against the specified axes with
        /// z-order of 0. If the object is an IPlot, the PlotSurface2D axes will also
        /// be updated.
        /// </summary>
        /// <param name="p">the IDrawable object to add to the plot surface</param>
        /// <param name="xp">the x-axis to add the plot against.</param>
        /// <param name="yp">the y-axis to add the plot against.</param>
        public void Add(IDrawable p, XAxisPosition xp, YAxisPosition yp)
        {
            Add(p, xp, yp, 0);
        }
        
        
        /// <summary>
        /// Adds a drawable object to the plot surface against the specified axes. If
		/// the object is an IPlot, the PlotSurface2D axes will also be updated.
		/// </summary>
		/// <param name="p">the IDrawable object to add to the plot surface</param>
		/// <param name="xp">the x-axis to add the plot against.</param>
		/// <param name="yp">the y-axis to add the plot against.</param>
		/// <param name="zOrder">The z-ordering when drawing (objects with lower numbers are drawn first)</param>
		public void Add( IDrawable p, XAxisPosition xp, YAxisPosition yp, int zOrder )
		{
			drawables_.Add( p );
			xAxisPositions_.Add( xp );
			yAxisPositions_.Add( yp );
            zPositions_.Add((double)zOrder);
            // fraction is to make key unique. With 10 million plots at same z, this buggers up.. 
            double fraction = (double)(++uniqueCounter_)/10000000.0f; 
            ordering_.Add( (double)zOrder + fraction, drawables_.Count - 1 );
            
            // if p is just an IDrawable, then it can't affect the axes.
			if ( p is IPlot )
			{
				UpdateAxes( false );
			}

		}

        private int uniqueCounter_ = 0;


        private void UpdateAxes( bool recalculateAll )
		{
            if (drawables_.Count != xAxisPositions_.Count || drawables_.Count != yAxisPositions_.Count)
            {
                throw new NPlotException("plots and axis position arrays our of sync");
            }

            int position = 0;

            // if we're not recalculating axes using all iplots then set
            // position to last one in list.
            if (!recalculateAll)
            {
                position = drawables_.Count - 1;
                if (position < 0) position = 0;
            }

            if (recalculateAll)
            {
                this.xAxis1_ = null;
                this.yAxis1_ = null;
                this.xAxis2_ = null;
                this.yAxis2_ = null;
            }

            for (int i = position; i < drawables_.Count; ++i)
            {

                // only update axes if this drawable is an IPlot.
                if (!(drawables_[position] is IPlot))
                    continue;

                IPlot p = (IPlot)drawables_[position];
                XAxisPosition xap = (XAxisPosition)xAxisPositions_[position];
                YAxisPosition yap = (YAxisPosition)yAxisPositions_[position];

                if (xap == XAxisPosition.Bottom)
                {
                    if (this.xAxis1_ == null)
                    {
                        this.xAxis1_ = p.SuggestXAxis();
                        if (this.xAxis1_ != null)
                        {
                            this.xAxis1_.TicksAngle = -(float)Math.PI / 2.0f;
                        }
                    }
                    else
                    {
                        this.xAxis1_.LUB(p.SuggestXAxis());
                    }

                    if (this.xAxis1_ != null)
                    {
                        this.xAxis1_.MinPhysicalLargeTickStep = 50;

                        if (this.AutoScaleAutoGeneratedAxes)
                        {
                            this.xAxis1_.AutoScaleText = true;
                            this.xAxis1_.AutoScaleTicks = true;
                            this.xAxis1_.TicksIndependentOfPhysicalExtent = true;
                        }
                        else
                        {
                            this.xAxis1_.AutoScaleText = false;
                            this.xAxis1_.AutoScaleTicks = false;
                            this.xAxis1_.TicksIndependentOfPhysicalExtent = false;
                        }
                    }
                }

                if (xap == XAxisPosition.Top)
                {
                    if (this.xAxis2_ == null)
                    {
                        this.xAxis2_ = p.SuggestXAxis();
                        if (this.xAxis2_ != null)
                        {
                            this.xAxis2_.TicksAngle = (float)Math.PI / 2.0f;
                        }
                    }
                    else
                    {
                        this.xAxis2_.LUB(p.SuggestXAxis());
                    }

                    if (this.xAxis2_ != null)
                    {
                        this.xAxis2_.MinPhysicalLargeTickStep = 50;

                        if (this.AutoScaleAutoGeneratedAxes)
                        {
                            this.xAxis2_.AutoScaleText = true;
                            this.xAxis2_.AutoScaleTicks = true;
                            this.xAxis2_.TicksIndependentOfPhysicalExtent = true;
                        }
                        else
                        {
                            this.xAxis2_.AutoScaleText = false;
                            this.xAxis2_.AutoScaleTicks = false;
                            this.xAxis2_.TicksIndependentOfPhysicalExtent = false;
                        }
                    }
                }

                if (yap == YAxisPosition.Left)
                {
                    if (this.yAxis1_ == null)
                    {
                        this.yAxis1_ = p.SuggestYAxis();
                        if (this.yAxis1_ != null)
                        {
                            this.yAxis1_.TicksAngle = (float)Math.PI / 2.0f;
                        }
                    }
                    else
                    {
                        this.yAxis1_.LUB(p.SuggestYAxis());
                    }

                    if (this.yAxis1_ != null)
                    {
                        if (this.AutoScaleAutoGeneratedAxes)
                        {
                            this.yAxis1_.AutoScaleText = true;
                            this.yAxis1_.AutoScaleTicks = true;
                            this.yAxis1_.TicksIndependentOfPhysicalExtent = true;
                        }
                        else
                        {
                            this.yAxis1_.AutoScaleText = false;
                            this.yAxis1_.AutoScaleTicks = false;
                            this.yAxis1_.TicksIndependentOfPhysicalExtent = false;
                        }
                    }
                }

                if (yap == YAxisPosition.Right)
                {
                    if (this.yAxis2_ == null)
                    {
                        this.yAxis2_ = p.SuggestYAxis();
                        if (this.yAxis2_ != null)
                        {
                            this.yAxis2_.TicksAngle = -(float)Math.PI / 2.0f;
                        }
                    }
                    else
                    {
                        this.yAxis2_.LUB(p.SuggestYAxis());
                    }

                    if (this.yAxis2_ != null)
                    {
                        if (this.AutoScaleAutoGeneratedAxes)
                        {
                            this.yAxis2_.AutoScaleText = true;
                            this.yAxis2_.AutoScaleTicks = true;
                            this.yAxis2_.TicksIndependentOfPhysicalExtent = true;
                        }
                        else
                        {
                            this.yAxis2_.AutoScaleText = false;
                            this.yAxis2_.AutoScaleTicks = false;
                            this.yAxis2_.TicksIndependentOfPhysicalExtent = false;
                        }
                    }

                }
            }

        }


		private void DetermineAxesToDraw( out Axis xAxis1, out Axis xAxis2, out Axis yAxis1, out Axis yAxis2 )
		{
			xAxis1 = this.xAxis1_;
			xAxis2 = this.xAxis2_;
			yAxis1 = this.yAxis1_;
			yAxis2 = this.yAxis2_;

			if (this.xAxis1_ == null)
			{
				if (this.xAxis2_ == null)
				{
					throw new NPlotException( "Error: No X-Axis specified" );
				}
				xAxis1 = (Axis)this.xAxis2_.Clone();
				xAxis1.HideTickText = true;
				xAxis1.TicksAngle = -(float)Math.PI / 2.0f;
			}

			if (this.xAxis2_ == null)
			{
				// don't need to check if xAxis1_ == null, as case already handled above.
				xAxis2 = (Axis)this.xAxis1_.Clone();
				xAxis2.HideTickText = true;
				xAxis2.TicksAngle = (float)Math.PI / 2.0f;
			}

			if (this.yAxis1_ == null)
			{
				if (this.yAxis2_ == null)
				{
					throw new NPlotException( "Error: No Y-Axis specified" );
				}
				yAxis1 = (Axis)this.yAxis2_.Clone();
				yAxis1.HideTickText = true;
				yAxis1.TicksAngle = (float)Math.PI / 2.0f;
			}

			if (this.yAxis2_ == null)
			{
				// don't need to check if yAxis1_ == null, as case already handled above.
				yAxis2 = (Axis)this.yAxis1_.Clone();
				yAxis2.HideTickText = true;
				yAxis2.TicksAngle = -(float)Math.PI / 2.0f;
			}

		}


		private void DeterminePhysicalAxesToDraw( Rectangle bounds, 
			Axis xAxis1, Axis xAxis2, Axis yAxis1, Axis yAxis2,
			out PhysicalAxis pXAxis1, out PhysicalAxis pXAxis2, 
			out PhysicalAxis pYAxis1, out PhysicalAxis pYAxis2 )
		{

			System.Drawing.Rectangle cb = bounds;

			pXAxis1 = new PhysicalAxis( xAxis1,
				new Point( cb.Left, cb.Bottom ), new Point( cb.Right, cb.Bottom ) );
			pYAxis1 = new PhysicalAxis( yAxis1,
				new Point( cb.Left, cb.Bottom ), new Point( cb.Left, cb.Top ) );
			pXAxis2 = new PhysicalAxis( xAxis2,
				new Point( cb.Left, cb.Top), new Point( cb.Right, cb.Top) );
			pYAxis2 = new PhysicalAxis( yAxis2,
				new Point( cb.Right, cb.Bottom ), new Point( cb.Right, cb.Top ) );

			int bottomIndent = padding_;
			if (!pXAxis1.Axis.Hidden) 
			{
				// evaluate its bounding box
				Rectangle bb = pXAxis1.GetBoundingBox();
				// finally determine its indentation from the bottom
				bottomIndent = bottomIndent + bb.Bottom - cb.Bottom;
			}

			int leftIndent = padding_;
			if (!pYAxis1.Axis.Hidden) 
			{
				// evaluate its bounding box
				Rectangle bb = pYAxis1.GetBoundingBox();
				// finally determine its indentation from the left
				leftIndent = leftIndent - bb.Left + cb.Left;
			}

			int topIndent = padding_;
			float scale = this.DetermineScaleFactor( bounds.Width, bounds.Height );
			int titleHeight;
			if (this.AutoScaleTitle)
			{
				titleHeight = Utils.ScaleFont(titleFont_, scale).Height;
			}
			else
			{
				titleHeight = titleFont_.Height;
			}

			//count number of new lines in title.
			int nlCount = 0;
			for (int i=0; i<title_.Length; ++i)
			{
				if (title_[i] == '\n')
					nlCount += 1;
			}
			titleHeight = (int)( ((float)nlCount*0.75 + 1.0f) * (float)titleHeight);

			if (!pXAxis2.Axis.Hidden)  
			{
				// evaluate its bounding box
				Rectangle bb = pXAxis2.GetBoundingBox();
				topIndent = topIndent - bb.Top + cb.Top;

				// finally determine its indentation from the top
				// correct top indendation to take into account plot title
				if (title_ != "" )
				{
					topIndent += (int)(titleHeight * 1.3f);
				}
			}

			int rightIndent = padding_;
			if (!pYAxis2.Axis.Hidden) 
			{
				// evaluate its bounding box
				Rectangle bb = pYAxis2.GetBoundingBox();

				// finally determine its indentation from the right
				rightIndent = (int)(rightIndent + bb.Right-cb.Right);
			}

			// now we have all the default calculated positions and we can proceed to
			// "move" the axes to their right places

			// primary axes (bottom, left)
			pXAxis1.PhysicalMin = new Point( cb.Left+leftIndent, cb.Bottom-bottomIndent );
			pXAxis1.PhysicalMax = new Point( cb.Right-rightIndent, cb.Bottom-bottomIndent );
			pYAxis1.PhysicalMin = new Point( cb.Left+leftIndent, cb.Bottom-bottomIndent );
			pYAxis1.PhysicalMax = new Point( cb.Left+leftIndent, cb.Top+topIndent );

			// secondary axes (top, right)
			pXAxis2.PhysicalMin = new Point( cb.Left+leftIndent, cb.Top+topIndent );
			pXAxis2.PhysicalMax = new Point( cb.Right-rightIndent, cb.Top+topIndent );
			pYAxis2.PhysicalMin = new Point( cb.Right-rightIndent, cb.Bottom-bottomIndent );
			pYAxis2.PhysicalMax = new Point( cb.Right-rightIndent, cb.Top+topIndent );

		}



		/// <summary>
		/// Draw the the PlotSurface2D and all contents [axes, drawables, and legend] on the 
		/// supplied graphics surface.
		/// </summary>
		/// <param name="g">The graphics surface on which to draw.</param>
		/// <param name="bounds">A bounding box on this surface that denotes the area on the
		/// surface to confine drawing to.</param>
		public void Draw( Graphics g, Rectangle bounds )
		{
			// determine font sizes and tick scale factor.
			float scale = DetermineScaleFactor( bounds.Width, bounds.Height );

			// if there is nothing to plot, return.
			if ( drawables_.Count == 0 )
			{
				// draw title
				float x_center = (bounds.Left + bounds.Right)/2.0f;
				float y_center = (bounds.Top + bounds.Bottom)/2.0f;
				Font scaled_font;
				if (this.AutoScaleTitle)
				{
					scaled_font = Utils.ScaleFont( titleFont_, scale );
				}
				else
				{
					scaled_font = titleFont_;
				}
				g.DrawString( title_, scaled_font, this.titleBrush_, new PointF(x_center,y_center), titleDrawFormat_ );

				return;
			}

			// determine the [non physical] axes to draw based on the axis properties set.
			Axis xAxis1 = null;
			Axis xAxis2 = null;
			Axis yAxis1 = null;
			Axis yAxis2 = null;
			this.DetermineAxesToDraw( out xAxis1, out xAxis2, out yAxis1, out yAxis2 );

			// apply scale factor to axes as desired.

			if (xAxis1.AutoScaleTicks) 
				xAxis1.TickScale = scale;
			if (xAxis1.AutoScaleText)
				xAxis1.FontScale = scale;
			if (yAxis1.AutoScaleTicks)
				yAxis1.TickScale = scale;
			if (yAxis1.AutoScaleText)
				yAxis1.FontScale = scale;
			if (xAxis2.AutoScaleTicks)
				xAxis2.TickScale = scale;
			if (xAxis2.AutoScaleText)
				xAxis2.FontScale = scale;
			if (yAxis2.AutoScaleTicks)
				yAxis2.TickScale = scale;
			if (yAxis2.AutoScaleText)
				yAxis2.FontScale = scale;

			// determine the default physical positioning of those axes.
			PhysicalAxis pXAxis1 = null;
			PhysicalAxis pYAxis1 = null;
			PhysicalAxis pXAxis2 = null;
			PhysicalAxis pYAxis2 = null;
			this.DeterminePhysicalAxesToDraw( 
				bounds, xAxis1, xAxis2, yAxis1, yAxis2,
				out pXAxis1, out pXAxis2, out pYAxis1, out pYAxis2 );

			float oldXAxis2Height = pXAxis2.PhysicalMin.Y;

			// Apply axes constraints
			for (int i=0; i<axesConstraints_.Count; ++i)
			{
				((AxesConstraint)axesConstraints_[i]).ApplyConstraint( 
					pXAxis1, pYAxis1, pXAxis2, pYAxis2 );
			}

			/////////////////////////////////////////////////////////////////////////
			// draw legend if have one.
			// Note: this will update axes if necessary. 

			Point legendPosition = new Point(0,0);
			if (this.legend_ != null)
			{
				legend_.UpdateAxesPositions( 
					pXAxis1, pYAxis1, pXAxis2, pYAxis2,
					this.drawables_, scale, this.padding_, bounds, 
					out legendPosition );
			}

			float newXAxis2Height = pXAxis2.PhysicalMin.Y;

			float titleExtraOffset = oldXAxis2Height - newXAxis2Height;
	
			// now we are ready to define the bounding box for the plot area (to use in clipping
			// operations.
			plotAreaBoundingBoxCache_ = new Rectangle( 
				Math.Min( pXAxis1.PhysicalMin.X, pXAxis1.PhysicalMax.X ),
				Math.Min( pYAxis1.PhysicalMax.Y, pYAxis1.PhysicalMin.Y ),
				Math.Abs( pXAxis1.PhysicalMax.X - pXAxis1.PhysicalMin.X + 1 ),
				Math.Abs( pYAxis1.PhysicalMin.Y - pYAxis1.PhysicalMax.Y + 1 )
			);
			bbXAxis1Cache_ = pXAxis1.GetBoundingBox();
			bbXAxis2Cache_ = pXAxis2.GetBoundingBox();
			bbYAxis1Cache_ = pYAxis1.GetBoundingBox();
			bbYAxis2Cache_ = pYAxis2.GetBoundingBox();

			// Fill in the background. 
			if ( this.plotBackColor_ != null )
			{
				g.FillRectangle(
					new System.Drawing.SolidBrush( (Color)this.plotBackColor_ ),
					(Rectangle)plotAreaBoundingBoxCache_ );
			}
			else if (this.plotBackBrush_ != null)
			{
				g.FillRectangle( 
					this.plotBackBrush_.Get( (Rectangle)plotAreaBoundingBoxCache_ ),
					(Rectangle)plotAreaBoundingBoxCache_ );
			}
			else if (this.plotBackImage_ != null)
			{
				g.DrawImage( 
					Utils.TiledImage( this.plotBackImage_ , new Size( 
						((Rectangle)plotAreaBoundingBoxCache_).Width,
						((Rectangle)plotAreaBoundingBoxCache_).Height ) ), 
					(Rectangle)plotAreaBoundingBoxCache_ );
			}

			// draw title
			float xt = (pXAxis2.PhysicalMax.X + pXAxis2.PhysicalMin.X)/2.0f;
			float yt = bounds.Top + this.padding_ - titleExtraOffset;
			Font scaledFont;
			if (this.AutoScaleTitle)
			{
				scaledFont = Utils.ScaleFont( titleFont_, scale );
			}
			else
			{
				scaledFont = titleFont_;
			}
			g.DrawString( title_, scaledFont, this.titleBrush_,	new PointF(xt,yt), titleDrawFormat_ );
			
			//count number of new lines in title.
			int nlCount = 0;
			for (int i=0; i<title_.Length; ++i)
			{
				if (title_[i] == '\n')
					nlCount += 1;
			}

			SizeF s = g.MeasureString(title_,scaledFont);
			bbTitleCache_ = new Rectangle( (int)(xt-s.Width/2), (int)(yt), (int)(s.Width), (int)(s.Height)*(nlCount+1) );

			// draw drawables..
			System.Drawing.Drawing2D.SmoothingMode smoothSave = g.SmoothingMode;

			g.SmoothingMode = this.smoothingMode_;

			bool legendDrawn = false;

			for ( int i_o = 0; i_o < ordering_.Count; ++i_o )
			{
	
                int i = (int)ordering_.GetByIndex(i_o);
				double zOrder = (double)ordering_.GetKey( i_o );
				if (zOrder > this.legendZOrder_)
				{
					// draw legend.
					if ( !legendDrawn && this.legend_ != null )
					{
						legend_.Draw( g, legendPosition, this.drawables_, scale );
						legendDrawn = true;
					}
				}

                IDrawable drawable = (IDrawable)drawables_[i];
				XAxisPosition xap = (XAxisPosition)xAxisPositions_[i];
				YAxisPosition yap = (YAxisPosition)yAxisPositions_[i];

				PhysicalAxis drawXAxis;
				PhysicalAxis drawYAxis;

				if ( xap == XAxisPosition.Bottom )
				{
					drawXAxis = pXAxis1;
				}
				else
				{
					drawXAxis = pXAxis2;
				}

				if ( yap == YAxisPosition.Left )
				{
					drawYAxis = pYAxis1;
				}
				else
				{
					drawYAxis = pYAxis2;
				}
	
				// set the clipping region.. (necessary for zoom)
				g.Clip = new Region((Rectangle)plotAreaBoundingBoxCache_);
				// plot.
				drawable.Draw( g, drawXAxis, drawYAxis );
				// reset it..
				g.ResetClip();
			}
			
			if ( !legendDrawn && this.legend_ != null )
			{
				legend_.Draw( g, legendPosition, this.drawables_, scale );
			}

			// cache the physical axes we used on this draw;
			this.pXAxis1Cache_ = pXAxis1;
			this.pYAxis1Cache_ = pYAxis1;
			this.pXAxis2Cache_ = pXAxis2;
			this.pYAxis2Cache_ = pYAxis2;

			g.SmoothingMode = smoothSave;

			// now draw axes.
			Rectangle axisBounds;
			pXAxis1.Draw( g, out axisBounds );
			pXAxis2.Draw( g, out axisBounds );
			pYAxis1.Draw( g, out axisBounds );
			pYAxis2.Draw( g, out axisBounds );

#if DEBUG_BOUNDING_BOXES
			g.DrawRectangle( new Pen(Color.Orange), (Rectangle) bbXAxis1Cache_ );
			g.DrawRectangle( new Pen(Color.Orange), (Rectangle) bbXAxis2Cache_ );
			g.DrawRectangle( new Pen(Color.Orange), (Rectangle) bbYAxis1Cache_ );
			g.DrawRectangle( new Pen(Color.Orange), (Rectangle) bbYAxis2Cache_ );
			g.DrawRectangle( new Pen(Color.Red,5.0F),(Rectangle) plotAreaBoundingBoxCache_);
			//if(this.ShowLegend)g.DrawRectangle( new Pen(Color.Chocolate, 3.0F), (Rectangle) bbLegendCache_);
			g.DrawRectangle( new Pen(Color.DeepPink,2.0F), (Rectangle) bbTitleCache_);
#endif

		}


		/// <summary>
		/// Clears the plot and resets all state to the default.
		/// </summary>
		public void Clear()
		{
			Init();
		}


		/// <summary>
		/// Legend to use. If this property is null [default], then the plot
		/// surface will have no corresponding legend.
		/// </summary>
		public NPlot.Legend Legend
		{
			get
			{
				return this.legend_;
			}
			set
			{
				this.legend_ = value;
			}
		}


		/// <summary>
		/// Add an axis constraint to the plot surface. Axes constraints give you 
		/// control over where NPlot positions each axes, and the world - pixel
		/// ratio.
		/// </summary>
		/// <param name="constraint">The axis constraint to add.</param>
		public void AddAxesConstraint( AxesConstraint constraint )
		{
			this.axesConstraints_.Add( constraint );
		}


		/// <summary>
		/// Whether or not the title will be scaled according to size of the plot surface.
		/// </summary>
		public bool AutoScaleTitle
		{
			get
			{
				return autoScaleTitle_;
			}
			set
			{
				autoScaleTitle_ = value;
			}
		}


		/// <summary>
		/// When plots are added to the plot surface, the axes they are attached to
		/// are immediately modified to reflect data of the plot. If 
		/// AutoScaleAutoGeneratedAxes is true when a plot is added, the axes will
		/// be turned in to auto scaling ones if they are not already [tick marks,
		/// tick text and label size scaled to size of plot surface]. If false,
		/// axes will not be autoscaling.
		/// </summary>
		public bool AutoScaleAutoGeneratedAxes
		{
			get
			{
				return autoScaleAutoGeneratedAxes_;
			}
			set
			{
				autoScaleAutoGeneratedAxes_ = value;
			}
		}


		/// <summary>
		/// Remove a drawable object. 
		/// Note that axes are not updated.
		/// </summary>
		/// <param name="p">Drawable to remove.</param>
		/// <param name="updateAxes">if true, the axes are updated.</param>
		public void Remove( IDrawable p, bool updateAxes ) 
		{
			int index = drawables_.IndexOf( p );
			if (index < 0)
				return;
			drawables_.RemoveAt( index );
			xAxisPositions_.RemoveAt( index );
			yAxisPositions_.RemoveAt( index );
            zPositions_.RemoveAt(index);

            if (updateAxes)
            {
                this.UpdateAxes(true);
            }

            this.RefreshZOrdering();
        }


		/// <summary>
		/// If a plot is removed, then the ordering_ list needs to be 
		/// recalculated. 
		/// </summary>
		private void RefreshZOrdering() 
		{
			uniqueCounter_ = 0;
			ordering_ = new SortedList();
			for (int i = 0; i < zPositions_.Count; ++i) 
			{
				double zpos = Convert.ToDouble(zPositions_[i]);
				double fraction = (double)(++uniqueCounter_) / 10000000.0f;
				double d = zpos + fraction;
				ordering_.Add(d, i);
			}
		}



        /// <summary>
        /// Gets an array list containing all drawables currently added to the PlotSurface2D.
        /// </summary>
        public ArrayList Drawables
        {
			get
			{
				return this.drawables_;
			}
		}


		/// <summary>
		/// Returns the x-axis associated with a given plot.
		/// </summary>
		/// <param name="plot">the plot to get associated x-axis.</param>
		/// <returns>the axis associated with the plot.</returns>
		public Axis WhichXAxis( IPlot plot )
		{
			int index = drawables_.IndexOf( plot );
			XAxisPosition p = (XAxisPosition)xAxisPositions_[index];
			if ( p == XAxisPosition.Bottom )
				return this.xAxis1_;
			else
				return this.xAxis2_;
		}


		/// <summary>
		/// Returns the y-axis associated with a given plot.
		/// </summary>
		/// <param name="plot">the plot to get associated y-axis.</param>
		/// <returns>the axis associated with the plot.</returns>
		public Axis WhichYAxis( IPlot plot )
		{
			int index = drawables_.IndexOf( plot );
			YAxisPosition p = (YAxisPosition)yAxisPositions_[index];
			if ( p == YAxisPosition.Left )
				return this.yAxis1_;
			else
				return this.yAxis2_;
		}


		/// <summary>
		/// Setting this value determines the order (relative to IDrawables added to the plot surface)
		/// that the legend is drawn.
		/// </summary>
		public int LegendZOrder
		{
			get
			{
				return legendZOrder_;
			}
			set
			{
				legendZOrder_ = value;
			}
		}
		int legendZOrder_ = -1;


    } 
} 


