//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="LibWrap.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace MyClasses
{
	public class LibWrap
	{
		[ DllImport( "User32.dll", EntryPoint= "MessageBeep" ) ]
		public static extern int MsgBeep( uint beepType );

		[ DllImport( "MsCMC.dll", EntryPoint= "GetProfileHeader" ) ]
		public static extern void GetProfileHeader( uint profile, ref PROFILEHEADER header );
	}

#pragma warning disable 169
	public struct CIEXYZ
	{
		long ciexyzX;
		long ciexyzY;
		long ciexyzZ;
	}

	public struct PROFILEHEADER
	{
		int     phSize;             // profile size in bytes
		int     phCMMType;          // CMM for this profile
		int     phVersion;          // profile format version number
		int     phClass;            // type of profile
		int     phDataColorSpace;   // color space of data
		int     phConnectionSpace;  // PCS
		int[]   phDateTime;         // date profile was created 3
		int     phSignature;        // magic number
		int     phPlatform;         // primary platform
		int     phProfileFlags;     // various bit settings
		int     phManufacturer;     // device manufacturer
		int     phModel;            // device model number
		int[]   phAttributes;       // device attributes 2
		int     phRenderingIntent;  // rendering intent
		CIEXYZ  phIlluminant;       // profile illuminant
		int     phCreator;          // profile creator
		char[]  phReserved;         // reserved for future use 44
	}
#pragma warning disable 169

// 		typedef long            FXPT2DOT30, FAR *LPFXPT2DOT30;
// 
// 		/* ICM Color Definitions */
// 		// The following two structures are used for defining RGB's in terms of CIEXYZ.
// 
// 		typedef struct tagCIEXYZ
// 		{
// 				FXPT2DOT30 ciexyzX;
// 				FXPT2DOT30 ciexyzY;
// 				FXPT2DOT30 ciexyzZ;
// 		} CIEXYZ;
// 		typedef CIEXYZ  FAR *LPCIEXYZ;
// 
// 		typedef struct tagICEXYZTRIPLE
// 		{
// 				CIEXYZ  ciexyzRed;
// 				CIEXYZ  ciexyzGreen;
// 				CIEXYZ  ciexyzBlue;
// 		} CIEXYZTRIPLE;
// 		typedef CIEXYZTRIPLE    FAR *LPCIEXYZTRIPLE;
//	}
}
