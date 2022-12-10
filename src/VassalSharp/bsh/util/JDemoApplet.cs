/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// BeanShell is distributed under the terms of the LGPL:                    *
/// GNU Library Public License http://www.gnu.org/copyleft/lgpl.html         *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Exploring Java, O'Reilly & Associates                          *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using bsh;
using bsh.util;
namespace bsh.util
{
	
	/// <summary>Run bsh as an applet for demo purposes.</summary>
	//UPGRADE_TODO: Class 'javax.swing.JApplet' was converted to 'System.Windows.Forms.UserControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJApplet'"
	[Serializable]
	public class JDemoApplet:System.Windows.Forms.UserControl
	{
		public JDemoApplet()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.Load += new System.EventHandler(this.JDemoApplet_StartEventHandler);
			this.Disposed += new System.EventHandler(this.JDemoApplet_StopEventHandler);
		}
		public bool isActiveVar = true;
		public void  init()
		{
			InitializeComponent();
			System.String debug = getParameter("debug");
			if (debug != null && debug.Equals("true"))
				Interpreter.DEBUG = true;
			
			System.String type = getParameter("type");
			if (type != null && type.Equals("desktop"))
			// start the desktop
				try
				{
					new Interpreter().eval("desktop()");
				}
				catch (TargetError te)
				{
					te.printStackTrace();
					//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Out.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Out.WriteLine(te.Target);
					SupportClass.WriteStackTrace(te.Target, Console.Error);
				}
				catch (EvalError evalError)
				{
					//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Out.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Out.WriteLine(evalError);
					SupportClass.WriteStackTrace(evalError, Console.Error);
				}
			else
			{
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
				/*
				((System.Windows.Forms.ContainerControl) this).setLayout(new BorderLayout());*/
				JConsole console = new JConsole();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
				((System.Windows.Forms.ContainerControl) this).Controls.Add(console);
				Interpreter interpreter = new Interpreter(console);
				new SupportClass.ThreadClass(new System.Threading.ThreadStart(interpreter.Run)).Start();
			}
		}
		public void  ResizeControl(System.Drawing.Size p)
		{
			this.Width = p.Width;
			this.Height = p.Height;
		}
		public void  ResizeControl(int p2, int p3)
		{
			this.Width = p2;
			this.Height = p3;
		}
		public System.String GetUserControlInfo()
		{
			return null;
		}
		public System.String[][] GetParameterInfo()
		{
			return null;
		}
		public System.String  TempDocumentBaseVar = "";
		public virtual System.Uri DocumentBase
		{
			get
			{
				if (TempDocumentBaseVar == "")
					return new System.Uri("http://127.0.0.1");
				else
					return new System.Uri(TempDocumentBaseVar);
			}
			
		}
		public System.Drawing.Image getImage(System.Uri p4)
		{
			Bitmap TemporalyBitmap = new Bitmap(p4.AbsolutePath);
			return (Image) TemporalyBitmap;
		}
		public System.Drawing.Image getImage(System.Uri p5, System.String p6)
		{
			Bitmap TemporalyBitmap = new Bitmap(p5.AbsolutePath + p6);
			return (Image) TemporalyBitmap;
		}
		public virtual System.Boolean isActive()
		{
			return isActiveVar;
		}
		public virtual void  start()
		{
			isActiveVar = true;
		}
		public virtual void  stop()
		{
			isActiveVar = false;
		}
		private void  JDemoApplet_StartEventHandler(System.Object sender, System.EventArgs e)
		{
			init();
			start();
		}
		private void  JDemoApplet_StopEventHandler(System.Object sender, System.EventArgs e)
		{
			stop();
		}
		public virtual String getParameter(System.String paramName)
		{
			return null;
		}
		#region Windows Form Designer generated code
		private void  InitializeComponent()
		{
			this.SuspendLayout();
			this.BackColor = Color.LightGray;
			this.ResumeLayout(false);
		}
		#endregion
	}
}