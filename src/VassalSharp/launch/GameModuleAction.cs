/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using GameModule = VassalSharp.build.GameModule;
namespace VassalSharp.launch
{
	
	/// <summary> Utility base class for {@link GameModule}-related actions, with auxilliary
	/// actions and error reporting.
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// 
	/// </author>
	[Serializable]
	public abstract class GameModuleAction:SupportClass.ActionSupport
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.GameModuleAction'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		protected internal System.Windows.Forms.Control comp;
		protected internal bool actionCancelled;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Runnable > actions = new ArrayList < Runnable >();
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public GameModuleAction(System.String name, System.Windows.Forms.Control comp):base(name)
		{
			this.comp = comp;
		}
		
		protected internal virtual System.String getMessage(System.Exception err)
		{
			System.String msg = err.GetType().getSimpleName();
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if (err.Message != null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				msg += (":  " + err.Message);
			}
			return msg;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			try
			{
				performAction(event_sender, e);
				if (!actionCancelled)
				{
					runActions();
				}
			}
			// FIXME: review error message
			catch (System.Exception e1)
			{
				reportError(e1);
			}
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'performAction' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public abstract void  performAction(System.Object event_sender, System.EventArgs evt);
		
		protected internal virtual void  reportError(System.Exception ex)
		{
			logger.error("", ex);
			SupportClass.OptionPaneSupport.ShowMessageDialog(comp, getMessage(ex));
		}
		
		/// <summary> Add an auxilliary action to be performed after the core action. For example, closing a window after a module has
		/// been loaded
		/// 
		/// </summary>
		/// <param name="r">
		/// </param>
		public virtual void  addAction(IThreadRunnable r)
		{
			actions.add(r);
		}
		
		protected internal virtual void  runActions()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Runnable r: actions)
			{
				r.run();
			}
		}
		static GameModuleAction()
		{
			logger = LoggerFactory.getLogger(typeof(GameModuleAction));
		}
	}
}