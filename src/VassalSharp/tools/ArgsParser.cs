using System;
namespace VassalSharp.tools
{
	
	/// <summary> Date: Mar 13, 2003</summary>
	public class ArgsParser
	{
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.Collections.Specialized.NameValueCollection Properties
		{
			get
			{
				return props;
			}
			
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Collections.Specialized.NameValueCollection props;
		public ArgsParser(System.String[] args)
		{
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			props = new System.Collections.Specialized.NameValueCollection();
			for (int i = 0; i < args.Length; ++i)
			{
				if (args[i].StartsWith("-"))
				{
					if (i < args.Length - 1 && !args[i + 1].StartsWith("-"))
					{
						props[(System.String) args[i].Substring(1)] = (System.String) args[++i];
					}
					else
					{
						props[(System.String) args[i].Substring(1)] = (System.String) "";
					}
				}
			}
		}
	}
}