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
using bsh;
namespace bsh.util
{
	
	/*
	This should go away eventually...  Native AWT sucks.
	Use JConsole and the desktop() environment.
	
	Notes: todo -
	clean up the watcher thread, set daemon status*/
	
	/// <summary>An old AWT based console for BeanShell.
	/// I looked everwhere for one, and couldn't find anything that worked.
	/// I've tried to keep this as small as possible, no frills.
	/// (Well, one frill - a simple history with the up/down arrows)
	/// My hope is that this can be moved to a lightweight (portable) component
	/// with JFC soon... but Swing is still very slow and buggy.
	/// Done: see JConsole.java
	/// The big Hack:
	/// The heinous, disguisting hack in here is to keep the caret (cursor)
	/// at the bottom of the text (without the user having to constantly click
	/// at the bottom).  It wouldn't be so bad if the damned setCaretPostition()
	/// worked as expected.  But the AWT TextArea for some insane reason treats
	/// NLs as characters... oh, and it refuses to let you set a caret position
	/// greater than the text length - for which it counts NLs as *one* character.
	/// The glorious hack to fix this is to go the TextComponent peer.  I really
	/// hate this.
	/// Out of date:
	/// </summary>
	/// <summary>This class is out of date.  It does not use the special blocking piped
	/// input stream that the jconsole uses.
	/// Deprecation:
	/// This file uses two deprecate APIs.  We want to be a PrintStream so
	/// that we can redirect stdout to our console... I don't see a way around
	/// this.  Also we have to use getPeer() for the big hack above.
	/// </summary>
	[Serializable]
	public class AWTConsole:System.Windows.Forms.TextBox, ConsoleInterface, IThreadRunnable
	{
		static private System.Int32 state15;
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(System.Windows.Forms.Form f)
			{
				InitBlock(f);
			}
			private void  InitBlock(System.Windows.Forms.Form f)
			{
				this.f = f;
			}
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			//UPGRADE_NOTE: Final variable f was copied into class AnonymousClassWindowAdapter. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Form f;
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				f.Dispose();
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state15 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.IO.StreamReader In
		{
			get
			{
				return new System.IO.StreamReader(in_Renamed, System.Text.Encoding.Default);
			}
			
		}
		
		private System.IO.Stream outPipe;
		private System.IO.Stream inPipe;
		
		// formerly public
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
		
		private System.Text.StringBuilder line = new System.Text.StringBuilder();
		private System.String startedLine;
		private int textLength = 0;
		private System.Collections.ArrayList history = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
		private int histLine = 0;
		
		public AWTConsole(int rows, int cols, System.IO.Stream cin, System.IO.Stream cout):base()
		{;;
			this.Multiline = true;
			this.WordWrap = false;
			this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			Font = new System.Drawing.Font("Monospaced", 14, System.Drawing.FontStyle.Regular);
			ReadOnly = !false;
			KeyDown += new System.Windows.Forms.KeyEventHandler(bsh.util.AWTConsole.keyDown);
			KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
			
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
					print("Console internal error...");
				}
			}
			
			// start the inpipe watcher
			inPipe = cin;
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(this.Run)).Start();
			
			Focus();
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.event.KeyEvent.getKeyChar' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_NOTE: The 'java.awt.event.InputEvent.getModifiers' method simulation might not work for some controls. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1284'"
			type(e.KeyValue, (char) e.KeyValue, state14);
			e.Handled = true;
		}
		
		public AWTConsole():this(12, 80, null, null)
		{
		}
		public AWTConsole(System.IO.Stream in_Renamed, System.IO.Stream out_Renamed):this(12, 80, in_Renamed, out_Renamed)
		{
		}
		
		public virtual void  type(int code, char ch, int modifiers)
		{
			switch (code)
			{
				
				case ((int) System.Windows.Forms.Keys.Back): 
					if (line.Length > 0)
					{
						line.Length -= 1;
						System.Windows.Forms.TextBox temp_TextBox;
						temp_TextBox = this;
						temp_TextBox.Text = temp_TextBox.Text.Substring(0, (textLength - 1)) + "" + temp_TextBox.Text.Substring(textLength);
						textLength--;
					}
					break;
				
				case ((int) System.Windows.Forms.Keys.Enter): 
					enter();
					break;
				
				case ((int) System.Windows.Forms.Keys.U): 
					if ((modifiers & (int) System.Windows.Forms.Keys.Control) > 0)
					{
						int len = line.Length;
						System.Windows.Forms.TextBox temp_TextBox2;
						temp_TextBox2 = this;
						temp_TextBox2.Text = temp_TextBox2.Text.Substring(0, (textLength - len)) + "" + temp_TextBox2.Text.Substring(textLength);
						line.Length = 0;
						histLine = 0;
						textLength = Text.Length;
					}
					else
						doChar(ch);
					break;
				
				case ((int) System.Windows.Forms.Keys.Up): 
					historyUp();
					break;
				
				case ((int) System.Windows.Forms.Keys.Down): 
					historyDown();
					break;
				
				case ((int) System.Windows.Forms.Keys.Tab): 
					line.Append("    ");
					AppendText("    ");
					textLength += 4;
					break;
					/*
					case ( KeyEvent.VK_LEFT ):	
					if (line.length() > 0) {
					break;*/
					// Control-C
				
				case ((int) System.Windows.Forms.Keys.C): 
					if ((modifiers & (int) System.Windows.Forms.Keys.Control) > 0)
					{
						line.Append("^C");
						AppendText("^C");
						textLength += 2;
					}
					else
						doChar(ch);
					break;
				
				default: 
					doChar(ch);
					break;
				
			}
		}
		
		private void  doChar(char ch)
		{
			if ((ch >= ' ') && (ch <= '~'))
			{
				line.Append(ch);
				AppendText(System.Convert.ToString(ch));
				textLength++;
			}
		}
		
		private void  enter()
		{
			System.String s;
			if (line.Length == 0)
			// special hack for empty return!
				s = ";\n";
			else
			{
				s = line + "\n";
				history.Add(line.ToString());
			}
			line.Length = 0;
			histLine = 0;
			AppendText("\n");
			textLength = Text.Length; // sync for safety
			acceptLine(s);
			
			setCaretPosition(textLength);
		}
		
		/* 
		Here's the really disguisting hack.
		We have to get to the peer because TextComponent will refuse to
		let us set us set a caret position greater than the text length.
		Great.  What a piece of crap.
		*/
		//UPGRADE_NOTE: The equivalent of method 'java.awt.TextComponent.setCaretPosition' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  setCaretPosition(int pos)
		{
			//UPGRADE_ISSUE: Method 'java.awt.Component.getPeer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentgetPeer'"
			((System.Windows.Forms.TextBox) getPeer()).Select(pos + countNLs(), 0);
		}
		
		/*
		This is part of a hack to fix the setCaretPosition() bug
		Count the newlines in the text
		*/
		private int countNLs()
		{
			System.String s = Text;
			int c = 0;
			for (int i = 0; i < s.Length; i++)
				if (s[i] == '\n')
					c++;
			return c;
		}
		
		private void  historyUp()
		{
			if (history.Count == 0)
				return ;
			if (histLine == 0)
			// save current line
				startedLine = line.ToString();
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
			
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = this;
			temp_TextBox.Text = temp_TextBox.Text.Substring(0, (textLength - line.Length)) + showline + temp_TextBox.Text.Substring(textLength);
			line = new System.Text.StringBuilder(showline);
			textLength = Text.Length;
		}
		
		private void  acceptLine(System.String line)
		{
			if (outPipe == null)
				print("Console internal error...");
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
		}
		
		public virtual void  println(System.Object o)
		{
			print(System.Convert.ToString(o) + "\n");
		}
		
		public virtual void  error(System.Object o)
		{
			System.Drawing.Color tempAux = System.Drawing.Color.Red;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			print(o, ref tempAux);
		}
		
		// No color
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  print(System.Object o, ref System.Drawing.Color c)
		{
			print("*** " + System.Convert.ToString(o));
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'print'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  print(System.Object o)
		{
			lock (this)
			{
				AppendText(System.Convert.ToString(o));
				textLength = Text.Length; // sync for safety
			}
		}
		
		private void  inPipeWatcher()
		{
			if (inPipe == null)
			{
				//UPGRADE_ISSUE: Constructor 'java.io.PipedOutputStream.PipedOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioPipedOutputStreamPipedOutputStream'"
				System.IO.StreamWriter pout = new PipedOutputStream();
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				out_Renamed = new System.IO.StreamWriter(pout);
				inPipe = new System.IO.StreamReader(pout.BaseStream);
			}
			sbyte[] ba = new sbyte[256]; // arbitrary blocking factor
			int read;
			while ((read = inPipe is VassalSharp.tools.image.PNGChunkSkipInputStream?((VassalSharp.tools.image.PNGChunkSkipInputStream) inPipe).read(ba):SupportClass.ReadInput(inPipe, ba, 0, ba.Length)) != - 1)
				print(new System.String(SupportClass.ToCharArray(ba), 0, read));
			
			println("Console: Input closed...");
		}
		
		public virtual void  Run()
		{
			try
			{
				inPipeWatcher();
			}
			catch (System.IO.IOException e)
			{
				println("Console: I/O Error...");
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			AWTConsole console = new AWTConsole();
			//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			System.Windows.Forms.Form temp_frame;
			temp_frame = new System.Windows.Forms.Form();
			temp_frame.Text = "Bsh Console";
			System.Windows.Forms.Form f = temp_frame;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			f.Controls.Add(console);
			console.Dock = System.Windows.Forms.DockStyle.Fill;
			console.BringToFront();
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			f.pack();
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Show();
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			f.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(f).windowClosing);
			
			Interpreter interpreter = new Interpreter(console);
			interpreter.Run();
		}
		
		public override System.String ToString()
		{
			return "BeanShell AWTConsole";
		}
		
		// unused
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
	}
}