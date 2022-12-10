/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.configure
{
	
	/// <summary> Action to edit the Properties of a component</summary>
	[Serializable]
	public class EditPropertiesAction:SupportClass.ActionSupport
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(EditPropertiesAction enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(EditPropertiesAction enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EditPropertiesAction enclosingInstance;
			public EditPropertiesAction Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosed(System.Object event_sender, System.EventArgs e)
			{
				openWindows.remove(Enclosing_Instance.target);
				if (Enclosing_Instance.tree != null && Enclosing_Instance.target is ConfigureTree.Mutable)
				{
					Enclosing_Instance.tree.nodeUpdated(Enclosing_Instance.target);
				}
			}
		}
		private const long serialVersionUID = 1L;
		
		protected internal Configurable target;
		protected internal HelpWindow helpWindow;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static Map < Configurable, PropertiesWindow > openWindows = 
		new HashMap < Configurable, PropertiesWindow >();
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		protected internal System.Windows.Forms.Form dialogOwner;
		protected internal ConfigureTree tree;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public EditPropertiesAction(Configurable target, HelpWindow helpWindow, System.Windows.Forms.Form dialogOwner):base(Resources.getString("Editor.ModuleEditor.properties"))
		{ //$NON-NLS-1$
			this.helpWindow = helpWindow;
			this.target = target;
			this.dialogOwner = dialogOwner;
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(target.Configurer != null);
		}
		
		/*
		* Used by ConfigureTree where Configurers may change the children of a node
		*/
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public EditPropertiesAction(Configurable target, HelpWindow helpWindow, System.Windows.Forms.Form dialogOwner, ConfigureTree tree):this(target, helpWindow, dialogOwner)
		{
			this.tree = tree;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			PropertiesWindow w = openWindows.get_Renamed(target);
			if (w == null)
			{
				w = new PropertiesWindow(dialogOwner, false, target, helpWindow);
				//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				w.Closed += new System.EventHandler(new AnonymousClassWindowAdapter(this).windowClosed);
				openWindows.put(target, w);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				w.Visible = true;
				if (tree != null)
				{
					tree.notifyStateChanged(true);
				}
			}
			w.BringToFront();
		}
	}
}