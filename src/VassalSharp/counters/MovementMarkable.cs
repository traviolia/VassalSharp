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
* User: unknown
* Date: Dec 30, 2002
* Time: 12:42:01 PM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A GamePiece with this trait will automatically be marked whenever it is moved.  A marked piece is
	/// indicated by drawing a specified image at a specified location
	/// </summary>
	public class MovementMarkable:Decorator, TranslatablePiece
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(VassalSharp.counters.Properties_Fields.MOVED);
			return l;
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		private System.Drawing.Size ImageSize
		{
			get
			{
				System.Drawing.Image icon = movedIcon.IconValue;
				return icon != null?new System.Drawing.Size(icon.Width, icon.Height):new System.Drawing.Size(0, 0);
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return "Mark When Moved";
			}
			
		}
		virtual public VassalSharp.build.module.documentation.HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("MarkMoved.htm");
			}
			
		}
		public const System.String ID = "markmoved;";
		
		private int xOffset = 0;
		private int yOffset = 0;
		private System.String command;
		private NamedKeyStroke key;
		private IconConfigurer movedIcon = new IconConfigurer(null, "Marker Image:  ", "/images/moved.gif");
		private bool hasMoved = false;
		
		public MovementMarkable():this(ID + "moved.gif;0;0", null)
		{
		}
		
		public MovementMarkable(System.String type, GamePiece p)
		{
			InitBlock();
			mySetType(type);
			setInner(p);
		}
		
		public virtual bool isMoved()
		{
			return hasMoved;
		}
		
		public virtual void  setMoved(bool b)
		{
			hasMoved = b;
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			movedIcon.setValue(st.nextToken());
			xOffset = st.nextInt(0);
			yOffset = st.nextInt(0);
			command = st.nextToken("Mark Moved");
			key = st.nextNamedKeyStroke('M');
		}
		
		public override void  mySetState(System.String newState)
		{
			hasMoved = "true".Equals(newState);
		}
		
		public override System.String myGetState()
		{
			return System.Convert.ToString(hasMoved);
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(movedIcon.ValueString).append(xOffset).append(yOffset).append(command).append(key);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[]{new KeyCommand(command, key, Decorator.getOutermost(this), this)};
		}
		
		public virtual Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			if (stroke != null && key.Equals(stroke))
			{
				ChangeTracker c = new ChangeTracker(this);
				// Set the property on the entire piece so all traits can respond
				Decorator.getOutermost(this).setProperty(VassalSharp.counters.Properties_Fields.MOVED, Boolean.valueOf(!hasMoved));
				return c.ChangeCommand;
			}
			else
			{
				return null;
			}
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = piece.boundingBox();
			SupportClass.RectangleSupport.AddRectangleToRectangle(ref r, piece.boundingBox());
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size d = ImageSize;
			SupportClass.RectangleSupport.AddRectangleToRectangle(ref r, new System.Drawing.Rectangle(xOffset, yOffset, d.Width, d.Height));
			return r;
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
			if (hasMoved && movedIcon.IconValue != null)
			{
				System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
				System.Drawing.Drawing2D.Matrix transform = g2d.Transform;
				g2d.ScaleTransform((System.Single) zoom, (System.Single) zoom, System.Drawing.Drawing2D.MatrixOrder.Append);
				//UPGRADE_TODO: Method 'javax.swing.Icon.paintIcon' was converted to 'System.Drawing.Graphics.drawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingIconpaintIcon_javaawtComponent_javaawtGraphics_int_int'"
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				g.DrawImage(movedIcon.IconValue, (int) System.Math.Round(x / zoom) + xOffset, (int) System.Math.Round(y / zoom) + yOffset);
				g2d.Transform = transform;
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.MOVED.Equals(key))
			{
				return isMoved()?true:false;
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.MOVED.Equals(key))
			{
				return isMoved()?true:false;
			}
			else
			{
				return base.getProperty(key);
			}
		}
		
		public override void  setProperty(System.Object key, System.Object val)
		{
			if (VassalSharp.counters.Properties_Fields.MOVED.Equals(key))
			{
				setMoved(true.Equals(val));
				piece.setProperty(key, val); // So other traits can respond to the property change
			}
			else
			{
				base.setProperty(key, val);
			}
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(command, "Mark Moved command");
		}
		
		
		private class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					SupportClass.OptionPaneSupport.ShowMessageDialog(Enclosing_Instance.box, "You must enable the \"Mark Pieces that Move\" option in one or more Map Windows", "Option not enabled", (int) System.Windows.Forms.MessageBoxIcon.Exclamation);
				}
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					bool enabled = false;
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(Map m: Map.getMapList())
					{
						System.String value_Renamed = m.getAttributeValueString(Map.MARK_MOVED);
						enabled = enabled || GlobalOptions.ALWAYS.Equals(value_Renamed) || GlobalOptions.PROMPT.Equals(value_Renamed);
					}
					if (!enabled)
					{
						IThreadRunnable runnable = new AnonymousClassRunnable(this);
						//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
						SwingUtilities.invokeLater(runnable);
					}
					return box;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(iconConfig.ValueString).append(xOff.ValueString).append(yOff.ValueString).append(command.ValueString).append(key.ValueString);
					return VassalSharp.counters.MovementMarkable.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "false";
				}
				
			}
			private IconConfigurer iconConfig;
			private IntConfigurer xOff;
			private IntConfigurer yOff;
			private StringConfigurer command;
			private NamedHotKeyConfigurer key;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box box;
			
			internal Ed(MovementMarkable p)
			{
				iconConfig = p.movedIcon;
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createVerticalBox();
				command = new StringConfigurer(null, "Command:  ", p.command);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(command.Controls);
				key = new NamedHotKeyConfigurer(null, "Keyboard command:  ", p.key);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(key.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(iconConfig.Controls);
				xOff = new IntConfigurer(null, "Horizontal Offset:  ", p.xOffset);
				yOff = new IntConfigurer(null, "Vertical Offset:  ", p.yOffset);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(xOff.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(yOff.Controls);
			}
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
	}
}