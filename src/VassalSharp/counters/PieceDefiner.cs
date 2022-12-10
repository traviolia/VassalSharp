/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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
using GameModule = VassalSharp.build.GameModule;
using GpIdSupport = VassalSharp.build.GpIdSupport;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using HelpWindowExtension = VassalSharp.build.module.documentation.HelpWindowExtension;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using BrowserSupport = VassalSharp.tools.BrowserSupport;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ReflectionUtils = VassalSharp.tools.ReflectionUtils;
namespace VassalSharp.counters
{
	
	/// <summary> This is the GamePiece designer dialog.  It appears when you edit
	/// the properties of a "Single Piece" in the Configuration window.
	/// </summary>
	[Serializable]
	public class PieceDefiner:System.Windows.Forms.Panel, HelpWindowExtension
	{
		static private System.Int32 state542;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassListSelectionListener
		{
			public AnonymousClassListSelectionListener(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.EventArgs evt)
			{
				System.Object o = Enclosing_Instance.availableList.SelectedItem;
				Enclosing_Instance.helpButton.Enabled = o is EditablePiece && ((EditablePiece) o).HelpFile != null;
				Enclosing_Instance.addButton.Enabled = o is Decorator;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.showHelpForPiece();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.showInputDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				System.String className = JOptionPane.showInputDialog(Enclosing_Instance, "Enter fully-qualified name of Java class to import");
				Enclosing_Instance.importPiece(className);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				System.Object selected = Enclosing_Instance.availableList.SelectedItem;
				if (selected is Decorator)
				{
					if (Enclosing_Instance.inUseModel.Count > 0)
					{
						Decorator c = (Decorator) selected;
						Enclosing_Instance.addTrait(c);
						if (SupportClass.ListBoxObjectCollectionSupport.LastElement(Enclosing_Instance.inUseModel).GetType() == c.GetType())
						{
							if (Enclosing_Instance.edit(Enclosing_Instance.inUseModel.Count - 1))
							{
								// Add was successful
							}
							else
							{
								// Add was cancelled
								if (!(Enclosing_Instance.inUseModel.Count == 0))
									Enclosing_Instance.removeTrait(Enclosing_Instance.inUseModel.Count - 1);
							}
						}
					}
				}
				else if (selected is GamePiece && Enclosing_Instance.inUseModel.Count == 0)
				{
					GamePiece p = null;
					try
					{
						p = (GamePiece) selected.GetType().getConstructor().newInstance();
					}
					//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					catch (System.Exception t)
					{
						ReflectionUtils.handleNewInstanceFailure(t, selected.GetType());
					}
					
					if (p != null)
					{
						Enclosing_Instance.setPiece(p);
						if (Enclosing_Instance.inUseModel.Count > 0)
						{
							if (Enclosing_Instance.edit(0))
							{
								// Add was successful
							}
							else
							{
								// Add was cancelled
								Enclosing_Instance.removeTrait(0);
							}
						}
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				int index = Enclosing_Instance.inUseList.SelectedIndex;
				if (index >= 0)
				{
					Enclosing_Instance.removeTrait(index);
					if (Enclosing_Instance.inUseModel.Count > 0)
					{
						//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
						Enclosing_Instance.inUseList.SelectedIndex = System.Math.Min(Enclosing_Instance.inUseModel.Count - 1, System.Math.Max(index, 0));
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassListSelectionListener1
		{
			public AnonymousClassListSelectionListener1(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'o '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Object o = Enclosing_Instance.inUseList.SelectedItem;
				Enclosing_Instance.propsButton.Enabled = o is EditablePiece;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int index = Enclosing_Instance.inUseList.SelectedIndex;
				//UPGRADE_NOTE: Final was removed from the declaration of 'copyAndRemove '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool copyAndRemove = Enclosing_Instance.inUseModel.Count > 0 && (index > 0 || !(Enclosing_Instance.inUseModel[0] is BasicPiece));
				Enclosing_Instance.copyButton.Enabled = copyAndRemove;
				Enclosing_Instance.removeButton.Enabled = copyAndRemove;
				
				Enclosing_Instance.pasteButton.Enabled = VassalSharp.counters.PieceDefiner.clipBoard != null;
				Enclosing_Instance.moveUpButton.Enabled = index > 1;
				Enclosing_Instance.moveDownButton.Enabled = index > 0 && index < Enclosing_Instance.inUseModel.Count - 1;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter
		{
			public AnonymousClassMouseAdapter(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
			{
				if (evt.Clicks == 2)
				{
					int index = Enclosing_Instance.inUseList.IndexFromPoint(new System.Drawing.Point(evt.X, evt.Y));
					if (index >= 0)
					{
						Enclosing_Instance.edit(index);
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				int index = Enclosing_Instance.inUseList.SelectedIndex;
				if (index >= 0)
				{
					Enclosing_Instance.edit(index);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener5
		{
			public AnonymousClassActionListener5(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				int index = Enclosing_Instance.inUseList.SelectedIndex;
				if (index > 1 && index < Enclosing_Instance.inUseModel.Count)
				{
					Enclosing_Instance.moveDecoratorUp(index);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener6' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener6
		{
			public AnonymousClassActionListener6(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				int index = Enclosing_Instance.inUseList.SelectedIndex;
				if (index > 0 && index < Enclosing_Instance.inUseModel.Count - 1)
				{
					Enclosing_Instance.moveDecoratorDown(index);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener7' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener7
		{
			public AnonymousClassActionListener7(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.pasteButton.Enabled = true;
				int index = Enclosing_Instance.inUseList.SelectedIndex;
				VassalSharp.counters.PieceDefiner.clipBoard = new TraitClipboard((Decorator) Enclosing_Instance.inUseModel[index]);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener8' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener8
		{
			public AnonymousClassActionListener8(PieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceDefiner enclosingInstance;
			public PieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				if (VassalSharp.counters.PieceDefiner.clipBoard != null)
				{
					Enclosing_Instance.paste();
				}
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state542 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = "";
			pieceIdLabel = temp_label;
		}
		virtual public HelpWindow BaseWindow
		{
			set
			{
			}
			
		}
		virtual public bool Changed
		{
			get
			{
				return changed;
			}
			
			set
			{
				changed = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		protected internal static System.Windows.Forms.ListBox.ObjectCollection availableModel;
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		protected internal System.Windows.Forms.ListBox.ObjectCollection inUseModel;
		//UPGRADE_ISSUE: Interface 'javax.swing.ListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListCellRenderer'"
		protected internal ListCellRenderer r;
		protected internal PieceSlot slot;
		private GamePiece piece;
		protected internal static TraitClipboard clipBoard;
		protected internal System.String pieceId = "";
		//UPGRADE_NOTE: The initialization of  'pieceIdLabel' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal System.Windows.Forms.Label pieceIdLabel;
		protected internal GpIdSupport gpidSupport;
		protected internal bool changed;
		
		/// <summary>Creates new form test </summary>
		public PieceDefiner()
		{
			InitBlock();
			initDefinitions();
			//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
			inUseModel = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
			r = new Renderer();
			slot = new PieceSlot();
			initComponents();
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			availableList.SelectedIndex = 0;
			Changed = false;
			gpidSupport = GameModule.getGameModule().getGpIdSupport();
		}
		
		public PieceDefiner(System.String id, GpIdSupport s):this()
		{
			pieceId = id;
			pieceIdLabel.Text = "Id: " + id;
			gpidSupport = s;
		}
		
		public PieceDefiner(GpIdSupport s):this()
		{
			gpidSupport = s;
		}
		
		protected internal static void  initDefinitions()
		{
			if (availableModel == null)
			{
				//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
				availableModel = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
				availableModel.Add(new BasicPiece());
				availableModel.Add(new Delete());
				availableModel.Add(new Clone());
				availableModel.Add(new Embellishment());
				availableModel.Add(new UsePrototype());
				availableModel.Add(new Labeler());
				availableModel.Add(new ReportState());
				availableModel.Add(new TriggerAction());
				availableModel.Add(new GlobalHotKey());
				availableModel.Add(new ActionButton());
				availableModel.Add(new FreeRotator());
				availableModel.Add(new Pivot());
				availableModel.Add(new Hideable());
				availableModel.Add(new Obscurable());
				availableModel.Add(new SendToLocation());
				availableModel.Add(new CounterGlobalKeyCommand());
				availableModel.Add(new Translate());
				availableModel.Add(new ReturnToDeck());
				availableModel.Add(new Immobilized());
				availableModel.Add(new PropertySheet());
				availableModel.Add(new TableInfo());
				availableModel.Add(new PlaceMarker());
				availableModel.Add(new Replace());
				availableModel.Add(new NonRectangular());
				availableModel.Add(new PlaySound());
				availableModel.Add(new MovementMarkable());
				availableModel.Add(new Footprint());
				availableModel.Add(new AreaOfEffect());
				availableModel.Add(new SubMenu());
				availableModel.Add(new RestrictCommands());
				availableModel.Add(new Restricted());
				availableModel.Add(new Marker());
				availableModel.Add(new DynamicProperty());
				availableModel.Add(new CalculatedProperty());
				availableModel.Add(new SetGlobalProperty());
			}
		}
		
		/// <summary> Plugins can add additional GamePiece definitions</summary>
		/// <param name="definition">
		/// </param>
		public static void  addDefinition(GamePiece definition)
		{
			initDefinitions();
			availableModel.Add(definition);
		}
		
		public virtual void  setPiece(GamePiece piece)
		{
			inUseModel.Clear();
			while (piece is Decorator)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final Class < ? > pieceClass = piece.getClass();
				
				inUseModel.Insert(0, piece);
				bool contains = false;
				for (int i = 0, j = availableModel.Count; i < j; ++i)
				{
					if (pieceClass.isInstance(availableModel[i]))
					{
						contains = true;
						break;
					}
				}
				
				if (!contains)
				{
					try
					{
						availableModel.Add(pieceClass.getConstructor().newInstance());
					}
					//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					catch (System.Exception t)
					{
						ReflectionUtils.handleNewInstanceFailure(t, pieceClass);
					}
				}
				
				piece = ((Decorator) piece).piece;
			}
			
			if (piece == null)
			{
				inUseModel.Insert(0, new BasicPiece());
			}
			else
			{
				inUseModel.Insert(0, piece);
			}
			
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			inUseList.SelectedIndex = 0;
			refresh();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		
		private void  refresh()
		{
			if (inUseModel.Count > 0)
			{
				piece = (GamePiece) SupportClass.ListBoxObjectCollectionSupport.LastElement(inUseModel);
			}
			else
			{
				piece = null;
			}
			slot.Piece = piece;
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			slot.Component.Refresh();
		}
		
		public virtual GamePiece getPiece()
		{
			return piece;
		}
		
		/// <summary> This method is called from within the constructor to initialize the form.</summary>
		private void  initComponents()
		{
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(slot.Component);
			
			System.Windows.Forms.Panel controls = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			controls.setLayout(new BoxLayout(controls, BoxLayout.X_AXIS));
			
			availablePanel = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			availableScroll = temp_scrollablecontrol;
			System.Windows.Forms.ListBox temp_ListBox;
			temp_ListBox = new System.Windows.Forms.ListBox();
			temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			availableList = temp_ListBox;
			helpButton = new System.Windows.Forms.Button();
			importButton = new System.Windows.Forms.Button();
			addRemovePanel = new System.Windows.Forms.Panel();
			addButton = new System.Windows.Forms.Button();
			removeButton = new System.Windows.Forms.Button();
			inUsePanel = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
			temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol2.AutoScroll = true;
			inUseScroll = temp_scrollablecontrol2;
			System.Windows.Forms.ListBox temp_ListBox2;
			temp_ListBox2 = new System.Windows.Forms.ListBox();
			temp_ListBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			inUseList = temp_ListBox2;
			propsButton = new System.Windows.Forms.Button();
			moveUpDownPanel = new System.Windows.Forms.Panel();
			moveUpButton = new System.Windows.Forms.Button();
			moveDownButton = new System.Windows.Forms.Button();
			copyButton = new System.Windows.Forms.Button();
			pasteButton = new System.Windows.Forms.Button();
			//        setLayout(new BoxLayout(this, 0));
			
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			availablePanel.setLayout(new BoxLayout(availablePanel, 1));
			
			
			availableList.Items.Clear();
			//UPGRADE_TODO: Method 'javax.swing.JList.setModel' was converted to 'System.Windows.Forms.ListBox.Items.AddRange' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetModel_javaxswingListModel'"
			availableList.Items.AddRange(availableModel);
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			availableList.SelectionMode = (System.Windows.Forms.SelectionMode) System.Windows.Forms.SelectionMode.One;
			//UPGRADE_ISSUE: Method 'javax.swing.JList.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListsetCellRenderer_javaxswingListCellRenderer'"
			availableList.setCellRenderer(r);
			availableList.SelectedValueChanged += new System.EventHandler(new AnonymousClassListSelectionListener(this).valueChanged);
			
			availableScroll.Controls.Add(availableList);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(availableScroll.CreateGraphics(), 0, 0, availableScroll.Width, availableScroll.Height, new TitledBorder("Available Traits"));
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			availablePanel.Controls.Add(availableScroll);
			
			
			helpButton.Text = "Help";
			helpButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(helpButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			availablePanel.Controls.Add(helpButton);
			
			importButton.Text = "Import";
			importButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(importButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			availablePanel.Controls.Add(importButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controls.Controls.Add(availablePanel);
			
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			addRemovePanel.setLayout(new BoxLayout(addRemovePanel, 1));
			
			addButton.Text = "Add ->";
			addButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(addButton);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
			addButton.setAlignmentX(Component.CENTER_ALIGNMENT);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			addRemovePanel.Controls.Add(addButton);
			
			removeButton.Text = "<- Remove";
			removeButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(removeButton);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
			removeButton.setAlignmentX(Component.CENTER_ALIGNMENT);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			addRemovePanel.Controls.Add(removeButton);
			
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
			pieceIdLabel.setAlignmentX(Component.CENTER_ALIGNMENT);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			addRemovePanel.Controls.Add(pieceIdLabel);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controls.Controls.Add(addRemovePanel);
			
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			inUsePanel.setLayout(new BoxLayout(inUsePanel, 1));
			
			inUseList.Items.Clear();
			//UPGRADE_TODO: Method 'javax.swing.JList.setModel' was converted to 'System.Windows.Forms.ListBox.Items.AddRange' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetModel_javaxswingListModel'"
			inUseList.Items.AddRange(inUseModel);
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			inUseList.SelectionMode = (System.Windows.Forms.SelectionMode) System.Windows.Forms.SelectionMode.One;
			//UPGRADE_ISSUE: Method 'javax.swing.JList.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListsetCellRenderer_javaxswingListCellRenderer'"
			inUseList.setCellRenderer(r);
			inUseList.SelectedValueChanged += new System.EventHandler(new AnonymousClassListSelectionListener1(this).valueChanged);
			
			inUseList.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.counters.PieceDefiner.mouseDown);
			inUseList.MouseUp += new System.Windows.Forms.MouseEventHandler(new AnonymousClassMouseAdapter(this).mouseReleased);
			inUseScroll.Controls.Add(inUseList);
			
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(inUseScroll.CreateGraphics(), 0, 0, inUseScroll.Width, inUseScroll.Height, new TitledBorder("Current Traits"));
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			inUsePanel.Controls.Add(inUseScroll);
			
			
			propsButton.Text = "Properties";
			propsButton.Click += new System.EventHandler(new AnonymousClassActionListener4(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(propsButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			inUsePanel.Controls.Add(propsButton);
			
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controls.Controls.Add(inUsePanel);
			
			
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			moveUpDownPanel.setLayout(new BoxLayout(moveUpDownPanel, BoxLayout.Y_AXIS));
			
			moveUpButton.Text = "Move Up";
			moveUpButton.Click += new System.EventHandler(new AnonymousClassActionListener5(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(moveUpButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			moveUpDownPanel.Controls.Add(moveUpButton);
			
			
			moveDownButton.Text = "Move Down";
			moveDownButton.Click += new System.EventHandler(new AnonymousClassActionListener6(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(moveDownButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			moveUpDownPanel.Controls.Add(moveDownButton);
			
			copyButton.Text = "Copy";
			copyButton.Click += new System.EventHandler(new AnonymousClassActionListener7(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(copyButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			moveUpDownPanel.Controls.Add(copyButton);
			
			pasteButton.Text = "Paste";
			pasteButton.Enabled = clipBoard != null;
			pasteButton.Click += new System.EventHandler(new AnonymousClassActionListener8(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(pasteButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			moveUpDownPanel.Controls.Add(pasteButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controls.Controls.Add(moveUpDownPanel);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(controls);
		}
		
		protected internal virtual void  paste()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Decorator c = (Decorator) GameModule.getGameModule().createPiece(clipBoard.Type, null);
			if (c is PlaceMarker)
			{
				((PlaceMarker) c).updateGpId(GameModule.getGameModule().getGpIdSupport());
			}
			c.setInner((GamePiece) SupportClass.ListBoxObjectCollectionSupport.LastElement(inUseModel));
			inUseModel.Add(c);
			c.mySetState(clipBoard.State);
			refresh();
		}
		
		protected internal virtual void  moveDecoratorDown(int index)
		{
			GamePiece selm1 = (GamePiece) inUseModel[index - 1];
			Decorator sel = (Decorator) inUseModel[index];
			Decorator selp1 = (Decorator) inUseModel[index + 1];
			Decorator selp2 = index < inUseModel.Count - 2?(Decorator) inUseModel[index + 2]:null;
			selp1.setInner(selm1);
			sel.setInner(selp1);
			if (selp2 != null)
			{
				selp2.setInner(sel);
			}
			inUseModel[index] = selp1;
			inUseModel[index + 1] = sel;
			((GamePiece) SupportClass.ListBoxObjectCollectionSupport.LastElement(inUseModel)).setProperty(VassalSharp.counters.Properties_Fields.OUTER, (System.Object) null);
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			inUseList.SelectedIndex = index + 1;
			refresh();
			Changed = true;
		}
		
		protected internal virtual void  moveDecoratorUp(int index)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'selm2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece selm2 = (GamePiece) inUseModel[index - 2];
			//UPGRADE_NOTE: Final was removed from the declaration of 'sel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Decorator sel = (Decorator) inUseModel[index];
			//UPGRADE_NOTE: Final was removed from the declaration of 'selm1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Decorator selm1 = (Decorator) inUseModel[index - 1];
			//UPGRADE_NOTE: Final was removed from the declaration of 'selp1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Decorator selp1 = index < inUseModel.Count - 1?(Decorator) inUseModel[index + 1]:null;
			sel.setInner(selm2);
			selm1.setInner(sel);
			if (selp1 != null)
			{
				selp1.setInner(selm1);
			}
			inUseModel[index] = selm1;
			inUseModel[index - 1] = sel;
			((GamePiece) SupportClass.ListBoxObjectCollectionSupport.LastElement(inUseModel)).setProperty(VassalSharp.counters.Properties_Fields.OUTER, (System.Object) null);
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			inUseList.SelectedIndex = index - 1;
			refresh();
			Changed = true;
		}
		
		protected internal virtual void  importPiece(System.String className)
		{
			if (className == null)
				return ;
			
			System.Object o = null;
			try
			{
				o = GameModule.getGameModule().getDataArchive().loadClass(className).getConstructor().newInstance();
			}
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception t)
			{
				ReflectionUtils.handleImportClassFailure(t, className);
			}
			
			if (o == null)
				return ;
			
			if (o is GamePiece)
			{
				availableModel.Add((GamePiece) o);
			}
			else
			{
				ErrorDialog.show("Error.not_a_gamepiece", className);
			}
		}
		
		private void  showHelpForPiece()
		{
			System.Object o = availableList.SelectedItem;
			if (o is EditablePiece)
			{
				HelpFile h = ((EditablePiece) o).HelpFile;
				BrowserSupport.openURL(h.Contents.ToString());
			}
		}
		
		protected internal virtual bool edit(int index)
		{
			System.Object o = inUseModel[index];
			if (!(o is EditablePiece))
			{
				return false;
			}
			EditablePiece p = (EditablePiece) o;
			if (p.getEditor() != null)
			{
				Ed ed = null;
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) this.Parent;
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				if (w is System.Windows.Forms.Form)
				{
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					ed = new Ed((System.Windows.Forms.Form) w, p);
				}
				else if (w is System.Windows.Forms.Form)
				{
					ed = new Ed((System.Windows.Forms.Form) w, p);
				}
				else
				{
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					ed = new Ed((System.Windows.Forms.Form) null, p);
				}
				//UPGRADE_NOTE: Final was removed from the declaration of 'oldState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String oldState = p.State;
				//UPGRADE_NOTE: Final was removed from the declaration of 'oldType '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String oldType = p.Type;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				ed.Visible = true;
				PieceEditor c = ed.Editor;
				if (c != null)
				{
					p.mySetType(c.Type);
					if (p is Decorator)
					{
						((Decorator) p).mySetState(c.State);
					}
					else
					{
						p.State = c.State;
					}
					if ((!p.Type.Equals(oldType)) || (!p.State.Equals(oldState)))
					{
						Changed = true;
					}
					refresh();
					return true;
				}
			}
			return false;
		}
		
		/// <summary>A Dialog for editing an EditablePiece's properties </summary>
		[Serializable]
		protected internal class Ed:System.Windows.Forms.Form
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener9' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener9
			{
				public AnonymousClassActionListener9(Ed enclosingInstance)
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
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					Enclosing_Instance.Dispose();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener10' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener10
			{
				public AnonymousClassActionListener10(Ed enclosingInstance)
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
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					Enclosing_Instance.ed = null;
					Enclosing_Instance.Dispose();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener11' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener11
			{
				public AnonymousClassActionListener11(VassalSharp.counters.EditablePiece p, Ed enclosingInstance)
				{
					InitBlock(p, enclosingInstance);
				}
				private void  InitBlock(VassalSharp.counters.EditablePiece p, Ed enclosingInstance)
				{
					this.p = p;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable p was copied into class AnonymousClassActionListener11. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.counters.EditablePiece p;
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
				{
					p.HelpFile.showWindow(Enclosing_Instance);
				}
			}
			virtual public PieceEditor Editor
			{
				get
				{
					return ed;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			internal PieceEditor ed;
			
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			internal Ed(System.Windows.Forms.Form owner, EditablePiece p):base()
			{
				//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_javalangString_boolean'"
				SupportClass.DialogSupport.SetDialog(this, owner, p.Description + " properties");
				initialize(p);
			}
			
			internal Ed(System.Windows.Forms.Form owner, EditablePiece p):base()
			{
				//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtDialog_javalangString_boolean'"
				SupportClass.DialogSupport.SetDialog(this, owner, p.Description + " properties");
				initialize(p);
			}
			
			private void  initialize(EditablePiece p)
			{
				ed = p.getEditor();
				setLayout(new MigLayout("ins dialog,fill", "[]unrel[]", ""));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(ed.Controls);
				ed.Controls.Dock = new System.Windows.Forms.DockStyle();
				ed.Controls.BringToFront();
				
				System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton("Ok");
				b.Click += new System.EventHandler(new AnonymousClassActionListener9(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(b);
				b.Dock = new System.Windows.Forms.DockStyle();
				b.BringToFront();
				
				b = SupportClass.ButtonSupport.CreateStandardButton("Cancel");
				b.Click += new System.EventHandler(new AnonymousClassActionListener10(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(b);
				b.Dock = new System.Windows.Forms.DockStyle();
				b.BringToFront();
				
				if (p.HelpFile != null)
				{
					b = SupportClass.ButtonSupport.CreateStandardButton("Help");
					b.Click += new System.EventHandler(new AnonymousClassActionListener11(p, this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(b);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					Controls.Add(b);
					b.Dock = new System.Windows.Forms.DockStyle();
					b.BringToFront();
				}
				
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				pack();
				System.Windows.Forms.Form generatedAux8 = Owner;
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			}
			#region Windows Form Designer generated code
			private void  InitializeComponent()
			{
				this.SuspendLayout();
				this.ResumeLayout(false);
			}
			#endregion
		}
		
		protected internal virtual void  removeTrait(int index)
		{
			inUseModel.RemoveAt(index);
			if (index < inUseModel.Count)
			{
				((Decorator) inUseModel[index]).setInner((GamePiece) inUseModel[index - 1]);
			}
			refresh();
			Changed = true;
		}
		
		protected internal virtual void  addTrait(Decorator c)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Class < ? extends Decorator > cClass = c.getClass();
			Decorator d = null;
			try
			{
				d = cClass.getConstructor().newInstance();
			}
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception t)
			{
				ReflectionUtils.handleNewInstanceFailure(t, cClass);
			}
			
			if (d != null)
			{
				if (d is PlaceMarker)
				{
					((PlaceMarker) d).updateGpId(gpidSupport);
				}
				d.setInner((GamePiece) SupportClass.ListBoxObjectCollectionSupport.LastElement(inUseModel));
				inUseModel.Add(d);
				Changed = true;
			}
			
			refresh();
		}
		
		private System.Windows.Forms.Panel availablePanel;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.ScrollableControl availableScroll;
		protected internal System.Windows.Forms.ListBox availableList;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Button importButton;
		private System.Windows.Forms.Panel addRemovePanel;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Panel inUsePanel;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.ScrollableControl inUseScroll;
		private System.Windows.Forms.ListBox inUseList;
		private System.Windows.Forms.Button propsButton;
		private System.Windows.Forms.Panel moveUpDownPanel;
		private System.Windows.Forms.Button moveUpButton;
		private System.Windows.Forms.Button moveDownButton;
		protected internal System.Windows.Forms.Button copyButton;
		protected internal System.Windows.Forms.Button pasteButton;
		
		//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
		[Serializable]
		private class Renderer:DefaultListCellRenderer
		{
			private const long serialVersionUID = 1L;
			
			public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool selected, bool hasFocus)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
				base.getListCellRendererComponent(list, value_Renamed, index, selected, hasFocus);
				if (value_Renamed is EditablePiece)
				{
					Text = ((EditablePiece) value_Renamed).Description;
				}
				else
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String s = value_Renamed.GetType().FullName;
					Text = s.Substring(s.LastIndexOf('.') + 1);
				}
				return this;
			}
		}
		
		/// <summary> Contents of the Copy/Paste buffer for traits in the editor</summary>
		/// <author>  rkinney
		/// 
		/// </author>
		//UPGRADE_NOTE: Inner class 'TraitClipboard' is now serializable, and this may become a security issue. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1234'"
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		protected internal class TraitClipboard
		{
			virtual public System.String Type
			{
				get
				{
					return type;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return state;
				}
				
			}
			private System.String type;
			private System.String state;
			public TraitClipboard(Decorator copy)
			{
				type = copy.myGetType();
				state = copy.myGetState();
			}
		}
	}
}