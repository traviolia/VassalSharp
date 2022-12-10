/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
namespace VassalSharp.tools.swing
{
	
	
	/// <summary> A label which word-wraps and fully justifies its text, and which
	/// reflows the text when resized.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JEditorPane' and 'System.Windows.Forms.RichTextBox' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	[Serializable]
	public class FlowLabel:System.Windows.Forms.RichTextBox
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassComponentAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassComponentAdapter
		{
			public AnonymousClassComponentAdapter(FlowLabel enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FlowLabel enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FlowLabel enclosingInstance;
			public FlowLabel Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public void  componentResized(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Size tempAux = System.Drawing.Size.Empty;
				Enclosing_Instance.Size = tempAux;
				//UPGRADE_ISSUE: Method 'java.awt.Component.removeComponentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentremoveComponentListener_javaawteventComponentListener'"
				Enclosing_Instance.removeComponentListener(this);
			}
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.String Text
		{
			get
			{
				return base.Text;
			}
			
			set
			{
				// check for HTML
				//UPGRADE_TODO: Method 'javax.swing.plaf.basic.BasicHTML.isHTMLString' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				if (BasicHTML.isHTMLString(value))
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.setContentType' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanesetContentType_javalangString'"
					setContentType("text/html");
				}
				base.Text = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private const int DEFAULT_WIDTH = 40;
		
		/// <summary> Creates a <code>FlowLabel</code> with the desired text and
		/// an initial width of 40em.
		/// 
		/// </summary>
		/// <param name="text">the text for the label
		/// </param>
		public FlowLabel(System.String text):this(text, DEFAULT_WIDTH)
		{
		}
		
		/// <summary> Creates a <code>FlowLabel</code> with the desired text
		/// and width.
		/// 
		/// </summary>
		/// <param name="text">the text for the label
		/// </param>
		/// <param name="width">the initial width of the label in em
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JEditorPane' and 'System.Windows.Forms.RichTextBox' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public FlowLabel(System.String text, int width):base()
		{
			ReadOnly = !false;
			Text = text;
			
			// FIXME: This is a workaround for Redhat Bugzilla Bug #459967:
			// JTextPane.setBackground() fails when using GTK LookAndFeel. Once this
			// bug is resolved, there is no need to make this component nonopaque.
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setOpaque' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetOpaque_boolean'"
			setOpaque(false);
			
			// set the colors and font a JLabel would have
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.putClientProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentputClientProperty_javalangObject_javalangObject'"
			putClientProperty(HONOR_DISPLAY_PROPERTIES, true);
			//UPGRADE_TODO: Method 'javax.swing.UIManager.getFont' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			Font = UIManager.getFont("Label.font");
			//UPGRADE_TODO: Method 'javax.swing.UIManager.getColor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ForeColor = UIManager.getColor("Label.foreground");
			//UPGRADE_TODO: Method 'javax.swing.UIManager.getColor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			BackColor = UIManager.getColor("Label.background");
			
			// set full justification for the text
			//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Interface 'javax.swing.text.StyledDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyledDocument'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTextPane.getStyledDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTextPanegetStyledDocument'"
			StyledDocument doc = getStyledDocument();
			//UPGRADE_NOTE: Final was removed from the declaration of 'sa '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Collections.Hashtable sa = new System.Collections.Hashtable();
			//UPGRADE_ISSUE: Method 'javax.swing.text.StyleConstants.setAlignment' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
			//UPGRADE_ISSUE: Field 'javax.swing.text.StyleConstants.ALIGN_JUSTIFIED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
			StyleConstants.setAlignment(sa, StyleConstants.ALIGN_JUSTIFIED);
			//UPGRADE_ISSUE: Method 'javax.swing.text.StyledDocument.setParagraphAttributes' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyledDocument'"
			//UPGRADE_ISSUE: Method 'javax.swing.text.Document.getLength' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
			doc.setParagraphAttributes(0, doc.getLength(), sa, false);
			
			//
			// This is a kludge to get around the fact that Swing layouts don't
			// support methods like getHeightForWidth(int) and so have no sensible
			// way of sizing widgets whose height and width are interdependent.
			//
			
			// convert the initial width from em to pixels
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int w = width * (int) Font.Size;
			
			// determine the preferred height at the initial width
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size d = new System.Drawing.Size(w, System.Int32.MaxValue);
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
			Size = d;
			//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.getPreferredSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanegetPreferredSize'"
			d.Height = getPreferredSize().Height;
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			Size = d;
			
			// unset the preferred size once we are laid out the first time
			Resize += new System.EventHandler(new AnonymousClassComponentAdapter(this).componentResized);
			
			//
			// end of preferred size kludge
			//
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'loremIpsum '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Form d = SupportClass.DialogSupport.CreateDialog();
			d.Text = "Flow Label Test";
			//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
			d.setModal(true);
			d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			d.Closing += new System.ComponentModel.CancelEventHandler(FlowLabel.FlowLabel_Closing_DISPOSE_ON_CLOSE);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = new FlowLabel(loremIpsum + "\n\n" + loremIpsum);
			d.Controls.Add(temp_Control);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			d.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(d, "Visible", true);
		}
		private static void  FlowLabel_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
		}
	}
}