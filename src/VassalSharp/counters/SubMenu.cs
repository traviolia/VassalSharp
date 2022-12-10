/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary>A trait that groups menu items of other traits into a sub-menu </summary>
	public class SubMenu:Decorator, TranslatablePiece
	{
		virtual public System.String Description
		{
			get
			{
				if ("Sub-Menu".Equals(MenuName))
				{
					return "Sub-Menu";
				}
				else
				{
					return "Sub-Menu:  " + MenuName;
				}
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("SubMenu.htm");
			}
			
		}
		virtual public System.String[] Subcommands
		{
			get
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < String > l = new ArrayList < String >();
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				return l.toArray(new System.String[l.size()]);
			}
			
		}
		virtual public System.String MenuName
		{
			get
			{
				return subMenu;
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return getInner().Shape;
			}
			
		}
		public const System.String ID = "submenu;";
		private System.String subMenu;
		private KeyCommandSubMenu keyCommandSubMenu;
		//UPGRADE_NOTE: Final was removed from the declaration of 'keyCommands '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private KeyCommand[] keyCommands = new KeyCommand[1];
		
		public SubMenu():this(ID + "Sub-Menu;", null)
		{
		}
		
		public SubMenu(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public override PieceEditor getEditor()
		{
			return new Editor(this);
		}
		
		public virtual void  mySetType(System.String type)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			subMenu = st.nextToken();
			keyCommandSubMenu = new KeyCommandSubMenu(subMenu, this, this);
			keyCommandSubMenu.Commands = StringArrayConfigurer.stringToArray(st.nextToken());
			
			keyCommands[0] = keyCommandSubMenu;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return keyCommands;
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(MenuName).append(StringArrayConfigurer.arrayToString(Subcommands));
			return ID + se.Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return getInner().boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			getInner().draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return getInner().getName();
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(MenuName, "Sub Menu Name");
		}
		
		public class Editor : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return panel;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(nameConfig.ValueString).append(commandsConfig.ValueString);
					return VassalSharp.counters.SubMenu.ID + se.Value;
				}
				
			}
			private StringConfigurer nameConfig;
			private StringArrayConfigurer commandsConfig;
			private System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
			
			public Editor(SubMenu p)
			{
				nameConfig = new StringConfigurer(null, "Menu name:  ", p.MenuName);
				commandsConfig = new StringArrayConfigurer(null, "Sub-commands", p.Subcommands);
				
				panel.setLayout(new MigLayout("fill", "[]rel[]"));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				panel.Controls.Add(nameConfig.Controls);
				nameConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				nameConfig.Controls.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				panel.Controls.Add(commandsConfig.Controls);
				commandsConfig.Controls.Dock = new System.Windows.Forms.DockStyle();
				commandsConfig.Controls.BringToFront();
			}
		}
	}
}