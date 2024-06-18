using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Photoshop;
using PhotoshopSupport;
using MyClasses;

namespace Photoshopper
{
	public class MergeToPanorama
	{
		Application m_app;

		public MergeToPanorama( Application app )
		{
			m_app =  app;
		}

		public void Run()
		{
			if ( m_app != null )
			{
				try
				{
					int id                                 =  m_app.StringIDToTypeID( "0f9db13f-a772-4035-9020-840f0e5e2f02" );
					ActionDescriptor actionDescriptor =  new ActionDescriptor();
	
					m_app.ExecuteAction( id, actionDescriptor, PsDialogModes.psDisplayAllDialogs );
				}
				
				catch( Exception ex )
				{
					if ( ex != null )
					{
					}
				}
			}
		}
	}
}
