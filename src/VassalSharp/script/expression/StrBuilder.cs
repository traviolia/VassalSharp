/*
* $Id: StrBuilder.java 7725 2011-07-31 18:51:43Z uckelman $
*
* Copyright (c) 2008-2012 Brent Easton
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

/*
* StrBuilder.
* Build a string constant. Allow user to enter a string and clean it up, add " if necessary and
* escape internal ".
*/
using System;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using ButtonFactory = VassalSharp.tools.ButtonFactory;
namespace VassalSharp.script.expression
{
	
	[Serializable]
	public class StrBuilder:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(StrBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StrBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StrBuilder enclosingInstance;
			public StrBuilder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.save();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(StrBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StrBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StrBuilder enclosingInstance;
			public StrBuilder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(StrBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StrBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StrBuilder enclosingInstance;
			public StrBuilder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs we)
			{
				we.Cancel = true;
				Enclosing_Instance.cancel();
			}
		}
		private const long serialVersionUID = 1L;
		protected internal StringConfigurer target;
		protected internal StringConfigurer entry;
		
		public StrBuilder(StringConfigurer c, System.Windows.Forms.Form parent):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtDialog_javalangString_boolean'"
			SupportClass.DialogSupport.SetDialog(this, parent, "String Builder");
			target = c;
			build("String");
		}
		
		public StrBuilder(System.Windows.Forms.Form parent, System.String string_Renamed, bool b):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtDialog_javalangString_boolean'"
			SupportClass.DialogSupport.SetDialog(this, parent, string_Renamed);
		}
		
		protected internal virtual void  build(System.String type)
		{
			setLayout(new MigLayout("fillx"));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
			p.setLayout(new MigLayout("fillx", "[grow 0]rel[grow 1]"));
			
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = type + ":";
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			p.Controls.Add(temp_Control);
			temp_Control.Dock = new System.Windows.Forms.DockStyle();
			temp_Control.BringToFront();
			entry = new StringConfigurer("", "");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			p.Controls.Add(entry.Controls);
			entry.Controls.Dock = new System.Windows.Forms.DockStyle();
			entry.Controls.BringToFront();
			
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'buttonBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel buttonBox = new JPanel(new MigLayout("", "[]rel[]rel[]"));
			//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button okButton = ButtonFactory.OkButton;
			okButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(okButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(okButton);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button cancelButton = ButtonFactory.CancelButton;
			cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(cancelButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(cancelButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			p.Controls.Add(buttonBox);
			buttonBox.Dock = new System.Windows.Forms.DockStyle();
			buttonBox.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(p);
			p.Dock = new System.Windows.Forms.DockStyle();
			p.BringToFront();
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			System.Windows.Forms.Control generatedAux9 = Parent;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Closing += new System.ComponentModel.CancelEventHandler(this.StrBuilder_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
		}
		
		protected internal virtual void  save()
		{
			System.String result = entry.ValueString;
			if (result.StartsWith("\"") && result.EndsWith("\""))
			{
				result = result.Substring(1, (result.Length - 1) - (1));
			}
			result = result.replace("\"", "\\\"");
			target.setValue("\"" + result + "\"");
			Dispose();
		}
		
		protected internal virtual void  cancel()
		{
			Dispose();
		}
		private void  StrBuilder_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
	}
}