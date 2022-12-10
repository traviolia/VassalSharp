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
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using Buildable = VassalSharp.build.Buildable;
using Map = VassalSharp.build.module.Map;
using Deck = VassalSharp.counters.Deck;
using EventFilter = VassalSharp.counters.EventFilter;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceFinder = VassalSharp.counters.PieceFinder;
using Properties = VassalSharp.counters.Properties;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Centers the map when user right-clicks on an empty hex</summary>
	public class MapCenterer:AbstractBuildable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPieceInStack' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPieceInStack:VassalSharp.counters.PieceInStack
		{
			public AnonymousClassPieceInStack(MapCenterer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MapCenterer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MapCenterer enclosingInstance;
			public MapCenterer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public override System.Object visitDeck(Deck d)
			{
				System.Drawing.Point pos = d.Position;
				System.Drawing.Point p = new System.Drawing.Point(pt.X - pos.X, pt.Y - pos.Y);
				return d.Shape.IsVisible(p)?d:null;
			}
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		private Map map;
		private PieceFinder finder;
		
		public override void  addTo(Buildable b)
		{
			finder = createPieceFinder();
			map = (Map) b;
			map.addLocalMouseListener(this);
		}
		
		/// <summary> When the user right-clicks on the Map, the view will center on the location
		/// of the click unless this {@link PieceFinder} locates a piece there.
		/// </summary>
		/// <returns>
		/// </returns>
		protected internal virtual PieceFinder createPieceFinder()
		{
			return new AnonymousClassPieceInStack(this);
		}
		
		public override System.String getAttributeValueString(System.String attName)
		{
			return null;
		}
		
		public override void  setAttribute(System.String attName, System.Object value_Renamed)
		{
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (e.isMetaDown())
			{
				GamePiece found = map.findPiece(new System.Drawing.Point(e.X, e.Y), finder);
				if (found != null)
				{
					EventFilter filter = (EventFilter) found.getProperty(VassalSharp.counters.Properties_Fields.SELECT_EVENT_FILTER);
					if (filter != null && filter.rejectEvent(event_sender, e))
					{
						found = null;
					}
				}
				if (found == null)
				{
					Map.View m = (Map.View) event_sender;
					m.getMap().centerAt(new System.Drawing.Point(e.X, e.Y));
				}
			}
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
	}
}