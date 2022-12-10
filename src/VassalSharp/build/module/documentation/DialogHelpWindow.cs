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
#if NEVER_DEFINED
using System;
using HtmlChart = VassalSharp.build.widget.HtmlChart;
//UPGRADE_TODO: The type 'VassalSharp.build.widget.HtmlChart.XTMLEditorKit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XTMLEditorKit = VassalSharp.build.widget.HtmlChart.XTMLEditorKit;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using ScrollPane = VassalSharp.tools.ScrollPane;
#else
using System;
//using HtmlChart = VassalSharp.build.widget.HtmlChart;
////UPGRADE_TODO: The type 'VassalSharp.build.widget.HtmlChart.XTMLEditorKit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using XTMLEditorKit = VassalSharp.build.widget.HtmlChart.XTMLEditorKit;
//using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
//using ScrollPane = VassalSharp.tools.ScrollPane;
#endif

namespace VassalSharp.build.module.documentation
{

#if NEVER_DEFINED
	/// <summary> A Dialog that displays HTML content, with navigation</summary>
	[Serializable]
	public class DialogHelpWindow : System.Windows.Forms.Form
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'javax.swing.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.RichTextBox pane;
		
		public DialogHelpWindow(System.String title, System.Uri contents, System.Windows.Forms.Form parent):base()
		{
			SupportClass.DialogSupport.SetDialog(this, parent);
			Text = title;
			Closing += new System.ComponentModel.CancelEventHandler(this.DialogHelpWindow_Closing_HIDE_ON_CLOSE);
			//setJMenuBar(MenuManager.getInstance().getMenuBarFor(this));
			
			//UPGRADE_TODO: Class 'javax.swing.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			pane = new System.Windows.Forms.RichTextBox();
			pane.ReadOnly = !false;
			//UPGRADE_NOTE: Some methods of the 'javax.swing.event.HyperlinkListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			pane.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.hyperlinkUpdate);
			
			/*
			* Allow <src> tag to display images from the module DataArchive
			* where no pathname included in the image name.
			*/
			//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.setContentType' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanesetContentType_javalangString'"
			pane.setContentType("text/html"); //$NON-NLS-1$
			XTMLEditorKit myHTMLEditorKit = new HtmlChart.XTMLEditorKit();
			pane.setEditorKit(myHTMLEditorKit);
			
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl scroll = new ScrollPane(pane);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(scroll);
			update(contents);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			Toolkit.getDefaultToolkit();
			System.Drawing.Size d = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
			int width = System.Math.Max(d.Width / 2, Size.Width);
			int height = System.Math.Max(d.Height / 2, Size.Height);
			width = System.Math.Min(width, d.Width * 2 / 3);
			height = System.Math.Min(height, d.Height * 2 / 3);
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			Size = new System.Drawing.Size(width, height);
			//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
			Location = new System.Drawing.Point(d.Width / 2 - width / 2, 0);
		}
		
		public virtual void  hyperlinkUpdate(System.Object event_sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			//UPGRADE_ISSUE: Field 'javax.swing.event.HyperlinkEvent.EventType.ACTIVATED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventHyperlinkEventEventType'"
			//UPGRADE_ISSUE: Method 'javax.swing.event.HyperlinkEvent.getEventType' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventHyperlinkEventgetEventType'"
			if (HyperlinkEvent.EventType.ACTIVATED.Equals(e.getEventType()))
			{
				if (new System.Uri(e.LinkText) != null)
				{
					update(new System.Uri(e.LinkText));
				}
			}
		}
		
		public virtual void  update(System.Uri contents)
		{
			if (contents != null)
			{
				try
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.setPage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanesetPage_javanetURL'"
					pane.setPage(contents);
				}
				catch (System.IO.IOException e)
				{
					ReadErrorDialog.error(e, contents.ToString());
				}
			}
			else
			{
				pane.Text = ""; //$NON-NLS-1$
			}
		}
		private void  DialogHelpWindow_Closing_HIDE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 1);
		}
	}
#else
	/// <summary> A Dialog that displays HTML content, with navigation</summary>
	[Serializable]
	public class DialogHelpWindow : System.Windows.Forms.Form
	{
		public DialogHelpWindow(System.String title, System.Uri contents, System.Windows.Forms.Form parent) : base()
		{
		}

		public virtual void hyperlinkUpdate(System.Object event_sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
		}

		public virtual void update(System.Uri contents)
		{
		}
		private void DialogHelpWindow_Closing_HIDE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form)sender, 1);
		}
	}
#endif
}