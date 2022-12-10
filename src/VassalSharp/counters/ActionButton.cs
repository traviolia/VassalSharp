/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using RecursionLimitException = VassalSharp.tools.RecursionLimitException;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A trait that acts like a button on a GamePiece, such that clicking on a
	/// particular area of the piece invokes a keyboard command
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class ActionButton:Decorator, EditablePiece, RecursionLimiter.Loopable
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
				return description.Length == 0?"Action Button":"Action Button - " + description;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("ActionButton.htm");
			}
			
		}
		virtual public System.String ComponentName
		{
			// Implement Loopable
			
			get
			{
				// Use inner name to prevent recursive looping when reporting errors.
				return piece.getName();
			}
			
		}
		virtual public System.String ComponentTypeName
		{
			get
			{
				return Description;
			}
			
		}
		public const System.String ID = "button;";
		protected internal NamedKeyStroke stroke;
		protected internal System.Drawing.Rectangle bounds = new System.Drawing.Rectangle();
		protected internal ButtonPusher pusher;
		protected internal System.String description = "";
		protected internal static ButtonPusher globalPusher = new ButtonPusher();
		
		public ActionButton():this(ID, null)
		{
		}
		
		public ActionButton(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
			pusher = globalPusher;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(stroke).append(bounds.X).append(bounds.Y).append(bounds.Width).append(bounds.Height).append(description);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
			if (getMap() != null)
			{
				pusher.register(getMap());
			}
			else
			{
				// Do not allow button pushes if piece is not on a map
				// pusher.register(obs, Decorator.getOutermost(this), x, y);
			}
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			stroke = st.nextNamedKeyStroke('A');
			bounds.X = st.nextInt(- 20);
			bounds.Y = st.nextInt(- 20);
			bounds.Width = st.nextInt(40);
			bounds.Height = st.nextInt(40);
			description = st.nextToken("");
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
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(strokeConfig.ValueString).append(xConfig.ValueString).append(yConfig.ValueString).append(widthConfig.ValueString).append(heightConfig.ValueString).append(descConfig.ValueString);
					return VassalSharp.counters.ActionButton.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box box;
			private IntConfigurer xConfig;
			private IntConfigurer yConfig;
			private IntConfigurer widthConfig;
			private IntConfigurer heightConfig;
			private NamedHotKeyConfigurer strokeConfig;
			protected internal StringConfigurer descConfig;
			
			public Ed(ActionButton p)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createVerticalBox();
				descConfig = new StringConfigurer(null, "Description:  ", p.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(descConfig.Controls);
				strokeConfig = new NamedHotKeyConfigurer(null, "Invoke Key Command:  ", p.stroke);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(strokeConfig.Controls);
				xConfig = new IntConfigurer(null, "Button X-offset:  ", p.bounds.X);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(xConfig.Controls);
				yConfig = new IntConfigurer(null, "Button Y-offset:  ", p.bounds.Y);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(yConfig.Controls);
				widthConfig = new IntConfigurer(null, "Button Width:  ", p.bounds.Width);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(widthConfig.Controls);
				heightConfig = new IntConfigurer(null, "Button Height:  ", p.bounds.Height);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(heightConfig.Controls);
			}
		}
		
		/// <summary> Registers mouse listeners with Maps and other components. Clicking the
		/// mouse checks for pieces with an ActionButton trait and invokes them if the
		/// click falls within the button's boundaries
		/// </summary>
		protected internal class ButtonPusher
		{
			static private System.Int32 state465;
			private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				state465 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Set < Map > maps = new HashSet < Map >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private java.util.Map < Component, ComponentMouseListener > 
			componentMouseListeners = new HashMap < Component, ComponentMouseListener >();
			
			public virtual void  register(Map map)
			{
				if (map != null)
				{
					if (!maps.contains(map))
					{
						map.addLocalMouseListener(new MapMouseListener(this, map));
						maps.add(map);
					}
				}
			}
			
			public virtual void  register(System.Windows.Forms.Control obs, GamePiece piece, int x, int y)
			{
				if (obs != null)
				{
					ComponentMouseListener l = componentMouseListeners.get_Renamed(obs);
					if (l == null)
					{
						l = new ComponentMouseListener(this, piece, x, y);
						obs.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.counters.ActionButton.ButtonPusher.mouseDown);
						obs.Click += new System.EventHandler(l.mouseClicked);
						componentMouseListeners.put(obs, l);
					}
					else
					{
						l.xOffset = x;
						l.yOffset = y;
						l.target = piece;
					}
				}
			}
			
			/// <summary> Handle a mouse click on the given GamePiece at the given location (where
			/// 0,0 is the center of the piece). Activate all Action Buttons in sequence
			/// that are not Masked or Hidden
			/// 
			/// </summary>
			/// <param name="p">
			/// </param>
			/// <param name="x">
			/// </param>
			/// <param name="y">
			/// </param>
			/// <param name="Offset">A function to determine the offset of the target piece. This
			/// callback is done for efficiency reasons, since computing the
			/// offset may be expensive (as in the case of a piece in an
			/// expanded stack on a map) and is only needed if the piece has the
			/// ActionButton trait
			/// </param>
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public virtual void  doClick(GamePiece p, ref System.Drawing.Point point)
			{
				for (GamePiece piece = p; piece is Decorator; piece = ((Decorator) piece).getInner())
				{
					if (piece is Obscurable)
					{
						if (((Obscurable) piece).obscuredToMe())
						{
							return ;
						}
					}
					else if (piece is Hideable)
					{
						if (((Hideable) piece).invisibleToMe())
						{
							return ;
						}
					}
					if (piece is ActionButton)
					{
						ActionButton action = (ActionButton) piece;
						if (action.stroke != null && action.stroke.KeyStroke != null && action.bounds.Contains(point))
						{
							// Save state prior to command
							p.setProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT, PieceCloner.Instance.clonePiece(p));
							try
							{
								RecursionLimiter.startExecution(action);
								Command command = p.keyEvent(action.stroke.KeyStroke);
								GameModule.getGameModule().sendAndLog(command);
							}
							catch (RecursionLimitException e)
							{
								RecursionLimiter.infiniteLoop(e);
							}
							finally
							{
								RecursionLimiter.endExecution();
							}
						}
					}
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MapMouseListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			protected internal class MapMouseListener
			{
				private void  InitBlock(ButtonPusher enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ButtonPusher enclosingInstance;
				public ButtonPusher Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private Map map;
				
				public MapMouseListener(ButtonPusher enclosingInstance, Map map)
				{
					InitBlock(enclosingInstance);
					this.map = map;
				}
				
				public void  mouseClicked(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
					System.Drawing.Point point = e.getPoint();
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece p = map.findPiece(point, VassalSharp.counters.PieceFinder_Fields.PIECE_IN_STACK);
					if (p != null)
					{
						System.Drawing.Point rel = map.positionOf(p);
						point.Offset(- rel.X, - rel.Y);
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						Enclosing_Instance.doClick(p, ref point);
					}
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ComponentMouseListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			protected internal class ComponentMouseListener
			{
				private void  InitBlock(ButtonPusher enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ButtonPusher enclosingInstance;
				public ButtonPusher Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private GamePiece target;
				private int xOffset;
				private int yOffset;
				
				public ComponentMouseListener(ButtonPusher enclosingInstance, GamePiece piece, int x, int y)
				{
					InitBlock(enclosingInstance);
					target = piece;
					xOffset = x;
					yOffset = y;
				}
				
				public void  mouseClicked(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
					System.Drawing.Point point = e.getPoint();
					point.Offset(- xOffset, - yOffset);
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					Enclosing_Instance.doClick(target, ref point);
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					((System.Windows.Forms.Control) event_sender).Refresh();
				}
			}
		}
	}
}