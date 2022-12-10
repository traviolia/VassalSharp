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
using Map = VassalSharp.build.module.Map;
using GamePiece = VassalSharp.counters.GamePiece;
using KeyBuffer = VassalSharp.counters.KeyBuffer;
using PieceFinder = VassalSharp.counters.PieceFinder;
using Stack = VassalSharp.counters.Stack;
namespace VassalSharp.build.module.map
{
	
	public class StackExpander : Buildable
	{
		protected internal Map map;
		
		public virtual void  addTo(Buildable b)
		{
			map = (Map) b;
			map.addLocalMouseListener(this);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return doc.CreateElement(GetType().FullName);
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
		}
		
		public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isConsumed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (!e.isConsumed())
			{
				if (e.Clicks == 2)
				{
					GamePiece p = map.findPiece(new System.Drawing.Point(e.X, e.Y), VassalSharp.counters.PieceFinder_Fields.STACK_ONLY);
					if (p != null)
					{
						KeyBuffer.Buffer.clear();
						((Stack) p).setExpanded(!((Stack) p).isExpanded());
						KeyBuffer.Buffer.add(((Stack) p).topPiece());
					}
					//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.consume' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
					e.consume();
				}
			}
		}
	}
}