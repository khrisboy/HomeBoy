//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="ControlsSerializer.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyClasses
{
	/// <summary>
	/// Class for serialization of Windows Form Controls.
	/// </summary>
	public abstract class ControlsSerializer
	{
		#region Fields
		
		protected ArrayList m_controlsAr;
		private bool m_okToShowMessages = true;

		#endregion Fields

		#region Constructors
		
		protected ControlsSerializer()
		{
		    m_controlsAr =  new ArrayList();
		}

		#endregion Constructors

		#region Properties
		
		public bool ShowMessages
		{
			get { return ( m_okToShowMessages ); }
			set { m_okToShowMessages =  value; }
		}

		#endregion Properties

		#region Methods

		// Abstract methods.
		public abstract void SerializeIn();
		public abstract void SerializeIn( Control control, string parenting );
		public abstract void SerializeOut();
		public abstract void SerializeOut( Control control, string parenting );

		// Non-abstract virtual methods.
		public virtual void Add(Control control)
		{
			m_controlsAr.Add( control );
		}

		public virtual void Remove( Control control )
		{
			m_controlsAr.Remove( control );
		}

		public virtual void RemoveAt( int index )
		{
			m_controlsAr.RemoveAt( index );
		}

		public virtual Control this[ int index ]
		{
			get
			{
				Control control =  null;

				try
				{
					control = (Control) m_controlsAr[ index ];
				}

				catch ( Exception ex )
				{
					if ( m_okToShowMessages )
					{
						MessageBox.Show( ex.Message );
					}
				}

				return ( control );
			}

			set
			{
				m_controlsAr[ index ] =  value;
			}
		}

		#endregion Methods
	}
}
