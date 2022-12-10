/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
namespace VassalSharp.tools
{
	
	/// <summary> FlowLayout subclass that fully supports wrapping of components.</summary>
	[Serializable]
	public class WrapLayout:System.Array
	{
		private const long serialVersionUID = 1L;
		
		// The preferred size for this container.
		private System.Drawing.Size preferredLayoutSize_Renamed_Field;
		
		/// <summary> Constructs a new <code>WrapLayout</code> with a left
		/// alignment and a default 5-unit horizontal and vertical gap.
		/// </summary>
		//UPGRADE_TODO: Constructor 'java.awt.FlowLayout.FlowLayout' was converted to 'System.Object[]' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFlowLayoutFlowLayout_int'"
		public WrapLayout():base((int) System.Drawing.ContentAlignment.TopLeft)
		{
		}
		
		/// <summary> Constructs a new <code>FlowLayout</code> with the specified
		/// alignment and a default 5-unit horizontal and vertical gap.
		/// The value of the alignment argument must be one of
		/// <code>WrapLayout</code>, <code>WrapLayout</code>,
		/// or <code>WrapLayout</code>.
		/// </summary>
		/// <param name="align">the alignment value
		/// </param>
		//UPGRADE_TODO: Constructor 'java.awt.FlowLayout.FlowLayout' was converted to 'System.Object[]' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFlowLayoutFlowLayout_int'"
		public WrapLayout(int align):base(align)
		{
		}
		
		/// <summary> Creates a new flow layout manager with the indicated alignment
		/// and the indicated horizontal and vertical gaps.
		/// <p>
		/// The value of the alignment argument must be one of
		/// <code>WrapLayout</code>, <code>WrapLayout</code>,
		/// or <code>WrapLayout</code>.
		/// </summary>
		/// <param name="align">the alignment value
		/// </param>
		/// <param name="hgap">the horizontal gap between components
		/// </param>
		/// <param name="vgap">the vertical gap between components
		/// </param>
		//UPGRADE_TODO: Constructor 'java.awt.FlowLayout.FlowLayout' was converted to 'System.Object[]' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFlowLayoutFlowLayout_int_int_int'"
		public WrapLayout(int align, int hgap, int vgap):base(align, hgap, vgap)
		{
		}
		
		/// <summary> Returns the preferred dimensions for this layout given the
		/// <i>visible</i> components in the specified target container.
		/// </summary>
		/// <param name="target">the component which needs to be laid out
		/// </param>
		/// <returns> the preferred dimensions to lay out the
		/// subcomponents of the specified container
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'java.awt.FlowLayout.preferredLayoutSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Drawing.Size preferredLayoutSize(System.Windows.Forms.Control target)
		{
			return layoutSize(target, true);
		}
		
		/// <summary> Returns the minimum dimensions needed to layout the <i>visible</i>
		/// components contained in the specified target container.
		/// </summary>
		/// <param name="target">the component which needs to be laid out
		/// </param>
		/// <returns> the minimum dimensions to lay out the
		/// subcomponents of the specified container
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'java.awt.FlowLayout.minimumLayoutSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Drawing.Size minimumLayoutSize(System.Windows.Forms.Control target)
		{
			return layoutSize(target, false);
		}
		
		/// <summary> Returns the minimum or preferred dimension needed to layout the target
		/// container.
		/// 
		/// </summary>
		/// <param name="target">target to get layout size for
		/// </param>
		/// <param name="preferred">should preferred size be calculated
		/// </param>
		/// <returns> the dimension to layout the target container
		/// </returns>
		private System.Drawing.Size layoutSize(System.Windows.Forms.Control target, bool preferred)
		{
			//UPGRADE_ISSUE: Method 'java.awt.Component.getTreeLock' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentgetTreeLock'"
			lock (target.getTreeLock())
			{
				//  Each row must fit with the width allocated to the containter.
				//  When the container width = 0, the preferred width of the container
				//  has not yet been calculated so lets ask for the maximum.
				
				System.Drawing.Size targetSize = target.Size;
				int targetWidth = 0;
				
				if (targetSize.Width == 0)
					targetWidth = System.Int32.MaxValue;
				else
					targetWidth = targetSize.Width;
				
				//UPGRADE_ISSUE: Method 'java.awt.FlowLayout.getHgap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFlowLayoutgetHgap'"
				int hgap = getHgap();
				//UPGRADE_ISSUE: Method 'java.awt.FlowLayout.getVgap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFlowLayoutgetVgap'"
				int vgap = getVgap();
				System.Int32[] insets = SupportClass.GetInsets(target);
				int maxWidth = targetWidth - (insets[1] + insets[2] + hgap * 2);
				
				//  Fit components into the allowed width
				
				System.Drawing.Size dim = new System.Drawing.Size(0, 0);
				int rowWidth = 0;
				int rowHeight = 0;
				
				int nmembers = (int) target.Controls.Count;
				
				for (int i = 0; i < nmembers; i++)
				{
					System.Windows.Forms.Control m = target.Controls[i];
					
					//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
					if (m.Visible)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Component.getPreferredSize' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						//UPGRADE_ISSUE: Method 'java.awt.Component.getMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentgetMinimumSize'"
						System.Drawing.Size d = preferred?m.Size:m.getMinimumSize();
						
						if (rowWidth + d.Width > maxWidth)
						{
							//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
							addRow(ref dim, rowWidth, rowHeight);
							rowWidth = 0;
							rowHeight = 0;
						}
						
						if (rowWidth > 0)
						{
							//UPGRADE_ISSUE: Method 'java.awt.FlowLayout.getHgap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFlowLayoutgetHgap'"
							rowWidth += getHgap();
						}
						
						rowWidth += d.Width;
						rowHeight = System.Math.Max(rowHeight, d.Height);
					}
				}
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				addRow(ref dim, rowWidth, rowHeight);
				
				dim.Width += insets[1] + insets[2] + hgap * 2;
				dim.Height += insets[0] + insets[3] + vgap * 2;
				
				return dim;
			}
		}
		
		/// <summary> Add a new row to the given dimension.
		/// 
		/// </summary>
		/// <param name="dim">dimension to add row to
		/// </param>
		/// <param name="rowWidth">the width of the row to add
		/// </param>
		/// <param name="rowHeight">the height of the row to add
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private void  addRow(ref System.Drawing.Size dim, int rowWidth, int rowHeight)
		{
			dim.Width = System.Math.Max(dim.Width, rowWidth);
			
			if (dim.Height > 0)
			{
				//UPGRADE_ISSUE: Method 'java.awt.FlowLayout.getVgap' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFlowLayoutgetVgap'"
				dim.Height += getVgap();
			}
			
			dim.Height += rowHeight;
		}
		
		/// <summary> Trap for recursive failure to fit components</summary>
		internal bool recursing = false;
		
		/// <summary> Lays out the container using the FlowLayout. If the components as laid
		/// out do not fit in the size of then cause tree to be layout again unless
		/// this is a recursive call.
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'java.awt.FlowLayout.layoutContainer' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  layoutContainer(System.Windows.Forms.Control target)
		{
			//UPGRADE_ISSUE: Method 'java.awt.FlowLayout.layoutContainer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFlowLayoutlayoutContainer_javaawtContainer'"
			base.layoutContainer(target);
			
			/*
			* Now see how big a container is needed to hold all components
			*/
			int maxX = 0;
			int maxY = 0;
			
			for (int i = 0; i < (int) target.Controls.Count; i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'cmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Control cmp = target.Controls[i];
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (!cmp.Visible)
				{
					continue;
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle b = cmp.Bounds;
				
				if (b.X + b.Width > maxX)
				{
					maxX = b.X + b.Width;
				}
				
				if (b.Y + b.Height > maxY)
				{
					maxY = b.Y + b.Height;
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size size = target.Size;
			
			if (maxX > size.Width || maxY > size.Height)
			{
				if (recursing)
				{
					return ;
				}
				
				recursing = true;
				target.Invalidate();
				
				if (target is System.Windows.Forms.Control)
				{
					((System.Windows.Forms.Control) target).Invalidate();
				}
				
				recursing = false;
			}
		}
	}
}