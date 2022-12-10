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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Decorator that filters events to prevent a GamePiece from
	/// being selected and/or moved.
	/// 
	/// Note: The Alt selection filter was originally implemented
	/// as a ctl-shift filter, but this conflicts with the standard counter
	/// selection interface and has not worked since v3.0.
	/// 
	/// </summary>
	public class Immobilized:Decorator, EditablePiece
	{
		public class AnonymousClassEventFilter : EventFilter
		{
			//UPGRADE_ISSUE: Class 'java.awt.event.InputEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			public virtual bool rejectEvent(InputEvent evt)
			{
				return true;
			}
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(VassalSharp.counters.Properties_Fields.TERRAIN);
			l.add(VassalSharp.counters.Properties_Fields.IGNORE_GRID);
			l.add(VassalSharp.counters.Properties_Fields.NON_MOVABLE);
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
		virtual public System.String Description
		{
			get
			{
				return "Does not stack";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("NonStacking.htm");
			}
			
		}
		
		public const System.String ID = "immob;";
		protected internal bool shiftToSelect = false;
		protected internal bool altToSelect = false;
		protected internal bool ignoreGrid = false;
		protected internal bool neverSelect = false;
		protected internal bool neverMove = false;
		protected internal bool moveIfSelected = false;
		protected internal EventFilter selectFilter;
		protected internal EventFilter moveFilter;
		
		protected internal const char MOVE_SELECTED = 'I';
		protected internal const char MOVE_NORMAL = 'N';
		protected internal const char NEVER_MOVE = 'V';
		protected internal const char IGNORE_GRID = 'g';
		protected internal const char SHIFT_SELECT = 'i';
		protected internal const char ALT_SELECT = 'c'; //NB. Using 'c' to maintain compatibility with old ctl-shift version
		protected internal const char NEVER_SELECT = 'n';
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'UseShift' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		public class UseShift : EventFilter
		{
			public UseShift(Immobilized enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Immobilized enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Immobilized enclosingInstance;
			public Immobilized Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Class 'java.awt.event.InputEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			public virtual bool rejectEvent(InputEvent evt)
			{
				return !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift) && !true.Equals(Enclosing_Instance.getProperty(VassalSharp.counters.Properties_Fields.SELECTED));
			}
		}
		
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'UseAlt' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		public class UseAlt : EventFilter
		{
			public UseAlt(Immobilized enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Immobilized enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Immobilized enclosingInstance;
			public Immobilized Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Class 'java.awt.event.InputEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			public virtual bool rejectEvent(InputEvent evt)
			{
				return !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Alt) && !true.Equals(Enclosing_Instance.getProperty(VassalSharp.counters.Properties_Fields.SELECTED));
			}
		}
		
		
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MoveIfSelected' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		protected internal class MoveIfSelected : EventFilter
		{
			public MoveIfSelected(Immobilized enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Immobilized enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Immobilized enclosingInstance;
			public Immobilized Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Class 'java.awt.event.InputEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			public virtual bool rejectEvent(InputEvent evt)
			{
				return !true.Equals(Enclosing_Instance.getProperty(VassalSharp.counters.Properties_Fields.SELECTED));
			}
		}
		
		//UPGRADE_NOTE: The initialization of  'NEVER' was moved to static method 'VassalSharp.counters.Immobilized'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static EventFilter NEVER;
		
		public Immobilized():this(null, Immobilized.ID)
		{
		}
		
		public Immobilized(GamePiece p, System.String type)
		{
			InitBlock();
			setInner(p);
			mySetType(type);
		}
		
		public virtual void  mySetType(System.String type)
		{
			shiftToSelect = false;
			altToSelect = false;
			neverSelect = false;
			ignoreGrid = false;
			neverMove = false;
			moveIfSelected = false;
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			System.String selectionOptions = st.nextToken("");
			System.String movementOptions = st.nextToken("");
			if (selectionOptions.IndexOf((System.Char) SHIFT_SELECT) >= 0)
			{
				shiftToSelect = true;
				moveIfSelected = true;
			}
			if (selectionOptions.IndexOf((System.Char) ALT_SELECT) >= 0)
			{
				altToSelect = true;
				moveIfSelected = true;
			}
			if (selectionOptions.IndexOf((System.Char) NEVER_SELECT) >= 0)
			{
				neverSelect = true;
				neverMove = true;
			}
			if (selectionOptions.IndexOf((System.Char) IGNORE_GRID) >= 0)
			{
				ignoreGrid = true;
			}
			if (movementOptions.Length > 0)
			{
				switch (movementOptions[0])
				{
					
					case NEVER_MOVE: 
						neverMove = true;
						moveIfSelected = false;
						break;
					
					case MOVE_SELECTED: 
						neverMove = false;
						moveIfSelected = true;
						break;
					
					default: 
						neverMove = false;
						moveIfSelected = false;
						break;
					
				}
			}
			if (neverSelect)
			{
				selectFilter = NEVER;
			}
			else if (shiftToSelect)
			{
				selectFilter = new UseShift(this);
			}
			else if (altToSelect)
			{
				selectFilter = new UseAlt(this);
			}
			else
			{
				selectFilter = null;
			}
			if (neverMove)
			{
				moveFilter = NEVER;
			}
			else if (moveIfSelected)
			{
				moveFilter = new MoveIfSelected(this);
			}
			else
			{
				moveFilter = null;
			}
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs e)
		{
			return null;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.NO_STACK.Equals(key))
			{
				return true;
			}
			else if (VassalSharp.counters.Properties_Fields.TERRAIN.Equals(key))
			{
				return Boolean.valueOf(moveIfSelected || neverMove);
			}
			else if (VassalSharp.counters.Properties_Fields.IGNORE_GRID.Equals(key))
			{
				return Boolean.valueOf(ignoreGrid);
			}
			else if (VassalSharp.counters.Properties_Fields.SELECT_EVENT_FILTER.Equals(key))
			{
				return selectFilter;
			}
			else if (VassalSharp.counters.Properties_Fields.MOVE_EVENT_FILTER.Equals(key))
			{
				return moveFilter;
			}
			else if (VassalSharp.counters.Properties_Fields.NON_MOVABLE.Equals(key))
			{
				return neverMove;
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.NO_STACK.Equals(key))
			{
				return true;
			}
			else if (VassalSharp.counters.Properties_Fields.TERRAIN.Equals(key))
			{
				return Boolean.valueOf(moveIfSelected || neverMove);
			}
			else if (VassalSharp.counters.Properties_Fields.IGNORE_GRID.Equals(key))
			{
				return Boolean.valueOf(ignoreGrid);
			}
			else if (VassalSharp.counters.Properties_Fields.SELECT_EVENT_FILTER.Equals(key))
			{
				return selectFilter;
			}
			else if (VassalSharp.counters.Properties_Fields.MOVE_EVENT_FILTER.Equals(key))
			{
				return moveFilter;
			}
			else if (VassalSharp.counters.Properties_Fields.NON_MOVABLE.Equals(key))
			{
				return neverMove;
			}
			else
			{
				return base.getProperty(key);
			}
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder(ID);
			if (neverSelect)
			{
				buffer.append(NEVER_SELECT);
			}
			else if (shiftToSelect)
			{
				buffer.append(SHIFT_SELECT);
			}
			else if (altToSelect)
			{
				buffer.append(ALT_SELECT);
			}
			if (ignoreGrid)
			{
				buffer.append(IGNORE_GRID);
			}
			buffer.append(';');
			if (neverMove)
			{
				buffer.append(NEVER_MOVE);
			}
			else if (moveIfSelected)
			{
				buffer.append(MOVE_SELECTED);
			}
			else
			{
				buffer.append(MOVE_NORMAL);
			}
			return buffer.toString();
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override void  mySetState(System.String s)
		{
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		private class Ed : PieceEditor
		{
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
					System.String s = VassalSharp.counters.Immobilized.ID;
					switch (selectionOption.SelectedIndex)
					{
						
						case 1: 
							s += VassalSharp.counters.Immobilized.SHIFT_SELECT;
							break;
						
						case 2: 
							s += VassalSharp.counters.Immobilized.ALT_SELECT;
							break;
						
						case 3: 
							s += VassalSharp.counters.Immobilized.NEVER_SELECT;
							break;
						}
					if (ignoreGridBox.Checked)
					{
						s += VassalSharp.counters.Immobilized.IGNORE_GRID;
					}
					s += ';';
					switch (movementOption.SelectedIndex)
					{
						
						case 0: 
							s += VassalSharp.counters.Immobilized.MOVE_NORMAL;
							break;
						
						case 1: 
							s += VassalSharp.counters.Immobilized.MOVE_SELECTED;
							break;
						
						case 2: 
							s += VassalSharp.counters.Immobilized.NEVER_MOVE;
							break;
						}
					return s;
				}
				
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			private System.Windows.Forms.ComboBox selectionOption;
			private System.Windows.Forms.ComboBox movementOption;
			private System.Windows.Forms.CheckBox ignoreGridBox;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box controls;
			
			public Ed(Immobilized p)
			{
				selectionOption = new System.Windows.Forms.ComboBox();
				selectionOption.Items.Add("normally");
				selectionOption.Items.Add("when shift-key down");
				selectionOption.Items.Add("when alt-key down");
				selectionOption.Items.Add("never");
				if (p.neverSelect)
				{
					selectionOption.SelectedIndex = 3;
				}
				else if (p.altToSelect)
				{
					selectionOption.SelectedIndex = 2;
				}
				else if (p.shiftToSelect)
				{
					selectionOption.SelectedIndex = 1;
				}
				else
				{
					selectionOption.SelectedIndex = 0;
				}
				ignoreGridBox = SupportClass.CheckBoxSupport.CreateCheckBox("Ignore map grid when moving?");
				ignoreGridBox.Checked = p.ignoreGrid;
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				controls = Box.createVerticalBox();
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Select piece:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				b.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(selectionOption);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				movementOption = new System.Windows.Forms.ComboBox();
				movementOption.Items.Add("normally");
				movementOption.Items.Add("only if selected");
				movementOption.Items.Add("never");
				if (p.neverMove)
				{
					movementOption.SelectedIndex = 2;
				}
				else if (p.moveIfSelected)
				{
					movementOption.SelectedIndex = 1;
				}
				else
				{
					movementOption.SelectedIndex = 0;
				}
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = "Move piece:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				b.Controls.Add(temp_Control2);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(movementOption);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(ignoreGridBox);
			}
		}
		static Immobilized()
		{
			NEVER = new AnonymousClassEventFilter();
		}
	}
}