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
using BadDataReport = VassalSharp.build.BadDataReport;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Widget = VassalSharp.build.Widget;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using ScrollPane = VassalSharp.tools.ScrollPane;
using Op = VassalSharp.tools.imageop.Op;
using OpIcon = VassalSharp.tools.imageop.OpIcon;
using SourceOp = VassalSharp.tools.imageop.SourceOp;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.build.widget
{
	
	/// <summary> An HtmlChart is used for displaying html information for the module. The
	/// charts are loaded as html files stored in the DataArchive. As a subclass of
	/// Widget, a Chart may be added to any Widget, but it may not contain children
	/// of its own
	/// </summary>
	public class HtmlChart:Widget
	{
		static private System.Int32 state318;
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state318 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
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
		private bool URL
		{
			get
			{
				//UPGRADE_ISSUE: Method 'javax.swing.text.Document.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
				return ((System.String) htmlWin.Text).getProperty("stream") != null;
			}
			
		}
		private System.String Text
		{
			set
			{
				htmlWin.Text = value;
				// ensure hyperlink engine knows we are no longer at the last URL
				//UPGRADE_ISSUE: Method 'javax.swing.text.Document.putProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
				((System.String) htmlWin.Text).putProperty("stream", null);
				htmlWin.Invalidate();
			}
			
		}
		override public System.Windows.Forms.Control Component
		{
			// Warning: Creating a JEditorPane with a "jar" url or using setPage()
			// with a jar URL will leave a resource open in the MOD file, making it
			// impossible to save or rename it. This might be acceptable for people
			// playing a module, but is unacceptable for editors; they can only save
			// their work to a new file. Therefore, we read the entire file instead
			// of simply using:
			//    GameModule.getGameModule().getDataArchive().getURL( fileName );
			
			get
			{
				if (htmlWin == null)
				{
					//UPGRADE_TODO: Class 'javax.swing.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					htmlWin = new System.Windows.Forms.RichTextBox();
					htmlWin.ReadOnly = !false;
					//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.setContentType' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanesetContentType_javalangString'"
					htmlWin.setContentType("text/html");
					XTMLEditorKit myHTMLEditorKit = new XTMLEditorKit();
					//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.setEditorKit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanesetEditorKit_javaxswingtextEditorKit'"
					htmlWin.setEditorKit(myHTMLEditorKit);
					
					//UPGRADE_NOTE: Some methods of the 'javax.swing.event.HyperlinkListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
					htmlWin.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(new HtmlChartHyperlinkListener().hyperlinkUpdate);
					htmlWin.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.widget.HtmlChart.mouseDown);
					
					setFile(fileName);
					
					scroller = new ScrollPane(htmlWin);
					//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
					//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.getPreferredSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanegetPreferredSize'"
					scroller.getViewport().Size = htmlWin.getPreferredSize();
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
					//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
					scroller.getViewport().setAlignmentY(0.0F);
				}
				return scroller;
			}
			
		}
		virtual public System.String FileName
		{
			get
			{
				return fileName;
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "HTML Chart";
			}
			
		}
		/// <summary> The Attributes of a Chart are:
		/// <dl>
		/// <dt><code>NAME</code></dt><dd>for the name of the chart</dd>
		/// <dt><code>FILE</code></dt><dd>for the name of the HTML file
		/// in the {@link VassalSharp.tools.DataArchive}</dd>
		/// </dl>
		/// </summary>
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{NAME, FILE};
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{"Name:  ", "HTML File:  "};
			}
			
		}
		new public const System.String NAME = "chartName";
		public const System.String FILE = "fileName";
		
		private System.String fileName;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.ScrollableControl scroller;
		//UPGRADE_TODO: Class 'javax.swing.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.RichTextBox htmlWin;
		
		public HtmlChart()
		{
			InitBlock();
		}
		
		private void  setFile(System.String fname)
		{
			Text = getFile(fname);
		}
		
		private System.String getFile(System.String fname)
		{
			if (fname == null)
				return null;
			
			System.String s = null;
			System.IO.Stream in_Renamed = null;
			try
			{
				in_Renamed = new BufferedInputStream(GameModule.getGameModule().getDataArchive().getInputStream(fname));
				s = IOUtils.toString(in_Renamed);
				in_Renamed.Close();
			}
			catch (System.IO.IOException e)
			{
				ErrorDialog.dataError(new BadDataReport(this, Resources.getString("Error.not_found", "Chart"), fname, e));
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			return s;
		}
		
		public override void  addTo(Buildable parent)
		{
		}
		
		public override void  removeFrom(Buildable parent)
		{
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("ChartWindow.htm", "HtmlChart");
		}
		
		public override void  setAttribute(System.String key, System.Object val)
		{
			if (NAME.Equals(key))
			{
				setConfigureName((System.String) val);
			}
			else if (FILE.Equals(key))
			{
				if (val is System.IO.FileInfo)
				{
					val = ((System.IO.FileInfo) val).Name;
				}
				fileName = ((System.String) val);
				if (htmlWin != null)
				{
					setFile(fileName);
				}
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, File.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(NAME.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return getConfigureName();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FILE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return fileName;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mousePressed(MouseEvent event)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(event.isMetaDown())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final JPopupMenu popup = new JPopupMenu();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final JMenuItem item = new JMenuItem(Return to default page);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	item.addActionListener(new ActionListener()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// Return to default page
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void actionPerformed(ActionEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setFile(fileName);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	popup.add(item);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(event.getComponent().isShowing())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		popup.show(event.getComponent(), event.getX(), event.getY());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseClicked(MouseEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseEntered(MouseEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseExited(MouseEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseReleased(MouseEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class HtmlChartHyperlinkListener
	{
		public virtual void  hyperlinkUpdate(System.Object event_sender, System.Windows.Forms.LinkClickedEventArgs event_Renamed)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.event.HyperlinkEvent.getEventType' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventHyperlinkEventgetEventType'"
			//UPGRADE_ISSUE: Field 'javax.swing.event.HyperlinkEvent.EventType.ACTIVATED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventHyperlinkEventEventType'"
			if (event_Renamed.getEventType() != HyperlinkEvent.EventType.ACTIVATED)
			{
				return ;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'desc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'javax.swing.event.HyperlinkEvent.getDescription' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventHyperlinkEventgetDescription'"
			System.String desc = event_Renamed.getDescription();
			if ((!isURL() && desc.IndexOf('/') < 0) || new System.Uri(event_Renamed.LinkText) == null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int hash = desc.LastIndexOf("#");
				if (hash < 0)
				{
					// no anchor
					setFile(desc);
				}
				else if (hash > 0)
				{
					// browse to the part before the anchor
					setFile(desc.Substring(0, (hash) - (0)));
				}
				
				if (hash != - 1)
				{
					// we have an anchor
					htmlWin.scrollToReference(desc.Substring(hash + 1));
				}
			}
			else
			{
				try
				{
					htmlWin.setPage(new System.Uri(event_Renamed.LinkText));
				}
				catch (System.IO.IOException ex)
				{
					ReadErrorDialog.error(ex, new System.Uri(event_Renamed.LinkText).ToString());
				}
				htmlWin.revalidate();
			}
		}
	}
	
	/// <summary> Extended HTML Editor kit to extend the <src> tag to display images
	/// from the module DataArchive where no pathname included in the image name.
	/// The image is placed on a label and returned as a ComponentView. An
	/// ImageView cannot be used as the standard Java HTML Renderer can only
	/// display Images from an external URL.
	/// </summary>
	//UPGRADE_ISSUE: Class 'javax.swing.text.html.HTMLEditorKit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtexthtmlHTMLEditorKit'"
	[Serializable]
	public class XTMLEditorKit:HTMLEditorKit
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_ISSUE: Interface 'javax.swing.text.ViewFactory' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextViewFactory'"
		public ViewFactory getViewFactory()
		{
			return new XTMLFactory();
		}
		
		//UPGRADE_TODO: Class 'javax.swing.text.html.HTMLEditorKit.HTMLFactory' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		//UPGRADE_ISSUE: Interface 'javax.swing.text.ViewFactory' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextViewFactory'"
		public class XTMLFactory:HTMLFactory, ViewFactory
		{
			//UPGRADE_TODO: Constructor 'javax.swing.text.html.HTMLEditorKit.HTMLFactory.HTMLFactory' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public XTMLFactory():base()
			{
			}
			
			//UPGRADE_ISSUE: Class 'javax.swing.text.View' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextView'"
			//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
			public View create(javax.swing.text.Element element)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'kind '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getAttributes' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				//UPGRADE_ISSUE: Field 'javax.swing.text.StyleConstants.NameAttribute' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextStyleConstants'"
				System.Web.UI.HtmlTextWriterTag kind = (System.Web.UI.HtmlTextWriterTag) (element.getAttributes()[javax.swing.text.StyleConstants.NameAttribute]);
				
				//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getName' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				if (kind is System.Web.UI.HtmlTextWriterTag && element.getName().Equals("img"))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'imageName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getAttributes' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
					System.String imageName = (System.String) element.getAttributes()[System.Web.UI.HtmlTextWriterAttribute.Src];
					if (imageName.IndexOf("/") < 0)
					{
						return new ImageComponentView(element);
					}
				}
				//UPGRADE_TODO: Method 'javax.swing.text.html.HTMLEditorKit.HTMLFactory.create' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				return base.create(element);
			}
			
			//UPGRADE_ISSUE: Class 'javax.swing.text.ComponentView' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextComponentView'"
			public class ImageComponentView:ComponentView
			{
				protected internal System.String imageName;
				protected internal SourceOp srcOp;
				
				/// <summary> Very basic Attribute handling only. Expand as needed.</summary>
				//UPGRADE_ISSUE: Constructor 'javax.swing.text.ComponentView.ComponentView' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextComponentView'"
				//UPGRADE_ISSUE: Interface 'javax.swing.text.Element' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
				public ImageComponentView(Element e):base(e)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.text.Element.getAttributes' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextElement'"
					imageName = ((System.String) e.getAttributes()[System.Web.UI.HtmlTextWriterAttribute.Src]);
					srcOp = imageName == null || imageName.Trim().Length == 0?null:Op.load(imageName);
				}
				
				protected internal System.Windows.Forms.Control createComponent()
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.Label label = new System.Windows.Forms.Label();
					label.Image = new OpIcon(srcOp);
					return label;
				}
			}
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}