using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MyControls
{
	public partial class MultiProgressBar : UserControl
	{
		// Data members.
		List<ProgressBar>	progressBars;

		int					step	=  5;
		Color				color	=  Color.MediumSpringGreen;
		ProgressBarStyle	style	=  ProgressBarStyle.Continuous;

		const int	verticalSpacing	=   5;
		const int	defaultHeight	=  14;

		public MultiProgressBar()
		{
			InitializeComponent();
		}

		public void SetValue( int value, int whichOne )
		{
			if ( whichOne < progressBars.Count )
			{
				progressBars[ whichOne ].Value	=  value;
			}
		}

		private void Add( ProgressBar progressBar )
		{
			if ( progressBars == null )
				progressBars	=  new List<ProgressBar>();

			// Add.
			progressBars.Add( progressBar );
			Controls.Add( progressBar );

			// Resize everyone.
			DoResize();			
		}

		private void DoResize()
		{
			for ( int i= 0;  i< progressBars.Count;  i++ )
			{
				progressBars[ i ].Location	=  CalcLocation( i );
				progressBars[ i ].Size		=  CalcSize();
			}
		}

		public int NumProgressBars
		{
			set
			{
				if ( value > 0 )
				{
					if ( progressBars == null )
						progressBars	=  new List<ProgressBar>( value );

					for ( int i=  progressBars.Count;  i< value;  i++ )
					{
						CreateProgressBar();  
					}
				}
				else
				{
				}
			}
		}

		public void Reset()
		{
			// Get rid of all bars.
			for ( int i= 0;  i< progressBars.Count; i++ )
			{
				progressBars[ i ]	=  null;
			}

			progressBars	=  null;
		}

		private void CreateProgressBar()
		{
			ProgressBar	theProgressBar	=  new ProgressBar();

			theProgressBar.ForeColor	=  Color;
			theProgressBar.Step			=  Step;
			theProgressBar.Style		=  Style;
			theProgressBar.Name			=  "theProgressBar" + ( progressBars.Count+1 );

			Add( theProgressBar );
		}

		private int DefaultHeight
		{
			get { return ( Math.Min( defaultHeight, Height/progressBars.Count - verticalSpacing )); }
		}

		private Size CalcSize()
		{
			Size	size	=  new Size( Width - 8, DefaultHeight );

			return ( size );
		}

		private Point CalcLocation( int whichOne )
		{
			double ith	=  whichOne+1.0;
			double y	=  Height/2 - ( ( 0.5*progressBars.Count )*DefaultHeight +
				                        ( progressBars.Count-1 )*verticalSpacing );

			y	+=  ( DefaultHeight+verticalSpacing )*whichOne;

			Point	point	=  new Point( 4, (int) y );

			return ( point );
		}

		public int Step
		{
			get { return ( step ); }

			set
			{
				step	=  value;
			}
		}

		public Color Color
		{
			get { return ( color ); }

			set
			{
				color	=  value;
			}
		}

		public ProgressBarStyle Style
		{
			get { return ( style ); }

			set
			{
				style	=  value;
			}
		}
	}
}
