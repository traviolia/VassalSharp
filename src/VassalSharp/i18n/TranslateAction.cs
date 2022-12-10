/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney, Brent Easton
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
using Configurable = VassalSharp.build.Configurable;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using ConfigureTree = VassalSharp.configure.ConfigureTree;
namespace VassalSharp.i18n
{
	
	/// <summary> Action to open the Translation Window for a component</summary>
	[Serializable]
	public class TranslateAction:SupportClass.ActionSupport
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(TranslateAction enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateAction enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateAction enclosingInstance;
			public TranslateAction Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosed(System.Object event_sender, System.EventArgs e)
			{
				openWindows.remove(Enclosing_Instance.target);
			}
		}
		private const long serialVersionUID = 1L;
		
		protected internal Configurable target;
		protected internal HelpWindow helpWindow;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static Map < Configurable, TranslateWindow > openWindows = 
		new HashMap < Configurable, TranslateWindow >();
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		protected internal System.Windows.Forms.Form dialogOwner;
		protected internal ConfigureTree tree;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public TranslateAction(Configurable target, HelpWindow helpWindow, ConfigureTree tree):base(Resources.getString("Editor.ModuleEditor.translate"))
		{ //$NON-NLS-1$
			this.helpWindow = helpWindow;
			this.target = target;
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			this.dialogOwner = (System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), tree);
			this.tree = tree;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			TranslateWindow w = openWindows.get_Renamed(target);
			if (w == null)
			{
				w = new TranslateWindow(dialogOwner, false, target, helpWindow, tree);
				//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				w.Closed += new System.EventHandler(new AnonymousClassWindowAdapter(this).windowClosed);
				openWindows.put(target, w);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				w.Visible = true;
			}
			w.BringToFront();
		}
	}
}