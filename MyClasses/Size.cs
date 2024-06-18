//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="Size.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;

namespace MyClasses
{
	/// <summary>
	/// Size class.
	/// </summary>
	public class Size
	{
		private double	width;
		private double	height;

		public Size()
		{
			width	=  height	=  0.0;
		}

		public Size( double w, double h )
		{
			width		=  w;
			height	=  h;
		}

		public double Width
		{
			get { return ( width ); }
			set { width	=  value; }
		}

		public double Height
		{
			get { return ( height ); }
			set { height	=  value; }
		}
	}

}
