/*
* $Id$
*
* Copyright (c) 2007 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An implementation of {@link Icon} using an {@link ImageOp} as a source.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
	[Serializable]
	public class OpIcon : System.Drawing.Image
	{
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		new virtual public int Width
		{
			get
			{
				return sop == null?- 1:sop.getWidth();
			}
			
		}
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		new virtual public int Height
		{
			get
			{
				return sop == null?- 1:sop.getHeight();
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the <code>ImageOp</code> which produces this icon's
		/// <code>Image</code>.
		/// 
		/// </summary>
		/// <returns> the <code>ImageOp</code> for this <code>OpIcon</code>
		/// </returns>
		/// <summary> Sets the <code>ImageOp</code> which produces this icon's
		/// <code>Image</code>.
		/// 
		/// </summary>
		/// <param name="op">the <code>ImageOp</code>
		/// </param>
		virtual public ImageOp Op
		{
			get
			{
				return sop;
			}
			
			set
			{
				sop = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		protected internal ImageOp sop;
		
		/// <summary> Creates an uninitialized icon.</summary>
		public OpIcon()
		{
		}
		
		/// <summary> Creates an <code>OpIcon</code> using a given <code>ImageOp</code> as
		/// its image source.
		/// 
		/// </summary>
		/// <param name="op">the <code>ImageOp</code> to be used by this <code>OpIcon</code>
		/// </param>
		public OpIcon(ImageOp op)
		{
			sop = op;
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// <p>The given <code>ImageOp</code> is called asynchronously when painting,
		/// so as not to block the Event Dispatch Thread.</p>
		/// 
		/// </summary>
		/// <param name="c">{@inheritDoc}
		/// </param>
		/// <param name="g">{@inheritDoc}
		/// </param>
		/// <param name="x">{@inheritDoc}
		/// </param>
		/// <param name="y">{@inheritDoc}
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.ImageIcon.paintIcon' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public virtual void  paintIcon(System.Windows.Forms.Control c, System.Drawing.Graphics g, int x, int y)
		{
			if (sop == null)
				return ;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Graphics.getClipBounds' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Drawing.Rectangle tempAux = System.Drawing.Rectangle.Truncate(g.ClipBounds);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			Repainter r = c == null?null:new Repainter(c, ref tempAux);
			
			try
			{
				g.drawImage(sop.getImage(r), x, y, c);
			}
			catch (CancellationException e)
			{
				ErrorDialog.bug(e);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				ErrorDialog.bug(e);
			}
			catch (ExecutionException e)
			{
				if (!Op.handleException(e))
					ErrorDialog.bug(e);
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.ImageIcon.getImage' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Drawing.Image getImage()
		{
			return sop == null?null:sop.getImage();
		}
		
		/// <summary> This method does nothing. It is overridden to prevent the
		/// image from being set this way.
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.ImageIcon.setImage' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  setImage(System.Drawing.Image img)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
	}
}