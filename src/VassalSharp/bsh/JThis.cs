/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
namespace bsh
{
	
	/// <summary>JThis is a dynamically loaded extension which extends This and adds 
	/// explicit support for AWT and JFC events, etc.  This is a backwards 
	/// compatability measure for JDK 1.2.  With 1.3+ there is a general 
	/// reflection proxy mechanism that allows the base This to implement 
	/// arbitrary interfaces.
	/// The NameSpace getThis() method will produce instances of JThis if 
	/// the java version is prior to 1.3 and swing is available...  (e.g. 1.2
	/// or 1.1 + swing installed)  
	/// Users of 1.1 without swing will have minimal interface support (just run()).
	/// </summary>
	/// <summary>Bsh doesn't run on 1.02 and below because there is no reflection! 
	/// Note: This module relies on features of Swing and will only compile
	/// with JDK1.2 or JDK1.1 + the swing package.  For other environments simply 
	/// do not compile this class.
	/// </summary>
	//UPGRADE_TODO: Interface 'javax.swing.event.AncestorListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.CaretListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.CellEditorListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.ListDataListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.MenuDragMouseListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.MenuKeyListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.TableColumnModelListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.TableModelListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.TreeModelListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	//UPGRADE_TODO: Interface 'javax.swing.event.UndoableEditListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
	[Serializable]
	class JThis:This, AncestorListener, CaretListener, CellEditorListener, DocumentListener, ListDataListener, MenuDragMouseListener, MenuKeyListener, TableColumnModelListener, TableModelListener, TreeModelListener, UndoableEditListener
	{
		
		internal JThis(NameSpace namespace_Renamed, Interpreter declaringInterp):base(namespace_Renamed, declaringInterp)
		{
		}
		
		public override System.String ToString()
		{
			return "'this' reference (JThis) to Bsh object: " + namespace_Renamed.Name;
		}
		
		internal virtual void  event_Renamed(System.String name, System.Object event_Renamed)
		{
			CallStack callstack = new CallStack(namespace_Renamed);
			BshMethod method = null;
			
			// handleEvent gets all events
			try
			{
				method = namespace_Renamed.getMethod("handleEvent", new System.Type[]{null});
			}
			catch (UtilEvalError e)
			{
				/*squeltch*/
			}
			
			if (method != null)
				try
				{
					method.invoke(new System.Object[]{event_Renamed}, declaringInterpreter, callstack, null);
				}
				catch (EvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					declaringInterpreter.error("local event hander method invocation error:" + e);
				}
			
			// send to specific event handler
			try
			{
				method = namespace_Renamed.getMethod(name, new System.Type[]{null});
			}
			catch (UtilEvalError e)
			{
				/*squeltch*/
			}
			if (method != null)
				try
				{
					method.invoke(new System.Object[]{event_Renamed}, declaringInterpreter, callstack, null);
				}
				catch (EvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					declaringInterpreter.error("local event hander method invocation error:" + e);
				}
		}
		
		// Listener interfaces
		
		//UPGRADE_ISSUE: Class 'javax.swing.event.AncestorEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventAncestorEvent'"
		public virtual void  ancestorAdded(System.Object event_sender, AncestorEvent e)
		{
			event_Renamed("ancestorAdded", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.AncestorEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventAncestorEvent'"
		public virtual void  ancestorRemoved(System.Object event_sender, AncestorEvent e)
		{
			event_Renamed("ancestorRemoved", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.AncestorEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventAncestorEvent'"
		public virtual void  ancestorMoved(System.Object event_sender, AncestorEvent e)
		{
			event_Renamed("ancestorMoved", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.CaretEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventCaretEvent'"
		public virtual void  caretUpdate(System.Object event_sender, CaretEvent e)
		{
			event_Renamed("caretUpdate", e);
		}
		public virtual void  editingStopped(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("editingStopped", e);
		}
		public virtual void  editingCanceled(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("editingCanceled", e);
		}
		public virtual void  stateChanged(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("stateChanged", e);
		}
		//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
		public virtual void  insertUpdate(DocumentEvent e)
		{
			event_Renamed("insertUpdate", e);
		}
		//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
		public virtual void  removeUpdate(DocumentEvent e)
		{
			event_Renamed("removeUpdate", e);
		}
		//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
		public virtual void  changedUpdate(DocumentEvent e)
		{
			event_Renamed("changedUpdate", e);
		}
		public virtual void  hyperlinkUpdate(System.Object event_sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			event_Renamed("internalFrameOpened", e);
		}
		public virtual void  internalFrameOpened(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("internalFrameOpened", e);
		}
		public virtual void  internalFrameClosing(System.Object event_sender,  System.ComponentModel.CancelEventArgs e)
		{
			event_Renamed("internalFrameClosing", e);
		}
		public virtual void  internalFrameClosed(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("internalFrameClosed", e);
		}
		public virtual void  internalFrameIconified(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("internalFrameIconified", e);
		}
		public virtual void  internalFrameDeiconified(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("internalFrameDeiconified", e);
		}
		public virtual void  internalFrameActivated(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("internalFrameActivated", e);
		}
		public virtual void  internalFrameDeactivated(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("internalFrameDeactivated", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.ListDataEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListDataEvent'"
		public virtual void  intervalAdded(System.Object event_sender, ListDataEvent e)
		{
			event_Renamed("intervalAdded", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.ListDataEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListDataEvent'"
		public virtual void  intervalRemoved(System.Object event_sender, ListDataEvent e)
		{
			event_Renamed("intervalRemoved", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.ListDataEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListDataEvent'"
		public virtual void  contentsChanged(System.Object event_sender, ListDataEvent e)
		{
			event_Renamed("contentsChanged", e);
		}
		public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("valueChanged", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.MenuDragMouseEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventMenuDragMouseEvent'"
		public virtual void  menuDragMouseEntered(System.Object event_sender, MenuDragMouseEvent e)
		{
			event_Renamed("menuDragMouseEntered", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.MenuDragMouseEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventMenuDragMouseEvent'"
		public virtual void  menuDragMouseExited(System.Object event_sender, MenuDragMouseEvent e)
		{
			event_Renamed("menuDragMouseExited", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.MenuDragMouseEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventMenuDragMouseEvent'"
		public virtual void  menuDragMouseDragged(System.Object event_sender, MenuDragMouseEvent e)
		{
			event_Renamed("menuDragMouseDragged", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.MenuDragMouseEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventMenuDragMouseEvent'"
		public virtual void  menuDragMouseReleased(System.Object event_sender, MenuDragMouseEvent e)
		{
			event_Renamed("menuDragMouseReleased", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.MenuKeyEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventMenuKeyEvent'"
		public virtual void  menuKeyTyped(System.Object event_sender, MenuKeyEvent e)
		{
			event_Renamed("menuKeyTyped", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.MenuKeyEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventMenuKeyEvent'"
		public virtual void  menuKeyPressed(System.Object event_sender, MenuKeyEvent e)
		{
			event_Renamed("menuKeyPressed", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.MenuKeyEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventMenuKeyEvent'"
		public virtual void  menuKeyReleased(System.Object event_sender, MenuKeyEvent e)
		{
			event_Renamed("menuKeyReleased", e);
		}
		public virtual void  menuSelected(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("menuSelected", e);
		}
		public virtual void  menuDeselected(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("menuDeselected", e);
		}
		public virtual void  menuCanceled(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("menuCanceled", e);
		}
		public virtual void  popupMenuWillBecomeVisible(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("popupMenuWillBecomeVisible", e);
		}
		public virtual void  popupMenuWillBecomeInvisible(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("popupMenuWillBecomeInvisible", e);
		}
		public virtual void  popupMenuCanceled(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("popupMenuCanceled", e);
		}
		public virtual void  columnAdded(System.Object event_sender, System.ComponentModel.CollectionChangeEventArgs e)
		{
			event_Renamed("columnAdded", e);
		}
		public virtual void  columnRemoved(System.Object event_sender, System.ComponentModel.CollectionChangeEventArgs e)
		{
			event_Renamed("columnRemoved", e);
		}
		public virtual void  columnMoved(System.Object event_sender, System.ComponentModel.CollectionChangeEventArgs e)
		{
			event_Renamed("columnMoved", e);
		}
		public virtual void  columnMarginChanged(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("columnMarginChanged", e);
		}
		public virtual void  columnSelectionChanged(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("columnSelectionChanged", e);
		}
		public virtual void  tableChanged(System.Object event_sender, System.Data.DataRowChangeEventArgs e)
		{
			event_Renamed("tableChanged", e);
		}
		public virtual void  treeExpanded(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			event_Renamed("treeExpanded", e);
		}
		public virtual void  treeCollapsed(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			event_Renamed("treeCollapsed", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.TreeModelEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeModelEvent'"
		public virtual void  treeNodesChanged(System.Object event_sender, TreeModelEvent e)
		{
			event_Renamed("treeNodesChanged", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.TreeModelEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeModelEvent'"
		public virtual void  treeNodesInserted(System.Object event_sender, TreeModelEvent e)
		{
			event_Renamed("treeNodesInserted", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.TreeModelEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeModelEvent'"
		public virtual void  treeNodesRemoved(System.Object event_sender, TreeModelEvent e)
		{
			event_Renamed("treeNodesRemoved", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.TreeModelEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventTreeModelEvent'"
		public virtual void  treeStructureChanged(System.Object event_sender, TreeModelEvent e)
		{
			event_Renamed("treeStructureChanged", e);
		}
		public virtual void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			event_Renamed("valueChanged", e);
		}
		public virtual void  treeWillExpand(System.Object event_sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			event_Renamed("treeWillExpand", e);
		}
		public virtual void  treeWillCollapse(System.Object event_sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			event_Renamed("treeWillCollapse", e);
		}
		//UPGRADE_ISSUE: Class 'javax.swing.event.UndoableEditEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventUndoableEditEvent'"
		public virtual void  undoableEditHappened(System.Object event_sender, UndoableEditEvent e)
		{
			event_Renamed("undoableEditHappened", e);
		}
		
		// Listener interfaces
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "actionPerformed", e);
		}
		public virtual void  adjustmentValueChanged(System.Object event_sender, System.Windows.Forms.ScrollEventArgs e)
		{
			event_Renamed(event_sender, "adjustmentValueChanged", e);
		}
		public virtual void  componentResized(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "componentResized", e);
		}
		public virtual void  componentMoved(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "componentMoved", e);
		}
		public virtual void  componentShown(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "componentShown", e);
		}
		public virtual void  componentHidden(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "componentHidden", e);
		}
		public virtual void  componentAdded(System.Object event_sender, System.Windows.Forms.ControlEventArgs e)
		{
			event_Renamed(event_sender, "componentAdded", e);
		}
		public virtual void  componentRemoved(System.Object event_sender, System.Windows.Forms.ControlEventArgs e)
		{
			event_Renamed(event_sender, "componentRemoved", e);
		}
		public virtual void  focusGained(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "focusGained", e);
		}
		public virtual void  focusLost(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "focusLost", e);
		}
		public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
		{
			if (event_sender is System.Windows.Forms.MenuItem)
				((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
			event_Renamed(event_sender, "itemStateChanged", e);
		}
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			event_Renamed(event_sender, "keyTyped", e);
		}
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			event_Renamed(event_sender, "keyPressed", e);
		}
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			event_Renamed(event_sender, "keyReleased", e);
		}
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "mouseClicked", e);
		}
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			event_Renamed(event_sender, "mousePressed", e);
		}
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			event_Renamed(event_sender, "mouseReleased", e);
		}
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "mouseEntered", e);
		}
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "mouseExited", e);
		}
		public virtual void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			event_Renamed(event_sender, "mouseDragged", e);
		}
		public virtual void  mouseMoved(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			event_Renamed(event_sender, "mouseMoved", e);
		}
		public virtual void  textValueChanged(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "textValueChanged", e);
		}
		public virtual void  windowOpened(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "windowOpened", e);
		}
		public virtual void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			event_Renamed(event_sender, "windowClosing", e);
		}
		public virtual void  windowClosed(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "windowClosed", e);
		}
		public virtual void  windowIconified(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("windowIconified", e);
		}
		public virtual void  windowDeiconified(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed("windowDeiconified", e);
		}
		public virtual void  windowActivated(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "windowActivated", e);
		}
		public virtual void  windowDeactivated(System.Object event_sender, System.EventArgs e)
		{
			event_Renamed(event_sender, "windowDeactivated", e);
		}
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
		{
			event_Renamed(event_sender, "propertyChange", e);
		}
		public virtual void  vetoableChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
		{
			event_Renamed("vetoableChange", e);
		}
		
		public virtual bool imageUpdate(System.Drawing.Image img, int infoflags, int x, int y, int width, int height)
		{
			
			BshMethod method = null;
			try
			{
				method = namespace_Renamed.getMethod("imageUpdate", new System.Type[]{null, null, null, null, null, null});
			}
			catch (UtilEvalError e)
			{
				/*squeltch*/
			}
			
			if (method != null)
				try
				{
					CallStack callstack = new CallStack(namespace_Renamed);
					method.invoke(new System.Object[]{img, new Primitive(infoflags), new Primitive(x), new Primitive(y), new Primitive(width), new Primitive(height)}, declaringInterpreter, callstack, null);
				}
				catch (EvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					declaringInterpreter.error("local event handler imageUpdate: method invocation error:" + e);
				}
			return true;
		}
	}
}