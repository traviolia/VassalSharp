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
//UPGRADE_TODO: The type 'org.apache.commons.lang.builder.HashCodeBuilder' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using HashCodeBuilder = org.apache.commons.lang.builder.HashCodeBuilder;
using GameModule = VassalSharp.build.GameModule;
using DataArchive = VassalSharp.tools.DataArchive;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageNotFoundException = VassalSharp.tools.image.ImageNotFoundException;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using SVGRenderer = VassalSharp.tools.image.svg.SVGRenderer;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which rotates and scales its source. Rotation
	/// is about the center of the source image, and scaling ranges from
	/// <code>(0,Double.MAX_VALUE)</code>. If a source is to be both rotated
	/// and scaled, using one <code>RotateScaleOp</code> will produce better
	/// results than doing the rotation and scaling separately with
	/// one {@link RotateOp} and one {@link ScaleOp}.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class RotateScaleOpSVGImpl:AbstractTileOpImpl, RotateScaleOp, SVGOp
	{
		private void  InitBlock()
		{
			return Collections.emptyList();
		}
		/// <summary> Returns the angle of rotation.
		/// 
		/// </summary>
		/// <returns> the angle of rotation, in degrees.
		/// </returns>
		virtual public double Angle
		{
			get
			{
				return angle;
			}
			
		}
		/// <summary> Returns the scale factor.
		/// 
		/// </summary>
		/// <returns> the scale factor, in the range <code>(0,Double.MAX_VALUE]</code>.
		/// </returns>
		virtual public double Scale
		{
			get
			{
				return scale;
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				return sop.Name;
			}
			
		}
		virtual public System.Collections.Hashtable Hints
		{
			get
			{
				return hints;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'sop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private SVGOp sop;
		//UPGRADE_NOTE: Final was removed from the declaration of 'scale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private double scale;
		//UPGRADE_NOTE: Final was removed from the declaration of 'angle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private double angle;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hints '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Collections.Hashtable hints;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int hash;
		
		/// <summary> Constructs an <code>ImageOp</code> which will rotate and scale
		/// the image produced by its source <code>ImageOp</code>.
		/// 
		/// </summary>
		/// <param name="sop">the source operation
		/// </param>
		/// <param name="angle">the angle of rotation, in degrees
		/// </param>
		/// <param name="scale">the scale factor
		/// </param>
		public RotateScaleOpSVGImpl(SVGOp sop, double angle, double scale):this(sop, angle, scale, ImageUtils.DefaultHints)
		{
		}
		
		/// <summary> Constructs an <code>ImageOp</code> which will rotate and scale
		/// the image produced by its source <code>ImageOp</code>.
		/// 
		/// </summary>
		/// <param name="sop">the source operation
		/// </param>
		/// <param name="angle">the angle of rotation, in degrees
		/// </param>
		/// <param name="scale">the scale factor
		/// </param>
		/// <param name="hints">rendering hints
		/// </param>
		/// <throws>  IllegalArgumentException if <code>sop == null</code>. </throws>
		public RotateScaleOpSVGImpl(SVGOp sop, double angle, double scale, System.Collections.Hashtable hints)
		{
			InitBlock();
			if (sop == null)
				throw new System.ArgumentException();
			if (scale <= 0)
				throw new System.ArgumentException("scale = " + scale);
			
			this.sop = sop;
			this.angle = angle;
			this.scale = scale;
			this.hints = hints;
			
			hash = new HashCodeBuilder().append(sop).append(scale).append(angle).append(hints).toHashCode();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < VassalSharp.tools.opcache.Op < ? >> getSources()
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  Exception passed up from the source <code>ImageOp</code>. </throws>
		public override System.Drawing.Bitmap eval()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'archive '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DataArchive archive = GameModule.getGameModule().getDataArchive();
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String name = Name;
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'renderer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SVGRenderer renderer = new SVGRenderer(archive.getURL(name), new System.IO.BufferedStream(archive.getInputStream(name)));
				
				if (size.IsEmpty)
					fixSize();
				
				return renderer.render(angle, scale);
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
		
		/// <summary>{@inheritDoc} </summary>
		protected internal override void  fixSize()
		{
			if ((size = SizeFromCache).IsEmpty)
			{
				System.Drawing.Rectangle tempAux = new Rectangle(sop.getSize());
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				size = ImageUtils.transform(ref tempAux, scale, angle).Size;
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (o == null || o.GetType() != this.GetType())
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'op '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			RotateScaleOpSVGImpl op = (RotateScaleOpSVGImpl) o;
			return scale == op.Scale && angle == op.Angle && hints.Equals(op.Hints) && sop.Equals(op.sop);
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return hash;
		}
	}
}