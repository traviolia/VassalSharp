using System;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
namespace VassalSharp.tools
{
	
	
	public class WarningDialog
	{
		private void  InitBlock()
		{
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Exclamation, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Exclamation, thrown, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, messageKey, args);
			return ProblemDialog.show((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, title, heading, message);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, thrown, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, key, messageKey, args);
			return ProblemDialog.showDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, key, title, heading, message);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Exclamation, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Exclamation, thrown, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, details, messageKey, args);
			return ProblemDialog.showDetails((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, details, title, heading, message);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, thrown, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, details, key, messageKey, args);
			return ProblemDialog.showDetailsDisableable((int) System.Windows.Forms.MessageBoxIcon.Exclamation, parent, thrown, details, key, title, heading, message);
		}
		private WarningDialog()
		{
			InitBlock();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		Component parent, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		Throwable thrown, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		final Component parent, 
		final Throwable thrown, 
		final String messageKey, 
		final Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > show(
		final Component parent, 
		final Throwable thrown, 
		final String title, 
		final String heading, 
		final String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Component parent, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Throwable thrown, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Component parent, 
		Throwable thrown, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDisableable(
		Component parent, 
		Throwable thrown, 
		Object key, 
		String title, 
		String heading, 
		String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Component parent, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Throwable thrown, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Component parent, 
		Throwable thrown, 
		String details, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetails(
		Component parent, 
		Throwable thrown, 
		String details, 
		String title, 
		String heading, 
		String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Component parent, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Throwable thrown, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Component parent, 
		Throwable thrown, 
		String details, 
		Object key, 
		String messageKey, 
		Object...args)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > showDetailsDisableable(
		Component parent, 
		Throwable thrown, 
		String details, 
		Object key, 
		String title, 
		String heading, 
		String message)
	}
}