using System;
using Info = VassalSharp.Info;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.version
{
	
	
	public class VersionUtils
	{
		public static VassalVersion Release
		{
			get
			{
				if (release == null)
					release = getVersion(baseURL + currentRelease);
				return release;
			}
			
		}
		public static VassalVersion Beta
		{
			get
			{
				if (beta == null)
					beta = getVersion(baseURL + currentBeta);
				return beta;
			}
			
		}
		protected internal VersionUtils()
		{
		}
		
		private const System.String baseURL = "http://www.vassalengine.org/util/";
		
		private const System.String currentRelease = "current-release";
		private const System.String currentBeta = "current-beta";
		private const System.String bugCheck = "check-version-bug.php?version=";
		
		
		private static VassalVersion release = null;
		private static VassalVersion beta = null;
		
		private static VassalVersion getVersion(System.String url)
		{
			System.IO.Stream in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
				in_Renamed = System.Net.WebRequest.Create(new System.Uri(url)).GetResponse().GetResponseStream();
				//UPGRADE_NOTE: Final was removed from the declaration of 'version '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				VassalVersion version = new VassalVersion(IOUtils.toString(in_Renamed).trim());
				in_Renamed.Close();
				return version;
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/*
		public static boolean isReportable(String version)
		throws IOException, NumberFormatException {
		
		InputStream in = null;
		try {
		in = new URL(baseURL + bugCheck + version).openStream();
		final int result = Integer.parseInt(IOUtils.toString(in));
		in.close();
		
		switch (result) {
		case 0: return false;
		case 1: return true;
		default:
		throw new NumberFormatException("bad return value: " + result);
		}
		}
		finally {
		IOUtils.closeQuietly(in);
		}
		}*/
		
		public static bool isCurrent(VassalVersion version)
		{
			// a version is current if it would update to itself
			return version.Equals(update(version));
		}
		
		public static VassalVersion update(VassalVersion version)
		{
			VassalVersion current = VersionUtils.Release;
			switch (sgn(version.compareTo(current)))
			{
				
				case - 1:  // version is older than the current release
					return current;
				
				case 0:  // version is the current release
					return version;
				
				case 1:  // version is newer than the current release
					{
						current = VersionUtils.Beta;
						switch (sgn(version.compareTo(current)))
						{
							
							case - 1:  // version is older than the current beta
								return current;
							
							case 0: 
							// version is the current beta
							case 1:  // version is newer than the current beta
								return version;
							}
					}
					break;
				}
			
			throw new System.SystemException();
		}
		
		private static int sgn(int i)
		{
			return i < 0?- 1:(i > 0?1:0);
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'v '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			VassalVersion v = new VassalVersion(Info.Version);
			System.Console.Out.WriteLine(v.ToString() + " is current? " + isCurrent(v));
		}
	}
}