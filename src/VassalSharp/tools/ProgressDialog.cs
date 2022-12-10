using System;
namespace VassalSharp.tools
{
	
	/// <deprecated> Moved to {@link VassalSharp.tools.swing.ProgressDialog}. 
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	[Serializable]
	public class ProgressDialog:VassalSharp.tools.swing.ProgressDialog
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public ProgressDialog(System.Windows.Forms.Form parent, System.String title, System.String text):base(parent, title, text)
		{
		}
	}
}