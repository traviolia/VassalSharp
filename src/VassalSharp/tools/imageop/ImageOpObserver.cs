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
//UPGRADE_TODO: The type 'VassalSharp.tools.opcache.OpObserver' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using OpObserver = VassalSharp.tools.opcache.OpObserver;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> <code>ImageOpObserver</code>s can be notified on the completion
	/// of an <code>ImageOp</code>.
	/// 
	/// <p>Objects implementing <code>ImageOpObserver</code> are intended to
	/// be passed as an argument to {@link ImageOp.getImage}, which which
	/// call back {@link #imageOpChange} on completion.</p>
	/// 
	/// <p>The most common implementaion of this interface is {@link Repainter},
	/// which can be used to repaint portions of {@link java.awt.Component}s
	/// which are waiting for images to be computed.</p>
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	/// <seealso cref="VassalSharp.tools.imageop.Repainter">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	interface ImageOpObserver extends OpObserver < BufferedImage >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	/// <summary> Called when the <code>ImageOp</code> under observation completes.
	/// 
	/// </summary>
	/// <param name="op">the <code>ImageOp</code> being observed
	/// </param>
	/// <param name="success"><code>true</code> iff the <code>op</code> succeeded
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void imageOpChange(ImageOp op, boolean success);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}