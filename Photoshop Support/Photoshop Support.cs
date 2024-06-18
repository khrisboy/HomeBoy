using System;
using MyClasses;

namespace PhotoshopSupport
{
	/// <summary>
	/// Resizing type enum.
	/// </summary>
	public enum ResizeType { Height, Width, MinBoth, MaxBoth };

	/// <summary>
	/// FilesInfo class
	/// </summary>
	public class FilesInfo
	{
		public String	inputDir;
		public String	outputDir;
		public bool		includeSubfolders;
		public bool		flaggedFilesOnly;
		public bool		selectedFilesOnly;

		public FilesInfo()
		{
			inputDir				=  "";
			outputDir			=  "";
			includeSubfolders	=  false;
			flaggedFilesOnly	=  false;
			selectedFilesOnly	=  false;
		}
		
		public void Load( DefaultsAr defaultsAr )
		{
			// If the default is not there we'll just keep what we have.
			inputDir				=  defaultsAr.Contains( "InputDirectory" )		?  defaultsAr[ "InputDirectory" ] : inputDir;
			outputDir			=  defaultsAr.Contains( "OutputDirectory" )		?  defaultsAr[ "OutputDirectory" ] : outputDir;
			includeSubfolders	=  defaultsAr.Contains( "IncludeSubFolders" )	?  defaultsAr[ "IncludeSubFolders" ] : includeSubfolders;
			flaggedFilesOnly	=  defaultsAr.Contains( "FlaggedFilesOnly" )		?  defaultsAr[ "FlaggedFilesOnly" ] : flaggedFilesOnly;
			selectedFilesOnly	=  defaultsAr.Contains( "SelectedFilesOnly" )	?  defaultsAr[ "SelectedFilesOnly" ] : selectedFilesOnly;
		}
		
		public void Save( DefaultsAr defaultsAr )
		{
			defaultsAr[ "InputDirectory" ]		=  (MyString) inputDir;
			defaultsAr[ "OutputDirectory" ]		=  (MyString) outputDir;
			defaultsAr[ "IncludeSubFolders" ]	=  (MyString) includeSubfolders;
			defaultsAr[ "FlaggedFilesOnly" ]		=  (MyString) flaggedFilesOnly;
			defaultsAr[ "SelectedFilesOnly" ]	=  (MyString) selectedFilesOnly;
		}
	}

	/// <summary>
	/// ImagesInfo class.
	/// </summary>
	public class ImagesInfo
	{
		public bool					resizeImages;
		public ResizeInfo			resizeInfoImages;
		public int					jpegQuality;
		public bool					thumbnails;
		public ResizeInfo			resizeInfoThumbs;
		public bool					saveImagesAsJPEG;	// JPEG (default) or TIFF
		public bool					saveThumbsAsGIF;	// GIF (default) or JPEG
		public string				colorSpace;

		public ImagesInfo()
		{
			resizeImages		=  true;
			jpegQuality			=  10;
			thumbnails			=  true;
			saveImagesAsJPEG	=  true;
			saveThumbsAsGIF	=  false;

			resizeInfoImages	=  new ResizeInfo();
			resizeInfoThumbs	=  new ResizeInfo();
		}
			
		public void Load( DefaultsAr defaultsAr )
		{
			// If the default is not there we'll just keep what we have.
			resizeImages		=  defaultsAr.Contains( "ResizeImages" )		?  defaultsAr[ "ResizeImages" ] : resizeImages;
			jpegQuality			=  defaultsAr.Contains( "JPEGQuality" )		?  Int32.Parse( defaultsAr[ "JPEGQuality" ] ) : jpegQuality;
			thumbnails			=  defaultsAr.Contains( "CreateThumbnails" )	?  defaultsAr[ "CreateThumbnails" ] : thumbnails;
			saveImagesAsJPEG	=  defaultsAr.Contains( "SaveImagesFormat" )	?  ( defaultsAr[ "SaveImagesFormat" ].ToString().ToLower() == "jpeg" ) : saveImagesAsJPEG;
			saveThumbsAsGIF	=  defaultsAr.Contains( "SaveThumbsFormat" )	?  ( defaultsAr[ "SaveThumbsFormat" ].ToString().ToLower() == "gif" ) : saveThumbsAsGIF;
		
			resizeInfoImages.Load( defaultsAr, "Images" );
			resizeInfoThumbs.Load( defaultsAr, "Thumbnails" );
		}
		
		public void Save( DefaultsAr defaultsAr )
		{
			defaultsAr[ "ResizeImages" ]		=  (MyString) resizeImages;
			defaultsAr[ "JPEGQuality" ]		=  (MyString) jpegQuality;
			defaultsAr[ "CreateThumbnails" ]	=  (MyString) thumbnails;
			defaultsAr[ "SaveImagesFormat" ]	=  (MyString) ( saveImagesAsJPEG ?  "JPEG" : "TIFF" );
			defaultsAr[ "SaveThumbsFormat" ]	=  (MyString) ( saveThumbsAsGIF ?  "GIG" : "JPEG" );
		
			resizeInfoImages.Save( defaultsAr, "Images" );
			resizeInfoThumbs.Save( defaultsAr, "Thumbnails" );
		}
	}

	public abstract class SharpenOptionsInfo
	{
		public abstract void Load( DefaultsAr defaultsAr, string iCode );
		public abstract void Save( DefaultsAr defaultsAr, string iCode );
	}

	public class UsmSharpenInfo : SharpenOptionsInfo
	{
		private int		amount;
		private double	radius;
		private int		threshhold;

		public UsmSharpenInfo()
		{
			amount		=  150;
			radius		=  1.5;
			threshhold	=  0;
		}

		public int Amount
		{
			get
			{
				return ( amount );
			}

			set
			{
				if ( value >= 1 && value <= 500 )
					amount	=  value;
				else if ( value < 1 )
					amount	=  1;
				else
					amount	=  500;
			}
		}

		public double Radius
		{
			get
			{
				return ( radius );
			}

			set
			{
				if ( value >= 0.1 && value <= 250.0 )
					radius	=  value;
				else if ( value < 0.1 )
					radius	=  0.1;
				else
					radius	=  250.0;
			}
		}

		public int Threshhold
		{
			get
			{
				return ( threshhold );
			}

			set
			{
				if ( value >= 0 && value <= 250 )
					threshhold	=  value;
				else if ( value < 0 )
					threshhold	=  0;
				else
					threshhold	=  250;
			}
		}
			
		public override void Load( DefaultsAr defaultsAr, string code )
		{
			if ( defaultsAr.Contains( "USMAmount"+code ) )
				Amount	=  Int32.Parse( defaultsAr[ "USMAmount"+code ] );
			if ( defaultsAr.Contains( "USMRadius"+code ) )
				Radius	=  Double.Parse( defaultsAr[ "USMRadius"+code ] );
			if ( defaultsAr.Contains( "USMThreshhold"+code ) )
				Threshhold	=  Int32.Parse( defaultsAr[ "USMThreshhold"+code ] );
		}
		
		public override void Save( DefaultsAr defaultsAr, string code )
		{
			defaultsAr[ "USMAmount"+code ]		=  (MyString) Amount;
			defaultsAr[ "USMRadius"+code ]		=  (MyString) Radius;
			defaultsAr[ "USMThreshhold"+code ]	=  (MyString) Threshhold;
		}

	}

	public class NikSharpenInfo : SharpenOptionsInfo
	{
		public enum NikProfileType { None= 0, Anna= 1, John= 2, Zap= 3 };
		
		private NikProfileType	profileType;

		public NikSharpenInfo()
		{
			profileType	=  NikProfileType.John;
		}

		public NikProfileType ProfileType
		{
			get { return ( profileType ); }
			set
			{
				if ( value >= NikProfileType.Anna && value <= NikProfileType.Zap )
					profileType	=  value;
				else if ( value <= NikProfileType.Anna )
					profileType	=  NikProfileType.Anna;
				else
					profileType	=  NikProfileType.Zap;
			}
		}

		public override void Load( DefaultsAr defaultsAr, string code )
		{
			if ( defaultsAr.Contains( "NikProfileType"+code ) )
			{
				if ( defaultsAr[ "NikProfileType"+code ] == "Anna" )
					profileType	=  NikProfileType.Anna;
				else if ( defaultsAr[ "NikProfileType"+code ] == "John" )
					profileType	=  NikProfileType.John;
				if ( defaultsAr[ "NikProfileType"+code ] == "Zap" )
					profileType	=  NikProfileType.Zap;
				else
				{
				}
			}
		}
		
		public override void Save( DefaultsAr defaultsAr, string code )
		{
			switch( profileType )
			{
				case NikProfileType.Anna:	defaultsAr[ "NikProfileType"+code ]	=  new MyString( "Anna" );	break;
				case NikProfileType.John:	defaultsAr[ "NikProfileType"+code ]	=  new MyString( "John" );	break;
				default:					defaultsAr[ "NikProfileType"+code ]	=  new MyString( "Zap" );	break;
			}
		}
	}

	public class NikDisplaySharpenInfo : NikSharpenInfo
	{
		private int	strength;

		public NikDisplaySharpenInfo()
		{
			strength	=  75;
		}

		public int Strength
		{
			get { return ( strength ); }
			set { strength =  value; }
		}

		public override void Load( DefaultsAr defaultsAr, string code )
		{
			base.Load( defaultsAr, code );

			if ( defaultsAr.Contains( "NikStrength"+code ) )
			{
				strength =  defaultsAr[ "NikStrength"+code ];
			}
		}
		
		public override void Save( DefaultsAr defaultsAr, string code )
		{
			base.Save( defaultsAr, code );

			defaultsAr[ "NikStrength"+code ] =  (MyString) strength;
		}
	}

	public class NikPrintSharpenInfo : NikSharpenInfo
	{
		private int printerResolution;
		private int paperType;

		public int PrinterResolution
		{
			get { return ( printerResolution ); }
			set { printerResolution =  value; }
		}

		public int PaperType
		{
			get { return ( paperType ); }
			set { paperType =  value; }
		}
	}

	public class SharpenInfo
	{
		public enum	SharpenType { None, Nik, NikDisplay, Usm };

		public SharpenType			sharpenType;
		public SharpenOptionsInfo	options;
		public bool						useMask;
		public string					maskString;

		public SharpenInfo()
		{
			sharpenType	=  SharpenType.None;
			useMask		=  false;
		}

		public void Load( DefaultsAr defaultsAr, string code )
		{
			if ( defaultsAr.Contains( "SharpenType"+code ) )
			{
				if ( defaultsAr[ "SharpenType"+code ] == "USM" )
				{
					sharpenType	=  SharpenType.Usm;
					options		=  new UsmSharpenInfo();
					
					options.Load( defaultsAr, code );
				}
				else if ( defaultsAr[ "SharpenType"+code ] == "Nik Display" )
				{
					sharpenType	=  SharpenType.NikDisplay;
					options		=  new NikDisplaySharpenInfo();
					
					options.Load( defaultsAr, code );
				}
				else if ( defaultsAr[ "SharpenType"+code ] == "Nik" )
				{
					sharpenType	=  SharpenType.Nik;
					options		=  new NikPrintSharpenInfo();
					
					options.Load( defaultsAr, code );
				}
				else if ( defaultsAr[ "SharpenType"+code ] == "None" )
				{
					sharpenType	=  SharpenType.None;
				}
				else
				{
				}
			}

			useMask		=  defaultsAr.Contains( "UseMask"+code )		?  defaultsAr[ "UseMask"+code ] : useMask;
			maskString	=  defaultsAr.Contains( "MaskString"+code )	?  defaultsAr[ "MaskString"+code ] : maskString;
		}
		
		public void Save( DefaultsAr defaultsAr, string code )
		{
			switch( sharpenType )
			{
				case SharpenType.Usm:			defaultsAr[ "SharpenType"+code ]	=  new MyString( "USM" );	break;
				case SharpenType.NikDisplay:	defaultsAr[ "SharpenType"+code ]	=  new MyString( "Nik Display" );	break;
				case SharpenType.Nik:			defaultsAr[ "SharpenType"+code ]	=  new MyString( "Nik" );	break;
				default:						defaultsAr[ "SharpenType"+code ]	=  new MyString( "None" );	break;
			}

			if ( options != null )
				options.Save( defaultsAr, code );

			defaultsAr[ "UseMask"+code ]		=  (MyString) useMask;
			defaultsAr[ "MaskString"+code ]	=  (MyString) maskString;
		}
	}

	public class SharpenInfosArray : System.Collections.CollectionBase
	{
		public void Add( SharpenInfo info )
		{
			List.Add( info );
		}

		public void Remove( int index )
		{
			// Check to see if there is an info at the supplied index.
			if ( index > Count - 1 || index < 0 )
			{
				// If no widget exists, a messagebox is shown and the operation 
				// is cancelled.
				//System.Windows.Forms.MessageBox.Show( "Index not valid!" );
			}
			else
			{
				List.RemoveAt( index ); 
			}
		}

		public SharpenInfo this[ int index ]
		{
			get
			{
				return ( (SharpenInfo) List[ index ] );
			}

			set
			{
				List[ index ]	=  value;
			}
		}
	}

	public class ResizeInfo
	{
		public int			resizeRegular;
		public ResizeType	typeRegular;
		public int			resizePanorama;
		public ResizeType	typePanorama;
		public double		panoramaAspect;

		public ResizeInfo()
		{
			resizeRegular	=  450;
			typeRegular		=  ResizeType.MaxBoth;
			resizePanorama	=  200;
			typePanorama	=  ResizeType.MinBoth; //***ResizeType.Height;
			panoramaAspect	=  3.0;
		}

		public void Load( DefaultsAr defaultsAr, string code )
		{
			resizeRegular	=  defaultsAr.Contains( "ResizeRegular"+code )	?  Int32.Parse( defaultsAr[ "ResizeRegular"+code ] ) : resizeRegular;
			resizePanorama	=  defaultsAr.Contains( "ResizePanorama"+code )	?  Int32.Parse( defaultsAr[ "ResizePanorama"+code ] ) : resizePanorama;
			panoramaAspect	=  defaultsAr.Contains( "PanoramaAspect"+code )	?  Double.Parse( defaultsAr[ "PanoramaAspect"+code ] ) : panoramaAspect;

			if ( defaultsAr.Contains( "ResizeTypeRegular"+code ) )
			{
				if ( defaultsAr[ "ResizeTypeRegular"+code ] == "Width" )
					typeRegular	=  ResizeType.Width;
				else if ( defaultsAr[ "ResizeTypeRegular"+code ] == "Height" )
					typeRegular	=  ResizeType.Height;
				else if ( defaultsAr[ "ResizeTypeRegular"+code ] == "MinBoth" )
					typeRegular	=  ResizeType.MinBoth;
				else if ( defaultsAr[ "ResizeTypeRegular"+code ] == "MaxBoth" )
					typeRegular	=  ResizeType.MaxBoth;
			}

			if ( defaultsAr.Contains( "ResizeTypePanorama"+code ) )
			{
				if ( defaultsAr[ "ResizeTypePanorama"+code ] == "Width" )
					typePanorama	=  ResizeType.Width;
				else if ( defaultsAr[ "ResizeTypePanorama"+code ] == "Height" )
					typePanorama	=  ResizeType.Height;
				else if ( defaultsAr[ "ResizeTypePanorama"+code ] == "MinBoth" )
					typePanorama	=  ResizeType.MinBoth;
				else if ( defaultsAr[ "ResizeTypePanorama"+code ] == "MaxBoth" )
					typePanorama	=  ResizeType.MaxBoth;
			}
		}
		
		public void Save( DefaultsAr defaultsAr, string code )
		{
			defaultsAr[ "ResizeRegular"+code ]	=  (MyString) resizeRegular;
			defaultsAr[ "ResizePanorama"+code ]	=  (MyString) resizePanorama;
			defaultsAr[ "PanoramaAspect"+code ]	=  (MyString) panoramaAspect;

			switch( typeRegular )
			{
				case ResizeType.Width:		defaultsAr[ "ResizeTypeRegular"+code ]	=  new MyString( "Width" );	break;
				case ResizeType.Height:		defaultsAr[ "ResizeTypeRegular"+code ]	=  new MyString( "Height" );	break;
				case ResizeType.MinBoth:	defaultsAr[ "ResizeTypeRegular"+code ]	=  new MyString( "MinBoth" );	break;
				default:							defaultsAr[ "ResizeTypeRegular"+code ]	=  new MyString( "MaxBoth" );	break;
			}

			switch( typePanorama )
			{
				case ResizeType.Width:		defaultsAr[ "ResizeTypePanorama"+code ]	=  new MyString( "Width" );	break;
				case ResizeType.Height:		defaultsAr[ "ResizeTypePanorama"+code ]	=  new MyString( "Height" );	break;
				case ResizeType.MinBoth:	defaultsAr[ "ResizeTypePanorama"+code ]	=  new MyString( "MinBoth" );	break;
				default:							defaultsAr[ "ResizeTypePanorama"+code ]	=  new MyString( "MaxBoth" );	break;
			}
		}
	}

	public class FrameInfo
	{
		public int			borderWidth;
		public RgbColor	borderColor;
		public RgbColor	backgroundColor;
		public int			backgroundWidth;
		public int			shadowWidth;
		public int			shadowBlur;
		public int			shadowSoftness;
		public bool			shadowRight;
		public bool			shadowDown;
		public RgbColor	shadowColor;
		public bool			sliceAndSave;
		public bool			makeFrame;

		public FrameInfo()
		{
			borderWidth			=  1;
			borderColor			=  new RgbColor( 0,0,0 );
			backgroundColor	=  new RgbColor( 255,216,255 );
			backgroundWidth	=  100;
			shadowWidth			=  10;
			shadowBlur			=  10;
			shadowSoftness		=  80;
			shadowRight			=  true;
			shadowDown			=  true;
			shadowColor			=  new RgbColor( 0,0,0 );
			sliceAndSave		=  false;
			makeFrame			=  true;
		}
		
		public void Load( DefaultsAr defaultsAr, string iCode )
		{
			string code;

			code	=  iCode.ToString();

			// If the default is not there we'll just keep what we have.
			borderWidth			=  defaultsAr.Contains( "BorderWidth"+code )			?  Int32.Parse( defaultsAr[ "BorderWidth"+code ] ) : borderWidth;
			backgroundWidth	=  defaultsAr.Contains( "BackgroundWidth"+code )	?  Int32.Parse( defaultsAr[ "BackgroundWidth"+code ] ) : backgroundWidth;
			shadowWidth			=  defaultsAr.Contains( "ShadowWidth"+code )			?  Int32.Parse( defaultsAr[ "ShadowWidth"+code ] ) : shadowWidth;
			shadowBlur			=  defaultsAr.Contains( "ShadowBlur"+code )			?  Int32.Parse( defaultsAr[ "ShadowBlur"+code ] ) : shadowBlur;
			shadowSoftness		=  defaultsAr.Contains( "ShadowSoftness"+code )		?  Int32.Parse( defaultsAr[ "ShadowSoftness"+code ] ) : shadowSoftness;
			shadowRight			=  defaultsAr.Contains( "ShadowRight"+code )			?  defaultsAr[ "ShadowRight"+code ] : shadowRight;
			shadowDown			=  defaultsAr.Contains( "ShadowDown"+code )			?  defaultsAr[ "ShadowDown"+code ] : shadowDown;
			sliceAndSave		=  defaultsAr.Contains( "SliceAndSave"+code )		?  defaultsAr[ "SliceAndSave"+code ] : sliceAndSave;
			makeFrame			=  defaultsAr.Contains( "MakeFrame"+code )			?  defaultsAr[ "MakeFrame"+code ] : makeFrame;

			if ( defaultsAr.Contains( "BorderColor"+code ) )
				borderColor.Equals( defaultsAr.Contains( "BorderColor"+code ) );
			if ( defaultsAr.Contains( "BackgroundColor"+code ) )
				backgroundColor.Equals( defaultsAr[ "BackgroundColor"+code ] );
			if ( defaultsAr.Contains( "ShadowColor"+code ) )
				shadowColor.Equals( defaultsAr[ "ShadowColor"+code ] );
		}
	
		public void Save( DefaultsAr defaultsAr, string iCode )
		{
			string code	=  iCode.ToString();

			defaultsAr[ "BorderWidth"+code ]			=  (MyString) borderWidth;
			defaultsAr[ "BorderColor"+code ]			=  (MyString) borderColor.ToString();
			defaultsAr[ "BackgroundColor"+code ]	=  (MyString) backgroundColor.ToString();
			defaultsAr[ "BackgroundWidth"+code ]	=  (MyString) backgroundWidth;
			defaultsAr[ "ShadowWidth"+code ]			=  (MyString) shadowWidth;
			defaultsAr[ "ShadowBlur"+code ]			=  (MyString) shadowBlur;
			defaultsAr[ "ShadowSoftness"+code ]		=  (MyString) shadowSoftness;
			defaultsAr[ "ShadowRight"+code ]			=  (MyString) shadowRight;
			defaultsAr[ "ShadowDown"+code ]			=  (MyString) shadowDown;
			defaultsAr[ "ShadowColor"+code ]			=  (MyString) shadowColor.ToString();
			defaultsAr[ "SliceAndSave"+code ]		=  (MyString) sliceAndSave;
			defaultsAr[ "MakeFrame"+code ]			=  (MyString) makeFrame;
		}
	}
}
