// ColorProfileInformer.h

#pragma once

using namespace System;

namespace ColorInformer
{
	public ref class ColorProfileInformer
	{
		public:
			ColorProfileInformer();

			static String^	GetProfileName( String^ filename );
	};
}
