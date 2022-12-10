/*
* $Id$
*
* Copyright (c) 2000-2005 by Rodney Kinney, Brent Easton
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using NamedKeyStrokeArrayConfigurer = VassalSharp.configure.NamedKeyStrokeArrayConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using PropertyExpressionConfigurer = VassalSharp.configure.PropertyExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> RestrictCommands
	/// Restrict the availability of Key Commands, depending on a
	/// Property Match String.
	/// - Variable list of Key Commands to restrict
	/// - Disable or Invisible
	/// 
	/// </summary>
	public class RestrictCommands:Decorator, EditablePiece
	{
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				System.String s = "Restrict Commands";
				if (name.Length > 0)
				{
					s += (" - " + name);
				}
				return s;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("RestrictCommands.htm");
			}
			
		}
		
		public const System.String ID = "hideCmd;";
		protected internal const System.String HIDE = "Hide";
		protected internal const System.String DISABLE = "Disable";
		
		protected internal System.String name = "";
		protected internal PropertyExpression propertyMatch = new PropertyExpression();
		protected internal System.String action = HIDE;
		protected internal NamedKeyStroke[] watchKeys = new NamedKeyStroke[0];
		
		public RestrictCommands():this(ID, null)
		{
		}
		
		public RestrictCommands(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(name).append(action).append(propertyMatch.Expression).append(NamedKeyStrokeArrayConfigurer.encode(watchKeys));
			
			return ID + se.Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		/*
		* Cancel execution of watched KeyStrokes
		*/
		public override Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			if (!matchesFilter())
			{
				return base.keyEvent(stroke);
			}
			else
			{
				for (int j = 0; j < watchKeys.Length; j++)
				{
					if (watchKeys[j].Equals(stroke))
					{
						return null;
					}
				}
			}
			return base.keyEvent(stroke);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'getKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] getKeyCommands()
		{
			KeyCommand[] commands = base.getKeyCommands();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < KeyCommand > newCommands = 
			new ArrayList < KeyCommand >(commands.length);
			if (matchesFilter())
			{
				for (int i = 0; i < commands.Length; i++)
				{
					bool matches = false;
					for (int j = 0; j < watchKeys.Length && !matches; j++)
					{
						matches = (watchKeys[j].Equals(commands[i].KeyStroke));
					}
					if (matches)
					{
						if (action.Equals(DISABLE))
						{
							KeyCommand newCommand = new KeyCommand(commands[i]);
							newCommand.setEnabled(false);
							newCommands.add(newCommand);
						}
					}
					else
					{
						newCommands.add(commands[i]);
					}
				}
				commands = newCommands.toArray(new KeyCommand[newCommands.size()]);
			}
			return commands;
		}
		
		protected internal virtual bool matchesFilter()
		{
			GamePiece outer = Decorator.getOutermost(this);
			if (!propertyMatch.Null)
			{
				if (!propertyMatch.accept(outer))
				{
					return false;
				}
			}
			return true;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			name = st.nextToken("");
			action = st.nextToken(HIDE);
			propertyMatch.Expression = st.nextToken("");
			
			System.String keys = st.nextToken("");
			if (keys.IndexOf(',') > 0)
			{
				watchKeys = NamedKeyStrokeArrayConfigurer.decode(keys);
			}
			else
			{
				watchKeys = new NamedKeyStroke[keys.Length];
				for (int i = 0; i < watchKeys.Length; i++)
				{
					watchKeys[i] = new NamedKeyStroke(keys[i], (int) System.Windows.Forms.Keys.Control);
				}
			}
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return box;
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
					se.append(name.ValueString).append((actionOption.SelectedIndex == 0)?VassalSharp.counters.RestrictCommands.HIDE:VassalSharp.counters.RestrictCommands.DISABLE).append(propertyMatch.ValueString).append(watchKeys.ValueString);
					return VassalSharp.counters.RestrictCommands.ID + se.Value;
				}
				
			}
			
			protected internal StringConfigurer name;
			protected internal PropertyExpressionConfigurer propertyMatch;
			protected internal NamedKeyStrokeArrayConfigurer watchKeys;
			protected internal System.Windows.Forms.ComboBox actionOption;
			protected internal System.Windows.Forms.Panel box;
			
			public Ed(RestrictCommands piece)
			{
				
				box = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				box.setLayout(new BoxLayout(box, BoxLayout.Y_AXIS));
				
				name = new StringConfigurer(null, "Description:  ", piece.name);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(name.Controls);
				
				actionOption = new System.Windows.Forms.ComboBox();
				actionOption.Items.Add(VassalSharp.counters.RestrictCommands.HIDE);
				actionOption.Items.Add(VassalSharp.counters.RestrictCommands.DISABLE);
				actionOption.SelectedIndex = (piece.action.Equals(VassalSharp.counters.RestrictCommands.HIDE))?0:1;
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Restriction:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				b.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(actionOption);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(b);
				
				propertyMatch = new PropertyExpressionConfigurer(null, "Restrict when properties match:  ", piece.propertyMatch, Decorator.getOutermost(piece));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(propertyMatch.Controls);
				
				watchKeys = new NamedKeyStrokeArrayConfigurer(null, "Restrict these Key Commands  ", piece.watchKeys);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(watchKeys.Controls);
			}
		}
	}
}