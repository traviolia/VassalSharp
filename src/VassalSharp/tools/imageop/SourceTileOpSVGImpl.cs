/*
* $Id$
*
* Copyright (c) 2007-2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.builder.HashCodeBuilder' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using HashCodeBuilder = org.apache.commons.lang.builder.HashCodeBuilder;
using GameModule = VassalSharp.build.GameModule;
using DataArchive = VassalSharp.tools.DataArchive;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageNotFoundException = VassalSharp.tools.image.ImageNotFoundException;
using SVGRenderer = VassalSharp.tools.image.svg.SVGRenderer;
//UPGRADE_TODO: The type 'VassalSharp.tools.opcache.Op' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Op = VassalSharp.tools.opcache.Op;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} for producing tiles directly from a source,
	/// without cobbling tiles from the source.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class SourceTileOpSVGImpl:AbstractTileOpImpl, SVGOp
	{
		private void  InitBlock()
		{
			return Collections.emptyList();
		}
		virtual public System.String Name
		{
			get
			{
				return sop.Name;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'sop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private SVGOp sop;
		//UPGRADE_NOTE: Final was removed from the declaration of 'x0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'y0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'x1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'y1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int x0;
		private int y0;
		private int x1;
		private int y1;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int hash;
		
		public SourceTileOpSVGImpl(SVGOp sop, int tileX, int tileY)
		{
			InitBlock();
			if (sop == null)
				throw new System.ArgumentException();
			
			if (tileX < 0 || tileX >= sop.getNumXTiles() || tileY < 0 || tileY >= sop.getNumYTiles())
				throw new System.IndexOutOfRangeException();
			
			this.sop = sop;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int tw = sop.getTileWidth();
			//UPGRADE_NOTE: Final was removed from the declaration of 'th '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int th = sop.getTileHeight();
			//UPGRADE_NOTE: Final was removed from the declaration of 'sw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int sw = sop.getWidth();
			//UPGRADE_NOTE: Final was removed from the declaration of 'sh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int sh = sop.getHeight();
			
			x0 = tileX * tw;
			y0 = tileY * th;
			x1 = System.Math.Min((tileX + 1) * tw, sw);
			y1 = System.Math.Min((tileY + 1) * th, sh);
			
			size = new System.Drawing.Size(x1 - x0, y1 - y0);
			
			hash = new HashCodeBuilder().append(sop).append(x0).append(y0).append(x1).append(y1).toHashCode();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < Op < ? >> getSources()
		
		public override System.Drawing.Bitmap eval()
		{
			// FIXME: getting archive this way is a kludge, we should get it from sop
			//UPGRADE_NOTE: Final was removed from the declaration of 'archive '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DataArchive archive = GameModule.getGameModule().getDataArchive();
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String name = Name;
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'renderer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SVGRenderer renderer = new SVGRenderer(archive.getURL(name), new System.IO.BufferedStream(archive.getInputStream(name)));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'aoi '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.RectangleF aoi = new System.Drawing.RectangleF(x0, y0, x1 - x0, y1 - y0);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return renderer.render(0.0, 1.0, ref aoi);
			}
			catch (System.IO.FileNotFoundException e)
			{
				throw new ImageNotFoundException(name, e);
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(name, e);
			}
		}
		
		protected internal override void  fixSize()
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (o == null || o.GetType() != this.GetType())
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'op '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SourceTileOpSVGImpl op = (SourceTileOpSVGImpl) o;
			return x0 == op.x0 && y0 == op.y0 && x1 == op.x1 && y1 == op.y1 && sop.Equals(op.sop);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return hash;
		}
	}
}