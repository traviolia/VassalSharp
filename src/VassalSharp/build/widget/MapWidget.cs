using System;
using Buildable = VassalSharp.build.Buildable;
using Widget = VassalSharp.build.Widget;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
namespace VassalSharp.build.widget
{
	
	public class MapWidget:Widget
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
				return "Map";
			}
			
		}
		override public System.Windows.Forms.Control Component
		{
			/*
			* Parent Widget has now completed building, so set up Drag Target handling if our parent is a TabWidget
			*/
			
			get
			{
				if (tab == null && parent is TabWidget)
				{
					tab = (System.Windows.Forms.TabControl) ((TabWidget) parent).Component;
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getClientProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetClientProperty_javalangObject'"
					if (tab.getClientProperty(typeof(TabSwitcher)) == null)
					{
						TabSwitcher switcher = new TabSwitcher(tab);
						//UPGRADE_ISSUE: Method 'javax.swing.JComponent.putClientProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentputClientProperty_javalangObject_javalangObject'"
						tab.putClientProperty(typeof(TabSwitcher), switcher);
						//UPGRADE_ISSUE: Method 'java.awt.Component.setDropTarget' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentsetDropTarget_javaawtdndDropTarget'"
						tab.AllowDrop = true;
						tab.DragEnter += new System.Windows.Forms.DragEventHandler(switcher.dragEnter_renamed);
						tab.DragOver += new System.Windows.Forms.DragEventHandler(switcher.dragOver_renamed);
						tab.DragLeave += new System.EventHandler(switcher.dragExit_renamed);
						tab.DragDrop += new System.Windows.Forms.DragEventHandler(switcher.drop_renamed);
						//UPGRADE_ISSUE: Constructor 'java.awt.dnd.DropTarget.DropTarget' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTarget'"
						tab.setDropTarget(new DropTarget(tab, (int) System.Windows.Forms.DragDropEffects.Move, switcher));
					}
				}
				return panel;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{"Name:  "};
			}
			
		}
		protected internal System.Windows.Forms.Panel panel;
		protected internal System.Windows.Forms.Control mapHolder;
		protected internal WidgetMap map;
		new protected internal Buildable parent;
		protected internal System.Windows.Forms.TabControl tab;
		
		public MapWidget()
		{
			InitBlock();
			panel = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			panel.setLayout(new BorderLayout());*/
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("ChartWindow.htm", "Map");
		}
		
		public override void  build(System.Xml.XmlElement e)
		{
			if (e == null)
			{
				WidgetMap map = new WidgetMap();
				map.build(null);
				map.addTo(this);
				add(map);
			}
			else
			{
				base.build(e);
			}
			/*
			* Maps must be built prior to game start, so force a rebuild immediately. Default for widgets is to defer build
			* until first call to getComponent()
			*/
			rebuild();
		}
		
		public override void  addTo(Buildable b)
		{
			base.addTo(b);
			parent = b;
		}
		
		public override void  add(Buildable b)
		{
			if (b is WidgetMap)
			{
				if (mapHolder != null)
				{
					panel.Controls.Remove(mapHolder);
					mapHolder = null;
				}
				map = (WidgetMap) b;
				mapHolder = map.getLayeredPane();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				panel.Controls.Add(mapHolder);
				mapHolder.Dock = System.Windows.Forms.DockStyle.Fill;
				mapHolder.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				panel.Controls.Add(map.ToolBar);
				map.ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
				map.ToolBar.SendToBack();
				panel.Invalidate();
			}
			base.add(b);
		}
		
		public override void  remove(Buildable b)
		{
			if (b is WidgetMap)
			{
				panel.Controls.Remove(mapHolder);
				panel.Controls.Remove(map.ToolBar);
				mapHolder = null;
			}
			base.remove(b);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String [] getAttributeNames()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ NAME
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setAttribute(String key, Object value)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setConfigureName((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String key)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getConfigureName();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	protected internal class TabSwitcher
	{
		protected internal System.Windows.Forms.TabControl tab;
		
		public TabSwitcher(System.Windows.Forms.TabControl tab)
		{
			this.tab = tab;
		}
		
		public virtual void  dragOver_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
		{
			if (tab != null)
			{
				System.Drawing.Point p = SupportClass.GetLocation(e);
				//UPGRADE_TODO: Method 'javax.swing.plaf.TabbedPaneUI.tabForCoordinate' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_ISSUE: Method 'javax.swing.JTabbedPane.getUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTabbedPanegetUI'"
				int tabNumber = tab.getUI().tabForCoordinate(tab, p.X, p.Y);
				if (tabNumber >= 0 && tabNumber != tab.SelectedIndex)
				{
					//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.setSelectedIndex' was converted to 'System.Windows.Forms.TabControl.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPanesetSelectedIndex_int'"
					tab.SelectedIndex = tabNumber;
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					tab.Refresh();
				}
			}
		}
		
		public virtual void  dragEnter_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
		{
			e.Effect = System.Windows.Forms.DragDropEffects.All;
		}
		
		public virtual void  dropActionChanged(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
		{
		}
		
		public virtual void  drop_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
		{
		}
		
		//UPGRADE_ISSUE: Class 'java.awt.dnd.DropTargetEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
		public virtual void  dragExit_renamed(System.Object event_sender, System.EventArgs e)
		{
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public WidgetMap getMap()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return map;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}