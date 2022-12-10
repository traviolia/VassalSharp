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
	//UPGRADE_TODO: Class 'java.applet.Applet' was converted to 'System.Windows.Forms.UserControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaappletApplet'"
	[Serializable]
	public class AWTDemoApplet:System.Windows.Forms.UserControl
	{
		public AWTDemoApplet()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.Load += new System.EventHandler(this.AWTDemoApplet_StartEventHandler);
			this.Disposed += new System.EventHandler(this.AWTDemoApplet_StopEventHandler);
		}
		public bool isActiveVar = true;
		public void  init()
		{
			InitializeComponent();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			setLayout(new BorderLayout());*/
			ConsoleInterface console = new AWTConsole();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			Controls.Add((System.Windows.Forms.Control) console);
			Interpreter interpreter = new Interpreter(console);
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(interpreter.Run)).Start();
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
		private void  AWTDemoApplet_StartEventHandler(System.Object sender, System.EventArgs e)
		{
			init();
			start();
		}
		private void  AWTDemoApplet_StopEventHandler(System.Object sender, System.EventArgs e)
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