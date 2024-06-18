//--------------------------------------------------------------------------------
// <copyright company="C-Cubed Co." file="Logger.cs">
//   Copyright (c) C-Cubed Co., 2016. All rights reserved.
// </copyright>
// <author>Chris Coan</author>
//--------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace MyClasses
{
	public class Logger
	{
		private	static string	logFile;
		private static DateTime	sectionStartTime;
		private static DateTime stepStartTime;
		private static DateTime startTime;
		private static string   sectionTag;
		private static string   startEndTag;
		private static string   stepTag;
		private static Mutex	theMutex;

		static Logger()
		{
			logFile	=  String.Empty;

			stepStartTime	=  DateTime.MinValue;

			try
			{
				// Create a named mutex.
				theMutex	=  new Mutex( false, "Logger_Mutex" );
			}

			catch( Exception ex )
			{
				Messager.Show( ex.Message, "Mutex Creation" );
			}

		}

		public static string LogFile
		{
			get { return ( logFile ); }
			set
			{
				theMutex.WaitOne();
				logFile	=  value;
				theMutex.ReleaseMutex();
			}
		}

		public static void StartSection( string section )
		{
			theMutex.WaitOne();

			sectionStartTime	=  DateTime.Now;
			sectionTag			=  section;

			WriteLine( "" );
			WriteLine( "Start section: " + section + " at " + DateTime.Now );

			theMutex.ReleaseMutex();
		}

		public static void EndSection()
		{
			theMutex.WaitOne();

			WriteLine( "End section:   " + sectionTag + " at " + DateTime.Now );
			WriteLine( "  Elapsed time: " + ( DateTime.Now-sectionStartTime ) );

			theMutex.ReleaseMutex();
		}

		public static void Start( string eventTag )
		{
			theMutex.WaitOne();

			startTime	=  DateTime.Now;
			startEndTag	=  eventTag;

			WriteLine( "  Start at: " + DateTime.Now + ", for:  " + eventTag );

			// Reset step time.
			stepStartTime	=  DateTime.MinValue;

			theMutex.ReleaseMutex();
		}

		public static void End()
		{
			theMutex.WaitOne();

			WriteLine( "  End at:   " + DateTime.Now + ", for:  " + startEndTag );
			WriteLine( "    Elapsed time: " + ( DateTime.Now-startTime ) );

			theMutex.ReleaseMutex();
		}

		public static void Step()
		{
			theMutex.WaitOne();

			Step( "" );

			theMutex.ReleaseMutex();
		}

		public static void Step( string eventTag )
		{
			theMutex.WaitOne();

			if ( stepStartTime != DateTime.MinValue )
			{
				WriteLine( "    Step: " + stepTag + " took " + ( DateTime.Now-stepStartTime ) );
			}
			else
			{
			}

			stepTag	=  eventTag;

			if ( eventTag != String.Empty )
				stepStartTime	=  DateTime.Now;
			else
				stepStartTime	=  DateTime.MinValue;

			theMutex.ReleaseMutex();
		}

		private static void WriteLine( string line )
		{
			theMutex.WaitOne();

			if ( LogFile != String.Empty )
			{
				using ( StreamWriter sw =  new StreamWriter( LogFile, true ) ) 
				{
					sw.WriteLine( line );
				}
			}

			theMutex.ReleaseMutex();
		}
	}
}
