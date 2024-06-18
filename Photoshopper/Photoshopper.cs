using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;

using Photoshop;
using PhotoshopSupport;
using MyClasses;

namespace Photoshopper
{
	/// <summary>
	/// Summary description for Photoshopper.
	/// </summary>
	public class Photoshopper
	{
		public Photoshop.Application	theApp;

		public Photoshopper( Photoshop.Application app )
		{
			// Set the Photoshop theApp object.
			theApp	=  app;
		}

		/////////////////////////////////////////////////////////////////////
		/// Action manager functions.
		///
		public void Fill( DPoint point, double opacity, int tolerance, bool antiAliased, bool contiguous, bool allLayers )
		{
			// Set the Action ID for Fill.
			int fillID	=  theApp.CharIDToTypeID( "Fl  " );

			// Make necessary Action Descriptors.	
			ActionDescriptor fillAD	=  new ActionDescriptor();
			ActionDescriptor ptAD	=  new ActionDescriptor();
        
			int h1	=   theApp.CharIDToTypeID( "Hrzn" );
			int h2	=   theApp.CharIDToTypeID( "#Rlt" );
			ptAD.PutUnitDouble( h1, h2, point.X);

			int v1	=   theApp.CharIDToTypeID( "Vrtc" );
			int v2	=   theApp.CharIDToTypeID( "#Rlt" );
			ptAD.PutUnitDouble( v1, v2, point.Y );
        
			int from	=  theApp.CharIDToTypeID( "From" );
			int pnt	=  theApp.CharIDToTypeID( "Pnt " );
			fillAD.PutObject( from, pnt, ptAD );
    
			// Tolerance.
			int	tol	=  theApp.CharIDToTypeID( "Tlrn" );
			fillAD.PutInteger( tol, tolerance );
    
			// All layers?
			int	mrgd	=  theApp.CharIDToTypeID( "Mrgd" );
			fillAD.PutBoolean( mrgd, allLayers );

			// Anti-aliased?
			int	aa	=  theApp.CharIDToTypeID( "AntA" );
			fillAD.PutBoolean( aa, antiAliased );
   
			// Color (foreground)
			int	usng	=  theApp.CharIDToTypeID( "Usng" );
			int	flcn	=  theApp.CharIDToTypeID( "FlCn" );
			int	fore	=  theApp.CharIDToTypeID( "FrgC" );
			fillAD.PutEnumerated( usng, flcn, fore );
    
			// Opacity.
			int	op			=  theApp.CharIDToTypeID( "Opct" );
			int	percent	=  theApp.CharIDToTypeID( "#Prc" );
			fillAD.PutUnitDouble( op, percent, opacity );

			// Contiguous?
			int	cont	=  theApp.CharIDToTypeID( "Cntg" );
			fillAD.PutBoolean( cont, contiguous );
    
			// Execute Fill.
			theApp.ExecuteAction( fillID, fillAD, PsDialogModes.psDisplayNoDialogs );
		}

		public void EditFill( RgbColor color, double opacity, string blendMode, bool preserveTrans )
		{
			// Set the Action ID for Fill.
			int fillID	=  theApp.CharIDToTypeID( "Fl  " );

			// Make necessary Action Descriptors.	
			ActionDescriptor fillAD	=  new ActionDescriptor();
    
			// Fill with a specified color
			int	usng	=  theApp.CharIDToTypeID( "Usng" );
			int	flcn	=  theApp.CharIDToTypeID( "FlCn" );
			int	colr	=  theApp.CharIDToTypeID( "Clr " );
   
			fillAD.PutEnumerated( usng, flcn, colr );

			// Set color to use.    
			ActionDescriptor	colorAD	=  new ActionDescriptor();
	
			int	red	=  theApp.CharIDToTypeID( "Rd  " );
			colorAD.PutDouble( red, color.R );
	
			int	green	=  theApp.CharIDToTypeID( "Grn " );
			colorAD.PutDouble( green, color.G );
	
			int	blue	=  theApp.CharIDToTypeID( "Bl  " );
			colorAD.PutDouble( blue, color.B );
	
			int	rgb	=  theApp.CharIDToTypeID( "RGBC" );
			fillAD.PutObject( colr, rgb, colorAD );

			if ( opacity > 0.0 )
			{
				// Opacity.
				int	op			=  theApp.CharIDToTypeID( "Opct" );
				int	percent	=  theApp.CharIDToTypeID( "#Prc" );
				fillAD.PutUnitDouble( op, percent, opacity );
			}

			if ( blendMode != "" )
			{
				// Mode.
				int	md		= theApp.CharIDToTypeID( "Md  " );
				int	blnm	= theApp.CharIDToTypeID( "BlnM" );
				int	mode	= theApp.CharIDToTypeID( blendMode );
				fillAD.PutEnumerated( md, blnm, mode );
			}
   
			// Preserve transparency?
			int	prst	=  theApp.CharIDToTypeID( "PrsT" );
			fillAD.PutBoolean( prst, preserveTrans );
    
			// Execute Fill.
			theApp.ExecuteAction( fillID, fillAD, PsDialogModes.psDisplayNoDialogs );
		}

		public void MakeCurrentLayerBackground()
		{
			int	backLayerID	=  theApp.CharIDToTypeID( "Mk  " );

			ActionDescriptor	backLayerAD	=  new ActionDescriptor();
			int					nullID		=  theApp.CharIDToTypeID( "null" );
			ActionReference	bacAR			=  new ActionReference();
			int					bckl			=  theApp.CharIDToTypeID( "BckL" );
	
			bacAR.PutClass( bckl );
			backLayerAD.PutReference( nullID, bacAR );
    
			int					usng	=  theApp.CharIDToTypeID( "Usng" );
			ActionReference	lyrAR	=  new ActionReference();
			int					lyr	=  theApp.CharIDToTypeID( "Lyr " );
			int					ordn	=  theApp.CharIDToTypeID( "Ordn" );
			int					trgt	=  theApp.CharIDToTypeID( "Trgt" );
	
			lyrAR.PutEnumerated( lyr, ordn, trgt );
    
			backLayerAD.PutReference( usng, lyrAR );
    
			// Do it!
			theApp.ExecuteAction( backLayerID, backLayerAD, PsDialogModes.psDisplayNoDialogs );
		}

		public void SelectRectArea( Bounds bounds, PsSelectionType selectMode, double featherAmount )
		{
			int	selectID;
			
			if ( selectMode == PsSelectionType.psDiminishSelection )
				selectID	=	theApp.CharIDToTypeID( "SbtF" );
			else if ( selectMode == PsSelectionType.psExtendSelection )
				selectID	=	theApp.CharIDToTypeID( "AddT" );
			else if ( selectMode == PsSelectionType.psIntersectSelection )
				selectID	=	theApp.CharIDToTypeID( "IntW" );
			else	// Replace selection.
				selectID	=	theApp.CharIDToTypeID( "setd" );

			ActionDescriptor	desc608	=  new ActionDescriptor();

			int					nullID		=  theApp.CharIDToTypeID( "null" );
			ActionReference	ref41			=  new ActionReference();
			int					channelID	=  theApp.CharIDToTypeID( "Chnl" );
			int					fselID		=  theApp.CharIDToTypeID( "fsel" );

			ref41.PutProperty( channelID, fselID );
			desc608.PutReference( nullID, ref41 );

			int					id3002	=  theApp.CharIDToTypeID( "T   " );
			ActionDescriptor	desc609	=  new ActionDescriptor();

			int	leftID	=  theApp.CharIDToTypeID( "Left" );
			int	pxlID		=  theApp.CharIDToTypeID( "#Pxl" );
			desc609.PutUnitDouble( leftID, pxlID, bounds[ 0 ] );

			int	topID	=  theApp.CharIDToTypeID( "Top " );
			desc609.PutUnitDouble( topID, pxlID, bounds[ 1 ] );

			int	RghtID	=  theApp.CharIDToTypeID( "Rght" );
			desc609.PutUnitDouble( RghtID, pxlID, bounds[ 2 ] );

			int	BtomID	=  theApp.CharIDToTypeID( "Btom" );
			desc609.PutUnitDouble( BtomID, pxlID, bounds[ 3 ] );

			int	id3011	=  theApp.CharIDToTypeID( "Rctn" );
			desc608.PutObject( id3002, id3011, desc609 );

			int	fthrID	=  theApp.CharIDToTypeID( "Fthr" );
			desc608.PutUnitDouble( fthrID, pxlID, featherAmount );

			theApp.ExecuteAction( selectID, desc608, PsDialogModes.psDisplayNoDialogs );
		}

		public void SelectLayerTransparencyChannel( string layerName, bool addIt )
		{
			if ( !addIt )
			{
				// Set selection to specified layer transparency channel.
				int					id436		=  theApp.CharIDToTypeID( "setd" );
				ActionDescriptor	desc84	=  new ActionDescriptor();
				int					id437		=  theApp.CharIDToTypeID( "null" );
				ActionReference	ref31		=  new ActionReference();
				int id438	=  theApp.CharIDToTypeID( "Chnl" );
				int id439	=  theApp.CharIDToTypeID( "fsel" );
				ref31.PutProperty( id438, id439 );
				desc84.PutReference( id437, ref31 );
				int id440	=  theApp.CharIDToTypeID( "T   " );
				ActionReference	ref32	=  new ActionReference();
				int id441	=  theApp.CharIDToTypeID( "Chnl" );
				int id442	=  theApp.CharIDToTypeID( "Chnl" );
				int id443	=  theApp.CharIDToTypeID( "Trsp" );
				ref32.PutEnumerated( id441, id442, id443 );
				int id444	=  theApp.CharIDToTypeID( "Lyr " );
				ref32.PutName( id444, layerName );
				desc84.PutReference( id440, ref32 );
	    
				theApp.ExecuteAction( id436, desc84, PsDialogModes.psDisplayNoDialogs );
			}
			else
			{
				// Add specified layer transparency channel to selection.
				int					id700		=  theApp.CharIDToTypeID( "Add " );
				ActionDescriptor	desc137	=  new ActionDescriptor();
				int					id701		=  theApp.CharIDToTypeID( "null" );
				ActionReference	ref70		=  new ActionReference();
				int					id702		=  theApp.CharIDToTypeID( "Chnl" );
				int					id703		=  theApp.CharIDToTypeID( "Chnl" );
				int					id704		=  theApp.CharIDToTypeID( "Trsp" );
				ref70.PutEnumerated( id702, id703, id704 );
				int					id705		=  theApp.CharIDToTypeID( "Lyr " );
				ref70.PutName( id705, layerName );
				desc137.PutReference( id701, ref70 );
				int					id706		=  theApp.CharIDToTypeID( "T   " );
				ActionReference	ref71		=  new ActionReference();
				int					id707		=  theApp.CharIDToTypeID( "Chnl" );
				int					id708		=  theApp.CharIDToTypeID( "fsel" );
				ref71.PutProperty( id707, id708 );
				desc137.PutReference( id706, ref71 );
	    
				theApp.ExecuteAction( id700, desc137, PsDialogModes.psDisplayNoDialogs );
			}
		}

		public void Crop()
		{
			// Crops based on whatever is selected.
			int					cropID	=  theApp.CharIDToTypeID( "Crop" );
			ActionDescriptor	desc		=  new ActionDescriptor();
	
			theApp.ExecuteAction( cropID, desc, PsDialogModes.psDisplayNoDialogs );
		}

		public void CopyMerged()
		{
			int	copyMID	=  theApp.CharIDToTypeID( "CpyM" );
	
			theApp.ExecuteAction( copyMID, null, PsDialogModes.psDisplayNoDialogs );
		}

		public Document NewDocFromCopy( string docName )
		{
			// Create New document, presets from clipboard (no changes).
			int	makeID	=  theApp.CharIDToTypeID( "Mk  " );
			ActionDescriptor	makeAD	=  new ActionDescriptor();
	
			int	newID	=  theApp.CharIDToTypeID( "Nw  " );
			ActionDescriptor	newAD	=  new ActionDescriptor();
	
			int	nameID	=  theApp.CharIDToTypeID( "Nm  " );
			newAD.PutString( nameID, docName );
	
			int	presetID	=  theApp.StringIDToTypeID( "preset" );
			newAD.PutString( presetID, "Clipboard" );
	
			int	id72	=  theApp.CharIDToTypeID( "Dcmn" );
			makeAD.PutObject( newID, id72, newAD );
	
			theApp.ExecuteAction( makeID, makeAD, PsDialogModes.psDisplayNoDialogs );
	
			// Now paste from the current clipboard contents into the new document.
			int	pasteID	=  theApp.CharIDToTypeID( "past" );
			ActionDescriptor	pasteAD	=  new ActionDescriptor();
	
			int id74	=   theApp.CharIDToTypeID( "AntA" );
			int id75	=   theApp.CharIDToTypeID( "Annt" );
			int id76	=   theApp.CharIDToTypeID( "Anno" );
			pasteAD.PutEnumerated( id74, id75, id76 );
	
			theApp.ExecuteAction( pasteID, pasteAD, PsDialogModes.psDisplayNoDialogs );
	
			// Return the new document reference.
			return ( theApp.ActiveDocument );
		}

		public ArrayList GetFilesFromFileBrowser( bool flagged, bool selected )
		{
			ArrayList			filesAr				=  new ArrayList();
			ActionReference	actionRef			=  new ActionReference();
			int					fileBrowserStrID	=  theApp.StringIDToTypeID( "fileBrowser" );
	
			actionRef.PutProperty( theApp.CharIDToTypeID( "Prpr" ), fileBrowserStrID );
			actionRef.PutEnumerated( theApp.CharIDToTypeID( "capp" ), theApp.CharIDToTypeID( "Ordn" ), theApp.CharIDToTypeID( "Trgt" ) );

			ActionDescriptor	actionDesc	=  theApp.ExecuteActionGet( actionRef );
	
			if ( actionDesc.Count > 0 && actionDesc.HasKey( fileBrowserStrID ) )
			{
				ActionDescriptor	fbDesc			=  actionDesc.GetObjectValue( fileBrowserStrID );
				int					keyFilesList	=  theApp.CharIDToTypeID( "flst" );
		
				if ( fbDesc.Count > 0  &&  fbDesc.HasKey( keyFilesList ) )
				{
					ActionList	fileList		=  fbDesc.GetList( keyFilesList );
					int			flaggedID	=  theApp.StringIDToTypeID( "flagged" );
					int			selectedID	=  theApp.CharIDToTypeID( "fsel" );
					int			keyPath		=  theApp.CharIDToTypeID( "Path" );
			
					for ( int i= 0;  i< fileList.Count;  i++ )
					{
						ActionDescriptor	fileDesc	= fileList.GetObjectValue( i );
				
						if ( fileDesc.Count > 0  &&  fileDesc.HasKey( keyPath ) )
						{
							if ( flagged && fileDesc.HasKey( flaggedID ) && fileDesc.GetBoolean( flaggedID ) )
							{
								string	fileOrFolder	=  fileDesc.GetPath( keyPath );
						
								if ( File.Exists( fileOrFolder ) )
									filesAr.Add( fileOrFolder );
							}
					
							if ( selected && fileDesc.HasKey( selectedID ) && fileDesc.GetBoolean( selectedID ) )
							{
								string	fileOrFolder	=  fileDesc.GetPath( keyPath );
						
								if ( File.Exists( fileOrFolder ) )
									filesAr.Add( fileOrFolder );
							}
						}
					}
				}
			}
	
			return ( filesAr );
		}

		public void NikSharpenInternet( SharpenOptionsInfo options )
		{
			if ( options.GetType() != typeof( NikSharpenInfo ) )
				throw( new ApplicationException( "Wrong type in argument to: NikSharpenInternet!" ) );

			int	profile	=  (int) ( (NikSharpenInfo) options ).ProfileType;

			int					nik			=  theApp.CharIDToTypeID( "spe3" );
			ActionDescriptor	ad				= 	new ActionDescriptor();
			int					nikProfile	= 	theApp.CharIDToTypeID( "par1" );
	
			ad.PutInteger( nikProfile, profile );
	
			theApp.ExecuteAction( nik, ad, PsDialogModes.psDisplayNoDialogs );
		}

		public void NikSharpen( int profile, int eyeDistance, int printerQuality, int printerRes )
		{
			int					nik	=  theApp.CharIDToTypeID( "spe2" );
			ActionDescriptor	ad		= 	new ActionDescriptor();
	
			int	nikProfile			= 	theApp.CharIDToTypeID( "par1" );	// 1-3,	Anna, John, Zap
			int	nikEyeDIstance		= 	theApp.CharIDToTypeID( "par2" );	// 0-4,	Book, Small Box, Large Box, Small Poster, Large Poster
			int	nikPrinterQuality	= 	theApp.CharIDToTypeID( "par3" );	// 0-4,	good= 4
			int	nikPrinterRes		= 	theApp.CharIDToTypeID( "par4" );	// 0-13,	1440x1440= 7
	
			ad.PutInteger( nikProfile, profile );
			ad.PutInteger( nikEyeDIstance, eyeDistance );
			ad.PutInteger( nikPrinterQuality, printerQuality );
			ad.PutInteger( nikPrinterRes, printerRes );
	
			theApp.ExecuteAction( nik, ad, PsDialogModes.psDisplayNoDialogs );
		}

		public void NikSharpen2Display( NikDisplaySharpenInfo options )
		{
			if ( options.GetType() != typeof( NikDisplaySharpenInfo ) )
				throw (new ApplicationException( "Wrong type in argument to: NikSharpen2Display!" ));

			NikSharpenInfo.NikProfileType profile	=  ((NikDisplaySharpenInfo) options).ProfileType;
			int							  strength	=  ((NikDisplaySharpenInfo) options).Strength;

			const string Anna	=  "C1= 60  C2= 60  C3= 60  C4= 60  C5= 60";
			const string John	=  "C1= 100  C2= 100  C3= 100  C4= 100  C5= 100";
			const string Zap	=  "C1= 110  C2= 110  C3= 110  C4= 110  C5= 110";

			int					nik	=  theApp.CharIDToTypeID( "s209" );
			ActionDescriptor	ad		= 	new ActionDescriptor();

			int	nikSharpenID	= 	theApp.CharIDToTypeID( "par1" );

			// "nik  Dist= 0  Auto= 1  FUJI_Paper= 2  FUJI_Printer= 1  INK_Paper= 2  INK_Printer= 1  INK_CAN_Paper= 2  INK_CAN_Printer= 1  INK_EPS_Paper= 2  INK_EPS_Printer= 1  INK_HP_Paper= 2  INK_HP_Printer= 1  INK_LEX_Paper= 2  INK_LEX_Printer= 1  LAB_Paper= 2  LAB_Printer= 1  OFF_Paper= 1  OFF_Printer= 133  ONS_str= 75  PHOTO_Paper= 2  PHOTO_Printer= 1  C1= 100  C2= 100  C3= 100  C4= 100  C5= 100  SharCont= 56  bC1= 255  bC2= 128  bC3= 0  bC4= 70  bC5= 180  rC1= 255  rC2= 128  rC3= 0  rC4= 200  rC5= 70  gC1= 255  gC2= 128  gC3= 0  gC4= 140  gC5= 130  RAW_str= 50 "
			string	nikSharpenInfoString =  "nik  Dist= 0  Auto= 1  FUJI_Paper= 2  FUJI_Printer= 1  INK_Paper= 2  INK_Printer= 1  INK_CAN_Paper= 2  INK_CAN_Printer= 1  INK_EPS_Paper= 2  INK_EPS_Printer= 1  INK_HP_Paper= 2  INK_HP_Printer= 1  INK_LEX_Paper= 2  INK_LEX_Printer= 1  LAB_Paper= 2  LAB_Printer= 1  OFF_Paper= 1  OFF_Printer= 133  ONS_str= %Strength  PHOTO_Paper= 2  PHOTO_Printer= 1  %SharpenLevel  SharCont= 56  bC1= 255  bC2= 128  bC3= 0  bC4= 70  bC5= 180  rC1= 255  rC2= 128  rC3= 0  rC4= 200  rC5= 70  gC1= 255  gC2= 128  gC3= 0  gC4= 140  gC5= 130  RAW_str= 50 ";

			// Amount of sharpening (use old default equivalents).
			if ( profile == NikSharpenInfo.NikProfileType.Anna )
				nikSharpenInfoString =  nikSharpenInfoString.Replace( "%SharpenLevel", Anna );
			else if ( profile == NikSharpenInfo.NikProfileType.John )
				nikSharpenInfoString =  nikSharpenInfoString.Replace( "%SharpenLevel", John );
			else if ( profile == NikSharpenInfo.NikProfileType.Zap )
				nikSharpenInfoString =  nikSharpenInfoString.Replace( "%SharpenLevel", Zap );

			// Strength.
			nikSharpenInfoString =  nikSharpenInfoString.Replace( "%Strength", strength.ToString() );		// 0-100

			ad.PutString( nikSharpenID, nikSharpenInfoString );

			// For some !$#@*! reason psDisplayAllDialogs does not work so we say error dlgs only which does.
			theApp.ExecuteAction( nik, ad, PsDialogModes.psDisplayErrorDialogs );
		}

		public void NikSharpen2( NikSharpenInfo.NikProfileType profile, int eyeDistance, int paperType, int printerRes )
		{
			const string Anna	=  "C1= 60  C2= 60  C3= 60  C4= 60  C5= 60";
			const string John	=  "C1= 100  C2= 100  C3= 100  C4= 100  C5= 100";
			const string Zap	=  "C1= 110  C2= 110  C3= 110  C4= 110  C5= 110";

			int					nik	=  theApp.CharIDToTypeID( "s203" );
			ActionDescriptor	ad		= 	new ActionDescriptor();
	
			int	nikSharpenID	= 	theApp.CharIDToTypeID( "par1" );

			// "nik  Dist= 0  Auto= 1  FUJI_Paper= 2  FUJI_Printer= 1  INK_Paper= 2  INK_Printer= 1  INK_CAN_Paper= 2  INK_CAN_Printer= 1  INK_EPS_Paper= 1  INK_EPS_Printer= 0  INK_HP_Paper= 2  INK_HP_Printer= 1  INK_LEX_Paper= 2  INK_LEX_Printer= 1  LAB_Paper= 2  LAB_Printer= 1  OFF_Paper= 1  OFF_Printer= 133  ONS_str= 75  PHOTO_Paper= 2  PHOTO_Printer= 1  C1= 100  C2= 100  C3= 100  C4= 100  C5= 100  SharCont= 56  bC1= 255  bC2= 128  bC3= 0  bC4= 70  bC5= 180  rC1= 255  rC2= 128  rC3= 0  rC4= 200  rC5= 70  gC1= 255  gC2= 128  gC3= 0  gC4= 140  gC5= 130  RAW_str= 50 "
			string	nikSharpenInfoString =  "nik  Dist= %EyeDistance  Auto= 1  FUJI_Paper= 2  FUJI_Printer= 1  INK_Paper= 2  INK_Printer= 1  INK_CAN_Paper= 2  INK_CAN_Printer= 1  INK_EPS_Paper= %EpsonPaper  INK_EPS_Printer= %EpsonQuality  INK_HP_Paper= 2  INK_HP_Printer= 1  INK_LEX_Paper= 2  INK_LEX_Printer= 1  LAB_Paper= 2  LAB_Printer= 1  OFF_Paper= 1  OFF_Printer= 133  ONS_str= 75  PHOTO_Paper= 2  PHOTO_Printer= 1  %SharpenLevel  SharCont= 56  bC1= 255  bC2= 128  bC3= 0  bC4= 70  bC5= 180  rC1= 255  rC2= 128  rC3= 0  rC4= 200  rC5= 70  gC1= 255  gC2= 128  gC3= 0  gC4= 140  gC5= 130  RAW_str= 50 ";
			
			// Amount of sharpening (use old default equivalents).
			if ( profile == NikSharpenInfo.NikProfileType.Anna )
				nikSharpenInfoString =  nikSharpenInfoString.Replace( "%SharpenLevel", Anna );
			else if ( profile == NikSharpenInfo.NikProfileType.John )
				nikSharpenInfoString =  nikSharpenInfoString.Replace( "%SharpenLevel", John );
			else if ( profile == NikSharpenInfo.NikProfileType.Zap )
				nikSharpenInfoString =  nikSharpenInfoString.Replace( "%SharpenLevel", Zap );

			// Paper type.
			nikSharpenInfoString =  nikSharpenInfoString.Replace( "%EpsonPaper", paperType.ToString()	 );		// 0-5 (Texture & Fine Art, Canvas, Plain, Matte, Luster, Glossy)

			// Printer resolution.
			nikSharpenInfoString =  nikSharpenInfoString.Replace( "%EpsonQuality", printerRes.ToString() );		// 0-6 (1440x720, 1440x1440, 2880x1440, 2880x720, 5760x1440, 1440x1440, 720x720)

			// Eye Distance.
			nikSharpenInfoString =  nikSharpenInfoString.Replace( "%EyeDistance", eyeDistance.ToString() );	// 0-5 (Auto, <2', 2-5', 4-8', 6-10', 10'+)
	
			ad.PutString( nikSharpenID, nikSharpenInfoString );
	
			// For some !$#@*! reason psDisplayAllDialogs does not work so we say error dlgs only which does.
			theApp.ExecuteAction( nik, ad, PsDialogModes.psDisplayErrorDialogs );
		}

		public void USMSharpen( SharpenOptionsInfo options )
		{
			if ( options.GetType() != typeof( UsmSharpenInfo ) )
				throw( new ApplicationException( "Wrong type in argument to: USMSharpen!" ) );

			int		amount		=  (int) ( (UsmSharpenInfo) options ).Amount;
			double	radius		=  (int) ( (UsmSharpenInfo) options ).Radius;
			int		threshhold	=  (int) ( (UsmSharpenInfo) options ).Threshhold;

			// Check for valid data.
			if ( amount < 1		|| amount > 500	||
				  radius < 0.1		|| radius > 250.0	||
				  threshhold < 0	|| threshhold > 250 )
				throw( new ArgumentOutOfRangeException( "One or more arguments are out of range for: USMSharpen function!" ) );

			int					usmID	=  theApp.CharIDToTypeID( "UnsM" );
			ActionDescriptor	usmAD	=  new ActionDescriptor();

			int id4814	=  theApp.CharIDToTypeID( "Amnt" );
			int id4815	=  theApp.CharIDToTypeID( "#Prc" );
			usmAD.PutUnitDouble( id4814, id4815, amount );

			int id4816	=  theApp.CharIDToTypeID( "Rds " );
			int id4817	=  theApp.CharIDToTypeID( "#Pxl" );
			usmAD.PutUnitDouble( id4816, id4817, radius );

			int id4818	=  theApp.CharIDToTypeID( "Thsh" );
			usmAD.PutInteger( id4818, threshhold );

			theApp.ExecuteAction( usmID, usmAD, PsDialogModes.psDisplayNoDialogs );
		}

		public void ConvertToProfile( string profile )
		{
			try
			{
				// Only convert if none or is different.
				if ( theApp.ActiveDocument.ColorProfileType != PsColorProfileType.psNo && 
					  theApp.ActiveDocument.ColorProfileName != profile )
				{
					theApp.ActiveDocument.ConvertProfile( profile, PsIntent.psPerceptual, true, true );
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Convert To Profile" );
			}
		}

		private void ConvertToProfile( string profile, PsIntent psIntent, bool blackPointCompensation, bool dither )
		{
			try
			{
				// Only convert if none or is different.
				if ( theApp.ActiveDocument.ColorProfileType != PsColorProfileType.psNo &&
					  theApp.ActiveDocument.ColorProfileName != profile )
				{
					theApp.ActiveDocument.ConvertProfile( profile, psIntent, blackPointCompensation, dither );
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Convert To Profile" );
			}
		}

		public void AssignProfile( string profile )
		{
			if ( profile.Length == 0 )
				return;

			try
			{
				// Only assign if none or is different.
				if ( theApp.ActiveDocument.ColorProfileType == PsColorProfileType.psNo || 
					  theApp.ActiveDocument.ColorProfileName != profile )
				{
					int					assignID	=  theApp.StringIDToTypeID( "assignProfile" );
					ActionDescriptor	assignAD	=  new ActionDescriptor();
					int					id1		=  theApp.CharIDToTypeID( "null" );
					ActionReference	ref1		=  new ActionReference();
		
					int	id2	=  theApp.CharIDToTypeID( "Dcmn" );
					int	id3	=  theApp.CharIDToTypeID( "Ordn" );
					int	id4	=  theApp.CharIDToTypeID( "Trgt" );
					ref1.PutEnumerated( id2, id3, id4 );
		
					assignAD.PutReference( id1, ref1 );
		
					if ( profile == null || profile == "" )
					{
						int	manageID	=  theApp.StringIDToTypeID( "manage" );
						assignAD.PutBoolean( manageID, true );
					}
					else
					{
						int	profileID	=	theApp.StringIDToTypeID( "profile" );
						assignAD.PutString( profileID, profile );
					}
		
					theApp.ExecuteAction( assignID, assignAD, PsDialogModes.psDisplayNoDialogs );
				}
			}

			catch( Exception ex )
			{
				MessageBox.Show( ex.Message, "Assign Profile" );
			}
		}

		public void MakeGuide( double where, bool isHorizontal )
		{
			// Make guide (in inches).
			int	id1	=	theApp.CharIDToTypeID( "Mk  " );
			ActionDescriptor	desc1	=  new ActionDescriptor();

			int	id2	=  theApp.CharIDToTypeID( "Nw  " );
			ActionDescriptor	desc2	=  new ActionDescriptor();

			int	id3	=  theApp.CharIDToTypeID( "Pstn" );
			int	id4	=  theApp.CharIDToTypeID( "#Rlt" );

			// Units are 72.0/inch.
			double	amount	=  where*72.0;

			desc2.PutUnitDouble( id3, id4, amount );

			int	id5	=  theApp.CharIDToTypeID( "Ornt" );
			int	id6	=  theApp.CharIDToTypeID( "Ornt" );
			int	id7;

			if ( isHorizontal )
				id7	=  theApp.CharIDToTypeID( "Hrzn" );
			else
				id7	=  theApp.CharIDToTypeID( "Vrtc" );

			desc2.PutEnumerated( id5, id6, id7 );

			int	id8	=  theApp.CharIDToTypeID( "Gd  " );

			desc1.PutObject( id2, id8, desc2 );

			theApp.ExecuteAction( id1, desc1, PsDialogModes.psDisplayNoDialogs );
		}

		public void MakeGuide( int where, bool isHorizontal )
		{
			// Make guide (in pixels).
			int	id1	=	theApp.CharIDToTypeID( "Mk  " );
			ActionDescriptor	desc1	=  new ActionDescriptor();

			int	id2	=  theApp.CharIDToTypeID( "Nw  " );
			ActionDescriptor	desc2	=  new ActionDescriptor();

			int	id3	=  theApp.CharIDToTypeID( "Pstn" );
			int	id4	=  theApp.CharIDToTypeID( "#Pxl" );

			desc2.PutUnitDouble( id3, id4, where );

			int	id5	=  theApp.CharIDToTypeID( "Ornt" );
			int	id6	=  theApp.CharIDToTypeID( "Ornt" );
			int	id7;

			if ( isHorizontal )
				id7	=  theApp.CharIDToTypeID( "Hrzn" );
			else
				id7	=  theApp.CharIDToTypeID( "Vrtc" );

			desc2.PutEnumerated( id5, id6, id7 );

			int	id8	=  theApp.CharIDToTypeID( "Gd  " );

			desc1.PutObject( id2, id8, desc2 );

			theApp.ExecuteAction( id1, desc1, PsDialogModes.psDisplayNoDialogs );
		}

		//////////////////////////////////////////////////////////////////////
		/// Other functions.
		/// 
		public ArrayList GetFilesToProcess( FilesInfo filesInfo )
		{
			ArrayList	filesList	=  new ArrayList();
	
			if ( filesInfo.flaggedFilesOnly || filesInfo.selectedFilesOnly )
				filesList	=  GetFilesFromFileBrowser( filesInfo.flaggedFilesOnly, filesInfo.selectedFilesOnly );
			else
			{
				if ( Directory.Exists( filesInfo.inputDir ) )
					filesList	=  new ArrayList( Directory.GetFiles( filesInfo.inputDir ) );
			}
	
			return ( filesList );
		}

		public Document OpenDoc( string filename )
		{
			try
			{
				Document	theDoc	=  theApp.Open( filename, null, null );

				return ( theDoc );
			}

			catch( Exception e )
			{
				// This will catch the user canceling out if Open displays a problem opening the file dialog.
				Console.WriteLine( e.Message );
	
				return ( null );
			}
		}

		public Size GetResizeDimensions( Document imageDoc, int minMax, ResizeType minMaxOrBoth )
		{
			Size	size	=  new Size();

			if ( minMaxOrBoth == ResizeType.MaxBoth )
			{
				if ( imageDoc.Width > imageDoc.Height )
				{	
					size.Width	=  minMax;
					size.Height	=  (size.Width/imageDoc.Width)*imageDoc.Height;
				}
				else
				{
					size.Height	=  minMax;
					size.Width	=  (size.Height/imageDoc.Height)*imageDoc.Width;
				}
			}
			else if ( minMaxOrBoth == ResizeType.MinBoth )
			{
				if ( imageDoc.Width < imageDoc.Height )
				{	
					size.Width	=  minMax;
					size.Height	=  (size.Width/imageDoc.Width)*imageDoc.Height;
				}
				else
				{
					size.Height	=  minMax;
					size.Width	=  (size.Height/imageDoc.Height)*imageDoc.Width;
				}
			}
			else if ( minMaxOrBoth == ResizeType.Height )
			{
				size.Height	=  minMax;
				size.Width	=  (size.Height/imageDoc.Height)*imageDoc.Width;
			}
			else if ( minMaxOrBoth == ResizeType.Width )
			{
				size.Width	=  minMax;
				size.Height	=  (size.Width/imageDoc.Width)*imageDoc.Height;
			}
	
			return ( size );
		}

		public void ResizeDoc( Document imageDoc, int resizeSize, ResizeType minMaxOrBoth )
		{
			// Get dimensions.
			Size	size;
	
			size	=  GetResizeDimensions( imageDoc, resizeSize, minMaxOrBoth );
	
			// Resize.
			imageDoc.ResizeImage( size.Width, size.Height, 96.0, PsResampleMethod.psBicubic );
		}

		public void AddBorder( Document imageDoc, int borderWidth, RgbColor borderColor )
		{
			// Add a border around image.
			double	newWidth		=  imageDoc.Width + 2*borderWidth;
			double	newHeight	=  imageDoc.Height + 2*borderWidth;
	
			// Save current background color.
			SolidColor	orgBackground	=  theApp.BackgroundColor;
			SolidColor	background		=  new SolidColor();

			// Set background to black (as that't what the resize canvas function uses).	
			background.RGB.Red	=  borderColor.R;
			background.RGB.Green	=  borderColor.G;
			background.RGB.Blue	=  borderColor.B;
	
			theApp.BackgroundColor.RGB	=  background.RGB;
	
			// Resize canvas.
			imageDoc.ResizeCanvas( newWidth, newHeight, PsAnchorPosition.psMiddleCenter );
	
			// Restore background color.
			theApp.BackgroundColor.RGB	=  orgBackground.RGB;
		}

		public void ResizeBackgroundLayer( Document imageDoc, double width )
		{
			// Add to existing image size.
			double	newWidth		=  width + imageDoc.Width;
			double	newHeight	=  width + imageDoc.Height;
	
			// Set active layer to 'Shadow'.
			imageDoc.ActiveLayer	=  imageDoc.ArtLayers[ "background" ];
	
			// Resize.
			imageDoc.ResizeCanvas( newWidth, newHeight, PsAnchorPosition.psMiddleCenter );
		}

		public void FillBackgroundLayer( Document imageDoc, RgbColor rgbColor )
		{
			// Set active layer to 'background'.
			imageDoc.ActiveLayer	=  imageDoc.ArtLayers[ "background" ];
	
			// Save current foreground color.
			SolidColor	orgForeground	=  theApp.ForegroundColor;
			SolidColor	newForeground	=  new SolidColor();

			// Set foreground to white.	
			newForeground.RGB.Red	=  rgbColor.R;
			newForeground.RGB.Green	=  rgbColor.G;
			newForeground.RGB.Blue	=  rgbColor.B;
	
			theApp.ForegroundColor.RGB	=  newForeground.RGB;

			// Fill it with foreground color.
			DPoint	point	=  new DPoint();
	
			Fill( point, 100.0, 96, true, true, false );
	
			// Restore foreground color.
			theApp.ForegroundColor	=  orgForeground;
		}

		public void SliceUpWebImage( Document imageDoc, string outputDir, int borderWidth, string prefix )
		{
			//      |                           |
			//      |                           |
			//  --- + ------------------------- + -----
			//      |                           |
			//      |                           |
			//      |                           |
			//      |                           |
			//      |                           |
			//      |                           |
			//      |                           |
			//      |                           |
			//  --- + ------------------------- + -----
			//      |                           |
			//      |                           |
			//
			try
			{
				// Get the bounds of the 'Photo' layer
				Bounds	boundsPhoto	=  new Bounds( imageDoc.ArtLayers[ "Photo" ].Bounds as Array );
		
				// Get the four corners that we want (minus the border).
				double	leftX		=	boundsPhoto[ 0 ] + borderWidth;
				double	topY		=	boundsPhoto[ 1 ] + borderWidth;
				double	rightX	=	boundsPhoto[ 2 ] - borderWidth;
				double	btmY		=	boundsPhoto[ 3 ] - borderWidth;
		
				// Get the entire document width and height.
				double	docWidth		=  imageDoc.Width;
				double	docHeight	=  imageDoc.Height;

				// Specify the names of the regions.
				StringCollection	regionNamesAr	=  new StringCollection();
		
				regionNamesAr.Add( "TopLeft" );
				regionNamesAr.Add( "TopCntr" );
				regionNamesAr.Add( "TopRight" );
				regionNamesAr.Add( "MidLeft" );
				regionNamesAr.Add( "MidRight" );
				regionNamesAr.Add( "BtmLeft" );
				regionNamesAr.Add( "BtmCntr" );
				regionNamesAr.Add( "BtmRight" );
			
				// Create the 8 regions.
				Bounds[]	regionsAr	=  new Bounds[8];
				double[]	corners1		=  new double[4] {0,			0,		leftX,		topY};
				double[]	corners2		=  new double[4] {leftX,	0,    rightX,		topY};
				double[]	corners3		=  new double[4] {rightX,	0,    docWidth,	topY};
				double[]	corners4		=  new double[4] {0,			topY, leftX,		btmY};
				double[]	corners5		=  new double[4] {rightX,	topY, docWidth,	btmY};
				double[]	corners6		=  new double[4] {0,			btmY, leftX,		docHeight};
				double[]	corners7		=  new double[4] {leftX,	btmY, rightX,		docHeight};
				double[]	corners8		=  new double[4] {rightX,	btmY, docWidth,	docHeight};

				regionsAr[ 0 ]	=  new Bounds( corners1 );
				regionsAr[ 1 ]	=  new Bounds( corners2 );
				regionsAr[ 2 ]	=  new Bounds( corners3 );
				regionsAr[ 3 ]	=  new Bounds( corners4 );
				regionsAr[ 4 ]	=  new Bounds( corners5 );
				regionsAr[ 5 ]	=  new Bounds( corners6 );
				regionsAr[ 6 ]	=  new Bounds( corners7 );
				regionsAr[ 7 ]	=  new Bounds( corners8 );
		
				// Now loop through and create 8 files from the eight regions.
				for ( int i= 0;  i< 8;  i++ )
				{
					// Make sure it's the active document.
					theApp.ActiveDocument	=  imageDoc;
			
					// Select (with Replace).
					SelectRectArea( regionsAr[ i ], PsSelectionType.psReplaceSelection, 0.0 );
			
					// Copy Merged (copy all layers).
					imageDoc.Selection.Copy( true );
			
					// New document with proper dimensions.
					Document	newDoc	=  theApp.Documents.Add( regionsAr[ i ][ 2 ]-regionsAr[ i ][ 0 ], regionsAr[ i ][ 3 ]-regionsAr[ i ][ 1 ],
																			 imageDoc.Resolution, regionNamesAr[ i ], PsNewDocumentMode.psNewRGB, PsDocumentFill.psWhite, 1.0,
																			 16, null );
																
					// Turn off color management (so we can paste without color conversions).
					newDoc.ColorProfileType	=  PsColorProfileType.psNo;
			
					// Paste from clipboard.
					theApp.ActiveDocument	=  newDoc;
			
					newDoc.Paste( false );

					string	filename	=  prefix+"_"+regionNamesAr[ i ] + ".gif";

					SaveAsGIF( newDoc, outputDir, filename );
			
					// Close.
					newDoc.Close( PsSaveOptions.psDoNotSaveChanges );
				}
	
				// Reactivate original image and deselect.
				theApp.ActiveDocument	=  imageDoc;
				imageDoc.Selection.Deselect();
			}
	
			catch( Exception e )
			{
				Console.WriteLine( "Exception in SliceUpWebImage:"+e.Message );
			}
		}

		public void MakeFrame( Document imageDoc, FrameInfo frameInfo )
		{
			// Add border.
			if ( frameInfo.borderWidth > 0 )
				AddBorder( imageDoc, frameInfo.borderWidth, frameInfo.borderColor );

			// Do shadow and background.
			if ( frameInfo.borderWidth > 0 )
			{
				// Rename background to "Photo" (note that numeric indices start at 1).
				imageDoc.ArtLayers[ 1 ].Name	=  "Photo";

				// Create 2 new layers and name them.
				ArtLayer	newLayer;

				newLayer			=  imageDoc.ArtLayers.Add();
				newLayer.Name	=  "Shadow";

				newLayer			=  imageDoc.ArtLayers.Add();
				newLayer.Name	=  "background";

				// Arrange the layers.
				imageDoc.ArtLayers[ "Photo" ].Move( imageDoc.ArtLayers[ "Shadow" ], PsElementPlacement.psPlaceBefore );
				imageDoc.ArtLayers[ "background" ].Move( imageDoc.ArtLayers[ "Shadow" ], PsElementPlacement.psPlaceAfter );

				// Resize 'background' layer.
				ResizeBackgroundLayer( imageDoc, 2.0*frameInfo.backgroundWidth );

				// Fill the 'background' layer.
				FillBackgroundLayer( imageDoc, new RgbColor() );

				// Make the 'background' layer the Background layer.
				MakeCurrentLayerBackground();

				// Make selection the transparency channel of the 'Photo' layer.
				SelectLayerTransparencyChannel( "Photo", false );

				// Set active layer to be 'Shadow'
				imageDoc.ActiveLayer	=  imageDoc.ArtLayers[ "Shadow" ];

				// Contract selection by 1 pixel.
				imageDoc.Selection.Contract( frameInfo.borderWidth );

				// Now fill 'Shadow' layer with shadow color.
				EditFill( frameInfo.shadowColor, 100.0, "", false );

				// Deselect.
				imageDoc.Selection.Deselect();

				// Get the active layer.
				ArtLayer	activelayer	=  (ArtLayer) imageDoc.ActiveLayer;

				// Gaussian Blur the black on the 'Shadow' layer.
				activelayer.ApplyGaussianBlur( frameInfo.shadowBlur );

				// Move the 'Shadow' layer down and to the right.
				float	horz	=  frameInfo.shadowRight ?  frameInfo.shadowWidth : -frameInfo.shadowWidth;
				float	vert	=  frameInfo.shadowDown  ?  frameInfo.shadowWidth : -frameInfo.shadowWidth;
		
				activelayer.Translate( horz, vert );

				// And make it softer by reducing the opacity.
				activelayer.Opacity	=  frameInfo.shadowSoftness;

				// Select 'Photo' and 'Shadow' transparency channels.
				SelectLayerTransparencyChannel( "Photo", false );
				SelectLayerTransparencyChannel( "Shadow", true );
		
				// Grow the selection by the background width.
				imageDoc.Selection.Grow( frameInfo.backgroundWidth, false ); 

				// Crop.
				Crop();

				// And deselect.
				imageDoc.Selection.Deselect();

				// Now set active layer to 'Background' and fill with web page color.
				imageDoc.ActiveLayer	=  imageDoc.ArtLayers[ "Background" ];

				FillBackgroundLayer( imageDoc, frameInfo.backgroundColor );
		
				Bounds	boundsPhoto	=  new Bounds( (Array) imageDoc.ArtLayers[ "Photo" ].Bounds );
		
				// Get the four corners that we want (minus the border).
				double	leftX		=	boundsPhoto[ 0 ] + frameInfo.borderWidth;
				double	topY		=	boundsPhoto[ 1 ] + frameInfo.borderWidth;
				double	rightX	=	boundsPhoto[ 2 ] - frameInfo.borderWidth;
				double	btmY		=	boundsPhoto[ 3 ] - frameInfo.borderWidth;
		
				// Get the entire document width and height.
				double	docWidth		=  imageDoc.Width;
				double	docHeight	=  imageDoc.Height;
		
				// Get the current border widths along each side.
				double	widthLeft	=  leftX;
				double	widthTop		=  topY;
				double	widthRight	=  docWidth-rightX;
				double	widthBottom	=  docHeight-btmY;
		
				// Calculate the difference between sides.
				double	deltaH	=  Math.Abs( widthRight-widthLeft );
				double	deltaV	=  Math.Abs( widthBottom-widthTop );

				// Now expand the background layer accordingly.
				PsAnchorPosition	anchorPos;
		
				if ( widthLeft > widthRight && widthBottom > widthTop )
					anchorPos	=  PsAnchorPosition.psBottomLeft;
				else if ( widthLeft > widthRight && widthTop > widthBottom )
					anchorPos	=  PsAnchorPosition.psTopLeft;
				else if ( widthRight > widthLeft && widthBottom > widthTop )
					anchorPos	=  PsAnchorPosition.psBottomRight;
				else if ( widthRight > widthLeft && widthTop > widthBottom )
					anchorPos	=  PsAnchorPosition.psTopRight;
				else
					anchorPos	=  PsAnchorPosition.psMiddleCenter;
		
				imageDoc.ResizeCanvas( docWidth+deltaH, docHeight+deltaV, anchorPos );
		
				// Now we have the image centered.
				// Make sure that our background width is as specified.
				boundsPhoto	=  new Bounds( (Array) imageDoc.ArtLayers[ "Photo" ].Bounds );
				docWidth		=  imageDoc.Width;
				docHeight	=  imageDoc.Height;
				widthLeft	=  boundsPhoto[ 0 ] + frameInfo.borderWidth;	// width right will be the same.
				widthTop		=  boundsPhoto[ 1 ] + frameInfo.borderWidth;	// width bottom will be the same.
		
				deltaH		=  frameInfo.backgroundWidth - widthLeft;
				deltaV		=  frameInfo.backgroundWidth - widthTop;
		
				// Adjust to final size.
				imageDoc.ResizeCanvas( docWidth+2*deltaH, docHeight+2*deltaV, PsAnchorPosition.psMiddleCenter );

				// And fill in the "new" area with the background color.
				FillBackgroundLayer( imageDoc, frameInfo.backgroundColor );
			}
		}

		public string SaveAs( Document imageDoc, string outputDir, string filename, Object saveAsOptions )
		{
			if ( outputDir != "" && !Directory.Exists( outputDir ) )
			{
				// Create a Folder object.
				DirectoryInfo	theFolder	=  Directory.CreateDirectory( outputDir );
			
				if ( !Directory.Exists( outputDir ) )
					throw( new Exception( "Couldn't create path:  "+outputDir ) );
			}
		
			// Get just the file name.
			string	docName	=  "";
			string	path		=  "";

			try
			{
				docName	=  filename.Equals( "" ) ? imageDoc.Name : filename;
				path		=  imageDoc.Path;

				if ( docName == "" )
					throw( new Exception( "No document name available!" ) );
			}

			catch( Exception ex )
			{
				if ( docName == "" )
					throw( ex );
			}

			int		lastDot			=  docName.LastIndexOf( "." );
			string	fileNameNoPath	=  docName.Substring( 0, lastDot );
			int		lastSlash		=  fileNameNoPath.LastIndexOf( "/" ) > -1 ?  fileNameNoPath.LastIndexOf( "/" )  : fileNameNoPath.LastIndexOf( "\\" ) ;

			fileNameNoPath	=  fileNameNoPath.Substring( lastSlash + 1 );
		
			// Now build the complete path.
			string	extension;

			if ( saveAsOptions.GetType() == typeof( JPEGSaveOptions ) )
				extension	=  ".jpg";
			else if ( saveAsOptions.GetType() == typeof( GIFSaveOptions ) )
				extension	=  ".gif";
			else if ( saveAsOptions.GetType() == typeof( TiffSaveOptions ) )
				extension	=  ".tif";
			else if ( saveAsOptions.GetType() == typeof( PhotoshopSaveOptions ) )
				extension	=  ".psd";
			else
				throw( new Exception( "Unknown SaveAs type!" ) );

			string	pathName;
			
			if ( outputDir != "" )
				pathName	=  outputDir + fileNameNoPath + extension;
			else
				pathName	=  path + fileNameNoPath + extension;

			imageDoc.SaveAs( pathName, saveAsOptions, false, PsExtensionType.psLowercase );

			return ( pathName );
		}

		public string SaveAsPSD( Document imageDoc, string outputDir, string filename )
		{
			string savedPath	=  "";

			try
			{
				// Set up the PSD SaveAs options.
				PhotoshopSaveOptions psdSaveOptions	=  new Photoshop.PhotoshopSaveOptions();
		
				psdSaveOptions.AlphaChannels		=  true;
				psdSaveOptions.EmbedColorProfile	=  true;
				psdSaveOptions.Layers				=  true;

				// Make sure extension is present.
				if ( filename.Length > 0 && !filename.ToLower().EndsWith( ".psd" ) )
					filename	+=  ".psd";

				savedPath =  SaveAs( imageDoc, outputDir, filename, psdSaveOptions );
			}

			catch( Exception e )
			{
				MessageBox.Show( e.Message, "SaveAsPSD" );
			}

			return ( savedPath );
		}

		public string SaveAsJPEG( Document imageDoc, string outputDir, string filename, int jpegQuality )
		{
			string savedPath = "";

			try
			{
				// Set up the JPEG SaveAs options.								
				JPEGSaveOptions	jpegSaveOptions	=  new Photoshop.JPEGSaveOptions();
		
				jpegSaveOptions.EmbedColorProfile	=  true;
				jpegSaveOptions.FormatOptions			=  PsFormatOptionsType.psProgressive;
				jpegSaveOptions.Quality					=  jpegQuality;
				jpegSaveOptions.Scans					=  4;

				// Make sure extension is present.
				if ( filename.Length > 0 && !filename.ToLower().EndsWith( ".jpg" ) )
					filename	+=  ".jpg";

				savedPath =  SaveAs( imageDoc, outputDir, filename, jpegSaveOptions );
			}

			catch( Exception e )
			{
				MessageBox.Show( e.Message, "SaveAsJPEG" );
			}

			return ( savedPath );
		}

		public string SaveAsTIFF( Document imageDoc, string outputDir, string filename )
		{
			string savedPath = "";

			try
			{
				// Set up the TIFF SaveAs options.								
				TiffSaveOptions	tiffSaveOptions	=  new Photoshop.TiffSaveOptions();
		
				tiffSaveOptions.AlphaChannels			=  true;
				tiffSaveOptions.EmbedColorProfile	=  imageDoc.ColorProfileType == PsColorProfileType.psNo ?  false : true;
				tiffSaveOptions.Layers					=  true;
				tiffSaveOptions.ImageCompression		=  Photoshop.PsTiffEncodingType.psTiffLZW;
				tiffSaveOptions.InterleaveChannels	=  true;
				tiffSaveOptions.SaveImagePyramid		=  true;
				tiffSaveOptions.LayerCompression		=  PsLayerCompressionType.psZIPLayerCompression;

				//*** Make sure extension is present.
				if ( filename.Length > 0 && !filename.ToLower().EndsWith( ".tif" ) )
					filename	+=  ".tif";

				savedPath =  SaveAs( imageDoc, outputDir, filename, tiffSaveOptions );
			}

			catch( Exception e )
			{
				MessageBox.Show( e.Message, "SaveAsTIFF" );
			}

			return ( savedPath );
		}

		public string SaveAsGIF( Document imageDoc, string outputDir, string filename )
		{
			string savedPath = "";

			try
			{
				// Set up the GIF SaveAs options.								
				GIFSaveOptions	gifSaveOptions	=  new GIFSaveOptions();
		
				gifSaveOptions.Dither					=  PsDitherType.psNoDither;
				gifSaveOptions.Forced					=  PsForcedColors.psNoForced;
				gifSaveOptions.Palette					=  PsPaletteType.psExact;
				gifSaveOptions.PreserveExactColors	=  true;

				// Make sure extension is present.
				if ( filename.Length > 0 && !filename.ToLower().EndsWith( ".gif" ) )
					filename	+=  ".gif";
		
				// Save it!
				savedPath =  SaveAs( imageDoc, outputDir, filename, gifSaveOptions );
			}

			catch( Exception e )
			{
				MessageBox.Show( e.Message, "SaveAsGIF" );
			}

			return ( savedPath );
		}

		public Document CreateNewDocument( double width, double height, int resolution, string name )
		{
			return ( CreateNewDocument( width, height, resolution, name, PsDocumentFill.psWhite ) );
		}

		public Document CreateNewDocument( double width, double height, int resolution, string name, PsDocumentFill fill )
		{
			// Save preferences.
			PsUnits	originalRulerUnits	=  theApp.Preferences.RulerUnits;
			
			// Set ruler units to inches
			theApp.Preferences.RulerUnits	=  PsUnits.psInches;

			Document	newDoc	=  theApp.Documents.Add( width, height, resolution, name, PsNewDocumentMode.psNewRGB, fill, 1.0, 16, null );

			// Restore units.
			theApp.Preferences.RulerUnits	=  originalRulerUnits;

			return ( newDoc );
		}
	}
}
