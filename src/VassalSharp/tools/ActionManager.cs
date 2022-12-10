using System;
namespace VassalSharp.tools
{
	
	
	public class ActionManager
	{
		private void  InitBlock()
		{
			return map.keySet();
		}
		public static ActionManager Instance
		{
			get
			{
				return instance;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final Map < Object, Action > map = new HashMap < Object, Action >();
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'instance '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly ActionManager instance = new ActionManager();
		
		private ActionManager()
		{
			InitBlock();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Set < ? > getActionIds()
		
		public virtual SupportClass.ActionSupport getAction(System.Object id)
		{
			return map.get_Renamed(id);
		}
		
		public virtual SupportClass.ActionSupport addAction(SupportClass.ActionSupport a)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.Action.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActiongetValue_javalangString'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.ACTION_COMMAND_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionACTION_COMMAND_KEY_f'"
			return addAction(a.getValue(Action.ACTION_COMMAND_KEY), a);
		}
		
		public virtual SupportClass.ActionSupport addAction(System.Object id, SupportClass.ActionSupport a)
		{
			return map.put(id, a);
		}
		
		public virtual SupportClass.ActionSupport removeAction(System.Object id)
		{
			return map.remove(id);
		}
		
		public virtual bool isEnabled(System.Object id)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport a = map.get_Renamed(id);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.isEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionisEnabled'"
			return a != null && a.isEnabled();
		}
		
		public virtual void  setEnabled(System.Object id, bool enabled)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport a = map.get_Renamed(id);
			if (a != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
				a.setEnabled(enabled);
			}
		}
	}
}