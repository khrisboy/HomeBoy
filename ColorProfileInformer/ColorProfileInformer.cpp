// This is the main DLL file.

#include "stdafx.h"

#include <string>
#include <iostream>
#include "icm.h"

#include "ColorProfileInformer.h"

using namespace ColorInformer;
using namespace std;

namespace
{
	void MarshalString( System::String^ s, std::string& os )
	{
		using namespace System::Runtime::InteropServices;

		const char* chars = (const char*)( Marshal::StringToHGlobalAnsi( s ) ).ToPointer();
	   
		os =  chars;
	   
		Marshal::FreeHGlobal( IntPtr( (void*) chars ) );
	}

	void MarshalString( std::string& os, System::String^& s )
	{
		using namespace System::Runtime::InteropServices;

		s	=  Marshal::PtrToStringAnsi( (IntPtr) const_cast<char *>( os.c_str() ) );
	}

	HPROFILE OpenColorProfile( const string& filename )
	{
		PROFILE		profile;
		HPROFILE	hProfile	=  NULL;
		size_t		size		=  filename.size();
		const char*	fileBuffer	=  filename.c_str();

		// Try to open the color profile that is specified.
		ZeroMemory( &profile, sizeof( PROFILE ) );

		profile.dwType			=  PROFILE_FILENAME;
		profile.pProfileData	=  (PVOID) fileBuffer;
		profile.cbDataSize		=  (DWORD) size+1;

		// Try to open the color profile that is specified.
		hProfile	=  OpenColorProfile( &profile, PROFILE_READ, FILE_SHARE_READ, OPEN_EXISTING );

		return ( hProfile );
	}
}

ColorProfileInformer::ColorProfileInformer()
{
}

String^	ColorProfileInformer::GetProfileName( String^ filename )
{
	string profileName;

	// Always start out empty.
	profileName =  "";
	
	BYTE* pBuffer	=  0;

	// Must have something there!
	string	profileFilename;
	
	MarshalString( filename, profileFilename );

	if ( profileFilename == "" )
	{
		profileName =  "Error:  Blank filename!";
		goto ABORT;
	}

	// See if we can open the color profile.
	HPROFILE	hProfile	=  OpenColorProfile( profileFilename );

	if ( !hProfile )
	{
		profileName =  "Error calling:  OpenColorProfile()!";
		goto ABORT;
	}

	// Get the profile header information.
	PROFILEHEADER	ph;

	ZeroMemory( &ph, sizeof( PROFILEHEADER ) );

	ph.phSize	=  sizeof( PROFILEHEADER );
	BOOL	bFlag	=  GetColorProfileHeader( hProfile, &ph );
	
	if ( !bFlag )
	{
		profileName	=  "Error calling:  GetColorProfileHeader()!";
		goto ABORT;
	}

   // Query the number of tagged elements in the opened profile.
	DWORD dwNumElements = 0;
	bFlag =  GetCountColorProfileElements( hProfile, &dwNumElements );
	
	if ( !bFlag )
	{
		profileName	=  "Error calling:  GetCountColorProfileElements()!";
		goto ABORT;
	}

	// For every 1-based index.
	BOOL		bReference;
	TAGTYPE	tt;
	DWORD		dwLen;

	for ( DWORD dw= 1;  dw<= dwNumElements;  dw++ )
	{
		// Get the tag name.
		bFlag	=  GetColorProfileElementTag( hProfile, dw, &tt );

		if ( !bFlag )
		{
			profileName	=  "Error calling:  GetColorProfileElementTag()!";
			goto ABORT;
		}

		// Determine the space that is needed for the tag data.
		bFlag	=  GetColorProfileElement( hProfile, tt, 0, &dwLen, NULL, &bReference );
		
		if ( dwLen == 0 || ( GetLastError() != ERROR_INSUFFICIENT_BUFFER ) )
		{
			profileName	=  "Error calling:  GetColorProfileElement()!";
			goto ABORT;
		}

		// Allocate space for the tag data.
		pBuffer	= ( BYTE*)(void *) GlobalAlloc( GPTR, dwLen );
		
		if ( pBuffer == NULL )
		{
			profileName	=  "Error calling:  GlobalAlloc()!";
			goto ABORT;
		}

		// Get the tag data.
		bFlag	=  GetColorProfileElement( hProfile, tt, 0, &dwLen, pBuffer, &bReference );

		if ( !bFlag )
		{
			profileName	=  "Error calling:  GetColorProfileElement()!";
			goto ABORT;
		}

		// For text and desc tags, print the data in a somewhat readable way.
		int indexStart	        =  -1;
		string::size_type count =  0;

		string	tag( (LPCSTR) pBuffer );

		if ( tag.find( "desc" ) == 0 )
		{
			// The size is stored in the 3rd word (9-12) and in the opposite Endian form so...
			unsigned long size =  0;
			int offset	       =  8;
			BYTE buf[ 4 ];

			buf[ 3 ] =  pBuffer[ offset ];
			buf[ 2 ] =  pBuffer[ offset+1 ];
			buf[ 1 ] =  pBuffer[ offset+2 ];
			buf[ 0 ] =  pBuffer[ offset+3 ];

			memcpy( &size, buf, 4 ); 

			indexStart =  12;
			count      =  size;

			string	name( (LPCSTR) ( pBuffer+indexStart ), count );

			profileName	=  name;

			break;
		}

		// Free the tag data buffer.
		GlobalFree( pBuffer );
		pBuffer	=  NULL;
  }

ABORT:
	if ( hProfile )
	{
		CloseColorProfile( hProfile );
	}
	else
	{
		//LPVOID lpMsgBuf;

		//FormatMessage( FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
		//				NULL,
		//				GetLastError(),
		//				0, // Default language
		//				(LPTSTR) &lpMsgBuf,
		//				0,
		//				NULL 
		//	);

		//// Display the string.
		//MessageBox( NULL, (LPCTSTR)lpMsgBuf, (LPCTSTR)"Error in GetProfileName", MB_OK | MB_ICONINFORMATION );
		//
		//// Free the buffer.
		//LocalFree( lpMsgBuf );
	}

	if ( pBuffer )
	{
		GlobalFree( pBuffer );
	}

	String^	profile	=  gcnew String( profileName.c_str() );

	return ( profile );
}