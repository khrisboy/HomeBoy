using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Photoshop;
using MyClasses;
using MyControls;

namespace Photoshopper
{
	public partial class TextItemCtrl : UserControl
	{
		private double	dpi					=  360.0;
		private bool	displayExceptions =  false;

		public TextItemCtrl()
		{
			InitializeComponent();
		}

		public bool Exceptions
		{
			get { return ( displayExceptions ); }
			set { displayExceptions =  value; }
		}

		public double Dpi
		{
			get { return ( dpi );  }
			set { dpi =  value; }
		}

		public double X
		{
			get { return (xyLocation.X); }
			set { xyLocation.X = value; }
		}

		public double Y
		{
			get { return ( xyLocation.Y ); }
			set { xyLocation.Y =  value; }
		}

		public string Contents
		{
			get { return ( ltText.Text ); }
			set { ltText.Text	=  value; }
		}

		public string Style
		{
			get { return ( ltStyle.Text ); }
			set { ltStyle.Text =  value; }
		}

		public PsJustification Justification
		{
			get
			{
				if ( (string) cbJustification.SelectedItem == "Left" )
					return ( PsJustification.psLeft );
				else if ( (string) cbJustification.SelectedItem == "Right" )
					return ( PsJustification.psRight );
				else if ( (string) cbJustification.SelectedItem == "Centered" )
					return ( PsJustification.psCenter );
				else if ( (string) cbJustification.SelectedItem == "Justified" )
					return ( PsJustification.psFullyJustified );
				else
				{
					return ( PsJustification.psLeft );
				}
			}
		}

		public PsAntiAlias AntiAlias
		{
			get
			{
				if ( (string) cbAntiAlias.SelectedItem == "None" )
					return ( PsAntiAlias.psNoAntialias );
				else if ( (string) cbAntiAlias.SelectedItem == "Sharp" )
					return ( PsAntiAlias.psSharp );
				else if ( (string) cbAntiAlias.SelectedItem == "Crisp" )
					return ( PsAntiAlias.psCrisp );
				else if ( (string) cbAntiAlias.SelectedItem == "Strong" )
					return ( PsAntiAlias.psStrong );
				else if ( (string) cbAntiAlias.SelectedItem == "Smooth" )
					return ( PsAntiAlias.psSmooth );
				else
				{
					return ( PsAntiAlias.psSharp );
				}
			}
		}

		public new bool Enabled
		{
			set
			{
				base.Enabled			=  value;
				grpTextData.Enabled	=  value;
			}
		}

		public TextItem Equals( TextItem item )
		{

			try
			{
				item.Font					=  ltFont.Text;
				item.Size					=  (double) ltFontSize;
				item.Justification		=  Justification;
				item.AntiAliasMethod		=  AntiAlias;

				item.Contents				=  ltText.Text;

				item.Color.RGB.HexValue =  ltRgbValue.Text;

				// Position.
				Array unitValues =  (Array) item.Position;

				unitValues.SetValue( X, 0 );
				unitValues.SetValue( Y, 1 );

				item.Position	=  unitValues;

				if ( ltStyle.Text != "" )
					( (ArtLayer) ( item.Parent ) ).ApplyStyle( ltStyle.Text );
			}

			catch( Exception ex )
			{
				if ( Exceptions )
					MessageBox.Show( ex.Message, "TextItem.Equals" );
			}

			return ( item );
		}

		private void TextItemCtrl_Load( object sender, EventArgs e )
		{
			// Default to inches.
			if ( !rbInches.Checked && !rbPixels.Checked )
				rbInches.Checked	=  true;

			// Default to left-justified
			if ( cbJustification.SelectedIndex == -1 )
				cbJustification.SelectedItem =  "Left";

			// Default to Sharp anti-aliasing.
			if ( cbAntiAlias.SelectedIndex == -1 )
				cbAntiAlias.SelectedItem =  "Sharp";
		}

		private void stDpi_Click( object sender, EventArgs e )
		{
			// Popup an edit box to update the dpi.
			LibWrap.MsgBeep( 0 );
		}

		private void rbUnits_CheckedChanged( object sender, EventArgs e )
		{
			if ( sender == rbInches && rbInches.Checked )
			{
				// Convert from pixels to inches.
				xyLocation.X	=  X / dpi;
				xyLocation.Y	=  Y / dpi;
			}
			else if ( sender == rbPixels && rbPixels.Checked )
			{
				// Convert from pixels to inches.
				xyLocation.X	=  X * dpi;
				xyLocation.Y	=  Y * dpi;
			}
		}
	}
}
