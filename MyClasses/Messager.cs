//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="Messager.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace MyClasses
{
	public class Messager
	{
		private static Object	displayObject;
		private static bool		beep;
		private static Timer	timer;

		static Messager()
		{
			displayObject	=  null;
			timer			=  null;
			beep			=  true;
		}

		public static void Show( string message )
		{
			Show( message, "" );
		}

		public static void Show( string message, string caption )
		{
			try
			{
				if ( displayObject != null )
				{
					string display = caption + ": " + message;

					// See if it has a Text property. (I know it's lame, but...)
					Type			t	=	displayObject.GetType();
					PropertyInfo	m	=  t.GetProperty( "Text", BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy );

					// And set.
					if ( m != null )
					{
						m.SetValue( displayObject, display, null );
					}

					if ( timer != null )
					{
						timer.Start();
					}
				}
				else
				{
					MessageBox.Show( message, caption );
				}

				if ( beep )
				{
					LibWrap.MsgBeep( 0 );
				}
			}

			catch( Exception )
			{
			}
		}

		public static Object DisplayObject
		{
			set { displayObject =  value; }
		}

		public static Timer Timer
		{
			set { timer =  value; }
		}

		public static bool Beep
		{
			set { beep =  value; }
		}
	}
}
