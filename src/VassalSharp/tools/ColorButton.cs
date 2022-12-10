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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
namespace VassalSharp.tools
{
	
	/// <summary> A {@link JButton} which displays a color swatch.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	public class ColorButton:System.Windows.Forms.Button
	{
		/// <summary> Set the color of the button.
		/// 
		/// </summary>
		/// <param name="c">the color to set
		/// </param>
		virtual public System.Drawing.Color Color
		{
			set
			{
				color = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		private static System.Drawing.Font FONT = new System.Drawing.Font("Dialog", 10, System.Drawing.FontStyle.Regular);
		
		private System.Drawing.Color color
		{
			get
			{
				return color_Renamed;
			}
			
			set
			{
				color_Renamed = value;
			}
			
		}
		private System.Drawing.Color color_Renamed;
		
		/// <summary> Create a button with no color.</summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public ColorButton():this(ref tempAux)
		{
		}
		
		/// <summary> Create a button with the specified color.
		/// 
		/// </summary>
		/// <param name="c">the color to set
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public ColorButton(ref System.Drawing.Color c):base()
		{
			color = c;
			Image = new SwatchIcon(this, 30, 15);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setMargin' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetMargin_javaawtInsets'"
			setMargin(new System.Int32[]{2, 2, 2, 2});
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SwatchIcon' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class SwatchIcon : System.Drawing.Image
		{
			private void  InitBlock(ColorButton enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ColorButton enclosingInstance;
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public int Width
			{
				get
				{
					return swatchWidth;
				}
				
			}
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public int Height
			{
				get
				{
					return swatchHeight;
				}
				
			}
			public ColorButton Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'swatchWidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int swatchWidth;
			//UPGRADE_NOTE: Final was removed from the declaration of 'swatchHeight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int swatchHeight;
			
			public SwatchIcon(ColorButton enclosingInstance, int width, int height)
			{
				InitBlock(enclosingInstance);
				swatchWidth = width;
				swatchHeight = height;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.Icon.paintIcon' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public virtual void  paintIcon(System.Windows.Forms.Control c, System.Drawing.Graphics g, int x, int y)
			{
				SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), x, y, swatchWidth - 1, swatchHeight - 1);
				
				if (!Enclosing_Instance.color.IsEmpty)
				{
					SupportClass.GraphicsManager.manager.SetColor(g, Enclosing_Instance.color);
					g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), x + 1, y + 1, swatchWidth - 2, swatchHeight - 2);
				}
				else
				{
					// paint no color and a "nil" if the color is null
					//UPGRADE_TODO: Method 'javax.swing.UIManager.getColor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					SupportClass.GraphicsManager.manager.SetColor(g, UIManager.getColor("controlText"));
					SupportClass.GraphicsManager.manager.SetFont(g, VassalSharp.tools.ColorButton.FONT);
					//UPGRADE_TODO: Method 'java.awt.Graphics.drawString' was converted to 'System.Drawing.Graphics.DrawString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphicsdrawString_javalangString_int_int'"
					//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.stringWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricsstringWidth_javalangString'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getAscent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					g.DrawString("nil", SupportClass.GraphicsManager.manager.GetFont(g), SupportClass.GraphicsManager.manager.GetBrush(g), x + (swatchWidth - VassalSharp.tools.ColorButton.FONT.stringWidth("nil")) / 2, y + (swatchHeight + SupportClass.GetAscent(VassalSharp.tools.ColorButton.FONT)) / 2 - SupportClass.GraphicsManager.manager.GetFont(g).GetHeight());
				}
			}
		}
		private static System.Drawing.Color tempAux = System.Drawing.Color.Empty;
	}
}