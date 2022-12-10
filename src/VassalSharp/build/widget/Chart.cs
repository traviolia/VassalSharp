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
using Buildable = VassalSharp.build.Buildable;
using Widget = VassalSharp.build.Widget;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using AdjustableSpeedScrollPane = VassalSharp.tools.AdjustableSpeedScrollPane;
using DataArchive = VassalSharp.tools.DataArchive;
using Op = VassalSharp.tools.imageop.Op;
using OpIcon = VassalSharp.tools.imageop.OpIcon;
using SourceOp = VassalSharp.tools.imageop.SourceOp;
namespace VassalSharp.build.widget
{
	
	/// <summary> A Chart is used for displaying charts and tables for the module.
	/// The charts are loaded as images stored in the DataArchive. As a subclass
	/// of Widget, a Chart may be added to any Widget, but it may not contain
	/// children of its own.
	/// </summary>
	public class Chart:Widget
	{
		private void  InitBlock()
		{
			return new System.Type[0];
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				String.
			}
		}
		override public System.Windows.Forms.Control Component
		{
			get
			{
				if (chart == null)
				{
					label = new System.Windows.Forms.Label();
					srcOp = fileName == null || fileName.Trim().Length == 0?null:Op.load(fileName);
					if (srcOp != null)
					{
						label.Image = new OpIcon(srcOp);
					}
					/*
					try {
					Image image = GameModule.getGameModule().getDataArchive().getCachedImage(fileName);
					ImageIcon icon = image == null ? null : new ImageIcon(image);
					label.setIcon(icon);
					}
					catch (IOException ex) {
					label.setText("Image " + fileName + " not found");
					}*/
					//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Drawing.Size d = label.Size;
					if (d.Width > 300 || d.Height > 300)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						System.Windows.Forms.ScrollableControl scroll = new AdjustableSpeedScrollPane(label);
						//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
						//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						scroll.getViewport().Size = label.Size;
						//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
						//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
						scroll.getViewport().setAlignmentY(0.0F);
						chart = scroll;
					}
					else
					{
						chart = label;
					}
				}
				return chart;
			}
			
		}
		virtual public System.String FileName
		{
			get
			{
				return fileName;
			}
			
		}
		/// <summary> The Attributes of a Chart are:
		/// 
		/// <pre>
		/// <code>
		/// NAME
		/// </code>
		/// for the name of the chart
		/// <code>
		/// FILE
		/// </code>
		/// for the name of the image in the {@link DataArchive}
		/// </pre>
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
				return new System.String[]{"Name:  ", "Image:  "};
			}
			
		}
		new public const System.String NAME = "chartName";
		public const System.String FILE = "fileName";
		private System.Windows.Forms.Control chart;
		private System.String fileName;
		private SourceOp srcOp;
		private System.Windows.Forms.Label label;
		
		public Chart()
		{
			InitBlock();
		}
		
		public override void  addTo(Buildable parent)
		{
		}
		
		public override void  removeFrom(Buildable parent)
		{
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("ChartWindow.htm", "Chart");
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
				if (label != null)
				{
					/*
					try {
					Image image = GameModule.getGameModule().getDataArchive().getCachedImage(fileName);
					ImageIcon icon = image == null ? null : new ImageIcon(image);
					label.setIcon(icon);
					label.revalidate();
					}
					catch (IOException ex) {
					}*/
					srcOp = fileName == null || fileName.Trim().Length == 0?null:Op.load(fileName);
					if (srcOp != null)
					{
						label.Image = new OpIcon(srcOp);
						label.Invalidate();
					}
				}
			}
		}
		
		/*
		* public Configurer[] getAttributeConfigurers() { Configurer config[] = new Configurer[2]; config[0] = new
		* StringConfigurer(NAME,"Name"); config[0].setValue(getConfigureName()); listenTo(config[0]);
		*
		* config[1] = new ImageConfigurer (FILE,"Image", GameModule.getGameModule().getArchiveWriter());
		* config[1].setValue(fileName); listenTo(config[1]);
		*
		* return config; }
		*/
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, Image.
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
	public static String getConfigureTypeName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Chart;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}