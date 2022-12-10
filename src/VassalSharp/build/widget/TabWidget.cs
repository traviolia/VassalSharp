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
using Buildable = VassalSharp.build.Buildable;
using Configurable = VassalSharp.build.Configurable;
using Widget = VassalSharp.build.Widget;
namespace VassalSharp.build.widget
{
	
	/// <summary> A Widget that corresponds to a JTabbedPane.
	/// Adding a Widget to a BoxWidget adds the child Widget's component
	/// to the JTabbedPane, setting the tab's name to the child's name
	/// (via {@link Configurable#getConfigureName})
	/// </summary>
	public class TabWidget:Widget
	{
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				String.
			}
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Tabbed Panel";
			}
			
		}
		override public System.Windows.Forms.Control Component
		{
			get
			{
				if (tab == null)
				{
					rebuild();
					tab = new System.Windows.Forms.TabControl();
					for (int i = 0; i < widgets.size(); ++i)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Widget w = widgets.get_Renamed(i);
						w.PropertyChange += new SupportClass.PropertyChangeEventHandler(this.propertyChange);
						//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
						SupportClass.TabControlSupport.AddTab(tab, w.getConfigureName(), new System.Windows.Forms.Panel());
					}
					tab.SelectedIndexChanged += new System.EventHandler(this.stateChanged);
					if (widgets.size() > 0)
					{
						//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.setSelectedIndex' was converted to 'System.Windows.Forms.TabControl.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPanesetSelectedIndex_int'"
						tab.SelectedIndex = 0;
					}
					stateChanged(null);
				}
				return tab;
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{NAME};
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{"Name:  "};
			}
			
		}
		private System.Windows.Forms.TabControl tab = null;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Widget > widgets = new ArrayList < Widget >();
		
		public TabWidget()
		{
			InitBlock();
		}
		
		public virtual void  stateChanged(System.Object event_sender, System.EventArgs e)
		{
			int index = tab.SelectedIndex;
			if (index >= 0)
			{
				tab.setComponentAt(index, widgets.get_Renamed(index).Component);
			}
		}
		
		public override void  add(Buildable b)
		{
			if (b is Widget)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Widget w = (Widget) b;
				widgets.add(w);
				if (tab != null)
				{
					tab.SelectedIndexChanged -= new System.EventHandler(this.stateChanged);
					if (widgets.size() > 1)
					{
						//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
						SupportClass.TabControlSupport.AddTab(tab, w.getConfigureName(), new System.Windows.Forms.Panel());
					}
					else
					{
						//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
						SupportClass.TabControlSupport.AddTab(tab, w.getConfigureName(), w.Component);
					}
					w.PropertyChange += new SupportClass.PropertyChangeEventHandler(this.propertyChange);
					tab.SelectedIndexChanged += new System.EventHandler(this.stateChanged);
				}
			}
			base.add(b);
		}
		
		public override void  remove(Buildable b)
		{
			if (b is Widget)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Widget w = (Widget) b;
				if (tab != null)
				{
					tab.SelectedIndexChanged -= new System.EventHandler(this.stateChanged); // prevent bad recursion
					tab.removeTabAt(widgets.indexOf(w));
					w.PropertyChange -= new SupportClass.PropertyChangeEventHandler(this.propertyChange);
					tab.SelectedIndexChanged += new System.EventHandler(this.stateChanged); // restore listener
				}
				widgets.remove(w);
			}
			base.remove(b);
		}
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			if (VassalSharp.build.Configurable_Fields.NAME_PROPERTY.Equals(evt.PropertyName))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Widget src = (Widget) event_sender;
				//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int index = widgets.indexOf(src);
				//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Object name = evt.NewValue;
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				tab.TabPages[index].Text = name == null?"":name.ToString();
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setAttribute(String name, Object value)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setConfigureName((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getConfigureName();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}