using System;
using Info = VassalSharp.Info;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools
{
	
	public class BugUtils
	{
		public static System.String ErrorLog
		{
			// FIXME: move this somewhere else?
			
			get
			{
				System.String log = null;
				System.IO.StreamReader r = null;
				try
				{
					//UPGRADE_TODO: Constructor 'java.io.FileReader.FileReader' was converted to 'System.IO.StreamReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					r = new System.IO.StreamReader(new System.IO.FileInfo(Info.ConfDir.FullName + "\\" + "errorLog").FullName, System.Text.Encoding.Default);
					log = IOUtils.toString(r);
					r.Close();
				}
				catch (System.IO.IOException e)
				{
					// Don't bother logging this---if we can't read the errorLog,
					// then we probably can't write to it either.
					IOUtils.closeQuietly(r);
				}
				
				return log;
			}
			
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  sendBugReport(System.String email, System.String description, System.String errorLog, System.Exception t)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'pb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			HTTPPostBuilder pb = new HTTPPostBuilder();
			
			System.IO.Stream in_Renamed = null;
			try
			{
				/*
				final URL url = new URL("http://sourceforge.net/tracker/index.php");
				pb.setParameter("group_id", "90612");
				pb.setParameter("atid", "594231");
				pb.setParameter("func", "postadd");
				pb.setParameter("category_id", "100");
				pb.setParameter("artifact_group_id", "100");
				pb.setParameter("summary", getSummary(t));
				pb.setParameter("details", email + "\n\n" + description);
				pb.setParameter("input_file", "errorLog", errorLog);
				pb.setParameter("file_description", "the errorLog");
				pb.setParameter("submit", "SUBMIT");*/
				//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String url = "http://www.vassalengine.org/util/bug.php";
				pb.setParameter("version", Info.Version);
				pb.setParameter("email", email);
				pb.setParameter("summary", getSummary(t));
				pb.setParameter("description", description);
				pb.setParameter("log", "errorLog", errorLog);
				
				in_Renamed = pb.post(url);
				//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String result = IOUtils.toString(in_Renamed);
				
				// script should return zero on success, otherwise it failed
				try
				{
					if (System.Int32.Parse(result) != 0)
					{
						throw new System.FormatException("Bad result: " + result);
					}
				}
				catch (System.FormatException e)
				{
					throw (System.IO.IOException) new System.IO.IOException().initCause(e);
				}
				
				in_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		private static System.String getSummary(System.Exception t)
		{
			System.String summary;
			if (t == null)
			{
				summary = "Automated Bug Report";
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'tc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.String tc = t.GetType().FullName;
				summary = tc.Substring(tc.LastIndexOf('.') + 1);
				
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (t.Message != null)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					summary += (": " + t.Message);
				}
				/*
				for (StackTraceElement e : t.getStackTrace()) {
				if (e.getClassName().startsWith("VASSAL")) {
				summary += " at "+e.getClassName()+"."+e.getMethodName()+" (line "+e.getLineNumber()+")";
				break;
				}
				}*/
			}
			return summary;
		}
	}
}