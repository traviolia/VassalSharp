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
// Things that are not in the core packages
namespace bsh.util
{
	
	/// <summary>A JFC/Swing based console for the BeanShell desktop.
	/// This is a descendant of the old AWTConsole.
	/// Improvements by: Mark Donszelmann <Mark.Donszelmann@cern.ch>
	/// including Cut & Paste
	/// Improvements by: Daniel Leuck
	/// including Color and Image support, key press bug workaround
	/// </summary>
	//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	[Serializable]
	public class JConsole:System.Windows.Forms.ScrollableControl, GUIConsoleInterface, IThreadRunnable
	{
		static private System.Int32 state21;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJTextPane' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JEditorPane' and 'System.Windows.Forms.RichTextBox' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		[Serializable]
		private class AnonymousClassJTextPane:System.Windows.Forms.RichTextBox
		{
			private void  InitBlock(JConsole enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JConsole enclosingInstance;
			public JConsole Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JEditorPane' and 'System.Windows.Forms.RichTextBox' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_ISSUE: Interface 'javax.swing.text.StyledDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyledDocument'"
			internal AnonymousClassJTextPane(JConsole enclosingInstance, javax.swing.text.StyledDocument Param1):base()
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public void  Cut()
			{
				if (Enclosing_Instance.text.SelectionStart < Enclosing_Instance.cmdStart)
				{
					base.Copy();
				}
				else
				{
					base.Cut();
				}
			}
			
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public void  Paste()
			{
				Enclosing_Instance.forceCaretMoveToEnd();
				base.Paste();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(System.Object o, JConsole enclosingInstance)
			{
				InitBlock(o, enclosingInstance);
			}
			private void  InitBlock(System.Object o, JConsole enclosingInstance)
			{
				this.o = o;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable o was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Object o;
			private JConsole enclosingInstance;
			public JConsole Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Enclosing_Instance.append(System.Convert.ToString(o));
				Enclosing_Instance.resetCommandStart();
				Enclosing_Instance.text.SelectionStart = Enclosing_Instance.cmdStart;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(System.Drawing.Image icon, JConsole enclosingInstance)
			{
				InitBlock(icon, enclosingInstance);
			}
			private void  InitBlock(System.Drawing.Image icon, JConsole enclosingInstance)
			{
				this.icon = icon;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable icon was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Drawing.Image icon;
			private JConsole enclosingInstance;
			public JConsole Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				System.Windows.Forms.IDataObject temp_ClipBoard;
				//UPGRADE_TODO: Method 'javax.swing.JTextPane.insertIcon' was converted to 'System.Windows.Forms.ClipBoard.SetDataObject' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextPaneinsertIcon_javaxswingIcon'"
				temp_ClipBoard = System.Windows.Forms.Clipboard.GetDataObject();
				System.Windows.Forms.Clipboard.SetDataObject(icon);
				Enclosing_Instance.text.Paste();
				System.Windows.Forms.Clipboard.SetDataObject(temp_ClipBoard);
				Enclosing_Instance.resetCommandStart();
				Enclosing_Instance.text.SelectionStart = Enclosing_Instance.cmdStart;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable2 : IThreadRunnable
		{
			public AnonymousClassRunnable2(System.Drawing.Font font, System.Drawing.Color color, System.Object o, JConsole enclosingInstance)
			{
				InitBlock(font, color, o, enclosingInstance);
			}
			private void  InitBlock(System.Drawing.Font font, System.Drawing.Color color, System.Object o, JConsole enclosingInstance)
			{
				this.font = font;
				this.color = color;
				this.o = o;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable font was copied into class AnonymousClassRunnable2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Drawing.Font font;
			//UPGRADE_NOTE: Final variable color was copied into class AnonymousClassRunnable2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Drawing.Color color;
			//UPGRADE_NOTE: Final variable o was copied into class AnonymousClassRunnable2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Object o;
			private JConsole enclosingInstance;
			public JConsole Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				System.Collections.IDictionary old = Enclosing_Instance.getStyle();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Enclosing_Instance.setStyle(font, ref color);
				Enclosing_Instance.append(System.Convert.ToString(o));
				Enclosing_Instance.resetCommandStart();
				Enclosing_Instance.text.SelectionStart = Enclosing_Instance.cmdStart;
				Enclosing_Instance.setStyle(old, true);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable3 : IThreadRunnable
		{
			public AnonymousClassRunnable3(System.String fontFamilyName, int size, System.Drawing.Color color, bool bold, bool italic, bool underline, System.Object o, JConsole enclosingInstance)
			{
				InitBlock(fontFamilyName, size, color, bold, italic, underline, o, enclosingInstance);
			}
			private void  InitBlock(System.String fontFamilyName, int size, System.Drawing.Color color, bool bold, bool italic, bool underline, System.Object o, JConsole enclosingInstance)
			{
				this.fontFamilyName = fontFamilyName;
				this.size = size;
				this.color = color;
				this.bold = bold;
				this.italic = italic;
				this.underline = underline;
				this.o = o;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable fontFamilyName was copied into class AnonymousClassRunnable3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String fontFamilyName;
			//UPGRADE_NOTE: Final variable size was copied into class AnonymousClassRunnable3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private int size;
			//UPGRADE_NOTE: Final variable color was copied into class AnonymousClassRunnable3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Drawing.Color color;
			//UPGRADE_NOTE: Final variable bold was copied into class AnonymousClassRunnable3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private bool bold;
			//UPGRADE_NOTE: Final variable italic was copied into class AnonymousClassRunnable3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private bool italic;
			//UPGRADE_NOTE: Final variable underline was copied into class AnonymousClassRunnable3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private bool underline;
			//UPGRADE_NOTE: Final variable o was copied into class AnonymousClassRunnable3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Object o;
			private JConsole enclosingInstance;
			public JConsole Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				System.Collections.IDictionary old = Enclosing_Instance.getStyle();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Enclosing_Instance.setStyle(fontFamilyName, size, ref color, bold, italic, underline);
				Enclosing_Instance.append(System.Convert.ToString(o));
				Enclosing_Instance.resetCommandStart();
				Enclosing_Instance.text.SelectionStart = Enclosing_Instance.cmdStart;
				Enclosing_Instance.setStyle(old, true);
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state21 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state21 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual public System.IO.Stream InputStream
		{
			get
			{
				return in_Renamed;
			}
			
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.IO.StreamReader In
		{
			get
			{
				return new System.IO.StreamReader(in_Renamed, System.Text.Encoding.Default);
			}
			
		}
		private System.String Cmd
		{
			get
			{
				System.String s = "";
				try
				{
					s = text.Text.Substring(cmdStart, textLength() - cmdStart);
				}
				//UPGRADE_TODO: Class 'javax.swing.text.BadLocationException' was converted to 'System.Exception' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				catch (System.Exception e)
				{
					// should not happen
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Out.WriteLine("Internal JConsole Error: " + e);
				}
				return s;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Drawing.Font Font
		{
			get
			{
				return base.Font;
			}
			
			set
			{
				base.Font = value;
				
				if (text != null)
					text.Font = value;
			}
			
		}
		virtual public NameCompletion NameCompletion
		{
			set
			{
				this.nameCompletion = value;
			}
			
		}
		virtual public bool WaitFeedback
		{
			set
			{
				if (value)
				{
					//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
					//UPGRADE_ISSUE: Member 'java.awt.Cursor.WAIT_CURSOR' was converted to 'System.Windows.Forms.Cursors.WaitCursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
					Cursor = System.Windows.Forms.Cursors.WaitCursor;
				}
				else
				{
					//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
					//UPGRADE_ISSUE: Member 'java.awt.Cursor.DEFAULT_CURSOR' was converted to 'System.Windows.Forms.Cursors.Default' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
					Cursor = System.Windows.Forms.Cursors.Default;
				}
			}
			
		}
		private const System.String CUT = "Cut";
		private const System.String COPY = "Copy";
		private const System.String PASTE = "Paste";
		
		private System.IO.Stream outPipe;
		private System.IO.Stream inPipe;
		private System.IO.Stream in_Renamed;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.IO.StreamWriter out_Renamed;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.IO.StreamWriter getOut()
		{
			return out_Renamed;
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.IO.StreamWriter getErr()
		{
			return out_Renamed;
		}
		
		private int cmdStart = 0;
		private System.Collections.ArrayList history = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
		private System.String startedLine;
		private int histLine = 0;
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Windows.Forms.ContextMenu menu;
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JEditorPane' and 'System.Windows.Forms.RichTextBox' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Windows.Forms.RichTextBox text;
		//UPGRADE_ISSUE: Class 'javax.swing.text.DefaultStyledDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDefaultStyledDocument'"
		private DefaultStyledDocument doc;
		
		internal NameCompletion nameCompletion;
		//UPGRADE_NOTE: Final was removed from the declaration of 'SHOW_AMBIG_MAX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal int SHOW_AMBIG_MAX = 10;
		
		// hack to prevent key repeat for some reason?
		private bool gotUp = true;
		
		public JConsole():this(null, null)
		{
		}
		
		//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane'"
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public JConsole(System.IO.Stream cin, System.IO.Stream cout):base()
		{
			this.AutoScroll = true;
			
			// Special TextPane which catches for cut and paste, both L&F keys and
			// programmatic	behaviour
			//UPGRADE_ISSUE: Constructor 'javax.swing.text.DefaultStyledDocument.DefaultStyledDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDefaultStyledDocument'"
			text = new AnonymousClassJTextPane(this, doc = new DefaultStyledDocument());
			
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			System.Drawing.Font font = new System.Drawing.Font("Monospaced", 14, System.Drawing.FontStyle.Regular);
			text.Text = "";
			text.Font = font;
			//UPGRADE_ISSUE: Method 'javax.swing.text.JTextComponent.setMargin' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextJTextComponentsetMargin_javaawtInsets'"
			text.setMargin(new System.Int32[]{7, 5, 7, 5});
			text.KeyDown += new System.Windows.Forms.KeyEventHandler(bsh.util.JConsole.keyDown);
			text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			text.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
			Controls.Add(text);
			
			// create popup	menu
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			menu = new System.Windows.Forms.ContextMenu();
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			menu.MenuItems.Add(new System.Windows.Forms.MenuItem(CUT)).Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(menu.MenuItems.Add(new System.Windows.Forms.MenuItem(CUT)));
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			menu.MenuItems.Add(new System.Windows.Forms.MenuItem(COPY)).Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(menu.MenuItems.Add(new System.Windows.Forms.MenuItem(COPY)));
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			menu.MenuItems.Add(new System.Windows.Forms.MenuItem(PASTE)).Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(menu.MenuItems.Add(new System.Windows.Forms.MenuItem(PASTE)));
			
			text.MouseDown += new System.Windows.Forms.MouseEventHandler(bsh.util.JConsole.mouseDown);
			text.Click += new System.EventHandler(this.mouseClicked);
			text.MouseEnter += new System.EventHandler(this.mouseEntered);
			text.MouseLeave += new System.EventHandler(this.mouseExited);
			text.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			text.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			
			// make	sure popup menu	follows	Look & Feel
			//UPGRADE_TODO: Method 'javax.swing.UIManager.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			UIManager.addPropertyChangeListener(this);
			
			outPipe = cout;
			if (outPipe == null)
			{
				//UPGRADE_ISSUE: Constructor 'java.io.PipedOutputStream.PipedOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedOutputStreamPipedOutputStream'"
				outPipe = new PipedOutputStream();
				try
				{
					in_Renamed = new System.IO.StreamReader(((System.IO.StreamWriter) outPipe).BaseStream);
				}
				catch (System.IO.IOException e)
				{
					System.Drawing.Color tempAux = System.Drawing.Color.Red;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					print("Console internal	error (1)...", ref tempAux);
				}
			}
			
			inPipe = cin;
			if (inPipe == null)
			{
				//UPGRADE_ISSUE: Constructor 'java.io.PipedOutputStream.PipedOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedOutputStreamPipedOutputStream'"
				System.IO.StreamWriter pout = new PipedOutputStream();
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				out_Renamed = new System.IO.StreamWriter(pout);
				try
				{
					inPipe = new BlockingPipedInputStream(pout);
				}
				catch (System.IO.IOException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					print("Console internal error: " + e);
				}
			}
			// Start the inpipe watcher
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(this.Run)).Start();
			
			requestFocus();
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.requestFocus' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  requestFocus()
		{
			//UPGRADE_TODO: Method 'javax.swing.JComponent.requestFocus' was converted to 'System.Windows.Forms.Control.Focus' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentrequestFocus'"
			base.Focus();
			//UPGRADE_TODO: Method 'javax.swing.JComponent.requestFocus' was converted to 'System.Windows.Forms.Control.Focus' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentrequestFocus'"
			text.Focus();
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			type(event_sender, e);
			gotUp = false;
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			type(event_sender, e);
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			gotUp = true;
			type(event_sender, e);
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'type'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		//UPGRADE_TODO: The equivalent of method type needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		private void  type(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			lock (this)
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
				switch (e.KeyValue)
				{
					
					case ((int) System.Windows.Forms.Keys.Enter): 
						//UPGRADE_ISSUE: Method 'java.awt.AWTEvent.getID' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAWTEventgetID'"
						if (e.getID() == System.Windows.Forms.KeyEventArgs.KeyCode)
						{
							if (gotUp)
							{
								enter();
								resetCommandStart();
								text.SelectionStart = cmdStart;
							}
						}
						e.Handled = true;
						//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
						text.Refresh();
						break;
					
					
					case ((int) System.Windows.Forms.Keys.Up): 
						//UPGRADE_ISSUE: Method 'java.awt.AWTEvent.getID' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAWTEventgetID'"
						if (e.getID() == System.Windows.Forms.KeyEventArgs.KeyCode)
						{
							historyUp();
						}
						e.Handled = true;
						break;
					
					
					case ((int) System.Windows.Forms.Keys.Down): 
						//UPGRADE_ISSUE: Method 'java.awt.AWTEvent.getID' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAWTEventgetID'"
						if (e.getID() == System.Windows.Forms.KeyEventArgs.KeyCode)
						{
							historyDown();
						}
						e.Handled = true;
						break;
					
					
					case ((int) System.Windows.Forms.Keys.Left): 
					case ((int) System.Windows.Forms.Keys.Back): 
					case ((int) System.Windows.Forms.Keys.Delete): 
						if (text.SelectionStart <= cmdStart)
						{
							// This doesn't work for backspace.
							// See default case for workaround
							e.Handled = true;
						}
						break;
					
					
					case ((int) System.Windows.Forms.Keys.Right): 
						forceCaretMoveToStart();
						break;
					
					
					case ((int) System.Windows.Forms.Keys.Home): 
						text.SelectionStart = cmdStart;
						e.Handled = true;
						break;
					
					
					case ((int) System.Windows.Forms.Keys.U):  // clear line
						//UPGRADE_NOTE: The 'java.awt.event.InputEvent.getModifiers' method simulation might not work for some controls. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1284'"
						if ((state19 & (int) System.Windows.Forms.Keys.Control) > 0)
						{
							replaceRange("", cmdStart, textLength());
							histLine = 0;
							e.Handled = true;
						}
						break;
					
					
					case ((int) System.Windows.Forms.Keys.Menu): 
					case ((int) System.Windows.Forms.Keys.CapsLock): 
					case ((int) System.Windows.Forms.Keys.ControlKey): 
					//UPGRADE_ISSUE: Field 'java.awt.event.KeyEvent.VK_META' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventKeyEventVK_META_f'"
					case (KeyEvent.VK_META): 
					case ((int) System.Windows.Forms.Keys.ShiftKey): 
					case ((int) System.Windows.Forms.Keys.PrintScreen): 
					case ((int) System.Windows.Forms.Keys.Scroll): 
					case ((int) System.Windows.Forms.Keys.Pause): 
					case ((int) System.Windows.Forms.Keys.Insert): 
					case ((int) System.Windows.Forms.Keys.F1): 
					case ((int) System.Windows.Forms.Keys.F2): 
					case ((int) System.Windows.Forms.Keys.F3): 
					case ((int) System.Windows.Forms.Keys.F4): 
					case ((int) System.Windows.Forms.Keys.F5): 
					case ((int) System.Windows.Forms.Keys.F6): 
					case ((int) System.Windows.Forms.Keys.F7): 
					case ((int) System.Windows.Forms.Keys.F8): 
					case ((int) System.Windows.Forms.Keys.F9): 
					case ((int) System.Windows.Forms.Keys.F10): 
					case ((int) System.Windows.Forms.Keys.F11): 
					case ((int) System.Windows.Forms.Keys.F12): 
					case ((int) System.Windows.Forms.Keys.Escape): 
						
						// only	modifier pressed
						break;
						
						// Control-C
					
					case ((int) System.Windows.Forms.Keys.C): 
						//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.text.JTextComponent.getSelectedText' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						if (text.SelectedText == null)
						{
							//UPGRADE_NOTE: The 'java.awt.event.InputEvent.getModifiers' method simulation might not work for some controls. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1284'"
							//UPGRADE_ISSUE: Method 'java.awt.AWTEvent.getID' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAWTEventgetID'"
							if (((state19 & (int) System.Windows.Forms.Keys.Control) > 0) && (e.getID() == System.Windows.Forms.KeyEventArgs.KeyCode))
							{
								append("^C");
							}
							e.Handled = true;
						}
						break;
					
					
					case ((int) System.Windows.Forms.Keys.Tab): 
						//UPGRADE_ISSUE: Method 'java.awt.AWTEvent.getID' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAWTEventgetID'"
						//UPGRADE_ISSUE: Field 'java.awt.event.KeyEvent.KEY_RELEASED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventKeyEventKEY_RELEASED_f'"
						if (e.getID() == KeyEvent.KEY_RELEASED)
						{
							System.String part = text.Text.Substring(cmdStart);
							doCommandCompletion(part);
						}
						e.Handled = true;
						break;
					
					
					default: 
						//UPGRADE_NOTE: The 'java.awt.event.InputEvent.getModifiers' method simulation might not work for some controls. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1284'"
						//UPGRADE_ISSUE: Field 'java.awt.event.InputEvent.META_MASK' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
						if ((state19 & ((int) System.Windows.Forms.Keys.Control | (int) System.Windows.Forms.Keys.Alt | InputEvent.META_MASK)) == 0)
						{
							// plain character
							forceCaretMoveToEnd();
						}
						
						/*
						The getKeyCode function always returns VK_UNDEFINED for
						keyTyped events, so backspace is not fully consumed.
						*/
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.event.KeyEvent.paramString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						if (e.ToString().IndexOf("Backspace") != - 1)
						{
							if (text.SelectionStart <= cmdStart)
							{
								e.Handled = true;
								break;
							}
						}
						
						break;
					
				}
			}
		}
		
		private void  doCommandCompletion(System.String part)
		{
			if (nameCompletion == null)
				return ;
			
			int i = part.Length - 1;
			
			// Character.isJavaIdentifierPart()  How convenient for us!! 
			//UPGRADE_ISSUE: Method 'java.lang.Character.isJavaIdentifierPart' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangCharacterisJavaIdentifierPart_char'"
			while (i >= 0 && (Character.isJavaIdentifierPart(part[i]) || part[i] == '.'))
				i--;
			
			part = part.Substring(i + 1);
			
			if (part.Length < 2)
			// reasonable completion length
				return ;
			
			//System.out.println("completing part: "+part);
			
			// no completion
			System.String[] complete = nameCompletion.completeName(part);
			if (complete.Length == 0)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				java.awt.Toolkit.getDefaultToolkit();
				//UPGRADE_TODO: Method 'java.awt.Toolkit.beep' was converted to 'System.Console.Write' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Console.Write("\a");
				return ;
			}
			
			// Found one completion (possibly what we already have)
			if (complete.Length == 1 && !complete.Equals(part))
			{
				System.String append = complete[0].Substring(part.Length);
				this.append(append);
				return ;
			}
			
			// Found ambiguous, show (some of) them
			
			System.String line = text.Text;
			System.String command = line.Substring(cmdStart);
			// Find prompt
			for (i = cmdStart; line[i] != '\n' && i > 0; i--)
				;
			System.String prompt = line.Substring(i + 1, (cmdStart) - (i + 1));
			
			// Show ambiguous
			System.Text.StringBuilder sb = new System.Text.StringBuilder("\n");
			for (i = 0; i < complete.Length && i < SHOW_AMBIG_MAX; i++)
				sb.Append(complete[i] + "\n");
			if (i == SHOW_AMBIG_MAX)
				sb.Append("...\n");
			
			System.Drawing.Color tempAux = System.Drawing.Color.Gray;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			print(sb, ref tempAux);
			print(prompt); // print resets command start
			append(command); // append does not reset command start
		}
		
		private void  resetCommandStart()
		{
			cmdStart = textLength();
		}
		
		private void  append(System.String string_Renamed)
		{
			int slen = textLength();
			SupportClass.SelectText(text, slen, slen);
			SupportClass.ReplaceSelection(text, string_Renamed);
		}
		
		private System.String replaceRange(System.Object s, int start, int end)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String st = s.ToString();
			SupportClass.SelectText(text, start, end);
			SupportClass.ReplaceSelection(text, st);
			//text.repaint();
			return st;
		}
		
		private void  forceCaretMoveToEnd()
		{
			if (text.SelectionStart < cmdStart)
			{
				// move caret first!
				text.SelectionStart = textLength();
			}
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		private void  forceCaretMoveToStart()
		{
			if (text.SelectionStart < cmdStart)
			{
				// move caret first!
			}
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		
		private void  enter()
		{
			System.String s = Cmd;
			
			if (s.Length == 0)
			// special hack	for empty return!
				s = ";\n";
			else
			{
				history.Add(s);
				s = s + "\n";
			}
			
			append("\n");
			histLine = 0;
			acceptLine(s);
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		private void  historyUp()
		{
			if (history.Count == 0)
				return ;
			if (histLine == 0)
			// save current line
				startedLine = Cmd;
			if (histLine < history.Count)
			{
				histLine++;
				showHistoryLine();
			}
		}
		
		private void  historyDown()
		{
			if (histLine == 0)
				return ;
			
			histLine--;
			showHistoryLine();
		}
		
		private void  showHistoryLine()
		{
			System.String showline;
			if (histLine == 0)
				showline = startedLine;
			else
				showline = ((System.String) history[history.Count - histLine]);
			
			replaceRange(showline, cmdStart, textLength());
			text.SelectionStart = textLength();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		internal System.String ZEROS = "000";
		
		private void  acceptLine(System.String line)
		{
			// Patch to handle Unicode characters
			// Submitted by Daniel Leuck
			System.Text.StringBuilder buf = new System.Text.StringBuilder();
			int lineLength = line.Length;
			for (int i = 0; i < lineLength; i++)
			{
				System.String val = System.Convert.ToString(line[i], 16);
				val = ZEROS.Substring(0, (4 - val.Length) - (0)) + val;
				buf.Append("\\u" + val);
			}
			line = buf.ToString();
			// End unicode patch
			
			if (outPipe == null)
			{
				System.Drawing.Color tempAux = System.Drawing.Color.Red;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				print("Console internal	error: cannot output ...", ref tempAux);
			}
			else
				try
				{
					sbyte[] temp_sbyteArray;
					temp_sbyteArray = SupportClass.ToSByteArray(SupportClass.ToByteArray(line));
					outPipe.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
					outPipe.Flush();
				}
				catch (System.IO.IOException e)
				{
					outPipe = null;
					throw new System.SystemException("Console pipe broken...");
				}
			//text.repaint();
		}
		
		public virtual void  println(System.Object o)
		{
			print(System.Convert.ToString(o) + "\n");
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		public virtual void  print(System.Object o)
		{
			invokeAndWait(new AnonymousClassRunnable(o, this));
		}
		
		/// <summary> Prints "\\n" (i.e. newline)</summary>
		public virtual void  println()
		{
			print("\n");
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		public virtual void  error(System.Object o)
		{
			System.Drawing.Color tempAux = System.Drawing.Color.Red;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			print(o, ref tempAux);
		}
		
		public virtual void  println(System.Drawing.Image icon)
		{
			print(icon);
			println();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		public virtual void  print(System.Drawing.Image icon)
		{
			if (icon == null)
				return ;
			
			invokeAndWait(new AnonymousClassRunnable1(icon, this));
		}
		
		public virtual void  print(System.Object s, System.Drawing.Font font)
		{
			System.Drawing.Color tempAux = System.Drawing.Color.Empty;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			print(s, font, ref tempAux);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  print(System.Object s, ref System.Drawing.Color color)
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			print(s, null, ref color);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  print(System.Object o, System.Drawing.Font font, ref System.Drawing.Color color)
		{
			invokeAndWait(new AnonymousClassRunnable2(font, color, o, this));
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  print(System.Object s, System.String fontFamilyName, int size, ref System.Drawing.Color color)
		{
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			print(s, fontFamilyName, size, ref color, false, false, false);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  print(System.Object o, System.String fontFamilyName, int size, ref System.Drawing.Color color, bool bold, bool italic, bool underline)
		{
			invokeAndWait(new AnonymousClassRunnable3(fontFamilyName, size, color, bold, italic, underline, o, this));
		}
		
		private System.Collections.IDictionary setStyle(System.Drawing.Font font)
		{
			System.Drawing.Color tempAux = System.Drawing.Color.Empty;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			return setStyle(font, ref tempAux);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private System.Collections.IDictionary setStyle(ref System.Drawing.Color color)
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			return setStyle(null, ref color);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private System.Collections.IDictionary setStyle(System.Drawing.Font font, ref System.Drawing.Color color)
		{
			if (font != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.isUnderline' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return setStyle(font.FontFamily.Name, (int) font.Size, ref color, font.Style == System.Drawing.FontStyle.Bold, font.Style == System.Drawing.FontStyle.Italic, StyleConstants.isUnderline(getStyle()));
			}
			else
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return setStyle(null, - 1, ref color);
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private System.Collections.IDictionary setStyle(System.String fontFamilyName, int size, ref System.Drawing.Color color)
		{
			System.Collections.IDictionary attr = new System.Collections.Hashtable();
			if (!color.IsEmpty)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setForeground' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				StyleConstants.setForeground(attr, color);
			}
			if (fontFamilyName != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setFontFamily' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				StyleConstants.setFontFamily(attr, fontFamilyName);
			}
			if (size != - 1)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setFontSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				StyleConstants.setFontSize(attr, size);
			}
			
			setStyle(attr);
			
			return getStyle();
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private System.Collections.IDictionary setStyle(System.String fontFamilyName, int size, ref System.Drawing.Color color, bool bold, bool italic, bool underline)
		{
			System.Collections.IDictionary attr = new System.Collections.Hashtable();
			if (!color.IsEmpty)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setForeground' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				StyleConstants.setForeground(attr, color);
			}
			if (fontFamilyName != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setFontFamily' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				StyleConstants.setFontFamily(attr, fontFamilyName);
			}
			if (size != - 1)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setFontSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				StyleConstants.setFontSize(attr, size);
			}
			//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setBold' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
			StyleConstants.setBold(attr, bold);
			//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setItalic' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
			StyleConstants.setItalic(attr, italic);
			//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setUnderline' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
			StyleConstants.setUnderline(attr, underline);
			
			setStyle(attr);
			
			return getStyle();
		}
		
		private void  setStyle(System.Collections.IDictionary attributes)
		{
			setStyle(attributes, false);
		}
		
		private void  setStyle(System.Collections.IDictionary attributes, bool overWrite)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.JTextPane.setCharacterAttributes' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTextPanesetCharacterAttributes_javaxswingtextAttributeSet_boolean'"
			text.setCharacterAttributes(attributes, overWrite);
		}
		
		private System.Collections.IDictionary getStyle()
		{
			//UPGRADE_ISSUE: Method 'javax.swing.JTextPane.getCharacterAttributes' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTextPanegetCharacterAttributes'"
			return text.getCharacterAttributes();
		}
		
		private void  inPipeWatcher()
		{
			sbyte[] ba = new sbyte[256]; //	arbitrary blocking factor
			int read;
			while ((read = inPipe is VassalSharp.tools.image.PNGChunkSkipInputStream?((VassalSharp.tools.image.PNGChunkSkipInputStream) inPipe).read(ba):SupportClass.ReadInput(inPipe, ba, 0, ba.Length)) != - 1)
			{
				print(new System.String(SupportClass.ToCharArray(ba), 0, read));
				//text.repaint();
			}
			
			println("Console: Input	closed...");
		}
		
		public virtual void  Run()
		{
			try
			{
				inPipeWatcher();
			}
			catch (System.IO.IOException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Drawing.Color tempAux = System.Drawing.Color.Red;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				print("Console: I/O Error: " + e + "\n", ref tempAux);
			}
		}
		
		public override System.String ToString()
		{
			return "BeanShell console";
		}
		
		// MouseListener Interface
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs event_Renamed)
		{
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs event_Renamed)
		{
			//UPGRADE_TODO: Method 'java.awt.event.MouseEvent.isPopupTrigger' was converted to 'System.Windows.Forms.MouseButtons.Right' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventMouseEventisPopupTrigger'"
			if (event_Renamed.Button == System.Windows.Forms.MouseButtons.Right)
			{
				//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
				menu.Show((System.Windows.Forms.Control) event_sender, new System.Drawing.Point(event_Renamed.X, event_Renamed.Y));
			}
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs event_Renamed)
		{
			//UPGRADE_TODO: Method 'java.awt.event.MouseEvent.isPopupTrigger' was converted to 'System.Windows.Forms.MouseButtons.Right' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventMouseEventisPopupTrigger'"
			if (event_Renamed.Button == System.Windows.Forms.MouseButtons.Right)
			{
				//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
				menu.Show((System.Windows.Forms.Control) event_sender, new System.Drawing.Point(event_Renamed.X, event_Renamed.Y));
			}
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			text.Refresh();
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs event_Renamed)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs event_Renamed)
		{
		}
		
		// property	change
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs event_Renamed)
		{
			if (event_Renamed.PropertyName.Equals("lookAndFeel"))
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.updateComponentTreeUI' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.updateComponentTreeUI(menu);
			}
		}
		
		// handle cut, copy	and paste
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs event_Renamed)
		{
			System.String cmd = SupportClass.CommandManager.GetCommand(event_sender);
			if (cmd.Equals(CUT))
			{
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.InvokeMethodAsVirtual(text, "Cut", new System.Object[]{});
			}
			else if (cmd.Equals(COPY))
			{
				text.Copy();
			}
			else if (cmd.Equals(PASTE))
			{
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.InvokeMethodAsVirtual(text, "Paste", new System.Object[]{});
			}
		}
		
		/// <summary> If not in the event thread run via SwingUtilities.invokeAndWait()</summary>
		private void  invokeAndWait(IThreadRunnable run)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.isEventDispatchThread' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			if (!SwingUtilities.isEventDispatchThread())
			{
				try
				{
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeAndWait' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeAndWait(run);
				}
				catch (System.Exception e)
				{
					// shouldn't happen
					if (e is bsh.TargetError)
						((bsh.TargetError) e).printStackTrace();
					else
						SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
			else
			{
				run.Run();
			}
		}
		
		/// <summary>The overridden read method in this class will not throw "Broken pipe"
		/// IOExceptions;  It will simply wait for new writers and data.
		/// This is used by the JConsole internal read thread to allow writers
		/// in different (and in particular ephemeral) threads to write to the pipe.
		/// It also checks a little more frequently than the original read().
		/// Warning: read() will not even error on a read to an explicitly closed 
		/// pipe (override closed to for that).
		/// </summary>
		public class BlockingPipedInputStream:System.IO.StreamReader
		{
			internal bool closed;
			public BlockingPipedInputStream(System.IO.StreamWriter pout):base(pout.BaseStream)
			{
			}
			public  override int Read()
			{
				if (closed)
					throw new System.IO.IOException("stream closed");
				
				//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.in' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreamin_f'"
				while (base.in_Renamed < 0)
				{
					// While no data */
					System.Threading.Monitor.PulseAll(this); // Notify any writers to wake up
					try
					{
						System.Threading.Monitor.Wait(this, TimeSpan.FromMilliseconds(750));
					}
					catch (System.Threading.ThreadInterruptedException e)
					{
						throw new System.IO.IOException();
					}
				}
				// This is what the superclass does.
				//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.buffer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreambuffer_f'"
				//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.out' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreamout_f'"
				int ret = buffer[base.out_Renamed++] & 0xFF;
				//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.out' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreamout_f'"
				//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.buffer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreambuffer_f'"
				if (base.out_Renamed >= buffer.Length)
				{
					//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.out' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreamout_f'"
					base.out_Renamed = 0;
				}
				//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.in' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreamin_f'"
				//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.out' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreamout_f'"
				if (base.in_Renamed == base.out_Renamed)
				{
					//UPGRADE_ISSUE: Field 'java.io.PipedInputStream.in' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedInputStreamin_f'"
					base.in_Renamed = - 1;
				} /* now empty */
				return ret;
			}
			public override void  Close()
			{
				closed = true;
				base.Close();
			}
		}
		
		private int textLength()
		{
			//UPGRADE_ISSUE: Method 'javax.swing.text.Document.getLength' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
			return ((System.String) text.Text).getLength();
		}
	}
}