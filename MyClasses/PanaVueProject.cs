using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace MyClasses
{
	public class PanaVueProject
	{
		public enum ProjectType { None= 0,
			                       LensWizard2in1Col,  LensWizard2in1Row, LensWizard3in1Col, LensWizard3in1Row,
			                       Mosaic, PanoramaCol, PanoramaRow, BatchCropping };

		public const string	tagProject				=  "panimaProject";
		public const string	tagProjectType			=  "projectType";

		public const string tagFullPathOfFinalImage	=  "fullPathOfFinalImage";
		public const string tagOriginalWidth		=  "originalWidth";
		public const string tagOriginalHeight		=  "originalHeight";

		public const string	tagImage				=  "image";
		public const string	tagImageNumber			=  "imageNumber";
		public const string	tagImageFilename		=  "filename";

		public const string	tagFlag					=  "flag";
		public const string	tagFlagID				=  "flagID";
		public const string	tagFlagLocation			=  "location";

		public const string	tagAntiAliasing			=  "antiAliasing";
		public const string	tagAdjustColors			=  "adjustColors";
		public const string  tagWrap360				=  "wrapping360";
		public const string  tagSharpen				=  "sharpenResultingImage";	// Only if Anti-Aliased?
		public const string	tagAutoCrop				=  "autoCrop";
		public const string	tagImageBlending		=  "imageBlending";
		public const string	tagAlignHorizontally	=  "alignRowsHorizontally";
		public const string	tagNumberOfFlagsToUse	=  "numberOfFlagsToUse";
		public const string	tagFinalImagePath		=  "fullPathOfFinalImage";

		public const string	tagAutoStitchingOptions				=  "autoStitchingOptions";				// Only for Automatic.
		public const string  tagResolution						=  "resolution";	// 1-5
		public const string  tagMinimumHorizontalOverlap		=  "minimumHorizontalOverlap";		// %
		public const string  tagMaximumHorizontalOverlap		=  "maximumHorizontalOverlap";
		public const string  tagMinimumVerticalOverlap			=  "minimumVerticalOverlap";
		public const string  tagMaximumVerticalOverlap			=  "maximumVerticalOverlap";
		public const string  tagHorizontalPerpendicularOverlap	=  "horizontalPerpendicularOverlap";
		public const string  tagVerticalPerpendicularOverlap	=  "verticalPerpendicularOverlap";
		public const string  tagRotationalSearch					=  "rotationalSearch";					// Degrees

		public const string	tagLens						=  "panimaLens";
		public const string	tagLensName					=  "name";
		public const string	tagLensMFOV					=  "mfov";
		public const string	tagLensDescription			=  "description";
		public const string	tagType						=  "type";
		public const string	tagTilt						=  "tilt";
		public const string	tagRoll						=  "roll";
		public const string	tagK1						=  "K1";
		public const string	tagK2						=  "K2";
		public const string	tagK3						=  "K3";
		public const string	tagHorizontalMisalignment	=  "horizontalMisalignment";
		public const string	tagVerticalMisalignment		=  "verticalMisalignment";

		public const string tagUserCustomTags	=  "userCustomTags";

		public const string tagCropMargins		=  "CropMargins";
		public const string tagCropLeft			=  "CropLeft";
		public const string tagCropRight		=  "CropRight";
		public const string tagCropTop			=  "CropTop";
		public const string tagCropBottom		=  "CropBottom";


		private ProjectType					type;
		private Dictionary<int,MyFileInfo>	fileInfosDict;
		XmlDocument							projectDoc;
		XmlNode								projectNode;
		private int							numberOfFlags		=  0;
		private double						imageBlending		=  0.20;
		private bool						antiAliasing		=  false;
		private bool						autoCrop				=  true;
		private bool						adjustColors		=  true;
		private bool						wrap360				=  false;
		private bool						alignHorizontally	=  false;
		private bool						sharpen				=  false;
		private string						lensName				=  "";
		private double						lensFocalLength	=  50.0;
		private string						pathOfFinalImage	=  "";
		private bool						cropMargins			=  false;
		private double						cropLeftMargin		=  0.0;
		private double						cropTopMargin		=  0.0;
		private double						cropRightMargin		=  0.0;
		private double						cropBottomMargin	=  0.0;

		private string								defaultTemplate	=  
			@"<?xml version='1.0' encoding='ISO-8859-1' standalone='yes'?>
				<panimaProject>
					<projectType>7</projectType>
					<adjustColors>1</adjustColors>
					<autoCrop>1</autoCrop>
					<imageBlending>0.25</imageBlending>
					<autoStitchingOptions>
						<resolution>1</resolution>
						<minimumHorizontalOverlap>12</minimumHorizontalOverlap>
						<maximumHorizontalOverlap>70</maximumHorizontalOverlap>
						<minimumVerticalOverlap>12</minimumVerticalOverlap>
						<maximumVerticalOverlap>70</maximumVerticalOverlap>
						<horizontalPerpendicularOverlap>7</horizontalPerpendicularOverlap>
						<verticalPerpendicularOverlap>7</verticalPerpendicularOverlap>
						<rotationalSearch>0</rotationalSearch>
					</autoStitchingOptions>
					<panimaLens>
						<name>50mm</name>
						<mfov>39.6</mfov>
					</panimaLens>
				</panimaProject>";

		public PanaVueProject()
		{
			projectDoc	=  new XmlDocument();

			Initialize();
		}

		public PanaVueProject( MyFileInfo info )
		{
			projectDoc	=  new XmlDocument();

			Open( info );
		}

		public PanaVueProject( string filename )
		{
			projectDoc	=  new XmlDocument();

			Open( filename );
		}

		public void Initialize()
		{
			projectDoc.Load( new StringReader( defaultTemplate ) );

			projectNode =  projectDoc.DocumentElement;
		}

		#region Helpers
		private bool GetSingleBoolValue( string tag, bool defaultValue )
		{
			bool result	=  defaultValue;

			try
			{
				result	=  Int32.Parse( GetSingleNodeStringValue( tag ) ) == 1;
			}

			catch( Exception )
			{
			}

			return ( result );
		}

		private int GetSingleIntValue( string tag, int defaultValue )
		{
			int result	=  defaultValue;

			try
			{
				result	=  Int32.Parse( GetSingleNodeStringValue( tag ) );
			}

			catch( Exception )
			{
			}

			return ( result );
		}

		private double GetSingleDoubleValue( string tag, double defaultValue )
		{
			double result	=  defaultValue;

			try
			{
				result	=  Double.Parse( GetSingleNodeStringValue( tag ) );
			}

			catch( Exception )
			{
			}

			return ( result );
		}

		private string GetSingleNodeStringValue( string tag )
		{
			string	nodeValue	=  "";

			XmlNodeList nodes	=  projectDoc.GetElementsByTagName( tag );
			XmlNode		node	=  nodes.Item( 0 );

			if ( node != null )
				nodeValue	=  node.InnerText;

			return ( nodeValue );
		}

		private void SetSingleNodeStringValue( XmlNode parent, string tag, string nodeValue )
		{
			try
			{
				XmlNode	node	=  parent[ tag ];

				if ( node == null )
				{
					node	=  projectDoc.CreateElement( tag );

					node	=  parent.AppendChild( node );
				}
				
				node.InnerText	=  nodeValue;
			}

			catch ( Exception )
			{
			}
		}

		private double GetCropMargin( string tag )
		{
			return ( GetSingleDoubleValue( tag, 0.0 ) );
		}

		private double SetCropMargin( string tag, double value )
		{
			SetSingleNodeStringValue( projectNode, tag, value.ToString() );

			return ( value );
		}
		#endregion

		#region Properties
		public bool CropMargins
		{
			get
			{
				cropMargins =  GetSingleBoolValue( tagCropMargins, false );

				return ( cropMargins );
			}

			set
			{
				cropMargins	=  value;

				SetSingleNodeStringValue( projectNode, tagCropMargins, "1" );
			}
		}

		public double CropLeftMargin
		{
			get { return ( cropLeftMargin =  GetCropMargin( tagCropLeft ) ); }
			set { cropLeftMargin =  SetCropMargin( tagCropLeft, value ); }
		}

		public double CropTopMargin
		{
			get { return ( cropTopMargin =  GetCropMargin( tagCropTop ) ); }
			set { cropTopMargin =  SetCropMargin( tagCropTop, value ); }
		}

		public double CropRightMargin
		{
			get { return ( cropRightMargin =  GetCropMargin( tagCropRight ) ); }
			set { cropRightMargin =  SetCropMargin( tagCropRight, value ); }
		}

		public double CropBottomMargin
		{
			get { return ( cropBottomMargin =  GetCropMargin( tagCropBottom ) ); }
			set { cropBottomMargin =  SetCropMargin( tagCropBottom, value ); }
		}

		public string PathOfFinalImage
		{
			get
			{
				try
				{
					pathOfFinalImage =  GetSingleNodeStringValue( tagFullPathOfFinalImage );
				}

				catch ( Exception )
				{
					Messager.Show( "No path specified for resulting image" );
					
					pathOfFinalImage =  "";
				}

				return ( pathOfFinalImage );
			}

			set
			{
				pathOfFinalImage =  value;

				SetSingleNodeStringValue( projectNode, tagFullPathOfFinalImage, pathOfFinalImage );
			}
		}

		public int NumberOfFlags
		{
			get
			{
				numberOfFlags =  GetSingleIntValue( tagNumberOfFlagsToUse, 0 );

				return ( numberOfFlags );
			}

			set
			{
				numberOfFlags	=  value;

				if ( numberOfFlags != 0 )
				{
					SetSingleNodeStringValue( projectNode, tagNumberOfFlagsToUse, numberOfFlags.ToString() );
				}
				else
				{
					XmlNode	node	=  projectNode[ tagNumberOfFlagsToUse ];

					if ( node != null )
						projectNode.RemoveChild( node );
				}
			}
		}

		public double ImageBlending
		{
			get
			{
				imageBlending =  GetSingleDoubleValue( tagImageBlending, 0.35 );

				return ( imageBlending );
			}

			set
			{
				imageBlending	=  value;

				SetSingleNodeStringValue( projectNode, tagImageBlending, imageBlending.ToString( "0.00" ) );
			}
		}

		public bool AntiAliasing
		{
			get
			{
				antiAliasing =  GetSingleBoolValue( tagAntiAliasing, false );

				return ( antiAliasing );
			}

			set
			{
				antiAliasing	=  value;

				if ( antiAliasing )
				{
					SetSingleNodeStringValue( projectNode, tagAntiAliasing, "1" );
				}
				else
				{
					XmlNode	node	=  projectNode[ tagAntiAliasing ];

					if ( node != null )
						projectNode.RemoveChild( node );
				}
			}
		}

		public bool AutoCrop
		{
			get
			{
				autoCrop =  GetSingleBoolValue( tagAutoCrop, false );

				return ( autoCrop );
			}

			set
			{
				autoCrop =  value;

				if ( autoCrop )
				{
					SetSingleNodeStringValue( projectNode, tagAutoCrop, "1" );
				}
				else
				{
					XmlNode	node	=  projectNode[ tagAutoCrop ];

					if ( node != null )
						projectNode.RemoveChild( node );
				}
			}
		}

		public bool AlignHorizontally
		{
			get
			{
				alignHorizontally	=  GetSingleBoolValue( tagAlignHorizontally, false );

				return ( alignHorizontally );
			}

			set
			{
				alignHorizontally	=  value;

				if ( alignHorizontally )
				{
					SetSingleNodeStringValue( projectNode, tagAlignHorizontally, "1" );
				}
				else
				{
					XmlNode	node	=  projectNode[ tagAlignHorizontally ];

					if ( node != null )
						projectNode.RemoveChild( node );
				}
			}
		}

		public bool AdjustColors
		{
			get
			{
				adjustColors =  GetSingleBoolValue( tagAdjustColors, false );

				return ( adjustColors );
			}

			set
			{
				adjustColors	=  value;

				if ( adjustColors )
				{
					SetSingleNodeStringValue( projectNode, tagAdjustColors, "1" );
				}
				else
				{
					XmlNode	node	=  projectNode[ tagAdjustColors ];

					if ( node != null )
						projectNode.RemoveChild( node );
				}
			}
		}

		public bool Wrap360
		{
			get
			{
				wrap360 =  GetSingleBoolValue( tagWrap360, false );

				return ( wrap360 );
			}

			set
			{
				wrap360	=  value;

				if ( wrap360 )
				{
					SetSingleNodeStringValue( projectNode, tagWrap360, "1" );
				}
				else
				{
					XmlNode	node	=  projectNode[ tagWrap360 ];

					if ( node != null )
						projectNode.RemoveChild( node );
				}
			}
		}

		public bool Sharpen
		{
			get
			{
				sharpen =  GetSingleBoolValue( tagSharpen, false );

				return ( sharpen );
			}

			set
			{
				sharpen	=  value;

				if ( sharpen )
				{
					SetSingleNodeStringValue( projectNode, tagSharpen, "1" );
				}
				else
				{
					XmlNode node = projectNode[ tagSharpen ];

					if ( node != null )
						projectNode.RemoveChild( node );
				}
			}
		}

		public ProjectType Type
		{
			get
			{
				type =  (ProjectType) GetSingleIntValue( tagProjectType, 7 );

				return ( type );
			}

			set
			{
				type	=  value;

				if ( type == 0 )
					type	=  PanaVueProject.ProjectType.PanoramaRow;		// Horizontal panorama.

				SetSingleNodeStringValue( projectNode, tagProjectType, ( (int) type ).ToString() );
			}
		}

		public string LensName
		{
			get
			{
				try
				{
					lensName =  GetSingleNodeStringValue( tagLensName );
				}

				catch ( Exception )
				{
					lensName =  "";
				}

				return ( lensName );
			}

			set
			{
				lensName =  value;

				XmlNode lensNode =  projectNode[ tagLens ];

				SetSingleNodeStringValue( lensNode, tagLensName, lensName );
			}
		}

		public double LensFocalLength
		{
			get
			{
				double mfov =  GetSingleDoubleValue( tagLensMFOV, 40.0 );

				lensFocalLength =  ( 36.0 / 2.0 ) / Math.Tan( ( Math.PI/180.0 )*( mfov/2.0 ) );

				return ( lensFocalLength );
			}

			set
			{
				lensFocalLength =  value;

				double mfov =  ( 180.0 / Math.PI )*( 2.0*Math.Atan( ( 36.0 / 2.0 ) / lensFocalLength ) );

				XmlNode lensNode =  projectNode[ tagLens ];

				SetSingleNodeStringValue( lensNode, tagLensMFOV, mfov.ToString() );
			}
		}

		public Dictionary<int,MyFileInfo> Files
		{
			get
			{
				XmlNodeList images =  projectDoc.GetElementsByTagName( tagImage );

				if ( fileInfosDict == null )
					fileInfosDict =  new Dictionary<int,MyFileInfo>();
				else
					fileInfosDict.Clear();

				foreach ( XmlNode node in images )
				{
					XmlNodeList	kids		=  node.ChildNodes;
					int			whichOne	=  -1;
					MyFileInfo	info		=  null;

					foreach( XmlNode kid in kids )
					{
						if ( kid.Name == tagImageNumber )
						{
							whichOne =  Int32.Parse( kid.InnerText );
						}
						else if ( kid.Name == tagImageFilename )
						{
							info	=  new MyFileInfo( kid.InnerText );
						}
					}

					if ( whichOne != -1 )
					{
						fileInfosDict[ whichOne ]	=  info;
					}
				}

				return ( fileInfosDict );
			}

			set
			{
				XmlNodeList oldImages =  projectDoc.GetElementsByTagName( tagImage );

				fileInfosDict	=  value;

				List<XmlNode>	imageNodes	=  new List<XmlNode>( fileInfosDict.Count );
				
				foreach( KeyValuePair<int,MyFileInfo> pair in fileInfosDict )
				{
					XmlNode	imageNode	=  null;

					foreach ( XmlNode node in oldImages )
					{
						XmlNode indexNode =  node[ tagImageNumber ];
						XmlNode fileNode  =  node[ tagImageFilename ];

						if ( indexNode != null && Int32.Parse( indexNode.InnerText ) == pair.Key &&
							  fileNode.InnerText == pair.Value.FullName )
						{
							// We have the same image so just clone it to keep it. (This way we can preserve
							// non-auto flag locations too if we're here 'cause we changed other project settings.
							imageNode =  node.CloneNode( true );
							break;
						}
					}

					if ( imageNode == null )
					{
						// Create a new image node.
						imageNode	=  projectDoc.CreateElement( tagImage );

						XmlNode indexNode =  projectDoc.CreateElement( tagImageNumber );
						XmlNode fileNode  =  projectDoc.CreateElement( tagImageFilename );

						indexNode.InnerText	=  pair.Key.ToString();
						fileNode.InnerText	=  pair.Value.FullName;

						imageNode.AppendChild( indexNode );
						imageNode.AppendChild( fileNode );
					}

					imageNodes.Add( imageNode );
				}

				// Get rid of the old image nodes.
				while ( oldImages.Count > 0 )
				{
					projectNode.RemoveChild( oldImages[ 0 ] );
				}

				// Now add the "new" ones.
				foreach( XmlNode node in imageNodes )
				{
					projectNode.AppendChild( node );
				}
			}
		}
		#endregion

		public bool Open( string filename )
		{
			bool	isCool	=  true;

			try
			{
				// Open the project file and read in the project info.
				projectDoc.Load( filename );

				// The project node is the root node.
				projectNode =  projectDoc.DocumentElement;
			}

			catch( Exception ex )
			{
				Messager.Show( ex.Message, "Read In PanaVue Project File" );
				isCool	=  false;
			}

			return ( isCool );
		}

		public bool Open( MyFileInfo info )
		{
			return ( Open( info.FullName ) );
		}

		public void Write( XmlWriter writer )
		{
			projectDoc.WriteTo( writer );
		}

		public void Save( string projectFilename )
		{
			// Save project.
			XmlTextWriter xmlWriter = new XmlTextWriter( projectFilename, null );

			xmlWriter.Formatting =  Formatting.Indented;

			Write( xmlWriter );

			xmlWriter.Flush();
			xmlWriter.Close();
		}
	}
}
