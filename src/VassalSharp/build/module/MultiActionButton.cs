using System;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Resources = VassalSharp.i18n.Resources;
using RecursionLimitException = VassalSharp.tools.RecursionLimitException;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
namespace VassalSharp.build.module
{
	
	/// <summary> Combines multiple buttons from the toolbar into a single button. Pushing the single button is equivalent to pushing
	/// the other buttons in order.
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class MultiActionButton:ToolbarMenu, RecursionLimiter.Loopable
	{
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString(Resources.DESCRIPTION), Resources.getString(Resources.BUTTON_TEXT), Resources.getString(Resources.TOOLTIP_TEXT), Resources.getString(Resources.BUTTON_ICON), Resources.getString(Resources.HOTKEY_LABEL), Resources.getString("Editor.MultiActionButton.buttons")};
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.MultiActionButton.component_type"); //$NON-NLS-1$
			}
			
		}
		virtual public System.String ComponentName
		{
			// Implement Loopable
			
			get
			{
				return getConfigureName();
			}
			
		}
		virtual public System.String ComponentTypeName
		{
			get
			{
				return ConfigureTypeName;
			}
			
		}
		
		public MultiActionButton():base()
		{
			setAttribute(BUTTON_TEXT, Resources.getString("Editor.MultiActionButton.component_type")); //$NON-NLS-1$
			setAttribute(TOOLTIP, Resources.getString("Editor.MultiActionButton.component_type")); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.putClientProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentputClientProperty_javalangObject_javalangObject'"
			launch_Renamed_Field.putClientProperty(MENU_PROPERTY, null);
		}
		
		public override void  launch()
		{
			// Pause logging to accumulate commands generated by the
			// separate toolbar buttons.
			//UPGRADE_NOTE: Final was removed from the declaration of 'mod '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule mod = GameModule.getGameModule();
			//UPGRADE_NOTE: Final was removed from the declaration of 'loggingPaused '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool loggingPaused = mod.pauseLogging();
			
			try
			{
				RecursionLimiter.startExecution(this);
				
				for (int i = 0, n = (int) menu.Controls.Count; i < n; ++i)
				{
					System.Windows.Forms.Control c = menu.Controls[i];
					if (c is System.Windows.Forms.MenuItem)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.doClick' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtondoClick'"
						((System.Windows.Forms.MenuItem) c).doClick();
					}
				}
			}
			catch (RecursionLimitException e)
			{
				RecursionLimiter.infiniteLoop(e);
			}
			finally
			{
				RecursionLimiter.endExecution();
				// If we are in control of logging, retrieve the accumulated Commands,
				// turn off pause and send the Commands to the log.
				if (loggingPaused)
				{
					mod.sendAndLog(mod.resumeLogging());
				}
			}
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("MultiActionButton.htm"); //$NON-NLS-1$
		}
	}
}