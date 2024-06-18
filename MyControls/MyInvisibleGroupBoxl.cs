//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="MyInvisibleGroupBoxl.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MyControls
{
	/// <summary>
	/// My GroubBox class
	/// </summary>summary>
	public class MyInvisibleGroupBox : MyControls.MyGroupBox
	{
		private bool	paintMe;

		public MyInvisibleGroupBox() : base()
		{
			paintMe	=  false;
		}

		public bool PaintMe
		{
			get
			{
				return ( paintMe );
			}

			set
			{
				paintMe	=  value;
			}
		}


		// Just need to override the OnPaint
		protected override void OnPaint( PaintEventArgs e )
		{
			if ( PaintMe )
				base.OnPaint(e);
		}

	}
}
