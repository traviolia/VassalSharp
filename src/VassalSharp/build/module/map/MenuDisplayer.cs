/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton
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
using Map = VassalSharp.build.module.Map;
using Deck = VassalSharp.counters.Deck;
using EventFilter = VassalSharp.counters.EventFilter;
using GamePiece = VassalSharp.counters.GamePiece;
using KeyCommand = VassalSharp.counters.KeyCommand;
using KeyCommandSubMenu = VassalSharp.counters.KeyCommandSubMenu;
using PieceFinder = VassalSharp.counters.PieceFinder;
using Properties = VassalSharp.counters.Properties;
namespace VassalSharp.build.module.map
{
	
	public class MenuDisplayer : Buildable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPieceInStack' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPieceInStack:VassalSharp.counters.PieceInStack
		{
			public AnonymousClassPieceInStack(MenuDisplayer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MenuDisplayer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MenuDisplayer enclosingInstance;
			public MenuDisplayer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public override System.Object visitDeck(Deck d)
			{
				System.Drawing.Point pos = d.Position;
				System.Drawing.Point p = new System.Drawing.Point(pt.X - pos.X, pt.Y - pos.Y);
				if (d.Shape.IsVisible(p))
				{
					return d;
				}
				else
				{
					return null;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPopupMenuListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPopupMenuListener
		{
			public AnonymousClassPopupMenuListener(MenuDisplayer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MenuDisplayer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MenuDisplayer enclosingInstance;
			public MenuDisplayer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  popupMenuCanceled(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.map.repaint();
			}
			
			public virtual void  popupMenuWillBecomeInvisible(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.map.repaint();
			}
			
			public virtual void  popupMenuWillBecomeVisible(System.Object event_sender, System.EventArgs evt)
			{
			}
		}
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		public static System.Drawing.Font POPUP_MENU_FONT = new System.Drawing.Font("Dialog", 10, System.Drawing.FontStyle.Regular);
		
		protected internal Map map;
		protected internal PieceFinder targetSelector;
		
		public virtual void  addTo(Buildable b)
		{
			targetSelector = createTargetSelector();
			map = (Map) b;
			map.addLocalMouseListener(this);
		}
		
		/// <summary> Return a {@link PieceFinder} instance that will select a
		/// {@link GamePiece} whose menu will be displayed when the
		/// user clicks on the map
		/// </summary>
		/// <returns>
		/// </returns>
		protected internal virtual PieceFinder createTargetSelector()
		{
			return new AnonymousClassPieceInStack(this);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return doc.CreateElement(GetType().FullName);
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static System.Windows.Forms.ContextMenu createPopup(GamePiece target)
		{
			return createPopup(target, false);
		}
		
		/// <summary> </summary>
		/// <param name="target">
		/// </param>
		/// <param name="global">If true, then apply the KeyCommands globally,
		/// i.e. to all selected pieces
		/// </param>
		/// <returns>
		/// </returns>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static System.Windows.Forms.ContextMenu createPopup(GamePiece target, bool global)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			KeyCommand[] c = (KeyCommand[]) target.getProperty(VassalSharp.counters.Properties_Fields.KEY_COMMANDS);
			if (c != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < JMenuItem > commands = new ArrayList < JMenuItem >();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < KeyStroke > strokes = new ArrayList < KeyStroke >();
				
				// Maps instances of KeyCommandSubMenu to corresponding JMenu
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				HashMap < KeyCommandSubMenu, JMenu > subMenus = 
				new HashMap < KeyCommandSubMenu, JMenu >();
				// Maps name to a list of commands with that name
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				HashMap < String, ArrayList < JMenuItem >> commandNames = 
				new HashMap < String, ArrayList < JMenuItem >>();
				
				for (int i = 0; i < c.Length; ++i)
				{
					c[i].Global = global;
					System.Windows.Forms.KeyEventArgs stroke = c[i].KeyStroke;
					System.Windows.Forms.MenuItem item = null;
					if (c[i] is KeyCommandSubMenu)
					{
						System.Windows.Forms.MenuItem subMenu = new System.Windows.Forms.MenuItem(c[i].LocalizedMenuText);
						subMenu.Font = POPUP_MENU_FONT;
						subMenus.put((KeyCommandSubMenu) c[i], subMenu);
						item = subMenu;
						commands.add(item);
						strokes.add(new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) '\x0000'));
					}
					else
					{
						if (strokes.contains(stroke))
						{
							System.Windows.Forms.MenuItem command = commands.get_Renamed(strokes.indexOf(stroke));
							//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.getAction' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtongetAction'"
							SupportClass.ActionSupport action = command.getAction();
							if (action != null)
							{
								//UPGRADE_ISSUE: Method 'javax.swing.Action.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActiongetValue_javalangString'"
								//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.getAction' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtongetAction'"
								//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
								System.String commandName = (System.String) command.getAction().getValue(Action.NAME);
								if (commandName == null || commandName.Length < c[i].Name.Length)
								{
									item = new System.Windows.Forms.MenuItem();
									item.Text = c[i].LocalizedMenuText;
									item.Click += new System.EventHandler(c[i].actionPerformed);
									SupportClass.CommandManager.CheckCommand(item);
									item.Font = POPUP_MENU_FONT;
									item.Enabled = c[i].isEnabled();
									commands.set_Renamed(strokes.indexOf(stroke), item);
								}
							}
						}
						else
						{
							strokes.add(stroke != null?stroke:new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) '\x0000'));
							item = new System.Windows.Forms.MenuItem();
							item.Text = c[i].LocalizedMenuText;
							item.Click += new System.EventHandler(c[i].actionPerformed);
							SupportClass.CommandManager.CheckCommand(item);
							item.Font = POPUP_MENU_FONT;
							item.Enabled = c[i].isEnabled();
							commands.add(item);
						}
					}
					if (c[i].Name != null && c[i].Name.Length > 0 && item != null)
					{
						//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
						if (l == null)
						{
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							l = new ArrayList < JMenuItem >();
							commandNames.put(c[i].Name, l);
						}
						l.add(item);
					}
				}
				
				// Move commands from main menu into submenus
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(java.util.Map.Entry < KeyCommandSubMenu, JMenu > e: 
				subMenus.entrySet())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'menuCommand '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					KeyCommandSubMenu menuCommand = e.getKey();
					//UPGRADE_NOTE: Final was removed from the declaration of 'subMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.MenuItem subMenu = e.getValue();
					
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
				
				// Promote contents of a single submenu [Removed as per Bug 4775]
				// if (commands.size() == 1) {
				//   if (commands.get(0) instanceof JMenu) {
				//     JMenu submenu = (JMenu) commands.get(0);
				//     commands.remove(submenu);
				//     for (int i = 0; i < submenu.getItemCount(); i++) {
				//       commands.add(submenu.getItem(i));
				//     }
				//   }
				// }
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(JMenuItem item: commands)
				{
					popup.add(item);
				}
			}
			return popup;
		}
		
		public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (e.isMetaDown())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece p = map.findPiece(new System.Drawing.Point(e.X, e.Y), targetSelector);
				if (p != null)
				{
					EventFilter filter = (EventFilter) p.getProperty(VassalSharp.counters.Properties_Fields.SELECT_EVENT_FILTER);
					if (filter == null || !filter.rejectEvent(event_sender, e))
					{
						//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
						System.Windows.Forms.ContextMenu popup = createPopup(p, true);
						System.Drawing.Point pt = map.componentCoordinates(new System.Drawing.Point(e.X, e.Y));
						//UPGRADE_NOTE: Some methods of the 'javax.swing.event.PopupMenuListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
						popup.Popup += new System.EventHandler(new AnonymousClassPopupMenuListener(this).popupMenuWillBecomeVisible);
						// It is possible for the map to close before the menu is displayed
						if (map.getView().isShowing())
						{
							popup.show(map.getView(), pt.X, pt.Y);
						}
						//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.consume' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
						e.consume();
					}
				}
			}
		}
	}
}