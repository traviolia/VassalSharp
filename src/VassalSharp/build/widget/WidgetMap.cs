using System;
using Map = VassalSharp.build.module.Map;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
namespace VassalSharp.build.widget
{
	
	public class WidgetMap:Map
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassVisibilityCondition : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition(WidgetMap enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(WidgetMap enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private WidgetMap enclosingInstance;
			public WidgetMap Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool shouldBeVisible()
			{
				return false;
			}
		}
		virtual public System.Windows.Forms.Control View
		{
			get
			{
				return base.View;
			}
			
		}
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		virtual public System.Windows.Forms.ScrollableControl Scroll
		{
			/*
			* Make the scroll pane accessible to the widget
			*/
			
			get
			{
				return scroll;
			}
			
		}
		public WidgetMap():base()
		{
		}
		
		/*
		* Minimal setup - remove all docking and toolbar setup
		*/
		public override void  setup(bool show)
		{
			if (show)
			{
				toolBar.Visible = true;
				theMap.Invalidate();
			}
			else
			{
				pieces.clear();
				boards.clear();
				toolBar.Visible = false;
			}
		}
		
		/// <summary> Widget maps are always undocked</summary>
		public virtual bool shouldDockIntoMainWindow()
		{
			return false;
		}
		
		/*
		* Hide options relating to toolbar buttons
		*/
		public override VisibilityCondition getAttributeVisibility(System.String name)
		{
			if (USE_LAUNCH_BUTTON.Equals(name) || BUTTON_NAME.Equals(name) || ICON.Equals(name) || HOTKEY.Equals(name))
			{
				return new AnonymousClassVisibilityCondition(this);
			}
			else
			{
				return base.getAttributeVisibility(name);
			}
		}
	}
}