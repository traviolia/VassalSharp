/*
* $Id$
*
* Copyright (c) 2005-2006 by Rodney Kinney, Brent Easton,
* Torsten Spindler, and Scot McConnachie
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
//UPGRADE_TODO: The type 'java.util.regex.Matcher' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Matcher = java.util.regex.Matcher;
//UPGRADE_TODO: The type 'java.util.regex.Pattern' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Pattern = java.util.regex.Pattern;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using MenuDisplayer = VassalSharp.build.module.map.MenuDisplayer;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using Configurer = VassalSharp.configure.Configurer;
using ConfigurerFactory = VassalSharp.configure.ConfigurerFactory;
using GamePieceFormattedStringConfigurer = VassalSharp.configure.GamePieceFormattedStringConfigurer;
using HotKeyConfigurer = VassalSharp.configure.HotKeyConfigurer;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using BasicPiece = VassalSharp.counters.BasicPiece;
using BoundsTracker = VassalSharp.counters.BoundsTracker;
using Decorator = VassalSharp.counters.Decorator;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceCloner = VassalSharp.counters.PieceCloner;
using PieceFilter = VassalSharp.counters.PieceFilter;
using PieceIterator = VassalSharp.counters.PieceIterator;
using Properties = VassalSharp.counters.Properties;
using PropertiesPieceFilter = VassalSharp.counters.PropertiesPieceFilter;
using Stack = VassalSharp.counters.Stack;
using Resources = VassalSharp.i18n.Resources;
using TranslatableConfigurerFactory = VassalSharp.i18n.TranslatableConfigurerFactory;
using PositionOption = VassalSharp.preferences.PositionOption;
using FormattedString = VassalSharp.tools.FormattedString;
using LaunchButton = VassalSharp.tools.LaunchButton;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using ScrollPane = VassalSharp.tools.ScrollPane;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.build.module
{
	
	public class Inventory:AbstractConfigurable, GameComponent, PlayerRoster.SideChangeListener
	{
		static private System.Int32 state132;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(Inventory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Inventory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Inventory enclosingInstance;
			public Inventory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.launch();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTreeSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassTreeSelectionListener
		{
			public AnonymousClassTreeSelectionListener(Inventory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Inventory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Inventory enclosingInstance;
			public Inventory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
			{
				if (Enclosing_Instance.centerOnPiece)
				{
					GamePiece piece = Enclosing_Instance.SelectedCounter;
					if (piece != null && piece.getMap() != null)
						piece.getMap().centerAt(piece.Position);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter
		{
			public AnonymousClassMouseAdapter(Inventory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(AnonymousClassMouseAdapter enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
				private class AnonymousClassRunnable : IThreadRunnable
				{
					public AnonymousClassRunnable(AnonymousClassPropertyChangeListener enclosingInstance)
					{
						InitBlock(enclosingInstance);
					}
					private void  InitBlock(AnonymousClassPropertyChangeListener enclosingInstance)
					{
						this.enclosingInstance = enclosingInstance;
					}
					private AnonymousClassPropertyChangeListener enclosingInstance;
					public AnonymousClassPropertyChangeListener Enclosing_Instance
					{
						get
						{
							return enclosingInstance;
						}
						
					}
					public virtual void  Run()
					{
						refresh();
					}
				}
				private void  InitBlock(AnonymousClassMouseAdapter enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private AnonymousClassMouseAdapter enclosingInstance;
				public AnonymousClassMouseAdapter Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				//$NON-NLS-1$
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					if (false.Equals(evt.NewValue))
					{
						//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
						SwingUtilities.invokeLater(new AnonymousClassRunnable(this));
					}
				}
			}
			private void  InitBlock(Inventory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Inventory enclosingInstance;
			public Inventory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
				if (Enclosing_Instance.showMenu && e.isMetaDown())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.getPathForLocation' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetPathForLocation_int_int'"
					TreePath path = Enclosing_Instance.tree.getPathForLocation(e.X, e.Y);
					if (path != null)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
						if (path.getLastPathComponent() is CounterNode)
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
							CounterNode node = (CounterNode) path.getLastPathComponent();
							//UPGRADE_NOTE: Final was removed from the declaration of 'piece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							GamePiece piece = node.Counter.Piece;
							if (piece != null)
							{
								//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
								System.Windows.Forms.ContextMenu menu = MenuDisplayer.createPopup(piece);
								//UPGRADE_ISSUE: Method 'javax.swing.JComponent.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentaddPropertyChangeListener_javalangString_javabeansPropertyChangeListener'"
								menu.addPropertyChangeListener("visible", new AnonymousClassPropertyChangeListener(this));
								//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
								menu.Show(Enclosing_Instance.tree, new System.Drawing.Point(e.X, e.Y));
							}
						}
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultTreeCellRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class 'javax.swing.tree.DefaultTreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
		[Serializable]
		private class AnonymousClassDefaultTreeCellRenderer:DefaultTreeCellRenderer
		{
			public AnonymousClassDefaultTreeCellRenderer(Inventory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassIcon' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassIcon : System.Drawing.Image
			{
				public AnonymousClassIcon(System.Drawing.Rectangle r, VassalSharp.counters.GamePiece piece, AnonymousClassDefaultTreeCellRenderer enclosingInstance)
				{
					InitBlock(r, piece, enclosingInstance);
				}
				private void  InitBlock(System.Drawing.Rectangle r, VassalSharp.counters.GamePiece piece, AnonymousClassDefaultTreeCellRenderer enclosingInstance)
				{
					this.r = r;
					this.piece = piece;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable r was copied into class AnonymousClassIcon. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Drawing.Rectangle r;
				//UPGRADE_NOTE: Final variable piece was copied into class AnonymousClassIcon. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.counters.GamePiece piece;
				private AnonymousClassDefaultTreeCellRenderer enclosingInstance;
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				new virtual public int Height
				{
					get
					{
						return r.Height;
					}
					
				}
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				new virtual public int Width
				{
					get
					{
						return r.Width;
					}
					
				}
				public AnonymousClassDefaultTreeCellRenderer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.Icon.paintIcon' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public virtual void  paintIcon(System.Windows.Forms.Control c, System.Drawing.Graphics g, int x, int y)
				{
					piece.draw(g, - r.X, - r.Y, c, Enclosing_Instance.Enclosing_Instance.pieceZoom);
				}
			}
			private void  InitBlock(Inventory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Inventory enclosingInstance;
			public Inventory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = - 250332615261355856L;
			
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool sel, bool expanded, bool leaf, int row, bool hasFocus)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.getTreeCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
				base.getTreeCellRendererComponent(tree, value_Renamed, sel, expanded, leaf && !Enclosing_Instance.foldersOnly, row, hasFocus);
				if (value_Renamed is CounterNode)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'piece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece piece = ((CounterNode) value_Renamed).Counter.Piece;
					if (piece != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Rectangle r = System.Drawing.Rectangle.Truncate(piece.Shape.GetBounds());
						//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
						r.X = (int) System.Math.Round(r.X * Enclosing_Instance.pieceZoom);
						//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
						r.Y = (int) System.Math.Round(r.Y * Enclosing_Instance.pieceZoom);
						//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
						r.Width = (int) System.Math.Round(r.Width * Enclosing_Instance.pieceZoom);
						//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
						r.Height = (int) System.Math.Round(r.Height * Enclosing_Instance.pieceZoom);
						Image = Enclosing_Instance.drawPieces?new AnonymousClassIcon(r, piece, this):null;
					}
				}
				return this;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(Inventory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Inventory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Inventory enclosingInstance;
			public Inventory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.inventoryToText();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(Inventory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Inventory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Inventory enclosingInstance;
			public Inventory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				refresh();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(Inventory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Inventory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Inventory enclosingInstance;
			public Inventory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(Enclosing_Instance.frame, "Visible", false);
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state132 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state132 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				String.
			}
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.Inventory.component_type"); //$NON-NLS-1$
			}
			
		}
		virtual public GamePiece SelectedCounter
		{
			get
			{
				GamePiece piece = null;
				CounterNode node = (CounterNode) tree.SelectedNode;
				if (node != null && node.Leaf)
				{
					piece = node.Counter.Piece;
				}
				return piece;
			}
			
		}
		virtual protected internal System.Windows.Forms.Control Component
		{
			get
			{
				return launch_Renamed_Field;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString(Resources.NAME_LABEL), Resources.getString(Resources.BUTTON_TEXT), Resources.getString(Resources.TOOLTIP_TEXT), Resources.getString(Resources.BUTTON_ICON), Resources.getString(Resources.HOTKEY_LABEL), Resources.getString("Editor.Inventory.show_pieces"), Resources.getString("Editor.Inventory.sort_group_properties"), Resources.getString("Editor.Inventory.label_folders"), Resources.getString("Editor.Inventory.show_folders"), Resources.getString("Editor.Inventory.label_pieces"), Resources.getString("Editor.Inventory.sort"), Resources.getString("Editor.Inventory.label_sort"), Resources.getString("Editor.Inventory.sort_method"), Resources.getString("Editor.Inventory.center_piece"), Resources.getString("Editor.Inventory.forward_keystroke"), Resources.getString("Editor.Inventory.rightclick_piece"), Resources.getString("Editor.Inventory.draw_piece"), Resources.getString("Editor.Inventory.zoom"), Resources.getString("Editor.Inventory.available")};
			}
			
		}
		protected internal LaunchButton launch_Renamed_Field;
		protected internal CounterInventory results;
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.TreeView tree;
		
		public const System.String VERSION = "2.1"; //$NON-NLS-1$
		public const System.String HOTKEY = "hotkey"; //$NON-NLS-1$
		public const System.String BUTTON_TEXT = "text"; //$NON-NLS-1$
		public const System.String NAME = "name"; //$NON-NLS-1$
		public const System.String ICON = "icon"; //$NON-NLS-1$
		public const System.String TOOLTIP = "tooltip"; //$NON-NLS-1$
		// public static final String DEST = "destination";
		
		/*
		* For use in formatted text output.
		*/
		protected internal System.String mapSeparator = "\n"; //$NON-NLS-1$
		protected internal System.String groupSeparator = "   "; //$NON-NLS-1$
		
		/*
		* Options Destination - Chat, Dialog, File.
		*/
		//  public static final String DEST_CHAT = "Chat Window";
		//  public static final String DEST_DIALOG = "Dialog Window";
		//  public static final String DEST_TREE = "Tree Window";
		//  public static final String[] DEST_OPTIONS = { DEST_CHAT, DEST_DIALOG, DEST_TREE };
		//  protected String destination = DEST_TREE;
		
		public const System.String FILTER = "include"; //$NON-NLS-1$
		protected internal PropertyExpression piecePropertiesFilter = new PropertyExpression(); //$NON-NLS-1$
		
		public const System.String GROUP_BY = "groupBy"; //$NON-NLS-1$
		protected internal System.String[] groupBy = new System.String[]{""}; //$NON-NLS-1$
		
		public const System.String NON_LEAF_FORMAT = "nonLeafFormat"; //$NON-NLS-1$
		protected internal System.String nonLeafFormat = "$PropertyValue$"; //$NON-NLS-1$
		
		public const System.String CENTERONPIECE = "centerOnPiece"; //$NON-NLS-1$
		protected internal bool centerOnPiece = true;
		
		public const System.String FORWARD_KEYSTROKE = "forwardKeystroke"; //$NON-NLS-1$
		protected internal bool forwardKeystroke = true;
		
		public const System.String SHOW_MENU = "showMenu"; //$NON-NLS-1$
		protected internal bool showMenu = true;
		
		public const System.String SIDES = "sides"; //$NON-NLS-1$
		protected internal System.String[] sides = null;
		
		public const System.String KEYSTROKE = "keystroke"; //$NON-NLS-1$
		protected internal System.Windows.Forms.KeyEventArgs keyStroke = null;
		
		public const System.String CUTBELOWROOT = "cutRoot"; //$NON-NLS-1$
		protected internal int cutBelowRoot = 0;
		
		public const System.String CUTABOVELEAVES = "cutLeaves"; //$NON-NLS-1$
		protected internal int cutAboveLeaves = 0;
		
		public const System.String LEAF_FORMAT = "leafFormat"; //$NON-NLS-1$
		protected internal System.String pieceFormat = "$PieceName$"; //$NON-NLS-1$
		
		public const System.String PIECE_ZOOM = "pieceZoom"; //$NON-NLS-1$
		protected internal double pieceZoom = .25;
		
		public const System.String DRAW_PIECES = "drawPieces"; //$NON-NLS-1$
		protected internal bool drawPieces = true;
		
		public const System.String FOLDERS_ONLY = "foldersOnly"; //$NON-NLS-1$
		protected internal bool foldersOnly = false;
		
		public const System.String SORT_PIECES = "sortPieces"; //$NON-NLS-1$
		protected internal bool sortPieces = true;
		
		public const System.String SORT_FORMAT = "sortFormat"; //$NON-NLS-1$
		protected internal System.String sortFormat = "$PieceName$"; //$NON-NLS-1$
		
		public const System.String ALPHA = "alpha"; //$NON-NLS-1$
		public const System.String LENGTHALPHA = "length"; //$NON-NLS-1$
		public const System.String NUMERIC = "numeric"; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'SORT_OPTIONS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String[] SORT_OPTIONS = new System.String[]{ALPHA, LENGTHALPHA, NUMERIC};
		
		protected internal System.String sortStrategy = ALPHA;
		
		public const System.String SORTING = "sorting"; //$NON-NLS-1$
		
		protected internal System.Windows.Forms.Form frame;
		
		public Inventory()
		{
			InitBlock();
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener al = new AnonymousClassActionListener(this);
			launch_Renamed_Field = new LaunchButton(null, TOOLTIP, BUTTON_TEXT, HOTKEY, ICON, al);
			setAttribute(NAME, Resources.getString("Inventory.inventory")); //$NON-NLS-1$
			setAttribute(BUTTON_TEXT, Resources.getString("Inventory.inventory")); //$NON-NLS-1$
			setAttribute(TOOLTIP, Resources.getString("Inventory.show_inventory")); //$NON-NLS-1$
			setAttribute(ICON, "/images/inventory.gif"); //$NON-NLS-1$
			launch_Renamed_Field.Enabled = false;
			launch_Renamed_Field.Visible = false;
		}
		
		public override void  addTo(Buildable b)
		{
			// Support for players changing sides
			PlayerRoster.addSideChangeListener(this);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
			launch_Renamed_Field.setAlignmentY(0.0F);
			GameModule.getGameModule().getToolBar().add(Component);
			GameModule.getGameModule().getGameState().addGameComponent(this);
			frame = new JDialog(GameModule.getGameModule().getFrame());
			frame.Text = getConfigureName();
			System.String key = "Inventory." + getConfigureName(); //$NON-NLS-1$
			GameModule.getGameModule().getPrefs().addOption(new PositionOption(key, frame));
			//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			frame.setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) frame), BoxLayout.Y_AXIS));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = initTree();
			frame.Controls.Add(temp_Control);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = initButtons();
			frame.Controls.Add(temp_Control2);
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			frame.Size = new System.Drawing.Size(250, 350);
		}
		
		/// <summary> Construct an explorer like interface for the selected counters</summary>
		protected internal virtual System.Windows.Forms.Control initTree()
		{
			// Initialize the tree to be displayed from the results tree
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			tree = new System.Windows.Forms.TreeView();
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setRootVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetRootVisible_boolean'"
			tree.setRootVisible(false);
			tree.ShowRootLines = true;
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetCellRenderer_javaxswingtreeTreeCellRenderer'"
			tree.setCellRenderer(initTreeCellRenderer());
			//UPGRADE_ISSUE: Method 'javax.swing.tree.TreeSelectionModel.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetSelectionModel'"
			//UPGRADE_ISSUE: Field 'javax.swing.tree.TreeSelectionModel.SINGLE_TREE_SELECTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			tree.getSelectionModel().setSelectionMode(TreeSelectionModel.SINGLE_TREE_SELECTION);
			// If wanted center on a selected counter
			tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(new AnonymousClassTreeSelectionListener(this).valueChanged);
			tree.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.Inventory.mouseDown);
			tree.MouseUp += new System.Windows.Forms.MouseEventHandler(new AnonymousClassMouseAdapter(this).mouseReleased);
			tree.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.Inventory.keyDown);
			tree.KeyDown += new System.Windows.Forms.KeyEventHandler(new HotKeySender().keyPressed);
			tree.KeyUp += new System.Windows.Forms.KeyEventHandler(new HotKeySender().keyReleased);
			tree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(new HotKeySender().keyTyped);
			
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
			System.Windows.Forms.ScrollableControl scrollPane = new ScrollPane(tree, true, true);
			refresh();
			return scrollPane;
		}
		
		//UPGRADE_ISSUE: Interface 'javax.swing.tree.TreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeCellRenderer'"
		protected internal virtual TreeCellRenderer initTreeCellRenderer()
		{
			return new AnonymousClassDefaultTreeCellRenderer(this);
		}
		
		protected internal virtual System.Windows.Forms.Control initButtons()
		{
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box buttonBox = Box.createHorizontalBox();
			// Written by Scot McConnachie.
			System.Windows.Forms.Button writeButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.SAVE));
			writeButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(writeButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(writeButton);
			System.Windows.Forms.Button refreshButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.REFRESH));
			refreshButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(refreshButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(refreshButton);
			System.Windows.Forms.Button closeButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CLOSE));
			closeButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(closeButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(closeButton);
			return buttonBox;
		}
		
		/// <author>  Scot McConnachie.
		/// Writes the inventory text data to a user selected file.
		/// This allows a module designer to use Inventory to create customized text
		/// reports from the game.
		/// </author>
		/// <author>  spindler
		/// Changed FileChooser to use the new Vassal.tool.FileChooser
		/// Changed Separator before getResultString call
		/// TODO add check for existing file
		/// TODO rework text display of Inventory
		/// </author>
		protected internal virtual void  inventoryToText()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'output '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder output = new StringBuilder(""); //$NON-NLS-1$
			FileChooser fc = GameModule.getGameModule().getFileChooser();
			if (fc.showSaveDialog() == FileChooser.CANCEL_OPTION)
				return ;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'file '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo file = fc.SelectedFile;
			
			// TODO replace this hack
			mapSeparator = System.Environment.NewLine; //$NON-NLS-1$
			// groupSeparator = mapSeparator + "  ";
			// groupSeparator = " ";
			output.append(results.ResultString);
			// .substring(1).replaceAll(
			//      mapSeparator, System.getProperty("line.separator"));
			
			System.IO.StreamWriter p = null;
			try
			{
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				//UPGRADE_TODO: Constructor 'java.io.FileWriter.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriterFileWriter_javaioFile'"
				//UPGRADE_TODO: Class 'java.io.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriter'"
				p = new System.IO.StreamWriter(new System.IO.StreamWriter(new System.IO.StreamWriter(file.FullName, false, System.Text.Encoding.Default).BaseStream, new System.IO.StreamWriter(file.FullName, false, System.Text.Encoding.Default).Encoding).BaseStream, new System.IO.StreamWriter(new System.IO.StreamWriter(file.FullName, false, System.Text.Encoding.Default).BaseStream, new System.IO.StreamWriter(file.FullName, false, System.Text.Encoding.Default).Encoding).Encoding);
				
				p.Write(output);
				//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.io.PrintWriter.close' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
				p.Close();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command c = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), Resources.getString("Inventory.wrote", file));
				c.execute();
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, file);
			}
			finally
			{
				IOUtils.closeQuietly(p);
			}
		}
		
		public override void  removeFrom(Buildable b)
		{
			GameModule.getGameModule().getToolBar().remove(Component);
			GameModule.getGameModule().getGameState().removeGameComponent(this);
		}
		
		public override void  add(Buildable b)
		{
		}
		
		public override void  remove(Buildable b)
		{
		}
		
		protected internal virtual void  launch()
		{
			refresh();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(frame, "Visible", true);
		}
		
		private void  buildTreeModel()
		{
			// Initialize all pieces with CurrentBoard correctly.
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(VassalSharp.build.module.Map m: VassalSharp.build.module.Map.getMapList())
			{
				m.getPieces();
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > path = new ArrayList < String >();
			for (int i = 0; i < groupBy.Length; i++)
				path.add(groupBy[i]);
			results = new CounterInventory(new Counter(this.getConfigureName()), path, sortPieces);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'pi '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceIterator pi = new PieceIterator(GameModule.getGameModule().getGameState().getAllPieces().iterator(), piecePropertiesFilter);
			
			while (pi.hasMoreElements())
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < String > groups = new ArrayList < String >();
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece p = pi.nextPiece();
				
				if (p is Decorator || p is BasicPiece)
				{
					for (int i = 0; i < groupBy.Length; i++)
					{
						if (groupBy[i].Length > 0)
						{
							System.String prop = (System.String) p.getProperty(groupBy[i]);
							if (prop != null)
								groups.add(prop);
						}
					}
					
					int count = 1;
					if (nonLeafFormat.Length > 0)
						count = getTotalValue(p);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Counter c = new Counter(p, groups, count, pieceFormat, sortFormat);
					// Store
					results.insert(c);
				}
			}
		}
		
		protected internal virtual int getTotalValue(GamePiece p)
		{
			System.String s = (System.String) p.getProperty(nonLeafFormat);
			int count = 1;
			try
			{
				count = System.Int32.Parse(s);
			}
			catch (System.FormatException e)
			{
				// Count each piece as 1 if the property isn't a number
				count = 1;
			}
			
			return count;
		}
		
		public override VassalSharp.build.module.documentation.HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("Inventory.htm"); //$NON-NLS-1$
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		IconConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		NamedKeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class,
		// DestConfig.class,
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		PropertyExpression.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String [].
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		PieceFormatConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		PieceFormatConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		SortConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Double.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String [].
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String [] getAttributeNames()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new String []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		NAME, 
		BUTTON_TEXT, 
		TOOLTIP, 
		ICON, 
		HOTKEY,
	// DEST,
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	FILTER, 
	GROUP_BY, 
	NON_LEAF_FORMAT, 
	FOLDERS_ONLY, 
	LEAF_FORMAT, 
	SORT_PIECES, 
	SORT_FORMAT, 
	SORTING, 
	CENTERONPIECE, 
	FORWARD_KEYSTROKE, 
	SHOW_MENU, 
	DRAW_PIECES, 
	PIECE_ZOOM, 
	SIDES
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class IconConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new IconConfigurer(key, name, "/images/inventory.gif"); //$NON-NLS-1$
		}
	}
	
	public class PieceFormatConfig : TranslatableConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new GamePieceFormattedStringConfigurer(key, name);
		}
	}
	//  public static class DestConfig implements ConfigurerFactory {
	//    public Configurer getConfigurer(AutoConfigurable c, String key, String name) {
	//      return new StringEnumConfigurer(key, name, DEST_OPTIONS);
	//    }
	//  }
	
	public class SortConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new StringEnumConfigurer(key, name, SORT_OPTIONS);
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setAttribute(String key, Object o)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setConfigureName((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FILTER.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		piecePropertiesFilter.setExpression((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(GROUP_BY.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(o instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		o = StringArrayConfigurer.stringToArray((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	groupBy =(String []) o;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(NON_LEAF_FORMAT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		nonLeafFormat =(String) o;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LEAF_FORMAT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		pieceFormat =(String) o;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(CENTERONPIECE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		centerOnPiece = getBooleanValue(o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_MENU.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showMenu = getBooleanValue(o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DRAW_PIECES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		drawPieces = getBooleanValue(o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FOLDERS_ONLY.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		foldersOnly = getBooleanValue(o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	cutAboveLeaves = foldersOnly ? 1: 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(PIECE_ZOOM.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(o instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		o = Double.valueOf((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	pieceZoom =((Double) o).doubleValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FORWARD_KEYSTROKE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		forwardKeystroke = getBooleanValue(o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SIDES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(o instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		o = StringArrayConfigurer.stringToArray((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	sides =(String []) o;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(KEYSTROKE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(o instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		o = HotKeyConfigurer.decode((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	keyStroke =(KeyStroke) o;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(CUTBELOWROOT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(o instanceof String) 
		cutBelowRoot = Integer.parseInt((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		cutBelowRoot =((Integer) o).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(CUTABOVELEAVES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(o instanceof String) 
		cutAboveLeaves = Integer.parseInt((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		cutAboveLeaves =((Integer) o).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SORT_PIECES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		sortPieces = getBooleanValue(o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SORT_FORMAT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		sortFormat =(String) o;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SORTING.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		sortStrategy =(String) o;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//    else if (DEST.equals(key)) {
	//      destination = (String) o;
	//    }
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		launch.setAttribute(key, o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private VisibilityCondition piecesVisible = new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return !foldersOnly;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public VisibilityCondition getAttributeVisibility(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(PIECE_ZOOM.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return drawPieces && !foldersOnly;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LEAF_FORMAT.equals(name) || CENTERONPIECE.equals(name) || FORWARD_KEYSTROKE.equals(name) || SHOW_MENU.equals(name) || DRAW_PIECES.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return piecesVisible;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return super.getAttributeVisibility(name);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <param name="o">
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected boolean getBooleanValue(Object o)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(o instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		o = Boolean.valueOf((String) o);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return((Boolean) o).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String key)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getConfigureName();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FILTER.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return piecePropertiesFilter.getExpression();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(GROUP_BY.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return StringArrayConfigurer.arrayToString(groupBy);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(NON_LEAF_FORMAT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return nonLeafFormat;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LEAF_FORMAT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return pieceFormat;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(CENTERONPIECE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(centerOnPiece);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FORWARD_KEYSTROKE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(forwardKeystroke);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_MENU.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(showMenu);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DRAW_PIECES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(drawPieces);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FOLDERS_ONLY.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(foldersOnly);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(PIECE_ZOOM.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(pieceZoom);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SIDES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return StringArrayConfigurer.arrayToString(sides);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(KEYSTROKE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return HotKeyConfigurer.encode(keyStroke);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(CUTBELOWROOT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return cutBelowRoot + ; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(CUTABOVELEAVES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return cutAboveLeaves + ; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SORT_PIECES.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return sortPieces + ; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SORT_FORMAT.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return sortFormat;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SORTING.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return sortStrategy;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//    else if (DEST.equals(key)) {
	//      return destination;
	//    }
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return launch.getAttributeValueString(key);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command getRestoreCommand()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setup(boolean gameStarting)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		launch.setEnabled(gameStarting && enabledForPlayersSide());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(gameStarting) 
	setupLaunch();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setupLaunch()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		launch.setEnabled(enabledForPlayersSide());
	// Only change button visibilty if it has not already been hidden by a ToolBarMenu
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(launch.getClientProperty(ToolbarMenu.HIDDEN_BY_TOOLBAR) == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		launch.setVisible(launch.isEnabled());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Update inventory according to change of side.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void sideChanged(String oldSide, String newSide)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setupLaunch();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected boolean enabledForPlayersSide()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(sides == null || sides.length == 0) 
		return true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < sides.length;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i ++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(sides [i].equalsIgnoreCase(PlayerRoster.getMySide())) 
		return true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return false;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//  protected void executeCommand() {
	//    if (destination.equals(DEST_CHAT)) {
	//      Command c = new NullCommand();
	//      String res[] = results.getResultStringArray();
	//
	//      for (int i = 0; i < res.length; i++) {
	//        c.append(new Chatter.DisplayText(GameModule.getGameModule().getChatter(), res[i]));
	//      }
	//      c.execute();
	//      GameModule.getGameModule().sendAndLog(c);
	//    }
	//    else if (destination.equals(DEST_DIALOG)) {
	//      String res[] = results.getResultStringArray();
	//      String text = "";
	//      for (int i = 0; i < res.length; i++) {
	//        text += res[i] + "\n";
	//      }
	//      JTextArea textArea = new JTextArea(text);
	//      textArea.setEditable(false);
	//
	//      JScrollPane scrollPane = new ScrollPane(textArea,
	//         JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED,
	//         JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
	//
	//      JOptionPane.showMessageDialog(GameModule.getGameModule().getFrame(), scrollPane, getConfigureName(), JOptionPane.PLAIN_MESSAGE);
	//    }
	//    else if (destination.equals(DEST_TREE)) {
	//      initTree();
	//    }
	//  }
	
	/// <returns> Command which only has some text in. The actual processing is done
	/// within the pieces.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Command sendHotKeyToPieces(final KeyStroke keyStroke)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Command c = new NullCommand();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final TreePath [] tp = tree.getSelectionPaths();
	// set to not get duplicates
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	HashSet < GamePiece > pieces = new HashSet < GamePiece >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < tp.length;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i ++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		CounterNode node =(CounterNode) tp [i].getLastPathComponent();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(node.isLeaf())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		pieces.add(node.getCounter().getPiece());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Iterator < CounterNode > j = node.iterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	j.hasNext();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final CounterNode childNode = j.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(childNode.isLeaf()) 
	pieces.add(childNode.getCounter().getPiece());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	for(GamePiece piece: pieces)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		GameModule.getGameModule().sendAndLog(piece.keyEvent(keyStroke));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Command myUndoCommand()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void refresh()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// Make an attempt to keep the same nodes expanded
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	HashSet < String > expanded = new HashSet < String >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0, n = tree.getRowCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < n;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(tree.isExpanded(i))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		expanded.add(tree.getPathForRow(i).getLastPathComponent().toString());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	buildTreeModel();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.setModel(results);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < tree.getRowCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(expanded.contains(
		tree.getPathForRow(i).getLastPathComponent().toString()))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		tree.expandRow(i);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class HotKeySender
	{
		internal BoundsTracker tracker;
		
		public virtual void  keyCommand(System.Windows.Forms.KeyEventArgs stroke)
		{
			if (forwardKeystroke)
			{
				CounterNode node = (CounterNode) tree.getLastSelectedPathComponent();
				if (node != null)
				{
					Command comm = getCommand(node, stroke);
					if (comm != null && !comm.Null)
					{
						tracker.repaint();
						GameModule.getGameModule().sendAndLog(comm);
						tracker = null;
						refresh();
					}
				}
			}
		}
		
		protected internal virtual Command getCommand(CounterNode node, System.Windows.Forms.KeyEventArgs stroke)
		{
			GamePiece p = node.Counter == null?null:node.Counter.Piece;
			Command comm = null;
			if (p != null)
			{
				// Save state first
				p.setProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT, PieceCloner.Instance.clonePiece(p));
				if (tracker == null)
				{
					tracker = new BoundsTracker();
					tracker.addPiece(p);
				}
				comm = p.keyEvent(stroke);
			}
			else
			{
				comm = new NullCommand();
				for (int i = 0, n = node.ChildCount; i < n; ++i)
				{
					comm = comm.append(getCommand((CounterNode) node.getChild(i), stroke));
				}
			}
			return comm;
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			keyCommand(KeyStroke.getKeyStrokeForEvent(event_sender, e));
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			keyCommand(KeyStroke.getKeyStrokeForEvent(event_sender, e));
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			keyCommand(KeyStroke.getKeyStrokeForEvent(event_sender, e));
		}
	}
	
	//  public static class Dest extends StringEnum {
	//    public String[] getValidValues(AutoConfigurable target) {
	//      return new String[] {DEST_CHAT, DEST_DIALOG, DEST_TREE};
	//    }
	//  }
	//  public static class SortStrategy extends StringEnum {
	//    public String[] getValidValues(AutoConfigurable target) {
	//      return new String[] {ALPHA, LENGTHALPHA, NUMERIC};
	//    }
	//  }
	
	/// <summary> Holds static information of and a reference to a gamepiece. Pay attention
	/// to the equals method. It checks if two pieces can be found under the same
	/// path!
	/// 
	/// </summary>
	/// <author>  Brent Easton and Torsten Spindler
	/// 
	/// </author>
	public class Counter : PropertySource
	{
		private void  InitBlock()
		{
			this.piece = piece;
			this.value_Renamed = value_Renamed;
			this.groups = groups;
			this.format = new FormattedString(format);
			this.sortingFormat = new FormattedString(sortFormat);
		}
		virtual public System.String Name
		{
			// piece can be null, so provide a alternate name
			
			get
			{
				if (piece != null)
					return piece.getName();
				return localName;
			}
			
		}
		virtual public System.String[] Path
		{
			get
			{
				return groups.toArray(new System.String[groups.size()]);
			}
			
		}
		virtual public int Value
		{
			get
			{
				return value_Renamed;
			}
			
			set
			{
				this.value_Renamed = value;
			}
			
		}
		virtual public GamePiece Piece
		{
			get
			{
				return piece;
			}
			
			set
			{
				this.piece = value;
			}
			
		}
		virtual public CounterNode Node
		{
			set
			{
				this.node = value;
			}
			
		}
		// The gamepiece is stored here to allow dynamic changes of name, location
		// and so forth
		protected internal GamePiece piece;
		protected internal GamePiece source;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < String > groups;
		protected internal int value_Renamed;
		// Only used when no piece is defined
		protected internal System.String localName;
		protected internal FormattedString format;
		protected internal FormattedString sortingFormat;
		protected internal CounterNode node;
		
		public Counter(System.String name):this(name, null)
		{
		}
		
		public Counter(System.String name, GamePiece p):this(null, null, 0, nonLeafFormat, sortFormat)
		{
			this.localName = name;
			this.source = p;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Counter(GamePiece piece, List < String > groups, int value, 
		String format, String sortFormat)
		
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}
		
		public override System.String ToString()
		{
			return format.getLocalizedText(this);
		}
		public virtual System.String toSortKey()
		{
			return sortingFormat.getLocalizedText(this);
		}
		
		public  override bool Equals(System.Object o)
		{
			if (!(o is Counter))
				return false;
			Counter c = (Counter) o;
			return Path.Equals(c.Path);
		}
		
		public virtual System.Object getProperty(System.Object key)
		{
			System.Object value_Renamed = null;
			System.String s = (System.String) key;
			if (s.StartsWith("sum_"))
			{
				//$NON-NLS-1$
				if (piece != null)
				{
					value_Renamed = piece.getProperty(s.Substring(4));
				}
				else
				{
					int sum = 0;
					int n = results.getChildCount(node);
					for (int i = 0; i < n; ++i)
					{
						try
						{
							CounterNode childNode = (CounterNode) results.getChild(node, i);
							sum += System.Int32.Parse((System.String) (childNode.Counter).getProperty(key));
						}
						catch (System.FormatException e)
						{
							// Count each piece as 1 if property isn't a number
							sum++;
						}
					}
					value_Renamed = System.Convert.ToString(sum);
				}
			}
			else if ("PropertyValue".Equals(s))
			{
				//$NON-NLS-1$
				return localName;
			}
			else if (piece != null)
			{
				value_Renamed = piece.getProperty(key);
			}
			else if (source != null)
			{
				value_Renamed = source.getProperty(key);
			}
			return value_Renamed;
		}
		
		public virtual System.Object getLocalizedProperty(System.Object key)
		{
			return getProperty(key);
		}
	}
	
	/// <summary> Filter to select pieces required
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	protected internal class Selector : PieceFilter
	{
		
		protected internal PieceFilter filter;
		
		public Selector(System.String include)
		{
			if (include != null && include.Length > 0)
			{
				filter = PropertiesPieceFilter.parse(include);
			}
		}
		
		public virtual bool accept(GamePiece piece)
		{
			// Honor visibility
			
			if (true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				return false;
			
			// Ignore Stacks, pieces are reported individually from GameState
			if (piece is Stack)
				return false;
			
			// Don't report pieces with no map
			if (piece.getMap() == null)
				return false;
			
			// Check for marker
			if (filter != null)
			{
				return filter.accept(piece);
			}
			
			// Default Accept piece
			return true;
		}
	}
	
	/// <summary> CounterNode for the result tree.
	/// 
	/// </summary>
	/// <author>  spindler
	/// 
	/// </author>
	public class CounterNode : System.IComparable
	{
		private void  InitBlock()
		{
			return children.iterator();
		}
		virtual public System.String Entry
		{
			get
			{
				return entry;
			}
			
		}
		virtual public Counter Counter
		{
			get
			{
				return counter;
			}
			
		}
		virtual public int ChildCount
		{
			get
			{
				return children.size();
			}
			
		}
		virtual public bool Leaf
		{
			get
			{
				return children.isEmpty();
			}
			
		}
		virtual public int Level
		{
			get
			{
				return level;
			}
			
			set
			{
				this.level = value;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< CounterNode >
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.String entry;
		//UPGRADE_NOTE: Final was removed from the declaration of 'counter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal Counter counter;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < CounterNode > children;
		protected internal int level;
		
		// protected int depth;
		
		public CounterNode(System.String entry, Counter counter, int level):this(entry, counter)
		{
			this.level = level;
		}
		
		protected internal CounterNode(System.String entry, Counter counter)
		{
			InitBlock();
			this.level = 0;
			// this.depth = 0;
			this.entry = entry;
			this.counter = counter;
			counter.Node = this;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			children = new ArrayList < CounterNode >();
		}
		
		public override System.String ToString()
		{
			if (counter != null)
			{
				return counter.ToString();
			}
			return Entry;
		}
		
		/// <summary> Places a separator between elements.
		/// The separator consists of an indent and a linebreak.
		/// </summary>
		/// <returns>
		/// </returns>
		protected internal virtual System.String separator()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sep '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder sep = new StringBuilder();
			
			if (Level > 0)
				sep.append(mapSeparator);
			for (int i = 0; i < Level; i++)
				sep.append(groupSeparator);
			return sep.toString();
		}
		
		public virtual System.String toResultString()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder name = new StringBuilder();
			
			name.append(separator());
			
			if (counter != null)
			{
				name.append(counter.ToString());
			}
			else
				name.append(Entry);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(CounterNode child: children)
			{
				name.append(child.toResultString());
			}
			return name.toString();
		}
		
		public virtual void  addChild(CounterNode counterNode, bool sort)
		{
			children.add(counterNode);
			if (sort)
				sortChildren();
		}
		
		public virtual void  addChild(int i, CounterNode counterNode, bool sort)
		{
			children.add(i, counterNode);
			if (sort)
				sortChildren();
		}
		
		protected internal virtual void  sortChildren()
		{
			if (sortStrategy.equals(ALPHA))
				Collections.sort(children);
			else if (sortStrategy.equals(LENGTHALPHA))
				Collections.sort(children, new LengthAlpha(this));
			else if (sortStrategy.equals(NUMERIC))
				Collections.sort(children, new Numerical(this));
			else
				Collections.sort(children);
		}
		
		public virtual void  removeChild(CounterNode child)
		{
			children.remove(child);
		}
		
		public virtual System.Object getChild(int index)
		{
			return children.get_Renamed(index);
		}
		
		public virtual int getIndexOfChild(System.Object child)
		{
			return children.indexOf(child);
		}
		
		public virtual int updateValues()
		{
			int value_Renamed = 0;
			if (counter != null)
				value_Renamed = counter.Value;
			
			// inform children about update
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(CounterNode child: children)
			{
				value_Renamed += child.updateValues();
			}
			
			// save new value in counter
			counter.Value = value_Renamed;
			return counter.Value;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < CounterNode > iterator()
		
		public virtual void  cutLevel(int cut)
		{
			if (cut == 0)
			{
				children.clear();
				return ;
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(CounterNode child: children)
			{
				child.cutLevel(cut - 1);
			}
		}
		
		public virtual void  cutLeaves()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < CounterNode > toBeRemoved = new ArrayList < CounterNode >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(CounterNode child: children)
			{
				if (child.Leaf)
					toBeRemoved.add(child);
				else
					child.cutLeaves();
			}
			children.removeAll(toBeRemoved);
		}
		
		/// <summary> Compare this CounterNode to another one based on the respective SortKeys.</summary>
		public virtual int compareTo(CounterNode node)
		{
			return String.CompareOrdinal(this.toSortKey(), node.toSortKey());
		}
		
		/// <summary> Sort this CounterNode by the counters key, if no counter use the label.
		/// If no children, use the name of the counterNode, probably could be
		/// $PropertyValue$ as well?
		/// 
		/// </summary>
		/// <returns> key as String
		/// </returns>
		protected internal virtual System.String toSortKey()
		{
			System.String sortKey = Entry;
			if (counter != null)
				sortKey = counter.toSortKey();
			if (!children.isEmpty())
			{
				sortKey = ToString();
			}
			return sortKey;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'CompareCounterNodes' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Base class for comparing two CounterNodes. Contains methods for
		/// sanity checking of arguments and comparing non-sane arguments.
		/// 
		/// </summary>
		/// <author>  spindler
		/// </author>
		protected internal class CompareCounterNodes
		{
			public CompareCounterNodes(CounterNode enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CounterNode enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CounterNode enclosingInstance;
			public CounterNode Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			/// <summary> Sanity check for arguments.</summary>
			/// <param name="arg0">
			/// </param>
			/// <param name="arg1">
			/// </param>
			/// <returns> true if arguments looks processable, false else
			/// </returns>
			protected internal virtual bool argsOK(System.Object arg0, System.Object arg1)
			{
				return (arg0 != null && arg1 != null && arg0 is CounterNode && arg1 is CounterNode);
			}
			
			protected internal virtual int compareStrangeArgs(System.Object arg0, System.Object arg1)
			{
				if (arg1.Equals(arg1))
					return 0;
				
				if (arg0 == null)
					return 1;
				if (arg1 == null)
					return - 1;
				if (arg0 is CounterNode && !(arg1 is CounterNode))
					return - 1;
				if (arg1 is CounterNode && !(arg0 is CounterNode))
					return 1;
				
				throw new System.ArgumentException("These CounterNodes are not strange!"); //$NON-NLS-1$
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Alpha' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Compare two CounterNodes based on the alphanumerical order of their
		/// SortKeys.
		/// 
		/// </summary>
		/// <author>  spindler
		/// </author>
		protected internal class Alpha:CompareCounterNodes, System.Collections.IComparer
		{
			public Alpha(CounterNode enclosingInstance):base(enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CounterNode enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CounterNode enclosingInstance;
			public new CounterNode Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< CounterNode >
			
			public virtual int compare(CounterNode left, CounterNode right)
			{
				if (!argsOK(left, right))
					return compareStrangeArgs(left, right);
				
				return left.compareTo(right);
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public System.Int32 Compare(System.Object x, System.Object y)
			{
				return 0;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Numerical' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Compare two CounterNodes based on the first integer value found in
		/// their SortKeys. If a CounterNodes SortKey does not contain an integer
		/// at all it is assigned the lowest available integer.
		/// 
		/// </summary>
		/// <author>  spindler
		/// </author>
		protected internal class Numerical:CompareCounterNodes, System.Collections.IComparer
		{
			public Numerical(CounterNode enclosingInstance):base(enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CounterNode enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				p = Pattern.compile(regex);
			}
			private CounterNode enclosingInstance;
			public new CounterNode Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< CounterNode >
			//UPGRADE_NOTE: Final was removed from the declaration of 'regex '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			protected internal System.String regex = "\\d+"; //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'p' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			protected internal Pattern p;
			
			/// <summary> Get first integer in key, if any. Otherwise return lowest possible
			/// integer.
			/// 
			/// </summary>
			/// <param name="key">is a string that may or may not contain an integer value
			/// </param>
			/// <returns> the value of the integer found, min(Integer) otherwise
			/// 
			/// </returns>
			protected internal virtual int getInt(System.String key)
			{
				int found = System.Int32.MinValue;
				Matcher match = p.matcher(key);
				
				if (!match.find())
				{
					// return minimum value
					return found;
				}
				int start = match.start();
				found = Integer.parseInt(key.substring(start, match.end()));
				
				// Check for sign
				if ((start > 0) && (key[start - 1] == '-'))
				{
					// negative integer found
					// FIXME: Is this a safe operation? What happens when
					// MAX_VALUE * -1 < MIN_VALUE?
					found *= (- 1);
				}
				return found;
			}
			
			/// <summary> Compare two CounterNodes based on the first integer found in their
			/// SortKeys.
			/// </summary>
			public virtual int compare(CounterNode left, CounterNode right)
			{
				if (!argsOK(left, right))
					return compareStrangeArgs(left, right);
				
				int l = getInt(left.toSortKey());
				int r = getInt(right.toSortKey());
				
				if (l < r)
					return - 1;
				if (l > r)
					return 1;
				
				return 0;
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public System.Int32 Compare(System.Object x, System.Object y)
			{
				return 0;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LengthAlpha' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Compare two CounterNodes based on the length of their SortKeys and
		/// alphanumerical sorting.
		/// 
		/// </summary>
		/// <author>  spindler
		/// </author>
		protected internal class LengthAlpha:CompareCounterNodes, System.Collections.IComparer
		{
			public LengthAlpha(CounterNode enclosingInstance):base(enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CounterNode enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CounterNode enclosingInstance;
			public new CounterNode Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< CounterNode >
			public virtual int compare(CounterNode left, CounterNode right)
			{
				if (!argsOK(left, right))
					return compareStrangeArgs(left, right);
				
				int leftLength = left.toSortKey().Length;
				int rightLength = right.toSortKey().Length;
				if (leftLength < rightLength)
					return - 1;
				if (leftLength > rightLength)
					return 1;
				// Native comparison
				return (left.compareTo(right));
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public System.Int32 Compare(System.Object x, System.Object y)
			{
				return 0;
			}
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		virtual public System.Int32 CompareTo(System.Object obj)
		{
			return 0;
		}
	}
	
	//UPGRADE_TODO: Interface 'javax.swing.tree.TreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	public class CounterInventory : System.Windows.Forms.TreeNode
	{
		public CounterInventory()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.root = new CounterNode(c.getName(), c);
			this.path = path;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this.inventory = new HashMap < String, CounterNode >();
			this.sort = sort;
			changed = true;
		}
		/// <summary> Deliver information of the tree as text.
		/// 
		/// </summary>
		/// <returns> String
		/// </returns>
		virtual public System.String ResultString
		{
			get
			{
				if (changed)
					updateTree();
				changed = false;
				return root.toResultString();
			}
			
		}
		/// <summary> Compatibility for DisplayResults class.
		/// 
		/// </summary>
		/// <returns> String[]
		/// </returns>
		virtual public System.String[] ResultStringArray
		{
			get
			{
				return new System.String[]{ResultString};
			}
			
		}
		// Needed for TreeModel
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < TreeModelListener > treeModelListeners = 
		new ArrayList < TreeModelListener >();
		// This contains shortcuts to the nodes of the tree
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Map < String, CounterNode > inventory;
		// The start of the tree
		protected internal CounterNode root;
		// Text view of the tree
		protected internal System.String resultString;
		// The path determines where a counter is found in the tree
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < String > path;
		// Small speed up, only update values in tree when something has changed
		protected internal bool changed;
		// Sort the tree
		protected internal bool sort;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public CounterInventory(Counter c, List < String > path, boolean sort)
		
		/// <summary> insert counter into the tree. It is not sorted in any way.
		/// 
		/// </summary>
		/// <param name="counter">
		/// </param>
		public virtual void  insert(Counter counter)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] path = counter.Path;
			//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder hash = new StringBuilder();
			
			CounterNode insertNode = root;
			CounterNode newNode = null;
			for (int j = 0; path != null && j < path.Length; j++)
			{
				hash.append(path[j]);
				if (inventory.get_Renamed(hash.toString()) == null)
				{
					newNode = new CounterNode(path[j], new Counter(path[j], counter.Piece), insertNode.Level + 1);
					inventory.put(hash.toString(), newNode);
					insertNode.addChild(newNode, sort);
				}
				insertNode = inventory.get_Renamed(hash.toString());
			}
			newNode = new CounterNode(counter.ToString(), counter, insertNode.Level + 1);
			insertNode.addChild(newNode, sort);
			changed = true;
		}
		
		private void  updateEntries()
		{
			root.updateValues();
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.getRoot' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public virtual System.Object getRoot()
		{
			if (changed)
				updateTree();
			return root;
		}
		
		private void  updateTree()
		{
			updateEntries();
			if (cutBelowRoot > 0)
				root.cutLevel(cutBelowRoot);
			for (int i = cutAboveLeaves; i > 0; i--)
				root.cutLeaves();
			changed = false;
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.getChildCount' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public virtual int getChildCount(System.Object parent)
		{
			CounterNode counter = (CounterNode) parent;
			return counter.ChildCount;
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.isLeaf' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public virtual bool isLeaf(System.Object node)
		{
			CounterNode counter = (CounterNode) node;
			return counter.Leaf;
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.addTreeModelListener' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_TODO: Interface 'javax.swing.event.TreeModelListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addTreeModelListener(TreeModelListener l)
		{
			treeModelListeners.add(l);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.removeTreeModelListener' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_TODO: Interface 'javax.swing.event.TreeModelListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  removeTreeModelListener(TreeModelListener l)
		{
			treeModelListeners.remove(l);
		}
		
		public virtual void  fireNodesRemoved(System.Object[] path, int[] childIndices, System.Object[] children)
		{
			//UPGRADE_ISSUE: Class 'javax.swing.event.TreeModelEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeModelEvent'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.event.TreeModelEvent.TreeModelEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeModelEvent'"
			TreeModelEvent e = new TreeModelEvent(this, path, childIndices, children);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(TreeModelListener l: treeModelListeners)
			{
				l.treeNodesRemoved(e);
			}
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.getChild' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public virtual System.Object getChild(System.Object parent, int index)
		{
			CounterNode counter = (CounterNode) parent;
			return counter.getChild(index);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.getIndexOfChild' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public virtual int getIndexOfChild(System.Object parent, System.Object child)
		{
			CounterNode counter = (CounterNode) parent;
			return counter.getIndexOfChild(child);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.tree.TreeModel.valueForPathChanged' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
		public virtual void  valueForPathChanged(TreePath path, System.Object newValue)
		{
			throw new System.NotSupportedException();
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}