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
using Map = VassalSharp.build.module.Map;
using Command = VassalSharp.command.Command;
using GamePiece = VassalSharp.counters.GamePiece;
using Stack = VassalSharp.counters.Stack;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Handles the drawing of cards in a {@link VassalSharp.build.module.PlayerHand}.
	/// Lays out the cards horizontally with no overlap and even spacing.
	/// </summary>
	public class HandMetrics:StackMetrics
	{
		public HandMetrics():base(false, 12, 0, 12, 0)
		{
		}
		
		public override void  draw(Stack stack, System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			stack.setExpanded(true);
			base.draw(stack, g, x, y, obs, zoom);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override void  draw(Stack stack, ref System.Drawing.Point location, System.Drawing.Graphics g, Map map, double zoom, ref System.Drawing.Rectangle visibleRect)
		{
			stack.setExpanded(true);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			base.draw(stack, ref location, g, map, zoom, ref visibleRect);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal override void  nextPosition(ref System.Drawing.Point currentPos, ref System.Drawing.Rectangle currentBounds, ref System.Drawing.Point nextPos, ref System.Drawing.Rectangle nextBounds, int dx, int dy)
		{
			int x = currentPos.X + currentBounds.Width + dx;
			int y = currentPos.Y;
			nextBounds.Location = new System.Drawing.Point(x, y);
			//UPGRADE_TODO: Method 'java.awt.Point.setLocation' was converted to 'System.Drawing.Point.Point' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			nextPos = new System.Drawing.Point((System.Int32) x, (System.Int32) y);
		}
		
		public virtual Command merge(GamePiece fixed_Renamed, GamePiece moving)
		{
			Command c = base.merge(fixed_Renamed, moving);
			map.getView().revalidate();
			return c;
		}
	}
}