/*
* $Id$
*
* Copyright (c) 2003 by Rodney Kinney
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using RemovePiece = VassalSharp.command.RemovePiece;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> This trait adds a command that creates a duplicate of the selected Gamepiece</summary>
	public class Delete:Decorator, TranslatablePiece
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(bool selected, Delete enclosingInstance)
			{
				InitBlock(selected, enclosingInstance);
			}
			private void  InitBlock(bool selected, Delete enclosingInstance)
			{
				this.selected = selected;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable selected was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private bool selected;
			private Delete enclosingInstance;
			public Delete Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				// Don't select if the next piece has itself been deleted
				if (GameModule.getGameModule().getGameState().getPieceForId(Enclosing_Instance.selected.getId()) != null)
				{
					KeyBuffer.Buffer.add(selected);
				}
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
		virtual public System.String Description
		{
			get
			{
				return "Delete";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("GamePiece.htm", "Delete");
			}
			
		}
		public const System.String ID = "delete;";
		protected internal KeyCommand[] keyCommands;
		protected internal KeyCommand deleteCommand;
		protected internal System.String commandName;
		protected internal NamedKeyStroke key;
		
		public Delete():this(ID + "Delete;D", null)
		{
		}
		
		public Delete(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public virtual void  mySetType(System.String type)
		{
			type = type.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			commandName = st.nextToken();
			key = st.nextNamedKeyStroke('D');
			keyCommands = null;
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(commandName).append(key);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (keyCommands == null)
			{
				deleteCommand = new KeyCommand(commandName, key, Decorator.getOutermost(this), this);
				if (commandName.Length > 0 && key != null && !key.Null)
				{
					keyCommands = new KeyCommand[]{deleteCommand};
				}
				else
				{
					keyCommands = new KeyCommand[0];
				}
			}
			deleteCommand.setEnabled(getMap() != null);
			return keyCommands;
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command c = null;
			myGetKeyCommands();
			if (deleteCommand.matches(stroke))
			{
				GamePiece outer = Decorator.getOutermost(this);
				if (Parent != null)
				{
					GamePiece next = Parent.getPieceBeneath(outer);
					if (next == null)
						next = Parent.getPieceAbove(outer);
					if (next != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'selected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						GamePiece selected = next;
						IThreadRunnable runnable = new AnonymousClassRunnable(selected, this);
						//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
						SwingUtilities.invokeLater(runnable);
					}
				}
				c = new RemovePiece(outer);
				c.execute();
			}
			return c;
		}
		
		public override void  mySetState(System.String newState)
		{
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
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(commandName, "Delete command");
		}
		
		public class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(nameInput.ValueString).append(keyInput.ValueString);
					return VassalSharp.counters.Delete.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			private StringConfigurer nameInput;
			private NamedHotKeyConfigurer keyInput;
			private System.Windows.Forms.Panel controls;
			
			public Ed(Delete p)
			{
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				nameInput = new StringConfigurer(null, "Command name:  ", p.commandName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(nameInput.Controls);
				
				keyInput = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", p.key);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(keyInput.Controls);
			}
		}
	}
}