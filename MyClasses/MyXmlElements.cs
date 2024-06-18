//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="MyXmlElements.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace MyClasses
{
	/// <summary>
	/// XmlElement-derived class so we can display them in comboboxes, etc.
	/// </summary>
	public class MyXmlElement
	{
		private XmlElement	xmlElement;

		#region Constructors

		public MyXmlElement( XmlDocument doc, string name )
		{
			xmlElement =  doc.CreateElement( name );
		}

		protected MyXmlElement()
		{
		}

		#endregion Constructors

		#region Properties

		public XmlElement XMLElement
		{
			get { return ( xmlElement ); }
			set { xmlElement	=  value; }
		}

		public string InnerText
		{
			get { return ( XmlConvert.DecodeName( XMLElement.InnerText ) ); }
			set { XMLElement.InnerText	=  XmlConvert.EncodeName( value );}
		}

		#endregion Properties

		#region Methods

		public override string ToString()
		{
			return ( InnerText );
		}

		public static implicit operator XmlElement ( MyXmlElement elem )
		{
			return ( elem.XMLElement );
		}

		#endregion Methods
	}

	public class XmlFileElement : MyXmlElement
	{
		#region Constructors

		public XmlFileElement( XmlDocument doc, string name ) : 
			base( doc, name )
		{
		}

		public XmlFileElement( XmlElement elem ) : 
			base()
		{
			try
			{
				XMLElement	=  elem;

				// Error if "Path" & "File" attributes are missing.
				FilePath	=  elem.Attributes[ "Path" ].Value + @"\" + elem.Attributes[ "File" ].Value;
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "XmlFileElement.XmlFileElement(1)" );
			}
		}

		#endregion Constructors

		#region Properties

		public string File
		{
			get { return ( XMLElement.GetAttribute( "File" ) ); }
				
			set
			{
				try
				{
					FileInfo	fileInfo	=  new FileInfo( value );

					XMLElement.RemoveAttribute( "File" );
					XMLElement.SetAttribute( "File", fileInfo.Name );
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message, "XmlFileElement.File" );
				}
			}
		}

		public string Path
		{
			get { return ( XMLElement.GetAttribute( "Path" ) ); }
				
			set
			{
				try
				{
					FileInfo	fileInfo	=  new FileInfo( value );

					XMLElement.RemoveAttribute( "Path" );
					XMLElement.SetAttribute( "Path", fileInfo.DirectoryName );
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message, "XmlFileElement.Path" );
				}
			}
		}

		public string FilePath
		{
			get
			{
				return ( Path + @"\" + File );
			}

			set
			{
				File	=  value;
				Path	=  value;
			}
		}

		public string Description
		{
			get { return ( XMLElement.GetAttribute( "Description" ) ); }
				
			set
			{
				try
				{
					XMLElement.RemoveAttribute( "Description" );
					XMLElement.SetAttribute( "Description", value );
				}

				catch( Exception ex )
				{
					MessageBox.Show( ex.Message, "XmlFileElement.Description" );
				}
			}
		}

		#endregion Properties

		#region Methods

		public override string ToString()
		{
			return ( File );
		}

		#endregion Methods
	}
}
