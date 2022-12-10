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
using Deck = VassalSharp.counters.Deck;
using GamePiece = VassalSharp.counters.GamePiece;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Base class for PieceCollection implementation that organize
	/// pieces into distinct layers.  The layers are drawn in order of their index, i.e.
	/// layer 0 is on the bottom.
	/// </summary>
	public abstract class CompoundPieceCollection : PieceCollection
	{
		virtual public GamePiece[] AllPieces
		{
			get
			{
				return getPieces(true);
			}
			
		}
		virtual public int LayerCount
		{
			get
			{
				return layers.Length;
			}
			
		}
		virtual public int BottomLayer
		{
			get
			{
				return bottomLayer;
			}
			
			/*
			* Set a new bottom layer. Take care of wrapping around ends of layer list.
			*/
			
			set
			{
				bottomLayer = value;
				if (bottomLayer < 0)
					bottomLayer = LayerCount - 1;
				if (bottomLayer >= LayerCount)
					bottomLayer = 0;
			}
			
		}
		virtual public int TopLayer
		{
			get
			{
				int layer = bottomLayer - 1;
				if (layer < 0)
				{
					layer = LayerCount - 1;
				}
				return layer;
			}
			
		}
		protected internal SimplePieceCollection[] layers;
		protected internal int bottomLayer = 0;
		protected internal bool[] enabled;
		
		protected internal CompoundPieceCollection(int layerCount)
		{
			initLayers(layerCount);
		}
		
		protected internal virtual void  initLayers(int layerCount)
		{
			layers = new SimplePieceCollection[layerCount];
			enabled = new bool[layerCount];
			for (int i = 0; i < layers.Length; ++i)
			{
				layers[i] = new SimplePieceCollection();
				enabled[i] = true;
			}
		}
		
		public virtual int getLayerForPiece(GamePiece p)
		{
			return 0;
		}
		
		public virtual System.String getLayerNameForPiece(GamePiece p)
		{
			return "";
		}
		
		public virtual int getLayerForName(System.String layerName)
		{
			return - 1;
		}
		
		protected internal virtual PieceCollection getCollectionForPiece(GamePiece p)
		{
			return layers[getLayerForPiece(p)];
		}
		
		public virtual void  add(GamePiece p)
		{
			getCollectionForPiece(p).add(p);
		}
		
		public virtual void  clear()
		{
			for (int i = 0; i < layers.Length; ++i)
			{
				layers[i].clear();
			}
		}
		
		/*
		* Return pieces in layer order from the bottom up. Take into account
		* layer rotation and enabled state.
		*/
		public virtual GamePiece[] getPieces()
		{
			return getPieces(false);
		}
		
		protected internal virtual GamePiece[] getPieces(bool includeDisabled)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < GamePiece > l = new ArrayList < GamePiece >();
			int layer = bottomLayer;
			for (int i = 0; i < layers.Length; ++i)
			{
				if (includeDisabled || (!includeDisabled && enabled[layer]))
				{
					//UPGRADE_TODO: Method 'java.util.Arrays.asList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilArraysasList_javalangObject[]'"
					l.addAll(new System.Collections.ArrayList(layers[layer].getPieces()));
				}
				layer++;
				if (layer >= layers.Length)
				{
					layer = 0;
				}
			}
			return l.toArray(new GamePiece[l.size()]);
		}
		
		public virtual int indexOf(GamePiece p)
		{
			int layer = getLayerForPiece(p);
			int index = layers[layer].indexOf(p);
			if (index >= 0)
			{
				for (int i = 0; i < layer - 1; ++i)
				{
					index += layers[i].getPieces().Length;
				}
			}
			return index;
		}
		
		public virtual void  remove(GamePiece p)
		{
			getCollectionForPiece(p).remove(p);
		}
		
		public virtual void  moveToBack(GamePiece p)
		{
			getCollectionForPiece(p).moveToBack(p);
		}
		
		public virtual void  moveToFront(GamePiece p)
		{
			getCollectionForPiece(p).moveToFront(p);
		}
		
		public virtual bool canMerge(GamePiece p1, GamePiece p2)
		{
			bool canMerge = false;
			if (p1 is Deck || p2 is Deck)
			{
				canMerge = true;
			}
			else if (p1 is Stack)
			{
				if (p2 is Stack)
				{
					canMerge = canStacksMerge((Stack) p1, (Stack) p2);
				}
				else
				{
					canMerge = canStackAndPieceMerge((Stack) p1, p2);
				}
			}
			else if (p2 is Stack)
			{
				canMerge = canStackAndPieceMerge((Stack) p2, p1);
			}
			else
			{
				canMerge = canPiecesMerge(p1, p2);
			}
			return canMerge;
		}
		
		protected internal virtual bool canStacksMerge(Stack s1, Stack s2)
		{
			return canPiecesMerge(s1.topPiece(), s2.topPiece());
		}
		
		protected internal virtual bool canStackAndPieceMerge(Stack s, GamePiece p)
		{
			bool canMerge = false;
			GamePiece top = s.topPiece();
			if (top != null)
			{
				canMerge = canPiecesMerge(top, p);
			}
			return canMerge;
		}
		
		protected internal virtual bool canPiecesMerge(GamePiece p1, GamePiece p2)
		{
			bool canMerge = false;
			if (p1 != null && p2 != null)
			{
				canMerge = !true.Equals(p1.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && !true.Equals(p2.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && !true.Equals(p1.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) && !true.Equals(p2.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME));
			}
			return canMerge;
		}
		
		/*
		* Rotate layers up or down, optionally skipping top layers not containing
		* counters.
		*/
		public virtual void  rotate(bool rotateUp, bool skipNullLayers)
		{
			if (skipNullLayers)
			{
				for (int i = 0; i < layers.Length; i++)
				{
					rotate(rotateUp);
					if (layers[TopLayer].getPieces().Length > 0)
					{
						return ;
					}
				}
			}
			else
			{
				rotate(rotateUp);
			}
		}
		
		/*
		* Rotate Layers up or down by 1 layer
		*/
		public virtual void  rotate(bool rotateUp)
		{
			if (rotateUp)
			{
				BottomLayer = bottomLayer - 1;
			}
			else
			{
				BottomLayer = bottomLayer + 1;
			}
		}
		
		/*
		* Enable/Disable layers
		*/
		public virtual void  setLayerEnabled(int layer, bool b)
		{
			if (layer >= 0 && layer < layers.Length)
			{
				enabled[layer] = b;
			}
		}
		
		public virtual void  toggleLayerEnabled(int layer)
		{
			if (layer >= 0 && layer < layers.Length)
			{
				enabled[layer] = !enabled[layer];
			}
		}
		
		public virtual void  setLayerEnabled(System.String layer, bool b)
		{
			setLayerEnabled(getLayerForName(layer), b);
		}
		
		public virtual void  toggleLayerEnabled(System.String layer)
		{
			toggleLayerEnabled(getLayerForName(layer));
		}
		
		/*
		* Reset Layers to original state
		*/
		public virtual void  reset()
		{
			BottomLayer = 0;
			for (int i = 0; i < layers.Length; i++)
			{
				enabled[i] = true;
			}
		}
	}
}