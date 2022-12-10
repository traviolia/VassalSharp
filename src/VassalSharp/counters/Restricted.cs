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
/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Jun 13, 2002
* Time: 9:52:40 PM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A GamePiece with the Restricted trait can only be manipulated by the player playing a specific side</summary>
	public class Restricted:Decorator, EditablePiece
	{
		virtual public System.String Description
		{
			get
			{
				return "Restricted Access";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("RestrictedAccess.htm");
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		public const System.String ID = "restrict;";
		private System.String[] side;
		private bool restrictByPlayer;
		private System.String owningPlayer = "";
		private bool restrictMovement = true;
		private static PlayerRoster.SideChangeListener handleRetirement;
		
		public Restricted():this(ID, null)
		{
		}
		
		public Restricted(System.String type, GamePiece p)
		{
			setInner(p);
			mySetType(type);
			if (handleRetirement == null)
			{
				handleRetirement = new RetirementHandler();
				PlayerRoster.addSideChangeListener(handleRetirement);
			}
		}
		
		public virtual void  mySetType(System.String type)
		{
			type = type.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			side = st.nextStringArray(0);
			restrictByPlayer = st.nextBoolean(false);
			restrictMovement = st.nextBoolean(true);
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
		
		public virtual bool isRestricted()
		{
			bool restricted = false;
			if (restrictByPlayer)
			{
				restricted = owningPlayer.Length > 0 && !GameModule.getUserId().equals(owningPlayer);
			}
			if ((restricted || !restrictByPlayer) && PlayerRoster.Active && GameModule.getGameModule().getGameState().isGameStarted())
			{
				restricted = true;
				for (int i = 0; i < side.Length; ++i)
				{
					if (side[i].Equals(PlayerRoster.getMySide()))
					{
						restricted = false;
						break;
					}
				}
			}
			return restricted;
		}
		
		/*  @Override
		public void setMap(Map m) {
		if (m != null && restrictByPlayer && owningPlayer.length() == 0) {
		owningPlayer = GameModule.getUserId();
		}
		super.setMap(m);
		}*/
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  setProperty(System.Object key, System.Object val)
		{
			if (VassalSharp.counters.Properties_Fields.SELECTED.Equals(key) && true.Equals(val) && restrictByPlayer && owningPlayer.Length == 0)
			{
				if (getMap() != null)
				{
					owningPlayer = GameModule.getUserId();
				}
				else
				{
					System.Console.Error.WriteLine("Selected, but map == null");
				}
			}
			base.setProperty(key, val);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'getKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] getKeyCommands()
		{
			if (!isRestricted())
			{
				return base.getKeyCommands();
			}
			else
			{
				return new KeyCommand[0];
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.RESTRICTED.Equals(key))
			{
				return Boolean.valueOf(isRestricted());
			}
			else if (VassalSharp.counters.Properties_Fields.RESTRICTED_MOVEMENT.Equals(key))
			{
				return Boolean.valueOf(isRestricted() && restrictMovement);
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.RESTRICTED.Equals(key))
			{
				return Boolean.valueOf(isRestricted());
			}
			else if (VassalSharp.counters.Properties_Fields.RESTRICTED_MOVEMENT.Equals(key))
			{
				return Boolean.valueOf(isRestricted() && restrictMovement);
			}
			else
			{
				return base.getProperty(key);
			}
		}
		
		public override System.String myGetState()
		{
			return owningPlayer;
		}
		
		public override System.String myGetType()
		{
			return ID + new SequenceEncoder(';').append(side).append(restrictByPlayer).append(restrictMovement).Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		public override Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			if (!isRestricted())
			{
				return base.keyEvent(stroke);
			}
			else
			{
				return null;
			}
		}
		
		public override void  mySetState(System.String newState)
		{
			owningPlayer = newState;
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
					return VassalSharp.counters.Restricted.ID + new SequenceEncoder(';').append(config.ValueString).append(byPlayer.booleanValue()).append(movementConfig.booleanValue()).getValue();
				}
				
			}
			private BooleanConfigurer byPlayer;
			private StringArrayConfigurer config;
			private BooleanConfigurer movementConfig;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box box;
			
			public Ed(Restricted r)
			{
				byPlayer = new BooleanConfigurer(null, "Also belongs to initially-placing player?", r.restrictByPlayer);
				config = new StringArrayConfigurer(null, "Belongs to side", r.side);
				movementConfig = new BooleanConfigurer(null, "Prevent non-owning players from moving piece?", r.restrictMovement);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createVerticalBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
				//UPGRADE_ISSUE: Field 'java.awt.Component.LEFT_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentLEFT_ALIGNMENT_f'"
				((System.Windows.Forms.Control) byPlayer.Controls).setAlignmentX(Box.LEFT_ALIGNMENT);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
				//UPGRADE_ISSUE: Field 'java.awt.Component.LEFT_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentLEFT_ALIGNMENT_f'"
				((System.Windows.Forms.Control) movementConfig.Controls).setAlignmentX(Box.LEFT_ALIGNMENT);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(config.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(byPlayer.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(movementConfig.Controls);
			}
		}
		/// <summary> When a player changes sides to become an observer, relinquish ownership of all pieces</summary>
		/// <author>  rodneykinney
		/// 
		/// </author>
		private class RetirementHandler : PlayerRoster.SideChangeListener, PieceVisitor
		{
			public RetirementHandler()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(GamePiece piece: m.getPieces())
				{
					c = c.append((Command) d.accept(Enclosing_Instance.piece));
				}
			}
			
			public virtual void  sideChanged(System.String oldSide, System.String newSide)
			{
				if (newSide == null)
				{
					PieceVisitorDispatcher d = new PieceVisitorDispatcher(this);
					Command c = new NullCommand();
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Map m: GameModule.getGameModule().getComponentsOf(Map.
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			class))
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			GameModule.getGameModule().sendAndLog(c);
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Object visitDefault(GamePiece p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Restricted r =(Restricted) Decorator.getDecorator(p, Restricted.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(r != null 
	&& r.restrictByPlayer 
	&& GameModule.getUserId().equals(r.owningPlayer))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		ChangeTracker t = new ChangeTracker(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	r.owningPlayer = ;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return t.getChangeCommand();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Object visitStack(Stack s)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Command c = new NullCommand();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(Iterator < GamePiece > it = s.getPiecesIterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	it.hasNext();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		c = c.append((Command) visitDefault(it.next()));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}